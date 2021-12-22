using MasterServerToolkit.Logging;
using MasterServerToolkit.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MasterServerToolkit.MasterServer
{
    public class SpawnersModule : BaseServerModule
    {
        public delegate void SpawnedProcessRegistrationHandler(SpawnTask task, IPeer peer);

        private int _spawnerId = 0;
        private int _spawnTaskId = 0;

        protected Dictionary<int, RegisteredSpawner> spawnersList;
        protected Dictionary<int, SpawnTask> spawnTasksList;

        [Header("Permissions"), SerializeField, Tooltip("Minimal permission level, necessary to register a spanwer")]
        protected int createSpawnerPermissionLevel = 0;

        [Tooltip("How often spawner queues are updated"), SerializeField]
        protected float queueUpdateFrequency = 0.1f;

        [Tooltip("If true, clients will be able to request spawns"), SerializeField]
        protected bool enableClientSpawnRequests = true;

        public event Action<RegisteredSpawner> OnSpawnerRegisteredEvent;
        public event Action<RegisteredSpawner> OnSpawnerDestroyedEvent;
        public event SpawnedProcessRegistrationHandler OnSpawnedProcessRegisteredEvent;

        public override void Initialize(IServer server)
        {
            spawnersList = new Dictionary<int, RegisteredSpawner>();
            spawnTasksList = new Dictionary<int, SpawnTask>();

            // Add handlers
            server.RegisterMessageHandler((short)MstMessageCodes.RegisterSpawner, RegisterSpawnerRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.ClientsSpawnRequest, ClientsSpawnRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.RegisterSpawnedProcess, RegisterSpawnedProcessRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.CompleteSpawnProcess, CompleteSpawnProcessRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.ProcessStarted, SetProcessStartedRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.ProcessKilled, SetProcessKilledRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.AbortSpawnRequest, AbortSpawnRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.GetSpawnFinalizationData, GetCompletionDataRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.UpdateSpawnerProcessesCount, SetSpawnedProcessesCountRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.MakeMatch, MakeMatch_Handler);

            // Coroutines
            StartCoroutine(StartQueueUpdater());
        }

        /// <summary>
        /// Creates spawner for given peer using options
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual RegisteredSpawner CreateSpawner(IPeer peer, SpawnerOptions options)
        {
            // Create registered spawner instance
            var spawnerInstance = new RegisteredSpawner(GenerateSpawnerId(), peer, options);

            // Find spawners in peer property
            Dictionary<int, RegisteredSpawner> peerSpawners = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredSpawners) as Dictionary<int, RegisteredSpawner>;

            // If this is the first time registering a spawners
            if (peerSpawners == null)
            {
                // Save the dictionary
                peerSpawners = new Dictionary<int, RegisteredSpawner>();
                peer.SetProperty((int)MstPeerPropertyCodes.RegisteredSpawners, peerSpawners);

                // Listen to disconnection
                peer.OnPeerDisconnectedEvent += OnRegisteredPeerDisconnect;
            }

            // Add a new spawner
            peerSpawners[spawnerInstance.SpawnerId] = spawnerInstance;

            // Add the spawner to a list of all spawners
            spawnersList[spawnerInstance.SpawnerId] = spawnerInstance;

            // Invoke the event
            OnSpawnerRegisteredEvent?.Invoke(spawnerInstance);

            return spawnerInstance;
        }

        /// <summary>
        /// Invokes when peer disconnected from server
        /// </summary>
        /// <param name="peer"></param>
        private void OnRegisteredPeerDisconnect(IPeer peer)
        {
            // Get registered spawners from peer property
            var peerSpawners = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredSpawners) as Dictionary<int, RegisteredSpawner>;

            // Return if now spawner found
            if (peerSpawners == null)
            {
                return;
            }

            // Create a copy so that we can iterate safely
            var registeredSpawners = peerSpawners.Values.ToList();

            // Destroy all spawners
            foreach (var registeredSpawner in registeredSpawners)
            {
                DestroySpawner(registeredSpawner);
            }
        }

        /// <summary>
        /// Destroys spawner
        /// </summary>
        /// <param name="spawner"></param>
        public void DestroySpawner(RegisteredSpawner spawner)
        {
            // Get spawner owner peer
            var peer = spawner.Peer;

            // If peer exists
            if (peer != null)
            {
                // Get spawners from peer property
                var peerSpawners = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredSpawners) as Dictionary<int, RegisteredSpawner>;

                // Remove the spawner from peer
                if (peerSpawners != null)
                    peerSpawners.Remove(spawner.SpawnerId);
            }

            // Remove the spawner from all spawners
            spawnersList.Remove(spawner.SpawnerId);

            // Invoke the event
            OnSpawnerDestroyedEvent?.Invoke(spawner);
        }

        /// <summary>
        /// Creates unique spawner id
        /// </summary>
        /// <returns></returns>
        public int GenerateSpawnerId()
        {
            return _spawnerId++;
        }

        /// <summary>
        /// Creates unique spawner tsak id
        /// </summary>
        /// <returns></returns>
        public int GenerateSpawnTaskId()
        {
            return _spawnTaskId++;
        }

        /// <summary>
        /// Start process on spawner side with given spawn options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SpawnTask Spawn(MstProperties options)
        {
            return Spawn(options, string.Empty);
        }

        /// <summary>
        /// Start process on spawner side with given spawn <paramref name="options"/>, <paramref name="region"/> and <paramref name="customOptions"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="region"></param>
        /// <param name="customOptions"></param>
        /// <returns></returns>
        public virtual SpawnTask Spawn(MstProperties options, string region)
        {
            // Get registered spawner by options and region
            var spawners = GetFilteredSpawners(options, region);


            if (spawners.Count == 0)
            {
                logger.Warn($"No spawner was returned after filtering. Region: {options.AsString(MstDictKeys.ROOM_REGION, "International")}");
                return null;
            }

            // Order from least busy server
            var orderedSpawners = spawners.OrderByDescending(s => s.CalculateFreeSlotsCount());
            var availableSpawner = orderedSpawners.FirstOrDefault(s => s.CanSpawnAnotherProcess());

            // Ignore, if all of the spawners are busy
            if (availableSpawner == null)
            {
                return null;
            }

            return Spawn(options, availableSpawner);
        }

        /// <summary>
        /// Start process on spawner side with given spawn <paramref name="options"/>, <paramref name="customOptions"/> and <paramref name="spawner"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="customOptions"></param>
        /// <param name="spawner"></param>
        /// <returns></returns>
        public virtual SpawnTask Spawn(MstProperties options, RegisteredSpawner spawner)
        {
            // Create new spawn task
            var task = new SpawnTask(GenerateSpawnTaskId(), spawner, options);

            // List this task
            spawnTasksList[task.Id] = task;

            // Add this task to queue
            spawner.AddTaskToQueue(task);

            logger.Debug("Spawner was found, and spawn task created: " + task);

            return task;
        }

        /// <summary>
        /// Retrieves a list of spawner that can be used with given properties and region name
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public virtual List<RegisteredSpawner> GetFilteredSpawners(MstProperties properties, string region)
        {
            return GetSpawners(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<RegisteredSpawner> GetSpawners()
        {
            return GetSpawners(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public virtual List<RegisteredSpawner> GetSpawners(string region)
        {
            // If region is not provided, retrieve all spawners
            if (string.IsNullOrEmpty(region))
            {
                return spawnersList.Values.ToList();
            }

            return GetSpawnersInRegion(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public virtual List<RegisteredSpawner> GetSpawnersInRegion(string region)
        {
            return spawnersList.Values
                .Where(s => s.Options.Region == region)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RegionInfo> GetRegions()
        {
            var list = new List<RegionInfo>();
            var regions = spawnersList.Values.Select(i => new RegionInfo()
            {
                Name = i.Options.Region,
                Ip = i.Options.MachineIp
            });

            foreach (var region in regions)
            {
                if (!list.Contains(region))
                {
                    list.Add(region);
                }
            }

            return list;
        }

        /// <summary>
        /// Returns true, if peer has permissions to register a spawner
        /// </summary>
        /// <param name="peer"></param>
        /// <returns></returns>
        protected virtual bool HasCreationPermissions(IPeer peer)
        {
            var extension = peer.GetExtension<SecurityInfoPeerExtension>();
            return extension != null && extension.PermissionLevel >= createSpawnerPermissionLevel;
        }

        protected virtual bool CanClientSpawn(IPeer peer, MstProperties options)
        {
            return enableClientSpawnRequests;
        }

        protected virtual IEnumerator StartQueueUpdater()
        {
            while (true)
            {
                yield return new WaitForSeconds(queueUpdateFrequency);

                foreach (var spawner in spawnersList.Values)
                {
                    try
                    {
                        spawner.UpdateQueue();
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                    }
                }
            }
        }

        #region Message Handlers

        protected virtual void MakeMatch_Handler(IIncomingMessage message)
        {
            var New_options = MstProperties.FromBytes(message.AsBytes()); // < new Mst use this ?

            RoomOptions room = message.Deserialize(new RoomOptions());
            short OP_Code1 = room.CustomOptions.AsShort("OP_Code");
            Debug.Log("MakeMatch_Handler || OP_Code || " + OP_Code1);

            int Match_Type = message.AsInt();
            int MaxConnections = 10;
            string RoomName = "new room";
            string Password = string.Empty;
            string RegionName = string.Empty;

            var spawnOptions = new MstProperties();
            //spawnOptions.Add(MstDictKeys.ROOM_MAX_PLAYERS, MaxConnections);
            spawnOptions.Add(MstDictKeys.ROOM_NAME, RoomName);
            spawnOptions.Add(MstDictKeys.ROOM_PASSWORD, Password);

            var customSpawnOptions = new MstProperties();
            customSpawnOptions.Add(Mst.Args.Names.StartClientConnection);

            RoomOptions options = message.Deserialize(new RoomOptions());
            short OP_Code = options.CustomOptions.AsShort("OP_Code");

            customSpawnOptions.Add("OP_Code", OP_Code);

            //Mst.Client.Spawners.RequestSpawn(spawnOptions, customSpawnOptions, regionName, (controller, error)

            // Set region to room by filter. If region is empty the room will be international
            spawnOptions.Set(MstDictKeys.ROOM_REGION, string.IsNullOrEmpty(RegionName) ? string.Empty : RegionName);

            // Create spawn request message packet
            var data = new SpawnRequestPacket()
            {
                // Options for spawners module
                Options = spawnOptions,

                // Options that will be send to room
                CustomOptions = customSpawnOptions
            };

            // "ClientsSpawnRequestHandler
            // "ClientsSpawnRequestHandler
            // "ClientsSpawnRequestHandler

            var spawnRequestData = data.ToString();
            var peer = message.Peer;

            logger.Info($"Client {peer.Id} requested to spawn room with options: {spawnRequestData}");

            if (spawnersList.Count == 0)
            {
                logger.Error("But no registered spawner was found!");
                message.Respond("No registered spawner was found", ResponseStatus.Failed);
                return;
            }

            // Check if current request is authorized
            if (!CanClientSpawn(peer, New_options))
            {
                logger.Error("Unauthorized request");
                // Client can't spawn
                message.Respond("Unauthorized", ResponseStatus.Unauthorized);
                return;
            }

            // Try to find existing request to prevent new one
            SpawnTask prevRequest = peer.GetProperty((int)MstPeerPropertyCodes.ClientSpawnRequest) as SpawnTask;

            if (prevRequest != null && !prevRequest.IsDoneStartingProcess)
            {
                logger.Warn("And he already has an active request");
                // Client has unfinished request
                message.Respond("You already have an active request", ResponseStatus.Failed);
                return;
            }

            // Create a new spawn task
            //var task = Spawn(data.Options, data.Options.AsString(MstDictKeys.ROOM_REGION), data.CustomOptions); < Old Version
            var task = Spawn(data.Options, data.Options.AsString(MstDictKeys.ROOM_REGION));

            // If spawn task is not created
            if (task == null)
            {
                logger.Warn("But all the servers are busy. Let him try again later");
                message.Respond("All the servers are busy. Try again later".ToBytes(), ResponseStatus.Failed);
                return;
            }

            // Save spawn task requester
            task.Requester = message.Peer;

            // Save the task as peer property
            peer.SetProperty((int)MstPeerPropertyCodes.ClientSpawnRequest, task);

            // Listen to status changes
            task.OnStatusChangedEvent += (status) =>
            {
                // Send status update
                var msg = Mst.Create.Message((short)MstMessageCodes.SpawnRequestStatusChange, new SpawnStatusUpdatePacket()
                {
                    SpawnId = task.Id,
                    Status = status
                });

                if (task.Requester != null && task.Requester.IsConnected)
                {
                    message.Peer.SendMessage(msg);
                    byte data_Type = (byte)Match_Type;
                    //Mst.Server.Connection.SendMessage((short)MstMessageCodes.MakeMatchFinish, data_Type);
                }
            };
        }

        /// <summary>
        /// Fired whe connected client has made request to spawn process
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ClientsSpawnRequestHandler(IIncomingMessage message)
        {
            // Parse data from message
            var options = MstProperties.FromBytes(message.AsBytes());
            var peer = message.Peer;

            logger.Info($"Client {peer.Id} requested to spawn room with options: {options}");

            if (spawnersList.Count == 0)
            {
                logger.Error("But no registered spawner was found!");
                message.Respond("No registered spawner was found", ResponseStatus.Failed);
                return;
            }

            // Check if current request is authorized
            if (!CanClientSpawn(peer, options))
            {
                logger.Error("Unauthorized request");
                // Client can't spawn
                message.Respond("Unauthorized", ResponseStatus.Unauthorized);
                return;
            }

            // Try to find existing request to prevent new one
            SpawnTask prevRequest = peer.GetProperty((int)MstPeerPropertyCodes.ClientSpawnRequest) as SpawnTask;

            if (prevRequest != null && !prevRequest.IsDoneStartingProcess)
            {
                logger.Warn("And he already has an active request");
                // Client has unfinished request
                message.Respond("You already have an active request", ResponseStatus.Failed);
                return;
            }

            // Create a new spawn task
            var task = Spawn(options, options.AsString(MstDictKeys.ROOM_REGION));

            // If spawn task is not created
            if (task == null)
            {
                logger.Warn("But all the servers are busy. Let him try again later");
                message.Respond("All the servers are busy. Try again later".ToBytes(), ResponseStatus.Failed);
                return;
            }

            // Save spawn task requester
            task.Requester = message.Peer;

            // Save the task as peer property
            peer.SetProperty((int)MstPeerPropertyCodes.ClientSpawnRequest, task);

            // Listen to status changes
            task.OnStatusChangedEvent += (status) =>
            {
                // Send status update
                var msg = Mst.Create.Message((short)MstMessageCodes.SpawnRequestStatusChange, new SpawnStatusUpdatePacket()
                {
                    SpawnId = task.Id,
                    Status = status
                });

                if (task.Requester != null && task.Requester.IsConnected)
                {
                    message.Peer.SendMessage(msg);
                }
            };

            message.Respond(task.Id, ResponseStatus.Success);
        }

        private void AbortSpawnRequestHandler(IIncomingMessage message)
        {
            var prevRequest = message.Peer.GetProperty((int)MstPeerPropertyCodes.ClientSpawnRequest) as SpawnTask;

            if (prevRequest == null)
            {
                message.Respond("There's nothing to abort", ResponseStatus.Failed);
                return;
            }

            if (prevRequest.Status >= SpawnStatus.Finalized)
            {
                message.Respond("You can't abort a completed request", ResponseStatus.Failed);
                return;
            }

            if (prevRequest.Status <= SpawnStatus.None)
            {
                message.Respond("Already aborting", ResponseStatus.Success);
                return;
            }

            logger.Debug($"Client [{message.Peer.Id}] requested to terminate process [{prevRequest.Id}]");

            prevRequest.Abort();

            message.Respond(ResponseStatus.Success);
        }

        protected virtual void GetCompletionDataRequestHandler(IIncomingMessage message)
        {
            var spawnId = message.AsInt();

            if (!spawnTasksList.TryGetValue(spawnId, out SpawnTask task))
            {
                message.Respond("Invalid request", ResponseStatus.Failed);
                return;
            }

            if (task.Requester != message.Peer)
            {
                message.Respond("You're not the requester", ResponseStatus.Unauthorized);
                return;
            }

            if (task.FinalizationPacket == null)
            {
                message.Respond("Task has no completion data", ResponseStatus.Failed);
                return;
            }

            // Respond with data (dictionary of strings)
            message.Respond(task.FinalizationPacket.FinalizationData.ToBytes(), ResponseStatus.Success);
        }

        protected virtual void RegisterSpawnerRequestHandler(IIncomingMessage message)
        {
            logger.Debug($"Client [{message.Peer.Id}] requested to be registered as spawner");

            // Check if peer has permissions to register spawner
            if (!HasCreationPermissions(message.Peer))
            {
                message.Respond("Insufficient permissions", ResponseStatus.Unauthorized);
                return;
            }

            // Read options
            var options = message.Deserialize(new SpawnerOptions());

            // Create new spawner
            var spawner = CreateSpawner(message.Peer, options);

            logger.Debug($"Client [{message.Peer.Id}] was successfully registered as spawner [{spawner.SpawnerId}] with options: {options}");

            // Respond with spawner id
            message.Respond(spawner.SpawnerId, ResponseStatus.Success);
        }

        /// <summary>
        /// Handles a message from spawned process. Spawned process send this message
        /// to notify server that it was started
        /// </summary>
        /// <param name="message"></param>
        protected virtual void RegisterSpawnedProcessRequestHandler(IIncomingMessage message)
        {
            var data = message.Deserialize(new RegisterSpawnedProcessPacket());

            // Try get psawn task by ID
            if (!spawnTasksList.TryGetValue(data.SpawnId, out SpawnTask task))
            {
                message.Respond("Invalid spawn task", ResponseStatus.Failed);
                logger.Error("Process tried to register to an unknown task");
                return;
            }

            // Check spawn task unique code
            if (task.UniqueCode != data.SpawnCode)
            {
                message.Respond("Unauthorized", ResponseStatus.Unauthorized);
                logger.Error("Spawned process tried to register, but failed due to mismaching unique code");
                return;
            }

            // Set task as registered
            task.OnRegistered(message.Peer);

            // Invoke event
            OnSpawnedProcessRegisteredEvent?.Invoke(task, message.Peer);

            // Respon to requester
            message.Respond(task.Options.ToDictionary().ToBytes(), ResponseStatus.Success);
        }

        protected virtual void CompleteSpawnProcessRequestHandler(IIncomingMessage message)
        {
            var data = message.Deserialize(new SpawnFinalizationPacket());

            if (spawnTasksList.TryGetValue(data.SpawnTaskId, out SpawnTask task))
            {
                if (task.RegisteredPeer != message.Peer)
                {
                    message.Respond("Unauthorized", ResponseStatus.Unauthorized);
                    logger.Error("Spawned process tried to complete spawn task, but it's not the same peer who registered to the task");
                }
                else
                {
                    task.OnFinalized(data);
                    message.Respond(ResponseStatus.Success);
                }
            }
            else
            {
                message.Respond("Invalid spawn task", ResponseStatus.Failed);
                logger.Error("Process tried to complete to an unknown task");
            }
        }

        protected virtual void SetProcessKilledRequestHandler(IIncomingMessage message)
        {
            var spawnId = message.AsInt();

            if (spawnTasksList.TryGetValue(spawnId, out SpawnTask task))
            {
                task.OnProcessKilled();
                task.Spawner.OnProcessKilled();
            }
        }

        protected virtual void SetProcessStartedRequestHandler(IIncomingMessage message)
        {
            var spawnId = message.AsInt();

            if (spawnTasksList.TryGetValue(spawnId, out SpawnTask task))
            {
                task.OnProcessStarted();
                task.Spawner.OnProcessStarted();
            }
        }

        private void SetSpawnedProcessesCountRequestHandler(IIncomingMessage message)
        {
            var packet = message.Deserialize(new IntPairPacket());

            if (spawnersList.TryGetValue(packet.A, out RegisteredSpawner spawner))
            {
                spawner.UpdateProcessesCount(packet.B);
            }
        }

        #endregion
    }
}

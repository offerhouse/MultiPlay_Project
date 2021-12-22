using MasterServerToolkit.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MasterServerToolkit.MasterServer
{
    public class RoomsModule : BaseServerModule, IGamesProvider
    {
        #region Unity Inspector

        [Header("Permissions")]
        [Tooltip("Minimal permission level, necessary to register a room")]
        [SerializeField]
        protected int registerRoomPermissionLevel = 0;

        #endregion

        /// <summary>
        /// ID of the last created room
        /// </summary>
        private int lastRoomId = 0;

        /// <summary>
        /// Registered rooms list
        /// </summary>
        protected Dictionary<int, RegisteredRoom> roomsList;

        /// <summary>
        /// Fired when new room is registered
        /// </summary>
        public event Action<RegisteredRoom> OnRoomRegisteredEvent;

        /// <summary>
        /// Fired when existing room is destroyed
        /// </summary>
        public event Action<RegisteredRoom> OnRoomDestroyedEvent;

        protected ProfilesModule ProfilesModule;

        /// <summary>
        /// Get next room id
        /// </summary>
        /// <returns></returns>
        public int NextRoomId => lastRoomId++;

        protected override void Awake()
        {
            base.Awake();

            roomsList = new Dictionary<int, RegisteredRoom>();
        }

        public override void Initialize(IServer server)
        {
            // Add handlers
            server.RegisterMessageHandler((short)MstMessageCodes.RegisterRoomRequest, RegisterRoomRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.DestroyRoomRequest, DestroyRoomRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.SaveRoomOptionsRequest, SaveRoomOptionsRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.GetRoomAccessRequest, GetRoomAccessRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.ValidateRoomAccessRequest, ValidateRoomAccessRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.PlayerLeftRoomRequest, PlayerLeftRoomRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Mirror_To_Master, Mirror_To_MasterHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Master_To_Mirror, Master_To_MirrorHandler);

            // Auth dependency setup
            ProfilesModule = server.GetModule<ProfilesModule>();

            // Maintain unconfirmed accesses
            InvokeRepeating(nameof(CleanUnconfirmedAccesses), 1f, 1f);
        }

        /// <summary>
        /// Cleans up the list of unconfirmed accesses
        /// </summary>
        private void CleanUnconfirmedAccesses()
        {
            foreach (var registeredRoom in roomsList.Values)
            {
                registeredRoom.ClearTimedOutAccesses();
            }
        }

        /// <summary>
        /// Returns true, if peer has permissions to register a game server
        /// </summary>
        /// <param name="peer"></param>
        /// <returns></returns>
        protected virtual bool HasRoomRegistrationPermissions(IPeer peer)
        {
            var extension = peer.GetExtension<SecurityInfoPeerExtension>();
            return extension.PermissionLevel >= registerRoomPermissionLevel;
        }

        /// <summary>
        /// Fired when registered room peer disconnected from master
        /// </summary>
        /// <param name="peer"></param>
        private void OnRegisteredPeerDisconnect(IPeer peer)
        {
            Dictionary<int, RegisteredRoom> peerRooms = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredRooms) as Dictionary<int, RegisteredRoom>;

            if (peerRooms == null)
            {
                return;
            }

            logger.Debug($"Client {peer.Id} was disconnected from server and it has registered rooms that also must be destroyed");

            // Create a copy so that we can iterate safely
            var registeredRooms = peerRooms.Values.ToList();

            foreach (var registeredRoom in registeredRooms)
            {
                DestroyRoom(registeredRoom);
            }
        }

        /// <summary>
        /// Registers a room to the server
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual RegisteredRoom RegisterRoom(IPeer peer, RoomOptions options)
        {
            short OP_Code = options.CustomOptions.AsShort("OP_Code");
            Debug.Log("RoomsModule || RegisterRoom || " + peer.Id + " || OP_Code || " + OP_Code); // < this peer.Id is rooms
            var room = new RegisteredRoom(NextRoomId, peer, options);
            Dictionary<int, RegisteredRoom> peerRooms = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredRooms) as Dictionary<int, RegisteredRoom>;

            if (peerRooms == null)
            {
                // If this is the first time creating a room

                // Save the dictionary
                peerRooms = new Dictionary<int, RegisteredRoom>();
                peer.SetProperty((int)MstPeerPropertyCodes.RegisteredRooms, peerRooms);

                // Listen to disconnect event
                peer.OnPeerDisconnectedEvent += OnRegisteredPeerDisconnect;
            }

            // Add a new room to peer
            peerRooms[room.RoomId] = room;

            // Add the room to a list of all rooms
            roomsList[room.RoomId] = room;

            // Invoke the event
            OnRoomRegisteredEvent?.Invoke(room);

            return room;
        }

        /// <summary>
        /// Unregisters a room from a server
        /// </summary>
        /// <param name="room"></param>
        public virtual void DestroyRoom(RegisteredRoom room)
        {
            var peer = room.Peer;

            if (peer != null)
            {
                var peerRooms = peer.GetProperty((int)MstPeerPropertyCodes.RegisteredRooms) as Dictionary<int, RegisteredRoom>;

                // Remove the room from peer
                if (peerRooms != null)
                {
                    peerRooms.Remove(room.RoomId);
                }
            }

            // Remove the room from all rooms
            roomsList.Remove(room.RoomId);
            room.Destroy();

            short room_ID = Convert.ToInt16(room.RoomId);
            RoomOptions option = new RoomOptions();
            option.CustomOptions.Add("Room_ID", room_ID);
            Mst.Server.Connection.SendMessage((short)MstMessageCodes.Destroy_Room, option);

            logger.Debug($"Room {room.RoomId} has been successfully destroyed");

            // Invoke the event
            OnRoomDestroyedEvent?.Invoke(room);
        }

        /// <summary>
        /// There are times when you need to change registered room options. This method will help you :)
        /// </summary>
        /// <param name="room"></param>
        /// <param name="options"></param>
        public virtual void ChangeRoomOptions(RegisteredRoom room, RoomOptions options)
        {
            room.ChangeOptions(options);
        }

        /// <summary>
        /// Returns list of all public rooms by given filter
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual IEnumerable<GameInfoPacket> GetPublicGames(IPeer peer, MstProperties filters)
        {
            var rooms = filters != null && filters.Has(MstDictKeys.ROOM_ID)
                ? roomsList.Values.Where(r => (r.Players.ContainsKey(peer.Id) || r.Options.IsPublic) && r.RoomId == filters.AsInt(MstDictKeys.ROOM_ID))
                : roomsList.Values.Where(r => r.Options.IsPublic);
            var games = new List<GameInfoPacket>();

            foreach (var room in rooms)
            {
                var game = new GameInfoPacket
                {
                    Id = room.RoomId,
                    Address = room.Options.RoomIp + ":" + room.Options.RoomPort,
                    MaxPlayers = room.Options.MaxConnections,
                    Name = room.Options.Name,
                    OnlinePlayers = room.OnlineCount,
                    Properties = GetPublicRoomOptions(peer, room, filters),
                    IsPasswordProtected = !string.IsNullOrEmpty(room.Options.Password),
                    Type = GameInfoType.Room,
                    Region = room.Options.Region
                };

                var players = room.Players.Values.Where(pl => pl.HasExtension<IUserPeerExtension>()).Select(pl => pl.GetExtension<IUserPeerExtension>().Username);
                game.OnlinePlayersList = players.ToList();
                games.Add(game);
            }

            return games;
        }

        /// <summary>
        /// Returns list of properties of public room
        /// </summary>
        /// <param name="player"></param>
        /// <param name="room"></param>
        /// <param name="playerFilters"></param>
        /// <returns></returns>
        public virtual MstProperties GetPublicRoomOptions(IPeer player, RegisteredRoom room, MstProperties playerFilters)
        {
            return room.Options.CustomOptions;
        }

        /// <summary>
        /// Returns room by given id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public RegisteredRoom GetRoom(int roomId)
        {
            roomsList.TryGetValue(roomId, out RegisteredRoom r);
            return r;
        }

        /// <summary>
        /// Returns the list of all registered rooms
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegisteredRoom> GetAllRooms()
        {
            return roomsList.Values;
        }

        /// <summary>
        /// Get room players list
        /// </summary>
        /// <param name="roomId"></param>
        public IEnumerable<IPeer> GetPlayersOfRoom(int roomId)
        {
            var r = GetRoom(roomId);
            return r?.Players.Values;
        }

        #region Message Handlers

        protected virtual void RegisterRoomRequestHandler(IIncomingMessage message)
        {
            Debug.Log("RegisterRoomRequestHandler");
            // OP_Code = 0;
            logger.Debug($"Client {message.Peer.Id} requested to register new room server");

            if (!HasRoomRegistrationPermissions(message.Peer))
            {
                logger.Debug($"But it has no permission");
                message.Respond("Insufficient permissions", ResponseStatus.Unauthorized);
                return;
            }

            var options = message.Deserialize(new RoomOptions());
            var room = RegisterRoom(message.Peer, options);

            logger.Debug($"Room {room.RoomId} has been successfully registered with options: {options}");

            // Respond with a room id

            options.CustomOptions.Set("Room_ID", room.RoomId);
            message.Respond(room.RoomId, ResponseStatus.Success);

            Mst.Server.Connection.SendMessage((short)MstMessageCodes.MakeMatchFinish, options);
            Debug.Log("Create_Room_Finish || " + options.OP_Code + " || " + options.CustomOptions.AsShort("Room_ID"));
        }

        protected virtual void DestroyRoomRequestHandler(IIncomingMessage message)
        {
            var roomId = message.AsInt();

            logger.Debug($"Client {message.Peer.Id} requested to destroy room server with id {roomId}");

            if (!roomsList.TryGetValue(roomId, out RegisteredRoom room))
            {
                logger.Debug($"But this room does not exist");
                message.Respond("Room does not exist", ResponseStatus.Failed);
                return;
            }

            if (message.Peer != room.Peer)
            {
                logger.Debug($"But it is not the creator of the room");
                message.Respond("You're not the creator of the room", ResponseStatus.Unauthorized);
                return;
            }

            DestroyRoom(room);

            message.Respond(ResponseStatus.Success);
        }

        protected virtual void ValidateRoomAccessRequestHandler(IIncomingMessage message)
        {
            // Parse message
            var data = message.Deserialize(new RoomAccessValidatePacket());

            // Trying to find room in list of registered
            if (!roomsList.TryGetValue(data.RoomId, out RegisteredRoom room))
            {
                message.Respond("Room does not exist", ResponseStatus.Failed);
                return;
            }

            // if this message is not received from owner of room
            if (message.Peer != room.Peer)
            {
                // Wrong peer of room registrar
                message.Respond("You're not the registrar of the room", ResponseStatus.Unauthorized);
                return;
            }

            // Trying to validate room access token
            if (!room.ValidateAccess(data.Token, out IPeer playerPeer))
            {
                message.Respond("Failed to confirm the access", ResponseStatus.Unauthorized);
                return;
            }

            var packet = new UsernameAndPeerIdPacket()
            {
                PeerId = playerPeer.Id
            };

            // Add username if available
            var userExt = playerPeer.GetExtension<IUserPeerExtension>();
            if (userExt != null)
            {
                packet.Username = userExt.Username ?? "";
            }

            // Respond with success and player's peer id
            message.Respond(packet, ResponseStatus.Success);
        }

        protected virtual void SaveRoomOptionsRequestHandler(IIncomingMessage message)
        {
            var data = message.Deserialize(new SaveRoomOptionsPacket());

            if (!roomsList.TryGetValue(data.RoomId, out RegisteredRoom room))
            {
                message.Respond("Room does not exist", ResponseStatus.Failed);
                return;
            }

            if (message.Peer != room.Peer)
            {
                // Wrong peer unregistering the room
                message.Respond("You're not the creator of the room", ResponseStatus.Unauthorized);
                return;
            }

            ChangeRoomOptions(room, data.Options);
            message.Respond(ResponseStatus.Success);
        }

        protected virtual void GetRoomAccessRequestHandler(IIncomingMessage message)
        {
            var data = message.Deserialize(new RoomAccessRequestPacket());

            // Let's find a room by Id which the player wants to join
            if (!roomsList.TryGetValue(data.RoomId, out RegisteredRoom room))
            {
                message.Respond("Room does not exist", ResponseStatus.Failed);
                return;
            }

            // If room requires the password and given password is not valid
            if (!string.IsNullOrEmpty(room.Options.Password) && room.Options.Password != data.Password)
            {
                message.Respond("Invalid password", ResponseStatus.Unauthorized);
                return;
            }

            // Send room access request to peer who owns it
            room.GetAccess(message.Peer, data.CustomOptions, (packet, error) =>
            {
                if (packet == null)
                {
                    message.Respond(error, ResponseStatus.Unauthorized);
                    return;
                }

                message.Respond(packet, ResponseStatus.Success);
            });
        }

        protected virtual void PlayerLeftRoomRequestHandler(IIncomingMessage message)
        {
            var data = message.Deserialize(new PlayerLeftRoomPacket());
            Debug.Log("PlayerLeftRoomRequestHandler || " + data.PeerId);

            if (!roomsList.TryGetValue(data.RoomId, out RegisteredRoom room))
            {
                message.Respond("Room does not exist", ResponseStatus.Failed);
                return;
            }

            if (message.Peer != room.Peer)
            {
                // Wrong peer unregistering the room
                message.Respond("You're not the creator of the room", ResponseStatus.Unauthorized);
                return;
            }

            IPeer peer = message.Peer;
            peer.SendMessage((short)MstMessageCodes.Player_Left_Room);

            room.RemovePlayer(data.PeerId);
            message.Respond(ResponseStatus.Success);
        }

        protected virtual void Mirror_To_MasterHandler(IIncomingMessage message)
        {
            Debug.Log("Mirror_To_MasterHandler");
        }

        protected virtual void Master_To_MirrorHandler(IIncomingMessage message)
        {
            Debug.Log("Master_To_MirrorHandler");
        }

        #endregion
    }
}



using MasterServerToolkit.Logging;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Networking;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MasterServerToolkit.Games
{
    public class MatchmakingBehaviour : BaseClientBehaviour
    {
        #region INSPECTOR

        /// <summary>
        /// Time to wait before match creation process will be aborted
        /// </summary>
        [SerializeField, Tooltip("Time to wait before match creation process will be aborted")]
        protected uint matchCreationTimeout = 60;

        public UnityEvent OnRoomStartedEvent;
        public UnityEvent OnRoomStartAbortedEvent;

        #endregion

        private static MatchmakingBehaviour _instance;

        public static MatchmakingBehaviour Instance
        {
            get
            {
                if (!_instance) Logs.Error("Instance of MatchmakingBehaviour is not found");
                return _instance;
            }
        }

        protected override void Awake()
        {
            if (_instance)
            {
                Destroy(_instance.gameObject);
                return;
            }

            _instance = this;

            base.Awake();
        }

        protected override void OnInitialize()
        {
            // Set cliet mode
            Mst.Client.Rooms.ForceClientMode = true;

            // Set MSF global options
            Mst.Options.Set(MstDictKeys.AUTOSTART_ROOM_CLIENT, true);
            Mst.Options.Set(MstDictKeys.ROOM_OFFLINE_SCENE_NAME, SceneManager.GetActiveScene().name);

            SetHandler((short)MstMessageCodes.Local_Join_Game_1v1, Local_Join_Game_1v1Handler);
            SetHandler((short)MstMessageCodes.Local_Rejoin_Game, Local_Rejoin_Game_Handler);
            SetHandler((short)MstMessageCodes.Player_Left_Room, Player_Left_Room_Handler);
            SetHandler((short)MstMessageCodes.Destroy_Room, Destroy_Room_Handler);
        }

        /// <summary>
        /// Tries to get access to room
        /// </summary>
        /// <param name="gameInfo"></param>
        /// <param name="password"></param>
        protected virtual void GetAccess(GameInfoPacket gameInfo, string password = "")
        {
            Debug.Log("GetAccess");
            Mst.Client.Rooms.GetAccess(gameInfo.Id, password, (access, error) =>
            {
                if (!string.IsNullOrEmpty(error))
                {
                    logger.Error(error);
                    Mst.Events.Invoke(MstEventKeys.showOkDialogBox, new OkDialogBoxEventMessage(error, null));
                }
            });
        }

        /// <summary>
        /// Sends request to master server to start new room process
        /// </summary>
        /// <param name="spawnOptions"></param>
        public virtual void CreateNewRoom(string regionName, MstProperties spawnOptions)
        {
            Debug.Log("CreateNewRoom || " + gameObject.name);
            Mst.Events.Invoke(MstEventKeys.showLoadingInfo, "Starting room... Please wait!");

            logger.Debug("Starting room... Please wait!");

            // Custom options that will be given to room directly
            var customSpawnOptions = new MstProperties();
            customSpawnOptions.Add(Mst.Args.Names.StartClientConnection, true);

            // Here is the example of using custom options. If your option name starts from "-room."
            // then this option will be added to custom room options on server automatically
            customSpawnOptions.Add("-room.CustomTextOption", "Here is room custom option");
            customSpawnOptions.Add("-room.CustomIdOption", Mst.Helper.CreateID_10());
            customSpawnOptions.Add("-room.CustomDateTimeOption", DateTime.Now.ToString());

            Mst.Client.Spawners.RequestSpawn(spawnOptions, customSpawnOptions, regionName, (controller, error) =>
            {
                if (controller == null)
                {
                    Mst.Events.Invoke(MstEventKeys.hideLoadingInfo);
                    Mst.Events.Invoke(MstEventKeys.showOkDialogBox, new OkDialogBoxEventMessage(error, null));
                    return;
                }

                Mst.Events.Invoke(MstEventKeys.showLoadingInfo, "Room started. Finalizing... Please wait!");

                // Wait for spawning status until it is finished
                // This status must be send by room
                MstTimer.WaitWhile(() =>
                {
                    return controller.Status != SpawnStatus.Finalized;
                }, (isSuccess) =>
                {
                    Mst.Events.Invoke(MstEventKeys.hideLoadingInfo);

                    if (!isSuccess)
                    {
                        Mst.Client.Spawners.AbortSpawn(controller.SpawnTaskId);

                        logger.Error("Failed spawn new room. Time is up!");
                        Mst.Events.Invoke(MstEventKeys.showOkDialogBox, new OkDialogBoxEventMessage("Failed spawn new room. Time is up!", null));

                        OnRoomStartAbortedEvent?.Invoke();

                        return;
                    }

                    Debug.Log("OnRoomStartedEvent 1");
                    OnRoomStartedEvent?.Invoke();
                    Debug.Log("OnRoomStartedEvent 2");

                    logger.Info("You have successfully spawned new room");

                }, matchCreationTimeout);
            });
        }

        /// <summary>
        /// Sends request to master server to start new room process
        /// </summary>
        /// <param name="spawnOptions"></param>
        public virtual void CreateNewRoom(MstProperties spawnOptions)
        {
            CreateNewRoom(string.Empty, spawnOptions);
        }

        /// <summary>
        /// Starts given match
        /// </summary>
        /// <param name="gameInfo"></param>
        public virtual void StartMatch(GameInfoPacket gameInfo)
        {
            Debug.Log("StartMatch || " + gameInfo.Address);
            // Save room Id in buffer, may be very helpful
            Mst.Options.Set(MstDictKeys.ROOM_ID, gameInfo.Id);
            // Save max players to buffer, may be very helpful
            Mst.Options.Set(MstDictKeys.ROOM_MAX_CONNECTIONS, gameInfo.MaxPlayers);

            if (gameInfo.IsPasswordProtected)
            {
                Mst.Events.Invoke(MstEventKeys.showPasswordDialogBox,
                    new PasswordInputDialoxBoxEventMessage("Room is required the password. Please enter room password below", () =>
                    {
                        // Get password if was set
                        string password = Mst.Options.AsString(MstDictKeys.ROOM_PASSWORD, "");

                        // Get access with password
                        GetAccess(gameInfo, password);
                    }));
            }
            else
            {
                GetAccess(gameInfo);
            }
        }

        private void Local_Rejoin_Game_Handler(IIncomingMessage message)
        {
            Debug.Log("Local_ReJoin_Game_Handler");
            RoomOptions room = message.Deserialize(new RoomOptions());

            Dictionary<string, string> dict = room.CustomOptions.ToDictionary();
            string m_Room_ID = null;
            foreach (KeyValuePair<string, string> item in dict)
            {
                if (item.Key == "Room_ID")
                    m_Room_ID = item.Value;
            }

            int Room_ID_Int;
            int.TryParse(m_Room_ID, out Room_ID_Int);

            MstProperties option = new MstProperties();

            option.Set(MstDictKeys.ROOM_NAME, room.Name);
            option.Set(MstDictKeys.ROOM_ID, m_Room_ID);
            option.Set(MstDictKeys.ROOM_PASSWORD, room.Password);
            option.Set(MstDictKeys.ROOM_IS_PUBLIC, room.IsPublic);
            option.Set(MstDictKeys.ROOM_REGION, room.Region);
            //option.Set(MstDictKeys.ROOM_MAX_PLAYERS, room.MaxConnections);
            option.Set(MstDictKeys.ROOM_PASSWORD, room.Password);

            GameInfoPacket info_packet = new GameInfoPacket();
            info_packet.Id = Room_ID_Int;
            info_packet.Address = room.RoomIp;
            info_packet.Name = room.Name;
            info_packet.Region = room.Region;
            info_packet.Type = GameInfoType.Unknown;
            info_packet.IsPasswordProtected = false;
            info_packet.MaxPlayers = room.MaxConnections;
            info_packet.OnlinePlayers = 0;
            info_packet.Properties = option;

            StartMatch(info_packet);
        }

        private void Local_Join_Game_1v1Handler(IIncomingMessage message)
        {
            Debug.Log("Local_Join_Game_1v1Handler");
            RoomOptions room = message.Deserialize(new RoomOptions());

            Dictionary<string, string> dict = room.CustomOptions.ToDictionary();
            string m_Room_ID = null;
            foreach (KeyValuePair<string, string> item in dict)
            {
                if (item.Key == "Room_ID")
                    m_Room_ID = item.Value;
            }

            int Room_ID_Int;
            // attempt to parse the value using the TryParse functionality of the integer type
            int.TryParse(m_Room_ID, out Room_ID_Int);

            MstProperties option = new MstProperties();

            option.Set(MstDictKeys.ROOM_NAME, room.Name);
            option.Set(MstDictKeys.ROOM_ID, m_Room_ID);
            option.Set(MstDictKeys.ROOM_PASSWORD, room.Password);
            option.Set(MstDictKeys.ROOM_IS_PUBLIC, room.IsPublic);
            option.Set(MstDictKeys.ROOM_REGION, room.Region);
            //option.Set(MstDictKeys.ROOM_MAX_PLAYERS, room.MaxConnections);
            option.Set(MstDictKeys.ROOM_PASSWORD, room.Password);

            GameInfoPacket info_packet = new GameInfoPacket();
            info_packet.Id = Room_ID_Int;
            info_packet.Address = room.RoomIp;
            info_packet.Name = room.Name;
            info_packet.Region = room.Region;
            info_packet.Type = GameInfoType.Unknown;
            info_packet.IsPasswordProtected = false;
            info_packet.MaxPlayers = room.MaxConnections;
            info_packet.OnlinePlayers = 0;
            info_packet.Properties = option;

            Debug.Log("info_packet.Address || " + info_packet.Address);

            StartMatch(info_packet);
        }

        private void Player_Left_Room_Handler(IIncomingMessage message) // 
        {
            Debug.Log("Player_Left_Room_Finish");
        }

        private void Destroy_Room_Handler(IIncomingMessage message) // 
        {
            Debug.Log("Destroy_Room_Finish");
        }
    }
}

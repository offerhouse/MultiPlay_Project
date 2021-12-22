using MasterServerToolkit.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MasterServerToolkit.MasterServer
{
    public class MatchmakerModule : BaseServerModule
    {

        public bool IsServer = false;
        public bool MakeMatch_1v1 = false, MakeMatch_2v2 = false, MakeMatch_2op = false, MakeMatch_4op = false;

        Dictionary<string, IPeer> Queue1v1_Dict = new Dictionary<string, IPeer>();
        Dictionary<string, IPeer> Queue2v2_Dict = new Dictionary<string, IPeer>();
        Dictionary<string, IPeer> Queue2op_Dict = new Dictionary<string, IPeer>();
        Dictionary<string, IPeer> Queue4op_Dict = new Dictionary<string, IPeer>();

        List<Battle_Room_Info> Battle_Info_List = new List<Battle_Room_Info>();

        public class Battle_Room_Info
        {
            public short Room_ID { get; set; }
            public string Room_IP { get; set; }
            public int Room_Port { get; set; }
            public string UserID_1 { get; set; }
            public string UserID_2 { get; set; }
            public string UserID_3 { get; set; }
            public string UserID_4 { get; set; }
            public IPeer IPeer_1 { get; set; }
            public IPeer IPeer_2 { get; set; }
            public IPeer IPeer_3 { get; set; }
            public IPeer IPeer_4 { get; set; }
        }

        short OP_Code;

        /// <summary>
        /// List of game providers
        /// </summary>
        public HashSet<IGamesProvider> GameProviders { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        protected SpawnersModule spawnersModule;

        protected override void Awake()
        {
            base.Awake();

            AddOptionalDependency<LobbiesModule>();
            AddDependency<SpawnersModule>();
        }

        public override void Initialize(IServer server)
        {
            GameProviders = new HashSet<IGamesProvider>();

            var roomsModule = server.GetModule<RoomsModule>();
            var lobbiesModule = server.GetModule<LobbiesModule>();
            spawnersModule = server.GetModule<SpawnersModule>();

            if (!spawnersModule) throw new Exception("SpawnersModule not found");

            // Dependencies
            if (roomsModule != null)
            {
                AddProvider(roomsModule);
            }

            if (lobbiesModule != null)
            {
                AddProvider(lobbiesModule);
            }

            // Add handlers
            server.RegisterMessageHandler((short)MstMessageCodes.FindGamesRequest, FindGamesRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.GetRegionsRequest, GetRegionsRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Check_Player_In_Game, Check_Player_In_Game_Handler);
            server.RegisterMessageHandler((short)MstMessageCodes.Queue1v1, Queue1v1Handler);
            //server.RegisterMessageHandler((short)MstMessageCodes.MakeMatchFinish, MakeMatchFinishHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Destroy_Room, Destroy_Room_Handler);
            server.RegisterMessageHandler((short)MstMessageCodes.Get_Room_Player_List, Get_Room_Player_List_Handler);
            server.RegisterMessageHandler((short)MstMessageCodes.Get_Player_Number_By_UserID, Get_Player_Number_By_UserID_Handler);
        }

        /// <summary>
        /// Add given provider to list
        /// </summary>
        /// <param name="provider"></param>
        public void AddProvider(IGamesProvider provider)
        {
            GameProviders.Add(provider);
        }

        #region INCOMING MESSAGES HANDLERS

        protected virtual void FindGamesRequestHandler(IIncomingMessage message)
        {
            try
            {
                var list = new List<GameInfoPacket>();
                var filters = MstProperties.FromBytes(message.AsBytes());

                foreach (var game in GameProviders.SelectMany(pr => pr.GetPublicGames(message.Peer, filters), (provider, game) => game))
                {
                    list.Add(game);
                }

                if (list.Count == 0)
                {
                    throw new MstMessageHandlerException("No game found. Try to create your own game", ResponseStatus.Default);
                }

                // Convert to generic list and serialize to bytes
                var bytes = list.Select(l => (ISerializablePacket)l).ToBytes();
                message.Respond(bytes, ResponseStatus.Success);
            }
            // If we got system exception
            catch (MstMessageHandlerException e)
            {
                message.Respond(e.Message, e.Status);
            }
            // If we got another exception
            catch (Exception e)
            {
                logger.Error(e.Message);
                message.Respond(e.Message, ResponseStatus.Error);
            }
        }

        protected virtual void GetRegionsRequestHandler(IIncomingMessage message)
        {
            try
            {
                var list = spawnersModule.GetRegions();

                if (list.Count == 0)
                {
                    throw new MstMessageHandlerException("No regions found. Please start spawner to get regions", ResponseStatus.Default);
                }

                message.Respond(new RegionsPacket()
                {
                    Regions = list
                }, ResponseStatus.Success);
            }
            // If we got system exception
            catch (MstMessageHandlerException e)
            {
                message.Respond(e.Message, e.Status);
            }
            // If we got another exception
            catch (Exception e)
            {
                logger.Error(e.Message);
                message.Respond(e.Message, ResponseStatus.Error);
            }
        }

        #endregion

        float Timer;

        private void Update()
        {
            //Timer += Time.deltaTime;
            //if (Timer >= 1)
            //{
            //    Timer = 0;
            //    if (Battle_Info_List.Count == 0)
            //        return;
            //    for (int i = 0; i < Battle_Info_List.Count; i++)
            //    {
            //        Battle_Room_Info info = Battle_Info_List[i];
            //        if (info != null)
            //            Debug.Log("Info || " + info.Room_ID + " || " + info.Room_IP + " || " + info.Room_Port + " || " +
            //                info.UserName_1 + " || " + info.UserName_2 + " || " + info.UserName_3 + " || " + info.UserName_4 + " || " +
            //                info.IPeer_1 + " || " + info.IPeer_2 + " || " + info.IPeer_3 + " || " + info.IPeer_4);
            //    }
            //}

            if (!IsServer)
                return;

            if (Queue1v1_Dict.Count >= 2 && !MakeMatch_1v1)
            {
                /// for Test 
                ///             Direct to Editor
                /// for test
                MakeMatch_1v1 = true;
                Debug.Log("Queue1v1_Dict.Count || " + Queue1v1_Dict.Count);
                MakeMatchFinishHandler();
                return;
                /// for Test 
                ///             Direct to Editor
                /// for test
                OP_Code = (short)MstMessageCodes.Queue1v1;
                MakeMatch_1v1 = true;
                RoomOptions room = new RoomOptions();
                room.CustomOptions.Add("OP_Code", OP_Code);
                Mst.Server.Connection.SendMessage((short)MstMessageCodes.MakeMatch, room);
            }
        }

        private void MakeMatchFinishHandler()// (IIncomingMessage message) // Finish make exe, pick player join game
        {
            Debug.Log("MakeMatchFinishHandler");
            /// for Test 
            ///             Direct to Editor
            /// for test
            RoomOptions room = new RoomOptions();
            short OP_Code = (short)MstMessageCodes.Queue1v1;
            room.OP_Code = OP_Code;
            /// for Test 
            ///             Direct to Editor
            /// for test

            //RoomOptions room = message.Deserialize(new RoomOptions());
            //short OP_Code = room.OP_Code;

            Battle_Room_Info Battle_Room_Info = new Battle_Room_Info();
            Battle_Room_Info.Room_ID = room.CustomOptions.AsShort("Room_ID");
            Battle_Room_Info.Room_IP = room.RoomIp;
            Battle_Room_Info.Room_Port = room.RoomPort;

            /// for Test 
            ///             Direct to Editor
            /// for test
            Battle_Room_Info.Room_ID = 0;
            Battle_Room_Info.Room_IP = "192.168.0.100";
            Battle_Room_Info.Room_Port = 7777;
            room.CustomOptions.Set("Room_ID", 0);
            room.RoomIp = "192.168.0.100";
            room.RoomPort = 7777;
            room.MaxConnections = 5;
            room.Password = string.Empty;
            room.AccessTimeoutPeriod = 10;
            room.Region = string.Empty;

            /// for Test 
            ///              Direct to Editor
            /// for test

            string Player_ID1 = null, Player_ID2 = null, Player_ID3 = null, Player_ID4 = null;
            IPeer Player_Peer1 = null, Player_Peer2 = null, Player_Peer3 = null, Player_Peer4 = null;

            Set_Player(OP_Code, null, null, null, out Player_ID1, out Player_Peer1);
            Set_Player(OP_Code, Player_ID1, null, null, out Player_ID2, out Player_Peer2);
            Set_Player1_and_Player_2();

            if (OP_Code == (short)MstMessageCodes.Queue2v2 || OP_Code == (short)MstMessageCodes.Queue4op)
            {
                Debug.Log("OP_Code || " + OP_Code);
                Set_Player(OP_Code, Player_ID1, Player_ID2, null, out Player_ID3, out Player_Peer3);
                Set_Player(OP_Code, Player_ID1, Player_ID2, Player_ID3, out Player_ID4, out Player_Peer4);
                Set_Player3_and_Player_4();
            }

            void Set_Player(short op_code, string ID_A, string ID_B, string ID_C, out string ID, out IPeer Peer)
            {
                ID = Get_Player_User_ID_In_Dict(op_code, ID_A, ID_B, ID_C);
                Peer = Get_IPeer_In_Dict(OP_Code, ID);
            }

            void Set_Player1_and_Player_2()
            {
                Battle_Room_Info.UserID_1 = Player_ID1;
                Battle_Room_Info.UserID_2 = Player_ID2;
                Battle_Room_Info.IPeer_1 = Player_Peer1;
                Battle_Room_Info.IPeer_2 = Player_Peer2;
            }

            void Set_Player3_and_Player_4()
            {
                Battle_Room_Info.UserID_3 = Player_ID3;
                Battle_Room_Info.UserID_4 = Player_ID4;
                Battle_Room_Info.IPeer_3 = Player_Peer3;
                Battle_Room_Info.IPeer_4 = Player_Peer4;
            }

            Battle_Info_List.Add(Battle_Room_Info);

            Order_Peer_Change_Scene(Player_Peer1);
            Order_Peer_Change_Scene(Player_Peer2);
            Order_Peer_Change_Scene(Player_Peer3);
            Order_Peer_Change_Scene(Player_Peer4);

            Delete_Player_In_Dict(OP_Code, Player_ID1);
            Delete_Player_In_Dict(OP_Code, Player_ID2);
            Delete_Player_In_Dict(OP_Code, Player_ID3);
            Delete_Player_In_Dict(OP_Code, Player_ID4);

            void Order_Peer_Change_Scene(IPeer Peer)
            {
                if (Peer != null)
                    Local_Change_Scene1v1(Peer, room);
            }

            switch (OP_Code)
            {
                case ((short)MstMessageCodes.Queue1v1): MakeMatch_1v1 = false; break;
                case ((short)MstMessageCodes.Queue2v2): MakeMatch_2v2 = false; break;
                case ((short)MstMessageCodes.Queue2op): MakeMatch_2op = false; break;
                case ((short)MstMessageCodes.Queue4op): MakeMatch_4op = false; break;
            }
        }

        private void Queue1v1Handler(IIncomingMessage message)
        {
            Debug.Log("Queue1v1Handler");
            short OP_Code = message.OpCode;
            string user_ID = message.Peer.GetExtension<IUserPeerExtension>().UserId;
            IPeer peer = message.Peer;
            bool User_ID_In_GameDict = Check_UserID_In_Dict(OP_Code, user_ID);
            if (User_ID_In_GameDict)
                return;
            if (!User_ID_In_GameDict)
                Add_Player_To_Dictionary(OP_Code, user_ID, peer);
        }

        private void Check_Player_In_Game_Handler(IIncomingMessage message)
        {
            string user_ID = message.Peer.GetExtension<IUserPeerExtension>().UserId;
            IPeer peer = message.Peer;
            bool User_ID_In_GameDict = Check_Player_In_Battle_Info_List(out short Room_ID, out string Room_IP, out int Room_Port);
            if (!User_ID_In_GameDict)
            {
                message.Respond(ResponseStatus.Default);
                return;
            }
            message.Respond(ResponseStatus.Success);
            RoomOptions room = new RoomOptions();
            room.CustomOptions.Set("Room_ID", 0);
            room.RoomIp = Room_IP;
            room.RoomPort = Room_Port;
            room.MaxConnections = 5;
            room.Password = string.Empty;
            room.AccessTimeoutPeriod = 10;
            room.Region = string.Empty;
            short OP_Code = (short)MstMessageCodes.Local_Rejoin_Game;
            room.OP_Code = OP_Code;
            Local_Change_Scene1v1(peer, room);

            bool Check_Player_In_Battle_Info_List(out short room_ID, out string room_IP, out int room_Port)
            {
                room_ID = 0; room_IP = null; room_Port = 0;
                foreach (Battle_Room_Info battle_info in Battle_Info_List)
                {
                    if (battle_info.UserID_1 == user_ID)
                    {
                        room_ID = battle_info.Room_ID;
                        room_IP = battle_info.Room_IP;
                        room_Port = battle_info.Room_Port;
                        battle_info.IPeer_1 = peer;
                        return true;
                    }
                    if (battle_info.UserID_2 == user_ID)
                    {
                        room_ID = battle_info.Room_ID;
                        room_IP = battle_info.Room_IP;
                        room_Port = battle_info.Room_Port;
                        battle_info.IPeer_2 = peer;
                        return true;
                    }
                    if (battle_info.UserID_3 == user_ID)
                    {
                        room_ID = battle_info.Room_ID;
                        room_IP = battle_info.Room_IP;
                        room_Port = battle_info.Room_Port;
                        battle_info.IPeer_3 = peer;
                        return true;
                    }
                    if (battle_info.UserID_4 == user_ID)
                    {
                        room_ID = battle_info.Room_ID;
                        room_IP = battle_info.Room_IP;
                        room_Port = battle_info.Room_Port;
                        battle_info.IPeer_4 = peer;
                        return true;
                    }
                }
                return false;
            }
        }

        public bool Check_UserID_In_Dict(short OP_Code, string user_ID)
        {
            Dictionary<string, IPeer> dict = Get_Dictionary_By_Type(OP_Code);
            foreach (string key in dict.Keys)
            {
                if (key == user_ID)
                    return true;
            }
            return false;
        }

        string Get_Player_User_ID_In_Dict(short OP_Code, string User_ID1, string User_ID2, string User_ID3)
        {
            string player = null;
            Dictionary<string, IPeer> dict = Get_Dictionary_By_Type(OP_Code);
            foreach (KeyValuePair<string, IPeer> item in dict)
            {
                if (item.Key != User_ID1 && item.Key != User_ID2 && item.Key != User_ID3)
                    player = item.Key;
            }
            return player;
        }

        IPeer Get_IPeer_In_Dict(short OP_Code, string User_ID)
        {
            IPeer player = null;
            Dictionary<string, IPeer> dict = Get_Dictionary_By_Type(OP_Code);
            foreach (KeyValuePair<string, IPeer> item in dict)
            {
                if (item.Key == User_ID)
                    player = item.Value;
            }
            return player;
        }

        void Delete_Player_In_Dict(short OP_Code, string User_ID)
        {
            if (User_ID == null)
                return;
            switch (OP_Code)
            {
                case ((short)MstMessageCodes.Queue1v1): Queue1v1_Dict.Remove(User_ID); break;
                case ((short)MstMessageCodes.Queue2v2): Queue2v2_Dict.Remove(User_ID); break;
                case ((short)MstMessageCodes.Queue2op): Queue2op_Dict.Remove(User_ID); break;
                case ((short)MstMessageCodes.Queue4op): Queue4op_Dict.Remove(User_ID); break;
            }
        }

        #region Queue_Dictionary
        Dictionary<string, IPeer> Get_Dictionary_By_Type(short OP_Code)
        {
            Dictionary<string, IPeer> dict = new Dictionary<string, IPeer>();
            if (OP_Code == (short)MstMessageCodes.Queue1v1)
                dict = Queue1v1_Dict;

            if (OP_Code == (short)MstMessageCodes.Queue2v2)
                dict = Queue2v2_Dict;

            if (OP_Code == (short)MstMessageCodes.Queue2op)
                dict = Queue2op_Dict;

            if (OP_Code == (short)MstMessageCodes.Queue4op)
                dict = Queue4op_Dict;

            return dict;
        }

        void Add_Player_To_Dictionary(short OP_Code, string User_ID, IPeer peer)
        {
            // AuthorList.Remove("Mahesh Chand");
            switch (OP_Code)
            {
                case ((short)MstMessageCodes.Queue1v1): Queue1v1_Dict.Add(User_ID, peer); break;
                case ((short)MstMessageCodes.Queue2v2): Queue2v2_Dict.Add(User_ID, peer); break;
                case ((short)MstMessageCodes.Queue2op): Queue2op_Dict.Add(User_ID, peer); break;
                case ((short)MstMessageCodes.Queue4op): Queue4op_Dict.Add(User_ID, peer); break;
            }
        }

        #endregion Dictionary

        private void Local_Change_Scene1v1(IPeer player_Peer, RoomOptions Room_Option) // Server
        {
            short OP_Code = Room_Option.OP_Code;
            Debug.Log("Local_Change_Scene1v1 || " + OP_Code);
            switch (OP_Code)
            {
                case ((short)MstMessageCodes.Queue1v1):
                    player_Peer.SendMessage((short)MstMessageCodes.Local_Join_Game_1v1, Room_Option);
                    break;
                case ((short)MstMessageCodes.Queue2v2):
                    player_Peer.SendMessage((short)MstMessageCodes.Local_Join_Game_2v2, Room_Option);
                    break;
                case ((short)MstMessageCodes.Queue2op):
                    player_Peer.SendMessage((short)MstMessageCodes.Local_Join_Game_2op, Room_Option);
                    break;
                case ((short)MstMessageCodes.Queue4op):
                    player_Peer.SendMessage((short)MstMessageCodes.Local_Join_Game_4op, Room_Option);
                    break;
                case ((short)MstMessageCodes.Local_Rejoin_Game):
                    player_Peer.SendMessage((short)MstMessageCodes.Local_Rejoin_Game, Room_Option);
                    break;
            }
        }

        private void Destroy_Room_Handler(IIncomingMessage message) // 
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            short Destroy_Room_ID = room.CustomOptions.AsShort("Room_ID");
            short room_ID;
            for (int i = 0; i < Battle_Info_List.Count; i++)
            {
                room_ID = Battle_Info_List[i].Room_ID;
                if (room_ID == Destroy_Room_ID)
                {
                    Tell_Peer_Room_Finish_Destroy(Battle_Info_List[i].IPeer_1);
                    Tell_Peer_Room_Finish_Destroy(Battle_Info_List[i].IPeer_2);
                    Tell_Peer_Room_Finish_Destroy(Battle_Info_List[i].IPeer_3);
                    Tell_Peer_Room_Finish_Destroy(Battle_Info_List[i].IPeer_4);

                    Battle_Info_List.Remove(Battle_Info_List[i]);
                    return;
                }
            }

            void Tell_Peer_Room_Finish_Destroy(IPeer peer)
            {
                if (peer == null)
                    return;
                if (!peer.IsConnected)
                    return;
                peer.SendMessage((short)MstMessageCodes.Destroy_Room);
            }
        }

        private void Get_Room_Player_List_Handler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            int Room_ID = room.CustomOptions.AsInt("Room_ID");
            int OP_Code = room.OP_Code;

            for (int i = 0; i < Battle_Info_List.Count; i++)
            {
                if (Battle_Info_List[i] != null)
                {
                    if (Battle_Info_List[i].Room_ID == Room_ID)
                    {
                        RoomOptions new_Room = new RoomOptions();
                        new_Room.CustomOptions.Set("ID1", Battle_Info_List[i].UserID_1);
                        new_Room.CustomOptions.Set("ID2", Battle_Info_List[i].UserID_2);
                        new_Room.CustomOptions.Set("ID3", Battle_Info_List[i].UserID_3);
                        new_Room.CustomOptions.Set("ID4", Battle_Info_List[i].UserID_4);
                        message.Respond(new_Room, ResponseStatus.Success);
                        return;
                    }
                }
            }
            message.Respond(ResponseStatus.Failed);
        }

        private void Get_Player_Number_By_UserID_Handler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            string User_ID = room.CustomOptions.AsString("User_ID");
            RoomOptions new_Room = new RoomOptions();
            for (int i = 0; i < Battle_Info_List.Count; i++)
            {
                if (Battle_Info_List[i] != null)
                {
                    if (Battle_Info_List[i].UserID_1 == User_ID)
                    {
                        new_Room.CustomOptions.Set("Player_Number", 1);
                        message.Respond(new_Room, ResponseStatus.Success);
                        return;
                    }
                    if (Battle_Info_List[i].UserID_2 == User_ID)
                    {
                        new_Room.CustomOptions.Set("Player_Number", 2);
                        message.Respond(new_Room, ResponseStatus.Success);
                        return;
                    }
                    if (Battle_Info_List[i].UserID_3 == User_ID)
                    {
                        new_Room.CustomOptions.Set("Player_Number", 3);
                        message.Respond(new_Room, ResponseStatus.Success);
                        return;
                    }
                    if (Battle_Info_List[i].UserID_4 == User_ID)
                    {
                        new_Room.CustomOptions.Set("Player_Number", 4);
                        message.Respond(new_Room, ResponseStatus.Success);
                        return;
                    }
                }
            }
            message.Respond(new_Room, ResponseStatus.Failed);
        }


    }
}

using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Networking;
using MasterServerToolkit.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MasterServerToolkit.Examples.BasicProfile
{
    public enum ObservablePropertiyCodes { DisplayName, Avatar, Bronze, Silver, Gold }

    public class DemoProfilesModule : ProfilesModule
    {
        [Header("Start Values"), SerializeField]
        private float bronze = 100;
        [SerializeField]
        private float silver = 50;
        [SerializeField]
        private float gold = 50;
        [SerializeField]
        private string avatarUrl = "https://i.imgur.com/JQ9pRoD.png";

        public HelpBox _header = new HelpBox()
        {
            Text = "This script is a custom module, which sets up profiles values for new users"
        };

        public override void Initialize(IServer server)
        {
            base.Initialize(server);

            // Set the new factory in ProfilesModule
            ProfileFactory = CreateProfileInServer;

            server.RegisterMessageHandler((short)MstMessageCodes.UpdateDisplayNameRequest, UpdateDisplayNameRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Update_Desk, Update_DeskHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Click_Shop_Button, Click_Shop_Button_DeskHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Refresh_Exchange, Refresh_Exchange_Handler);
            server.RegisterMessageHandler((short)MstMessageCodes.End_Game_Reward, End_Game_Reward_Handler);
            server.RegisterMessageHandler((short)MstMessageCodes.Client_Get_Profile, Client_Get_Profile_Handler);

            //Update profile resources each 5 sec
            //InvokeRepeating(nameof(IncreaseResources), 1f, 1f);
        }

        /// <summary>
        /// This method is just for creation of profile on server side as default for users that are logged in for the first time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientPeer"></param>
        /// <returns></returns>
        private ObservableServerProfile CreateProfileInServer(string userId, IPeer clientPeer)
        {
            Debug.Log("CreateProfileInServer");
            return GetComponent<Account_Info>().Request_New_Profile(userId, clientPeer);
        }

        private void IncreaseResources()
        {
            foreach (var profile in Profiles)
            {
                var bronzeProperty = profile.GetProperty<ObservableFloat>((short)ObservablePropertiyCodes.Bronze);
                var silverProperty = profile.GetProperty<ObservableFloat>((short)ObservablePropertiyCodes.Silver);
                var goldProperty = profile.GetProperty<ObservableFloat>((short)ObservablePropertiyCodes.Gold);

                bronzeProperty.Add(1f);
                silverProperty.Add(0.1f);
                goldProperty.Add(0.01f);
            }
        }

        private void UpdateDisplayNameRequestHandler(IIncomingMessage message)
        {
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();

            Debug.Log("UpdateDisplayNameRequestHandler || " + userExtension.Username);
            if (userExtension == null || userExtension.Account == null)
            {
                message.Respond("Invalid session", ResponseStatus.Unauthorized);
                return;
            }

            var newProfileData = new Dictionary<string, string>().FromBytes(message.AsBytes());

            try
            {
                if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
                {
                    profile.GetProperty<ObservableString>((short)ObservablePropertiyCodes.DisplayName).Set(newProfileData["displayName"]);
                    profile.GetProperty<ObservableString>((short)ObservablePropertiyCodes.Avatar).Set(newProfileData["avatarUrl"]);

                    message.Respond(ResponseStatus.Success);
                }
                else
                {
                    message.Respond("Invalid session", ResponseStatus.Unauthorized);
                }
            }
            catch (Exception e)
            {
                message.Respond($"Internal Server Error: {e}", ResponseStatus.Error);
            }
        }

        private void Client_Get_Profile_Handler(IIncomingMessage message)
        {
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();

            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                message.Respond(profile.ToBytes(), ResponseStatus.Success);
            }
        }

        private void Update_DeskHandler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            Dictionary<string, string> dict = room.CustomOptions.ToDictionary();

            if (dict.Count == 0)
                return;

            List<short> Desk_Slot_Tower;
            List<List<short>> All_Desk = new List<List<short>>();
            List<List<short>> Dirty_List = new List<List<short>>();
            short current_Desk = 1;

            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                for (short i = 1; i < 9; i++) // current desk
                {
                    for (short j = 1; j < 6; j++) // current slot
                    {
                        short desk = i;
                        short slot = j;
                        short Tower_Number = (short)Get_Desk_Property(profile, desk, slot).GetValue();
                        Desk_Slot_Tower = new List<short>();
                        Desk_Slot_Tower.Add(i); // current desk
                        Desk_Slot_Tower.Add(j); // current slot
                        Desk_Slot_Tower.Add(Tower_Number); // modified Tower
                        All_Desk.Add(Desk_Slot_Tower);
                    }
                }
            } // Server get Desk List

            foreach (KeyValuePair<string, string> item in dict)
            {
                if (item.Key == "Current_Desk")
                    current_Desk = Convert.ToInt16(item.Value);

                if (item.Key != "Current_Desk")
                {
                    short Desk_Number = Convert.ToInt16(item.Key.Substring(0, 1));
                    short Slot_Number = Convert.ToInt16(item.Key.Substring(1, 1));
                    short Tower_Number = Convert.ToInt16(item.Value);

                    Desk_Slot_Tower = new List<short>();
                    Desk_Slot_Tower.Add(Desk_Number); // current desk
                    Desk_Slot_Tower.Add(Slot_Number); // current slot
                    Desk_Slot_Tower.Add(Tower_Number); // modified Tower
                    Dirty_List.Add(Desk_Slot_Tower);
                    Debug.Log("Dirty || Desk || " + Desk_Number + " || Slot_Number || " + Slot_Number + " || " + Tower_Number);

                    //foreach (var Dirty in Dirty_List)
                    //{
                    //    Debug.Log("Dirty || Desk || " + Dirty[0] + " || Slot_Number || " + Dirty[1] + " || " + Dirty[2]);
                    //}
                }
            } // Create Dirty List

            for (int i = 0; i < Dirty_List.Count; i++) // Old Desk + Dirty Desk
            {
                List<short> Dirty_Desk = Dirty_List[i];
                short Dirty_Desk_Number = Dirty_Desk[0];
                short Dirty_Slot_Number = Dirty_Desk[1];
                for (int d = 0; d < All_Desk.Count; d++)
                {
                    if (All_Desk[i] != null)
                    {
                        List<short> Desk = All_Desk[i];
                        short Desk_Number = Desk[0];
                        short Slot_Number = Desk[1];

                        if (Dirty_Desk_Number == Desk_Number && Dirty_Slot_Number == Slot_Number)
                            All_Desk[i] = Dirty_Desk;
                    }
                }
            }

            bool Check_Duplicates_Tower = false;

            foreach (var Desk in All_Desk) // Check duplicates Tower in same desk
            {
                short desk_Number = Desk[0]; short slot_Number = Desk[1]; short TowerNumber = Desk[2];
                foreach (var Check_Desk in All_Desk)
                {
                    short check_desk_number = Check_Desk[0];
                    short check_slot_number = Check_Desk[1];
                    short check_towernumber = Check_Desk[2];
                    if (desk_Number == check_desk_number && slot_Number != check_slot_number && TowerNumber == check_towernumber)
                    {
                        Debug.LogError("Same_Tower_in_Desk || desk_Number || " + desk_Number + " || " + check_desk_number);
                        Debug.LogError("Same_Tower_in_Desk || slot_Number || " + slot_Number + " || " + check_slot_number);
                        Debug.LogError("Same_Tower_in_Desk || TowerNumber || " + TowerNumber + " || " + check_towernumber);
                        Check_Duplicates_Tower = true;
                    }
                }
            }

            if (Check_Duplicates_Tower)
                return;

            foreach (var dirty in Dirty_List)
            {
                short Desk_Number = dirty[0];
                short Slot_Number = dirty[1];
                short Tower_Number = dirty[2];
                Assign_DeskUpdate(Desk_Number, Slot_Number, Tower_Number);
                Debug.Log("Desk_Number || " + Desk_Number + " || " + Slot_Number + " || " + Tower_Number);
            }

            MstTimer.WaitForSeconds(3f, () =>
            {
                if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile2))
                {
                    profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Current_Desk).Set(current_Desk);
                    int D1 = profile2.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_01).GetValue();
                    int D2 = profile2.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_02).GetValue();
                    int D3 = profile2.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_03).GetValue();
                    int D4 = profile2.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_04).GetValue();
                    int D5 = profile2.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_05).GetValue();

                    Debug.Log("Update_DeskHandler " + D1 + " || " + D2 + " || " + D3 + " || " + D4 + " || " + D5);

                } // Server get Desk List
            });

            void Assign_DeskUpdate(short desk, short slot, short tower_Number)
            {
                var Property = Get_Desk_Property(profile, desk, slot);
                Property.Set(tower_Number);
                message.Respond(profile.ToBytes(), ResponseStatus.Success);
            }
        }

        ObservableInt Get_Desk_Property(ObservableServerProfile profile, short Desk_Number, short Slot_Number)
        {
            ObservableInt Property = null;
            switch (Desk_Number)
            {
                case 1:
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_05);
                    }
                    break;
                case (2):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_05);
                    }
                    break;
                case (3):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_05);
                    }
                    break;
                case (4):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_05);
                    }
                    break;
                case (5):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_05);
                    }
                    break;
                case (6):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_05);
                    }
                    break;
                case (7):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_05);
                    }
                    break;
                case (8):
                    switch (Slot_Number)
                    {
                        case (1): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_01);
                        case (2): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_02);
                        case (3): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_03);
                        case (4): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_04);
                        case (5): return profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_05);
                    }
                    break;
            }
            return Property;
        }

        private void Click_Shop_Button_DeskHandler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            short Button_Code = room.OP_Code;
            bool Success = false;
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                if (Button_Code == (short)Shop_Code.Refresh_InGame_Sell_Button)
                    GetComponent<Shop_Info>().Click_InGame_Refresh_Button(profile, Button_Code);

                if (Button_Code == (short)Shop_Code.InGame_Sell_1_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 1);
                if (Button_Code == (short)Shop_Code.InGame_Sell_2_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 2);
                if (Button_Code == (short)Shop_Code.InGame_Sell_3_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 3);
                if (Button_Code == (short)Shop_Code.InGame_Sell_4_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 4);
                if (Button_Code == (short)Shop_Code.InGame_Sell_5_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 5);
                if (Button_Code == (short)Shop_Code.InGame_Sell_6_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 6);
                if (Button_Code == (short)Shop_Code.InGame_Sell_7_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 7);
                if (Button_Code == (short)Shop_Code.InGame_Sell_8_Button)
                    GetComponent<Shop_Info>().Click_InGame_Button(profile, 8);

                if (Button_Code == (short)Shop_Code.Exchange_Button_1)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 1);
                if (Button_Code == (short)Shop_Code.Exchange_Button_2)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 2);
                if (Button_Code == (short)Shop_Code.Exchange_Button_3)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 3);
                if (Button_Code == (short)Shop_Code.Exchange_Button_4)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 4);
                if (Button_Code == (short)Shop_Code.Exchange_Button_5)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 5);
                if (Button_Code == (short)Shop_Code.Exchange_Button_6)
                    GetComponent<Shop_Info>().Click_Exchange_Button(profile, 6);
                Success = true;
            }

            MstTimer.WaitForSeconds(1f, () =>
            {

            });
        }

        private void Refresh_Exchange_Handler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            short Button_Code = room.OP_Code;
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                var Exchange_Time = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Exchange_Time);
                int Exchange_Time_Value = Exchange_Time.GetValue();

                DateTime last_Refresh_Exchange_Time = GetComponent<Shop_Info>().Convert_Int_to_DateTime(Exchange_Time_Value);
                System.DateTime Next_Update = last_Refresh_Exchange_Time.AddHours(2);

                if (DateTime.UtcNow > Next_Update) //////
                {
                    DateTime now = DateTime.UtcNow;
                    int now_Time = GetComponent<Shop_Info>().Covert_DateTime_to_Int(now);

                    Exchange_Time.Set(now_Time);
                    GetComponent<Shop_Info>().Create_Exhchange_Shop(profile);
                }
            }
        }

        private void End_Game_Reward_Handler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());

            bool winner = room.CustomOptions.AsBool("Winner");
            string room_code = room.CustomOptions.AsString("op_room_Code");
            string User_ID = room.CustomOptions.AsString("User_ID");
            short exp = room.CustomOptions.AsShort("EXP");
            short gold = room.CustomOptions.AsShort("Gold");
            short diamond = room.CustomOptions.AsShort("Diamond");
            short soul = room.CustomOptions.AsShort("Soul");
            int combine_point = room.CustomOptions.AsShort("Combine_Point");
            int high_damage = room.CustomOptions.AsShort("High_Damage");
            int highest_hp_enemy = room.CustomOptions.AsShort("Highest_HP_Enemy");
            int killed_enemy = room.CustomOptions.AsShort("Killed_Enemy");
            short wave_number = room.CustomOptions.AsShort("Wave_Number");

            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(User_ID, out ObservableServerProfile profile))
            {
                profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Player_EXP).Add(exp);
                profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).Add(gold);
                profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Diamond).Add(diamond);
                profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_03).Add(soul);

                int D_Task1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_Type).GetValue();
                int D_Task2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_Type).GetValue();
                int D_Task3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_Type).GetValue();
                ObservableInt D_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_1);
                ObservableInt D_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_2);
                ObservableInt D_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_3);

                int W_Task1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_Type).GetValue();
                int W_Task2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_Type).GetValue();
                int W_Task3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_Type).GetValue();
                ObservableInt W_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_1);
                ObservableInt W_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_2);
                ObservableInt W_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_3);

                bool Winner_1v1 = false, Winner_2v2 = false, Played_1v1 = false, Played_2v2 = false, Played_2op = false, Played_4op = false;
                if (room_code == "1v1")
                {
                    Played_1v1 = true;
                    if (winner)
                        Winner_1v1 = true;
                }
                if (room_code == "2v2")
                {
                    Played_2v2 = true;
                    if (winner)
                        Winner_2v2 = true;
                }
                if (room_code == "2op")
                    Played_2op = true;
                if (room_code == "4op")
                    Played_4op = true;

                Check_and_Set_Task(D_Task1, D_QTY_1);
                Check_and_Set_Task(D_Task2, D_QTY_2);
                Check_and_Set_Task(D_Task3, D_QTY_3);

                Check_and_Set_Task(W_Task1, W_QTY_1);
                Check_and_Set_Task(W_Task2, W_QTY_2);
                Check_and_Set_Task(W_Task3, W_QTY_3);

                void Check_and_Set_Task(int task_Code, ObservableInt Property)
                {
                    if (task_Code == (short)Task_Code.Play_1V1_Task && Played_1v1)
                    {
                        Property.Add(1);
                    }
                    if (task_Code == (short)Task_Code.Play_2OP_Task && Played_2v2)
                    {
                        Property.Add(1);
                    }
                    if (task_Code == (short)Task_Code.Win_Task && (Winner_1v1 || Winner_2v2))
                    {
                        Property.Add(1);
                    }
                    if (task_Code == (short)Task_Code.Highest_Attack_Point_Task)
                    {
                        int property_highest_damage = Property.GetValue();
                        if (high_damage > property_highest_damage)
                            Property.Set(high_damage);
                    }
                    if (task_Code == (short)Task_Code.Enemy_Kill_Task)
                    {
                        Property.Add(killed_enemy);
                    }
                    if (task_Code == (short)Task_Code.OP_Passed_Wave_Task)
                    {
                        int property_wave_number = Property.GetValue();
                        if (wave_number > property_wave_number)
                            Property.Set(wave_number);
                    }
                    if (task_Code == (short)Task_Code.Winning_Streak_Task)
                    {
                        if (!winner)
                            Property.Set(0);
                        if (winner)
                            Property.Add(1);
                    }
                    if (task_Code == (short)Task_Code.High_Combine_Tower_Point_Task)
                    {
                        int property_combine_point = Property.GetValue();
                        if (combine_point > property_combine_point)
                            Property.Set(combine_point);
                    }
                    if (task_Code == (short)Task_Code.Kill_Over_HP_Enemy_Task)
                    {
                        int property_highest_hp_enemy = Property.GetValue();
                        if (highest_hp_enemy > property_highest_hp_enemy)
                            Property.Set(highest_hp_enemy);
                    }
                }
            }

            MstTimer.WaitForSeconds(2f, () =>
            {
                message.Respond(ResponseStatus.Success);
            });
        }
    }
}

//using Mirror;
//using Firebase;
//using Firebase.Database;
//using Firebase.Unity.Editor;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class Get_Player_Status : NetworkBehaviour
//{
//    DatabaseReference reference;
//    public Player_Status.Player Player;
//    DataSnapshot snapshot;
//    public bool Data_Finish = false;

//    public string Player_ID;
//    public GameObject status;

//    void Start()
//    {
//        // Set up the Editor before calling into the realtime database.
//        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://picnic-888fe.firebaseio.com/");

//        // Get the root reference location of the database.
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    public void getCharacterInfo(GameObject _Status, GameObject Online_Profile)
//    {
//        //_character_Status = character_Status;
//        status = _Status;
//        Player_Status player_Status = _Status.GetComponent<Player_Status>();
//        Player = player_Status.Creat_New_Player_Status();

//        if (Online_Profile.GetComponent<Player_Status>().player == null)
//        {
//            Debug.Log("Player_is_Null");
//        }
//        Update_To_FireBase();
//    }

//    public void Update_To_FireBase()
//    {
//        Debug.Log("Update_To_FireBase");
//        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://picnic-888fe.firebaseio.com/");

//        // Get the root reference location of the database.
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//        reference.GetValueAsync().ContinueWith(task => {

//            if (task.IsFaulted)
//            {
//                // Handle the error...
//            }
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                bool id_Exist = snapshot.Child(Player_ID).Exists;
//                Debug.Log("id_Exist " + id_Exist);
//                if (id_Exist)
//                {
//                    Dictionary<string, object> Values = snapshot.Child(Player_ID).Value as Dictionary<string, object>;
//                    foreach (var Value in Values)
//                    {
//                        int value = 0;
//                        var Player_Status_Name = Value.Key;
//                        var Player_Status_Value = Value.Value;
//                        string name = Player_Status_Name.ToString();
//                        string status = Player_Status_Value.ToString();

//                        if (name == "Shop_Timer")
//                        {
//                            long time_value = Convert.ToInt64(status);
//                            Convert_Time_Character_Status(Player_Status_Name, time_value);
//                        }

//                        else if (status == "True" || status == "False")
//                        {
//                            bool bool_value = false;
//                            if (status == "True")
//                            {
//                                bool_value = true;
//                            }
//                            Convert_Bool_Character_Status(Player_Status_Name, bool_value);
//                        }
//                        else
//                        {
//                            try
//                            {
//                                value = Convert.ToInt32(Player_Status_Value);
//                                Convet_int_Character_Status(Player_Status_Name, value);
//                            }
//                            catch (System.Exception e)
//                            {
//                                Debug.Log("Exception " + e + " || " + Player_ID);
//                            }
//                        }
//                    }
//                    Data_Finish = true;
//                    reference = null;
//                    snapshot = null;
//                }
//                else
//                {
//                    Create_New_Character_Status_To_Server();
//                    Data_Finish = true;
//                }
//            }
//        });
//    }

//    void Convert_Bool_Character_Status(string Player_Status_Name, bool Player_Status_Value)
//    {
//        switch (Player_Status_Name)
//        {
//            case "Tower_101_Active": Player.Tower_101_Active = Player_Status_Value; break;
//            case "Tower_102_Active": Player.Tower_102_Active = Player_Status_Value; break;
//            case "Tower_103_Active": Player.Tower_103_Active = Player_Status_Value; break;
//            case "Tower_104_Active": Player.Tower_104_Active = Player_Status_Value; break;
//            case "Tower_105_Active": Player.Tower_105_Active = Player_Status_Value; break;
//            case "Tower_106_Active": Player.Tower_106_Active = Player_Status_Value; break;
//            case "Tower_107_Active": Player.Tower_107_Active = Player_Status_Value; break;
//            case "Tower_108_Active": Player.Tower_108_Active = Player_Status_Value; break;
//            case "Tower_109_Active": Player.Tower_109_Active = Player_Status_Value; break;
//            case "Tower_110_Active": Player.Tower_110_Active = Player_Status_Value; break;

//            case "Tower_201_Active": Player.Tower_201_Active = Player_Status_Value; break;
//            case "Tower_202_Active": Player.Tower_202_Active = Player_Status_Value; break;
//            case "Tower_203_Active": Player.Tower_203_Active = Player_Status_Value; break;
//            case "Tower_204_Active": Player.Tower_204_Active = Player_Status_Value; break;
//            case "Tower_205_Active": Player.Tower_205_Active = Player_Status_Value; break;
//            case "Tower_206_Active": Player.Tower_206_Active = Player_Status_Value; break;
//            case "Tower_207_Active": Player.Tower_207_Active = Player_Status_Value; break;
//            case "Tower_208_Active": Player.Tower_208_Active = Player_Status_Value; break;
//            case "Tower_209_Active": Player.Tower_209_Active = Player_Status_Value; break;
//            case "Tower_210_Active": Player.Tower_210_Active = Player_Status_Value; break;

//            case "Tower_301_Active": Player.Tower_301_Active = Player_Status_Value; break;
//            case "Tower_302_Active": Player.Tower_302_Active = Player_Status_Value; break;
//            case "Tower_303_Active": Player.Tower_303_Active = Player_Status_Value; break;
//            case "Tower_304_Active": Player.Tower_304_Active = Player_Status_Value; break;
//            case "Tower_305_Active": Player.Tower_305_Active = Player_Status_Value; break;
//            case "Tower_306_Active": Player.Tower_306_Active = Player_Status_Value; break;
//            case "Tower_307_Active": Player.Tower_307_Active = Player_Status_Value; break;
//            case "Tower_308_Active": Player.Tower_308_Active = Player_Status_Value; break;
//            case "Tower_309_Active": Player.Tower_309_Active = Player_Status_Value; break;
//            case "Tower_310_Active": Player.Tower_310_Active = Player_Status_Value; break;

//            case "Tower_401_Active": Player.Tower_401_Active = Player_Status_Value; break;
//            case "Tower_402_Active": Player.Tower_402_Active = Player_Status_Value; break;
//            case "Tower_403_Active": Player.Tower_403_Active = Player_Status_Value; break;
//            case "Tower_404_Active": Player.Tower_404_Active = Player_Status_Value; break;
//            case "Tower_405_Active": Player.Tower_405_Active = Player_Status_Value; break;
//            case "Tower_406_Active": Player.Tower_406_Active = Player_Status_Value; break;
//            case "Tower_407_Active": Player.Tower_407_Active = Player_Status_Value; break;
//            case "Tower_408_Active": Player.Tower_408_Active = Player_Status_Value; break;
//            case "Tower_409_Active": Player.Tower_409_Active = Player_Status_Value; break;
//            case "Tower_410_Active": Player.Tower_410_Active = Player_Status_Value; break;

//            case "Tower_501_Active": Player.Tower_501_Active = Player_Status_Value; break;
//            case "Tower_502_Active": Player.Tower_502_Active = Player_Status_Value; break;
//            case "Tower_503_Active": Player.Tower_503_Active = Player_Status_Value; break;
//            case "Tower_504_Active": Player.Tower_504_Active = Player_Status_Value; break;
//            case "Tower_505_Active": Player.Tower_505_Active = Player_Status_Value; break;
//            case "Tower_506_Active": Player.Tower_506_Active = Player_Status_Value; break;
//            case "Tower_507_Active": Player.Tower_507_Active = Player_Status_Value; break;
//            case "Tower_508_Active": Player.Tower_508_Active = Player_Status_Value; break;
//            case "Tower_509_Active": Player.Tower_509_Active = Player_Status_Value; break;
//            case "Tower_510_Active": Player.Tower_510_Active = Player_Status_Value; break;

//            case "Tower_601_Active": Player.Tower_601_Active = Player_Status_Value; break;
//            case "Tower_602_Active": Player.Tower_602_Active = Player_Status_Value; break;
//            case "Tower_603_Active": Player.Tower_603_Active = Player_Status_Value; break;
//            case "Tower_604_Active": Player.Tower_604_Active = Player_Status_Value; break;
//            case "Tower_605_Active": Player.Tower_605_Active = Player_Status_Value; break;
//            case "Tower_606_Active": Player.Tower_606_Active = Player_Status_Value; break;
//            case "Tower_607_Active": Player.Tower_607_Active = Player_Status_Value; break;
//            case "Tower_608_Active": Player.Tower_608_Active = Player_Status_Value; break;
//            case "Tower_609_Active": Player.Tower_609_Active = Player_Status_Value; break;
//            case "Tower_610_Active": Player.Tower_610_Active = Player_Status_Value; break;
//            case "Tower_611_Active": Player.Tower_611_Active = Player_Status_Value; break;
//            case "Tower_612_Active": Player.Tower_612_Active = Player_Status_Value; break;
//            case "Tower_613_Active": Player.Tower_613_Active = Player_Status_Value; break;
//            case "Tower_614_Active": Player.Tower_614_Active = Player_Status_Value; break;
//            case "Tower_615_Active": Player.Tower_615_Active = Player_Status_Value; break;
//            case "Tower_616_Active": Player.Tower_616_Active = Player_Status_Value; break;
//            case "Tower_617_Active": Player.Tower_617_Active = Player_Status_Value; break;
//            case "Tower_618_Active": Player.Tower_618_Active = Player_Status_Value; break;
//            case "Tower_619_Active": Player.Tower_619_Active = Player_Status_Value; break;
//            case "Tower_620_Active": Player.Tower_620_Active = Player_Status_Value; break;

//            case "Tower_701_Active": Player.Tower_701_Active = Player_Status_Value; break;
//            case "Tower_702_Active": Player.Tower_702_Active = Player_Status_Value; break;
//            case "Tower_703_Active": Player.Tower_703_Active = Player_Status_Value; break;
//            case "Tower_704_Active": Player.Tower_704_Active = Player_Status_Value; break;
//            case "Tower_705_Active": Player.Tower_705_Active = Player_Status_Value; break;
//            case "Tower_706_Active": Player.Tower_706_Active = Player_Status_Value; break;
//            case "Tower_707_Active": Player.Tower_707_Active = Player_Status_Value; break;
//            case "Tower_708_Active": Player.Tower_708_Active = Player_Status_Value; break;
//            case "Tower_709_Active": Player.Tower_709_Active = Player_Status_Value; break;
//            case "Tower_710_Active": Player.Tower_710_Active = Player_Status_Value; break;
//            case "Tower_711_Active": Player.Tower_711_Active = Player_Status_Value; break;
//            case "Tower_712_Active": Player.Tower_712_Active = Player_Status_Value; break;
//            case "Tower_713_Active": Player.Tower_713_Active = Player_Status_Value; break;
//            case "Tower_714_Active": Player.Tower_714_Active = Player_Status_Value; break;
//            case "Tower_715_Active": Player.Tower_715_Active = Player_Status_Value; break;
//            case "Tower_716_Active": Player.Tower_716_Active = Player_Status_Value; break;
//            case "Tower_717_Active": Player.Tower_717_Active = Player_Status_Value; break;
//            case "Tower_718_Active": Player.Tower_718_Active = Player_Status_Value; break;
//            case "Tower_719_Active": Player.Tower_719_Active = Player_Status_Value; break;
//            case "Tower_720_Active": Player.Tower_720_Active = Player_Status_Value; break;
//            case "Tower_721_Active": Player.Tower_721_Active = Player_Status_Value; break;
//            case "Tower_722_Active": Player.Tower_722_Active = Player_Status_Value; break;
//            case "Tower_723_Active": Player.Tower_723_Active = Player_Status_Value; break;
//            case "Tower_724_Active": Player.Tower_724_Active = Player_Status_Value; break;
//            case "Tower_725_Active": Player.Tower_725_Active = Player_Status_Value; break;
//            case "Tower_726_Active": Player.Tower_726_Active = Player_Status_Value; break;
//            case "Tower_727_Active": Player.Tower_727_Active = Player_Status_Value; break;
//            case "Tower_728_Active": Player.Tower_728_Active = Player_Status_Value; break;
//            case "Tower_729_Active": Player.Tower_729_Active = Player_Status_Value; break;
//            case "Tower_730_Active": Player.Tower_730_Active = Player_Status_Value; break;

//            case "Statue_01_Active": Player.Statue_01_Active = Player_Status_Value; break;
//            case "Statue_02_Active": Player.Statue_02_Active = Player_Status_Value; break;
//            case "Statue_03_Active": Player.Statue_03_Active = Player_Status_Value; break;
//            case "Statue_04_Active": Player.Statue_04_Active = Player_Status_Value; break;
//            case "Statue_05_Active": Player.Statue_05_Active = Player_Status_Value; break;
//            case "Statue_06_Active": Player.Statue_06_Active = Player_Status_Value; break;
//            case "Statue_07_Active": Player.Statue_07_Active = Player_Status_Value; break;
//            case "Statue_08_Active": Player.Statue_08_Active = Player_Status_Value; break;
//            case "Statue_09_Active": Player.Statue_09_Active = Player_Status_Value; break;
//            case "Statue_10_Active": Player.Statue_10_Active = Player_Status_Value; break;
//            case "Statue_11_Active": Player.Statue_11_Active = Player_Status_Value; break;
//            case "Statue_12_Active": Player.Statue_12_Active = Player_Status_Value; break;
//            case "Statue_13_Active": Player.Statue_13_Active = Player_Status_Value; break;
//            case "Statue_14_Active": Player.Statue_14_Active = Player_Status_Value; break;
//            case "Statue_15_Active": Player.Statue_15_Active = Player_Status_Value; break;
//            case "Statue_16_Active": Player.Statue_16_Active = Player_Status_Value; break;
//            case "Statue_17_Active": Player.Statue_17_Active = Player_Status_Value; break;
//            case "Statue_18_Active": Player.Statue_18_Active = Player_Status_Value; break;
//            case "Statue_19_Active": Player.Statue_19_Active = Player_Status_Value; break;
//            case "Statue_20_Active": Player.Statue_20_Active = Player_Status_Value; break;
//        }
//    }

//    void Convet_int_Character_Status(string Player_Status_Name, int Player_Status_Value)
//    {
//        switch (Player_Status_Name)
//        {
//            case "Player_EXP": Player.Player_EXP = Player_Status_Value; break;
//            case "Player_Level": Player.Player_Level = Player_Status_Value; break;
//            case "Token_01": Player.Token_01 = Player_Status_Value; break;
//            case "Token_02": Player.Token_02 = Player_Status_Value; break;
//            case "Token_03": Player.Token_03 = Player_Status_Value; break;
//            case "Token_04": Player.Token_04 = Player_Status_Value; break;
//            case "Token_05": Player.Token_05 = Player_Status_Value; break;
//            case "Diamond": Player.Diamond = Player_Status_Value; break;
//            case "Gold": Player.Gold = Player_Status_Value; break;

//            case "Tower_101_Level": Player.Tower_101_Level = Player_Status_Value; break;
//            case "Tower_101_EXP": Player.Tower_101_EXP = Player_Status_Value; break;
//            case "Tower_102_Level": Player.Tower_102_Level = Player_Status_Value; break;
//            case "Tower_102_EXP": Player.Tower_102_EXP = Player_Status_Value; break;
//            case "Tower_103_Level": Player.Tower_103_Level = Player_Status_Value; break;
//            case "Tower_103_EXP": Player.Tower_103_EXP = Player_Status_Value; break;
//            case "Tower_104_Level": Player.Tower_104_Level = Player_Status_Value; break;
//            case "Tower_104_EXP": Player.Tower_104_EXP = Player_Status_Value; break;
//            case "Tower_105_Level": Player.Tower_105_Level = Player_Status_Value; break;
//            case "Tower_105_EXP": Player.Tower_105_EXP = Player_Status_Value; break;
//            case "Tower_106_Level": Player.Tower_106_Level = Player_Status_Value; break;
//            case "Tower_106_EXP": Player.Tower_106_EXP = Player_Status_Value; break;
//            case "Tower_107_Level": Player.Tower_107_Level = Player_Status_Value; break;
//            case "Tower_107_EXP": Player.Tower_107_EXP = Player_Status_Value; break;
//            case "Tower_108_Level": Player.Tower_108_Level = Player_Status_Value; break;
//            case "Tower_108_EXP": Player.Tower_108_EXP = Player_Status_Value; break;
//            case "Tower_109_Level": Player.Tower_109_Level = Player_Status_Value; break;
//            case "Tower_109_EXP": Player.Tower_109_EXP = Player_Status_Value; break;
//            case "Tower_110_Level": Player.Tower_110_Level = Player_Status_Value; break;
//            case "Tower_110_EXP": Player.Tower_110_EXP = Player_Status_Value; break;

//            case "Tower_201_Level": Player.Tower_201_Level = Player_Status_Value; break;
//            case "Tower_201_EXP": Player.Tower_201_EXP = Player_Status_Value; break;
//            case "Tower_202_Level": Player.Tower_202_Level = Player_Status_Value; break;
//            case "Tower_202_EXP": Player.Tower_202_EXP = Player_Status_Value; break;
//            case "Tower_203_Level": Player.Tower_203_Level = Player_Status_Value; break;
//            case "Tower_203_EXP": Player.Tower_203_EXP = Player_Status_Value; break;
//            case "Tower_204_Level": Player.Tower_204_Level = Player_Status_Value; break;
//            case "Tower_204_EXP": Player.Tower_204_EXP = Player_Status_Value; break;
//            case "Tower_205_Level": Player.Tower_205_Level = Player_Status_Value; break;
//            case "Tower_205_EXP": Player.Tower_205_EXP = Player_Status_Value; break;
//            case "Tower_206_Level": Player.Tower_206_Level = Player_Status_Value; break;
//            case "Tower_206_EXP": Player.Tower_206_EXP = Player_Status_Value; break;
//            case "Tower_207_Level": Player.Tower_207_Level = Player_Status_Value; break;
//            case "Tower_207_EXP": Player.Tower_207_EXP = Player_Status_Value; break;
//            case "Tower_208_Level": Player.Tower_208_Level = Player_Status_Value; break;
//            case "Tower_208_EXP": Player.Tower_208_EXP = Player_Status_Value; break;
//            case "Tower_209_Level": Player.Tower_209_Level = Player_Status_Value; break;
//            case "Tower_209_EXP": Player.Tower_209_EXP = Player_Status_Value; break;
//            case "Tower_210_Level": Player.Tower_210_Level = Player_Status_Value; break;
//            case "Tower_210_EXP": Player.Tower_210_EXP = Player_Status_Value; break;

//            case "Tower_301_Level": Player.Tower_301_Level = Player_Status_Value; break;
//            case "Tower_301_EXP": Player.Tower_301_EXP = Player_Status_Value; break;
//            case "Tower_302_Level": Player.Tower_302_Level = Player_Status_Value; break;
//            case "Tower_302_EXP": Player.Tower_302_EXP = Player_Status_Value; break;
//            case "Tower_303_Level": Player.Tower_303_Level = Player_Status_Value; break;
//            case "Tower_303_EXP": Player.Tower_303_EXP = Player_Status_Value; break;
//            case "Tower_304_Level": Player.Tower_304_Level = Player_Status_Value; break;
//            case "Tower_304_EXP": Player.Tower_304_EXP = Player_Status_Value; break;
//            case "Tower_305_Level": Player.Tower_305_Level = Player_Status_Value; break;
//            case "Tower_305_EXP": Player.Tower_305_EXP = Player_Status_Value; break;
//            case "Tower_306_Level": Player.Tower_306_Level = Player_Status_Value; break;
//            case "Tower_306_EXP": Player.Tower_306_EXP = Player_Status_Value; break;
//            case "Tower_307_Level": Player.Tower_307_Level = Player_Status_Value; break;
//            case "Tower_307_EXP": Player.Tower_307_EXP = Player_Status_Value; break;
//            case "Tower_308_Level": Player.Tower_308_Level = Player_Status_Value; break;
//            case "Tower_308_EXP": Player.Tower_308_EXP = Player_Status_Value; break;
//            case "Tower_309_Level": Player.Tower_309_Level = Player_Status_Value; break;
//            case "Tower_309_EXP": Player.Tower_309_EXP = Player_Status_Value; break;
//            case "Tower_310_Level": Player.Tower_310_Level = Player_Status_Value; break;
//            case "Tower_310_EXP": Player.Tower_310_EXP = Player_Status_Value; break;

//            case "Tower_401_Level": Player.Tower_401_Level = Player_Status_Value; break;
//            case "Tower_401_EXP": Player.Tower_401_EXP = Player_Status_Value; break;
//            case "Tower_402_Level": Player.Tower_402_Level = Player_Status_Value; break;
//            case "Tower_402_EXP": Player.Tower_402_EXP = Player_Status_Value; break;
//            case "Tower_403_Level": Player.Tower_403_Level = Player_Status_Value; break;
//            case "Tower_403_EXP": Player.Tower_403_EXP = Player_Status_Value; break;
//            case "Tower_404_Level": Player.Tower_404_Level = Player_Status_Value; break;
//            case "Tower_404_EXP": Player.Tower_404_EXP = Player_Status_Value; break;
//            case "Tower_405_Level": Player.Tower_405_Level = Player_Status_Value; break;
//            case "Tower_405_EXP": Player.Tower_405_EXP = Player_Status_Value; break;
//            case "Tower_406_Level": Player.Tower_406_Level = Player_Status_Value; break;
//            case "Tower_406_EXP": Player.Tower_406_EXP = Player_Status_Value; break;
//            case "Tower_407_Level": Player.Tower_407_Level = Player_Status_Value; break;
//            case "Tower_407_EXP": Player.Tower_407_EXP = Player_Status_Value; break;
//            case "Tower_408_Level": Player.Tower_408_Level = Player_Status_Value; break;
//            case "Tower_408_EXP": Player.Tower_408_EXP = Player_Status_Value; break;
//            case "Tower_409_Level": Player.Tower_409_Level = Player_Status_Value; break;
//            case "Tower_409_EXP": Player.Tower_409_EXP = Player_Status_Value; break;
//            case "Tower_410_Level": Player.Tower_410_Level = Player_Status_Value; break;
//            case "Tower_410_EXP": Player.Tower_410_EXP = Player_Status_Value; break;

//            case "Tower_501_Level": Player.Tower_501_Level = Player_Status_Value; break;
//            case "Tower_501_EXP": Player.Tower_501_EXP = Player_Status_Value; break;
//            case "Tower_502_Level": Player.Tower_502_Level = Player_Status_Value; break;
//            case "Tower_502_EXP": Player.Tower_502_EXP = Player_Status_Value; break;
//            case "Tower_503_Level": Player.Tower_503_Level = Player_Status_Value; break;
//            case "Tower_503_EXP": Player.Tower_503_EXP = Player_Status_Value; break;
//            case "Tower_504_Level": Player.Tower_504_Level = Player_Status_Value; break;
//            case "Tower_504_EXP": Player.Tower_504_EXP = Player_Status_Value; break;
//            case "Tower_505_Level": Player.Tower_505_Level = Player_Status_Value; break;
//            case "Tower_505_EXP": Player.Tower_505_EXP = Player_Status_Value; break;
//            case "Tower_506_Level": Player.Tower_506_Level = Player_Status_Value; break;
//            case "Tower_506_EXP": Player.Tower_506_EXP = Player_Status_Value; break;
//            case "Tower_507_Level": Player.Tower_507_Level = Player_Status_Value; break;
//            case "Tower_507_EXP": Player.Tower_507_EXP = Player_Status_Value; break;
//            case "Tower_508_Level": Player.Tower_508_Level = Player_Status_Value; break;
//            case "Tower_508_EXP": Player.Tower_508_EXP = Player_Status_Value; break;
//            case "Tower_509_Level": Player.Tower_509_Level = Player_Status_Value; break;
//            case "Tower_509_EXP": Player.Tower_509_EXP = Player_Status_Value; break;
//            case "Tower_510_Level": Player.Tower_510_Level = Player_Status_Value; break;
//            case "Tower_510_EXP": Player.Tower_510_EXP = Player_Status_Value; break;

//            case "Tower_601_Level": Player.Tower_601_Level = Player_Status_Value; break;
//            case "Tower_601_EXP": Player.Tower_601_EXP = Player_Status_Value; break;
//            case "Tower_602_Level": Player.Tower_602_Level = Player_Status_Value; break;
//            case "Tower_602_EXP": Player.Tower_602_EXP = Player_Status_Value; break;
//            case "Tower_603_Level": Player.Tower_603_Level = Player_Status_Value; break;
//            case "Tower_603_EXP": Player.Tower_603_EXP = Player_Status_Value; break;
//            case "Tower_604_Level": Player.Tower_604_Level = Player_Status_Value; break;
//            case "Tower_604_EXP": Player.Tower_604_EXP = Player_Status_Value; break;
//            case "Tower_605_Level": Player.Tower_605_Level = Player_Status_Value; break;
//            case "Tower_605_EXP": Player.Tower_605_EXP = Player_Status_Value; break;
//            case "Tower_606_Level": Player.Tower_606_Level = Player_Status_Value; break;
//            case "Tower_606_EXP": Player.Tower_606_EXP = Player_Status_Value; break;
//            case "Tower_607_Level": Player.Tower_607_Level = Player_Status_Value; break;
//            case "Tower_607_EXP": Player.Tower_607_EXP = Player_Status_Value; break;
//            case "Tower_608_Level": Player.Tower_608_Level = Player_Status_Value; break;
//            case "Tower_608_EXP": Player.Tower_608_EXP = Player_Status_Value; break;
//            case "Tower_609_Level": Player.Tower_609_Level = Player_Status_Value; break;
//            case "Tower_609_EXP": Player.Tower_609_EXP = Player_Status_Value; break;
//            case "Tower_610_Level": Player.Tower_610_Level = Player_Status_Value; break;
//            case "Tower_610_EXP": Player.Tower_610_EXP = Player_Status_Value; break;
//            case "Tower_611_Level": Player.Tower_611_Level = Player_Status_Value; break;
//            case "Tower_611_EXP": Player.Tower_611_EXP = Player_Status_Value; break;
//            case "Tower_612_Level": Player.Tower_612_Level = Player_Status_Value; break;
//            case "Tower_612_EXP": Player.Tower_612_EXP = Player_Status_Value; break;
//            case "Tower_613_Level": Player.Tower_613_Level = Player_Status_Value; break;
//            case "Tower_613_EXP": Player.Tower_613_EXP = Player_Status_Value; break;
//            case "Tower_614_Level": Player.Tower_614_Level = Player_Status_Value; break;
//            case "Tower_614_EXP": Player.Tower_614_EXP = Player_Status_Value; break;
//            case "Tower_615_Level": Player.Tower_615_Level = Player_Status_Value; break;
//            case "Tower_615_EXP": Player.Tower_615_EXP = Player_Status_Value; break;
//            case "Tower_616_Level": Player.Tower_616_Level = Player_Status_Value; break;
//            case "Tower_616_EXP": Player.Tower_616_EXP = Player_Status_Value; break;
//            case "Tower_617_Level": Player.Tower_617_Level = Player_Status_Value; break;
//            case "Tower_617_EXP": Player.Tower_617_EXP = Player_Status_Value; break;
//            case "Tower_618_Level": Player.Tower_618_Level = Player_Status_Value; break;
//            case "Tower_618_EXP": Player.Tower_618_EXP = Player_Status_Value; break;
//            case "Tower_619_Level": Player.Tower_619_Level = Player_Status_Value; break;
//            case "Tower_619_EXP": Player.Tower_619_EXP = Player_Status_Value; break;
//            case "Tower_620_Level": Player.Tower_620_Level = Player_Status_Value; break;
//            case "Tower_620_EXP": Player.Tower_620_EXP = Player_Status_Value; break;

//            case "Tower_701_Level": Player.Tower_701_Level = Player_Status_Value; break;
//            case "Tower_701_EXP": Player.Tower_701_EXP = Player_Status_Value; break;
//            case "Tower_702_Level": Player.Tower_702_Level = Player_Status_Value; break;
//            case "Tower_702_EXP": Player.Tower_702_EXP = Player_Status_Value; break;
//            case "Tower_703_Level": Player.Tower_703_Level = Player_Status_Value; break;
//            case "Tower_703_EXP": Player.Tower_703_EXP = Player_Status_Value; break;
//            case "Tower_704_Level": Player.Tower_704_Level = Player_Status_Value; break;
//            case "Tower_704_EXP": Player.Tower_704_EXP = Player_Status_Value; break;
//            case "Tower_705_Level": Player.Tower_705_Level = Player_Status_Value; break;
//            case "Tower_705_EXP": Player.Tower_705_EXP = Player_Status_Value; break;
//            case "Tower_706_Level": Player.Tower_706_Level = Player_Status_Value; break;
//            case "Tower_706_EXP": Player.Tower_706_EXP = Player_Status_Value; break;
//            case "Tower_707_Level": Player.Tower_707_Level = Player_Status_Value; break;
//            case "Tower_707_EXP": Player.Tower_707_EXP = Player_Status_Value; break;
//            case "Tower_708_Level": Player.Tower_708_Level = Player_Status_Value; break;
//            case "Tower_708_EXP": Player.Tower_708_EXP = Player_Status_Value; break;
//            case "Tower_709_Level": Player.Tower_709_Level = Player_Status_Value; break;
//            case "Tower_709_EXP": Player.Tower_709_EXP = Player_Status_Value; break;
//            case "Tower_710_Level": Player.Tower_710_Level = Player_Status_Value; break;
//            case "Tower_710_EXP": Player.Tower_710_EXP = Player_Status_Value; break;
//            case "Tower_711_Level": Player.Tower_711_Level = Player_Status_Value; break;
//            case "Tower_711_EXP": Player.Tower_711_EXP = Player_Status_Value; break;
//            case "Tower_712_Level": Player.Tower_712_Level = Player_Status_Value; break;
//            case "Tower_712_EXP": Player.Tower_712_EXP = Player_Status_Value; break;
//            case "Tower_713_Level": Player.Tower_713_Level = Player_Status_Value; break;
//            case "Tower_713_EXP": Player.Tower_713_EXP = Player_Status_Value; break;
//            case "Tower_714_Level": Player.Tower_714_Level = Player_Status_Value; break;
//            case "Tower_714_EXP": Player.Tower_714_EXP = Player_Status_Value; break;
//            case "Tower_715_Level": Player.Tower_715_Level = Player_Status_Value; break;
//            case "Tower_715_EXP": Player.Tower_715_EXP = Player_Status_Value; break;
//            case "Tower_716_Level": Player.Tower_716_Level = Player_Status_Value; break;
//            case "Tower_716_EXP": Player.Tower_716_EXP = Player_Status_Value; break;
//            case "Tower_717_Level": Player.Tower_717_Level = Player_Status_Value; break;
//            case "Tower_717_EXP": Player.Tower_717_EXP = Player_Status_Value; break;
//            case "Tower_718_Level": Player.Tower_718_Level = Player_Status_Value; break;
//            case "Tower_718_EXP": Player.Tower_718_EXP = Player_Status_Value; break;
//            case "Tower_719_Level": Player.Tower_719_Level = Player_Status_Value; break;
//            case "Tower_719_EXP": Player.Tower_719_EXP = Player_Status_Value; break;
//            case "Tower_720_Level": Player.Tower_720_Level = Player_Status_Value; break;
//            case "Tower_720_EXP": Player.Tower_720_EXP = Player_Status_Value; break;
//            case "Tower_721_Level": Player.Tower_721_Level = Player_Status_Value; break;
//            case "Tower_721_EXP": Player.Tower_721_EXP = Player_Status_Value; break;
//            case "Tower_722_Level": Player.Tower_722_Level = Player_Status_Value; break;
//            case "Tower_722_EXP": Player.Tower_722_EXP = Player_Status_Value; break;
//            case "Tower_723_Level": Player.Tower_723_Level = Player_Status_Value; break;
//            case "Tower_723_EXP": Player.Tower_723_EXP = Player_Status_Value; break;
//            case "Tower_724_Level": Player.Tower_724_Level = Player_Status_Value; break;
//            case "Tower_724_EXP": Player.Tower_724_EXP = Player_Status_Value; break;
//            case "Tower_725_Level": Player.Tower_725_Level = Player_Status_Value; break;
//            case "Tower_725_EXP": Player.Tower_725_EXP = Player_Status_Value; break;
//            case "Tower_726_Level": Player.Tower_726_Level = Player_Status_Value; break;
//            case "Tower_726_EXP": Player.Tower_726_EXP = Player_Status_Value; break;
//            case "Tower_727_Level": Player.Tower_727_Level = Player_Status_Value; break;
//            case "Tower_727_EXP": Player.Tower_727_EXP = Player_Status_Value; break;
//            case "Tower_728_Level": Player.Tower_728_Level = Player_Status_Value; break;
//            case "Tower_728_EXP": Player.Tower_728_EXP = Player_Status_Value; break;
//            case "Tower_729_Level": Player.Tower_729_Level = Player_Status_Value; break;
//            case "Tower_729_EXP": Player.Tower_729_EXP = Player_Status_Value; break;
//            case "Tower_730_Level": Player.Tower_730_Level = Player_Status_Value; break;
//            case "Tower_730_EXP": Player.Tower_730_EXP = Player_Status_Value; break;

//            case "Statue_01_Level": Player.Statue_01_Level = Player_Status_Value; break;
//            case "Statue_01_EXP": Player.Statue_01_EXP = Player_Status_Value; break;
//            case "Statue_02_Level": Player.Statue_02_Level = Player_Status_Value; break;
//            case "Statue_02_EXP": Player.Statue_02_EXP = Player_Status_Value; break;
//            case "Statue_03_Level": Player.Statue_03_Level = Player_Status_Value; break;
//            case "Statue_03_EXP": Player.Statue_03_EXP = Player_Status_Value; break;
//            case "Statue_04_Level": Player.Statue_04_Level = Player_Status_Value; break;
//            case "Statue_04_EXP": Player.Statue_04_EXP = Player_Status_Value; break;
//            case "Statue_05_Level": Player.Statue_05_Level = Player_Status_Value; break;
//            case "Statue_05_EXP": Player.Statue_05_EXP = Player_Status_Value; break;
//            case "Statue_06_Level": Player.Statue_06_Level = Player_Status_Value; break;
//            case "Statue_06_EXP": Player.Statue_06_EXP = Player_Status_Value; break;
//            case "Statue_07_Level": Player.Statue_07_Level = Player_Status_Value; break;
//            case "Statue_07_EXP": Player.Statue_07_EXP = Player_Status_Value; break;
//            case "Statue_08_Level": Player.Statue_08_Level = Player_Status_Value; break;
//            case "Statue_08_EXP": Player.Statue_08_EXP = Player_Status_Value; break;
//            case "Statue_09_Level": Player.Statue_09_Level = Player_Status_Value; break;
//            case "Statue_09_EXP": Player.Statue_09_EXP = Player_Status_Value; break;
//            case "Statue_10_Level": Player.Statue_10_Level = Player_Status_Value; break;
//            case "Statue_10_EXP": Player.Statue_10_EXP = Player_Status_Value; break;
//            case "Statue_11_Level": Player.Statue_11_Level = Player_Status_Value; break;
//            case "Statue_11_EXP": Player.Statue_11_EXP = Player_Status_Value; break;
//            case "Statue_12_Level": Player.Statue_12_Level = Player_Status_Value; break;
//            case "Statue_12_EXP": Player.Statue_12_EXP = Player_Status_Value; break;
//            case "Statue_13_Level": Player.Statue_13_Level = Player_Status_Value; break;
//            case "Statue_13_EXP": Player.Statue_13_EXP = Player_Status_Value; break;
//            case "Statue_14_Level": Player.Statue_14_Level = Player_Status_Value; break;
//            case "Statue_14_EXP": Player.Statue_14_EXP = Player_Status_Value; break;
//            case "Statue_15_Level": Player.Statue_15_Level = Player_Status_Value; break;
//            case "Statue_15_EXP": Player.Statue_15_EXP = Player_Status_Value; break;
//            case "Statue_16_Level": Player.Statue_16_Level = Player_Status_Value; break;
//            case "Statue_16_EXP": Player.Statue_16_EXP = Player_Status_Value; break;
//            case "Statue_17_Level": Player.Statue_17_Level = Player_Status_Value; break;
//            case "Statue_17_EXP": Player.Statue_17_EXP = Player_Status_Value; break;
//            case "Statue_18_Level": Player.Statue_18_Level = Player_Status_Value; break;
//            case "Statue_18_EXP": Player.Statue_18_EXP = Player_Status_Value; break;
//            case "Statue_19_Level": Player.Statue_19_Level = Player_Status_Value; break;
//            case "Statue_19_EXP": Player.Statue_19_EXP = Player_Status_Value; break;
//            case "Statue_20_Level": Player.Statue_20_Level = Player_Status_Value; break;
//            case "Statue_20_EXP": Player.Statue_20_EXP = Player_Status_Value; break;

//            case "Desk_1_01": Player.Desk_1_01 = Player_Status_Value; break;
//            case "Desk_1_02": Player.Desk_1_02 = Player_Status_Value; break;
//            case "Desk_1_03": Player.Desk_1_03 = Player_Status_Value; break;
//            case "Desk_1_04": Player.Desk_1_04 = Player_Status_Value; break;
//            case "Desk_1_05": Player.Desk_1_05 = Player_Status_Value; break;

//            case "Desk_2_01": Player.Desk_2_01 = Player_Status_Value; break;
//            case "Desk_2_02": Player.Desk_2_02 = Player_Status_Value; break;
//            case "Desk_2_03": Player.Desk_2_03 = Player_Status_Value; break;
//            case "Desk_2_04": Player.Desk_2_04 = Player_Status_Value; break;
//            case "Desk_2_05": Player.Desk_2_05 = Player_Status_Value; break;

//            case "Desk_3_01": Player.Desk_3_01 = Player_Status_Value; break;
//            case "Desk_3_02": Player.Desk_3_02 = Player_Status_Value; break;
//            case "Desk_3_03": Player.Desk_3_03 = Player_Status_Value; break;
//            case "Desk_3_04": Player.Desk_3_04 = Player_Status_Value; break;
//            case "Desk_3_05": Player.Desk_3_05 = Player_Status_Value; break;

//            case "Desk_4_01": Player.Desk_4_01 = Player_Status_Value; break;
//            case "Desk_4_02": Player.Desk_4_02 = Player_Status_Value; break;
//            case "Desk_4_03": Player.Desk_4_03 = Player_Status_Value; break;
//            case "Desk_4_04": Player.Desk_4_04 = Player_Status_Value; break;
//            case "Desk_4_05": Player.Desk_4_05 = Player_Status_Value; break;

//            case "Desk_5_01": Player.Desk_5_01 = Player_Status_Value; break;
//            case "Desk_5_02": Player.Desk_5_02 = Player_Status_Value; break;
//            case "Desk_5_03": Player.Desk_5_03 = Player_Status_Value; break;
//            case "Desk_5_04": Player.Desk_5_04 = Player_Status_Value; break;
//            case "Desk_5_05": Player.Desk_5_05 = Player_Status_Value; break;

//            case "Desk_6_01": Player.Desk_6_01 = Player_Status_Value; break;
//            case "Desk_6_02": Player.Desk_6_02 = Player_Status_Value; break;
//            case "Desk_6_03": Player.Desk_6_03 = Player_Status_Value; break;
//            case "Desk_6_04": Player.Desk_6_04 = Player_Status_Value; break;
//            case "Desk_6_05": Player.Desk_6_05 = Player_Status_Value; break;

//            case "Desk_7_01": Player.Desk_7_01 = Player_Status_Value; break;
//            case "Desk_7_02": Player.Desk_7_02 = Player_Status_Value; break;
//            case "Desk_7_03": Player.Desk_7_03 = Player_Status_Value; break;
//            case "Desk_7_04": Player.Desk_7_04 = Player_Status_Value; break;
//            case "Desk_7_05": Player.Desk_7_05 = Player_Status_Value; break;

//            case "Desk_8_01": Player.Desk_8_01 = Player_Status_Value; break;
//            case "Desk_8_02": Player.Desk_8_02 = Player_Status_Value; break;
//            case "Desk_8_03": Player.Desk_8_03 = Player_Status_Value; break;
//            case "Desk_8_04": Player.Desk_8_04 = Player_Status_Value; break;
//            case "Desk_8_05": Player.Desk_8_05 = Player_Status_Value; break;

//            case "Current_Desk": Player.Current_Desk = Player_Status_Value; break;

//            case "Critical_Damage": Player.Critical_Damage = Player_Status_Value; break;
//        }
//    }

//    void Convert_Time_Character_Status(string Player_Status_Name, long Player_Status_Value)
//    {
//        switch (Player_Status_Name)
//        {
//            case "Timer_01": Player.Timer_01 = Player_Status_Value; break;
//            case "Timer_02": Player.Timer_02 = Player_Status_Value; break;
//            case "Timer_03": Player.Timer_03 = Player_Status_Value; break;
//            case "Timer_04": Player.Timer_04 = Player_Status_Value; break;
//            case "Timer_05": Player.Timer_05 = Player_Status_Value; break;
//        }
//    }

//    public void Create_New_Character_Status_To_Server()
//    {
//        string character = JsonUtility.ToJson(Player);
//        Debug.Log("Create_New_Character_Status_To_Server " + Player);
//        reference.Child(Player_ID).SetRawJsonValueAsync(character);

//        Update_To_FireBase();
//    }
//}

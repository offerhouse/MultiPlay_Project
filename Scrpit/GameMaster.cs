using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using MasterServerToolkit.Networking;
using MasterServerToolkit.MasterServer;

public class GameMaster : NetworkBehaviour
{
    public GameObject Object_Manager;
    Object_Manager object_Manager;
    public Player_Status.Player Player1_Online_Profile; // for stroe player status . Tower level . skill . desk etc.
    public Player_Status.Player Player2_Online_Profile;
    public Player_Status.Player Player3_Online_Profile;
    public Player_Status.Player Player4_Online_Profile;

    public int Player1_Gold, Player2_Gold, Player3_Gold, Player4_Gold;

    public int Player_Tower_Gold_01 = 10, Player_Tower_Gold_02 = 10, Player_Tower_Gold_03 = 10, Player_Tower_Gold_04 = 10;

    public GameObject Player1; // for PlayerNetwork . control game and Connect between Server and Local .
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Map_Manager;

    GameObject Player_01_Map_Controller;
    GameObject Player_02_Map_Controller;
    GameObject Player_03_Map_Controller;
    GameObject Player_04_Map_Controller;

    public int Player_01_Map_Point;
    public int Player_02_Map_Point;
    public int Player_03_Map_Point;
    public int Player_04_Map_Point;

    public GameObject Player01_Map_Point_Loc;
    public GameObject Player02_Map_Point_Loc;
    public GameObject Player03_Map_Point_Loc;
    public GameObject Player04_Map_Point_Loc;

    float Core_HP_Timer; // Send Core HP to local each second
    public GameObject Player_01_Core, Player_02_Core, Player_03_Core, Player_04_Core;
    int Player_01_Core_HP = 100, Player_02_Core_HP = 100, Player_03_Core_HP = 100, Player_04_Core_HP = 100;

    public GameObject Map_01, Map_02, Map_03, Map_04, Map_05, Map_06, Map_07, Map_08, Map_09, Map_10;
    public GameObject Enemy_01, Enemy_02, Enemy_03, Enemy_04, Enemy_05, Enemy_06, Enemy_07, Enemy_08, Enemy_09, Enemy_10;

    public GameObject Protector_01, Protector_02, Protector_03, Protector_04, Protector_05, Protector_06;
    public GameObject Attacker_01, Attacker_02, Attacker_03, Attacker_04, Attacker_05, Attacker_06;

    int Protector_Number = 0, Enemy_Number = 0, Attacker_Number = 0;

    float Spawn_Timer = 0;
    float Protector_Control_Timer = 0;
    Map_Controller Map_Controller_01;
    Map_Controller Map_Controller_02;
    Map_Controller Map_Controller_03;
    Map_Controller Map_Controller_04;

    public int Player1_Protector_Number = 0;
    public int Player2_Protector_Number = 0;
    public int Player3_Protector_Number = 0;
    public int Player4_Protector_Number = 0;

    int Player_Spwan_Number_1 = 0;
    int Player_Spwan_Number_2 = 0;
    int Player_Spwan_Number_3 = 0;
    int Player_Spwan_Number_4 = 0;

    float Spwaner_01_HP = 100, Spwaner_02_HP = 100, Spwaner_03_HP = 100, Spwaner_04_HP = 100;
    float Spwaner_01_DMG = 30, Spwaner_02_DMG = 30, Spwaner_03_DMG = 30, Spwaner_04_DMG = 30;
    int Spwaner_01_GOLD = 10, Spwaner_02_GOLD = 10, Spwaner_03_GOLD = 10, Spwaner_04_GOLD = 10;
    int Spwan_Number_01, Spwan_Number_02, Spwan_Number_03, Spwan_Number_04;
    public short Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04;

    public List<GameObject> Enemy = new List<GameObject>();
    public List<GameObject> HolyLight_List = new List<GameObject>();
    public List<GameObject> Ground_Boom_List = new List<GameObject>();

    int Attacker_Change_To_Enemy_Number = 0;
    public short op_room_Code = 0;
    bool Player_1_in_Game, Player_2_in_Game, Player_3_in_Game, Player_4_in_Game;
    bool Player_1_Win, Player_2_Win, Player_3_Win, Player_4_Win;
    public bool End_Game;
    #region About Task
    short Player1_Combine_Point, Player2_Combine_Point, Player3_Combine_Point, Player4_Combine_Point;
    int Player1_High_Damage, Player2_High_Damage, Player3_High_Damage, Player4_High_Damage;
    int Player1_Highest_HP_Enemy, Player2_Highest_HP_Enemy, Player3_Highest_HP_Enemy, Player4_Highest_HP_Enemy;
    int Player1_Killed_Enemy, Player2_Killed_Enemy, Player3_Killed_Enemy, Player4_Killed_Enemy;
    short Wave_Number = 0, Wave_Stage = 1, Soul_Stage = 1, Soul_Number = 0, Diamond = 0, Gold = 0;
    bool End_Game_Send_Finish_Player1, End_Game_Send_Finish_Player2, End_Game_Send_Finish_Player3, End_Game_Send_Finish_Player4;
    #endregion

    public void GameMaster_Start()
    {
        Debug.Log("GameMaster_Start");
        object_Manager = Object_Manager.GetComponent<Object_Manager>();
        op_room_Code = object_Manager.Get_Current_Match_Type_OP_Code();

        if (op_room_Code == (short)OP_Room_Code.Match1v1 || op_room_Code == (short)OP_Room_Code.Match2op)
        {
            Player_1_in_Game = true; Player_2_in_Game = true; Player_3_in_Game = false; Player_4_in_Game = false;
        }
        if (op_room_Code == (short)OP_Room_Code.Match2v2 || op_room_Code == (short)OP_Room_Code.Match4op)
        {
            Player_1_in_Game = true; Player_2_in_Game = true; Player_3_in_Game = true; Player_4_in_Game = true;
        }

        if (Player01_Map_Point_Loc != null && Player_1_in_Game)
        {
            Player_01_Map_Controller = Instantiate(Get_Map_Type(Map_Type_Player_01), Player01_Map_Point_Loc.transform.position, Quaternion.identity);
            Map_Controller_01 = Player_01_Map_Controller.GetComponent<Map_Controller>();

            Set_Map_Controller(Map_Controller_01, 1);
            Set_Core(Player_01_Map_Controller, 1, Player1, Object_Manager, gameObject);

            if (Player1 != null)
            {
                Object_Manager.GetComponent<Object_Manager>().GM_Set_MapController_and_Core(1, Player_01_Map_Controller, Player_01_Core);
                Set_Player_Number_To_Tower_Controller(Map_Controller_01, 1, Player1);
                Player_01_Core.GetComponent<Core>().Tower_Controller = Map_Controller_01.Tower_Controller;
                Send_Desk_Level_To_Local(Player1);
                Send_Desk_Number_To_Local(Player1);
                Set_Gold(1, 2000);

                short[] PlayerNumber__OPCode__AllPlayerMapType = new short[] { 1, op_room_Code, Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04 };
                Player1.GetComponent<Player_Network>().GameMaster_Setup_Finish_Run_1_Time_Only(PlayerNumber__OPCode__AllPlayerMapType);
            }
        }
        if (Player02_Map_Point_Loc != null && Player_2_in_Game)
        {
            Player_02_Map_Controller = Instantiate(Get_Map_Type(Map_Type_Player_02), Player02_Map_Point_Loc.transform.position, Quaternion.identity);
            Map_Controller_02 = Player_02_Map_Controller.GetComponent<Map_Controller>();

            Set_Map_Controller(Map_Controller_02, 2);
            Set_Core(Player_02_Map_Controller, 2, Player2, Object_Manager, gameObject);

            if (Player2 != null)
            {
                Object_Manager.GetComponent<Object_Manager>().GM_Set_MapController_and_Core(2, Player_02_Map_Controller, Player_02_Core);
                Set_Player_Number_To_Tower_Controller(Map_Controller_02, 2, Player2);
                Player_02_Core.GetComponent<Core>().Tower_Controller = Map_Controller_02.Tower_Controller;
                Send_Desk_Level_To_Local(Player2);
                Send_Desk_Number_To_Local(Player2);
                Set_Gold(2, 2000);

                short[] PlayerNumber__OPCode__AllPlayerMapType = new short[] { 2, op_room_Code, Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04 };
                Player2.GetComponent<Player_Network>().GameMaster_Setup_Finish_Run_1_Time_Only(PlayerNumber__OPCode__AllPlayerMapType);
            }
        }
        if (Player03_Map_Point_Loc != null && Player_3_in_Game)
        {
            Player_03_Map_Controller = Instantiate(Get_Map_Type(Map_Type_Player_03), Player03_Map_Point_Loc.transform.position, Quaternion.identity);
            Map_Controller_03 = Player_03_Map_Controller.GetComponent<Map_Controller>();

            Set_Map_Controller(Map_Controller_03, 3);
            Set_Core(Player_03_Map_Controller, 3, Player3, Object_Manager, gameObject);

            if (Player3 != null)
            {
                Object_Manager.GetComponent<Object_Manager>().GM_Set_MapController_and_Core(3, Player_03_Map_Controller, Player_03_Core);
                Set_Player_Number_To_Tower_Controller(Map_Controller_03, 3, Player3);
                Player_03_Core.GetComponent<Core>().Tower_Controller = Map_Controller_03.Tower_Controller;
                Send_Desk_Level_To_Local(Player3);
                Send_Desk_Number_To_Local(Player3);
                Set_Gold(3, 2000);

                short[] PlayerNumber__OPCode__AllPlayerMapType = new short[] { 3, op_room_Code, Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04 };
                Player3.GetComponent<Player_Network>().GameMaster_Setup_Finish_Run_1_Time_Only(PlayerNumber__OPCode__AllPlayerMapType);
            }
        }
        if (Player04_Map_Point_Loc != null && Player_4_in_Game)
        {
            Player_04_Map_Controller = Instantiate(Get_Map_Type(Map_Type_Player_04), Player04_Map_Point_Loc.transform.position, Quaternion.identity);
            Map_Controller_04 = Player_04_Map_Controller.GetComponent<Map_Controller>();

            Set_Map_Controller(Map_Controller_04, 4);
            Set_Core(Player_04_Map_Controller, 4, Player4, Object_Manager, gameObject);

            if (Player4 != null)
            {
                Object_Manager.GetComponent<Object_Manager>().GM_Set_MapController_and_Core(4, Player_04_Map_Controller, Player_04_Core);
                Set_Player_Number_To_Tower_Controller(Map_Controller_04, 4, Player4);
                Player_04_Core.GetComponent<Core>().Tower_Controller = Map_Controller_04.Tower_Controller;
                Send_Desk_Level_To_Local(Player4);
                Send_Desk_Number_To_Local(Player4);
                Set_Gold(4, 2000);

                short[] PlayerNumber__OPCode__AllPlayerMapType = new short[] { 4, op_room_Code, Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04 };
                Player4.GetComponent<Player_Network>().GameMaster_Setup_Finish_Run_1_Time_Only(PlayerNumber__OPCode__AllPlayerMapType);
            }
        }

        Setup_Map_Walk_Path();

        void Set_Player_Number_To_Tower_Controller(Map_Controller Map_Controller, int number, GameObject Player)
        {
            GameObject Tower_Controller = Map_Controller.Tower_Controller;
            if (Tower_Controller != null)
            {
                Object_Manager.GetComponent<Object_Manager>().Set_Tower_Controller(number, Tower_Controller);
                Tower_Controller.GetComponent<Tower_Controller>().Player_Number = number;
                Tower_Controller.GetComponent<Tower_Controller>().Player_Profile = Player;
                Player_Status.Player player_profile = Get_Player_Profile(number);
                int Current_Desk = player_profile.Current_Desk;
                int[] Desk = Player.GetComponent<Player_Status>().Get_Current_Tower_Desk(Current_Desk);
                Tower_Controller.GetComponent<Tower_Controller>().Set_Desk_Tower(Desk);

                Tower_Controller.GetComponent<Tower_Controller>().GameMaster = gameObject;
                Tower_Controller.GetComponent<Tower_Controller>().Map_Manager = Map_Manager;
                Tower_Controller.GetComponent<Tower_Controller>().Power_Up_Desk_1 = 1;
                Tower_Controller.GetComponent<Tower_Controller>().Power_Up_Desk_2 = 1;
                Tower_Controller.GetComponent<Tower_Controller>().Power_Up_Desk_3 = 1;
                Tower_Controller.GetComponent<Tower_Controller>().Power_Up_Desk_4 = 1;
                Tower_Controller.GetComponent<Tower_Controller>().Power_Up_Desk_5 = 1;
                Tower_Controller.GetComponent<Tower_Controller>().Tower_Controller_Setup_Start();
            }
        }

        void Set_Map_Controller(Map_Controller map, short player_Number)
        {
            map.Map_Manager = Map_Manager;
            map.Player_Number = player_Number;
            map.Set_End_Point_Player_Number();
            map.op_room_Code = op_room_Code;
        }

        void Set_Core(GameObject map_Controller, short player_Number, GameObject Player_Obj, GameObject Object_Manager, GameObject GM)
        {
            GameObject core_Obj = map_Controller.GetComponent<Map_Controller>().Core;
            core_Obj.GetComponent<Core>().Player_Number = player_Number;
            core_Obj.GetComponent<Core>().Player_Obj = Player_Obj;
            core_Obj.GetComponent<Core>().Object_Manager = Object_Manager;
            core_Obj.GetComponent<Core>().GM = GM;

            if (player_Number == 1)
                Player_01_Core = core_Obj;
            if (player_Number == 2)
                Player_02_Core = core_Obj;
            if (player_Number == 3)
                Player_03_Core = core_Obj;
            if (player_Number == 4)
                Player_04_Core = core_Obj;
        }

        gameObject.name = "Game_Master";
    }

    // Update is called once per frame
    void Update()
    {
        if (End_Game)
        {
            //Debug.Log("End_Game || " + op_room_Code);
            return;
        }

        if (Object_Manager == null)
        {
            //Debug.Log("Object_Manager_Null");
            return;
        }

        if (!Object_Manager.GetComponent<Object_Manager>().Object_Manager_Finish)
        {
            Debug.Log("Object_Manager_Not_Finish");
            return;
        }

        Spawn_Timer += Time.deltaTime;
        Protector_Control_Timer += Time.deltaTime;
        Core_HP_Timer += Time.deltaTime;
        if (Spawn_Timer >= 0.5f)
        {
            Spawn_Timer = 0;
            Spawn_Enemy(1);
            Spwaner_01_HP *= 1.01f;
            Spwaner_02_HP *= 1.01f;
            Spwaner_03_HP *= 1.01f;
            Spwaner_04_HP *= 1.01f;
            Soul_Number += Soul_Stage;
            if (Enemy_Number == 10)
            {
                Enemy_Number = 0;
                Wave_Stage++;
                Wave_Number++;
                Gold += 50;
                if (Gold >= 30000)
                    Gold = 30000;
            }
            if (Wave_Stage == 10)
            {
                Wave_Stage = 0;
                Soul_Stage++;
                Diamond += 1;
            }
        }
        if (Protector_Control_Timer >= 1) // if Protector Patrol too close will turn ahead to another direction
        {
            Protector_Control_Timer = 0;
            Protector_Control(1);
            Protector_Control(2);
        }

        if (Core_HP_Timer >= 1)
        {
            if (op_room_Code == 0)
                return;

            Core_HP_Timer = 0;
            Send_Core_HP_To_Player();
        }

        Check_Player_Core_HP(); //Non Stop Game to test
    }

    void Check_Player_Core_HP()
    {
        if (Player_01_Core_HP < 1)
            Set_End_Game(1);

        if (Player_02_Core_HP < 2)
            Set_End_Game(2);

        if (Player_03_Core_HP < 3)
            Set_End_Game(3);

        if (Player_04_Core_HP < 4)
            Set_End_Game(4);

        void Set_End_Game(short Player_Number)
        {
            if (End_Game)
                return;
            //Debug.Log("Set_End_Game");
            End_Game = true;

            Disable_Core();

            Tower[] m_towers = GameObject.FindObjectsOfType<Tower>();
            foreach (Tower m_tower in m_towers)
                m_tower.enabled = false;

            Enemy[] m_enemys = GameObject.FindObjectsOfType<Enemy>();
            foreach (Enemy m_enemy in m_enemys)
                m_enemy.enabled = false;

            switch (op_room_Code)
            {
                case ((short)OP_Room_Code.Match1v1):
                    switch (Player_Number)
                    {
                        case (1): Set_Winner(false, true, false, false); break;
                        case (2): Set_Winner(true, false, false, false); break;
                    }
                    break;
                case ((short)OP_Room_Code.Match2v2):
                    switch (Player_Number)
                    {
                        case (1): Set_Winner(false, false, true, true); break;
                        case (2): Set_Winner(false, false, true, true); break;
                        case (3): Set_Winner(true, true, false, false); break;
                        case (4): Set_Winner(true, true, false, false); break;
                    }
                    break;
                case ((short)OP_Room_Code.Match2op):

                    break;
                case ((short)OP_Room_Code.Match4op):

                    break;
            }

            void Set_Winner(bool player1_Win, bool player2_Win, bool player3_Win, bool player4_Win)
            {
                Player_1_Win = player1_Win;
                Player_2_Win = player2_Win;
                Player_3_Win = player3_Win;
                Player_4_Win = player4_Win;
            }

            Set_and_Send_Out();

            void Set_and_Send_Out()
            {
                RoomOptions Player1_Packet = new RoomOptions();
                RoomOptions Player2_Packet = new RoomOptions();
                RoomOptions Player3_Packet = new RoomOptions();
                RoomOptions Player4_Packet = new RoomOptions();
                short exp = 0; short diamond = 0; short soul = 0;
                if (op_room_Code == (short)OP_Room_Code.Match1v1 || op_room_Code == (short)OP_Room_Code.Match2v2)
                {
                    exp = 10;
                    if (object_Manager.User_ID1 == null)
                        End_Game_Send_Finish_Player1 = true;
                    if (object_Manager.User_ID2 == null)
                        End_Game_Send_Finish_Player2 = true;
                    End_Game_Send_Finish_Player3 = true;
                    End_Game_Send_Finish_Player4 = true;
                }
                if (op_room_Code == (short)OP_Room_Code.Match2v2)
                {
                    exp = 10;
                    if (object_Manager.User_ID3 == null)
                        End_Game_Send_Finish_Player3 = true;
                    if (object_Manager.User_ID4 == null)
                        End_Game_Send_Finish_Player4 = true;
                }
                if (op_room_Code == (short)OP_Room_Code.Match2op || op_room_Code == (short)OP_Room_Code.Match4op)
                {
                    soul = Soul_Number;
                    if (object_Manager.User_ID1 == null)
                        End_Game_Send_Finish_Player1 = true;
                    if (object_Manager.User_ID2 == null)
                        End_Game_Send_Finish_Player2 = true;
                    if (object_Manager.User_ID3 == null)
                        End_Game_Send_Finish_Player3 = true;
                    if (object_Manager.User_ID4 == null)
                        End_Game_Send_Finish_Player4 = true;
                }

                if (object_Manager.User_ID1 != null)
                {
                    Player1_Packet = Set_Packet(object_Manager.User_ID1, exp, Gold, diamond, soul, Player1_Combine_Point,
                        Player1_High_Damage, Player1_Highest_HP_Enemy, Player1_Killed_Enemy, Wave_Number);
                    if (Player_1_Win)
                        Player1_Packet.CustomOptions.Set("Winner", true);
                    Send_Reward_To_MST(Player1_Packet, 1);
                }

                if (object_Manager.User_ID2 != null)
                {
                    Player2_Packet = Set_Packet(object_Manager.User_ID2, exp, Gold, diamond, soul, Player2_Combine_Point,
                        Player2_High_Damage, Player2_Highest_HP_Enemy, Player2_Killed_Enemy, Wave_Number);
                    if (Player_2_Win)
                        Player2_Packet.CustomOptions.Set("Winner", true);
                    Send_Reward_To_MST(Player2_Packet, 2);
                }

                if (object_Manager.User_ID3 != null)
                {
                    Player3_Packet = Set_Packet(object_Manager.User_ID3, exp, Gold, diamond, soul, Player3_Combine_Point,
                        Player3_High_Damage, Player3_Highest_HP_Enemy, Player3_Killed_Enemy, Wave_Number);
                    if (Player_3_Win)
                        Player3_Packet.CustomOptions.Set("Winner", true);
                    Send_Reward_To_MST(Player3_Packet, 3);
                }

                if (object_Manager.User_ID4 != null)
                {
                    Player4_Packet = Set_Packet(object_Manager.User_ID4, exp, Gold, diamond, soul, Player4_Combine_Point,
                        Player4_High_Damage, Player4_Highest_HP_Enemy, Player4_Killed_Enemy, Wave_Number);
                    if (Player_4_Win)
                        Player4_Packet.CustomOptions.Set("Winner", true);
                    Send_Reward_To_MST(Player4_Packet, 4);
                }
            }

            RoomOptions Set_Packet(string User_ID, short exp, short gold, short diamond, short soul, int combine_point, int high_damage,
                int highest_hp_enemy, int killed_enemy, short wave_number)
            {
                string Room_Code = null;
                RoomOptions room_option = new RoomOptions();
                room_option.CustomOptions.Add("Winner", false);
                if (op_room_Code == (short)OP_Room_Code.Match1v1)
                    Room_Code = "1v1";
                if (op_room_Code == (short)OP_Room_Code.Match2v2)
                    Room_Code = "2v2";
                if (op_room_Code == (short)OP_Room_Code.Match2op)
                    Room_Code = "2op";
                if (op_room_Code == (short)OP_Room_Code.Match4op)
                    Room_Code = "4op";
                room_option.CustomOptions.Add("op_room_Code", Room_Code);
                room_option.CustomOptions.Add("User_ID", User_ID);
                room_option.CustomOptions.Add("EXP", exp);
                room_option.CustomOptions.Add("Gold", gold);
                room_option.CustomOptions.Add("Diamond", diamond);
                room_option.CustomOptions.Add("Soul", soul);
                room_option.CustomOptions.Add("Combine_Point", combine_point);
                room_option.CustomOptions.Add("High_Damage", high_damage);
                room_option.CustomOptions.Add("Highest_HP_Enemy", highest_hp_enemy);
                room_option.CustomOptions.Add("Killed_Enemy", killed_enemy);
                room_option.CustomOptions.Add("Wave_Number", wave_number);
                return room_option;
            }

            void Send_Reward_To_MST(RoomOptions Room_Option, short player_number)
            {
                Mst.Client.Connection.SendMessage((short)MstMessageCodes.End_Game_Reward, Room_Option, (status, response) =>
                {
                    if (status == ResponseStatus.Success)
                    {
                        if (player_number == 1)
                        {
                            if (Player1)
                                Player1.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                            Player1 = null;
                        }

                        if (player_number == 2)
                        {
                            if (Player2)
                                Player2.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                            Player2 = null;
                        }

                        if (player_number == 3)
                        {
                            End_Game_Send_Finish_Player3 = true;
                            if (Player3)
                                Player3.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                            Player3 = null;
                        }

                        if (player_number == 4)
                        {
                            End_Game_Send_Finish_Player4 = true;
                            if (Player4)
                                Player4.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                            Player4 = null;
                        }

                        MstTimer.WaitForSeconds(1f, () =>
                        {
                            //Stop_Room();
                        });
                    }

                    if (End_Game_Send_Finish_Player1 && End_Game_Send_Finish_Player2 && End_Game_Send_Finish_Player3 &&
                    End_Game_Send_Finish_Player4)
                    {
                        //if (Player1)
                        //    Player1.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                        //if (Player2)
                        //    Player2.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                        //if (Player3)
                        //    Player3.GetComponent<Player_Network>().Server_Tell_Client_End_Game();
                        //if (Player4)
                        //    Player4.GetComponent<Player_Network>().Server_Tell_Client_End_Game();

                        //MstTimer.WaitForSeconds(3f, () =>
                        //{
                        //    NetworkServer.Shutdown();
                        //});
                    }
                });
            }

            void Stop_Room()
            {
                NetworkManager.singleton.StopClient();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif !UNITY_EDITOR && !UNITY_WEBGL
            Application.Quit();
#elif !UNITY_EDITOR && UNITY_WEBGL
            MsfAlert(webGlQuitMessage);
            Logs.Info(webGlQuitMessage);
#endif

            }
        }
    }

    public void Core_Damage(short player_number, int Enemy_HP)
    {
        switch (player_number)
        {
            case (1):
                Player_01_Core_HP -= Enemy_HP;
                Player_01_Core.GetComponent<Core>().Core_HP = Player_01_Core_HP;
                break;
            case (2):
                Player_02_Core_HP -= Enemy_HP;
                Player_02_Core.GetComponent<Core>().Core_HP = Player_02_Core_HP;
                break;
            case (3):
                Player_03_Core_HP -= Enemy_HP;
                Player_03_Core.GetComponent<Core>().Core_HP = Player_03_Core_HP;
                break;
            case (4):
                Player_04_Core_HP -= Enemy_HP;
                Player_04_Core.GetComponent<Core>().Core_HP = Player_04_Core_HP;
                break;
        }
        Send_Core_Damage_To_Player(Enemy_HP);
        Send_Core_HP_To_Player();
    }

    void Send_Core_Damage_To_Player(int Enemy_HP)
    {

        if (Player_1_in_Game && Player1)
            Player1.GetComponent<Player_Network>().Core_Damage_Tag(1, Enemy_HP);

        if (Player_2_in_Game && Player2)
            Player2.GetComponent<Player_Network>().Core_Damage_Tag(2, Enemy_HP);

        if (Player_3_in_Game && Player3)
            Player3.GetComponent<Player_Network>().Core_Damage_Tag(3, Enemy_HP);

        if (Player_4_in_Game && Player4)
            Player4.GetComponent<Player_Network>().Core_Damage_Tag(4, Enemy_HP);
    }

    void Send_Core_HP_To_Player()
    {
        if (Player_1_in_Game)
        {
            Player_01_Core_HP = (int)Player_01_Core.GetComponent<Core>().Core_HP;
            if (!Player1)
                Player1 = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(1);
            if (Player1)
                Player1.GetComponent<Player_Network>().Send_2Player_Core_HP_To_Local(Player_01_Core_HP, Player_02_Core_HP);
        }

        if (Player_2_in_Game)
        {
            Player_02_Core_HP = (int)Player_02_Core.GetComponent<Core>().Core_HP;
            if (!Player2)
                Player2 = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(2);
            if (Player2)
                Player2.GetComponent<Player_Network>().Send_2Player_Core_HP_To_Local(Player_01_Core_HP, Player_02_Core_HP);
        }

        if (Player_3_in_Game)
        {
            Player_03_Core_HP = (int)Player_03_Core.GetComponent<Core>().Core_HP;
            if (!Player3)
                Player3 = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(3);
            if (Player3)
                Player3.GetComponent<Player_Network>().Send_4Player_Core_HP_To_Local(Player_01_Core_HP, Player_02_Core_HP, Player_03_Core_HP, Player_04_Core_HP);
        }

        if (Player_4_in_Game)
        {
            Player_04_Core_HP = (int)Player_04_Core.GetComponent<Core>().Core_HP;
            if (!Player4)
                Player4 = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(4);
            if (Player4)
                Player4.GetComponent<Player_Network>().Send_4Player_Core_HP_To_Local(Player_01_Core_HP, Player_02_Core_HP, Player_03_Core_HP, Player_04_Core_HP);
        }
    }

    public short GM_Find_Opponent_Number(short player_number)
    {
        return (short)Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(player_number);
    }

    #region about Enemy || Protector
    void Spawn_Enemy(int Type)
    {
        Spwan_Enemy_Protector_Add_Gold(1);
        Spwan_Enemy_Protector_Add_Gold(2);
        Spwan_Enemy_Protector_Add_Gold(3);
        Spwan_Enemy_Protector_Add_Gold(4);

        int Speed = 1;

        Map_Controller Map_Controller_Player_1 = null, Map_Controller_Player_2 = null, Map_Controller_Player_3 = null,
            Map_Controller_Player_4 = null;

        if (op_room_Code == (short)OP_Room_Code.Match1v1)
        {
            int Opponent_Number_Player_1 = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(1);
            Map_Controller_Player_1 = Get_Map_Controller_By_Number(Opponent_Number_Player_1);

            int Opponent_Number_Player_2 = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(2);
            Map_Controller_Player_2 = Get_Map_Controller_By_Number(Opponent_Number_Player_2);

            int Opponent_Number_Player_3 = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(3);
            Map_Controller_Player_3 = Get_Map_Controller_By_Number(Opponent_Number_Player_3);

            int Opponent_Number_Player_4 = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(4);
            Map_Controller_Player_4 = Get_Map_Controller_By_Number(Opponent_Number_Player_4);
        }

        if (op_room_Code == (short)OP_Room_Code.Match2op)
        {
            Map_Controller_Player_1 = Get_Map_Controller_By_Number(1);
            Map_Controller_Player_2 = Get_Map_Controller_By_Number(2);
        }

        int Player_QTY = 0;

        if (op_room_Code == (short)OP_Room_Code.Match1v1)
            Player_QTY = 2;
        if (op_room_Code == (short)OP_Room_Code.Match2v2)
            Player_QTY = 4;
        if (op_room_Code == (short)OP_Room_Code.Match2op)
            Player_QTY = 4;
        if (op_room_Code == (short)OP_Room_Code.Match4op)
            Player_QTY = 4;

        Enemy_Number++;
        //Enemy.
        GameObject Player_01_Enemy = null;
        GameObject Player_02_Enemy = null;
        GameObject Player_03_Enemy = null;
        GameObject Player_04_Enemy = null;
        bool player_01_enemy_get_from_pool = false;
        bool player_02_enemy_get_from_pool = false;
        bool player_03_enemy_get_from_pool = false;
        bool player_04_enemy_get_from_pool = false;
        GameObject Player_Spwan_Point = null;

        if (Player_QTY >= 1)
        {
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
                Player_Spwan_Point = Map_Controller_Player_1.Get_1V1_Gate_Point();

            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Player_Spwan_Point = Map_Controller_Player_1.Get_Enemy_Gate_Point(1);

            if (Enemy.Count != 0)
            {
                Player_01_Enemy = Get_Enemy_Form_Pool();
                player_01_enemy_get_from_pool = true;
                Pool_Active_Game_Object(Player_01_Enemy, Player_Spwan_Point.transform.position);
            }
            Player_01_Enemy = Spwan_Enemy(Player_01_Enemy, Player_Spwan_Point, player_01_enemy_get_from_pool, Enemy_01);
            Player_01_Enemy.GetComponent<Enemy>().Set_Enemy(gameObject, true, false, false, "Player01_Enemy " + Enemy_Number, "Enemy_01", 1,
    Spwaner_01_HP, Spwaner_01_DMG, Speed, Spwaner_01_GOLD, Type, "Spawn_Enemy"); // enemy , protector , attacker
        }
        if (Player_QTY >= 2)
        {
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
                Player_Spwan_Point = Map_Controller_Player_2.Get_1V1_Gate_Point();

            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Player_Spwan_Point = Map_Controller_Player_2.Get_Enemy_Gate_Point(2);

            if (Enemy.Count != 0)
            {
                Player_02_Enemy = Get_Enemy_Form_Pool();
                player_02_enemy_get_from_pool = true;
                Pool_Active_Game_Object(Player_02_Enemy, Player_Spwan_Point.transform.position);
            }
            Player_02_Enemy = Spwan_Enemy(Player_02_Enemy, Player_Spwan_Point, player_02_enemy_get_from_pool, Enemy_01);
            Player_02_Enemy.GetComponent<Enemy>().Set_Enemy(gameObject, true, false, false, "Player02_Enemy " + Enemy_Number, "Enemy_02", 2,
    Spwaner_02_HP, Spwaner_02_DMG, Speed, Spwaner_02_GOLD, Type, "Spawn_Enemy"); // enemy , protector , attacker
        }
        if (Player_QTY >= 3)
        {
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
                Player_Spwan_Point = Map_Controller_Player_3.Get_1V1_Gate_Point();

            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Player_Spwan_Point = Map_Controller_Player_3.Get_Enemy_Gate_Point(2);

            if (Enemy.Count != 0)
            {
                Player_03_Enemy = Get_Enemy_Form_Pool();
                player_03_enemy_get_from_pool = true;
                Pool_Active_Game_Object(Player_03_Enemy, Player_Spwan_Point.transform.position);
            }
            Player_03_Enemy = Spwan_Enemy(Player_03_Enemy, Player_Spwan_Point, player_03_enemy_get_from_pool, Enemy_01);
            Player_03_Enemy.GetComponent<Enemy>().Set_Enemy(gameObject, true, false, false, "Player03_Enemy " + Enemy_Number, "Enemy_03", 3,
                Spwaner_03_HP, Spwaner_03_DMG, Speed, Spwaner_03_GOLD, Type, "Spawn_Enemy"); // enemy , protector , attacker
        }
        if (Player_QTY >= 4)
        {
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
                Player_Spwan_Point = Map_Controller_Player_4.Get_1V1_Gate_Point();

            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Player_Spwan_Point = Map_Controller_Player_4.Get_Enemy_Gate_Point(2);

            if (Enemy.Count != 0)
            {
                Player_04_Enemy = Get_Enemy_Form_Pool();
                player_04_enemy_get_from_pool = true;
                Pool_Active_Game_Object(Player_04_Enemy, Player_Spwan_Point.transform.position);
            }
            Player_04_Enemy = Spwan_Enemy(Player_04_Enemy, Player_Spwan_Point, player_04_enemy_get_from_pool, Enemy_01);
            Player_04_Enemy.GetComponent<Enemy>().Set_Enemy(gameObject, true, false, false, "Player04_Enemy " + Enemy_Number, "Enemy_04", 4,
                Spwaner_04_HP, Spwaner_04_DMG, Speed, Spwaner_04_GOLD, Type, "Spawn_Enemy"); // enemy , protector , attacker
        }

        GameObject Spwan_Enemy(GameObject Enemy, GameObject Spwan_Point, bool get_from_pool, GameObject Spwan_Enemy_Obj)
        {
            if (!get_from_pool)
            {
                Enemy = Instantiate(Spwan_Enemy_Obj, Spwan_Point.transform.position, Quaternion.identity);
                Debug.Log("Spwan_Enemy || " + Enemy);
                NetworkServer.Spawn(Enemy);
            }

            Enemy.GetComponent<Enemy>().Original_Enemy = true;
            short Path_Code = (short)Enemy_Code.Normal_Walk;
            short Next_Point_Code = (short)Enemy_Code.Next_Point;
            GameObject Player_Next_Point_01 = Get_Point_From_Object_Mount(Spwan_Point, "Enemy", Next_Point_Code, 1, Path_Code);
            GameObject Player_Next_Point_02 = Get_Point_From_Object_Mount(Spwan_Point, "Enemy", Next_Point_Code, 2, Path_Code);
            GameObject Next_Point = Get_Point(Player_Next_Point_01, Player_Next_Point_02);

            Enemy.GetComponent<Enemy>().GM = gameObject;
            Enemy.GetComponent<Enemy>().Move_Target_Point = Next_Point;
            Enemy.GetComponent<Enemy>().Previous_Point = Spwan_Point;
            Enemy.GetComponent<Enemy>().loc_spwan = Spwan_Point;
            Enemy.GetComponent<Enemy>().Set_Test_Point_Name(Spwan_Point.gameObject.name);
            Enemy.GetComponent<Enemy>().OP_Path_Code = (short)Enemy_Code.Normal_Walk;

            Enemy.GetComponent<Enemy>()._enemy = true;

            if (get_from_pool)
            {
                Enemy.GetComponent<Enemy>().Server_Prepare_Target_Set_Run(true, Spwan_Point.transform.position, Next_Point.transform.position, 16);
            }
            return Enemy;
        }
    }

    public GameObject Get_Point_From_Object_Mount(GameObject Current_Point, string Type, short Enemy_Code_Point, int number,
        short Enemy_Path_Code)
    {
        GameObject Point = null;
        if (Current_Point == null)
            return null;

        Object_Mount mount = Current_Point.GetComponent<Object_Mount>();
        if (mount == null)
            return null;

        if (op_room_Code == (short)OP_Room_Code.Match1v1)
        {
            if (Type == "Enemy" && Enemy_Code_Point == (short)Enemy_Code.Previous_Point && number == 1)
                if (mount.Obj_1V1_Enemy_Previous_Point_01 != null)
                    return mount.Obj_1V1_Enemy_Previous_Point_01;

            if (Type == "Enemy" && Enemy_Code_Point == (short)Enemy_Code.Previous_Point && number == 2)
                if (mount.Obj_1V1_Enemy_Previous_Point_02 != null)
                    return mount.Obj_1V1_Enemy_Previous_Point_02;

            if (Type == "Enemy" && Enemy_Code_Point == (short)Enemy_Code.Next_Point && number == 1)
                if (mount.Obj_1V1_Enemy_Next_Point_01 != null)
                    return mount.Obj_1V1_Enemy_Next_Point_01;

            if (Type == "Enemy" && Enemy_Code_Point == (short)Enemy_Code.Next_Point && number == 2)
                if (mount.Obj_1V1_Enemy_Next_Point_02 != null)
                    return mount.Obj_1V1_Enemy_Next_Point_02;

            if (Type == "Protector" && Enemy_Code_Point == (short)Enemy_Code.Previous_Point && number == 1)
                if (mount.Obj_1V1_Protector_Previous_Point != null)
                    return mount.Obj_1V1_Protector_Previous_Point;

            if (Type == "Protector" && Enemy_Code_Point == (short)Enemy_Code.Next_Point && number == 2)
                if (mount.Obj_1V1_Protector_Next_Point != null)
                    return mount.Obj_1V1_Protector_Next_Point;

            if (Type == "Attacker" && Enemy_Code_Point == (short)Enemy_Code.Previous_Point && number == 1)
                if (mount.Obj_1V1_Attacker_Previous_Point_01 != null)
                    return mount.Obj_1V1_Attacker_Previous_Point_01;

            if (Type == "Attacker" && Enemy_Code_Point == (short)Enemy_Code.Previous_Point && number == 2)
                if (mount.Obj_1V1_Attacker_Previous_Point_02 != null)
                    return mount.Obj_1V1_Attacker_Previous_Point_02;

            if (Type == "Attacker" && Enemy_Code_Point == (short)Enemy_Code.Next_Point && number == 1)
                if (mount.Obj_1V1_Attacker_Next_Point_01 != null)
                    return mount.Obj_1V1_Attacker_Next_Point_01;

            if (Type == "Attacker" && Enemy_Code_Point == (short)Enemy_Code.Next_Point && number == 2)
                if (mount.Obj_1V1_Attacker_Next_Point_02 != null)
                    return mount.Obj_1V1_Attacker_Next_Point_02;
            return Point;
        }

        if (op_room_Code == (short)OP_Room_Code.Match2op)
        {
            if (Type == "Enemy")
            {
                if (number == 1 && Enemy_Code_Point == (short)Enemy_Code.Previous_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Enemy_Previous_Point_01;

                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Enemy_Around_Map_Previous_Point_01;
                }
                if (number == 1 && Enemy_Code_Point == (short)Enemy_Code.Next_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Enemy_Next_Point_01;
                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Enemy_Around_Map_Next_Point_01;
                }
                if (number == 2 && Enemy_Code_Point == (short)Enemy_Code.Previous_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Enemy_Previous_Point_02;

                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Enemy_Around_Map_Previous_Point_02;
                }
                if (number == 2 && Enemy_Code_Point == (short)Enemy_Code.Next_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Enemy_Next_Point_02;
                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Enemy_Around_Map_Next_Point_02;
                }
            }
            if (Type == "Attacker" || Type == "Protector")
            {
                if (number == 1 && Enemy_Code_Point == (short)Enemy_Code.Previous_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Attacker_Previous_Point_01;

                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Attacker_Around_Map_Previous_Point_01;
                }
                if (number == 1 && Enemy_Code_Point == (short)Enemy_Code.Next_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Attacker_Next_Point_01;
                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Attacker_Around_Map_Next_Point_01;
                }
                if (number == 2 && Enemy_Code_Point == (short)Enemy_Code.Previous_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Attacker_Previous_Point_02;

                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Attacker_Around_Map_Previous_Point_02;
                }
                if (number == 2 && Enemy_Code_Point == (short)Enemy_Code.Next_Point)
                {
                    if (Enemy_Path_Code == (short)Enemy_Code.Normal_Walk)
                        return mount.Obj_2OP_Attacker_Next_Point_02;
                    if (Enemy_Path_Code == (short)Enemy_Code.Around_Map_Walk)
                        return mount.Obj_2OP_Attacker_Around_Map_Next_Point_02;
                }
            }
        }
        return Point;
    }

    public GameObject Get_Point(GameObject Point_1, GameObject Point_2)
    {
        GameObject m_next_Point = null;
        if (Point_1 != null && Point_2 != null)
        {
            int Random_Number = Random.Range(1, 3);
            if (Random_Number == 1)
                m_next_Point = Point_1;
            if (Random_Number == 2)
                m_next_Point = Point_2;
        }
        if (Point_1 != null && Point_2 == null)
            m_next_Point = Point_1;
        if (Point_1 == null && Point_2 != null)
            m_next_Point = Point_2;

        return m_next_Point;
    }

    int Spwan_Enemy_Protector_Add_Gold(int Player_Number)
    {
        int Gold = 0;
        if (Player_Number == 1)
        {
            Spwaner_01_DMG *= 1.013f;
            Spwan_Number_01++;
            Spwaner_01_GOLD++;
            Gold = Spwaner_01_GOLD;
            Player_Spwan_Number_1++;
        }
        if (Player_Number == 2)
        {
            Spwaner_02_DMG *= 1.013f;
            Spwan_Number_02++;
            Spwaner_02_GOLD++;
            Gold = Spwaner_02_GOLD;
            Player_Spwan_Number_2++;
        }
        if (Player_Number == 3)
        {
            Spwaner_03_DMG *= 1.013f;
            Spwan_Number_03++;
            Spwaner_03_GOLD++;
            Gold = Spwaner_03_GOLD;
            Player_Spwan_Number_3++;
        }
        if (Player_Number == 4)
        {
            Spwaner_04_DMG *= 1.013f;
            Spwan_Number_04++;
            Spwaner_04_GOLD++;
            Gold = Spwaner_04_GOLD;
            Player_Spwan_Number_4++;
        }
        //Debug.Log("Player_Number || " + Player_Number + " || Gold || " + Gold);
        return Gold;
    }

    //(bool Normal_or_Fall_Protector, int Player_Number, float Portector_HP, float Protector_DMG, float Protector_SPD, Vector3 POS, 
    //int Type, GameObject Tower)
    public void Spwan_Protector(Spwan_Enemy_Packet packet)
    {
        short Type_Code = packet.Type_Code;
        short Spwan_Code = packet.Spawn_Code;
        float Protector_HP = packet.HP;
        short Player_Number = packet.Player_Number;
        float Protector_DMG = packet.Damage;
        float Protector_SPD = packet.Speed;
        GameObject Tower = packet.Tower;
        Vector3 POS = packet.POS;

        if (Protector_HP < 100)
            Protector_HP = 100;

        int Protector_GOLD = Spwan_Enemy_Protector_Add_Gold(Player_Number);
        Protector_Number++;
        if (Player_Number == 1 && Player1_Protector_Number >= 12)
            return;

        if (Player_Number == 2 && Player2_Protector_Number >= 12)
            return;

        if (Player_Number == 3 && Player3_Protector_Number >= 12)
            return;

        if (Player_Number == 4 && Player4_Protector_Number >= 12)
            return;

        string Object_Name = null;
        string Object_Tag = null;
        //int Opponent_Number = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(Player_Number);
        Map_Controller Map_Controller = Get_Map_Controller_By_Number(Player_Number);

        if (Player_Number == 1)
        {
            Object_Name = "Player01_Protector || " + Protector_Number;
            Object_Tag = "Player01_Protector";
            Player1_Protector_Number++;
        }
        if (Player_Number == 2)
        {
            Object_Name = "Player02_Protector || " + Protector_Number;
            Object_Tag = "Player02_Protector";
            Player2_Protector_Number++;
        }

        if (Player_Number == 3)
        {
            Object_Name = "Player03_Protector || " + Protector_Number;
            Object_Tag = "Player03_Protector";
            Player3_Protector_Number++;
        }

        if (Player_Number == 4)
        {
            Object_Name = "Player04_Protector || " + Protector_Number;
            Object_Tag = "Player04_Protector";
            Player4_Protector_Number++;
        }

        int Player_Spawn_Number = 0;
        Vector3 pos = Vector3.zero;
        GameObject Player_Spwan_Point = null;
        if (Spwan_Code == (short)Enemy_Code.Normal_Spwan) // Normal Protector
        {
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
            {
                short Protector_Spwan_Point_QTY = Map_Controller.Get_Map_Controller_Protector_Spwan_Point_QTY();
                Player_Spawn_Number = Get_Random_Number(Protector_Spwan_Point_QTY);
                Player_Spwan_Point = Map_Controller.Get_Protector_Spawn_Point(Player_Spawn_Number);
            }

            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Player_Spwan_Point = Map_Controller.Get_Protector_Spawn_Point(Player_Number);

            pos = Player_Spwan_Point.transform.position;
        }

        if (Spwan_Code == (short)Enemy_Code.Fall_Spwan) // Fall Protector
            pos = POS;

        GameObject Player_Protector = null;
        bool player_protector_enemy_get_from_pool = false;

        if (Enemy.Count != 0)
        {
            Player_Protector = Get_Enemy_Form_Pool();
            player_protector_enemy_get_from_pool = true;
            Pool_Active_Game_Object(Player_Protector, pos);
        }

        if (!player_protector_enemy_get_from_pool)
        {
            Player_Protector = Instantiate(Enemy_01, pos, Quaternion.identity);
            NetworkServer.Spawn(Player_Protector);
        }

        GameObject Move_Point = null;

        if (Spwan_Code == (short)Enemy_Code.Normal_Spwan) // Normal Protector
        {
            short Walk_Code = (short)Enemy_Code.Normal_Walk;
            if (op_room_Code == (short)OP_Room_Code.Match1v1)
            {
                Move_Point = Player_Spwan_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point;
                Player_Protector.GetComponent<Enemy>().OP_Path_Code = Walk_Code;
            }
            if (op_room_Code == (short)OP_Room_Code.Match2op)
            {
                Get_Point_From_Object_Mount(Player_Spwan_Point, "Protector", (short)Enemy_Code.Next_Point, Player_Number, Walk_Code);
            }
        }


        if (Spwan_Code == (short)Enemy_Code.Fall_Spwan) // Fall Protector
        {
            GameObject[] Protector_Point = Get_Nearest_Object_By_Tag(Player_Protector, "Attacker_Point");
            Move_Point = Calculate_Nearest_Point(Protector_Point, Player_Protector, false); // Attacker_or_Protector ,false = protector
        }

        Player_Protector.GetComponent<Enemy>().GM = gameObject;
        Player_Protector.GetComponent<Enemy>().Move_Target_Point = Move_Point;
        Player_Protector.GetComponent<Enemy>().Previous_Point = Player_Spwan_Point;
        Player_Protector.GetComponent<Enemy>().loc_spwan = Player_Spwan_Point;
        Player_Protector.GetComponent<Enemy>()._protector = true;
        Player_Protector.GetComponent<Enemy>().Set_Test_Point_Name(Player_Spwan_Point.gameObject.name);

        if (player_protector_enemy_get_from_pool)
        {
            Player_Protector.GetComponent<Enemy>().Server_Prepare_Target_Set_Run(true, pos, Move_Point.transform.position, 17);
        }

        if (Type_Code == (short)Enemy_Code.Devil)
        {
            int Combine_Point = Tower.GetComponent<Tower>().Combine_Up_Point;
            int Tower_Level = Tower.GetComponent<Tower>().Level;
            Player_Protector.GetComponent<Enemy>().Set_Rate(Combine_Point + Tower_Level);
        }

        int Type = Tower.GetComponent<Tower>().Enemy_Code_To_Number_Type(Type_Code);
        //GameObject gamemaster, bool enemy, bool protector, bool attacker, string Enemy_Name_For_Client, string Tag, int player_Enemy,
        //float hp, float Damage, float Speed, int Gold
        Player_Protector.GetComponent<Enemy>().Set_Enemy(gameObject, false, true, false, Object_Name, Object_Tag,
            Player_Number, Protector_HP, Protector_DMG, Protector_SPD, Protector_GOLD, Type, "Spwan_Protector");
    }

    public void Spwan_Attacker(Spwan_Enemy_Packet packet)
    //(bool Normal_or_Fall_Attacker, int Player_Number, float Attacker_HP, float Attacker_DMG,
    //float Attacker_SPD, Vector3 POS, int Type, GameObject Tower)
    {
        short Type_Code = packet.Type_Code;
        short Spwan_Code = packet.Spawn_Code;
        short Player_Number = packet.Player_Number;
        float Attacker_HP = packet.HP;
        float Attacker_DMG = packet.Damage;
        float Attacker_SPD = packet.Speed;
        GameObject Tower = packet.Tower;
        Vector3 POS = packet.POS;

        if (Type_Code == 0)
            Debug.LogWarning("Type || " + Type_Code + " || Name || " + gameObject.name + " || Tag || " + gameObject.tag);
        int Attacker_GOLD = 0;
        string Object_Name = null;
        string Object_Tag = null;
        Attacker_Number++;

        //int Opponent_Number = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(Player_Number);
        Map_Controller Map_Controller = Get_Map_Controller_By_Number(Player_Number);

        if (Player_Number == 1)
        {
            Object_Name = "Player01_Attacker || " + Attacker_Number;
            Object_Tag = "Player01_Attacker";
        }
        if (Player_Number == 2)
        {
            Object_Name = "Player02_Attacker || " + Attacker_Number;
            Object_Tag = "Player02_Attacker";
        }

        if (Player_Number == 3)
        {
            Object_Name = "Player03_Attacker || " + Attacker_Number;
            Object_Tag = "Player03_Attacker";
        }

        if (Player_Number == 4)
        {
            Object_Name = "Player04_Attacker || " + Attacker_Number;
            Object_Tag = "Player04_Attacker";
        }

        int Player_Spawn_Number = 0;
        Vector3 pos = Vector3.zero;
        GameObject Player_Spwan_Point = null;
        if (Spwan_Code == (short)Enemy_Code.Normal_Spwan) // Normal Attacker
        {
            short Protector_Spwan_Point_QTY = Map_Controller.Get_Map_Controller_Protector_Spwan_Point_QTY();
            Player_Spawn_Number = Get_Random_Number(Protector_Spwan_Point_QTY);
            Player_Spwan_Point = Map_Controller.Get_Protector_Spawn_Point(Player_Spawn_Number);
            pos = Player_Spwan_Point.transform.position;
        }

        if (Spwan_Code == (short)Enemy_Code.Fall_Spwan) // Fall Attacker
            pos = POS;

        GameObject Player_Attacker = null;
        bool player_Attacker_enemy_get_from_pool = false;

        if (Enemy.Count != 0)
        {
            Player_Attacker = Get_Enemy_Form_Pool();
            player_Attacker_enemy_get_from_pool = true;
            Pool_Active_Game_Object(Player_Attacker, pos);
        }

        if (!player_Attacker_enemy_get_from_pool)
        {
            Player_Attacker = Instantiate(Enemy_01, pos, Quaternion.identity);
            NetworkServer.Spawn(Player_Attacker);
        }

        GameObject Move_Point = null;

        if (Spwan_Code == (short)Enemy_Code.Normal_Spwan) // Normal Attacker
        {
            Move_Point = Player_Spwan_Point.GetComponent<Object_Mount>().Obj_1V1_Attacker_Next_Point_01;
            Player_Attacker.GetComponent<Enemy>().OP_Path_Code = (short)Enemy_Code.Normal_Walk;
        }

        if (Spwan_Code == (short)Enemy_Code.Fall_Spwan) // Fall Attacker
        {
            GameObject[] Attacker_Point = Get_Nearest_Object_By_Tag(Player_Attacker, "Attacker_Point");
            Move_Point = Calculate_Nearest_Point(Attacker_Point, Player_Attacker, true); // Attacker_or_Protector ,true = Attacker
        }

        Player_Attacker.GetComponent<Enemy>().GM = gameObject;
        Player_Attacker.GetComponent<Enemy>().Move_Target_Point = Move_Point;
        Player_Attacker.GetComponent<Enemy>().Previous_Point = Player_Spwan_Point;
        Player_Attacker.GetComponent<Enemy>().loc_spwan = Move_Point;
        Player_Attacker.GetComponent<Enemy>()._attacker = true;
        Player_Attacker.GetComponent<Enemy>().Set_Test_Point_Name(Move_Point.gameObject.name);

        if (player_Attacker_enemy_get_from_pool)
            Player_Attacker.GetComponent<Enemy>().Server_Prepare_Target_Set_Run(true, pos, Move_Point.transform.position, 18);

        int Type = Tower.GetComponent<Tower>().Enemy_Code_To_Number_Type(Type_Code);

        //GameObject gamemaster, bool enemy, bool protector, bool attacker, string Enemy_Name_For_Client, string Tag, int player_Enemy,
        //float hp, float Damage, float Speed, int Gold
        Player_Attacker.GetComponent<Enemy>().Set_Enemy(gameObject, false, false, true, Object_Name, Object_Tag,
            Player_Number, Attacker_HP, Attacker_DMG, Attacker_SPD, Attacker_GOLD, Type, "Spwan_Attacker");

        if (Type == 3) // Thief
        {
            Player_Attacker.GetComponent<Enemy>().Thief_Time = Tower.GetComponent<Tower>().Special_Effect_Time;
            Player_Attacker.GetComponent<Enemy>().Thief_Gold_Rate = Tower.GetComponent<Tower>().Special_Rate_1;
            Player_Attacker.GetComponent<Enemy>().Thief_Steal_Rate = Tower.GetComponent<Tower>().Special_Rate_2;
            Player_Attacker.GetComponent<Enemy>().Thief_Steal_QTY = Tower.GetComponent<Tower>().Special_Rate_1;
        }

        if (Type == 4)
        {
            Player_Attacker.GetComponent<Enemy>().Original_Enemy = false;
            Player_Attacker.GetComponent<Enemy>().Boomer = true;
        }

        if (Type == 5)
        {
            int Combine_Point = Tower.GetComponent<Tower>().Combine_Up_Point;
            int Tower_Level = Tower.GetComponent<Tower>().Level;
            Player_Attacker.GetComponent<Enemy>().Set_Rate(Combine_Point + Tower_Level);
        }
    }

    int Get_Random_Number(int Max_Number)
    {
        int number = 0;
        number = Random.Range(1, Max_Number + 1);
        return number;
    }

    void Protector_Control(int Protector_Number)
    {
        string Protector_Tag = "null";
        if (Protector_Number == 1)
            Protector_Tag = "Player01_Protector";
        if (Protector_Number == 2)
            Protector_Tag = "Player02_Protector";
        GameObject Temp = null;
        GameObject[] Protector_01 = GameObject.FindGameObjectsWithTag(Protector_Tag);

        foreach (GameObject Protector in Protector_01)
        {
            bool Attacking = Protector.GetComponent<Enemy>().Attacking;
            bool Patroling = Protector.GetComponent<Enemy>().Patroling;
            if (!Attacking && Patroling)
            {
                Temp = Check_Nearest_Enemy(Protector);
                if (Temp != null)
                {
                    if (Temp.GetComponent<Enemy>().Move_Target_Point == Protector.GetComponent<Enemy>().Move_Target_Point)
                    {
                        Transform Move_Point = Protector.GetComponent<Enemy>().Move_Target_Point.transform;

                        bool Protector_Clockwise = Protector.GetComponent<Enemy>().Protector_Clockwise;
                        if (!Protector_Clockwise)
                        {
                            Protector.GetComponent<Enemy>().Move_Target_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point;
                            Protector.GetComponent<Enemy>().Protector_Clockwise = true;
                            Protector.GetComponent<Enemy>().Reset_Run();
                        }
                        if (Protector_Clockwise)
                        {
                            Protector.GetComponent<Enemy>().Move_Target_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Previous_Point;
                            Protector.GetComponent<Enemy>().Protector_Clockwise = false;
                            Protector.GetComponent<Enemy>().Reset_Run();
                        }
                        return;
                    }
                }
            }
        }

        GameObject Check_Nearest_Enemy(GameObject _Object_01)
        {
            GameObject Temp_Object = null;
            GameObject[] Protector = GameObject.FindGameObjectsWithTag(Protector_Tag);
            foreach (GameObject Target_Protector in Protector)
            {
                if (Target_Protector == _Object_01)
                {
                    break;
                }
                bool Attacking = Target_Protector.GetComponent<Enemy>().Attacking;
                if (!Attacking)
                {
                    float temp_distance = Vector3.Distance(_Object_01.transform.position, Target_Protector.transform.position);
                    if (temp_distance <= 0.5f)
                    {
                        return Target_Protector;
                    }
                }
            }
            return Temp_Object;
        }
    }

    public int Enemy_Obj_Opponent(int player_Number,
        bool Enemy_obj, bool Protector_Obj, bool Attacker_Obj, bool Con_Enemy_Obj, bool Atk_To_Enemy_Obj)
    {
        int Enemy_Opponent = Map_Manager.GetComponent<Map_Manager>().Enemy_Type_Find_Opponent(player_Number,
            Enemy_obj, Protector_Obj, Attacker_Obj, Con_Enemy_Obj, Atk_To_Enemy_Obj);
        return Enemy_Opponent;
    }

    public int Enemy_Obj_Owner(int player_Number,
        bool Enemy_obj, bool Protector_Obj, bool Attacker_Obj, bool Con_Enemy_Obj, bool Atk_To_Enemy_Obj)
    {
        int Enemy_Owner = Map_Manager.GetComponent<Map_Manager>().Enemy_Type_Find_Owner(player_Number,
            Enemy_obj, Protector_Obj, Attacker_Obj, Con_Enemy_Obj, Atk_To_Enemy_Obj);
        return Enemy_Owner;
    }

    public void Enemy_Attack_Counter(int Enemy_Owner)
    {
        GameObject tower_Controller = Object_Manager.GetComponent<Object_Manager>().Get_Tower_Controller(Enemy_Owner);
        if (tower_Controller != null)
            tower_Controller.GetComponent<Tower_Controller>().Set_Tower_Counter(3, null);// 706 Attacker_Attack_Counter_To_Bouns_Damage
    }

    public void Enemy_Dead(int player_Number, int GOLD, int MAX_HP, string Tag, GameObject Enemy)
    {
        //Debug.Log("Enemy_Dead || " + player_Number + " || " + GOLD);
        int Exp = 20;
        int[] Owner_Number = Map_Manager.GetComponent<Map_Manager>().Enemy_Number_Find_Owner_And_Parter(player_Number);

        for (int i = 0; i < Owner_Number.Length; i++)
        {
            if (Owner_Number[i] == 1)
                Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner(1, Player_01_Core, object_Manager.Get_Tower_Controller(1));

            if (Owner_Number[i] == 2)
                Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner(2, Player_02_Core, object_Manager.Get_Tower_Controller(2));

            if (Owner_Number[i] == 3)
                Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner(3, Player_03_Core, object_Manager.Get_Tower_Controller(3));

            if (Owner_Number[i] == 4)
                Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner(4, Player_04_Core, object_Manager.Get_Tower_Controller(4));
        }

        Set_Highest_HP_Enemy((short)player_Number, MAX_HP);
        Set_Killed_Enemy((short)player_Number);

        void Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner(int m_owner_Number, GameObject Core, GameObject m_tower_Controller)
        {
            //Debug.Log("Set_Gold_and_Core_Rate_Dead_Counter_To_Player_Owner || " + GOLD + " || " + m_tower_Controller + " || " + m_owner_Number);
            if (m_tower_Controller == null)
                return;
            int m_Gold = Get_Gold(m_owner_Number) + GOLD;
            float Core_Coveert_HP_Rate = Core.GetComponent<Core>().Core_Convert_Enemy_HP_To_Core_HP_Rate;
            float Core_Convert_Enemy_Bouns_HP = Core.GetComponent<Core>().Core_Convert_Enemy_Bouns_HP;
            float HP = (MAX_HP * Core_Coveert_HP_Rate) + Core_Convert_Enemy_Bouns_HP;
            Core.GetComponent<Core>().Core_Add_HP(HP);
            m_tower_Controller.GetComponent<Tower_Controller>().Set_Desk_Exp(Exp);
            Set_Gold(m_owner_Number, m_Gold);
            m_tower_Controller.GetComponent<Tower_Controller>().Set_Tower_Counter(2, Enemy);
        }
    }

    public int Get_Player_Tower_Gold(int player_Number)
    {
        int[] Player_Tower_Gold = new int[] { 0, Player_Tower_Gold_01, Player_Tower_Gold_02, Player_Tower_Gold_03, Player_Tower_Gold_04 };

        return Player_Tower_Gold[player_Number];
    }

    public void Create_Tower_Set_Gold(int player_Number)
    {
        int Gold = Get_Gold(player_Number);
        if (player_Number == 1)
        {
            Gold -= Player_Tower_Gold_01;
            Player_Tower_Gold_01 += 10;
        }
        if (player_Number == 2)
        {
            Gold -= Player_Tower_Gold_02;
            Player_Tower_Gold_02 += 10;
        }
        if (player_Number == 3)
        {
            Gold -= Player_Tower_Gold_03;
            Player_Tower_Gold_03 += 10;
        }
        if (player_Number == 4)
        {
            Gold -= Player_Tower_Gold_04;
            Player_Tower_Gold_04 += 10;
        }
        Set_Gold(player_Number, Gold);
    }

    public int Get_Gold(int player_Number)
    {
        int Gold = 0;
        switch (player_Number)
        {
            case (1):
                Gold = Player1_Gold;
                break;
            case (2):
                Gold = Player2_Gold;
                break;
            case (3):
                Gold = Player3_Gold;
                break;
            case (4):
                Gold = Player4_Gold;
                break;
        }
        return Gold;
    }

    public void Set_Gold(int player_Number, int Amount)
    {
        switch (player_Number)
        {
            case (1):
                Player1_Gold = Amount;
                Send_Gold_and_Exp_To_PlayerNetwork(1, Player1, Player1_Gold);
                break;
            case (2):
                Player2_Gold = Amount;
                Send_Gold_and_Exp_To_PlayerNetwork(2, Player2, Player2_Gold);
                break;
            case (3):
                Player3_Gold = Amount;
                Send_Gold_and_Exp_To_PlayerNetwork(3, Player3, Player3_Gold);
                break;
            case (4):
                Player4_Gold = Amount;
                Send_Gold_and_Exp_To_PlayerNetwork(4, Player4, Player4_Gold);
                break;
        }
    }

    void Send_Desk_Number_To_Local(GameObject player)
    {
        player.GetComponent<Player_Network>().Set_Desk_Number_To_Local_UI();
    }

    public void Send_Desk_Level_To_Local(GameObject player)
    {
        GameObject Tower_Controller = Get_Tower_Controller_By_Player_Obj(player);
        if (Tower_Controller != null)
            Tower_Controller.GetComponent<Tower_Controller>().Send_Tower_Level_To_Local();
    }

    public void Send_Desk_Level_To_Local_By_Player_Number(short player_Number, GameObject player_Obj)
    {
        Debug.Log("Send_Desk_Level_To_Local_By_Player_Number || " + player_Number);
        GameObject Tower_Controller = object_Manager.Get_Tower_Controller(player_Number);
        Debug.Log("Tower_Controller || " + Tower_Controller);
        if (Tower_Controller != null)
            Tower_Controller.GetComponent<Tower_Controller>().Send_Tower_Level_To_Local(player_Obj);
    }

    void Send_Gold_and_Exp_To_PlayerNetwork(int Player_Number, GameObject player, int Gold)
    {
        GameObject Tower_Controller = object_Manager.Get_Tower_Controller(Player_Number);
        if (Tower_Controller == null)
            return;
        float exp1 = Tower_Controller.GetComponent<Tower_Controller>().Desk_01_Exp;
        float exp2 = Tower_Controller.GetComponent<Tower_Controller>().Desk_02_Exp;
        float exp3 = Tower_Controller.GetComponent<Tower_Controller>().Desk_03_Exp;
        float exp4 = Tower_Controller.GetComponent<Tower_Controller>().Desk_04_Exp;
        float exp5 = Tower_Controller.GetComponent<Tower_Controller>().Desk_05_Exp;
        int Create_Tower_Gold = Get_Player_Create_Tower_Gold(Player_Number);
        if (!player)
            player = object_Manager.Get_Player_Obj(Player_Number);
        if (player != null)
            player.GetComponent<Player_Network>().Set_Player_Gold_And_Desk_Exp(Gold, Create_Tower_Gold, exp1, exp2, exp3, exp4, exp5);
    }

    void Send_Gold_To_PlayerNetwork(int Player_Number, GameObject player, int Gold)
    {
        if (!player)
            player = object_Manager.Get_Player_Obj(Player_Number);
        if (player != null)
            player.GetComponent<Player_Network>().Set_Player_Gold(Gold);
    }

    public void Attacker_Change_To_Enemy(GameObject Attacker, int player_number, float HP, float DMG, float SPD, int Type)
    {
        Attacker.GetComponent<Enemy>().Original_Enemy = false;
        Attacker_Change_To_Enemy_Number++;
        string Name_for_Client = null;
        string Tag = null;

        Set_Enemy_Tag_By_Player_Number((short)player_number, out Name_for_Client, out Tag);

        // Get Opponent Spwan Point
        int Opponent_Number_Player = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(player_number);
        Map_Controller Map_Controller_Player = Get_Map_Controller_By_Number(Opponent_Number_Player);
        int Opponent_Spawn_Number = Get_Random_Number(Map_Controller_Player.Get_Map_Controller_Enemy_Spwan_Point_QTY());
        GameObject Opponent_Spwan_Point = Map_Controller_Player.Get_Enemy_Gate_Point(Opponent_Spawn_Number);
        // Get Opponent Spwan Point

        Name_for_Client = Name_for_Client + " || " + Attacker_Change_To_Enemy_Number;
        short Enemy_Empty_Path_Code = 0;
        GameObject Enemy_Next_Point_01 = Get_Point_From_Object_Mount(Opponent_Spwan_Point, "Enemy", (short)Enemy_Code.Next_Point, 1, Enemy_Empty_Path_Code);
        GameObject Enemy_Next_Point_02 = Get_Point_From_Object_Mount(Opponent_Spwan_Point, "Enemy", (short)Enemy_Code.Next_Point, 2, Enemy_Empty_Path_Code);
        Attacker.GetComponent<Enemy>().Move_Target_Point = Get_Point(Enemy_Next_Point_01, Enemy_Next_Point_02);
        Attacker.GetComponent<Enemy>().Previous_Point = Opponent_Spwan_Point;

        Attacker.transform.position = Opponent_Spwan_Point.transform.position;
        Attacker.GetComponent<Enemy>().GM = gameObject;
        Attacker.GetComponent<Enemy>().Set_Test_Point_Name("Enemy_To_Attacker");
        Attacker.GetComponent<Enemy>().Set_Enemy(gameObject, true, false, false, Name_for_Client, Tag, player_number, HP, DMG, SPD, 0, Type, "Attacker_Change_To_Enemy");
        Attacker.GetComponent<Enemy>().Attacker_To_Enemy_Object = true;
    }

    public void Capture_Enemy(Spwan_Enemy_Packet packet)
    {
        //GameObject M = packet.Source_Obj.GetComponent<Enemy>().Move_Target_Point;
        //GameObject P = packet.Source_Obj.GetComponent<Enemy>().Previous_Point;
        //string Tag = packet.Source_Obj.tag;
        //float HP = packet.Source_Obj.GetComponent<Enemy>().HP;

        //Debug.Log("Capture_Enemy_100 || " + packet.Source_Obj.name + " || " + M + " || " + P + " || " + Tag + " || " + HP);

        StartCoroutine(Wait_GM_Captur_Enemy_To_Own(packet, 1.0f));
    }

    IEnumerator Wait_GM_Captur_Enemy_To_Own(Spwan_Enemy_Packet packet, float Time)
    {
        yield return new WaitForSeconds(Time);

        Captur_Enemy_To_Own(packet);
    }

    public void Captur_Enemy_To_Own(Spwan_Enemy_Packet packet)
    {
        GameObject Capture_Obj = packet.Source_Obj;
        GameObject Point = Capture_Obj.GetComponent<Enemy>().Move_Target_Point;
        Capture_Obj.GetComponent<Enemy>().enabled = true;
        Capture_Obj.tag = packet.Source_Obj_Tag;
        if (Point == null)
            Debug.LogWarning("Captur_Enemy_To_Own || " + Capture_Obj.name + " || " + Point + " || " + Capture_Obj.tag);
        bool enemy = false, protector = false, attacker = false;

        short player_number = packet.Player_Number;
        float HP = packet.HP;
        float DMG = packet.Damage;
        float SPD = packet.Speed;
        int Type = packet.Type_Code;
        short Attack_Protector_Code = packet.Attack_Protector_Code;

        Debug.Log("Captur_Enemy_To_Own || New_Tag || " + player_number + " || " + Capture_Obj.tag);

        string Tag = Change_Capture_To_Own_Tag(player_number, Capture_Obj.tag);
        Debug.Log("Tag || " + Tag);
        if (Tag == "Dead")
            return;

        if (Tag.Contains("Enemy"))
        {
            enemy = true;
            protector = false;
            attacker = false;
        }
        if (Tag.Contains("Protector"))
        {
            enemy = false;
            protector = true;
            attacker = false;
        }

        if (Tag.Contains("Attacker"))
        {
            enemy = false;
            protector = false;
            attacker = true;
        }

        string Name_for_Client = "Captur_Enemy";

        Capture_Obj.GetComponent<Enemy>().Original_Enemy = false;
        Attacker_Change_To_Enemy_Number++;

        Point = Capture_Obj.GetComponent<Enemy>().Move_Target_Point;
        if (Point == null)
            Debug.LogWarning("Captur_Enemy_To_Own || " + Capture_Obj.name + " || " + Point);

        float current_HP = Capture_Obj.GetComponent<Enemy>().HP;
        float max_HP = Capture_Obj.GetComponent<Enemy>().MAX_HP;
        if (current_HP < 1)
            Debug.LogWarning("current_HP || " + current_HP + " || " + max_HP + " || " + Tag + " || " + player_number);

        Capture_Obj.GetComponent<Enemy>().GM = gameObject;
        Capture_Obj.GetComponent<Enemy>().Set_Enemy(gameObject, enemy, protector, attacker, Name_for_Client, Tag, player_number,
            HP, DMG, SPD, 0, Type, "Capture_Enemy");
    }

    #endregion

    string Change_Capture_To_Own_Tag(short player_Number, string Original_Tag)
    {
        string Tag_First_Character = Original_Tag;
        short enemy_Code = 0;
        if (Original_Tag.Contains("Enemy"))
            enemy_Code = (short)Enemy_Code.Enemy;

        if (Original_Tag.Contains("Attacker") || Original_Tag.Contains("Protector"))
            enemy_Code = (short)Enemy_Code.Attacker;

        switch (player_Number)
        {
            case (1):
                switch (enemy_Code)
                {
                    case ((short)Enemy_Code.Enemy): return "Player01_Attacker";
                    case ((short)Enemy_Code.Attacker): return "Enemy_01";
                }
                break;
            case (2):
                switch (enemy_Code)
                {
                    case ((short)Enemy_Code.Enemy): return "Player02_Attacker";
                    case ((short)Enemy_Code.Attacker): return "Enemy_02";
                }
                break;
            case (3):
                switch (enemy_Code)
                {
                    case ((short)Enemy_Code.Enemy): return "Player03_Attacker";
                    case ((short)Enemy_Code.Attacker): return "Enemy_03";
                }
                break;
            case (4):
                switch (enemy_Code)
                {
                    case ((short)Enemy_Code.Enemy): return "Player04_Attacker";
                    case ((short)Enemy_Code.Attacker): return "Enemy_04";
                }
                break;
        }
        return null;
    }

    public void Set_Enemy_Tag_By_Player_Number(short player_Number, out string Name_for_Client, out string Tag)
    {
        Name_for_Client = null;
        Tag = null;
        switch (player_Number)
        {
            case (1):
                Name_for_Client = "Player01_Enemy";
                Tag = "Enemy_01";
                break;
            case (2):
                Name_for_Client = "Player02_Enemy";
                Tag = "Enemy_02";
                break;
            case (3):
                Name_for_Client = "Player03_Enemy";
                Tag = "Enemy_03";
                break;
            case (4):
                Name_for_Client = "Player04_Enemy";
                Tag = "Enemy_04";
                break;
        }

    }

    public GameObject[] Get_Nearest_Object_By_Tag(GameObject _obj, string Tag)
    {
        float Distance = Mathf.Infinity;
        GameObject[] Array = GameObject.FindGameObjectsWithTag(Tag);
        float[] distance = new float[Array.Length];
        GameObject[] temp_Array = new GameObject[Array.Length];
        int number = 0;
        foreach (GameObject temp_obj in Array)
        {
            float distanceToPoint = Vector3.Distance(_obj.transform.position, temp_obj.transform.position);
            if (distanceToPoint <= Distance)
            {
                temp_Array[number] = temp_obj;
                distance[number] = distanceToPoint;
                number++;
            }

            bool Wait_Sorting = true;
            float tempfloat = 0;
            GameObject tempobj = null;
            while (Wait_Sorting)
            {
                Wait_Sorting = false;
                for (int i = 0; i < distance.Length - 1; i++)
                {
                    if (distance[i] > distance[i + 1] && distance[i + 1] > 0)
                    {
                        tempobj = temp_Array[i + 1];
                        tempfloat = distance[i + 1];
                        temp_Array[i + 1] = temp_Array[i];
                        distance[i + 1] = distance[i];
                        temp_Array[i] = tempobj;
                        distance[i] = tempfloat;
                        Wait_Sorting = true;
                    }
                }
            }
        }
        return temp_Array;
    }

    public GameObject Calculate_Nearest_Point(GameObject[] _obj, GameObject Enemy, bool Attacker_or_Protector)
    {
        GameObject Point = _obj[0], Next_Point_01 = null, Next_Point_02 = null;
        GameObject Next_Point = null, Previous_Point = null, Previous_Point_01 = null, Previous_Point_02 = null;

        float Distance_Point_To_N_Point = 99, Distance_Point_To_P_Point = 99,
            Distance_Enemy_To_N_Point = 99, Distance_Enemy_To_P_Point = 99;

        bool End_Point = Point.GetComponent<Object_Mount>().B_1V1_Attacker_End_Point;
        if (Attacker_or_Protector && End_Point)
            return Point;

        short Enemy_Empty_Path_Code = 0;

        if (Attacker_or_Protector) // Attacker
        {
            Previous_Point_01 = Get_Point_From_Object_Mount(Point, "Attacker", (short)Enemy_Code.Previous_Point, 1, Enemy_Empty_Path_Code);
            Previous_Point_02 = Get_Point_From_Object_Mount(Point, "Attacker", (short)Enemy_Code.Previous_Point, 2, Enemy_Empty_Path_Code);

            Next_Point_01 = Get_Point_From_Object_Mount(Point, "Attacker", (short)Enemy_Code.Next_Point, 1, Enemy_Empty_Path_Code);
            Next_Point_02 = Get_Point_From_Object_Mount(Point, "Attacker", (short)Enemy_Code.Next_Point, 2, Enemy_Empty_Path_Code);
        }
        if (!Attacker_or_Protector) // Protector
        {
            Previous_Point_01 = Get_Point_From_Object_Mount(Point, "Protector", (short)Enemy_Code.Previous_Point, 1, Enemy_Empty_Path_Code);
            Next_Point_01 = Get_Point_From_Object_Mount(Point, "Protector", (short)Enemy_Code.Next_Point, 1, Enemy_Empty_Path_Code);
        }

        Previous_Point = Get_Point(Previous_Point_01, Previous_Point_02); // Random_Get 1 point
        Next_Point = Get_Point(Next_Point_01, Next_Point_02); // Random_Get 1 point

        if (Previous_Point != null)
        {
            Distance_Point_To_P_Point = Vector3.Distance(Previous_Point.transform.position, Point.transform.position);
            Distance_Enemy_To_P_Point = Vector3.Distance(Previous_Point.transform.position, Enemy.transform.position);
        }

        if (Next_Point != null)
        {
            Distance_Point_To_N_Point = Vector3.Distance(Next_Point.transform.position, Point.transform.position);
            Distance_Enemy_To_N_Point = Vector3.Distance(Next_Point.transform.position, Enemy.transform.position);
        }

        if (!Attacker_or_Protector) // Protector
        {
            if (Previous_Point != null && Distance_Point_To_P_Point > Distance_Enemy_To_P_Point)
                return Previous_Point;

            if (Next_Point != null && Distance_Point_To_N_Point > Distance_Enemy_To_N_Point)
                return Next_Point;
        }

        if (Attacker_or_Protector) // Attacker
        {
            bool Next_Point_is_Attacker_End_Point = Check_Point_Attacker_End_Point(Next_Point);
            bool Point_is_Attacker_End_Point = Check_Point_Attacker_End_Point(Point);

            if (Next_Point_is_Attacker_End_Point && Distance_Point_To_N_Point > Distance_Enemy_To_N_Point)
                return Next_Point;

            if (Point_is_Attacker_End_Point)
                return Point;

            if (Next_Point == null)
                return Point;

            if (Previous_Point == null)
                return Point;

            if (Distance_Point_To_N_Point > Distance_Enemy_To_N_Point)
                return Next_Point;

            if (Distance_Point_To_P_Point > Distance_Enemy_To_P_Point)
                return Point;

            return Point;
        }

        bool Check_Point_Attacker_End_Point(GameObject m_Point)
        {
            bool Point_is_not_end_Point = false;
            if (m_Point != null)
            {
                Point_is_not_end_Point = m_Point.GetComponent<Object_Mount>().B_1V1_Attacker_End_Point;
            }
            return Point_is_not_end_Point;
        }

        return Point;
    }

    public void Get_Attacker_Point(GameObject Player_Attacker)
    {
        GameObject Move_Point = null;
        GameObject[] Attacker_Point = Get_Nearest_Object_By_Tag(Player_Attacker, "Attacker_Point");
        Move_Point = Calculate_Nearest_Point(Attacker_Point, Player_Attacker, true); // Attacker_or_Protector ,true = protector
        Player_Attacker.GetComponent<Enemy>().Move_Target_Point = Move_Point;
    }

    public void Create_Tower(GameObject Player)
    {
        GameObject tower_Controller = Get_Tower_Controller_By_Player_Obj(Player);
        if (tower_Controller != null)
            tower_Controller.GetComponent<Tower_Controller>().Create_Tower(Player);
    }

    public void Combine_Tower(GameObject Player, GameObject Drag_Tower, GameObject Target_Tower)
    {
        if (End_Game)
            return;
        GameObject tower_Controller = Get_Tower_Controller_By_Player_Obj(Player);
        if (tower_Controller != null)
            tower_Controller.GetComponent<Tower_Controller>().Check_Combine_Tower(Drag_Tower, Target_Tower);
    }

    GameObject Get_Tower_Controller_By_Player_Obj(GameObject Player)
    {
        GameObject tower_Controller = null;
        if (Player1 == Player)
        {
            tower_Controller = Map_Controller_01.Tower_Controller;
        }
        if (Player2 == Player)
        {
            tower_Controller = Map_Controller_02.Tower_Controller;
        }
        if (Player3 == Player)
        {
            tower_Controller = Map_Controller_03.Tower_Controller;
        }
        if (Player4 == Player)
        {
            tower_Controller = Map_Controller_04.Tower_Controller;
        }
        return tower_Controller;
    }

    public GameObject Get_Tower_Controller_By_Player_Number(int Player_Number)
    {
        switch (Player_Number)
        {
            case (1): return Map_Controller_01.Tower_Controller;
            case (2): return Map_Controller_02.Tower_Controller;
            case (3): return Map_Controller_03.Tower_Controller;
            case (4): return Map_Controller_04.Tower_Controller;
        }
        return null;
    }

    public void GM_Control_Enemy_Damage(GameObject Enemy, float dmg, short Code, int C_Rate, int C_Damage, float time,
        string Enemy_Name, string who)
    {
        bool Enemy_No_Damage = Enemy.GetComponent<Enemy>().No_Damage;
        if (Enemy_No_Damage)
            return;
        StartCoroutine(Count_Time_Enemy_Damage(Enemy, dmg, Code, C_Rate, C_Damage, time, Enemy_Name, who));
    }

    IEnumerator Count_Time_Enemy_Damage(GameObject Enemy, float dmg, short Code, int C_Rate, int C_Damage, float time,
        string Enemy_Name, string who)
    {
        if (Enemy.name != Enemy_Name)
            yield break;
        yield return new WaitForSeconds(time);
        if (Enemy != null)
            Enemy.GetComponent<Enemy>().Enemy_Damage(dmg, Code, C_Rate, C_Damage, Enemy_Name, who);
    }

    public void Add_Gold(int player_Number, int Gold)
    {
        //Debug.Log("Add_Gold || " + Gold);
        switch (player_Number)
        {
            case (1):
                Player1_Gold += Gold;
                Send_Gold_To_PlayerNetwork(1, Player1, Player1_Gold);
                break;
            case (2):
                Player2_Gold += Gold;
                Send_Gold_To_PlayerNetwork(2, Player2, Player2_Gold);
                break;
            case (3):
                Player3_Gold += Gold;
                Send_Gold_To_PlayerNetwork(3, Player3, Player3_Gold);
                break;
            case (4):
                Player4_Gold += Gold;
                Send_Gold_To_PlayerNetwork(4, Player4, Player4_Gold);
                break;
        }
    }

    public void Subtract_Gold(int player_Number, int Gold)
    {
        switch (player_Number)
        {
            case (1):
                Player1_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(1, Player1, Player1_Gold);
                break;
            case (2):
                Player2_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(2, Player2, Player2_Gold);
                break;
            case (3):
                Player3_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(3, Player3, Player3_Gold);
                break;
            case (4):
                Player4_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(4, Player4, Player4_Gold);
                break;
        }
    }

    int Get_Player_Create_Tower_Gold(int player_Number)
    {
        int[] Array = new int[] { 0, Player_Tower_Gold_01, Player_Tower_Gold_02, Player_Tower_Gold_03, Player_Tower_Gold_04 };
        return Array[player_Number];
    }

    public void Steal_Gold(int player_Number, int Gold)
    {
        int Target_Steal_Player_Number = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Number(player_Number);
        switch (Target_Steal_Player_Number)
        {
            case (1):
                Player1_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(1, Player1, Player1_Gold);
                break;
            case (2):
                Player2_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(2, Player2, Player2_Gold);
                break;
            case (3):
                Player3_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(3, Player3, Player3_Gold);
                break;
            case (4):
                Player4_Gold -= Gold;
                Send_Gold_To_PlayerNetwork(4, Player4, Player4_Gold);
                break;
        }

        switch (player_Number)
        {
            case (1):
                Player1_Gold += Gold;
                Send_Gold_To_PlayerNetwork(1, Player1, Player1_Gold);
                break;
            case (2):
                Player2_Gold += Gold;
                Send_Gold_To_PlayerNetwork(2, Player2, Player2_Gold);
                break;
            case (3):
                Player3_Gold += Gold;
                Send_Gold_To_PlayerNetwork(3, Player3, Player3_Gold);
                break;
            case (4):
                Player4_Gold += Gold;
                Send_Gold_To_PlayerNetwork(4, Player4, Player4_Gold);
                break;
        }
    }

    public string[] Get_Opponent_Tag(int player_Number)
    {
        string[] Opponent_Enemy_Tag = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Tag(player_Number);
        return Opponent_Enemy_Tag;
    }

    public GameObject[] Enemy_Tag_To_Enemy_Obj(string[] Tag)
    {
        return Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(Tag);
    }


    public int[] Tag_Find_Opponent(string Enemy_Tag)
    {
        int[] Opponent_Enemy_Tag = Map_Manager.GetComponent<Map_Manager>().Tag_Find_Opponent(Enemy_Tag);
        return Opponent_Enemy_Tag;
    }

    public void Tower_Desk_Power_Up(GameObject Player, int Desk_Number)
    {
        GameObject tower_Controller = Get_Tower_Controller_By_Player_Obj(Player);
        if (tower_Controller != null)
            tower_Controller.GetComponent<Tower_Controller>().Tower_Desk_Power_Up(Player, Desk_Number);
    }

    GameObject Get_Enemy_Form_Pool()
    {
        for (int i = 0; i < Enemy.Count; i++)
        {
            if (Enemy[i] != null)
            {
                float max_hp = Enemy[i].GetComponent<Enemy>().MAX_HP;
                float hp = Enemy[i].GetComponent<Enemy>().HP;

                if (max_hp < 0 || hp < 0)
                    Debug.LogWarning("max_hp || " + max_hp + " || hp || " + hp + " || Name || " + Enemy[i].name);
            }
        }

        GameObject enemy = null;
        for (int i = 0; i < Enemy.Count; i++)
        {
            if (Enemy[i] != null)
            {
                enemy = Enemy[i];
                Enemy.Remove(enemy);
                return enemy;
            }
        }
        return enemy;
    }

    void Pool_Active_Game_Object(GameObject _obj, Vector3 POS)
    {
        _obj.transform.position = POS;
        //_obj.GetComponent<Enemy>().Server_Enemy_Set_Visible();
    }

    public void Treasure_Reward(float Treasure_Rate, GameObject Enemy, int Enemy_Number)
    {
        int Type = 0; int QTY = 0;
        int Random_Rate = Random.Range(1, 101);
        Random_Rate = 0;
        if (Random_Rate > Treasure_Rate)
            return;

        int Random_Number = Random.Range(1, 1001);
        if (Random_Number < 901) // Gold
        {
            Type = 1;
            QTY = Random_Number / 4;
            if (QTY < 100)
                QTY = 100;
        }
        if (Random_Number >= 901 && Random_Number <= 999) // Diamond
        {
            Type = 2;
            QTY = 1;
            if (Random_Number >= 986 && Random_Number <= 990)
                QTY = 2;
            if (Random_Number >= 991 && Random_Number <= 995)
                QTY = 3;
            if (Random_Number >= 996 && Random_Number <= 997)
                QTY = 4;
            if (Random_Number >= 998 && Random_Number <= 999)
                QTY = 5;
        }
        if (Random_Number == 1000) // Token_01
        {
            Type = 3;
            QTY = 1;
        }

        Enemy.GetComponent<Enemy>().Tell_Client_Local_Treasure(Type, QTY);
        int[] Owner_And_Parter = Map_Manager.GetComponent<Map_Manager>().Enemy_Number_Find_Owner_And_Parter(Enemy_Number);
        for (int i = 0; i < Owner_And_Parter.Length; i++)
        {
            int player_Number = Owner_And_Parter[i];
            if (player_Number == 0)
                return;

            if (player_Number != 0)
                Set_Reward_To_Player_Profile(player_Number, Type, QTY, true); // Add
        }
    }

    public void Set_Reward_To_Player_Profile(int Player_Number, int Type, int QTY, bool Add_or_Subtract)
    {
        Player_Status.Player player = Get_Player_Profile(Player_Number);

        if (Type == 1)
        {
            if (Add_or_Subtract) player.Gold += QTY;
            if (!Add_or_Subtract) player.Gold -= QTY;
        }

        if (Type == 2)
        {
            if (Add_or_Subtract) player.Diamond += QTY;
            if (!Add_or_Subtract) player.Diamond -= QTY;
        }

        if (Type == 3)
        {
            if (Add_or_Subtract) player.Token_01 += QTY;
            if (!Add_or_Subtract) player.Token_01 -= QTY;
        }

        if (Type == 4)
        {
            if (Add_or_Subtract) player.Token_02 += QTY;
            if (!Add_or_Subtract) player.Token_02 -= QTY;
        }

        if (Type == 5)
        {
            if (Add_or_Subtract) player.Token_03 += QTY;
            if (!Add_or_Subtract) player.Token_03 -= QTY;
        }

        if (Type == 6)
        {
            if (Add_or_Subtract) player.Token_04 += QTY;
            if (!Add_or_Subtract) player.Token_04 -= QTY;
        }

        if (Type == 7)
        {
            if (Add_or_Subtract) player.Token_05 += QTY;
            if (!Add_or_Subtract) player.Token_05 -= QTY;
        }
    }

    Player_Status.Player Get_Player_Profile(int Number)
    {
        switch (Number)
        {
            case (1): return Player1_Online_Profile;
            case (2): return Player2_Online_Profile;
            case (3): return Player3_Online_Profile;
            case (4): return Player4_Online_Profile;
        }
        return null;
    }

    GameObject Get_Map_Type(int Number)
    {
        GameObject[] array = new GameObject[] { null, Map_01, Map_02, Map_03, Map_04, Map_05, Map_06, Map_07, Map_08, Map_09, Map_10 };
        return array[Number];
    }

    Map_Controller Get_Map_Controller_By_Number(int Number)
    {
        Map_Controller[] array = new Map_Controller[] { null, Map_Controller_01, Map_Controller_02, Map_Controller_03, Map_Controller_04 };
        return array[Number];
    }

    public NetworkConnection Get_Player_Connection(int Number)
    {
        switch (Number)
        {
            case (1):
                if (Player1 == null) break;
                return Player1.GetComponent<NetworkIdentity>().connectionToClient;
            case (2):
                if (Player2 == null) break;
                return Player2.GetComponent<NetworkIdentity>().connectionToClient;

            case (3):
                if (Player3 == null) break;
                return Player3.GetComponent<NetworkIdentity>().connectionToClient;
            case (4):
                if (Player4 == null) break;
                return Player4.GetComponent<NetworkIdentity>().connectionToClient;
        }
        return null;
    }

    public short Get_Total_Player_Number_In_Match()
    {
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1): return 2;
            case ((short)OP_Room_Code.Match2op): return 2;
            case ((short)OP_Room_Code.Match2v2): return 4;
            case ((short)OP_Room_Code.Match4op): return 4;
        }
        return 0;
    }

    #region Task
    public void Set_Highest_Combine_Point(short player_Number, short combine_Up_Point)
    {
        switch (player_Number)
        {
            case (1): if (combine_Up_Point > Player1_Combine_Point) Player1_Combine_Point = combine_Up_Point; break;
            case (2): if (combine_Up_Point > Player2_Combine_Point) Player2_Combine_Point = combine_Up_Point; break;
            case (3): if (combine_Up_Point > Player3_Combine_Point) Player3_Combine_Point = combine_Up_Point; break;
            case (4): if (combine_Up_Point > Player4_Combine_Point) Player4_Combine_Point = combine_Up_Point; break;
        }
    }

    public void Set_High_Damage(short player_Number, int Player_High_Damage)
    {
        switch (player_Number)
        {
            case (1): if (Player_High_Damage > Player1_High_Damage) Player1_High_Damage = Player_High_Damage; break;
            case (2): if (Player_High_Damage > Player2_High_Damage) Player2_High_Damage = Player_High_Damage; break;
            case (3): if (Player_High_Damage > Player3_High_Damage) Player3_High_Damage = Player_High_Damage; break;
            case (4): if (Player_High_Damage > Player4_High_Damage) Player4_High_Damage = Player_High_Damage; break;
        }
    }

    public void Set_Highest_HP_Enemy(short player_Number, int Player_Highest_HP_Enemy)
    {
        switch (player_Number)
        {
            case (1): if (Player_Highest_HP_Enemy > Player1_Highest_HP_Enemy) Player1_Highest_HP_Enemy = Player_Highest_HP_Enemy; break;
            case (2): if (Player_Highest_HP_Enemy > Player2_Highest_HP_Enemy) Player2_Highest_HP_Enemy = Player_Highest_HP_Enemy; break;
            case (3): if (Player_Highest_HP_Enemy > Player3_Highest_HP_Enemy) Player3_Highest_HP_Enemy = Player_Highest_HP_Enemy; break;
            case (4): if (Player_Highest_HP_Enemy > Player4_Highest_HP_Enemy) Player4_Highest_HP_Enemy = Player_Highest_HP_Enemy; break;
        }
    }

    public void Set_Killed_Enemy(short player_Number)
    {
        switch (player_Number)
        {
            case (1): Player1_Killed_Enemy++; break;
            case (2): Player2_Killed_Enemy++; break;
            case (3): Player3_Killed_Enemy++; break;
            case (4): Player4_Killed_Enemy++; break;
        }
    }
    #endregion

    void Disable_Core()
    {
        if (Player_01_Core)
            Player_01_Core.GetComponent<Core>().enabled = false;
        if (Player_02_Core)
            Player_02_Core.GetComponent<Core>().enabled = false;
        if (Player_03_Core)
            Player_03_Core.GetComponent<Core>().enabled = false;
        if (Player_04_Core)
            Player_04_Core.GetComponent<Core>().enabled = false;
    }

    public GameObject Get_Object_From_Pool_By_Tag(string Tag)
    {
        switch (Tag)
        {
            case ("Ground_Boom"):
                GameObject Damage_Bar_Obj = Get_Pool(Ground_Boom_List);
                return Damage_Bar_Obj;
        }
        return null;

        GameObject Get_Pool(List<GameObject> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i] != null)
                {
                    GameObject obj = List[i];
                    List.Remove(obj);
                    return obj;
                }
            }
            return null;
        }
    }

    //Player_1 Enemy :
    //Portal_Point_1_Start > Around_Map_1_Start
    //Arund_Map_1_End > Portal_Point_1_End

    //Player_2 Enemy :
    //Portal_Point_2_Start > Around_Map_2_Start
    //Arund_Map_2_End > Portal_Point_2_End

    void Setup_Map_Walk_Path()
    {
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                break;
            case ((short)OP_Room_Code.Match2op):
                break;
            case ((short)OP_Room_Code.Match2v2):
                break;
            case ((short)OP_Room_Code.Match4op):
                break;
        }
    }

    public short Get_OP_Path_Code(int player_number, short Enemy_Code_Walk)
    {
        //Enemy_Code_Walk = Normal_Walk,
        //Enemy_Code_Walk = Around_Map_Walk
        short Path_Code = 0;
        if (player_number == 1)
        {
            if (Enemy_Code_Walk == (short)Enemy_Code.Normal_Walk)
                return (short)Enemy_Code.OP_Path_1;
            if (Enemy_Code_Walk == (short)Enemy_Code.Around_Map_Walk)
                return (short)Enemy_Code.OP_Path_Around_Map_1;
        }

        if (player_number == 2)
        {
            if (Enemy_Code_Walk == (short)Enemy_Code.Normal_Walk)
                return (short)Enemy_Code.OP_Path_2;
            if (Enemy_Code_Walk == (short)Enemy_Code.Around_Map_Walk)
                return (short)Enemy_Code.OP_Path_Around_Map_2;
        }

        if (player_number == 3)
        {
            if (Enemy_Code_Walk == (short)Enemy_Code.Normal_Walk)
                return (short)Enemy_Code.OP_Path_3;
            if (Enemy_Code_Walk == (short)Enemy_Code.Around_Map_Walk)
                return (short)Enemy_Code.OP_Path_Around_Map_3;
        }

        if (player_number == 4)
        {
            if (Enemy_Code_Walk == (short)Enemy_Code.Normal_Walk)
                return (short)Enemy_Code.OP_Path_4;
            if (Enemy_Code_Walk == (short)Enemy_Code.Around_Map_Walk)
                return (short)Enemy_Code.OP_Path_Around_Map_4;
        }
        return Path_Code;
    }
}

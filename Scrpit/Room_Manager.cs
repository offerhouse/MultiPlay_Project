using UnityEngine;
using Mirror;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Networking;
using MasterServerToolkit.Bridges.MirrorNetworking;
using System;
using System.Collections.Generic;
using System.Collections;

public class Room_Manager : NetworkBehaviour
{
    public int Room_ID;
    public short OP_Code;
    public GameObject Player1, Player2, Player3, Player4, Room_Server, Object_Manager;
    public string UserID_1, UserID_2, UserID_3, UserID_4;
    public bool Get_Master_Info = false;
    public bool Set_Player_Finish = false;
    public bool Room_Server_Setup_Finish = false;
    public bool Room_Manager_Setup_Finish = false;

    Player_Status.Player Profile_1, Profile_2, Profile_3, Profile_4;

    RoomServerManager room_Server;

    public int GOLD_01, GOLD_02;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Set_Room", 0.0f, 2.0f);

        if (Room_Server == null)
            Check_Room_Server();
    }

    void Check_Room_Server()
    {
        Room_Server = Find_Room_Server();
    }

    GameObject Find_Room_Server()
    {
        GameObject room_Server = GameObject.Find("-- MIRROR_ROOM_SERVER");
        Debug.Log("room_Server || " + room_Server);
        return room_Server;
    }

    void Set_Room()
    {
        Debug.Log("Set_Room");
        if (!Get_Master_Info && !Room_Server_Setup_Finish)
            Set_Profile();
        if (Get_Master_Info && !Set_Player_Finish && !Room_Server_Setup_Finish)
        {
            Search_and_Set_Player();
        }
        if (Get_Master_Info && Set_Player_Finish && !Room_Server_Setup_Finish)
            Fill_Player_Profile(); // Room_Server_Setup_Finish
        if (Get_Master_Info && Set_Player_Finish && Room_Server_Setup_Finish && !Room_Manager_Setup_Finish)
            Set_Object_Manager();
        if (Room_Manager_Setup_Finish)
            Setup_Finish();
    }

    void Set_Room_Server()
    {
        room_Server = Room_Server.GetComponent<RoomServerManager>();
    }

    void Set_Profile()
    {
        if (Room_Server != null)
        {
            if (room_Server == null)
                Set_Room_Server();

            bool Change_Scene_Finish = Room_Server.GetComponent<NetworkManager>().Load_Scene_Finish;

            if (!Change_Scene_Finish)
                return;

            if (room_Server != null)
            {
                Room_ID = -1;
                Room_ID = room_Server.Get_Room_ID();
                OP_Code = room_Server.Get_OP_Code();
                OP_Code = (short)MstMessageCodes.Queue1v1;
                RoomOptions option = new RoomOptions();
                option.CustomOptions.Set("Room_ID", Room_ID);
                option.OP_Code = OP_Code;

                if (Room_ID != -1 && OP_Code != 0)
                {
                    Mst.Client.Connection.SendMessage((short)MstMessageCodes.Get_Room_Player_List, option, (status, response) =>
                    {
                        if (status == ResponseStatus.Success)
                        {
                            RoomOptions room = response.Deserialize(new RoomOptions());
                            UserID_1 = room.CustomOptions.AsString("ID1");
                            UserID_2 = room.CustomOptions.AsString("ID2");
                            UserID_3 = room.CustomOptions.AsString("ID3");
                            UserID_4 = room.CustomOptions.AsString("ID4");
                            Get_Master_Info = true;
                        }
                    });
                }
            }
        }
    }

    void Search_and_Set_Player()
    {
        Debug.Log("Search_and_Set_Player");

        GameObject NetworkManager = GameObject.Find("-- MIRROR_ROOM_SERVER");
        RoomNetworkManager rm = NetworkManager.GetComponent<RoomNetworkManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            string User_ID = null;
            if (player != null)
                User_ID = player.GetComponent<NetworkIdentity>().User_ID;

            bool user_id_is_Empty = String.IsNullOrEmpty(User_ID);
            if (user_id_is_Empty)
            {
                int connectionID = player.GetComponent<NetworkIdentity>().connectionToClient.connectionId;
                User_ID = Get_User_ID_by_Room_Player(player);
            }

            user_id_is_Empty = String.IsNullOrEmpty(User_ID);
            if (!user_id_is_Empty)
            {
                if (Check_User_ID(UserID_1, User_ID))
                {
                    Player1 = player;
                    player.GetComponent<Player_Network>().Player_Number = 1;
                    Debug.Log("Test_1 || " + UserID_1 + " || " + Player1);
                }

                if (Check_User_ID(UserID_2, User_ID))
                {
                    Player2 = player;
                    player.GetComponent<Player_Network>().Player_Number = 2;
                    Debug.Log("Test_2 || " + UserID_2 + " || " + Player2);
                }

                if (Check_User_ID(UserID_3, User_ID))
                {
                    Player3 = player;
                    player.GetComponent<Player_Network>().Player_Number = 3;
                    Debug.Log("Test_3 || " + UserID_3 + " || " + Player3);
                }

                if (Check_User_ID(UserID_3, User_ID))
                {
                    Player4 = player;
                    player.GetComponent<Player_Network>().Player_Number = 4;
                    Debug.Log("Test_4 || " + UserID_4 + " || " + Player4);
                }
            }
        }

        if (room_Server == null)
            Set_Room_Server();

        if (OP_Code == (short)MstMessageCodes.Queue1v1 || OP_Code == (short)MstMessageCodes.Queue2v2)
            if (Player1 != null && Player2 != null)
                Set_Player_Finish = true;

        if (OP_Code == (short)MstMessageCodes.Queue2op || OP_Code == (short)MstMessageCodes.Queue4op)
            if (Player1 != null && Player2 != null && Player3 != null && Player4 != null)
                Set_Player_Finish = true;

        bool Check_User_ID(string Current_UserID, string New_UserID)
        {
            if (String.IsNullOrEmpty(Current_UserID) || String.IsNullOrEmpty(New_UserID))
                return false;
            if (Current_UserID == New_UserID)
                return true;
            return false;
        }
    }

    string Get_User_ID_by_Room_Player(GameObject Player_Obj)
    {
        RoomPlayer room_Player = null;
        string User_ID = null;
        GameObject NetworkManager = GameObject.Find("-- MIRROR_ROOM_SERVER");
        RoomNetworkManager rm = NetworkManager.GetComponent<RoomNetworkManager>();
        Debug.Log("rm || " + rm);
        int connectionID = Player_Obj.GetComponent<NetworkIdentity>().connectionToClient.connectionId;
        Debug.Log("connectionID || " + connectionID);
        room_Player = rm.Get_Profile_By_Room_Connection(connectionID);
        Debug.Log("room_Player || " + room_Player);
        if (room_Player != null)
        {
            rm.Set_User_ID_To_Player_Obj(connectionID, Player_Obj);
            User_ID = room_Player.UserId;
        }
        return User_ID;
    }

    void Fill_Player_Profile()
    {
        Debug.Log("Fill_Player_Profile");
        if (!String.IsNullOrEmpty(UserID_1) && Player1 != null && Profile_1 == null)
            Get_and_Fill_Profile(Player1, UserID_1);

        if (!String.IsNullOrEmpty(UserID_2) && Player2 != null && Profile_2 == null)
            Get_and_Fill_Profile(Player2, UserID_2);

        if (!String.IsNullOrEmpty(UserID_3) && Player3 != null && Profile_3 == null)
            Get_and_Fill_Profile(Player3, UserID_3);

        if (!String.IsNullOrEmpty(UserID_4) && Player4 != null && Profile_4 == null)
            Get_and_Fill_Profile(Player4, UserID_4);

        if (OP_Code == (short)MstMessageCodes.Queue1v1 || OP_Code == (short)MstMessageCodes.Queue2v2)
            if (Profile_1 != null && Profile_2 != null)
                Room_Server_Setup_Finish = true;

        if (OP_Code == (short)MstMessageCodes.Queue2op || OP_Code == (short)MstMessageCodes.Queue4op)
            if (Profile_1 != null && Profile_2 != null && Profile_3 != null && Profile_4 != null)
                Room_Server_Setup_Finish = true;

        void Get_and_Fill_Profile(GameObject player, string UserID)
        {
            GameObject NetworkManager = GameObject.Find("-- MIRROR_ROOM_SERVER");
            RoomNetworkManager rm = NetworkManager.GetComponent<RoomNetworkManager>();
            int connectionID = player.GetComponent<NetworkIdentity>().connectionToClient.connectionId;
            RoomPlayer room_Player = rm.Get_Profile_By_Room_Connection(connectionID);
            ObservableServerProfile profile = room_Player.Profile;
            Debug.Log("Get_and_Fill_Profile || " + profile.PropertyCount);

            if (UserID_1 == profile.UserId && profile.UserId != null && Player1 != null)
                Profile_1 = get_player(Player1, profile);

            if (UserID_2 == profile.UserId && profile.UserId != null && Player2 != null)
                Profile_2 = get_player(Player2, profile);

            if (UserID_3 == profile.UserId && profile.UserId != null && Player3 != null)
                Profile_3 = get_player(Player3, profile);

            if (UserID_4 == profile.UserId && profile.UserId != null && Player4 != null)
                Profile_4 = get_player(Player4, profile);

            Player_Status.Player get_player(GameObject player_obj, ObservableServerProfile m_profile)
            {
                player_obj.GetComponent<Player_Status>().Set_Local_Profile(m_profile);
                return player_obj.GetComponent<Player_Status>().player;
            }
        }
    }

    void Set_Object_Manager()
    {
        Debug.Log("Set_Object_Manager");
        GameObject Obj = GameObject.Find("Object_Manager");
        if (Obj == null)
            return;

        Object_Manager = Obj;
        Object_Manager object_Manager = Object_Manager.GetComponent<Object_Manager>();
        if (Player1 != null)
            object_Manager.Player_Object_1 = Player1;

        if (Player2 != null)
            object_Manager.Player_Object_2 = Player2;

        object_Manager.Profile_1 = Profile_1;
        object_Manager.Profile_2 = Profile_2;
        object_Manager.User_ID1 = UserID_1;
        object_Manager.User_ID2 = UserID_2;
        object_Manager.OP_Code = OP_Code;

        Debug.Log("object_Manager.Profile_1 || Selected_Map || " + object_Manager.Profile_1.Selected_Map);
        Debug.Log("object_Manager.Profile_2 || Critical_Damage || " + object_Manager.Profile_2.Critical_Damage);

        if (OP_Code == (short)MstMessageCodes.Queue2op || OP_Code == (short)MstMessageCodes.Queue4op)
        {
            object_Manager.Player_Object_3 = Player3;
            object_Manager.Player_Object_4 = Player4;
            object_Manager.Profile_3 = Profile_3;
            object_Manager.Profile_4 = Profile_4;
            object_Manager.User_ID3 = UserID_3;
            object_Manager.User_ID4 = UserID_4;
        }
        Room_Manager_Setup_Finish = true;
    }

    void Setup_Finish()
    {
        CancelInvoke("Set_Room");
        GameObject Obj = GameObject.Find("Object_Manager");
        Obj.GetComponent<Object_Manager>().Setup_Start();
    }

    void test()
    {
        //string ID = "60c8db269c102bf3a406901d";
        Dictionary<string, ObservableServerProfile> m_profilesList = Mst.Server.Profiles.Get_Profile_List();

        foreach (KeyValuePair<string, ObservableServerProfile> item in m_profilesList)
        {
            ObservableServerProfile m_proFile = item.Value;
            string ID = m_proFile.UserId;
            int TEXT = 0;
            string name = m_proFile.GetProperty<ObservableString>((short)MstProFilePropertyCode.DisplayName).GetValue();
            Debug.Log("TEXT || " + TEXT + " || " + name);
        }
    }

    #region Player Rejoin Game
    public void Rejoin_Game_Set_Player_Object(GameObject Player_Obj)
    {
        Debug.Log("Rejoin_Game_Set_Player_Object");
        int connectionID = Player_Obj.GetComponent<NetworkIdentity>().connectionToClient.connectionId;
        Debug.Log("connectionID || " + connectionID);
        GameObject NetworkManager = GameObject.Find("-- MIRROR_ROOM_SERVER");
        Debug.Log("NetworkManager || " + NetworkManager);
        RoomNetworkManager rm = NetworkManager.GetComponent<RoomNetworkManager>();
        Debug.Log("rm || " + rm);
        rm.Set_User_ID_To_Player_Obj(connectionID, Player_Obj);

        string User_ID = Get_User_ID_by_Room_Player(Player_Obj);
        //string User_ID = Player_Obj.GetComponent<NetworkIdentity>().User_ID;
        RoomOptions new_Room = new RoomOptions();
        new_Room.CustomOptions.Set("User_ID", User_ID);

        Debug.Log("User_ID || " + User_ID);

        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Get_Player_Number_By_UserID, new_Room, (status, response) =>
        {
            Debug.Log("status || " + status);
            if (status == ResponseStatus.Success)
            {
                RoomOptions room = response.Deserialize(new RoomOptions());
                short Player_Number = room.CustomOptions.AsShort("Player_Number");

                RoomPlayer room_Player = rm.Get_Profile_By_Room_Connection(connectionID);
                ObservableServerProfile profile = room_Player.Profile;
                Player_Obj.GetComponent<Player_Status>().Set_Local_Profile(profile);
                Player_Status.Player m_Plyaer = Player_Obj.GetComponent<Player_Status>().player;

                GameObject Obj = GameObject.Find("Object_Manager");
                if (Obj == null)
                    return;

                Object_Manager om = Obj.GetComponent<Object_Manager>();
                GameMaster GM = om.GameMaster.GetComponent<GameMaster>();
                GM.Get_Tower_Controller_By_Player_Number(Player_Number).GetComponent<Tower_Controller>().Player_Profile = Player_Obj;
                short P1_Map = (short)om.Profile_1.Selected_Map;
                short P2_Map = (short)om.Profile_2.Selected_Map;
                short P3_Map = (short)om.Profile_3.Selected_Map;
                short P4_Map = (short)om.Profile_4.Selected_Map;
                short op_Room_Code = 0;
                if (OP_Code == (short)MstMessageCodes.Queue1v1)
                    op_Room_Code = (short)OP_Room_Code.Match1v1;
                if (OP_Code == (short)MstMessageCodes.Queue2v2)
                    op_Room_Code = (short)OP_Room_Code.Match2v2;
                if (OP_Code == (short)MstMessageCodes.Queue2op)
                    op_Room_Code = (short)OP_Room_Code.Match2op;
                if (OP_Code == (short)MstMessageCodes.Queue4op)
                    op_Room_Code = (short)OP_Room_Code.Match4op;

                short[] PlayerNumber__OPCode__AllPlayerMapType = new short[] { Player_Number, op_Room_Code, P1_Map, P2_Map, P3_Map, P4_Map };
                Player_Obj.GetComponent<Player_Network>().GameMaster_Setup_Finish_Run_1_Time_Only(PlayerNumber__OPCode__AllPlayerMapType);
                GM.Send_Desk_Level_To_Local_By_Player_Number(Player_Number, Player_Obj);
                Player_Obj.GetComponent<Player_Network>().Set_Desk_Number_To_Local_UI(); // Set_UI

                List<GameObject> temp_towers = new List<GameObject>();
                List<GameObject> towers = new List<GameObject>();
                temp_towers.AddRange(GameObject.FindGameObjectsWithTag("Tower_01"));
                temp_towers.AddRange(GameObject.FindGameObjectsWithTag("Tower_02"));
                temp_towers.AddRange(GameObject.FindGameObjectsWithTag("Tower_03"));
                temp_towers.AddRange(GameObject.FindGameObjectsWithTag("Tower_04"));

                foreach (GameObject tower in temp_towers)
                {
                    if (tower.activeSelf)
                        towers.Add(tower);
                }

                for (int i = 0; i < towers.Count; i++)
                {
                    if (towers[i] != null)
                    {
                        float time = i * 0.0f;
                        StartCoroutine(Update_Tower_From_Server_To_Local(time, towers[i], om, Player_Number, Player_Obj));
                    }
                }

                List<GameObject> temp_Enemy = new List<GameObject>();
                List<GameObject> enemys = new List<GameObject>();
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy_01"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy_02"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy_03"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Enemy_04"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player01_Protector"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player02_Protector"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player03_Protector"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player04_Protector"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player01_Attacker"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player02_Attacker"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player03_Attacker"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player04_Attacker"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player_01_Control_Enemy"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player_02_Control_Enemy"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player_03_Control_Enemy"));
                temp_Enemy.AddRange(GameObject.FindGameObjectsWithTag("Player_04_Control_Enemy"));

                foreach (GameObject enemy in temp_Enemy)
                {
                    if (enemy.activeSelf)
                        enemys.Add(enemy);
                }

                for (int i = 0; i < enemys.Count; i++)
                {
                    if (enemys[i] != null)
                    {
                        float time = i * 0.0f;
                        StartCoroutine(Update_Enemy_From_Server_To_Local(time, enemys[i], om, Player_Obj));
                    }
                }

                float Wait_Time = enemys.Count * 0.1f;
                if (towers.Count > enemys.Count)
                    Wait_Time = towers.Count * 0.1f;

                StartCoroutine(Update_All_Tower_And_Enemy_Finish(Wait_Time, om, GM, Player_Number, Player_Obj, User_ID, m_Plyaer));
            }
        });
    }

    IEnumerator Update_Tower_From_Server_To_Local(float time, GameObject Tower_Obj, Object_Manager om, int Player_Number, GameObject Player_Obj)
    {
        yield return new WaitForSeconds(time);

        if (!Tower_Obj || !Tower_Obj.activeSelf)
            yield break;

        Tower m_Tower = Tower_Obj.GetComponent<Tower>();
        int current_Tower_Player_Number = m_Tower.Player_Number;
        if (Tower_Obj.activeSelf && !m_Tower.Player && Player_Number == current_Tower_Player_Number)
            m_Tower.Player = Player_Obj;

        m_Tower.Update_Tower_Status_To_Local(Player_Obj);
    }

    IEnumerator Update_Enemy_From_Server_To_Local(float time, GameObject Enemy_Obj, Object_Manager om, GameObject Player_Obj)
    {
        yield return new WaitForSeconds(time);
        if (!Enemy_Obj.activeSelf)
            yield break;

        Enemy m_Enemy = Enemy_Obj.GetComponent<Enemy>();

        if (!Enemy_Obj.activeSelf || !m_Enemy)
            Debug.LogWarning("Enemy_Obj.activeSelf || " + Enemy_Obj.name + " || Enemy_Obj.activeSelf || " + Enemy_Obj.activeSelf
                + " || m_Enemy || " + m_Enemy);

        NetworkConnection conn = Player_Obj.GetComponent<NetworkIdentity>().connectionToClient;
        m_Enemy.Refresh_Status(conn);
    }

    IEnumerator Update_All_Tower_And_Enemy_Finish(float time, Object_Manager om, GameMaster GM, short Player_Number,
        GameObject Player_Obj, string User_ID, Player_Status.Player m_Plyaer)
    {
        yield return new WaitForSeconds(time);

        switch (Player_Number)
        {
            case (1):
                Player1 = Player_Obj;
                UserID_1 = User_ID;
                Profile_1 = m_Plyaer;
                om.Player_Object_1 = Player_Obj;
                om.Profile_1 = m_Plyaer;
                om.User_ID1 = User_ID;
                GM.Player1 = Player_Obj;
                GM.Player1_Online_Profile = m_Plyaer;
                break;
            case (2):
                Player2 = Player_Obj;
                UserID_2 = User_ID;
                Profile_2 = m_Plyaer;
                om.Player_Object_2 = Player_Obj;
                om.Profile_2 = m_Plyaer;
                om.User_ID2 = User_ID;
                GM.Player2 = Player_Obj;
                GM.Player2_Online_Profile = m_Plyaer;
                break;
            case (3):
                Player3 = Player_Obj;
                UserID_3 = User_ID;
                Profile_3 = m_Plyaer;
                om.Player_Object_3 = Player_Obj;
                om.Profile_3 = m_Plyaer;
                om.User_ID3 = User_ID;
                GM.Player3 = Player_Obj;
                GM.Player3_Online_Profile = m_Plyaer;
                break;
            case (4):
                Player4 = Player_Obj;
                UserID_4 = User_ID;
                Profile_4 = m_Plyaer;
                om.Player_Object_4 = Player_Obj;
                om.Profile_4 = m_Plyaer;
                om.User_ID4 = User_ID;
                GM.Player4 = Player_Obj;
                GM.Player4_Online_Profile = m_Plyaer;
                break;
        }
    }
    #endregion


}

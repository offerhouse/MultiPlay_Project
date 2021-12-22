using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Network : NetworkBehaviour
{
    bool Player_Netowrk_Awake_Finish = false;
    public string Player_ID;
    //GameObject Temp_Player_Status_Object;
    //GameObject Temp_Get_Player_Status_Object;
    public GameObject Player_UI;
    //Get_Player_Status get_Player_status;
    Player_Status Player_Status;
    public Player_Status.Player My_Local_Profile;
    public GameObject My_Online_Profile;
    NetworkConnection My_Connection;
    public int Player_Number, Online_HP, Online_EXP;

    // Match_Server
    public bool Player, Match_Server;
    GameObject Match_Manager;
    short op_room_code;
    bool Check_Spawn_Scene_Object_and_Spawn_Player_Finish;

    public GameObject Bullet;

    //
    void Awake()
    {
        InvokeRepeating("Check_Local_Player", 0.0f, 1.0f);

        DontDestroyOnLoad(transform.gameObject);
        Player_Netowrk_Awake_Finish = true;
    }

    void Check_Local_Player()
    {
        if (isLocalPlayer || isClient || isServer)
        {
            CancelInvoke("Check_Local_Player");
        }

        Debug.Log("isLocalPlayer || " + isLocalPlayer);

        if (isLocalPlayer)
        {
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            m_local_Manager.GetComponent<Local_Manager>().Local_Player = gameObject;
            InvokeRepeating("Spawn_Scene_Object_and_Spawn_Player_Finish", 0.0f, 1.0f);
        }
    }

    void Spawn_Scene_Object_and_Spawn_Player_Finish()
    {
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        if (!Local_ProFile)
            return;

        bool Spawn_Scene_Object_Finish = Local_ProFile.GetComponent<Mirror_Info>().Spawn_Scene_Object_Finish;
        if (Spawn_Scene_Object_Finish)
        {
            Local_ProFile.GetComponent<Mirror_Info>().Spawn_Scene_Object_Finish = false;
            CancelInvoke("Spawn_Scene_Object_and_Spawn_Player_Finish");

            bool Rejoin_Game = Local_ProFile.GetComponent<Player_Status>().ReJoinGame;
            Debug.Log("Rejoin_Game || " + Rejoin_Game);
            if (Rejoin_Game)
                Start_Rejoin_Game_Setup();
        }
    }

    void Start()
    {
        //GameObject Map_Manager = GameObject.Find("Map_Manager");
        //if (Map_Manager != null)
        //{
        //    Player = Map_Manager.GetComponent<Map_Manager>().Player;
        //    Match_Server = Map_Manager.GetComponent<Map_Manager>().Match_Server;
        //}
        //if (isLocalPlayer)
        //{
        //    if (Player)
        //    {
        //        transform.name = "Player";
        //        Debug.Log("isLocalPlayer " + isLocalPlayer);
        //        Debug.Log("Player_ID || " + Player_ID);

        //        CmdGet_Character_Status(Player_ID);
        //    }
        //    if (Match_Server)
        //    {
        //        Debug.Log("Match_Server || " + Match_Server);
        //        Match_Manager = GameObject.Find("Match_Manager");
        //        Cmd_Tell_Match_Manager();
        //    }
        //}
        //if (isServer)
        //{
        //    Debug.Log("isServer " + isServer);
        //    transform.name = "Server";
        //}
    }

    #region Player

    public void Find_Player_UI()
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        if (m_local_Manager)
            Player_UI = m_local_Manager.GetComponent<Local_Manager>().Player_UI;
    }

    public void Send_1Player_Core_HP_To_Local_Test(int Core_HP_01)
    {
        Rpc_Set_1Player_Core_HP(Core_HP_01);
    }

    public void Send_2Player_Core_HP_To_Local(int Core_HP_01, int Core_HP_02)
    {
        Rpc_Set_2Player_Core_HP(Core_HP_01, Core_HP_02);
    }

    public void Send_4Player_Core_HP_To_Local(int Core_HP_01, int Core_HP_02, int Core_HP_03, int Core_HP_04)
    {
        Rpc_Set_4Player_Core_HP(Core_HP_01, Core_HP_02, Core_HP_03, Core_HP_04);
    }

    [ClientRpc]
    void Rpc_Set_1Player_Core_HP(int Core_HP_01)
    {
        if (Player_Number == 0) return;
        Set_Cor_HP_Text(1, Core_HP_01);
    }

    [ClientRpc]
    void Rpc_Set_2Player_Core_HP(int Core_HP_01, int Core_HP_02)
    {
        if (Player_Number == 0) return;
        Set_Cor_HP_Text(1, Core_HP_01);
        Set_Cor_HP_Text(2, Core_HP_02);
    }

    [ClientRpc]
    void Rpc_Set_4Player_Core_HP(int Core_HP_01, int Core_HP_02, int Core_HP_03, int Core_HP_04)
    {
        if (Player_Number == 0) return;
        Set_Cor_HP_Text(1, Core_HP_01);
        Set_Cor_HP_Text(2, Core_HP_02);
        Set_Cor_HP_Text(3, Core_HP_03);
        Set_Cor_HP_Text(4, Core_HP_04);
    }

    void Set_Cor_HP_Text(int player_Number, float Core_HP)
    {
        GameObject Map = Get_Local_Map(player_Number);
        if (!Map)
            return;
        GameObject Core_Text = Map.GetComponent<Object_Status>().Core_HP_Text;
        Core_Text.GetComponent<Text>().text = Core_HP.ToString();
    }

    void Create_Local_Player_Profile(string player_ID)
    {
        //GameObject temp_status = Resources.Load("Player_Status") as GameObject;
        //GameObject Status = My_Online_Profile = (GameObject)Instantiate(temp_status, Vector3.zero, Quaternion.identity);
        //Player_Status Player_Status = Status.GetComponent<Player_Status>();
        //My_Local_Profile = Player_Status.Creat_New_Player_Status();
    }

    [Command] // this is Server Side
    public void CmdGet_Character_Status(string player_ID)
    {
        //gameObject.name = "Player " + player_ID;
        //this.Player_ID = player_ID;
        //Create_Local_Player_Profile(player_ID);
        //NetworkIdentity identity = GetComponent<NetworkIdentity>();
        //My_Connection = identity.connectionToClient;

        //GameObject temp_status = Resources.Load("Player_Status") as GameObject;
        //Temp_Player_Status_Object = (GameObject)Instantiate(temp_status, Vector3.zero, Quaternion.identity);
        //Player_Status = Temp_Player_Status_Object.GetComponent<Player_Status>();

        //GameObject get_player_status = Resources.Load("Get_Player_Status") as GameObject;
        //Temp_Get_Player_Status_Object = (GameObject)Instantiate(get_player_status, Vector3.zero, Quaternion.identity);
        //get_Player_status = Temp_Get_Player_Status_Object.GetComponent<Get_Player_Status>();
        //get_Player_status.Player_ID = player_ID;

        //get_Player_status.getCharacterInfo(Temp_Player_Status_Object, My_Online_Profile);
        //StartCoroutine(Wait_Data_Finish(false, My_Connection));
    }

    //IEnumerator Wait_Data_Finish(bool wait, NetworkConnection conn)
    //{
    //while (!wait)
    //{
    //    //wait = get_Player_status.Data_Finish;
    //    yield return new WaitForSeconds(0.5f);

    //}
    ////Player_Status.Player player_to_client = Temp_Get_Player_Status_Object.GetComponent<Get_Player_Status>().Player;

    ////My_Online_Profile.GetComponent<Player_Status>().player = My_Online_Profile.GetComponent<Player_Status>().Creat_New_Player_Status();
    ////My_Online_Profile.GetComponent<Player_Status>().player = Temp_Get_Player_Status_Object.GetComponent<Get_Player_Status>().Player;
    //My_Online_Profile.GetComponent<Player_Status>().Player_Object = gameObject;
    ////My_Online_Profile.GetComponent<Player_Status>().player.Player_ID = Player_ID;
    ////player_to_client.Player_ID = Player_ID;
    ////TargetSetObject(conn, player_to_client);//, Test, HP, EXP , Shop_Timer);

    //Destroy(Temp_Player_Status_Object);
    //Destroy(Temp_Get_Player_Status_Object);
    //}

    public void One_VS_One()
    {
        CmdCreate_One_VS_One_Map();
    }

    [Command]
    public void CmdCreate_One_VS_One_Map()
    {
        GameObject Map_Manager = GameObject.Find("Map_Manager");
        //Map_Manager.GetComponent<Map_Manager>().Create_One_VS_One_Map(gameObject);
    }

    #region Desk and Gold To Local
    public void Set_Player_Desk_Power_Up(int Desk_Level_1, int Desk_Level_2, int Desk_Level_3, int Desk_Level_4, int Desk_Level_5)
    {
        Debug.Log("Set_Player_Desk_Power_Up || " + Desk_Level_1 + " || " + Desk_Level_2 + " || " + Desk_Level_3 + " || " + Desk_Level_4 + " || " + Desk_Level_5);
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_Desk_Power_Level(My_Connection, Desk_Level_1, Desk_Level_2, Desk_Level_3, Desk_Level_4, Desk_Level_5);
    }

    public void Set_Player_Gold_And_Desk_Exp(int Gold, int Create_Tower_Gold, float Desk_1, float Desk_2, float Desk_3, float Desk_4, float Desk_5)
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_Gold_and_Desk_Exp(My_Connection, Gold, Create_Tower_Gold, Desk_1, Desk_2, Desk_3, Desk_4, Desk_5);
    }

    public void Set_Player_Gold(int Gold)
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_Update_Gold(My_Connection, Gold);
    }

    [TargetRpc] // Server to Client
    public void Target_Update_Gold(NetworkConnection conn, int Gold)
    {
        if (!Player_UI)
            Find_Player_UI();
        if (Player_UI)
            Player_UI.GetComponent<UI>().Update_Gold(Gold);
    }

    [TargetRpc] // Server to Client
    public void Target_Desk_Power_Level(NetworkConnection conn, int Desk_Lv_1, int Desk_Lv_2, int Desk_Lv_3, int Desk_Lv_4, int Desk_Lv_5)
    {
        Debug.Log("Target_Desk_Power_Level || " + Desk_Lv_1 + " || " + Desk_Lv_2 + " || " + Desk_Lv_3 + " || " + Desk_Lv_4 + " || " + Desk_Lv_5);
        if (!Player_UI)
            Find_Player_UI();
        if (Player_UI)
            Player_UI.GetComponent<UI>().Update_Desk_Level(Desk_Lv_1, Desk_Lv_2, Desk_Lv_3, Desk_Lv_4, Desk_Lv_5);
    }

    [TargetRpc] // Server to Client
    public void Target_Gold_and_Desk_Exp(NetworkConnection conn, int Gold, int Create_Tower_Gold, float Desk_1, float Desk_2, float Desk_3, float Desk_4, float Desk_5)
    {
        if (!Player_UI)
            Find_Player_UI();
        if (Player_UI)
            Player_UI.GetComponent<UI>().Update_Gold_and_Desk_Exp(Gold, Create_Tower_Gold, Desk_1, Desk_2, Desk_3, Desk_4, Desk_5);
    }

    public void Set_Desk_Number_To_Local_UI()
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        int Desk_Number = GetComponent<Player_Status>().player.Current_Desk;
        int[] Desk = GetComponent<Player_Status>().Get_Current_Tower_Desk(Desk_Number);
        short[] short_Desk = new short[] { 0, (short)Desk[1], (short)Desk[2], (short)Desk[3], (short)Desk[4], (short)Desk[5] };
        Target_Set_Desk_Number_To_Local_UI(My_Connection, short_Desk);
    }

    [TargetRpc] // Server to Client
    public void Target_Set_Desk_Number_To_Local_UI(NetworkConnection conn, short[] short_Desk)
    {
        Debug.Log("Target_Set_Desk_Number_To_Local_UI");
        if (!Player_UI)
            Find_Player_UI();
        if (Player_UI)
            Player_UI.GetComponent<UI>().Set_Desk_Icon(short_Desk[1], short_Desk[2], short_Desk[3], short_Desk[4], short_Desk[5]);
    }

    #endregion

    [TargetRpc] // Server to Client
    public void TargetSetObject(NetworkConnection conn, Player_Status.Player player)// , bool wait)
    {
        Debug.Log("TargetSetObject");
        My_Local_Profile = player;
        Debug.Log("Load Finish");
        //SceneManager.LoadScene(1);
    }

    [TargetRpc] // Server to Client
    public void TargetBattle_Start(NetworkConnection conn, int player_number)
    {
        Player_Number = player_number;
        Set_Local_Camera();
    }

    public void Create_Tower()
    {
        //Debug.Log("Create_Tower");
        CmdCreate_Tower();
    }

    [Command]
    public void CmdCreate_Tower()
    {
        GameObject GM_Object = GameObject.Find("Game_Master");
        //Debug.Log("Game_Master || " + GM_Object);
        GM_Object.GetComponent<GameMaster>().Create_Tower(gameObject);
    }

    [Command]
    public void CmdCombine_Tower_Check(GameObject Drag_Tower, GameObject Target_Tower)
    {
        GameObject GM_Object = GameObject.Find("Game_Master");
        GM_Object.GetComponent<GameMaster>().Combine_Tower(gameObject, Drag_Tower, Target_Tower);

    }

    public void Not_Allow_Combine_Tower(GameObject Drag_Tower, GameObject Target_Tower)
    {
        RpcNot_Allow_Combine(Drag_Tower, Target_Tower);
    }

    [ClientRpc]
    void RpcNot_Allow_Combine(GameObject Drag_Tower, GameObject Target_Tower)
    {
        Drag_Tower.GetComponent<Tower>().Reset_Position();
        Drag_Tower.GetComponent<Tower>().collider_hit_Tower = false;
        Drag_Tower.GetComponent<Tower>().Dragger = false;
        Drag_Tower.GetComponent<Tower>().End_Drag = false;
        Drag_Tower.GetComponent<CapsuleCollider>().enabled = true;

        Drag_Tower.GetComponent<Tower>().Reset_Position();
        Target_Tower.GetComponent<Tower>().collider_hit_Tower = false;
        Target_Tower.GetComponent<Tower>().Dragger = false;
        Target_Tower.GetComponent<Tower>().End_Drag = false;
        Target_Tower.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Set_Local_Camera()
    {
        Debug.LogWarning("Set_Local_Camera || " + Player_Number);
        if (Player_Number == 1)
            Set_Camera(true, false, false, false);

        if (Player_Number == 2)
            Set_Camera(false, true, false, false);

        if (Player_Number == 3)
            Set_Camera(false, false, true, false);

        if (Player_Number == 4)
            Set_Camera(false, false, false, true);

        void Set_Camera(bool camera_one, bool camera_two, bool camera_three, bool camera_four)
        {
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            Local_Manager lm = m_local_Manager.GetComponent<Local_Manager>();
            GameObject camera_1 = lm.Get_Camera(1);
            GameObject camera_2 = lm.Get_Camera(2);
            GameObject camera_3 = lm.Get_Camera(3);
            GameObject camera_4 = lm.Get_Camera(4);

            lm.Player_01_Light.SetActive(camera_one);
            lm.Player_02_Light.SetActive(camera_two);
            lm.Player_03_Light.SetActive(camera_three);
            lm.Player_04_Light.SetActive(camera_four);

            if (camera_1 != null)
                camera_1.SetActive(camera_one);

            if (camera_2 != null)
                camera_2.SetActive(camera_two);

            if (camera_3 != null)
                camera_3.SetActive(camera_three);

            if (camera_4 != null)
                camera_4.SetActive(camera_four);
        }
    }

    public void All_Enemy_Fear()
    {
        Cmd_All_Enemy_Fear();
    }

    [Command]
    public void Cmd_All_Enemy_Fear()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy_01");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Enemy_Set_Fear();
            Debug.Log("Cmd_All_Enemy_Fear");
        }
    }

    GameObject Get_Local_Map(int play_number)
    {
        GameObject Map = null;
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        if (!m_local_Manager)
            return Map;
        switch (play_number)
        {
            case (1):
                Map = m_local_Manager.GetComponent<Local_Manager>().Player_01_Local_Map;
                break;
            case (2):
                Map = m_local_Manager.GetComponent<Local_Manager>().Player_02_Local_Map;
                break;
            case (3):
                Map = m_local_Manager.GetComponent<Local_Manager>().Player_03_Local_Map;
                break;
            case (4):
                Map = m_local_Manager.GetComponent<Local_Manager>().Player_04_Local_Map;
                break;
        }
        return Map;
    }

    public void Tower_Desk_Power_Up(int Desk_Number)
    {
        Cmd_Tower_Desk_Power_Up(Desk_Number);
    }

    [Command]
    public void Cmd_Tower_Desk_Power_Up(int Desk_Number)
    {
        GameObject GM_Object = GameObject.Find("Game_Master");
        GM_Object.GetComponent<GameMaster>().Tower_Desk_Power_Up(gameObject, Desk_Number);
    }

    public void GameMaster_Setup_Finish_Run_1_Time_Only(short[] PlayerNumber__OPCode__AllPlayerMapType)
    {
        Debug.Log("GameMaster_Setup_Finish_Run_1_Time_Only");
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_GameMaster_Setup_Finish_Run_1_Time_Only(My_Connection, PlayerNumber__OPCode__AllPlayerMapType);
    }

    [TargetRpc] // GameMaster load Finish . tell local run 1 time
    public void Target_GameMaster_Setup_Finish_Run_1_Time_Only(NetworkConnection conn, short[] PlayerNumber__OPCode__AllPlayerMapType)
    {
        Player_Number = PlayerNumber__OPCode__AllPlayerMapType[0];
        op_room_code = PlayerNumber__OPCode__AllPlayerMapType[1];
        Debug.Log("Target_GameMaster_Setup_Finish_Run_1_Time_Only || " + op_room_code);
        if (!Player_UI)
            Find_Player_UI();

        GameObject Local_Manager = GameObject.Find("Local_Manager");

        if (op_room_code == (short)OP_Room_Code.Match1v1)
            Local_Manager.GetComponent<Local_Manager>().One_VS_One = true;
        if (op_room_code == (short)OP_Room_Code.Match2op)
            Local_Manager.GetComponent<Local_Manager>().Two_VS_Two = true;
        if (op_room_code == (short)OP_Room_Code.Match2v2)
            Local_Manager.GetComponent<Local_Manager>().Two_Cooperation = true;
        if (op_room_code == (short)OP_Room_Code.Match4op)
            Local_Manager.GetComponent<Local_Manager>().Four_Cooperation = true;

        Local_Manager.GetComponent<Local_Manager>().Player1_Map_Type = PlayerNumber__OPCode__AllPlayerMapType[2];
        Local_Manager.GetComponent<Local_Manager>().Player2_Map_Type = PlayerNumber__OPCode__AllPlayerMapType[3];
        Local_Manager.GetComponent<Local_Manager>().Player3_Map_Type = PlayerNumber__OPCode__AllPlayerMapType[4];
        Local_Manager.GetComponent<Local_Manager>().Player4_Map_Type = PlayerNumber__OPCode__AllPlayerMapType[5];

        Local_Manager.GetComponent<Local_Manager>().Local_Player = gameObject;
        Local_Manager.GetComponent<Local_Manager>().Set_Local_Player(Player_Number, gameObject);
        Local_Manager.GetComponent<Local_Manager>().Instantiate_Local_Map();
        Set_Local_Camera();

        Find_Player_UI();
        Player_UI.GetComponent<UI>().Local_Player = gameObject;
        Debug.Log("Player_Number || " + Player_Number);
        GameObject Map = Get_Local_Map(Player_Number);
        if (!Map)
            return;
        Debug.Log("Map || " + Map);
        Map.GetComponent<Object_Status>().Set_Group_Array();
    }
    #endregion

    #region Local_Check_Variable_Empty // for Reconnect Game
    // Tower
    public void Client_Reconnect_Game_Request_Tower_Refresh_Status(GameObject _Obj)
    {
        //bool connected = Telepathy.Client.Connected;
        bool isConnected = NetworkClient.isConnected;

        if (isConnected && Player_Netowrk_Awake_Finish)
            Cmd_Client_Reconnect_Game_Request_Tower_Refresh_Status(_Obj);
    }

    [Command]
    void Cmd_Client_Reconnect_Game_Request_Tower_Refresh_Status(GameObject _Obj)
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        _Obj.GetComponent<Tower>().Refresh_Status(My_Connection);
    }
    // Enemy
    public void Client_Reconnect_Game_Request_Enemy_Refresh_Status(GameObject _Obj)
    {
        bool isConnected = NetworkClient.isConnected;

        //if (isConnected && Player_Netowrk_Awake_Finish)
        //Cmd_Client_Reconnect_Game_Request_Enemy_Refresh_Status(_Obj);

    }

    [Command]
    void Cmd_Client_Reconnect_Game_Request_Enemy_Refresh_Status(GameObject _Obj)
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        _Obj.GetComponent<Enemy>().Refresh_Status(My_Connection);
    }
    #endregion

    public void Quit_Game()
    {
        Debug.Log("Quit_Game_Player_Network");
        // NetworkClient.Shutdown(); OLD
        NetworkClient.Disconnect();
        //Find_and_Destroy_GameOBject("-- MIRROR_NETWORK_MANAGER"); < old mirror and mst version
        Find_and_Destroy_GameOBject("-- MIRROR_ROOM_SERVER");
        Find_and_Destroy_GameOBject("-- MIRROR_ROOM_CLIENT");
        Find_and_Destroy_GameOBject("Local_ProFile");

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        void Find_and_Destroy_GameOBject(string name)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
                Destroy(obj);
        }
    }

    void Start_Rejoin_Game_Setup()
    {
        Debug.Log("Rejoin_Game_Set_Player_Object");
        Cmd_Rejoin_Game_Set_Player_Object();
    }

    [Command]
    public void Cmd_Rejoin_Game_Set_Player_Object()
    {
        GameObject Room_Manager_Object = GameObject.Find("Room_Manager");
        Room_Manager_Object.GetComponent<Room_Manager>().Rejoin_Game_Set_Player_Object(gameObject);
    }

    //public void Server_Tell_Tower_Set_Visible(GameObject Obj)
    //{
    //    Target_Local_Tower_Set_Visible(Obj);
    //}

    //[TargetRpc]
    //public void Target_Local_Tower_Set_Visible(GameObject Obj)
    //{
    //    Obj.GetComponent<Tower>().enabled = true;
    //    Obj.GetComponent<Tower>().Local_Tower_Set_Visible();
    //}

    public void Server_Tell_Client_End_Game()
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_Local_End_Game(My_Connection);
    }

    [TargetRpc]
    public void Target_Local_End_Game(NetworkConnection Conn)
    {
        Local_End_Game();
    }

    public void Local_End_Game()
    {
        Debug.Log("||_Local_End_Game_||");
        #region Reset Local Object to Pool
        GameObject Tower_Folder = GameObject.Find("Tower");
        GameObject Local_Manager_Folder = GameObject.Find("Local_Manager");
        GameObject pool_Manager = GameObject.Find("Pool_Manager");

        foreach (Transform obj in Local_Manager_Folder.transform)
        {
            if (obj.GetComponent<Tower>())
            {
                Debug.Log("Tower_in_Local_Manager || " + obj.name + " || " + Tower_Folder);
                obj.GetComponent<Tower>().Reset_Tower_To_Pool();
            }
        }

        foreach (Transform obj in Tower_Folder.transform)
        {
            obj.GetComponent<Tower>().Reset_Tower_To_Pool();
            Debug.Log("Tower_Folder || " + obj.name);
        }

        int temp_number_1 = 0;
        foreach (Transform obj in pool_Manager.GetComponent<Pool_Manager>().Tower_Folder.transform)
        {
            temp_number_1++;
        }
        Debug.Log("temp_number_1 || " + temp_number_1);

        GameObject Enemy_Folder = GameObject.Find("Enemy");
        foreach (Transform obj in Enemy_Folder.transform)
        {
            obj.GetComponent<Enemy>().Local_Send_Obj_To_Pool();
            Debug.Log("Enemy_Folder || " + obj.name);
        }

        GameObject Protector_Folder = GameObject.Find("Protector");
        foreach (Transform obj in Protector_Folder.transform)
        {
            obj.GetComponent<Enemy>().Local_Send_Obj_To_Pool();
            Debug.Log("Protector_Folder || " + obj.name);
        }

        GameObject Attacker_Folder = GameObject.Find("Attacker");
        foreach (Transform obj in Attacker_Folder.transform)
        {
            obj.GetComponent<Enemy>().Local_Send_Obj_To_Pool();
            Debug.Log("Attacker_Folder || " + obj.name);
        }

        if (pool_Manager)
        {
            int number = 0;
            Pool_Manager m_Pool = pool_Manager.GetComponent<Pool_Manager>();

            foreach (Transform obj in m_Pool.Tower_Folder.transform)
            {
                number++;
                obj.GetComponent<Tower>().Reset_Tower_To_Pool();
                Debug.Log("m_Pool.Tower_Folder || " + obj.name + " || " + gameObject.activeSelf);
            }

            Debug.Log("Total_Number || " + number);
            Set_To_Pool_Folder(Tower_Folder, m_Pool.Tower_Folder);
            Set_To_Pool_Folder(Enemy_Folder, m_Pool.Enemy_Folder);
            Set_To_Pool_Folder(Protector_Folder, m_Pool.Enemy_Folder);
            Set_To_Pool_Folder(Attacker_Folder, m_Pool.Enemy_Folder);
            //Set_To_Pool_Folder(Attacker_Folder, m_Pool.Enemy_Folder);
            
            int temp_number_2 = 0;
            foreach (Transform obj in m_Pool.Tower_Folder.transform)
            {
                temp_number_2 ++;
            }
            Debug.Log("temp_number_2 || " + temp_number_2);

            m_Pool.Reset_Pool();
        }
        #endregion
        //NetworkManager.singleton.StopHost();
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        GameObject end_game_canvas = m_local_Manager.GetComponent<Local_Manager>().End_Game_Canvas;
        end_game_canvas.SetActive(true);
        end_game_canvas.GetComponent<MasterServerToolkit.MasterServer.Info>().End_Game_Canvas();
        GameObject local_Profile = GameObject.Find("Local_ProFile");
        if (local_Profile)
        {
            local_Profile.name = "OLD_Profile";
            Debug.Log("Local_ProFile_1 || " + local_Profile.name);
            Destroy(local_Profile);
            Debug.Log("Local_ProFile_2 || " + local_Profile.name);
        }
        Destroy(gameObject);
    }

    void Set_To_Pool_Folder(GameObject current_Folder, GameObject target_Folder)
    {
        foreach (Transform obj in current_Folder.transform)
        {
            obj.transform.SetParent(target_Folder.transform);
            Debug.Log("current_Folder || " + current_Folder.name + " || target_Folder || " + target_Folder + " || " + obj.name);
        }
    }

    public void Core_Shot_Enemy(GameObject Closest_Enemy, short Core_Player_Number)
    {
        My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        Target_Core_Shot_Enemy(My_Connection, Closest_Enemy, Core_Player_Number);
    }

    public void Core_Damage_Tag(short player_Number, int Damage_Value)
    {
        Vector3 POS = new Vector3(0, -50, 0);
        GameObject local_Manager = GameObject.Find("Local_Manager");
        if (!local_Manager)
            return;
        if (local_Manager)
        {
            GameObject Core = local_Manager.GetComponent<Local_Manager>().Get_Core_By_Player_Number((short)player_Number);
            POS = Core.transform.position + new Vector3(0, 1.5f, 0);
        }

        GameObject Camera = local_Manager.GetComponent<Local_Manager>().Get_Camera();
        GameObject m_Pool_Manager = GameObject.Find("Pool_Manager");
        if (!m_Pool_Manager)
            return;

        GameObject damage_bar = m_Pool_Manager.GetComponent<Pool_Manager>().Get_Object_From_Pool_By_Tag("Damage_Bar");
        if (damage_bar != null)
        {
            damage_bar.transform.position = POS;
            damage_bar.SetActive(true);
        }
        if (damage_bar == null)
        {
            GameObject Damage_Bar_OBj = m_Pool_Manager.GetComponent<Pool_Manager>().Damage_Bar_OBj;
            damage_bar = Instantiate(Damage_Bar_OBj, POS, Quaternion.identity);
        }

        short code = (short)Enemy_Code.Normal_Attack;
        damage_bar.GetComponent<Object_Status>().Set_Damage_Bar(code, Damage_Value, Camera);
    }

    [TargetRpc]
    public void Target_Core_Shot_Enemy(NetworkConnection Conn, GameObject Closest_Enemy, short Core_Player_Number)
    {
        Vector3 POS = new Vector3(0, -50, 0);
        GameObject local_Manager = GameObject.Find("Local_Manager");
        if (local_Manager)
        {
            GameObject Core = local_Manager.GetComponent<Local_Manager>().Get_Core_By_Player_Number((short)Core_Player_Number);
            POS = Core.transform.position + new Vector3(0, 1.5f, 0);
        }

        float Distance = Vector3.Distance(POS, Closest_Enemy.transform.position);
        GameObject bullet = null;
        bool Get_Bullet_from_pool = false;
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        if (pool_Manager)
            if (pool_Manager.GetComponent<Pool_Manager>().Bullet_Pool.Count != 0)
            {
                bullet = local_Manager.GetComponent<Local_Manager>().Get_Bullet_Form_Pool();
                if (bullet)
                {
                    Get_Bullet_from_pool = true;
                    bullet.transform.position = POS;
                    bullet.SetActive(true);
                }
            }

        if (!Get_Bullet_from_pool)
            bullet = Instantiate(Bullet, POS, Quaternion.identity);

        float Send_To_Pool_Time = Distance / 12;
        bullet.GetComponent<Bullet>().Send_To_Pool_By_Time(Send_To_Pool_Time);
        //GameObject target, int Core_Type use 101 bullet , bool move_bullet, bool line_render, float speed, Target object name
        bullet.GetComponent<Bullet>().Set_Bullet(Closest_Enemy, 101, true, false, 12, Closest_Enemy.name);
    }
}

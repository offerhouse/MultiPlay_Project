using System.Collections;
using System.Collections.Generic;
using MasterServerToolkit.MasterServer;
using UnityEngine;

public class Object_Manager : MonoBehaviour
{
    public short OP_Code;
    public GameObject GameMaster, Map_Manager, Player_Object_1, Player_Object_2, Player_Object_3, Player_Object_4;

    public GameObject Map_Point_01, Map_Point_02, Map_Point_03, Map_Point_04;
    public GameObject Map_01, Map_02, Map_03, Map_04, Map_05, Map_06, Map_07, Map_08, Map_09, Map_10, Map_11, Map_12;

    public GameObject Core_01, Core_02, Core_03, Core_04;
    public GameObject Tower_Controller_01, Tower_Controller_02, Tower_Controller_03, Tower_Controller_04;
    public GameObject Map_Controller_01, Map_Controller_02, Map_Controller_03, Map_Controller_04;
    public Player_Status.Player Profile_1, Profile_2, Profile_3, Profile_4;
    public string User_ID1, User_ID2, User_ID3, User_ID4;
    public int Map_Type_Player_01, Map_Type_Player_02, Map_Type_Player_03, Map_Type_Player_04;

    bool GM_Setup_Finish, Map_Manager_Finish;
    public bool Object_Manager_Finish;

    public void Setup_Start()
    {
        if (!GM_Setup_Finish)
            InvokeRepeating("Start_Setup", 0.0f, 1.0f);
    }

    void Start_Setup()
    {
        if (GameMaster == null)
            return;

        if (!GM_Setup_Finish)
        {
            GameMaster gm = GameMaster.GetComponent<GameMaster>();
            gm.Object_Manager = gameObject;
            Debug.Log("Profile_1.Selected_Map || " + Profile_1.Selected_Map);
            Debug.Log("Profile_2.Selected_Map || " + Profile_2.Selected_Map);
            if (Player_Object_1 != null)
            {
                Map_Type_Player_01 = Profile_1.Selected_Map;
                gm.Map_Type_Player_01 = (short)Map_Type_Player_01;
                gm.Player1 = Player_Object_1;
                gm.Player1_Online_Profile = Profile_1;
            }
            if (Player_Object_2 != null)
            {
                Map_Type_Player_02 = Profile_2.Selected_Map;
                gm.Map_Type_Player_02 = (short)Map_Type_Player_02;
                gm.Player2 = Player_Object_2;
                gm.Player2_Online_Profile = Profile_2;
            }
            if (Player_Object_3 != null)
            {
                Map_Type_Player_03 = Profile_3.Selected_Map;
                gm.Map_Type_Player_03 = (short)Map_Type_Player_03;
                gm.Player3 = Player_Object_3;
                gm.Player3_Online_Profile = Profile_3;
            }
            if (Player_Object_4 != null)
            {
                Map_Type_Player_04 = Profile_4.Selected_Map;
                gm.Map_Type_Player_04 = (short)Map_Type_Player_04;
                gm.Player4 = Player_Object_4;
                gm.Player4_Online_Profile = Profile_4;
            }
            GM_Setup_Finish = true;
            gm.GameMaster_Start();
        }

        if (!Map_Manager_Finish)
        {
            Map_Manager.GetComponent<Map_Manager>().Object_Manager = gameObject;
            Map_Manager.GetComponent<Map_Manager>().OP_Code = OP_Code;
            Map_Manager.GetComponent<Map_Manager>().Setup_Map_Manager();
            Map_Manager_Finish = true;
        }

        if (!Map_Manager_Finish)
            return;


        // last step ;;;;; Instantiate map on Map_Manager
        //Debug.Log("Create_One_VS_One_Map " + Player_Object);
        //GameObject one_vs_one = Instantiate(One_VS_One_Map, Vector3.zero, Quaternion.identity);
        //one_vs_one.GetComponent<GameMaster>().Player1 = Player_Object;
        //one_vs_one.GetComponent<GameMaster>().Map_Manager = gameObject;
        //One_VS_One = true;

        Object_Manager_Finish = true;
        CancelInvoke("Start_Setup");
    }

    public void Set_Tower_Controller(int Player_Number, GameObject tower_controller)
    {
        if (Player_Number == 1) Tower_Controller_01 = tower_controller;
        if (Player_Number == 2) Tower_Controller_02 = tower_controller;
        if (Player_Number == 3) Tower_Controller_03 = tower_controller;
        if (Player_Number == 4) Tower_Controller_04 = tower_controller;
    }

    public void GM_Set_MapController_and_Core(int Player_Number, GameObject map_Controller, GameObject Core)
    {
        switch (Player_Number)
        {
            case (1):
                Map_Controller_01 = map_Controller;
                Core_01 = Core;
                break;
            case (2):
                Map_Controller_02 = map_Controller;
                Core_02 = Core;
                break;
            case (3):
                Map_Controller_03 = map_Controller;
                Core_03 = Core;
                break;
            case (4):
                Map_Controller_04 = map_Controller;
                Core_04 = Core;
                break;
        }
    }

    public short Get_Current_Match_Type_OP_Code()
    {
        short op_room_Code = 0;
        if (OP_Code == (short)MstMessageCodes.Queue1v1)
            op_room_Code = (short)OP_Room_Code.Match1v1;

        if (OP_Code == (short)MstMessageCodes.Queue2v2)
            op_room_Code = (short)OP_Room_Code.Match2v2;

        if (OP_Code == (short)MstMessageCodes.Queue2op)
            op_room_Code = (short)OP_Room_Code.Match2op;

        if (OP_Code == (short)MstMessageCodes.Queue4op)
            op_room_Code = (short)OP_Room_Code.Match4op;

        return op_room_Code;
    }

    public GameObject Get_Player_Obj(int number)
    {
        GameObject[] player_obj = new GameObject[] { null, Player_Object_1, Player_Object_2, Player_Object_3, Player_Object_4 };
        return player_obj[number];
    }

    public GameObject Get_Tower_Controller(int number)
    {
        GameObject[] T = new GameObject[] { null, Tower_Controller_01, Tower_Controller_02, Tower_Controller_03, Tower_Controller_04 };
        return T[number];
    }

    public Player_Status.Player Get_Player_profile(int number)
    {
        Player_Status.Player[] T = new Player_Status.Player[] { null, Profile_1, Profile_2, Profile_3, Profile_4 };
        return T[number];
    }

}

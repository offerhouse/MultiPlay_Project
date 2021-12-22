using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Tower_Controller : NetworkBehaviour
{
    public int Player_Number; // this is Player1 , Player2 , Player3, Player4
    public GameObject Camera;
    //public GameObject Player_Online_Profile;
    public GameObject Player_Profile;
    public GameObject Tower_Object;

    public GameObject GameMaster;
    public GameObject Map_Manager;
    public GameObject Core;

    public int Tower_Slot_QTY, Tower_Number = 0;
    public GameObject[] Tower_Slot_Array = new GameObject[25];
    public GameObject Tower_Slot_01, Tower_Slot_02, Tower_Slot_03, Tower_Slot_04, Tower_Slot_05, Tower_Slot_06, Tower_Slot_07, Tower_Slot_08;
    public GameObject Tower_Slot_09, Tower_Slot_10, Tower_Slot_11, Tower_Slot_12, Tower_Slot_13, Tower_Slot_14, Tower_Slot_15, Tower_Slot_16;
    public GameObject Tower_Slot_17, Tower_Slot_18, Tower_Slot_19, Tower_Slot_20, Tower_Slot_21, Tower_Slot_22, Tower_Slot_23, Tower_Slot_24;

    public GameObject[] Local_Tower = new GameObject[25];

    [HideInInspector] Tower_Slot_Class[] Tower_Slot_Class_Array;
    [HideInInspector]
    public Tower_Slot_Class Slot_01, Slot_02, Slot_03, Slot_04, Slot_05, Slot_06, Slot_07, Slot_08, Slot_09, Slot_10, Slot_11, Slot_12;
    [HideInInspector]
    public Tower_Slot_Class Slot_13, Slot_14, Slot_15, Slot_16, Slot_17, Slot_18, Slot_19, Slot_20, Slot_21, Slot_22, Slot_23, Slot_24;

    int Player_Desk_Tower_01, Player_Desk_Tower_02, Player_Desk_Tower_03, Player_Desk_Tower_04, Player_Desk_Tower_05;
    public float Basic_Damage_Bouns_Player_01 = 0, Basic_Damage_Bouns_Player_02 = 0, Basic_Damage_Bouns_Player_03 = 0, Basic_Damage_Bouns_Player_04 = 0;

    public Group_Bouns[] Group;
    [HideInInspector] public Group_Bouns Group_01, Group_02, Group_03, Group_04, Group_05, Group_06, Group_07, Group_08, Group_09;
    [HideInInspector] public int[] GroupSlot_01, GroupSlot_02, GroupSlot_03, GroupSlot_04, GroupSlot_05, GroupSlot_06, GroupSlot_07, GroupSlot_08, GroupSlot_09;

    public int Tower_Level_Desk_1, Tower_Level_Desk_2, Tower_Level_Desk_3, Tower_Level_Desk_4, Tower_Level_Desk_5;
    public int Power_Up_Desk_1 = 1, Power_Up_Desk_2 = 1, Power_Up_Desk_3 = 1, Power_Up_Desk_4 = 1, Power_Up_Desk_5 = 1;
    public float Desk_01_Exp, Desk_02_Exp, Desk_03_Exp, Desk_04_Exp, Desk_05_Exp;

    bool combining = false;

    public List<GameObject> Tower = new List<GameObject>();

    public class Group_Bouns
    {
        public float Basic_Damage;
        public float Speed;
        public float Critical;
        public float Add_Core;
        public float Add_Core_Damage;
        public float Exp;
        public float Lucky;

        public Group_Bouns(float damage, float speed, float critical, float add_core, float add_core_damage, float exp, float lucky)
        {
            Basic_Damage = damage;
            Speed = speed;
            Critical = critical;
            Add_Core = add_core;
            Add_Core_Damage = add_core_damage;
            Exp = exp;
            Lucky = lucky;
        }
    }

    [System.Serializable]
    public class Tower_Slot_Class
    {
        public GameObject Tower;
        public int Slot_Number;
        public int Type;
        public int Combine_Up_Point;

        public Tower_Slot_Class(GameObject tower, int slot_number, int type, int combine_up_point)
        {
            Tower = tower;
            Slot_Number = slot_number;
            Type = type;
            Combine_Up_Point = combine_up_point;
        }
    }

    public void Set_Desk_Tower(int[] Desk)
    {
        Player_Desk_Tower_01 = Desk[1];
        Tower_Level_Desk_1 = Player_Profile.GetComponent<Player_Status>().Get_Tower_Level(Player_Desk_Tower_01);
        Player_Desk_Tower_02 = Desk[2];
        Tower_Level_Desk_2 = Player_Profile.GetComponent<Player_Status>().Get_Tower_Level(Player_Desk_Tower_02);
        Player_Desk_Tower_03 = Desk[3];
        Tower_Level_Desk_3 = Player_Profile.GetComponent<Player_Status>().Get_Tower_Level(Player_Desk_Tower_03);
        Player_Desk_Tower_04 = Desk[4];
        Tower_Level_Desk_4 = Player_Profile.GetComponent<Player_Status>().Get_Tower_Level(Player_Desk_Tower_04);
        Player_Desk_Tower_05 = Desk[5];
        Tower_Level_Desk_5 = Player_Profile.GetComponent<Player_Status>().Get_Tower_Level(Player_Desk_Tower_05);
    }

    // Start is called before the first frame update
    public void Tower_Controller_Setup_Start()
    {
        Tower_Object = Resources.Load("Tower_Object") as GameObject;

        if (!isServer)
        {
            Debug.Log("!isServer " + !isServer);
            NetworkClient.RegisterPrefab(Tower_Object);
            Debug.Log("Tower_Object " + Tower_Object);
        }

        Tower_Slot_Array = new GameObject[] {null,
            Tower_Slot_01,Tower_Slot_02,Tower_Slot_03,Tower_Slot_04,Tower_Slot_05,Tower_Slot_06,Tower_Slot_07,Tower_Slot_08,
            Tower_Slot_09,Tower_Slot_10,Tower_Slot_11,Tower_Slot_12,Tower_Slot_13,Tower_Slot_14,Tower_Slot_15,Tower_Slot_16,
            Tower_Slot_17,Tower_Slot_18,Tower_Slot_19,Tower_Slot_20,Tower_Slot_21,Tower_Slot_22,Tower_Slot_23,Tower_Slot_24};

        Tower_Slot_Class_Array = new Tower_Slot_Class[Tower_Slot_QTY + 1];
        for (int i = 1; i < Tower_Slot_QTY + 1; i++)
        {
            Tower_Slot_Class temp_Class = new Tower_Slot_Class(null, 0, 0, 0);
            Set_Slot_Class(i, temp_Class);
            Tower_Slot_Class_Array[i] = temp_Class;
        }
        Reset_All_Group_Empty();
        Group = new Group_Bouns[] { null, Group_01, Group_02, Group_03, Group_04, Group_05, Group_06, Group_07, Group_08, Group_09 };

        //GroupSlot_01 = Assign_Tower_Slot_Number_To_Group_Slot(1);
        //GroupSlot_02 = Assign_Tower_Slot_Number_To_Group_Slot(2);
        //GroupSlot_03 = Assign_Tower_Slot_Number_To_Group_Slot(3);
        //GroupSlot_04 = Assign_Tower_Slot_Number_To_Group_Slot(4);
        //GroupSlot_05 = Assign_Tower_Slot_Number_To_Group_Slot(5);
        //GroupSlot_06 = Assign_Tower_Slot_Number_To_Group_Slot(6);
        //GroupSlot_07 = Assign_Tower_Slot_Number_To_Group_Slot(7);
        //GroupSlot_08 = Assign_Tower_Slot_Number_To_Group_Slot(8);
        //GroupSlot_09 = Assign_Tower_Slot_Number_To_Group_Slot(9);

        int[] Assign_Tower_Slot_Number_To_Group_Slot(int Group_Number)
        {
            int number = 1;
            int[] Group_Slot = new int[25];
            for (int i = 1; i < Group_Slot.Length; i++)
            {
                GameObject Tower_Slot = Tower_Slot_Array[i];
                if (Tower_Slot != null)
                {
                    int m_group_number = Tower_Slot_Array[i].GetComponent<Tower_Slot>().Group_Number;
                    if (Group_Number == m_group_number)
                    {
                        Group_Slot[number] = Tower_Slot_Array[i].GetComponent<Tower_Slot>().Tower_Slot_Number;
                        number++;
                    }
                }
            }
            return Group_Slot;
        }
    }

    void Reset_All_Group_Empty()
    {
        Group_01 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_02 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_03 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_04 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_05 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_06 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_07 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_08 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
        Group_09 = new Group_Bouns(0, 0, 0, 0, 0, 0, 0);
    }

    void Set_Slot_Class(int number, Tower_Slot_Class Class)
    {
        switch (number)
        {
            case (1):
                Slot_01 = Class;
                break;
            case (2):
                Slot_02 = Class;
                break;
            case (3):
                Slot_03 = Class;
                break;
            case (4):
                Slot_04 = Class;
                break;
            case (5):
                Slot_05 = Class;
                break;
            case (6):
                Slot_06 = Class;
                break;
            case (7):
                Slot_07 = Class;
                break;
            case (8):
                Slot_08 = Class;
                break;
            case (9):
                Slot_09 = Class;
                break;
            case (10):
                Slot_10 = Class;
                break;
            case (11):
                Slot_11 = Class;
                break;
            case (12):
                Slot_12 = Class;
                break;
            case (13):
                Slot_13 = Class;
                break;
            case (14):
                Slot_14 = Class;
                break;
            case (15):
                Slot_15 = Class;
                break;
            case (16):
                Slot_16 = Class;
                break;
            case (17):
                Slot_17 = Class;
                break;
            case (18):
                Slot_18 = Class;
                break;
            case (19):
                Slot_19 = Class;
                break;
            case (20):
                Slot_20 = Class;
                break;
            case (21):
                Slot_21 = Class;
                break;
            case (22):
                Slot_22 = Class;
                break;
            case (23):
                Slot_23 = Class;
                break;
            case (24):
                Slot_24 = Class;
                break;
        }
    }

    public void Create_Tower(GameObject Player)
    {
        Tower_Number++;
        int Gold = GameMaster.GetComponent<GameMaster>().Get_Gold(Player_Number);
        int Player_Tower_Gold = GameMaster.GetComponent<GameMaster>().Get_Player_Tower_Gold(Player_Number);
        if (Player_Tower_Gold > Gold)
            return;

        int Random_Number = Random.Range(1, 6);
        int Tower_Type = Get_Random_Tower(Random_Number);
        int Tower_Level = Get_Tower_Level(Random_Number);
        int Empty_Tower_Slot = Random_Get_Empty_Tower_Slot();

        if (Empty_Tower_Slot == 0)
            return;

        GameMaster.GetComponent<GameMaster>().Create_Tower_Set_Gold(Player_Number);
        // Slot_Number;
        // Type;
        // Combine_Up_Point;

        Vector3 Create_Tower_POS = Tower_Slot_Array[Empty_Tower_Slot].transform.position;
        GameObject m_Tower = null;
        bool Get_Tower_From_Pool = false;
        if (Tower.Count > 0)
        {
            m_Tower = Get_Tower_Form_Pool();
            Get_Tower_From_Pool = true;
            m_Tower.transform.position = Create_Tower_POS;
            //m_Tower.GetComponent<Tower>().Pool_Active_Tower();
        }

        if (!Get_Tower_From_Pool)
        {
            m_Tower = Instantiate(Tower_Object, Create_Tower_POS, Quaternion.identity);
            NetworkServer.Spawn(m_Tower);
        }

        Tower_Slot_Class temp_Class = new Tower_Slot_Class(m_Tower, Empty_Tower_Slot, Tower_Type, 1);
        Tower_Slot_Class_Array[Empty_Tower_Slot] = temp_Class;
        Set_Slot_Class(Empty_Tower_Slot, temp_Class);

        int Group_Number = Tower_Slot_Array[Empty_Tower_Slot].GetComponent<Tower_Slot>().Group_Number;
        float Group_Critical_Rate = Group[Group_Number].Critical;

        //Debug.Log("Core || " + Core);
        m_Tower.GetComponent<Tower>().GM = GameMaster;
        m_Tower.GetComponent<Tower>().Core = Core;
        m_Tower.GetComponent<Tower>().Tower_Controller = gameObject;
        m_Tower.GetComponent<Tower>().Map_Manager = Map_Manager;
        m_Tower.GetComponent<Tower>().Update_Tower_Info(Player_Number, Empty_Tower_Slot, Tower_Type, 1, Tower_Level, Player_Profile,
            Group_Critical_Rate, 0, Tower_Number, Create_Tower_POS);
        Set_Tower_Bouns(Empty_Tower_Slot);
    }

    int Get_Random_Tower(int Desk_Number)
    {
        switch (Desk_Number)
        {
            case (1): return Player_Desk_Tower_01;
            case (2): return Player_Desk_Tower_02;
            case (3): return Player_Desk_Tower_03;
            case (4): return Player_Desk_Tower_04;
            case (5): return Player_Desk_Tower_05;
        }
        return 0;
    }

    int Get_Tower_Level(int Desk_Number)
    {
        switch (Desk_Number)
        {
            case (1): return Tower_Level_Desk_1;
            case (2): return Tower_Level_Desk_2;
            case (3): return Tower_Level_Desk_3;
            case (4): return Tower_Level_Desk_4;
            case (5): return Tower_Level_Desk_5;
        }
        return 0;
    }

    int Random_Get_Empty_Tower_Slot()
    {
        int Empty_Slot_number = 0;
        int number = 1;
        int[] temp_Empty_Tower_Slot = new int[Tower_Slot_QTY + 1];
        for (int i = 1; i < Tower_Slot_QTY + 1; i++)
        {
            if (Tower_Slot_Class_Array[i].Type == 0)
            {
                temp_Empty_Tower_Slot[number] = i;
                number++;
            }
        }
        Empty_Slot_number = temp_Empty_Tower_Slot[Random.Range(1, number)];
        return Empty_Slot_number;
    }

    public void Check_Combine_Tower(GameObject Drag_Tower, GameObject Target_Tower)
    {
        NetworkConnection conn = Player_Profile.GetComponent<NetworkIdentity>().connectionToClient;
        Target_Tower.GetComponent<Tower>().Reset_All_Tower_Material(conn);
        if (combining)
            return;
        combining = true;

        bool Allow_Combine = false, Tower_isNotLock = false, Same_Player = false, Same_Type = false, Same_Combine_Point = false;

        bool Drag_Tower_Lock = Drag_Tower.GetComponent<Tower>().Lock;
        bool Target_Tower_Lock = Target_Tower.GetComponent<Tower>().Lock;

        int Drag_Tower_Player_Number = Get_Tower_Number(Drag_Tower, "Player_Number");
        int Target_Tower_Player_Number = Get_Tower_Number(Target_Tower, "Player_Number");
        int Drag_Tower_Type_Number = Get_Tower_Number(Drag_Tower, "Type_Number");
        int Target_Tower_Type_Number = Get_Tower_Number(Target_Tower, "Type_Number");
        int Drag_Tower_Combine_Up_Point_Number = Get_Tower_Number(Drag_Tower, "Combine_Up_Point");
        int Target_Tower_Combine_Up_Point_Number = Get_Tower_Number(Target_Tower, "Combine_Up_Point");

        if (!Drag_Tower_Lock && !Target_Tower_Lock)
            Tower_isNotLock = true;
        if (Drag_Tower_Player_Number == Target_Tower_Player_Number)
            Same_Player = true;
        if (Drag_Tower_Type_Number == Target_Tower_Type_Number)
            Same_Type = true;
        if (Drag_Tower_Combine_Up_Point_Number == Target_Tower_Combine_Up_Point_Number)
            Same_Combine_Point = true;
        if (Drag_Tower_Type_Number == 611 || Target_Tower_Combine_Up_Point_Number == 611) // Combine_Tower
            Same_Type = true;

        if (Tower_isNotLock && Same_Player && Same_Type && Same_Combine_Point)
            Allow_Combine = true;

        if (Allow_Combine)
        {
            Combine_Tower(Drag_Tower, Target_Tower);
        }
        if (!Allow_Combine)
        {
            combining = false;
            Player_Profile.GetComponent<Player_Network>().Not_Allow_Combine_Tower(Drag_Tower, Target_Tower);
        }
    }

    public void Combine_Tower(GameObject Drag_Tower, GameObject Target_Tower)
    {
        Tower_Number++;
        // Set_New_Tower_On_Target_Slot;
        int Target_Tower_Slot_Number = Get_Tower_Number(Target_Tower, "Slot_Number");
        int New_Combine_Point = Get_Tower_Number(Target_Tower, "Combine_Up_Point") + 1;
        int Random_Number = Random.Range(1, 6);
        int Tower_Type = Get_Random_Tower(Random_Number);
        int Tower_Level = Get_Tower_Level(Random_Number);

        Tower_Slot_Class temp_Target_Class = new Tower_Slot_Class(Target_Tower, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point);

        Tower_Slot_Class_Array[Target_Tower_Slot_Number] = temp_Target_Class;
        Set_Slot_Class(Target_Tower_Slot_Number, temp_Target_Class);

        // Clear_Drag_Tower
        int Drag_Tower_Slot_Number = 0;
        Drag_Tower_Slot_Number = Get_Tower_Number(Drag_Tower, "Slot_Number");
        Tower_Slot_Class temp_Class = new Tower_Slot_Class(null, Drag_Tower_Slot_Number, 0, 0);
        Tower_Slot_Class_Array[Drag_Tower_Slot_Number] = temp_Class;
        Set_Slot_Class(Drag_Tower_Slot_Number, temp_Class);
        Drag_Tower.GetComponent<Tower>().Reset_Tower_To_Pool();
        Drag_Tower.GetComponent<Tower>().Server_Tower_Set_InVisible();
        Tower.Add(Drag_Tower);

        int Drag_Tower_Type = Tower_Slot_Class_Array[Drag_Tower_Slot_Number].Type;

        Target_Tower.GetComponent<Tower>().Setup_Finish = false;

        int Group_Number = Tower_Slot_Array[Target_Tower_Slot_Number].GetComponent<Tower_Slot>().Group_Number;
        float Group_Critical_Rate = Group[Group_Number].Critical;

        Vector3 Create_Tower_POS = Tower_Slot_Array[Target_Tower_Slot_Number].transform.position;

        Target_Tower.GetComponent<Tower>().Update_Tower_Info(Player_Number, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point,
            Tower_Level, Player_Profile, Group_Critical_Rate, Drag_Tower_Slot_Number, Tower_Number, Create_Tower_POS);
        Set_Tower_Bouns(Target_Tower_Slot_Number);
        Set_Tower_Bouns(Drag_Tower_Slot_Number); // for update neighbor any disctoune effect . eg : Chain
        GameMaster.GetComponent<GameMaster>().Set_Highest_Combine_Point((short)Player_Number, (short)New_Combine_Point);
        combining = false;
    }

    int Get_Tower_Number(GameObject Tower, string Type)
    {
        int Tower_Number = 0;
        if (Type == "Player_Number")
        {
            Tower_Number = Tower.GetComponent<Tower>().Player_Number;
        }

        if (Type == "Slot_Number")
        {
            Tower_Number = Tower.GetComponent<Tower>().Slot_Number;
        }

        if (Type == "Type_Number")
        {
            Tower_Number = Tower.GetComponent<Tower>().Type_Number;
        }

        if (Type == "Combine_Up_Point")
        {
            Tower_Number = Tower.GetComponent<Tower>().Combine_Up_Point;
        }
        return Tower_Number;
    }

    void Set_Tower_Bouns(int Slot_Number)
    {
        //601 Chain
        //604 Group EXP
        //605 Group Speed
        //701 Full Screen Exp
        //702 Full Screen Speed
        //718 Lucky

        //Reset_All_Group_Empty();

        int Type = Tower_Slot_Class_Array[Slot_Number].Type;
        int Combine_Up_Point = Tower_Slot_Class_Array[Slot_Number].Combine_Up_Point;
        int Group_Number = Tower_Slot_Array[Slot_Number].GetComponent<Tower_Slot>().Group_Number;

        if (Type == 0)
        {

        }
        if (Type != 601)
        {
            Reset_Chain(Slot_Number);
        }
        if (Type == 601) //Chain //_chain
        {
            Set_Chain(Slot_Number);
        }
        Assign_All_Tower_Slot_Bouns();
        Check_and_Set_Full_Screen(701); //Full_Screen_Exp
        Check_and_Set_Full_Screen(702); //Full_Screen_Speed
        Update_All_Tower_Status();
    }

    void Update_All_Tower_Status()
    {
        for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
        {
            GameObject Tower = Tower_Slot_Class_Array[i].Tower;
            if (Tower != null)
                Tower.GetComponent<Tower>().Update_Tower_Status();
        }
    }

    void Assign_All_Tower_Slot_Bouns()
    {
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_01);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_02);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_03);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_04);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_05);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_06);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_07);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_08);
        Assign_Group_Info_To_All_Tower_Slot(GroupSlot_09);
    }

    #region Chain

    void Reset_Chain(int Slot_Number)
    {
        Tower_Slot tower_Slot = Tower_Slot_Array[Slot_Number].GetComponent<Tower_Slot>();
        Check_Obj_And_Deactive(tower_Slot.Chain_Effect_Top);
        Check_Obj_And_Deactive(tower_Slot.Chain_Effect_Right);
        Check_Obj_And_Deactive(tower_Slot.Chain_Effect_Buttom);
        Check_Obj_And_Deactive(tower_Slot.Chain_Effect_Left);

        void Check_Obj_And_Deactive(GameObject _obj)
        {
            if (_obj == null)
                return;
            if (_obj.activeSelf)
                _obj.SetActive(false);
        }
    }

    public void Set_Chain(int Slot_Number) // Setup_Chain
    {
        int chain_number = 0;
        int[] Checked_Chain_Slot = new int[25];

        Chain(Slot_Number);

        void Chain(int slot_number)
        {
            bool Tower_in_Array_Check = Check_Chain_Tower_in_Array(slot_number);
            if (!Tower_in_Array_Check)
                Set_Chain_Tower_To_Array(slot_number);
        }

        void Set_Chain_Tower_To_Array(int slot_number)
        {
            Checked_Chain_Slot[chain_number] = slot_number;
            GameObject Tower = Tower_Slot_Class_Array[slot_number].Tower;
            if (Tower != null)
            {
                chain_number++;
                Tower.GetComponent<Tower>().Chain_Connect_Number = chain_number;
                Tower_Slot Tower_Slot = Tower_Slot_Array[slot_number].GetComponent<Tower_Slot>();
                Tower.GetComponent<Tower>().Chain_Weapon_Point_Top = Tower_Slot.Chain_Effect_Top;
                Tower.GetComponent<Tower>().Chain_Weapon_Point_Right = Tower_Slot.Chain_Effect_Right;
                Tower.GetComponent<Tower>().Chain_Weapon_Point_Buttom = Tower_Slot.Chain_Effect_Buttom;
                Tower.GetComponent<Tower>().Chain_Weapon_Point_Left = Tower_Slot.Chain_Effect_Left;
            }
            Check_Neighbor_is_Chain(slot_number);
        }

        void Check_Neighbor_is_Chain(int slot_number)
        {
            Tower_Slot Tower_Slot = Tower_Slot_Array[slot_number].GetComponent<Tower_Slot>();
            Check_Obj_And_Deactive(Tower_Slot.Chain_Effect_Top);
            Check_Obj_And_Deactive(Tower_Slot.Chain_Effect_Right);
            Check_Obj_And_Deactive(Tower_Slot.Chain_Effect_Buttom);
            Check_Obj_And_Deactive(Tower_Slot.Chain_Effect_Left);

            void Check_Obj_And_Deactive(GameObject _obj)
            {
                if (_obj == null)
                    return;
                if (!_obj.activeSelf)
                    _obj.SetActive(true);
            }

            GameObject Neighbor_01 = Tower_Slot.Neighbor_Top;
            GameObject Neighbor_02 = Tower_Slot.Neighbor_Right;
            GameObject Neighbor_03 = Tower_Slot.Neighbor_Buttom;
            GameObject Neighbor_04 = Tower_Slot.Neighbor_Left;
            int Neighbor_01_Slot_Number = Get_Slot_Number(Neighbor_01);
            int Neighbor_02_Slot_Number = Get_Slot_Number(Neighbor_02);
            int Neighbor_03_Slot_Number = Get_Slot_Number(Neighbor_03);
            int Neighbor_04_Slot_Number = Get_Slot_Number(Neighbor_04);

            Tower_Slot_is_Chain(Neighbor_01_Slot_Number);
            Tower_Slot_is_Chain(Neighbor_02_Slot_Number);
            Tower_Slot_is_Chain(Neighbor_03_Slot_Number);
            Tower_Slot_is_Chain(Neighbor_04_Slot_Number);

            int Get_Slot_Number(GameObject Neighbor)
            {
                if (Neighbor != null)
                    return Neighbor.GetComponent<Tower_Slot>().Tower_Slot_Number;
                return 0;
            }

            void Tower_Slot_is_Chain(int neighbor_slot_number)
            {
                if (neighbor_slot_number == 0)
                    return;
                int neighbor_Type = Tower_Slot_Class_Array[neighbor_slot_number].Type;
                if (neighbor_Type == 601)
                    Chain(neighbor_slot_number);
            }
        }

        bool Check_Chain_Tower_in_Array(int Tower_Slot_Number)
        {
            for (int i = 0; i < Checked_Chain_Slot.Length; i++)
            {
                if (Checked_Chain_Slot[i] == Tower_Slot_Number)
                    return true;
            }
            return false;
        }

        for (int i = 0; i < Checked_Chain_Slot.Length; i++)
        {
            int slot_number = Checked_Chain_Slot[i];
            if (slot_number != 0)
            {
                if (Tower_Slot_Class_Array[slot_number].Tower != null)
                {
                    GameObject Tower_Obj = Tower_Slot_Class_Array[slot_number].Tower;
                    if (Tower_Obj != null)
                        Tower_Obj.GetComponent<Tower>().Chain_Connect_Number = chain_number;
                }
            }
        }
    }

    #endregion

    public void Set_Tower_Counter(int Type, GameObject Enemy) // 1:602 Counter Attack , 2:603 Counter Dead 3:706 Attacker_Attack_Counter_To_Bouns_Damage
    {
        for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
        {
            // 1 Attack , 2 Dead , 3 Attacker Attack
            GameObject Tower = Tower_Slot_Class_Array[i].Tower;
            if (Tower != null)
            {
                int Tower_Type_Number = Tower.GetComponent<Tower>().Type_Number;
                if (Tower_Type_Number == 602 && Type == 1)
                    Tower.GetComponent<Tower>().Counter(1, Enemy);
                if (Tower_Type_Number == 603 && Type == 2)
                    Tower.GetComponent<Tower>().Counter(2, Enemy);
                if (Tower_Type_Number == 706 && Type == 3)
                    Tower.GetComponent<Tower>().Counter(3, Enemy);
                if (Tower_Type_Number == 709 && Type == 2)
                    Tower.GetComponent<Tower>().Counter(2, Enemy);
                if (Tower_Type_Number == 710 && Type == 2)
                    Tower.GetComponent<Tower>().Counter(2, Enemy);
                if (Tower_Type_Number == 713 && Type == 1)
                    Tower.GetComponent<Tower>().Counter(1, Enemy);
                if (Tower_Type_Number == 714 && Type == 2)
                    Tower.GetComponent<Tower>().Counter(2, Enemy);
                if (Tower_Type_Number == 716 && Type == 2)
                    Tower.GetComponent<Tower>().Counter(2, Enemy);
            }
        }
    }

    public void Check_and_Set_Full_Screen(int Type)
    {
        float old_value = 0, highest_value = 0;
        for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
        {
            //GameObject tower, int slot_number, int type, int combine_up_point
            GameObject Tower = Tower_Slot_Class_Array[i].Tower;
            if (Tower != null)
            {
                int Type_Number = Tower.GetComponent<Tower>().Type_Number;
                if (Type_Number == Type)
                {
                    old_value = Calculate_Tower_Bouns(Type_Number, i);
                    if (old_value > highest_value)
                        highest_value = old_value;
                }
            }
        }

        // Set Highest value to Tower
        Set_All_Tower_Value(Type);

        void Set_All_Tower_Value(int Type_Number)
        {
            for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
            {
                GameObject tower = Tower_Slot_Class_Array[i].Tower;
                if (tower != null)
                {
                    if (Type_Number == 701)
                    {
                        if (highest_value > tower.GetComponent<Tower>().EXP_Bouns)
                            tower.GetComponent<Tower>().EXP_Bouns = highest_value;
                    }
                    if (Type_Number == 702)
                    {
                        if (highest_value > tower.GetComponent<Tower>().Speed_Bouns)
                            tower.GetComponent<Tower>().Speed_Bouns = highest_value;
                    }
                }
            }
        }
    }

    public GameObject Get_Non_Same_Type_Tower(int Tower_Type)
    {
        GameObject Tower = null;
        GameObject[] Tower_Array = Get_All_Non_Same_Type_Tower(Tower_Type);
        if (Tower_Array.Length >= 1)
        {
            int Random_Number = Random.Range(0, Tower_Array.Length);
            Tower = Tower_Array[Random_Number];
        }
        return Tower;
    }

    GameObject[] Get_All_Non_Same_Type_Tower(int Tower_Type)
    {
        GameObject[] Temp_Array = new GameObject[Tower_Slot_Class_Array.Length];
        int number = 0;

        for (int i = 0; i < Tower_Slot_Class_Array.Length; i++)
        {
            if (Tower_Slot_Class_Array[i] != null)
            {
                GameObject Tower = Tower_Slot_Class_Array[i].Tower;
                int m_tower_Type = Tower_Slot_Class_Array[i].Type;
                if (Tower != null && m_tower_Type != Tower_Type)
                {
                    Temp_Array[number] = Tower;
                    number++;
                }
            }
        }
        GameObject[] Tower_Array = new GameObject[number];
        for (int i = 0; i < Tower_Array.Length; i++)
        {
            GameObject tower = Temp_Array[i];
            if (tower != null)
                Tower_Array[i] = tower;
        }
        return Tower_Array;
    }

    void Assign_Group_Info_To_All_Tower_Slot(int[] Group_Array)
    {
        // 604 Group Exp , 605 Group Speed , 613 Group Basic Attack , 
        //701 Full Screen Exp , 702 Full Screen Speed , 718 Group Lucky , 720 Group_Critical , 721 Group_Critical_Damage
        bool Allow_Group_Exp = false, Allow_Group_Speed = false, Allow_Group_Basic_Attack = false, Allow_Group_Lucky = false,
            Allow_Group_Critical = false, Allow_Group_Critical_Damage = false;
        float Group_Exp = 0, Group_Speed = 0, Group_Basic_Attack = 0, Group_Lucky = 0, Group_Critical = 0, Group_Critical_Damage = 0;

        Check_Any_Group_Type_Tower_In_Group(604, out Allow_Group_Exp, out Group_Exp);
        Check_Any_Group_Type_Tower_In_Group(605, out Allow_Group_Speed, out Group_Speed);
        Check_Any_Group_Type_Tower_In_Group(613, out Allow_Group_Basic_Attack, out Group_Basic_Attack);
        Check_Any_Group_Type_Tower_In_Group(718, out Allow_Group_Lucky, out Group_Lucky);
        Check_Any_Group_Type_Tower_In_Group(720, out Allow_Group_Critical, out Group_Critical);
        Check_Any_Group_Type_Tower_In_Group(721, out Allow_Group_Critical_Damage, out Group_Critical_Damage);

        Assign_Value_To_Tower();

        void Check_Any_Group_Type_Tower_In_Group(int Type, out bool In_Group, out float highest_value)
        {
            float old_value = 0;
            highest_value = 0;
            In_Group = false;
            for (int i = 1; i < Group_Array.Length; i++)
            {
                int Slot_Number = Group_Array[i];
                if (Slot_Number != 0)
                {
                    GameObject Tower = Tower_Slot_Class_Array[Slot_Number].Tower;
                    if (Tower != null)
                    {
                        int Type_Number = Tower.GetComponent<Tower>().Type_Number;
                        if (Type_Number == Type)
                        {
                            In_Group = true;
                            old_value = Calculate_Tower_Bouns(Type_Number, Slot_Number);
                            if (old_value > highest_value)
                                highest_value = old_value;
                        }
                    }
                }
            }
        }

        void Assign_Value_To_Tower()
        {
            for (int i = 1; i < Group_Array.Length; i++)
            {
                int Slot_Number = Group_Array[i];
                if (Slot_Number != 0)
                {
                    GameObject Tower = Tower_Slot_Class_Array[Slot_Number].Tower;
                    if (Tower != null)
                    {
                        Tower.GetComponent<Tower>().Set_Tower_Bouns(Group_Exp, Group_Speed, Group_Basic_Attack, Group_Lucky,
                            Group_Critical, Group_Critical_Damage);
                    }
                }
            }
        }
    }

    float Calculate_Tower_Bouns(int type, int slot_number)
    {
        int combine_up_point = 0;
        GameObject m_Tower = Tower_Slot_Class_Array[slot_number].Tower;

        if (m_Tower != null)
            combine_up_point = m_Tower.GetComponent<Tower>().Combine_Up_Point;

        int Power_Up_Point = 0;
        if (Player_Desk_Tower_01 == type)
            Power_Up_Point = Power_Up_Desk_1;

        if (Player_Desk_Tower_02 == type)
            Power_Up_Point = Power_Up_Desk_2;

        if (Player_Desk_Tower_03 == type)
            Power_Up_Point = Power_Up_Desk_3;

        if (Player_Desk_Tower_04 == type)
            Power_Up_Point = Power_Up_Desk_4;

        if (Player_Desk_Tower_05 == type)
            Power_Up_Point = Power_Up_Desk_5;

        float Basic_Value_01 = m_Tower.GetComponent<Tower_Info>().Tower_Basic_Info(type).Special_Rate_1;
        float Combine_Value_01 = m_Tower.GetComponent<Tower_Info>().Tower_Comine_Up_Detail(type).Special_Rate_1;
        float Level_Value_01 = m_Tower.GetComponent<Tower_Info>().Tower_Level_Up_Detail(type).Special_Rate_1;

        float Basic_Value = Basic_Value_01;
        float Combine_Value = Combine_Value_01 * combine_up_point;
        float Level_Value = Level_Value_01 * Power_Up_Point;
        float Total_Value = Basic_Value + Combine_Value + Level_Value;
        float return_Value = Total_Value;
        return return_Value;
    }

    public void Set_Local_Tower(int Slot, GameObject Tower)
    {
        Local_Tower[Slot] = Tower;
    }

    #region Tower_Level or Desk

    bool Check_Desk_Allow_Level_Up(int Desk_Number)
    {
        int[] Desk_Level = new int[] { 0, Power_Up_Desk_1, Power_Up_Desk_2, Power_Up_Desk_3, Power_Up_Desk_4, Power_Up_Desk_5 };
        float[] Desk_Level_Exp = new float[] { 0, Desk_01_Exp, Desk_02_Exp, Desk_03_Exp, Desk_04_Exp, Desk_05_Exp };
        int Level = Desk_Level[Desk_Number];
        float Exp = Desk_Level_Exp[Desk_Number];
        int Require_Exp = Get_Tower_Power_Up_Require_Exp(Level);
        bool Allow_Level_Up = false;
        if (Exp >= (int)Require_Exp)
            Allow_Level_Up = true;
        return Allow_Level_Up;
    }

    public int Get_Tower_Power_Up_Require_Exp(int Level)
    {
        int[] Tower_Exp = new int[] { 0, 5, 10, 20, 40, 80, 160, 320, 640, 1280, 2560, 5120, 10240, 20480, 40960, 99999 };
        return Tower_Exp[Level];
    }

    public int Get_Tower_Power_Up_Require_Gold(int Level)
    {
        int[] Tower_Exp = new int[] { 0, 100, 200, 400, 800, 1200, 1500, 2000, 3000, 4000, 5000, 7500, 10000, 12000, 15000, 20000 };
        return Tower_Exp[Level];
    }

    public void Send_Tower_Level_To_Local()
    {
        Player_Network player = Player_Profile.GetComponent<Player_Network>();
        player.Set_Player_Desk_Power_Up(Power_Up_Desk_1, Power_Up_Desk_2, Power_Up_Desk_3, Power_Up_Desk_4, Power_Up_Desk_5);
    }

    public void Send_Tower_Level_To_Local(GameObject Player_Obj)
    {
        Debug.Log("Send_Tower_Level_To_Local || " + Power_Up_Desk_1 + " || " + Power_Up_Desk_2 + " || " + Power_Up_Desk_3 + " || " + Power_Up_Desk_4 + " || " + Power_Up_Desk_5);
        Player_Network player = Player_Obj.GetComponent<Player_Network>();
        player.Set_Player_Desk_Power_Up(Power_Up_Desk_1, Power_Up_Desk_2, Power_Up_Desk_3, Power_Up_Desk_4, Power_Up_Desk_5);
    }

    public GameObject Get_Neighbor_Tower_Same_Combine_Point(int input_Slot_Number, int input_Combine_Point)
    {
        int Number = 0;
        GameObject[] Array = new GameObject[4];
        GameObject Target_Tower = null;
        GameObject Tower = Tower_Slot_Class_Array[input_Slot_Number].Tower;
        Tower_Slot Tower_Slot = Tower_Slot_Array[input_Slot_Number].GetComponent<Tower_Slot>();

        GameObject Neighbor_01 = Tower_Slot.Neighbor_Top;
        GameObject Neighbor_02 = Tower_Slot.Neighbor_Right;
        GameObject Neighbor_03 = Tower_Slot.Neighbor_Buttom;
        GameObject Neighbor_04 = Tower_Slot.Neighbor_Left;
        int Neighbor_01_Slot_Number = Get_Slot_Number(Neighbor_01);
        int Neighbor_02_Slot_Number = Get_Slot_Number(Neighbor_02);
        int Neighbor_03_Slot_Number = Get_Slot_Number(Neighbor_03);
        int Neighbor_04_Slot_Number = Get_Slot_Number(Neighbor_04);

        int Get_Slot_Number(GameObject Neighbor)
        {
            if (Neighbor != null)
                return Neighbor.GetComponent<Tower_Slot>().Tower_Slot_Number;
            return 0;
        }

        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_01_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_02_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_03_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_04_Slot_Number);

        if (Number == 0)
            return null;

        int Random_Number = Random.Range(0, Number);
        GameObject temp_Tower = Array[Random_Number];
        if (temp_Tower != null)
        {
            temp_Tower.GetComponent<Tower>().Lock = true;
            Target_Tower = temp_Tower;
        }

        void Set_Same_Combine_Point_Neighbor_To_Array(int Neighbor_Slot_Number)
        {
            if (Tower_Slot_Class_Array[Neighbor_Slot_Number] != null)
            {
                GameObject m_Tower = Tower_Slot_Class_Array[Neighbor_Slot_Number].Tower;
                if (m_Tower != null)
                {
                    bool Tower_Lock = m_Tower.GetComponent<Tower>().Lock;
                    int Combine_Up_Point = m_Tower.GetComponent<Tower>().Combine_Up_Point;
                    if (Combine_Up_Point == input_Combine_Point && !Tower_Lock)
                    {
                        Array[Number] = m_Tower;
                        Number++;
                    }
                }
            }
        }
        return Target_Tower;
    }

    public GameObject Get_Random_Neighbor_Tower(int input_Slot_Number)
    {
        int Number = 0;
        GameObject[] Array = new GameObject[4];
        GameObject Target_Tower = null;
        GameObject Tower = Tower_Slot_Class_Array[input_Slot_Number].Tower;
        Tower_Slot Tower_Slot = Tower_Slot_Array[input_Slot_Number].GetComponent<Tower_Slot>();

        GameObject Neighbor_01 = Tower_Slot.Neighbor_Top;
        GameObject Neighbor_02 = Tower_Slot.Neighbor_Right;
        GameObject Neighbor_03 = Tower_Slot.Neighbor_Buttom;
        GameObject Neighbor_04 = Tower_Slot.Neighbor_Left;
        int Neighbor_01_Slot_Number = Get_Slot_Number(Neighbor_01);
        int Neighbor_02_Slot_Number = Get_Slot_Number(Neighbor_02);
        int Neighbor_03_Slot_Number = Get_Slot_Number(Neighbor_03);
        int Neighbor_04_Slot_Number = Get_Slot_Number(Neighbor_04);

        int Get_Slot_Number(GameObject Neighbor)
        {
            if (Neighbor != null)
                return Neighbor.GetComponent<Tower_Slot>().Tower_Slot_Number;
            return 0;
        }

        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_01_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_02_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_03_Slot_Number);
        Set_Same_Combine_Point_Neighbor_To_Array(Neighbor_04_Slot_Number);

        if (Number == 0)
            return null;

        int Random_Number = Random.Range(0, Number);
        GameObject temp_Tower = Array[Random_Number];
        if (temp_Tower != null)
        {
            temp_Tower.GetComponent<Tower>().Lock = true;
            Target_Tower = temp_Tower;
        }

        void Set_Same_Combine_Point_Neighbor_To_Array(int Neighbor_Slot_Number)
        {
            if (Tower_Slot_Class_Array[Neighbor_Slot_Number] != null)
            {
                GameObject m_Tower = Tower_Slot_Class_Array[Neighbor_Slot_Number].Tower;
                if (m_Tower != null)
                {
                    bool Tower_Lock = m_Tower.GetComponent<Tower>().Lock;
                    if (!Tower_Lock)
                    {
                        Array[Number] = m_Tower;
                        Number++;
                    }
                }
            }
        }
        return Target_Tower;
    }

    public void Level_Up_Tower(GameObject Target_Tower)
    {
        Tower_Number++;
        //Debug.Log("Level_Up_Tower || " + Target_Tower + " || " + Target_Tower.GetComponent<Tower>().Slot_Number);
        // Set_New_Tower_On_Target_Slot;
        int Target_Tower_Slot_Number = Get_Tower_Number(Target_Tower, "Slot_Number");
        int New_Combine_Point = Get_Tower_Number(Target_Tower, "Combine_Up_Point") + 1;
        int Random_Number = Random.Range(1, 6);
        int Tower_Type = Get_Random_Tower(Random_Number);
        int Tower_Level = Get_Tower_Level(Random_Number);

        Tower_Slot_Class temp_Target_Class = new Tower_Slot_Class(Target_Tower, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point);

        Tower_Slot_Class_Array[Target_Tower_Slot_Number] = temp_Target_Class;
        Set_Slot_Class(Target_Tower_Slot_Number, temp_Target_Class);

        Target_Tower.GetComponent<Tower>().Setup_Finish = false;

        int Group_Number = Tower_Slot_Array[Target_Tower_Slot_Number].GetComponent<Tower_Slot>().Group_Number;
        float Group_Critical_Rate = Group[Group_Number].Critical;

        Vector3 Create_Tower_POS = Tower_Slot_Array[Target_Tower_Slot_Number].transform.position;

        Target_Tower.GetComponent<Tower>().Update_Tower_Info(Player_Number, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point,
            Tower_Level, Player_Profile, Group_Critical_Rate, 0, Tower_Number, Create_Tower_POS);
        Set_Tower_Bouns(Target_Tower_Slot_Number);
        GameMaster.GetComponent<GameMaster>().Set_Highest_Combine_Point((short)Player_Number, (short)New_Combine_Point);
    }

    public void Level_Down_Tower(GameObject Target_Tower)
    {
        Tower_Number++;
        //Debug.Log("Level_Down_Tower || " + Target_Tower + " || " + Target_Tower.GetComponent<Tower>().Slot_Number);
        // Set_New_Tower_On_Target_Slot;
        int Target_Tower_Slot_Number = Get_Tower_Number(Target_Tower, "Slot_Number");
        int New_Combine_Point = Get_Tower_Number(Target_Tower, "Combine_Up_Point") - 1;
        int Random_Number = Random.Range(1, 6);
        int Tower_Type = Get_Random_Tower(Random_Number);
        int Tower_Level = Get_Tower_Level(Random_Number);

        if (New_Combine_Point == 0)
        {
            // Clear_Drag_Tower
            Target_Tower.GetComponent<Tower>().Setup_Finish = false;
            Tower_Slot_Class temp_Class = new Tower_Slot_Class(null, Target_Tower_Slot_Number, 0, 0);
            Tower_Slot_Class_Array[Target_Tower_Slot_Number] = temp_Class;
            Set_Slot_Class(Target_Tower_Slot_Number, temp_Class);
            Target_Tower.GetComponent<Tower>().Reset_Tower_To_Pool();
            Target_Tower.GetComponent<Tower>().Server_Tower_Set_InVisible();
            Tower.Add(Target_Tower);
            return;
        }

        Tower_Slot_Class temp_Target_Class = new Tower_Slot_Class(Target_Tower, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point);

        Tower_Slot_Class_Array[Target_Tower_Slot_Number] = temp_Target_Class;
        Set_Slot_Class(Target_Tower_Slot_Number, temp_Target_Class);

        Target_Tower.GetComponent<Tower>().Setup_Finish = false;

        int Group_Number = Tower_Slot_Array[Target_Tower_Slot_Number].GetComponent<Tower_Slot>().Group_Number;
        float Group_Critical_Rate = Group[Group_Number].Critical;

        Vector3 Create_Tower_POS = Tower_Slot_Array[Target_Tower_Slot_Number].transform.position;

        Target_Tower.GetComponent<Tower>().Update_Tower_Info(Player_Number, Target_Tower_Slot_Number, Tower_Type, New_Combine_Point,
            Tower_Level, Player_Profile, Group_Critical_Rate, 0, Tower_Number, Create_Tower_POS);
        Set_Tower_Bouns(Target_Tower_Slot_Number);
    }

    public void Set_Desk_Exp(float Exp)
    {
        float Share_Exp = 0;
        int Tower_Desk_01 = 0, Tower_Desk_02 = 0, Tower_Desk_03 = 0, Tower_Desk_04 = 0, Tower_Desk_05 = 0, Total_Tower_On_Map = 0;
        float Exp_Bouns_1 = 0, Exp_Bouns_2 = 0, Exp_Bouns_3 = 0, Exp_Bouns_4 = 0, Exp_Bouns_5 = 0;
        for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
        {
            GameObject Tower = Tower_Slot_Class_Array[i].Tower;
            if (Tower != null)
            {
                int Tower_Type_Number = Tower.GetComponent<Tower>().Type_Number;
                if (Tower_Type_Number == Player_Desk_Tower_01)
                {
                    Tower_Desk_01++;
                    Exp_Bouns_1 += Tower.GetComponent<Tower>().EXP_Bouns;
                }
                if (Tower_Type_Number == Player_Desk_Tower_02)
                {
                    Tower_Desk_02++;
                    Exp_Bouns_2 += Tower.GetComponent<Tower>().EXP_Bouns;
                }
                if (Tower_Type_Number == Player_Desk_Tower_03)
                {
                    Tower_Desk_03++;
                    Exp_Bouns_3 += Tower.GetComponent<Tower>().EXP_Bouns;
                }
                if (Tower_Type_Number == Player_Desk_Tower_04)
                {
                    Tower_Desk_04++;
                    Exp_Bouns_4 += Tower.GetComponent<Tower>().EXP_Bouns;
                }
                if (Tower_Type_Number == Player_Desk_Tower_05)
                {
                    Tower_Desk_05++;
                    Exp_Bouns_5 += Tower.GetComponent<Tower>().EXP_Bouns;
                }
            }
        }

        Total_Tower_On_Map = Tower_Desk_01 + Tower_Desk_02 + Tower_Desk_03 + Tower_Desk_04 + Tower_Desk_05;
        Share_Exp = Exp / Total_Tower_On_Map;
        Desk_01_Exp += (Tower_Desk_01 * Share_Exp * (1 + (Exp_Bouns_1 / 100)));
        Desk_02_Exp += (Tower_Desk_02 * Share_Exp * (1 + (Exp_Bouns_2 / 100)));
        Desk_03_Exp += (Tower_Desk_03 * Share_Exp * (1 + (Exp_Bouns_3 / 100)));
        Desk_04_Exp += (Tower_Desk_04 * Share_Exp * (1 + (Exp_Bouns_4 / 100)));
        Desk_05_Exp += (Tower_Desk_05 * Share_Exp * (1 + (Exp_Bouns_5 / 100)));
    }

    public void Tower_Desk_Power_Up(GameObject Player, int Desk_Number)
    {
        Debug.Log("Tower_Desk_Level_Up || " + Desk_Number);
        int[] desk_lv_array = new int[] { 0, Power_Up_Desk_1, Power_Up_Desk_2, Power_Up_Desk_3, Power_Up_Desk_4, Power_Up_Desk_5 };
        float[] desk_Exp_Array = new float[] { 0, Desk_01_Exp, Desk_02_Exp, Desk_03_Exp, Desk_04_Exp, Desk_05_Exp };

        int Gold = GameMaster.GetComponent<GameMaster>().Get_Gold(Player_Number);
        int desk_Level = desk_lv_array[Desk_Number];
        int Require_Level_Up_Gold = Get_Tower_Power_Up_Require_Gold(desk_Level);

        bool Enough_Gold = false;
        bool Enough_Exp = false;
        float current_Exp = desk_Exp_Array[Desk_Number];
        int require_exp = Get_Tower_Power_Up_Require_Exp(desk_Level);

        if (Gold >= Require_Level_Up_Gold)
            Enough_Gold = true;
        if (current_Exp >= require_exp)
            Enough_Exp = true;

        if (!Enough_Gold || !Enough_Exp)
            return;

        if (Enough_Gold && current_Exp >= require_exp)
        {
            if (Desk_Number == 1)
                Power_Up_Desk_1 = Set_Tower_Power_Up(Power_Up_Desk_1);
            if (Desk_Number == 2)
                Power_Up_Desk_2 = Set_Tower_Power_Up(Power_Up_Desk_2);
            if (Desk_Number == 3)
                Power_Up_Desk_3 = Set_Tower_Power_Up(Power_Up_Desk_3);
            if (Desk_Number == 4)
                Power_Up_Desk_4 = Set_Tower_Power_Up(Power_Up_Desk_4);
            if (Desk_Number == 5)
                Power_Up_Desk_5 = Set_Tower_Power_Up(Power_Up_Desk_5);
        }
        GameMaster.GetComponent<GameMaster>().Subtract_Gold(Player_Number, Require_Level_Up_Gold);
        Send_Tower_Level_To_Local();
        Gold = GameMaster.GetComponent<GameMaster>().Get_Gold(Player_Number);
        GameMaster.GetComponent<GameMaster>().Set_Gold(Player_Number, Gold);
        int Set_Tower_Power_Up(int m_desk_Level)
        {
            m_desk_Level++;
            int[] Array = new int[] { 0, Player_Desk_Tower_01, Player_Desk_Tower_02, Player_Desk_Tower_03, Player_Desk_Tower_04, Player_Desk_Tower_05 };
            int Type_Number = Array[Desk_Number];

            for (int i = 1; i < Tower_Slot_Class_Array.Length; i++)
            {
                int Tower_Type = Tower_Slot_Class_Array[i].Type;
                if (Tower_Type == Type_Number)
                {
                    GameObject Tower = Tower_Slot_Class_Array[i].Tower;
                    Tower.GetComponent<Tower>().Power_Up_Point = m_desk_Level;
                    Tower.GetComponent<Tower>().Update_Tower_Status();
                }
            }
            return m_desk_Level;
        }
        Assign_All_Tower_Slot_Bouns();
        Check_and_Set_Full_Screen(701); //Full_Screen_Exp
        Check_and_Set_Full_Screen(702); //Full_Screen_Speed
        Update_All_Tower_Status();
    }

    #endregion

    public int Get_Tower_Power_Up(int Type_Number)
    {
        if (Type_Number == Player_Desk_Tower_01) return Power_Up_Desk_1;
        if (Type_Number == Player_Desk_Tower_02) return Power_Up_Desk_2;
        if (Type_Number == Player_Desk_Tower_03) return Power_Up_Desk_3;
        if (Type_Number == Player_Desk_Tower_04) return Power_Up_Desk_4;
        if (Type_Number == Player_Desk_Tower_05) return Power_Up_Desk_5;
        return 0;
    }

    public GameObject Get_Tower_Form_Pool()
    {
        GameObject tower = null;
        for (int i = 0; i < Tower.Count; i++)
        {
            if (Tower[i] != null)
            {
                tower = Tower[i];
                Tower.Remove(tower);
                return tower;
            }
        }
        return tower;
    }

    public void Add_Basic_Damage_Bouns(int Player_Number)
    {
        switch (Player_Number)
        {
            case (1):
                Basic_Damage_Bouns_Player_01++;
                break;
            case (2):
                Basic_Damage_Bouns_Player_02++;
                break;
            case (3):
                Basic_Damage_Bouns_Player_03++;
                break;
            case (4):
                Basic_Damage_Bouns_Player_04++;
                break;
        }
    }

    public float Get_Basic_Damage_Bouns(int Player_Number)
    {
        switch (Player_Number)
        {
            case (1): return Basic_Damage_Bouns_Player_01;
            case (2): return Basic_Damage_Bouns_Player_02;
            case (3): return Basic_Damage_Bouns_Player_03;
            case (4): return Basic_Damage_Bouns_Player_04;
        }
        return 0;
    }
}

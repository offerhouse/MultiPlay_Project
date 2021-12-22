using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Manager : MonoBehaviour
{
    public int Player_Number;
    public bool One_VS_One, Two_VS_Two, Two_Cooperation, Four_Cooperation;
    public GameObject Local_Player, Player_UI, Map_Manager;
    public GameObject Local_Player_01, Local_Player_02, Local_Player_03, Local_Player_04;
    public GameObject Player_01_Local_Map, Player_02_Local_Map, Player_03_Local_Map, Player_04_Local_Map;
    public GameObject Map_01, Map_02, Map_03, Map_04, Map_05, Map_06, Map_07, Map_08, Map_09, Map_10, Map_11, Map_12;
    public GameObject Map_Point_01, Map_Point_02, Map_Point_03, Map_Point_04;

    public GameObject Player_01_Core, Player_02_Core, Player_03_Core, Player_04_Core;
    public GameObject Player_01_Local_Camera, Player_02_Local_Camera, Player_03_Local_Camera, Player_04_Local_Camera;
    public GameObject Player_01_Light, Player_02_Light, Player_03_Light, Player_04_Light;
    public GameObject[] Player_01_Tower_Array = new GameObject[25];
    public GameObject[] Player_02_Tower_Array = new GameObject[25];
    public GameObject[] Player_03_Tower_Array = new GameObject[25];
    public GameObject[] Player_04_Tower_Array = new GameObject[25];

    public GameObject Enemy_01;
    public GameObject Enemy_02;

    public short Player1_Map_Type, Player2_Map_Type, Player3_Map_Type, Player4_Map_Type;

    public int Player_Gold;
    public int Desk_01_Level = 1, Desk_02_Level = 1, Desk_03_Level = 1, Desk_04_Level = 1, Desk_05_Level = 1;
    public float Desk_01_Exp, Desk_02_Exp, Desk_03_Exp, Desk_04_Exp, Desk_05_Exp;

    public int[] Tower_Exp = new int[] { 0, 5, 10, 20, 40, 80, 160, 320, 640, 1280, 2560, 5120, 10240, 20480, 40960, 99999 };
    public int[] Tower_Level_Up_Gold = new int[] { 0, 100, 200, 400, 800, 1200, 1500, 2000, 3000, 4000, 5000, 7500, 10000, 12000, 15000, 20000 };

    public GameObject Pool_Manager;
    //public List<GameObject> Bullet = new List<GameObject>();
    public List<GameObject> Fall_Explosion_Effect = new List<GameObject>();
    //public List<GameObject> Treasure = new List<GameObject>();
    //public List<GameObject> HP_Bar = new List<GameObject>();

    public GameObject End_Game_Canvas, End_Game_Loading_Bar, End_Game_Button;

    public void Instantiate_Local_Map()
    {
        GameObject Map = null;
        if (One_VS_One || Two_Cooperation)
        {
            Map = Get_Selected_Map_Type(Player1_Map_Type);
            Player_01_Local_Map = Instantiate(Map, Map_Point_01.transform.position, Quaternion.identity);
            Player_01_Core = Player_01_Local_Map.GetComponent<Object_Status>().Core;

            Map = Get_Selected_Map_Type(Player2_Map_Type);
            Player_02_Local_Map = Instantiate(Map, Map_Point_02.transform.position, Quaternion.identity);
            Player_02_Core = Player_02_Local_Map.GetComponent<Object_Status>().Core;
        }

        if (Two_VS_Two || Four_Cooperation)
        {
            Map = Get_Selected_Map_Type(Player1_Map_Type);
            Player_01_Local_Map = Instantiate(Map, Map_Point_01.transform.position, Quaternion.identity);
            Player_01_Core = Player_01_Local_Map.GetComponent<Object_Status>().Core;

            Map = Get_Selected_Map_Type(Player2_Map_Type);
            Player_02_Local_Map = Instantiate(Map, Map_Point_02.transform.position, Quaternion.identity);
            Player_02_Core = Player_02_Local_Map.GetComponent<Object_Status>().Core;

            Map = Get_Selected_Map_Type(Player3_Map_Type);
            Player_03_Local_Map = Instantiate(Map, Map_Point_03.transform.position, Quaternion.identity);
            Player_03_Core = Player_03_Local_Map.GetComponent<Object_Status>().Core;

            Map = Get_Selected_Map_Type(Player4_Map_Type);
            Player_04_Local_Map = Instantiate(Map, Map_Point_04.transform.position, Quaternion.identity);
            Player_04_Core = Player_04_Local_Map.GetComponent<Object_Status>().Core;
        }

        Map_Manager.GetComponent<Map_Manager>().One_VS_One = One_VS_One;
        Map_Manager.GetComponent<Map_Manager>().Two_Cooperation = Two_Cooperation;
        Map_Manager.GetComponent<Map_Manager>().Two_VS_Two = Two_VS_Two;
        Map_Manager.GetComponent<Map_Manager>().Four_Cooperation = Four_Cooperation;
    }

    GameObject Get_Local_Map(int player_Number)
    {
        GameObject[] Local_Map = new GameObject[] { null, Player_01_Local_Map, Player_02_Local_Map, Player_03_Local_Map, Player_04_Local_Map };
        return Local_Map[player_Number];
    }

    GameObject Get_Selected_Map_Type(int Type_Number)
    {
        GameObject[] Map_Type = new GameObject[] { null, Map_01, Map_02, Map_03, Map_04, Map_05, Map_06, Map_07, Map_08, Map_09, Map_10, Map_11, Map_12 };
        return Map_Type[Type_Number];
    }

    public void Start_Drag_Black_Cannot_Combine_Tower(GameObject Original_Tower, int Type, int Combine_Point, int Play_Number)
    {
        foreach (Transform child in transform)
        {
            GameObject Tower = child.gameObject;
            int Slot_Number = child.GetComponent<Tower>().Slot_Number;
            int type = child.GetComponent<Tower>().Type_Number;
            int combine_up_Point = child.GetComponent<Tower>().Combine_Up_Point;
            int player_number = child.GetComponent<Tower>().Player_Number;

            if (type != 0)
            {
                if (Original_Tower != Tower && (Type != type || Combine_Point != combine_up_Point || player_number != Play_Number))
                {
                    Tower.GetComponent<Tower>().Set_Ground_Material(true);
                    Tower.GetComponent<Tower>().Dim_Icon_and_Level();
                }
            }
        }
    }

    public void End_Drag_Reset_Tower(GameObject Original_Tower, int Type, int Combine_Point)
    {
        foreach (Transform child in transform)
        {
            GameObject Tower = child.gameObject;
            int type = child.GetComponent<Tower>().Type_Number;

            if (type != 0)
            {
                if (Tower != Original_Tower)
                {
                    Tower.GetComponent<Tower>().Set_Ground_Material(false);
                    Tower.GetComponent<Tower>().Reset_Icon_and_Level();
                }
            }
        }
    }

    public void Set_Camera(int Number, GameObject camera)
    {
        if (Number == 1)
            Player_01_Local_Camera = camera;
        if (Number == 2)
            Player_02_Local_Camera = camera;
        if (Number == 3)
            Player_03_Local_Camera = camera;
        if (Number == 4)
            Player_04_Local_Camera = camera;
    }

    public GameObject Get_Camera()
    {
        GameObject camera = null;
        if (Player_Number == 1)
            camera = Player_01_Local_Camera;
        if (Player_Number == 2)
            camera = Player_02_Local_Camera;
        if (Player_Number == 3)
            camera = Player_03_Local_Camera;
        if (Player_Number == 4)
            camera = Player_04_Local_Camera;
        return camera;
    }

    public GameObject Get_Camera(short player_number)
    {
        if (player_number == 1)
            return Player_01_Local_Camera;
        if (player_number == 2)
            return Player_02_Local_Camera;
        if (player_number == 3)
            return Player_03_Local_Camera;
        if (player_number == 4)
            return Player_04_Local_Camera;
        return null;
    }

    public void Set_Tower_Floor(short player_Number, short slot_Number, short Code)
    {
        GameObject Tower_Slot = Get_Tower_Slot_By_Player_Number_and_Slot_Number(player_Number, slot_Number);
        GameObject Tower_Floor = Tower_Slot.GetComponent<Tower_Slot>().Tower_Floor;
        if (Code == (short)Tower_Code.Active)
            Tower_Floor.SetActive(true);
        if (Code == (short)Tower_Code.Inactive)
            Tower_Floor.SetActive(false);
    }

    GameObject Get_Tower_Slot_By_Player_Number_and_Slot_Number(short player_Number, short Tower_Number)
    {
        switch (player_Number)
        {
            case (1): return Player_01_Local_Map.GetComponent<Object_Status>().Get_Tower_Slot_Array()[Tower_Number];
            case (2): return Player_02_Local_Map.GetComponent<Object_Status>().Get_Tower_Slot_Array()[Tower_Number];
            case (3): return Player_03_Local_Map.GetComponent<Object_Status>().Get_Tower_Slot_Array()[Tower_Number];
            case (4): return Player_04_Local_Map.GetComponent<Object_Status>().Get_Tower_Slot_Array()[Tower_Number];
        }
        return null;
    }

    public void Check_All_Chain(int player_Number) // Check all (type 601)chain tower Chain effect
    {
        GameObject Map = Get_Local_Map(player_Number);
        if (Map == null)
            return;
        GameObject[] Tower_Array = Get_Tower_Array(player_Number);
        GameObject[] Tower_Slot_Array = Map.GetComponent<Object_Status>().Get_Tower_Slot_Array();
        Tower_Slot m_tower_slot = null;
        for (int i = 0; i < Tower_Slot_Array.Length; i++)
        {
            int m_type_Number = 0;
            GameObject Tower_Obj = Tower_Array[i];
            GameObject Tower_Slot_Obj = Tower_Slot_Array[i];
            if (Tower_Slot_Obj != null)
                m_tower_slot = Tower_Slot_Obj.GetComponent<Tower_Slot>();

            if (Tower_Obj != null)
            {
                m_type_Number = Tower_Obj.GetComponent<Tower>().Type_Number;
                if (m_type_Number == 601)
                {
                    GameObject Neighbor_Top = m_tower_slot.Neighbor_Top;
                    GameObject Neighbor_Right = m_tower_slot.Neighbor_Right;
                    GameObject Neighbor_Buttom = m_tower_slot.Neighbor_Buttom;
                    GameObject Neighbor_Left = m_tower_slot.Neighbor_Left;

                    bool Top_Is_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Top);
                    bool Right_Is_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Right);
                    bool Buttom_Is_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Buttom);
                    bool Left_Is_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Left);

                    bool Top_Is_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Neighbor_Top, Tower_Array);
                    bool Right_Is_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Neighbor_Right, Tower_Array);
                    bool Buttom_Is_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Neighbor_Buttom, Tower_Array);
                    bool Left_Is_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Neighbor_Left, Tower_Array);

                    bool Active_Top_Chain = Check_Chain_and_Pillar(Top_Is_Road, Top_Is_Chain_Tower, true);
                    bool Active_Top_Pillar = Check_Chain_and_Pillar(Top_Is_Road, Top_Is_Chain_Tower, false);

                    bool Active_Right_Chain = Check_Chain_and_Pillar(Right_Is_Road, Right_Is_Chain_Tower, true);
                    bool Active_Right_Pillar = Check_Chain_and_Pillar(Right_Is_Road, Right_Is_Chain_Tower, false);

                    bool Active_Buttom_Chain = Check_Chain_and_Pillar(Buttom_Is_Road, Buttom_Is_Chain_Tower, true);
                    bool Active_Buttom_Pillar = Check_Chain_and_Pillar(Buttom_Is_Road, Buttom_Is_Chain_Tower, false);

                    bool Active_Left_Chain = Check_Chain_and_Pillar(Left_Is_Road, Left_Is_Chain_Tower, true);
                    bool Active_Left_Pillar = Check_Chain_and_Pillar(Left_Is_Road, Left_Is_Chain_Tower, false);

                    GameObject Chain_Weapon_Point_Top = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Top;
                    GameObject Chain_Weapon_Point_Right = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Right;
                    GameObject Chain_Weapon_Point_Buttom = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Buttom;
                    GameObject Chain_Weapon_Point_Left = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Left;

                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Top, Active_Top_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Top, Active_Top_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Top, Active_Top_Pillar);

                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Right, Active_Right_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Right, Active_Right_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Right, Active_Right_Pillar);

                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Buttom, Active_Buttom_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Buttom, Active_Buttom_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Buttom, Active_Buttom_Pillar);

                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Left, Active_Left_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Left, Active_Left_Chain);
                    Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Left, Active_Left_Pillar);

                    bool Check_Chain_and_Pillar(bool is_Road, bool is_Chain, bool check_tower)
                    {
                        bool Check = false;
                        if (check_tower && (is_Road || is_Chain))
                            Check = true; ;

                        if (!check_tower && is_Road && !is_Chain)
                            Check = true; ;

                        return Check;
                    }
                }

                if (m_type_Number != 601)
                {
                    Deactive_All_Chain_Effect();
                }
            }

            if (Tower_Slot_Obj != null && m_type_Number != 601)
                Deactive_All_Chain_Effect();

            if (Tower_Obj != null)
            {
                GameObject Slot_Top = m_tower_slot.Neighbor_Top;
                GameObject Slot_Right = m_tower_slot.Neighbor_Right;
                GameObject Slot_Buttom = m_tower_slot.Neighbor_Buttom;
                GameObject Slot_Left = m_tower_slot.Neighbor_Left;

                bool Top_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Top);
                bool Right_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Right);
                bool Buttom_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Buttom);
                bool Left_Road = Check_Road_Slot(m_tower_slot.Road_Slot_Left);

                bool Top_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Slot_Top, Tower_Array);
                bool Right_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Slot_Right, Tower_Array);
                bool Buttom_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Slot_Buttom, Tower_Array);
                bool Left_Chain_Tower = Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(601, Slot_Left, Tower_Array);
            }

            bool Check_Road_Slot(GameObject Road_Slot)
            {
                if (Road_Slot != null)
                    return true;
                return false;
            }

            void Deactive_All_Chain_Effect()
            {
                if (Tower_Obj != null)
                {
                    GameObject Chain_Weapon_Point_Top = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Top;
                    GameObject Chain_Weapon_Point_Right = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Right;
                    GameObject Chain_Weapon_Point_Buttom = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Buttom;
                    GameObject Chain_Weapon_Point_Left = Tower_Obj.GetComponent<Tower>().Chain_Weapon_Point_Left;
                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Top, false);
                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Right, false);
                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Buttom, false);
                    Active_or_Deactive_Chain_Effect(Chain_Weapon_Point_Left, false);
                }

                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Top, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Right, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Buttom, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Pillar_Left, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Top, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Right, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Buttom, false);
                Active_or_Deactive_Chain_Effect(m_tower_slot.Chain_Effect_Left, false);
            }

            void Active_or_Deactive_Chain_Effect(GameObject _obj, bool Active)
            {
                string Tag = Get_Target_Tag();
                if (Active && _obj.activeSelf == false)
                {
                    _obj.SetActive(true);
                    //Set_Chain_Weapon_Point(float Dmg, string tag, int critical_Rate, int critical_Damage, float time)
                    if (_obj.GetComponent<Object_Status>() != null)
                        _obj.GetComponent<Object_Status>().Set_Chain_Weapon_Point(0, Tag, 0, 0, 1.0f, null);
                }

                if (!Active && _obj.activeSelf == true)
                    _obj.SetActive(false);
            }
        }
    }

    public void Check_All_Group_Effect(int player_Number)
    {
        GameObject Map = Get_Local_Map(player_Number);
        if (Map == null) return;
        GameObject[] Tower_Array = Get_Tower_Array(player_Number);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group1);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group2);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group3);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group4);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group5);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group6);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group7);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group8);
        Check_Tower_Group(Map.GetComponent<Object_Status>().Group9);

        void Check_Tower_Group(int[] Group_Array)
        {
            // 604 Group Exp , 605 Group Speed , 613 Group Basic Attack , 
            //701 Full Screen Exp , 702 Full Screen Speed , 718 Group Lucky , 720 Group_Critical , 721 Group_Critical_Damage
            bool Allow_Group_Exp = false, Allow_Group_Speed = false, Allow_Group_Basic_Attack = false, Allow_Group_Lucky = false,
                Allow_Group_Critical = false, Allow_Group_Critical_Damage = false;

            Check_Any_Group_Type_Tower_In_Group(Group_Array, 604, out Allow_Group_Exp);
            Check_Any_Group_Type_Tower_In_Group(Group_Array, 605, out Allow_Group_Speed);
            Check_Any_Group_Type_Tower_In_Group(Group_Array, 613, out Allow_Group_Basic_Attack);
            Check_Any_Group_Type_Tower_In_Group(Group_Array, 718, out Allow_Group_Lucky);
            Check_Any_Group_Type_Tower_In_Group(Group_Array, 720, out Allow_Group_Critical);
            Check_Any_Group_Type_Tower_In_Group(Group_Array, 721, out Allow_Group_Critical_Damage);
            Set_Tower_Group_Effect(Group_Array);

            void Check_Any_Group_Type_Tower_In_Group(int[] group_Array, int Type, out bool In_Group)
            {
                In_Group = false;
                for (int i = 1; i < group_Array.Length; i++)
                {
                    int Slot_Number = group_Array[i];
                    if (Slot_Number != 0)
                    {
                        GameObject Tower = Tower_Array[Slot_Number];
                        if (Tower != null)
                        {
                            int Type_Number = Tower.GetComponent<Tower>().Type_Number;
                            if (Type_Number == Type)
                            {
                                In_Group = true;
                            }
                        }
                    }
                }
            }

            void Set_Tower_Group_Effect(int[] group_Array)
            {
                if (group_Array.Length == 0)
                    return;
                for (int i = 1; i < group_Array.Length; i++)
                {
                    int Slot_Number = group_Array[i];
                    if (Slot_Number != 0)
                    {
                        GameObject Tower = Tower_Array[Slot_Number];
                        if (Tower != null)
                        {
                            Tower.GetComponent<Tower>().Set_Group_Effect(604, Allow_Group_Exp);
                            Tower.GetComponent<Tower>().Set_Group_Effect(605, Allow_Group_Speed);
                            //Tower.GetComponent<Tower>().Set_Group_Effect(613, Allow_Group_Basic_Attack);
                            Tower.GetComponent<Tower>().Set_Group_Effect(718, Allow_Group_Lucky);
                            //Tower.GetComponent<Tower>().Set_Group_Effect(720, Allow_Group_Critical);
                            //Tower.GetComponent<Tower>().Set_Group_Effect(721, Allow_Group_Critical_Damage);
                        }
                    }
                }
            }
        }
    }

    GameObject[] Get_Tower_Array(int player_Number)
    {
        GameObject[] Tower_Array = null;
        switch (player_Number)
        {
            case (1):
                Tower_Array = Player_01_Tower_Array;
                break;
            case (2):
                Tower_Array = Player_02_Tower_Array;
                break;
            case (3):
                Tower_Array = Player_03_Tower_Array;
                break;
            case (4):
                Tower_Array = Player_04_Tower_Array;
                break;
        }
        return Tower_Array;
    }

    public void Add_Tower_To_Array_Group(GameObject Tower, int player_number, int slot_Number)
    {
        if (player_number == 1) Player_01_Tower_Array[slot_Number] = Tower;
        if (player_number == 2) Player_02_Tower_Array[slot_Number] = Tower;
        if (player_number == 3) Player_03_Tower_Array[slot_Number] = Tower;
        if (player_number == 4) Player_04_Tower_Array[slot_Number] = Tower;
    }

    bool Tower_Slot_Obj_Convert_To_Tower_is_Same_Type(int Check_Type_Number, GameObject TowerSlot, GameObject[] Tower_Array)
    {
        bool same_Type = false;
        if (TowerSlot == null)
            return false;
        int Slot_Number = TowerSlot.GetComponent<Tower_Slot>().Tower_Slot_Number;
        GameObject m_tower = Tower_Array[Slot_Number];
        if (m_tower == null)
            return false;
        int Tower_Type_Number = m_tower.GetComponent<Tower>().Type_Number;
        if (Tower_Type_Number == Check_Type_Number)
            same_Type = true;

        return same_Type;
    }

    public string Get_Target_Tag()
    {
        string name = null;
        if (Player_Number == 1)
            name = "Enemy_01";
        if (Player_Number == 2)
            name = "Enemy_02";
        if (Player_Number == 3)
            name = "Enemy_03";
        if (Player_Number == 4)
            name = "Enemy_04";
        return name;
    }

    public GameObject Get_Bullet_Form_Pool()
    {
        GameObject bullet = null;
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        if (!pool_Manager)
            return null;
        List<GameObject> Bullet_Pool_List = pool_Manager.GetComponent<Pool_Manager>().Bullet_Pool;
        if (Bullet_Pool_List.Count == 0)
            return null;
        for (int i = 0; i < Bullet_Pool_List.Count; i++)
        {
            if (Bullet_Pool_List[i] != null)
            {
                bullet = Bullet_Pool_List[i];
                Bullet_Pool_List.Remove(bullet);
                return bullet;
            }
        }
        return bullet;
    }

    public GameObject Get_HP_Bar_Form_Pool()
    {
        GameObject HP_Bar_Obj = null;
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        if (!pool_Manager)
            return null;
        List<GameObject> HP_Bar_List = pool_Manager.GetComponent<Pool_Manager>().HP_Bar_Pool;
        if (HP_Bar_List.Count == 0)
            return null;
        for (int i = 0; i < HP_Bar_List.Count; i++)
        {
            if (HP_Bar_List[i] != null)
            {
                HP_Bar_Obj = HP_Bar_List[i];
                HP_Bar_List.Remove(HP_Bar_Obj);
                return HP_Bar_Obj;
            }
        }
        return HP_Bar_Obj;
    }

    public GameObject Get_Treasure_Box_Form_Pool()
    {
        GameObject Treasure_Box_obj = null;
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        if (!pool_Manager)
            return null;
        List<GameObject> treasure_box_List = pool_Manager.GetComponent<Pool_Manager>().Treasure_Box_Pool;
        if (treasure_box_List.Count == 0)
            return null;
        for (int i = 0; i < treasure_box_List.Count; i++)
        {
            if (treasure_box_List[i] != null)
            {
                Treasure_Box_obj = treasure_box_List[i];
                treasure_box_List.Remove(Treasure_Box_obj);
                return Treasure_Box_obj;
            }
        }
        return Treasure_Box_obj;
    }

    public void Add_Obj_To_Pool_By_Time(GameObject effect, float Time, string Name)
    {
        StartCoroutine(Add_Obj_To_List(effect, Time, Name));
    }

    IEnumerator Add_Obj_To_List(GameObject obj, float Time, string Name)
    {
        yield return new WaitForSeconds(Time);
        if (Name == "Explosion_Effect")
        {
            obj.SetActive(false);
            Fall_Explosion_Effect.Add(obj);
        }
        if (Name == "Treasure")
        {
            Object_Status m_Treasure = obj.GetComponent<Object_Status>();

            obj.SetActive(false);
            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            if (!pool_Manager)
                yield break;
            List<GameObject> treasure_box_List = pool_Manager.GetComponent<Pool_Manager>().Treasure_Box_Pool;
            treasure_box_List.Add(obj);
        }
        if (Name == "HP_Bar")
        {
            obj.SetActive(false);
            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            if (!pool_Manager)
                yield break;
            List<GameObject> HP_Bar_List = pool_Manager.GetComponent<Pool_Manager>().HP_Bar_Pool;
            HP_Bar_List.Add(obj);
        }
        if (Name == "Damage_Bar")
        {
            obj.SetActive(false);
            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            if (!pool_Manager)
                yield break;
            List<GameObject> Damage_Bar_List = pool_Manager.GetComponent<Pool_Manager>().Damage_Bar_Pool;
            Damage_Bar_List.Add(obj);
        }
    }

    public GameObject Get_Fall_Explosion_Effect_Form_Pool()
    {
        GameObject explosion_effect = null;
        for (int i = 0; i < Fall_Explosion_Effect.Count; i++)
        {
            if (Fall_Explosion_Effect[i] != null)
            {
                explosion_effect = Fall_Explosion_Effect[i];
                Fall_Explosion_Effect.Remove(explosion_effect);
                return explosion_effect;
            }
        }
        return explosion_effect;
    }

    public void Deactive_Object(GameObject _obj)
    {
        _obj.SetActive(false);
    }

    public void Local_Check_All_Group_And_Screen_Bouns(int player_Number)
    {
        //604 Group_exp //605 Group_Speed //613 Group_Basic_Attack //718 Group_Lucky //720 Group_Critical //721 Group_Critical_Damage
        //701 Full_Screen Exp //702 Full_Screen_Speed

        GameObject Player_Map = null;
        GameObject[] Tower_Array = Get_Tower_Array(player_Number);
        if (Player_Number == 1)
            Player_Map = Player_01_Local_Map;

        if (Player_Number == 2)
            Player_Map = Player_02_Local_Map;

        if (Player_Number == 3)
            Player_Map = Player_03_Local_Map;

        if (Player_Number == 4)
            Player_Map = Player_04_Local_Map;

        if (Player_Map == null) return;
        GameObject[] tower_slot_array = Player_Map.GetComponent<Object_Status>().Tower_Slot_Array;

        int[] Group_Slot_Array;
        for (int i = 1; i < 9; i++)
        {
            Group_Slot_Array = Get_Group_Slot_Array(i);
            for (int j = 1; j < Group_Slot_Array.Length; j++)
            {
                Check_Any_Group_Type_Tower_In_Group(604, Group_Slot_Array, i);
                Check_Any_Group_Type_Tower_In_Group(605, Group_Slot_Array, i);
                Check_Any_Group_Type_Tower_In_Group(613, Group_Slot_Array, i);
                Check_Any_Group_Type_Tower_In_Group(718, Group_Slot_Array, i);
                Check_Any_Group_Type_Tower_In_Group(720, Group_Slot_Array, i);
                Check_Any_Group_Type_Tower_In_Group(721, Group_Slot_Array, i);
            }
        }

        Set_Full_Screen_Bouns(701);
        Set_Full_Screen_Bouns(702);

        void Check_Any_Group_Type_Tower_In_Group(int Type_Number, int[] Group_Array, int Group_Number)
        {
            bool Group_Type_Tower_In_Group = false;
            for (int i = 1; i < Group_Array.Length; i++)
            {
                int Slot_Number = Group_Array[i];
                if (Slot_Number != 0)
                {
                    GameObject m_tower = Tower_Array[Slot_Number];
                    if (m_tower != null)
                    {
                        int Type = m_tower.GetComponent<Tower>().Type_Number;
                        if (Type == Type_Number)
                        {
                            Group_Type_Tower_In_Group = true;
                            Set_Group_Bouns_Effect(Type_Number, Group_Array, true);
                        }
                    }
                }
            }

            if (!Group_Type_Tower_In_Group)
                Set_Group_Bouns_Effect(Type_Number, Group_Array, false);
        }

        void Set_Full_Screen_Bouns(int Type)
        {
            bool Full_Screen_Bouns = false;
            for (int i = 1; i < tower_slot_array.Length; i++)
            {
                GameObject m_Tower_slot = tower_slot_array[i];
                if (m_Tower_slot != null)
                {
                    GameObject m_tower = Tower_Array[i];
                    if (m_tower != null)
                    {
                        int m_type = m_tower.GetComponent<Tower>().Type_Number;
                        if (m_type == 701 || m_type == 702)
                        {
                            Full_Screen_Bouns = true;
                            Set_Full_Screen_Tower_Bouns(m_type, true);
                        }
                    }
                }
            }
            if (!Full_Screen_Bouns)
                Set_Full_Screen_Tower_Bouns(Type, false);

            void Set_Full_Screen_Tower_Bouns(int Type_Number, bool Active_or_Deactive)
            {
                for (int i = 1; i < Tower_Array.Length; i++)
                {
                    GameObject tower = Tower_Array[i];
                    if (tower != null)
                        Set_Effect_Active_or_Deactive(tower, Type_Number, Active_or_Deactive);
                }
            }
        }

        void Set_Group_Bouns_Effect(int Type_Number, int[] group_Slot_Array, bool Active_or_Deactive)
        {
            for (int i = 1; i < group_Slot_Array.Length; i++)
            {
                int Slot_Number = group_Slot_Array[i];
                GameObject tower = Tower_Array[Slot_Number];
                if (tower != null)
                    Set_Effect_Active_or_Deactive(tower, Type_Number, Active_or_Deactive);
            }
        }

        int[] Get_Group_Slot_Array(int Group_Number)
        {
            Object_Status Obj = Player_Map.GetComponent<Object_Status>();
            int[] Array = Obj.Group1;
            if (Group_Number == 1)
                Array = Obj.Group1;
            if (Group_Number == 2)
                Array = Obj.Group2;
            if (Group_Number == 3)
                Array = Obj.Group3;
            if (Group_Number == 4)
                Array = Obj.Group4;
            if (Group_Number == 5)
                Array = Obj.Group5;
            if (Group_Number == 6)
                Array = Obj.Group6;
            if (Group_Number == 7)
                Array = Obj.Group7;
            if (Group_Number == 8)
                Array = Obj.Group8;
            return Array;
        }

        void Set_Effect_Active_or_Deactive(GameObject tower, int Type, bool Active_or_Deactive)
        {
            GameObject Effect = null;
            if (Type == 604)
                Effect = tower.GetComponent<Tower>().Shot_Effect_604;

            if (Type == 605)
                Effect = tower.GetComponent<Tower>().Shot_Effect_605;

            if (Type == 613)
                Effect = tower.GetComponent<Tower>().Shot_Effect_613;

            if (Type == 718)
                Effect = tower.GetComponent<Tower>().Shot_Effect_718;

            if (Type == 720)
                Effect = tower.GetComponent<Tower>().Shot_Effect_720;

            if (Type == 721)
                Effect = tower.GetComponent<Tower>().Shot_Effect_721;

            if (Type == 701)
                Effect = tower.GetComponent<Tower>().Shot_Effect_701;

            if (Type == 702)
                Effect = tower.GetComponent<Tower>().Shot_Effect_702;

            if (Effect != null)
            {
                if (Active_or_Deactive)
                {
                    if (!Effect.activeSelf)
                        Effect.SetActive(true);
                }
                if (!Active_or_Deactive)
                {
                    if (Effect.activeSelf)
                        Effect.SetActive(false);
                }
            }
        }
    }

    public void Set_Local_Player(int player_number, GameObject player)
    {
        Player_Number = player_number;
        if (player_number == 1) Local_Player_01 = player;
        if (player_number == 2) Local_Player_02 = player;
        if (player_number == 3) Local_Player_03 = player;
        if (player_number == 4) Local_Player_04 = player;
    }

    public GameObject Get_Local_Player()
    {
        GameObject[] Local_Player = new GameObject[] { null, Local_Player_01, Local_Player_02, Local_Player_03, Local_Player_04 };
        return Local_Player[Player_Number];
    }

    public GameObject Get_Core_By_Player_Number(short player_Number)
    {
        GameObject[] Core = new GameObject[] { null, Player_01_Core, Player_02_Core, Player_03_Core, Player_04_Core };
        return Core[player_Number];
    }
}

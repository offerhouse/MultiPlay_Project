using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
using UnityEngine.UI;

public class Tower : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //, IPointerDownHandler
{
    // Tower_Status
    public float Damage;
    public float Speed;
    public float Distance;
    public float Add_Core_HP;
    public float Core_Bouns_Rate;
    public float Special_Effect_Time;
    public float Basic_HP_For_Solider;
    public float Basic_Attack_For_Solider;
    public float Heal_HP;
    public float Special_Rate_1;
    public float Special_Rate_2;
    public float Special_Rate_3;
    public int Chain_Connect_Number = 0;

    public int Counter_Attack, Counter_Dead, EXP, Level = 1, Counter_Attacker_to_Basic_Damage = 0;
    public float Basic_Damage_Bouns, EXP_Bouns, Speed_Bouns, Lucky_Bouns;
    public int Critical_Rate, Critical_Damage;

    float Timer, Timer2;
    float Timer_Add_Core_HP;
    Tower_Info.Tower_Info_Detail Basic_Detail;
    Tower_Info.Tower_Info_Detail Combine_Up_Detail;
    Tower_Info.Tower_Info_Detail Level_Up_Detail;
    Tower_Info.Tower_Info_Detail Power_Up_Detail;

    // End Tower_Status

    public bool collider_hit_Tower;

    public bool Dragger, End_Drag;
    public bool Lock, in_Pool;
    public int Player_Number;
    public int Slot_Number;
    public int Type_Number;
    public int Combine_Up_Point;
    public int Power_Up_Point;
    public int Group_Number;
    public int Tower_Number;
    public GameObject GM;
    public GameObject Player;
    public GameObject Core;
    public GameObject Tower_Controller;
    public GameObject Map_Manager;
    public GameObject HP_Bar;
    public GameObject Gold_Bar;
    public GameObject Bullet;
    public GameObject Lightning_Bullet;
    public GameObject Target_Tower;

    public Vector3 Tower_Position;
    public bool Summon_Bullet;
    public bool Move_Bullet;
    public bool Line_Render;
    public GameObject Chain_Weapon_Point_Top, Chain_Weapon_Point_Right, Chain_Weapon_Point_Buttom, Chain_Weapon_Point_Left;

    #region Tower Model
    [HideInInspector]
    public GameObject Ground_01, Ground_02, Ground_03, Ground_04, Ground_05, Ground_06, Ground_07, Ground_08, Ground_09, Ground_10;
    [HideInInspector]
    public GameObject Ground_11, Ground_12, Ground_13, Ground_14, Ground_15;

    [HideInInspector] public GameObject Icon_101, Icon_102, Icon_103, Icon_104, Icon_105, Icon_106, Icon_107, Icon_108, Icon_109, Icon_110;
    [HideInInspector] public GameObject Icon_201, Icon_202, Icon_203, Icon_204, Icon_205, Icon_206, Icon_207, Icon_208, Icon_209, Icon_210;
    [HideInInspector] public GameObject Icon_301, Icon_302, Icon_303, Icon_304, Icon_305, Icon_306, Icon_307, Icon_308, Icon_309, Icon_310;
    [HideInInspector] public GameObject Icon_401, Icon_402, Icon_403, Icon_404, Icon_405, Icon_406, Icon_407, Icon_408, Icon_409, Icon_410;
    [HideInInspector] public GameObject Icon_501, Icon_502, Icon_503, Icon_504, Icon_505, Icon_506, Icon_507, Icon_508, Icon_509, Icon_510;
    [HideInInspector] public GameObject Icon_601, Icon_602, Icon_603, Icon_604, Icon_605, Icon_606, Icon_607, Icon_608, Icon_609, Icon_610;
    [HideInInspector] public GameObject Icon_611, Icon_612, Icon_613, Icon_614, Icon_615, Icon_616, Icon_617, Icon_618, Icon_619, Icon_620;
    [HideInInspector] public GameObject Icon_701, Icon_702, Icon_703, Icon_704, Icon_705, Icon_706, Icon_707, Icon_708, Icon_709, Icon_710;
    [HideInInspector] public GameObject Icon_711, Icon_712, Icon_713, Icon_714, Icon_715, Icon_716, Icon_717, Icon_718, Icon_719, Icon_720;
    [HideInInspector] public GameObject Icon_721, Icon_722, Icon_723, Icon_724, Icon_725, Icon_726, Icon_727, Icon_728, Icon_729, Icon_730;

    [HideInInspector] public GameObject Level_01, Level_02, Level_03, Level_04, Level_05, Level_06, Level_07, Level_08, Level_09, Level_10;
    [HideInInspector] public GameObject Level_11, Level_12, Level_13, Level_14, Level_15;

    [HideInInspector]
    public GameObject D_L01, D_L02, D_L03, D_L04, D_L05, D_L06, D_L07, D_L08, D_L09, D_L10,
        D_L11, D_L12, D_L13, D_L14, D_L15;
    GameObject[] Dim_Level_Array;

    #endregion
    CapsuleCollider m_collider;

    #region Ground_Material
    [HideInInspector] public Material G_01, G_02, G_03, G_04, G_05, G_06, G_07, G_08, G_09, G_10, G_11, G_12, G_13, G_14, G_15, Ground_Dim;
    Material[] Ground_Material_Array;

    [HideInInspector] public Material Tower_Dim, T_01, T_02, T_03, T_04, T_05, T_06, T_07, T_08, T_09, T_10, T_11, T_12, T_13, T_14, T_15;
    Material[] Tower_Material_Array;

    [HideInInspector] public Color I_Dim, I_101, I_102, I_103, I_104, I_105, I_106, I_107, I_108, I_109, I_110;
    [HideInInspector] public Color I_201, I_202, I_203, I_204, I_205, I_206, I_207, I_208, I_209, I_210;
    [HideInInspector] public Color I_301, I_302, I_303, I_304, I_305, I_306, I_307, I_308, I_309, I_310;
    [HideInInspector] public Color I_401, I_402, I_403, I_404, I_405, I_406, I_407, I_408, I_409, I_410;
    [HideInInspector] public Color I_501, I_502, I_503, I_504, I_505, I_506, I_507, I_508, I_509, I_510;
    [HideInInspector] public Color I_601, I_602, I_603, I_604, I_605, I_606, I_607, I_608, I_609, I_610;
    [HideInInspector] public Color I_611, I_612, I_613, I_614, I_615, I_616, I_617, I_618, I_619, I_620;
    [HideInInspector] public Color I_701, I_702, I_703, I_704, I_705, I_706, I_707, I_708, I_709, I_710;
    [HideInInspector] public Color I_711, I_712, I_713, I_714, I_715, I_716, I_717, I_718, I_719, I_720;
    [HideInInspector] public Color I_721, I_722, I_723, I_724, I_725, I_726, I_727, I_728, I_729, I_730;

    #endregion

    public GameObject Shot_Effect_101, Shot_Effect_102, Shot_Effect_103, Shot_Effect_104, Shot_Effect_105;
    public GameObject Shot_Effect_201, Shot_Effect_202, Shot_Effect_203, Shot_Effect_204, Shot_Effect_205, Shot_Effect_206;
    public GameObject Shot_Effect_301, Shot_Effect_302, Shot_Effect_303, Shot_Effect_304, Shot_Effect_305;
    public GameObject Shot_Effect_401, Shot_Effect_402, Shot_Effect_403, Shot_Effect_404, Shot_Effect_405, Shot_Effect_406;
    public GameObject Shot_Effect_501, Shot_Effect_502, Shot_Effect_503, Shot_Effect_504, Shot_Effect_505, Shot_Effect_506, Shot_Effect_507;
    public GameObject Shot_Effect_601, Shot_Effect_602, Shot_Effect_603, Shot_Effect_604, Shot_Effect_605, Shot_Effect_606;
    public GameObject Shot_Effect_607, Shot_Effect_608, Shot_Effect_609, Shot_Effect_610, Shot_Effect_611, Shot_Effect_612, Shot_Effect_613;
    public GameObject Shot_Effect_701, Shot_Effect_702, Shot_Effect_703, Shot_Effect_704, Shot_Effect_705, Shot_Effect_706;
    public GameObject Shot_Effect_707, Shot_Effect_708, Shot_Effect_709, Shot_Effect_710, Shot_Effect_711, Shot_Effect_712;
    public GameObject Shot_Effect_713, Shot_Effect_714, Shot_Effect_715, Shot_Effect_716, Shot_Effect_717, Shot_Effect_717B, Shot_Effect_718;
    public GameObject Shot_Effect_719, Shot_Effect_720, Shot_Effect_721;

    public GameObject Smoke_01;
    public GameObject Fall_Enemy_Helper;
    public GameObject temp_Enemy_Helper;

    bool Start_Dragging;
    public bool Setup_Finish = false;

    bool One_VS_One = false, Two_VS_Two = false, Two_Cooperation = false, Four_Cooperation = false;

    public GameObject Test_S, Test_D, Test_E, Test_C, Test_R1, Test_R2, Test_L;
    Local_Manager local_manager;

    public class Tower_To_Target_Info
    {
        public GameObject Target; // Instant_Bullet = false , bullet Fix_Time = false , bullet_Speed = 4
        public bool Instant_Bullet;
        public bool Fix_Time_Bullet;
        public float Fix_Time;
        public float Bullet_Speed;
        public float Over_Time; // shot target how many times .
        public float Damage;
        public float Effect_Time; // Slow , Stun, Posion .... how long time;
        public short Special_Effect_Code;
        public short Action_Code;
        public Spwan_Enemy_Packet packet;
        public float Time;
        public int Tower_Name; // if tower name is not same will stop shot (cause combine tower)
        public bool Summon_Bullet;
        public bool Effect_On;
        public int Tower_Type;
        public short Spawn_Enemy_Code;
        public string Enemy_Name;

        public Tower_To_Target_Info(GameObject target, bool instant_Bullet, bool fix_Time_Bullet, float fix_Time,
            float bullet_Speed, float over_Time, float damage, float effect_Time)
        {
            Target = target;
            Instant_Bullet = instant_Bullet;
            Fix_Time_Bullet = fix_Time_Bullet;
            Fix_Time = fix_Time;
            Bullet_Speed = bullet_Speed;
            Over_Time = over_Time;
            Damage = damage;
            Effect_Time = effect_Time;
        }
    }
    
    void OnEnable()
    {
        Debug.Log("NetworkClient.local_tower_active_from_pool_Enable");
        NetworkClient.local_tower_active_from_pool += Local_Tower_Set_Visible;
    }

    void Setup_Start()
    {
        //Debug.Log("Type_Number " + Type_Number);
        m_collider = gameObject.GetComponent<CapsuleCollider>();
        Set_Status_Class();

        if (!isServer)
        {
            Update_Tower_Icon();
            Ground_Material_Array = new Material[] { Ground_Dim, G_01, G_02, G_03, G_04, G_05, G_06, G_07, G_08, G_09, G_10,
    G_11,G_12,G_13,G_14,G_15};
            Tower_Material_Array = new Material[] { Tower_Dim, T_01, T_02, T_03, T_04, T_05, T_06, T_07, T_08, T_09, T_10,
    T_11,T_12,T_13,T_14,T_15};
            Dim_Level_Array = new GameObject[] { null,D_L01, D_L02, D_L03, D_L04, D_L05, D_L06, D_L07, D_L08, D_L09, D_L10,
    D_L11, D_L12, D_L13, D_L14, D_L15};
        }
    }

    void Set_Status_Class()
    {
        Basic_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);
        Combine_Up_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);
        Level_Up_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);

        Basic_Detail = gameObject.GetComponent<Tower_Info>().Tower_Basic_Info(Type_Number);
        Combine_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Comine_Up_Detail(Type_Number);
        Power_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Comine_Up_Detail(Type_Number);
        Level_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Level_Up_Detail(Type_Number);
        //Debug.Log("Set_Status_Class || " + Basic_Detail.Damage);
    }

    #region Drag_Tower_Change_Tower_Material
    public void Reset_All_Tower_Material(NetworkConnection conn)
    {
        Target_Reset_All_Tower_Material(conn);
    }

    [TargetRpc] // Server to Client
    public void Target_Reset_All_Tower_Material(NetworkConnection conn)
    {
        Debug.Log("Target_Reset_All_Tower_Material");
        if (!isServer)
        {
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            m_local_Manager.GetComponent<Local_Manager>().End_Drag_Reset_Tower(gameObject, Type_Number, Combine_Up_Point);
        }
    }

    [TargetRpc] // Server to Client
    public void Target_Set_Tower_To_Local_Manager(NetworkConnection conn)// , bool wait)
    {
        Debug.Log("Target_Set_Tower_To_Local_Manager");
        if (!isServer)
        {
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            gameObject.transform.SetParent(m_local_Manager.transform);
        }
    }
    #endregion

    #region Server to Local

    public void Server_Prepare_Update_Tower_Info(int player_Number, int slot_Number, int type_Number, int combine_point_up, int level,
        GameObject player, bool summon_bullet, bool move_bullet, bool line_render, float dmg, float spd, float add_core_hp,
        float special_effect_time, float basic_hp_for_solider, float heal_hp, float S_Rate_1, float S_Rate_2, float S_Rate_3,
        int critical_rate, int critical_damage, int Drag_Tower_Number, Vector3 tower_poisition)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Update_Tower_Info(Conn, player_Number, slot_Number, type_Number, combine_point_up, Level, player, Summon_Bullet, Move_Bullet,
                Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time, Basic_HP_For_Solider, Heal_HP, Special_Rate_1,
                Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, Drag_Tower_Number, tower_poisition);
        }
    }

    [TargetRpc]
    void Target_Update_Tower_Info(NetworkConnection Conn, int player_Number, int slot_Number, int type_Number, int combine_point_up,
        int level, GameObject player, bool summon_bullet, bool move_bullet, bool line_render, float dmg, float spd, float add_core_hp,
        float special_effect_time, float basic_hp_for_solider, float heal_hp, float S_Rate_1, float S_Rate_2, float S_Rate_3,
        int critical_rate, int critical_damage, int Drag_Tower_Number, Vector3 tower_poisition)
    {
        Local_Update_Tower_Info(player_Number, slot_Number, type_Number, combine_point_up, Level, player, Summon_Bullet, Move_Bullet,
        Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time, Basic_HP_For_Solider, Heal_HP, Special_Rate_1,
        Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, Drag_Tower_Number, tower_poisition);
    }

    void Server_Prepare_Heal_To_All_Client(GameObject[] Target)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Heal_To_All_Client(Conn, Target);
        }
    }

    [TargetRpc]
    void Target_Heal_To_All_Client(NetworkConnection Conn, GameObject[] Target)
    {
        Debug.Log("Target_Heal_To_All_Client");
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }

        float Shot_Effect_Time = Get_Shot_Effect_Time(Type_Number);
        if (Shot_Effect_Time > 0)
        {
            GameObject Shot_Effect = Get_Shot_Effect(Type_Number);
            Shot_Effect.SetActive(false);
            Shot_Effect.SetActive(true);
            StartCoroutine(Deactive_Object(Shot_Effect, Shot_Effect_Time, false));
        }

        for (int i = 0; i < Target.Length; i++)
        {
            if (Target[i] != null)
            {
                //Target[i].GetComponent<Enemy>().Local_Heal_HP_Bar(Value[i]);

                float time = Get_Effect_Time(Type_Number);
                if (time > 0)
                {
                    Target[i].GetComponent<Enemy>().Local_Set_Enemy_Effect(Type_Number, time);
                }
            }
        }
    }

    void Server_Prepare_Target_Lightning_To_All_Client(GameObject Target)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Lightning_To_All_Client(Conn, Target);
        }
    }

    [TargetRpc] // Only Tower 303 and 507 will use this ?
    void Target_Lightning_To_All_Client(NetworkConnection Conn, GameObject Target)
    {
        GameObject Start_Point = null;
        Debug.Log("Target_Lightning_To_All_Client || " + Type_Number);
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }

        Vector3 POS = new Vector3(0, 1.5f, 0);
        if (Target == null)
            return;
        if (Start_Point == null)
        {
            Start_Point = new GameObject();
            Start_Point.transform.position = Tower_Position + POS;
            Destroy(Start_Point, 3.0f);
        }

        if (Type_Number == 507)
        {
            POS = new Vector3(0, 5f, 0);
            Start_Point.transform.position = Target.transform.position + POS;
        }

        GameObject lightning_bullet = null;
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        int Line_Render_Pool_Number = pool_Manager.GetComponent<Pool_Manager>().Bullet_Line_Render_Pool.Count;
        if (pool_Manager)
            if (Line_Render_Pool_Number > 0)
            {
                lightning_bullet = pool_Manager.GetComponent<Pool_Manager>().Get_Object_From_Pool_By_Tag("Lightning_Line_Render");
                if (lightning_bullet)
                {

                    pool_Manager.GetComponent<Pool_Manager>().Bullet_Line_Render_Pool.Remove(lightning_bullet);
                    lightning_bullet.transform.position = Vector3.zero;
                    lightning_bullet.SetActive(true);
                }
            }

        if (!lightning_bullet)
        {
            lightning_bullet = Instantiate(Lightning_Bullet, Vector3.zero, Quaternion.identity);
            pool_Manager.GetComponent<Pool_Manager>().Set_Obj_To_Folder_By_Name("Lightning_Line_Render", lightning_bullet);
        }

        if (Type_Number == 507)
        {
            lightning_bullet.GetComponent<Effect_Lighting_Multi_Line_Renderer>().Line_Render_Fix_Width = 0.7f;
            lightning_bullet.GetComponent<Effect_Lighting_Multi_Line_Renderer>().Active_Effect_01();
            lightning_bullet.GetComponent<Effect_Lighting_Multi_Line_Renderer>().time = 1.0f;
        }

        Debug.Log("lightning_bullet || " + lightning_bullet + " || " + Line_Render_Pool_Number);
        lightning_bullet.GetComponent<Effect_Lighting_Multi_Line_Renderer>().Set_Light_And_Start(Start_Point, Target, Type_Number);
    }

    void Server_Prepare_Set_Gold_Bar(int gold)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Gold_Bar(Conn, gold);
        }
    }

    [TargetRpc]
    void Target_Set_Gold_Bar(NetworkConnection Conn, int gold)
    {
        Debug.Log("Target_Set_Gold_Bar");
        if (Player_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }

        Vector3 POS = Tower_Position + new Vector3(0, 1.5f, 0);
        GameObject gold_bar = Instantiate(Gold_Bar, POS, Quaternion.identity);
        if (!Player)
            Player = Get_Online_Player_Object_From_Object_Manager(Player_Number);
        if (Player)
        {
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            Local_Manager lm = m_local_Manager.GetComponent<Local_Manager>();
            GameObject camera = lm.Get_Camera((short)Player_Number);
            gold_bar.GetComponent<Object_Status>().Set_Gold_Bar(gold, camera);
        }
    }

    void Server_Prepare_Bullet_To_All_Client(GameObject Target, float Time, float bullet_Spd, bool m_summon_bullet, bool Effect_On)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Bullet_To_All_Client(Conn, Target, Time, bullet_Spd, m_summon_bullet, Effect_On);
        }
    }

    [TargetRpc]
    void Target_Bullet_To_All_Client(NetworkConnection Conn, GameObject Target, float Time, float bullet_Spd, bool m_summon_bullet,
        bool Effect_On)
    {
        if (Type_Number == 0)
            return;

        bool summon_bullet = Summon_Bullet;
        if (m_summon_bullet == false)
            summon_bullet = false;

        // HP Bar
        if (Target == null)
            return;

        Vector3 POS = Target.transform.position + new Vector3(0, 1.5f, 0);

        // Effect
        float time = Get_Effect_Time(Type_Number);
        if (time > 0 && Effect_On)
            Target.GetComponent<Enemy>().Set_Enemy_Effect(Type_Number, time);

        if (summon_bullet)
        {
            GameObject Shot_Effect = Get_Shot_Effect(Type_Number);
            float Shot_Effect_Time = Get_Shot_Effect_Time(Type_Number);
            Shot_Effect.SetActive(false);
            Shot_Effect.SetActive(true);
            Deactive_Object(Shot_Effect, Shot_Effect_Time, false);
        }

        // Bullet
        if (summon_bullet)
        {
            if (Line_Render)
                POS = POS + new Vector3(0, 10f, 0);
            if (Line_Render && Type_Number == 303)
                POS = POS + new Vector3(0, 1.5f, 0);
            if (Move_Bullet)
                POS = Tower_Position + new Vector3(0, 1.5f, 0);
            if (!Line_Render & !Move_Bullet)
                POS = POS + new Vector3(0, 0.5f, 0);
            if (Type_Number == 403 || Type_Number == 503 || Type_Number == 609) // Fall_Protecter or Fall_Attacker
                POS = Tower_Position + new Vector3(5, 15.0f, 5);

            float Distance = Vector3.Distance(POS, Target.transform.position);
            GameObject bullet = null;
            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            if (pool_Manager)
                if (pool_Manager.GetComponent<Pool_Manager>().Bullet_Pool.Count != 0)
                {
                    bullet = local_manager.Get_Bullet_Form_Pool();
                    if (bullet)
                    {
                        bullet.transform.position = POS;
                        bullet.SetActive(true);
                    }
                }
            if (!bullet)
                bullet = Instantiate(Bullet, POS, Quaternion.identity);
            if (!Line_Render & !Move_Bullet)
                bullet.GetComponent<Bullet>().Send_To_Pool_By_Time(2.0f);
            //GameObject target, int type, bool move_bullet, bool line_render, float speed, Target object name
            bullet.GetComponent<Bullet>().Set_Bullet(Target, Type_Number, Move_Bullet, Line_Render, bullet_Spd, Target.name);
        }
    }

    void Server_Prepare_Specify_Shot_Effect(int type_number)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Specify_Shot_Effect(Conn, type_number);
        }
    }

    [TargetRpc]
    void Target_Specify_Shot_Effect(NetworkConnection Conn, int type_number)
    {
        Debug.Log("Target_Specify_Shot_Effect");
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }
        if (type_number == 711 || type_number == 7112)
        {
            GameObject Smoke_Effect = Instantiate(Smoke_01, Tower_Position, Quaternion.identity);
            Destroy(Smoke_Effect, 4.0f);
        }
        GameObject Shot_Effect = Get_Shot_Effect(type_number);
        float Shot_Effect_Time = Get_Shot_Effect_Time(type_number);
        Shot_Effect.SetActive(false);
        Shot_Effect.SetActive(true);
        StartCoroutine(Deactive_Object(Shot_Effect, Shot_Effect_Time, false));
    }

    void Server_Prepare_Shot_Effect()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Shot_Effect(Conn);
        }
    }

    [TargetRpc]
    void Target_Shot_Effect(NetworkConnection Conn)
    {
        //if (Type_Number == 609)
        //    Debug.Log("RpcStart_Shot_Effect || " + Type_Number);
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }
        GameObject Shot_Effect = Get_Shot_Effect(Type_Number);
        float Shot_Effect_Time = Get_Shot_Effect_Time(Type_Number);
        if (!Shot_Effect)
            Debug.Log("Shot_Effect_is_Null || Type_Number || " + Type_Number);
        Shot_Effect.SetActive(false);
        Shot_Effect.SetActive(true);
        StartCoroutine(Deactive_Object(Shot_Effect, Shot_Effect_Time, false));
    }

    void Server_Prepare_All_Screen_Enemy_Effect(int Type_Number, float time, int play_number, float damage, float Effect_Time)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_All_Screen_Enemy_Effect(Conn, Type_Number, time, play_number, damage, Effect_Time);
        }
    }

    [TargetRpc]
    void Target_All_Screen_Enemy_Effect(NetworkConnection Conn, int Type_Number, float time, int play_number, float damage,
        float Effect_Time)
    {
        Debug.Log("Target_All_Screen_Enemy_Effect || " + Type_Number + " || " + time);
        GameObject Effect = null;
        // Opponent Enemy || Attacker || Protector
        GameObject[] All_Opponent_Enemy_Attacker_Protector = Get_All_Opponent_Obj_Form_Map_Manager();
        for (int i = 0; i < All_Opponent_Enemy_Attacker_Protector.Length; i++)
        {
            if (Type_Number == 401) // Full_Screen_Thunder
                Effect = All_Opponent_Enemy_Attacker_Protector[i].GetComponent<Enemy>().Thunder_Effect_01;
            if (Type_Number == 402) // Full_Screen_Ice
            {
                GameObject Body = All_Opponent_Enemy_Attacker_Protector[i].GetComponent<Enemy>().Client_Model;
                Effect = Body.GetComponent<Object_Status>().Ice_Effect;
            }

            //RpcSend_Bullet_To_All_Client(All_Enemy_and_Enemy_Protector[i], damage, 0, 0, true, true);

            Vector3 POS = All_Opponent_Enemy_Attacker_Protector[i].transform.position + new Vector3(0, 1.5f, 0);

            GameObject Shot_Effect = Get_Shot_Effect(Type_Number);
            float Shot_Effect_Time = Get_Shot_Effect_Time(Type_Number);
            Shot_Effect.SetActive(false);
            Shot_Effect.SetActive(true);
            StartCoroutine(Deactive_Object(Shot_Effect, Shot_Effect_Time, false));
            Effect.SetActive(true);
            All_Opponent_Enemy_Attacker_Protector[i].GetComponent<Enemy>().Deactive_Enemy_Object(Effect, 1.5f, false);
        }
    }

    void Server_Prepare_Reset_Local_Tower()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Reset_Local_Tower(Conn);
        }
    }

    [TargetRpc]
    void Target_Reset_Local_Tower(NetworkConnection Conn)
    {
        Debug.Log("Target_Reset_Local_Tower");
        Reset_Tower_To_Pool();
    }

    void Server_Prepare_Pool_Active_Tower(short player_Number, short slot_Number, Vector3 POS)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
            {
                Target_Active_Tower(Conn, player_Number, slot_Number, POS);
                Server_Tower_Set_Visible();
            }
        }
    }

    [TargetRpc]
    void Target_Active_Tower(NetworkConnection Conn, short player_Number, short slot_Number, Vector3 POS)
    {
        transform.position = POS;
        Local_Tower_Set_Visible(player_Number, slot_Number);
    }
    #endregion

    #region Get Ground , Icon , Level

    void Update_Tower_Icon()
    {
        if (!isServer)
        {
            Disable_All_Tower();
            GameObject Ground = Get_Ground(Combine_Up_Point);
            GameObject Icon = Get_Icon(Type_Number);
            GameObject Level = Get_Combine_Level(Combine_Up_Point);
            Color Color = Get_Type_Color(Type_Number);
            if (Ground != null)
                Ground.SetActive(true);
            if (Icon != null)
            {
                Set_Type_Color(Icon, Color);
                Icon.SetActive(true);
            }

            if (Level != null)
            {
                Set_Type_Color(Level, Color);
                Level.SetActive(true);
            }
        }
    }

    public void Reset_Icon_and_Level()
    {
        GameObject Icon = Get_Icon(Type_Number);
        GameObject Level = Get_Combine_Level(Combine_Up_Point);
        Color Color = Get_Type_Color(Type_Number);
        Level.SetActive(true);
        Dim_Level_Array[Combine_Up_Point].SetActive(false);
        Set_Type_Color(Icon, Color);
    }

    public void Dim_Icon_and_Level()
    {
        //Debug.Log("Dim_Icon_and_Level");
        GameObject Icon = Get_Icon(Type_Number);
        GameObject Level = Get_Combine_Level(Combine_Up_Point);
        Color Color = Get_Type_Color(0);
        Level.SetActive(false);
        Dim_Level_Array[Combine_Up_Point].SetActive(true);
        Set_Type_Color(Icon, Color);
    }

    void Set_Type_Color(GameObject _object, Color Color)
    {
        ParticleSystem.MainModule _object_P = _object.GetComponent<ParticleSystem>().main;
        _object_P.startColor = Color;
    }

    void Disable_All_Tower()
    {
        for (int i = 1; i < 16; i++)
        {
            GameObject Ground = Get_Ground(i);
            if (Ground != null)
                Ground.SetActive(false);
            GameObject Level = Get_Combine_Level(i);
            if (Level != null)
                Level.SetActive(false);
        }
        Clear_All_Icon(101, 111);
        Clear_All_Icon(201, 211);
        Clear_All_Icon(301, 311);
        Clear_All_Icon(401, 411);
        Clear_All_Icon(501, 511);
        Clear_All_Icon(601, 621);
        Clear_All_Icon(701, 731);
        void Clear_All_Icon(int start_Number, int End_Number)
        {
            for (int i = start_Number; i < End_Number; i++)
            {
                GameObject Icon = Get_Icon(i);
                if (Icon != null)
                    Icon.SetActive(false);
            }
        }
    }

    public Color Get_Type_Color(int number)
    {
        Color m_Color = I_Dim;
        switch (number)
        {
            case (0):
                m_Color = I_Dim;
                break;
            case (101):
                m_Color = I_101;
                break;
            case (102):
                m_Color = I_102;
                break;
            case (103):
                m_Color = I_103;
                break;
            case (104):
                m_Color = I_104;
                break;
            case (105):
                m_Color = I_105;
                break;
            case (106):
                m_Color = I_106;
                break;
            case (107):
                m_Color = I_107;
                break;
            case (108):
                m_Color = I_108;
                break;
            case (109):
                m_Color = I_109;
                break;
            case (110):
                m_Color = I_110;
                break;
            case (201):
                m_Color = I_201;
                break;
            case (202):
                m_Color = I_202;
                break;
            case (203):
                m_Color = I_203;
                break;
            case (204):
                m_Color = I_204;
                break;
            case (205):
                m_Color = I_205;
                break;
            case (206):
                m_Color = I_206;
                break;
            case (207):
                m_Color = I_207;
                break;
            case (208):
                m_Color = I_208;
                break;
            case (209):
                m_Color = I_209;
                break;
            case (210):
                m_Color = I_210;
                break;
            case (301):
                m_Color = I_301;
                break;
            case (302):
                m_Color = I_302;
                break;
            case (303):
                m_Color = I_303;
                break;
            case (304):
                m_Color = I_304;
                break;
            case (305):
                m_Color = I_305;
                break;
            case (306):
                m_Color = I_306;
                break;
            case (307):
                m_Color = I_307;
                break;
            case (308):
                m_Color = I_308;
                break;
            case (309):
                m_Color = I_309;
                break;
            case (310):
                m_Color = I_310;
                break;
            case (401):
                m_Color = I_401;
                break;
            case (402):
                m_Color = I_402;
                break;
            case (403):
                m_Color = I_403;
                break;
            case (404):
                m_Color = I_404;
                break;
            case (405):
                m_Color = I_405;
                break;
            case (406):
                m_Color = I_406;
                break;
            case (407):
                m_Color = I_407;
                break;
            case (408):
                m_Color = I_408;
                break;
            case (409):
                m_Color = I_409;
                break;
            case (410):
                m_Color = I_410;
                break;
            case (501):
                m_Color = I_501;
                break;
            case (502):
                m_Color = I_502;
                break;
            case (503):
                m_Color = I_503;
                break;
            case (504):
                m_Color = I_504;
                break;
            case (505):
                m_Color = I_505;
                break;
            case (506):
                m_Color = I_506;
                break;
            case (507):
                m_Color = I_507;
                break;
            case (508):
                m_Color = I_508;
                break;
            case (509):
                m_Color = I_509;
                break;
            case (510):
                m_Color = I_510;
                break;
            case (601):
                m_Color = I_601;
                break;
            case (602):
                m_Color = I_602;
                break;
            case (603):
                m_Color = I_603;
                break;
            case (604):
                m_Color = I_604;
                break;
            case (605):
                m_Color = I_605;
                break;
            case (606):
                m_Color = I_606;
                break;
            case (607):
                m_Color = I_607;
                break;
            case (608):
                m_Color = I_608;
                break;
            case (609):
                m_Color = I_609;
                break;
            case (610):
                m_Color = I_610;
                break;
            case (611):
                m_Color = I_611;
                break;
            case (612):
                m_Color = I_612;
                break;
            case (613):
                m_Color = I_613;
                break;
            case (614):
                m_Color = I_614;
                break;
            case (615):
                m_Color = I_615;
                break;
            case (616):
                m_Color = I_616;
                break;
            case (617):
                m_Color = I_617;
                break;
            case (618):
                m_Color = I_618;
                break;
            case (619):
                m_Color = I_619;
                break;
            case (620):
                m_Color = I_620;
                break;
            case (701):
                m_Color = I_701;
                break;
            case (702):
                m_Color = I_702;
                break;
            case (703):
                m_Color = I_703;
                break;
            case (704):
                m_Color = I_704;
                break;
            case (705):
                m_Color = I_705;
                break;
            case (706):
                m_Color = I_706;
                break;
            case (707):
                m_Color = I_707;
                break;
            case (708):
                m_Color = I_708;
                break;
            case (709):
                m_Color = I_709;
                break;
            case (710):
                m_Color = I_710;
                break;
            case (711):
                m_Color = I_711;
                break;
            case (712):
                m_Color = I_712;
                break;
            case (713):
                m_Color = I_713;
                break;
            case (714):
                m_Color = I_714;
                break;
            case (715):
                m_Color = I_715;
                break;
            case (716):
                m_Color = I_716;
                break;
            case (717):
                m_Color = I_717;
                break;
            case (718):
                m_Color = I_718;
                break;
            case (719):
                m_Color = I_719;
                break;
            case (720):
                m_Color = I_720;
                break;
            case (721):
                m_Color = I_721;
                break;
            case (722):
                m_Color = I_722;
                break;
            case (723):
                m_Color = I_723;
                break;
            case (724):
                m_Color = I_724;
                break;
            case (725):
                m_Color = I_725;
                break;
            case (726):
                m_Color = I_726;
                break;
            case (727):
                m_Color = I_727;
                break;
            case (728):
                m_Color = I_728;
                break;
            case (729):
                m_Color = I_729;
                break;
            case (730):
                m_Color = I_730;
                break;
        }
        return m_Color;
    }

    public GameObject Get_Ground(int number)
    {
        GameObject Ground = null;
        switch (number)
        {
            case (1):
                Ground = Ground_01;
                break;
            case (2):
                Ground = Ground_02;
                break;
            case (3):
                Ground = Ground_03;
                break;
            case (4):
                Ground = Ground_04;
                break;
            case (5):
                Ground = Ground_05;
                break;
            case (6):
                Ground = Ground_06;
                break;
            case (7):
                Ground = Ground_07;
                break;
            case (8):
                Ground = Ground_08;
                break;
            case (9):
                Ground = Ground_09;
                break;
            case (10):
                Ground = Ground_10;
                break;
            case (11):
                Ground = Ground_11;
                break;
            case (12):
                Ground = Ground_12;
                break;
            case (13):
                Ground = Ground_13;
                break;
            case (14):
                Ground = Ground_14;
                break;
            case (15):
                Ground = Ground_15;
                break;
        }
        return Ground;
    }

    public GameObject Get_Icon(int number)
    {
        GameObject Icon = null;
        switch (number)
        {
            case (101):
                Icon = Icon_101;
                break;
            case (102):
                Icon = Icon_102;
                break;
            case (103):
                Icon = Icon_103;
                break;
            case (104):
                Icon = Icon_104;
                break;
            case (105):
                Icon = Icon_105;
                break;
            case (106):
                Icon = Icon_106;
                break;
            case (107):
                Icon = Icon_107;
                break;
            case (108):
                Icon = Icon_108;
                break;
            case (109):
                Icon = Icon_109;
                break;
            case (110):
                Icon = Icon_110;
                break;
            case (201):
                Icon = Icon_201;
                break;
            case (202):
                Icon = Icon_202;
                break;
            case (203):
                Icon = Icon_203;
                break;
            case (204):
                Icon = Icon_204;
                break;
            case (205):
                Icon = Icon_205;
                break;
            case (206):
                Icon = Icon_206;
                break;
            case (207):
                Icon = Icon_207;
                break;
            case (208):
                Icon = Icon_208;
                break;
            case (209):
                Icon = Icon_209;
                break;
            case (210):
                Icon = Icon_210;
                break;
            case (301):
                Icon = Icon_301;
                break;
            case (302):
                Icon = Icon_302;
                break;
            case (303):
                Icon = Icon_303;
                break;
            case (304):
                Icon = Icon_304;
                break;
            case (305):
                Icon = Icon_305;
                break;
            case (306):
                Icon = Icon_306;
                break;
            case (307):
                Icon = Icon_307;
                break;
            case (308):
                Icon = Icon_308;
                break;
            case (309):
                Icon = Icon_309;
                break;
            case (310):
                Icon = Icon_310;
                break;
            case (401):
                Icon = Icon_401;
                break;
            case (402):
                Icon = Icon_402;
                break;
            case (403):
                Icon = Icon_403;
                break;
            case (404):
                Icon = Icon_404;
                break;
            case (405):
                Icon = Icon_405;
                break;
            case (406):
                Icon = Icon_406;
                break;
            case (407):
                Icon = Icon_407;
                break;
            case (408):
                Icon = Icon_408;
                break;
            case (409):
                Icon = Icon_409;
                break;
            case (410):
                Icon = Icon_410;
                break;
            case (501):
                Icon = Icon_501;
                break;
            case (502):
                Icon = Icon_502;
                break;
            case (503):
                Icon = Icon_503;
                break;
            case (504):
                Icon = Icon_504;
                break;
            case (505):
                Icon = Icon_505;
                break;
            case (506):
                Icon = Icon_506;
                break;
            case (507):
                Icon = Icon_507;
                break;
            case (508):
                Icon = Icon_508;
                break;
            case (509):
                Icon = Icon_509;
                break;
            case (510):
                Icon = Icon_510;
                break;
            case (601):
                Icon = Icon_601;
                break;
            case (602):
                Icon = Icon_602;
                break;
            case (603):
                Icon = Icon_603;
                break;
            case (604):
                Icon = Icon_604;
                break;
            case (605):
                Icon = Icon_605;
                break;
            case (606):
                Icon = Icon_606;
                break;
            case (607):
                Icon = Icon_607;
                break;
            case (608):
                Icon = Icon_608;
                break;
            case (609):
                Icon = Icon_609;
                break;
            case (610):
                Icon = Icon_610;
                break;
            case (611):
                Icon = Icon_611;
                break;
            case (612):
                Icon = Icon_612;
                break;
            case (613):
                Icon = Icon_613;
                break;
            case (614):
                Icon = Icon_614;
                break;
            case (615):
                Icon = Icon_615;
                break;
            case (616):
                Icon = Icon_616;
                break;
            case (617):
                Icon = Icon_617;
                break;
            case (618):
                Icon = Icon_618;
                break;
            case (619):
                Icon = Icon_619;
                break;
            case (620):
                Icon = Icon_620;
                break;
            case (701):
                Icon = Icon_701;
                break;
            case (702):
                Icon = Icon_702;
                break;
            case (703):
                Icon = Icon_703;
                break;
            case (704):
                Icon = Icon_704;
                break;
            case (705):
                Icon = Icon_705;
                break;
            case (706):
                Icon = Icon_706;
                break;
            case (707):
                Icon = Icon_707;
                break;
            case (708):
                Icon = Icon_708;
                break;
            case (709):
                Icon = Icon_709;
                break;
            case (710):
                Icon = Icon_710;
                break;
            case (711):
                Icon = Icon_711;
                break;
            case (712):
                Icon = Icon_712;
                break;
            case (713):
                Icon = Icon_713;
                break;
            case (714):
                Icon = Icon_714;
                break;
            case (715):
                Icon = Icon_715;
                break;
            case (716):
                Icon = Icon_716;
                break;
            case (717):
                Icon = Icon_717;
                break;
            case (718):
                Icon = Icon_718;
                break;
            case (719):
                Icon = Icon_719;
                break;
            case (720):
                Icon = Icon_720;
                break;
            case (721):
                Icon = Icon_721;
                break;
            case (722):
                Icon = Icon_722;
                break;
            case (723):
                Icon = Icon_723;
                break;
            case (724):
                Icon = Icon_724;
                break;
            case (725):
                Icon = Icon_725;
                break;
            case (726):
                Icon = Icon_726;
                break;
            case (727):
                Icon = Icon_727;
                break;
            case (728):
                Icon = Icon_728;
                break;
            case (729):
                Icon = Icon_729;
                break;
            case (730):
                Icon = Icon_730;
                break;
        }
        return Icon;
    }

    public GameObject Get_Combine_Level(int number)
    {
        GameObject Level = null;
        switch (number)
        {
            case (1):
                Level = Level_01;
                break;
            case (2):
                Level = Level_02;
                break;
            case (3):
                Level = Level_03;
                break;
            case (4):
                Level = Level_04;
                break;
            case (5):
                Level = Level_05;
                break;
            case (6):
                Level = Level_06;
                break;
            case (7):
                Level = Level_07;
                break;
            case (8):
                Level = Level_08;
                break;
            case (9):
                Level = Level_09;
                break;
            case (10):
                Level = Level_10;
                break;
            case (11):
                Level = Level_11;
                break;
            case (12):
                Level = Level_12;
                break;
            case (13):
                Level = Level_13;
                break;
            case (14):
                Level = Level_14;
                break;
            case (15):
                Level = Level_15;
                break;
        }
        return Level;
    }

    public void Set_Ground_Material(bool Dim)
    {
        GameObject Ground = Get_Ground(Combine_Up_Point);
        if (Dim)
        {
            Ground.GetComponent<MeshRenderer>().material = Ground_Material_Array[0];
            GameObject m_Tower = Ground.transform.GetChild(0).gameObject;
            if (m_Tower != null)
                m_Tower.GetComponent<MeshRenderer>().material = Tower_Material_Array[0];
        }
        if (!Dim)
        {
            Ground.GetComponent<MeshRenderer>().material = Ground_Material_Array[Combine_Up_Point];
            GameObject m_Tower = Ground.transform.GetChild(0).gameObject;
            if (m_Tower != null)
                m_Tower.GetComponent<MeshRenderer>().material = Tower_Material_Array[Combine_Up_Point];
        }
    }

    #endregion

    #region Tower Info (Type , Combine Point , Slot Number) // Start Here
    public void Update_Tower_Info(int player_Number, int slot_Number, int type_Number, int combine_point_up, int level,
        GameObject player, float critical_rate, int Drag_Tower_Number, int tower_number, Vector3 POS)
    {
        //Debug.Log("Update_Tower_Info_1 || " + Setup_Finish);
        Level = level;
        Type_Number = type_Number;
        Tower_Number = tower_number;
        Lock = false;
        if (!Setup_Finish)
        {
            Setup_Start();
            Setup_Finish = true;
        }

        if (isServer)
        {
            GameObject object_Manager = GameObject.Find("Object_Manager");
            short op_room_Code = object_Manager.GetComponent<Object_Manager>().Get_Current_Match_Type_OP_Code();

            if (op_room_Code == (short)OP_Room_Code.Match1v1)
                One_VS_One = true;
            if (op_room_Code == (short)OP_Room_Code.Match2op)
                Two_VS_Two = true;
            if (op_room_Code == (short)OP_Room_Code.Match2v2)
                Two_Cooperation = true;
            if (op_room_Code == (short)OP_Room_Code.Match4op)
                Four_Cooperation = true;

            Power_Up_Point = Tower_Controller.GetComponent<Tower_Controller>().Get_Tower_Power_Up(Type_Number);
        }
        Tower_Position = POS;
        Counter_Attack = 0;
        Counter_Dead = 0;

        Summon_Bullet = Basic_Detail.Summon_Bullet;
        Move_Bullet = Basic_Detail.Move_Bullet;
        Line_Render = Basic_Detail.Line_Render;

        Player = player;
        Player_Number = player_Number;
        Slot_Number = slot_Number;
        Type_Number = type_Number;
        Combine_Up_Point = combine_point_up;
        Critical_Rate = (int)critical_rate;

        if (Critical_Rate < 5)
            Critical_Rate = 5;

        if (Critical_Rate > 50)
            Critical_Rate = 50;

        if (isServer)
        {
            GameObject Object_Manager = GameObject.Find("Object_Manager");
            Critical_Damage = Object_Manager.GetComponent<Object_Manager>().Get_Player_profile(Player_Number).Critical_Damage;
        }

        if (Player_Number == 1)
            gameObject.tag = "Tower_01";
        if (Player_Number == 2)
            gameObject.tag = "Tower_02";
        if (Player_Number == 3)
            gameObject.tag = "Tower_03";
        if (Player_Number == 4)
            gameObject.tag = "Tower_04";

        in_Pool = false;

        if (isServer)
        {
            Server_Tower_Set_Visible();
            Server_Prepare_Pool_Active_Tower((short)Player_Number, (short)slot_Number, transform.position);
            Update_Tower_Status();
            Server_Prepare_Update_Tower_Info(player_Number, slot_Number, type_Number, combine_point_up, Level, player, Summon_Bullet, Move_Bullet,
                Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time, Basic_HP_For_Solider, Heal_HP, Special_Rate_1,
                Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, Drag_Tower_Number, POS);
            if (!player)
                player = Get_Online_Player_Object_From_Object_Manager(Player_Number);
            if (player)
            {
                NetworkConnection conn = player.GetComponent<NetworkIdentity>().connectionToClient;
                Target_Set_Tower_To_Local_Manager(conn);
            }
            Map_Manager = GM.GetComponent<GameMaster>().Map_Manager;
        }
    }

    void Local_Update_Tower_Info(int player_Number, int slot_Number, int type_Number, int combine_point_up, int level, GameObject player,
        bool summon_bullet, bool move_bullet, bool line_render, float dmg, float spd, float add_core_hp, float special_effect_time,
        float basic_hp_for_solider, float heal_hp, float S_Rate_1, float S_Rate_2, float S_Rate_3, int critical_rate,
        int critical_damage, int Drag_Tower_Number, Vector3 tower_poisition)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        local_manager = m_local_Manager.GetComponent<Local_Manager>();
        Map_Manager = GameObject.Find("Map_Manager");

        //local_manager.Player_Number = player_Number;
        Level = level;
        One_VS_One = local_manager.One_VS_One;
        Two_VS_Two = local_manager.Two_VS_Two;
        Two_Cooperation = local_manager.Two_Cooperation;
        Four_Cooperation = local_manager.Four_Cooperation;

        local_manager.Add_Tower_To_Array_Group(gameObject, player_Number, slot_Number);


        Player_Number = player_Number;
        Damage = dmg;
        Speed = spd;
        Add_Core_HP = add_core_hp;
        Special_Effect_Time = special_effect_time;
        Basic_HP_For_Solider = basic_hp_for_solider;
        Heal_HP = heal_hp;
        Special_Rate_1 = S_Rate_1;
        Special_Rate_2 = S_Rate_2;
        Special_Rate_3 = S_Rate_3;
        Summon_Bullet = summon_bullet;
        Move_Bullet = move_bullet;
        Line_Render = line_render;
        Setup_Finish = false;
        Critical_Damage = critical_damage;
        Dragger = false;
        End_Drag = false;
        Update_Tower_Info(player_Number, slot_Number, type_Number, combine_point_up, level, player, critical_rate, 0, 0, tower_poisition);
        Update_Tower_Icon();
        in_Pool = false;

        Tower_Position = tower_poisition;
        local_manager.Check_All_Chain(player_Number);
        local_manager.Check_All_Group_Effect(player_Number);
        local_manager.Local_Check_All_Group_And_Screen_Bouns(player_Number);
    }

    #endregion

    #region about Drag Tower
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Player_Number != GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Player_Number)
            return;

        m_collider.enabled = false;
        Dragger = true;
        Start_Dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Player_Number != GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Player_Number)
            return;

        //Debug.Log("OnDrag");
        gameObject.name = "Dragger";
        m_collider.isTrigger = true;
        // Vector3.up makes it move in the world x/z plane.
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        float distamce;
        if (plane.Raycast(ray, out distamce))
        {
            transform.position = ray.origin + ray.direction * distamce;
        }
        if (Start_Dragging)
        {
            Start_Dragging = false;
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            m_local_Manager.GetComponent<Local_Manager>().Start_Drag_Black_Cannot_Combine_Tower(gameObject, Type_Number, Combine_Up_Point, Player_Number);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_collider.enabled = true;
        StartCoroutine("Check_Collider_Other_Tower");
    }

    public void Reset_Position()
    {
        transform.position = Tower_Position;
    }

    IEnumerator Check_Collider_Other_Tower()
    {
        yield return new WaitForSeconds(0.3f);
        if (!collider_hit_Tower)
        {
            Reset_Position();
            Dragger = false;
            End_Drag = false;
            GameObject m_local_Manager = GameObject.Find("Local_Manager");
            m_local_Manager.GetComponent<Local_Manager>().End_Drag_Reset_Tower(gameObject, Type_Number, Combine_Up_Point);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<Tower>())
            return;

        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        bool Target_Tower_is_Own_Tower = false, Drag_Tower_is_Own_Tower = false, Same_Tag = false, Drag_is_Dragger = false, allow_Combine = false;

        if (other.tag == gameObject.tag)
            Same_Tag = true;

        if (Player_Number == m_local_Manager.GetComponent<Local_Manager>().Player_Number)
            Target_Tower_is_Own_Tower = true;

        if (other.GetComponent<Tower>().Player_Number == m_local_Manager.GetComponent<Local_Manager>().Player_Number)
            Drag_Tower_is_Own_Tower = true;

        if (other.GetComponent<Tower>().Dragger)
            Drag_is_Dragger = true;

        if (Target_Tower_is_Own_Tower && Drag_Tower_is_Own_Tower && Same_Tag && !Dragger && Drag_is_Dragger && !isServer)
            allow_Combine = true;

        if (!allow_Combine)
        {
            Drag_Tower_is_Own_Tower = false;
            other.GetComponent<Tower>().Reset_Position();
            other.GetComponent<Tower>().Dragger = false;
            other.GetComponent<Tower>().End_Drag = false;
            m_local_Manager.GetComponent<Local_Manager>().End_Drag_Reset_Tower(other.gameObject, Type_Number, Combine_Up_Point);
        }

        if (allow_Combine)
        {
            other.GetComponent<Tower>().collider_hit_Tower = true;
            other.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            GameObject Drag_Tower = other.gameObject;
            GameObject Target_Tower = gameObject;

            if (!Player)
                Player = GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Get_Local_Player();
            if (Player)
                Player.GetComponent<Player_Network>().CmdCombine_Tower_Check(Drag_Tower, Target_Tower);
        }
    }
    #endregion

    #region Tower Status , ATK , Speed , Distance etc ...

    public void Update_Tower_Status_To_Local(GameObject Player_Obj)
    {
        Update_Tower_Status();
        NetworkConnection Conn = Player_Obj.GetComponent<NetworkIdentity>().connectionToClient;
        if (Conn != null)
            Target_Update_Tower_Info(Conn, Player_Number, Slot_Number, Type_Number,
        Combine_Up_Point, Level, Player, Summon_Bullet, Move_Bullet, Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time,
        Basic_HP_For_Solider, Heal_HP, Special_Rate_1, Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, 0,
        Tower_Position);
    }

    public void Update_Tower_Status()
    {
        Basic_Detail = gameObject.GetComponent<Tower_Info>().Tower_Basic_Info(Type_Number);
        Combine_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Comine_Up_Detail(Type_Number);
        Power_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Level_Up_Detail(Type_Number);
        Level_Up_Detail = gameObject.GetComponent<Tower_Info>().Tower_Level_Up_Detail(Type_Number);

        float Basic_Damage = Update_Basic_Damage();
        float Core_Bouns_Damage = Update_Core_Bouns_Damage();
        float Core_HP = Core.GetComponent<Core>().Core_HP;
        Basic_Damage_Bouns = Tower_Controller.GetComponent<Tower_Controller>().Get_Basic_Damage_Bouns(Player_Number);
        Damage = Calculate_Basic_Damage();

        if (Type_Number == 608)
        {
            Debug.Log("Update_Tower_Status || 608 || Basic_Damage || " + Basic_Damage + " || Core_Bouns_Damage || " + Core_Bouns_Damage
                + " || Core_HP || " + Core_HP + " || Basic_Damage_Bouns || " + Basic_Damage_Bouns + " || Damage || " + Damage);
        }

        Speed = Update_Basic_Speed();
        Distance = Update_Basic_Distance();
        Add_Core_HP = Update_Basic_Add_Core_HP();

        Special_Effect_Time = Update_Basic_Special_Effect_Time();

        Heal_HP = Update_Basic_Heal_HP(Core_HP);

        Special_Rate_1 = Update_Basic_Special_Rate_1();
        Special_Rate_2 = Update_Basic_Special_Rate_2();
        Special_Rate_3 = Update_Basic_Special_Rate_3();

        float basic_hp_for_solider = Update_Basic_HP_For_Solider();
        Basic_HP_For_Solider = basic_hp_for_solider * Special_Rate_1 * (Core_HP / 1000);
        if (Basic_HP_For_Solider < 1)
            Basic_HP_For_Solider = 1;

        Basic_Attack_For_Solider = Damage + Special_Rate_2 * (Core_HP / 1000);
        if (Basic_Attack_For_Solider < 1)
            Basic_Attack_For_Solider = 1;

        if (Type_Number == 608)
        {
            Debug.Log("608 || Basic_HP_For_Solider || " + Basic_HP_For_Solider + " || Basic_Attack_For_Solider || " +
                Basic_Attack_For_Solider);
        }

        in_Pool = false;
    }

    float Calculate_Basic_Damage()
    {
        float Basic_Damage = Update_Basic_Damage();
        float Core_Bouns_Damage_Rate = Update_Core_Bouns_Damage();
        float Core_HP = Core.GetComponent<Core>().Core_HP;

        float Basic_and_Bouns = Basic_Damage + Basic_Damage_Bouns;
        float Core_Rate_for_Damage = Core_Bouns_Damage_Rate * (Core_HP / 100);

        float Core_Bouns = Core_HP / 10000;
        if (Core_Bouns < 1)
            Core_Bouns = 1;

        float dmg = (Basic_and_Bouns + Core_Rate_for_Damage + Core_HP) * Core_Bouns;

        if (Type_Number == 608)
        {
            Debug.Log("608 || Calculate_Basic_Damage ||  Basic_Damage || " + Basic_Damage + " || Basic_Damage_Bouns || " +
                Basic_Damage_Bouns + " || Core_Bouns_Damage_Rate || " + Core_Bouns_Damage_Rate + " || Core_HP || " +
                Core_HP + " || dmg || " + dmg);
        }
        return dmg;
    }

    float Update_Basic_Damage()
    {
        float Basic_Value = Basic_Detail.Damage;
        float Combine_Value = Combine_Up_Detail.Damage * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Damage * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Damage * (Level - 1);
        float Core_Value = Core.GetComponent<Core>().Core_Basic_Damage;
        float value = Basic_Value + Combine_Value + Level_Value + Core_Value + Power_Up_Value;
        return value;
    }

    float Update_Core_Bouns_Damage()
    {
        float Basic_Value = Basic_Detail.Core_Bouns_Rate;
        float Combine_Value = Combine_Up_Detail.Core_Bouns_Rate * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Core_Bouns_Rate * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Core_Bouns_Rate * (Level - 1);
        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Speed()
    {
        float temp_speed_bouns = Speed_Bouns, speed_bouns = 0;
        float Basic_Value = Basic_Detail.Speed;
        float Combine_Value = Combine_Up_Detail.Speed * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Speed * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Speed * (Level - 1);
        if (Speed_Bouns > 75)
            temp_speed_bouns = 75f;

        speed_bouns = 1 - (temp_speed_bouns / 100);
        float value = (Basic_Value + Combine_Value + Level_Value + Power_Up_Value) * speed_bouns;
        return value;
    }

    float Update_Basic_Distance()
    {
        float Basic_Value = Basic_Detail.Distance;
        float Combine_Value = Combine_Up_Detail.Distance * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Distance * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Distance * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Add_Core_HP()
    {
        float Basic_Value = Basic_Detail.Add_Core;
        float Combine_Value = Combine_Up_Detail.Add_Core * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Add_Core * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Add_Core * (Level - 1);
        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Special_Effect_Time()
    {
        float Basic_Value = Basic_Detail.Special_Effect_Time;
        float Combine_Value = Combine_Up_Detail.Special_Effect_Time * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Special_Effect_Time * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Special_Effect_Time * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_HP_For_Solider()
    {
        float Basic_Value = Basic_Detail.Basic_HP_For_Solider;
        float Combine_Value = Combine_Up_Detail.Basic_HP_For_Solider * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Basic_HP_For_Solider * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Basic_HP_For_Solider * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Heal_HP(float core_hp)
    {
        float Basic_Value = Basic_Detail.Heal_HP + (core_hp * (Update_Core_Bouns_Damage() / 100));
        float Combine_Value = Combine_Up_Detail.Heal_HP * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Heal_HP * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Heal_HP * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Special_Rate_1()
    {
        float Basic_Value = Basic_Detail.Special_Rate_1;
        float Combine_Value = Combine_Up_Detail.Special_Rate_1 * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Special_Rate_1 * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Special_Rate_1 * (Level - 1);

        float Lucky_Value = Lucky_Bouns;
        float Special_Type_Lucky_Value = Get_Special_Type_Lucky_Value();
        if (Special_Type_Lucky_Value <= 0) // check tower is counter type .....
            Lucky_Value = Special_Type_Lucky_Value * Lucky_Bouns; // counter type is -1 -2 

        float value = Basic_Value + Combine_Value + Level_Value + Lucky_Value + Power_Up_Value;
        if (Special_Type_Lucky_Value <= 0)
            return value;
        if (Special_Type_Lucky_Value > 0)
        {
            float Max_Value = Check_Special_Rate_Maximum();
            if (value > Max_Value && Check_Special_Rate_Maximum() != 0)
                value = Max_Value;
        }
        return value;
    }

    float Update_Basic_Special_Rate_2()
    {
        float Basic_Value = Basic_Detail.Special_Rate_2;
        float Combine_Value = Combine_Up_Detail.Special_Rate_2 * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Special_Rate_2 * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Special_Rate_2 * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Update_Basic_Special_Rate_3()
    {
        float Basic_Value = Basic_Detail.Special_Rate_3;
        float Combine_Value = Combine_Up_Detail.Special_Rate_3 * (Combine_Up_Point - 1);
        float Power_Up_Value = Power_Up_Detail.Special_Rate_3 * (Power_Up_Point - 1);
        float Level_Value = Level_Up_Detail.Special_Rate_3 * (Level - 1);

        float value = Basic_Value + Combine_Value + Level_Value + Power_Up_Value;
        return value;
    }

    float Check_Special_Rate_Maximum()
    {
        float Maximum_Value = 0;
        switch (Type_Number)
        {
            case (206):
            case (301):
            case (305):
            case (605):
                Maximum_Value = 50;
                break;
            case (610):
            case (402):
                Maximum_Value = 40;
                break;
            case (701):
                Maximum_Value = 30;
                break;
            case (715):
            case (707):
                Maximum_Value = 25;
                break;
            case (708):
                Maximum_Value = 10;
                break;
        }
        return Maximum_Value;
    }

    float Get_Special_Type_Lucky_Value()
    {
        float Special_Value = 1;
        switch (Type_Number)
        {
            case (711):
                Special_Value = 0;
                break;

            case (603):
            case (706):
            case (709):
            case (710):
            case (714):
                Special_Value = -1;
                break;
            case (602):
            case (713):
                Special_Value = -2;
                break;
            case (716):
                Special_Value = -5;
                break;
        }
        return Special_Value;
    }

    public void Set_Tower_Bouns(float exp, float speed, float basic_Attack, float lucky, float critical, float critical_dmg)
    {
        Basic_Damage_Bouns = basic_Attack;
        EXP_Bouns = exp;
        Speed_Bouns = speed;
        Critical_Rate = (int)critical;
        Critical_Damage = (int)critical_dmg;
        Lucky_Bouns = lucky;
    }
    #endregion

    void Update()
    {
        if (in_Pool)
            return;

        if (isServer)
        {
            Timer_Add_Core_HP += Time.deltaTime;
            if (Timer_Add_Core_HP >= 1)
            {
                if (Add_Core_HP < 20 && Type_Number == 704)
                    Debug.Log("Add_Core_HP || " + Add_Core_HP);

                Timer_Add_Core_HP = 0;
                Core.GetComponent<Core>().Core_Add_HP(Add_Core_HP);
            }

            Timer += Time.deltaTime;
            if (Type_Number == 201)
                Speed = 1.0f;
            if (Timer >= Speed)
            {
                // Type 1 = Find 1 Target to shot
                // Type 2 = Find Area Enemy
                // Type 3 = Summon Protector // Attacker
                // Type 4 = Heal Area Protector
                // Type 5 = no need target , buff tower
                // Type 6 = Full Screen to all enemy
                int Tower_Target_Type = Get_Tower_Target_Type();
                if (Tower_Target_Type == 1)
                {
                    string Type = "null";
                    if (Type_Number == 707)
                        Type = "Resurrection";
                    if (Type_Number == 708)
                        Type = "Treasure";
                    GameObject[] Closest_Core_Enemy = Get_Opponent_Enemy_Obj_Form_Map_Manager();
                    if (Closest_Core_Enemy.Length == 0)
                        return;
                    if (Closest_Core_Enemy[0] == null)
                        return;
                    Closest_Core_Enemy[0] = Search_Closest_Core_Enemy_In_Area(Type, Closest_Core_Enemy);
                    GameObject Closest_Enemy = Closest_Core_Enemy[0];


                    if (Closest_Core_Enemy[0] != null)
                        Tower_Action(Tower_Target_Type, Closest_Core_Enemy);
                }
                if (Tower_Target_Type == 2)
                {
                    GameObject[] Enemy_In_Area = Get_Opponent_Enemy_Obj_Form_Map_Manager();
                    if (Enemy_In_Area.Length == 0)
                        return;
                    Enemy_In_Area = Get_and_Sort_Enemy_Obj(Enemy_In_Area);
                    if (Enemy_In_Area[0] != null)
                        Tower_Action(Tower_Target_Type, Enemy_In_Area);
                }
                if (Tower_Target_Type == 4)
                {
                    GameObject[] Protector_And_Attacker_In_Area = Get_All_Team_Attacker_Protector_Obj_Form_Map_Manager();
                    if (Protector_And_Attacker_In_Area.Length == 0)
                        return;
                    Protector_And_Attacker_In_Area = Get_and_Sort_Enemy_Obj(Protector_And_Attacker_In_Area);
                    if (Protector_And_Attacker_In_Area[0] != null)
                        Tower_Action(Tower_Target_Type, Protector_And_Attacker_In_Area);
                }
                if (Tower_Target_Type == 3 || Tower_Target_Type == 5)
                {
                    GameObject[] no_need_Enemy = new GameObject[0];
                    Tower_Action(Tower_Target_Type, no_need_Enemy);
                }
                if (Tower_Target_Type == 6)
                {
                    GameObject[] All_Enemy_and_Enemy_Protector = Get_All_Opponent_Obj_Form_Map_Manager();
                    if (All_Enemy_and_Enemy_Protector.Length == 0)
                        return;
                    if (All_Enemy_and_Enemy_Protector.Length != 0)
                        Tower_Action(Tower_Target_Type, All_Enemy_and_Enemy_Protector);
                }
            }
            Update_Test_Text();
        }
    }

    void Update_Test_Text()
    {
        //public GameObject Test_S, Test_D, Test_E, Test_T, Test_R1, Test_R2, Test_L
        Test_S.GetComponent<Text>().text = Level.ToString();
        Deactive(Test_D);
        Deactive(Test_E);
        Deactive(Test_C);
        Deactive(Test_R1);
        Deactive(Test_R2);
        Deactive(Test_L);
        //Test_D.GetComponent<Text>().text = Damage.ToString();
        //Test_E.GetComponent<Text>().text = Special_Effect_Time.ToString();
        //Test_C.GetComponent<Text>().text = Add_Core_HP.ToString();
        //Test_R1.GetComponent<Text>().text = Special_Rate_1.ToString();
        //Test_R2.GetComponent<Text>().text = Special_Rate_2.ToString();
        //Test_L.GetComponent<Text>().text = Level.ToString();

        void Deactive(GameObject _obj)
        {
            if (_obj.activeSelf)
                _obj.SetActive(false);
        }
    }

    int Get_Tower_Target_Type()
    {
        int Type = 0;
        switch (Type_Number)
        {
            case 101:
            case 102:
            case 103:
            case 201:
            case 203:
            case 204:
            case 205:
            case 206:
            case 301:
            case 302:
            case 303:
            case 305:
            case 403:
            case 406:
            case 503:
            case 507:
            case 609: // Fall Horse
            case 610:
            case 611:
            case 612:
            case 719:
            case 707:
            case 708:
                Type = 1;
                // Shot 1 Target
                break;

            case 505:
                Type = 2;
                // Area All Enemy
                break;

            // Protector
            case 104:
            case 502:
            case 607: // Devil 
                        // Attacker
            case 202:
            case 501:
            case 504: // Theft
            case 506: // Boomer
            case 606: // Devil
            case 608: // Dragon
                Type = 3;
                break;

            case 105:
            case 715:
                // Heal Area Protector
                Type = 4;
                break;

            case 304:
            case 404:
            case 405:
            case 602:
            case 603:
            case 604:
            case 605:
            case 701:
            case 702:
            case 703:
            case 704:
            case 705:
            case 706:
            case 709:
            case 710:
            case 711:
            case 712:
            case 713:
            case 714:
            case 716:
            case 717:
            case 718:
            case 601:
                // no need target , buff tower
                Type = 5;
                break;
            case 401:
            case 402:
                // Full Screen to all enemy
                Type = 6;
                break;
        }
        return Type;
    }

    void Tower_Action(int attack_type, GameObject[] Target)
    {
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }
        //if (Type_Number == 403)
        //    Debug.Log("attack_type || " + attack_type + " || Type_Number || " + Type_Number);
        bool Heal_or_Damage = false;
        Update_Tower_Status();
        Tower_To_Target_Info Tower_Packet = new Tower_To_Target_Info(null, false, false, 0, 0, 0, 0, 0);
        Tower_To_Target_Info temp_Packet = new Tower_To_Target_Info(null, false, false, 0, 0, 0, 0, 0);
        bool Tower_Shot = false;
        int Protector_Number = GM.GetComponent<GameMaster>().Player1_Protector_Number;

        switch (attack_type)
        {
            case (1): // Type 1 = Find 1 Target to shot
                Heal_or_Damage = false;

                if (Type_Number != 403)
                    Timer = 0;

                Timer = 0;
                switch (Type_Number)
                {
                    case 101:
                    case 201:
                    case 203:
                    case 204:
                    case 206:
                    case 301:
                    case 302:
                    case 305:
                        // target(obj), instant_Bullet(bool), fix_Time_Bullet(bool), float fix_Time(float), 
                        // bullet_Speed(float), over_Time(float), damage(float), effect_Time(float)
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, Special_Effect_Time, Damage, 0);
                        Tower_Shot = true;
                        break;
                    case 102:
                        // Instant_Bullet = false , bullet Fix_Time = true , bullet_fixed speed = 0.5
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, true, 0.5f, 10, 0, Damage, 0);
                        Tower_Shot = true;
                        break;
                    case 103:
                        // Instant_Bullet = false , bullet Fix_Time = true , bullet_fixed speed = 0.5
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, false, 0.5f, 0, 0, Damage, 0);
                        Tower_Shot = true;
                        break;
                    case 205:
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, false, 1.0f, 0, 0, Damage, 0);
                        Tower_Shot = true;
                        break;
                    case 303:
                        GameObject[] Target_List = new GameObject[11];
                        GameObject[] Enemy_Array = Get_Opponent_Enemy_Obj_Form_Map_Manager();
                        Target_List[0] = Target[0];
                        GameObject Start_Point = null;
                        for (int i = 0; i < Special_Rate_1 + 1; i++)
                        {
                            if (Target_List[i] != null)
                            {
                                Tower_Packet = new Tower_To_Target_Info(Target_List[i], true, false, 1.0f, 0, 0, Damage, 0);
                                Tower_Packet.Action_Code = (short)Tower_Code.Damage;
                                Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Spawn_None;
                                Shot_Bullet_To_Closest_Core_Single_Enemy(Tower_Packet);
                                Server_Prepare_Target_Lightning_To_All_Client(Target_List[i]);
                                GameObject target = Search_Closest_Enemy(Target_List[i], Target_List, Enemy_Array);
                                if (target != null)
                                {
                                    Target_List[i + 1] = target;
                                    Start_Point = Target_List[i];
                                }
                            }
                        }
                        break;
                    case 403:
                        if (Protector_Number < 12)
                        {
                            Tower_Shot = true;
                            Timer = 0;
                            Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                            Tower_Packet.Action_Code = (short)Tower_Code.None;
                            Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Basic;
                        }
                        break;
                    case 503:
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        Tower_Packet.Action_Code = (short)Tower_Code.None;
                        Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Basic;
                        break;
                    case 406:
                        Tower_Shot = true;
                        GameObject[] All_Enemy_and_Enemy_Protector = Get_All_Opponent_Obj_Form_Map_Manager();
                        int Random_Number = Random.Range(0, All_Enemy_and_Enemy_Protector.Length);
                        Target[0] = All_Enemy_and_Enemy_Protector[Random_Number];
                        // target(obj), instant_Bullet(bool), fix_Time_Bullet(bool), float fix_Time(float), 
                        // bullet_Speed(float), over_Time(float), damage(float), effect_Time(float)
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, false, 0.7f, 0, 0, Damage, 0);
                        break;
                    case 507:
                        Tower_Shot = false;
                        GameObject[] All_Enemy_and_Enemy_Protector_507 = Get_All_Opponent_Obj_Form_Map_Manager();
                        Target[0] = Get_High_HP_Single_By_Array(All_Enemy_and_Enemy_Protector_507);
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, false, 1.0f, 0, 0, Damage, 0);
                        Tower_Packet.Action_Code = (short)Tower_Code.Damage;
                        Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Spawn_None;
                        Shot_Bullet_To_Closest_Core_Single_Enemy(Tower_Packet);
                        Server_Prepare_Target_Lightning_To_All_Client(Target[0]);
                        break;
                    case 609:
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        Tower_Packet.Action_Code = (short)Tower_Code.None;
                        Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Horse;
                        break;
                    case 610:
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        break;
                    case 611:
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        break;
                    case 612: // Instant Bullet
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, false, 0.5f, 0, 0, Damage, 0);
                        break;
                    case 707:
                    case 719:
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        break;
                    case 708: // Instant Bullet
                        Tower_Shot = true;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], false, false, 0, 10, 0, Damage, 0);
                        break;
                }
                break;
            case (2):
                // Type 2 = Find Area Enemy
                switch (Type_Number)
                {
                    case (505):
                        for (int i = 0; i < Target.Length; i++)
                        {
                            if (Target[i] != null)
                            {
                                Timer = 0;
                                float distance = Vector3.Distance(Target[i].transform.position, Tower_Position);
                                if (Special_Effect_Time > i && distance <= Distance)
                                {
                                    float dmg = (Damage * Special_Rate_1) * (i + 1);
                                    Tower_Packet = new Tower_To_Target_Info(Target[i], false, true, 0.5f, 0, 0, dmg, 0);
                                    Tower_Packet.Action_Code = (short)Tower_Code.Damage;
                                    Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Horse;
                                    Shot_Bullet_To_Closest_Core_Single_Enemy(Tower_Packet);
                                }
                            }
                        }
                        break;
                }
                break;
            case (3): // Type 3 = Summon Protector // Attacker
                Spwan_Enemy_Packet packet = new Spwan_Enemy_Packet();
                packet.Spawn_Code = (short)Enemy_Code.Normal_Spwan;
                packet.POS = Vector3.zero;
                packet.Speed = 1;
                switch (Type_Number)
                {
                    case (104):
                        // Summon Protector
                        if (Protector_Number >= 12)
                            return;

                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(1);
                        Spwan_Protector(packet); // Normal Protector
                        break;
                    case (502):
                        // Summon Protector
                        Protector_Number = GM.GetComponent<GameMaster>().Player1_Protector_Number;
                        if (Protector_Number >= 12)
                            return;

                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(2);
                        packet.Speed = 2;
                        Spwan_Protector(packet); // Horse_Protector
                        break;
                    case (202): // Summon Attacker
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(1);
                        Spwan_Attacker(packet); // 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief
                        break;
                    case (501): // Summon Attacker
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(2);
                        packet.Speed = 2;
                        Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief
                        break;
                    case (504): // Summon Thief
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(3);
                        packet.Speed = 0.8f;
                        Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief
                        break;
                    case (506): // Summon Boomer
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(4);
                        Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer
                        break;
                    case (606): // Summon Devil Attacker
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(5);
                        Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer, 5 Devil
                        break;
                    case (607): // Summon Devil Protector
                        Protector_Number = GM.GetComponent<GameMaster>().Player1_Protector_Number;
                        if (Protector_Number >= 12)
                            return;

                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(5);
                        Spwan_Protector(packet); // 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer, 5 Devil
                        break;
                    case (608): // Summon Dragon
                        Timer = 0;
                        packet.Type_Code = Number_To_Enemy_Code_Type(6);
                        Spwan_Attacker(packet);// 6 Dragon
                        break;
                }
                break;
            case (4): // Type 4 = Heal Area Protector
                Add_Attack_Time_To_Tower_Controller();
                Heal_or_Damage = true;
                switch (Type_Number)
                {
                    case (105):
                        Tower_Shot = true;
                        Timer = 0;
                        Tower_Packet = new Tower_To_Target_Info(Target[0], true, true, 0.5f, 0, 0, Heal_HP, 0);
                        Tower_Packet.Action_Code = (short)Tower_Code.Heal;
                        break;
                    case (715):

                        GameObject Non_Armor_Protector_or_Attacker = Search_Closest_Core_Enemy_In_Area("Armor", Target);
                        if (Non_Armor_Protector_or_Attacker != null)
                        {
                            Timer = 0;
                            Tower_Packet = new Tower_To_Target_Info(Non_Armor_Protector_or_Attacker, true, true, 0.5f, 0, 0, Heal_HP, 0);
                            Tower_Packet.Action_Code = (short)Tower_Code.Heal;
                            Tower_Shot = true;
                        }
                        break;
                }
                break;
            case (5): // Type 5 = no need target , buff tower
                switch (Type_Number)
                {
                    case (304): // Bank
                    case (705): // Bank
                        Timer = 0;
                        int Gold = (int)Special_Rate_1;
                        GM.GetComponent<GameMaster>().Add_Gold(Player_Number, Gold);
                        Server_Prepare_Set_Gold_Bar(Gold);
                        Add_Attack_Time_To_Tower_Controller();
                        break;
                    case (404): // Blood_Core
                        Timer = 0;
                        Server_Prepare_Shot_Effect();
                        Add_Attack_Time_To_Tower_Controller();
                        break;
                    case (601):
                        Timer = 0;
                        string Tag = Get_Target_Tag();
                        Set_Chain(Chain_Weapon_Point_Top);
                        Set_Chain(Chain_Weapon_Point_Right);
                        Set_Chain(Chain_Weapon_Point_Buttom);
                        Set_Chain(Chain_Weapon_Point_Left);

                        void Set_Chain(GameObject _Obj)
                        {
                            if (_Obj == null)
                                return;
                            if (_Obj != null)
                                _Obj.GetComponent<Object_Status>().Set_Chain_Weapon_Point(Damage, Tag, Critical_Rate,
                                    Critical_Damage, 1.0f, Tower_Controller);
                        }
                        break;
                    case (704): // Blood_Pool_Big
                        Timer = 0;
                        Server_Prepare_Shot_Effect();
                        Add_Attack_Time_To_Tower_Controller();
                        break;
                    case (711):
                        GameObject target_Tower = Tower_Controller.GetComponent<Tower_Controller>().Get_Non_Same_Type_Tower(711);
                        if (target_Tower != null)
                        {
                            Add_Attack_Time_To_Tower_Controller();
                            target_Tower.GetComponent<Tower>().Timer += 1;
                            Timer = 0;
                            Server_Prepare_Shot_Effect();
                            target_Tower.GetComponent<Tower>().Server_Prepare_Specify_Shot_Effect(711);
                        }
                        break;
                    case (712):
                        GameObject[] All_Own_Enemy_Protector_Attacker = Get_All_Team_Obj_Form_Map_Manager();
                        if (All_Own_Enemy_Protector_Attacker.Length > 0)
                        {
                            Timer = 0;
                            Add_Attack_Time_To_Tower_Controller();
                            Heal_All_Target(All_Own_Enemy_Protector_Attacker);
                        }
                        break;
                    case (717):
                        Timer2 += Time.deltaTime;
                        if (Timer2 >= 1)
                        {
                            Timer2 = 0;
                            if (Timer > Speed)
                            {
                                Tower_Controller tc = Tower_Controller.GetComponent<Tower_Controller>();
                                Target_Tower = tc.Get_Neighbor_Tower_Same_Combine_Point(Slot_Number, Combine_Up_Point);
                                if (Target_Tower != null)
                                {
                                    Lock = true;
                                    Target_Tower.GetComponent<Tower>().Server_Prepare_Specify_Shot_Effect(717);
                                    Server_Prepare_Specify_Shot_Effect(7172);
                                    Timer = 0;
                                    tc.Level_Up_Tower(Target_Tower);
                                    tc.Level_Down_Tower(gameObject);
                                    return;
                                }
                            }
                            if (Timer > (Speed * 2))
                            {
                                Tower_Controller tc = Tower_Controller.GetComponent<Tower_Controller>();
                                Target_Tower = tc.Get_Random_Neighbor_Tower(Slot_Number);
                                if (Target_Tower != null)
                                {
                                    Lock = true;
                                    Target_Tower.GetComponent<Tower>().Server_Prepare_Specify_Shot_Effect(717);
                                    Server_Prepare_Specify_Shot_Effect(7172);
                                    Timer = 0;
                                    tc.Level_Up_Tower(Target_Tower);
                                    tc.Level_Down_Tower(gameObject);
                                    return;
                                }
                            }
                        }
                        break;
                    case (718):
                        GameObject target_Tower_718 = Tower_Controller.GetComponent<Tower_Controller>().Get_Non_Same_Type_Tower(718);
                        if (target_Tower_718 != null)
                        {
                            Timer = 0;
                            Add_Attack_Time_To_Tower_Controller();
                            float Lucky = target_Tower_718.GetComponent<Tower>().Lucky_Bouns;
                            Debug.Log("Lucky 1 || " + Lucky + " || " + Speed + " || " + Slot_Number);
                            target_Tower_718.GetComponent<Tower>().Lucky_Bouns += Lucky_Bouns;
                            Lucky = target_Tower_718.GetComponent<Tower>().Lucky_Bouns;
                            Server_Prepare_Shot_Effect();
                            target_Tower_718.GetComponent<Tower>().Server_Prepare_Specify_Shot_Effect(718);
                        }
                        break;
                }
                break;
            case (6): // Type 6 = Full Screen to all enemy                    case 401:
                float Effect_Time = 0;
                Timer = 0;
                Add_Attack_Time_To_Tower_Controller();
                switch (Type_Number)
                {
                    case (401):
                        Effect_Time = 1.5f;
                        break;
                    case (402):
                        Effect_Time = Special_Effect_Time;
                        break;
                }
                for (int i = 0; i < Target.Length; i++)
                {
                    string Name = Target[i].name;
                    //Target[i].GetComponent<Enemy>().Enemy_Damage(Damage, Heal_or_Damage, Critical_Rate, Critical_Damage, Name, "Full_Ice");
                    if (Type_Number == 402)
                    {
                        Target[i].GetComponent<Enemy>().Ice_01_Start = true;
                        Target[i].GetComponent<Enemy>().Ice_01_Time = Special_Effect_Time;
                        Target[i].GetComponent<Enemy>().Slow_Rate = Special_Rate_1;
                    }
                }
                Server_Prepare_All_Screen_Enemy_Effect(Type_Number, 0.0f, Player_Number, Damage, Effect_Time);
                break;
        }

        if (Tower_Shot)
        {
            Tower_Packet.Action_Code = (short)Tower_Code.Damage;
            Tower_Packet.Spawn_Enemy_Code = (short)Enemy_Code.Spawn_None;
            Shot_Bullet_To_Closest_Core_Single_Enemy(Tower_Packet);
        }
    }

    void Spwan_Protector(Spwan_Enemy_Packet packet)// (bool Normal_or_Fall_Protector, float Speed, Vector3 pos, int Type)
    {
        Add_Attack_Time_To_Tower_Controller();
        if (packet.Spawn_Code == (short)Enemy_Code.Normal_Spwan) // Normal Protector
        {
            Server_Prepare_Shot_Effect();
        }

        packet.Player_Number = (short)Player_Number;
        packet.HP = Basic_HP_For_Solider;
        packet.Damage = Damage;
        packet.Tower = gameObject;

        //int Player_Number, float Portector_HP, float Protector_DMG, float Protector_SPD
        GM.GetComponent<GameMaster>().Spwan_Protector(packet);
    }

    void Spwan_Attacker(Spwan_Enemy_Packet packet)//(bool Normal_or_Fall_Attacker, float Speed, Vector3 pos, int Type) // 1 Normal , 2 Horse , 3 Thief
    {
        packet.Player_Number = (short)Player_Number;
        packet.HP = Basic_HP_For_Solider;
        packet.Damage = Damage;
        packet.Tower = gameObject;

        Add_Attack_Time_To_Tower_Controller();
        Server_Prepare_Shot_Effect();
        //int Player_Number, float Portector_HP, float Protector_DMG, float Protector_SPD
        GM.GetComponent<GameMaster>().Spwan_Attacker(packet);
    }

    void Shot_To_All_Target_In_Area(GameObject[] Target_In_Area, bool Heal, float value, bool Instant_Bullet, bool fix_Time, float Bullet_Speed)
    {
        for (int i = 0; i < Target_In_Area.Length - 1; i++)
        {
            // action to area target
        }
    }

    void Shot_Bullet_To_Closest_Core_Single_Enemy(Tower_To_Target_Info packet)
    {
        // Type = Spawn Enemy Code
        short Spawn_Enemy_Code = packet.Spawn_Enemy_Code;
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }
        if (Type_Number == 503 && Spawn_Enemy_Code == (short)Enemy_Code.Spawn_None)
            Debug.LogWarning(Type_Number + " || Spawn_Enemy_Code || " + Spawn_Enemy_Code + " || " + gameObject.name + " || Tag || "
                + gameObject.tag + " || " + packet.Target);
        // target(obj), instant_Bullet(bool), fix_Time_Bullet(bool), float fix_Time(float), 
        // bullet_Speed(float), over_Time(float), damage(float), effect_Time(float)
        float Distance_To_Enemy = 0;
        float time = 0;
        float Over_Time = packet.Over_Time;
        packet.Enemy_Name = packet.Target.name;
        if (!packet.Instant_Bullet && !packet.Fix_Time_Bullet)
        {
            Vector3 pos = Tower_Position;
            if (Type_Number == 403 || Type_Number == 503 || Type_Number == 609) // Fall Enemy
            {
                pos = packet.Target.transform.position + new Vector3(5, 15.0f, 5);
                packet.Bullet_Speed = 20.0f;
            }

            Distance_To_Enemy = Vector3.Distance(packet.Target.transform.position, pos);
            time = Distance_To_Enemy / packet.Bullet_Speed;

            if (Type_Number == 403 || Type_Number == 503 || Type_Number == 609) // Fall Enemy
                time += 0.5f;
        }
        if (packet.Fix_Time_Bullet)
            time = packet.Fix_Time;

        packet.Time = time;

        if (Over_Time > 0)
        {
            int QTY = Mathf.RoundToInt(Over_Time);
            StartCoroutine(Over_Time_send_To_Target_Again(packet, QTY));
        }

        // Test
        if (Type_Number == 403 || Type_Number == 503 || Type_Number == 609) // Fall Enemy
        {
            float x = packet.Target.transform.position.x;
            if (x >= 10)
            {
                bool pause = packet.Target.GetComponent<Enemy>().Pause;
                string Tag = packet.Target.tag;
                Debug.LogWarning(packet.Target.gameObject.name + " || pause || " + pause + " || Tag || " + Tag);
            }
        }
        // Test

        // Send_Damage_To_Gamemaster for highest damage attack to enemy task 
        GM.GetComponent<GameMaster>().Set_High_Damage((short)Player_Number, (int)packet.Damage);
        // Send_Damage_To_Gamemaster for highest damage attack to enemy task 

        Add_Attack_Time_To_Tower_Controller();
        packet.Time = time;
        packet.Summon_Bullet = Summon_Bullet;
        packet.Effect_On = true;
        packet.Tower_Name = Tower_Number;
        //public GameObject Target; // Instant_Bullet = false , bullet Fix_Time = false , bullet_Speed = 4
        //public bool Instant_Bullet;
        //public bool Fix_Time_Bullet;
        //public float Fix_Time;
        //public float Bullet_Speed;
        //public float Over_Time; // shot target how many times .
        //public float Damage;
        //public float Effect_Time; // Slow , Stun, Posion .... how long time;
        //public short Special_Effect_Code;
        //public Spwan_Enemy_Packet packet;
        //public float Time;
        //public int Tower_Name; // if tower name is not same will stop shot (cause combine tower)
        //public bool Summon_Bullet;
        //public bool Effect_On;
        //public int Tower_Type;
        //public string Enemy_Name;
        //(bool Heal_or_Damage, Tower_To_Target_Info info, float time, bool summon_bullet, bool Effect_On,
        //            int Type, string enemy_name, int tower_name)

        StartCoroutine(wait_second_send_To_Target(packet));
        if (Type_Number == 401 || Type_Number == 402)
            return;
        Server_Prepare_Shot_Effect();
    }

    public void Counter(int Type, GameObject Enemy) // 1:Counter Attack , 2:Counter Dead
    {
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            return;
        }
        if (Type == 1) // 602
        {
            //Debug.Log("Counter || Counter_Attack || " + Counter_Attack + " || Slot_Number || " + Slot_Number);
            //Debug.Log("Counter || Special_Rate_1 || " + Special_Rate_1 + " || Special_Rate_2 || " + Special_Rate_2);
            Counter_Attack++;
            if (Counter_Attack >= Special_Rate_1)
            {
                Spwan_Enemy_Packet packet = new Spwan_Enemy_Packet();
                packet.Spawn_Code = (short)Enemy_Code.Normal_Spwan;
                packet.Speed = 1;
                packet.POS = Vector3.zero;
                packet.Type_Code = Number_To_Enemy_Code_Type(5);

                Counter_Attack = 0;
                if (Type_Number == 602)
                    Core.GetComponent<Core>().Core_Add_HP(Special_Rate_2);
                if (Type_Number == 713)
                    Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer, 5 Devil

                Server_Prepare_Shot_Effect();
            }
        }
        if (Type == 2) // 603 , 709 , 710
        {
            Counter_Dead++;
            //if (Type_Number == 714)
            //    Debug.Log("714 || " + Counter_Dead + " || " + Special_Rate_1 + " || " + Slot_Number);
            if (Counter_Dead >= Special_Rate_1)
            {
                Counter_Dead = 0;
                if (Type_Number == 603)
                    Core.GetComponent<Core>().Core_Add_HP(Special_Rate_2);
                if (Type_Number == 709)
                    Core.GetComponent<Core>().Core_Convert_Enemy_Bouns_HP += Special_Rate_2;
                if (Type_Number == 710)
                {
                    int Gold = GM.GetComponent<GameMaster>().Get_Gold(Player_Number);
                    Gold += (int)Special_Rate_2;
                    GM.GetComponent<GameMaster>().Set_Gold(Player_Number, Gold);
                }
                if (Type_Number == 714)
                {
                    Spwan_Enemy_Packet packet = new Spwan_Enemy_Packet();
                    packet.Spawn_Code = (short)Enemy_Code.Normal_Spwan;
                    packet.Speed = 1;
                    packet.POS = Vector3.zero;
                    packet.Type_Code = Number_To_Enemy_Code_Type(5);
                    Spwan_Attacker(packet);// 1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer, 5 Devil
                }

                if (Type_Number == 716)
                {
                    int Enemy_Number = Enemy.GetComponent<Enemy>().Player_Number;
                    GM.GetComponent<GameMaster>().Treasure_Reward(99999, Enemy, Enemy_Number);
                }
                Server_Prepare_Shot_Effect();
            }
        }
        if (Type == 3) // 706
        {
            Counter_Attacker_to_Basic_Damage++;
            if (Counter_Attacker_to_Basic_Damage >= Special_Rate_1)
            {
                Counter_Attacker_to_Basic_Damage = 0;
                Tower_Controller.GetComponent<Tower_Controller>().Add_Basic_Damage_Bouns(Player_Number); // Damage Bouns +1;
                Server_Prepare_Shot_Effect();
            }
        }
    }

    void Add_Attack_Time_To_Tower_Controller()
    {
        Tower_Controller.GetComponent<Tower_Controller>().Set_Tower_Counter(1, null); // 1 Counter Attack
    }

    IEnumerator Over_Time_send_To_Target_Again(Tower_To_Target_Info packet, int QTY)
    {
        GameObject Target = packet.Target;
        if (Target == null)
            yield break;
        if (Target.name != packet.Enemy_Name)
            yield break;
        yield return new WaitForSeconds(1);
        if (Target != null)
        {
            if (Target.name == packet.Enemy_Name)
                StartCoroutine(wait_second_send_To_Target(packet));
        }
    }

    //(bool Heal_or_Damage, Tower_To_Target_Info info, float time, bool summon_bullet, bool Effect_On,
    //    int Type, string enemy_name, int tower_name)

    IEnumerator wait_second_send_To_Target(Tower_To_Target_Info packet)
    {
        short Spawn_Enemy_Code = packet.Spawn_Enemy_Code;
        if (Type_Number == 0)
        {
            Local_Check_Variable_Empty();
            yield break;
        }

        GameObject Target = packet.Target;
        if (Type_Number == 503 && Spawn_Enemy_Code == (short)Enemy_Code.Spawn_None)
            Debug.LogWarning(Type_Number + " || Type || " + Spawn_Enemy_Code + " || " + gameObject.name + " || Tag || "
                + gameObject.tag + " || " + packet.Target);

        if ((Type_Number == 403 || Type_Number == 503 || Type_Number == 609) && Spawn_Enemy_Code == (short)Enemy_Code.Spawn_None || Target == null)
            yield break;

        if (Tower_Number != packet.Tower_Name)
            yield break;

        float temp_Damage = Damage;
        Vector3 POS = Target.transform.position;

        if (Target.name != packet.Enemy_Name)
            yield break;
        float Bullet_Speed = packet.Bullet_Speed;

        if (Type_Number == 303 || Type_Number == 507) // Lightning // Thunder_Highest_HP
            Summon_Bullet = false;

        if (Type_Number != 401 && Type_Number != 402 && Type_Number != 505) // Full_Screen_Thunder // Full_Screen_Ice // 3_Attack
            Server_Prepare_Bullet_To_All_Client(Target, packet.Time, Bullet_Speed, Summon_Bullet, packet.Effect_On);

        yield return new WaitForSeconds(packet.Time);

        bool Same_Target = false;
        bool Same_Tower = false;
        if (Tower_Number == packet.Tower_Name)
            Same_Tower = true;

        if (!Same_Tower)
            yield break;

        if (Target.name == packet.Enemy_Name)
            Same_Target = true;

        if (Type_Number == 203 && Target != null && Same_Target) // Ice
        {
            Target.GetComponent<Enemy>().Ice_01_Start = true;
            Target.GetComponent<Enemy>().Ice_01_Time = Special_Effect_Time;
            Target.GetComponent<Enemy>().Slow_Rate = Special_Rate_1;
        }
        if (Type_Number == 204 && Target != null && Same_Target) // Fire_Boom
        {
            Target.GetComponent<Enemy>().Spwan_Sub_Bullet(Type_Number, Damage, Critical_Rate, Critical_Damage);
        }
        if (Type_Number == 206 && Target != null && Same_Target) // Fear
        {
            Target.GetComponent<Enemy>().Fear_01_Start = true;
            Target.GetComponent<Enemy>().Fear_01_Time = Special_Effect_Time;
            Target.GetComponent<Enemy>().Fear_Rate = Special_Rate_1;
        }
        if (Type_Number == 301 && Target != null && Same_Target) // Stun
        {
            Target.GetComponent<Enemy>().Stun_01_Start = true;
            Target.GetComponent<Enemy>().Stun_01_Time = Special_Effect_Time;
            Target.GetComponent<Enemy>().Stun_Rate = Special_Rate_1;
        }
        if (Type_Number == 302 && Target != null && Same_Target) // Debuff
        {
            bool Debuff = true;
            Target.GetComponent<Enemy>().Set_Armor_Rate(Debuff, Special_Rate_1, Slot_Number.ToString());
        }

        if (Type_Number == 305 && Target != null && Same_Target) // Control
        {
            Target.GetComponent<Enemy>().Control_01_Start = true;
            Target.GetComponent<Enemy>().Control_01_Time = Special_Effect_Time;
            Target.GetComponent<Enemy>().Control_Rate = Special_Rate_1;
            Target.GetComponent<Enemy>().Temp_Player_Number = Player_Number;
        }

        if (Type_Number == 403 && Target != null && Same_Target)
        {
            Spwan_Enemy_Packet Enemy_Packet = new Spwan_Enemy_Packet();
            Enemy_Packet.Spawn_Code = (short)Enemy_Code.Fall_Spwan;
            Enemy_Packet.Fall_Target_Object = Target;
            Enemy_Packet.POS = POS;
            Enemy_Packet.Type_Code = packet.Spawn_Enemy_Code;
            Enemy_Packet.Attack_Protector_Code = (short)Enemy_Code.Protector;
            Enemy_Packet.Same_Target = Same_Target;
            Fall_Enemy(Enemy_Packet);
        }

        if (Type_Number == 503 && Target != null) // Fall_Attacker
        {
            Spwan_Enemy_Packet Enemy_Packet = new Spwan_Enemy_Packet();
            Enemy_Packet.Spawn_Code = (short)Enemy_Code.Fall_Spwan;
            Enemy_Packet.Fall_Target_Object = Target;
            Enemy_Packet.POS = POS;
            Enemy_Packet.Type_Code = packet.Spawn_Enemy_Code;
            Enemy_Packet.Attack_Protector_Code = (short)Enemy_Code.Attacker;
            Enemy_Packet.Same_Target = Same_Target;
            Fall_Enemy(Enemy_Packet);
        }

        if (Type_Number == 505 && Target != null) // 3 Attack
            if (Same_Target)
                Target.GetComponent<Enemy>().Server_Prepare_Target_Set_Enemy_Effect(Type_Number, 0.5f, true); // 0.5f Deactive Effect

        if (Type_Number == 609 && Target != null) // Fall_Horse_Attack
        {
            Spwan_Enemy_Packet Enemy_Packet = new Spwan_Enemy_Packet();
            Enemy_Packet.Spawn_Code = (short)Enemy_Code.Fall_Spwan;
            Enemy_Packet.Fall_Target_Object = Target;
            Enemy_Packet.Speed = 2;
            Enemy_Packet.POS = POS;
            Enemy_Packet.Type_Code = packet.Spawn_Enemy_Code;
            Enemy_Packet.Attack_Protector_Code = (short)Enemy_Code.Attacker;
            Enemy_Packet.Same_Target = Same_Target;
            Fall_Enemy(Enemy_Packet);
        }

        if (Type_Number == 610 && Target != null && Same_Target) // Exchange_Solider
        {
            int Random_Number = Random.Range(1, 101);
            if (Special_Rate_1 >= Random_Number && Target.name == packet.Enemy_Name)
                Target.GetComponent<Enemy>().Capture_Enemy();
        }

        if (Type_Number == 612 && Target != null && Same_Target)
        {
            float distance = Vector3.Distance(Target.transform.position, Tower_Position);
            if (distance >= 0.5)
            {
                float final_distance = distance - 0.5f;
                float Distance_Percent = 1 - (final_distance * 0.3f);
                temp_Damage = Damage * Distance_Percent;
            }
        }
        if (Type_Number == 707 && Target != null && Same_Target)
        {
            //Debug.Log("707 Bullet || summon_bullet || " + summon_bullet);
            float distance = Vector3.Distance(Target.transform.position, Tower_Position);
            int Random_Number = Random.Range(1, 101);
            Random_Number = 0;
            if (Special_Rate_1 >= Random_Number)
            {
                Target.GetComponent<Enemy>().Resurrection = true;
                Target.GetComponent<Enemy>().Server_Prepare_Target_Set_Special_Effect(707);
            }
        }
        if (Type_Number == 708 && Target != null && Same_Target)
        {
            Target.GetComponent<Enemy>().Set_Treasure(Special_Rate_1);
            Target.GetComponent<Enemy>().Server_Prepare_Target_Set_Special_Effect(708);
        }

        if (Type_Number == 715 && Target != null && Same_Target)
        {
            Target.GetComponent<Enemy>().Damage += Special_Rate_1;
            Target.GetComponent<Enemy>().Set_Armor_Rate(true, Special_Rate_2, Slot_Number.ToString());
        }
        //if (Type_Number == 715)
        //    Debug.Log("Target || " + Target.name + " || " + Target.GetComponent<Enemy>().Armor);

        short code = 0;
        if (packet.Action_Code == (short)Tower_Code.Damage)
            code = (short)Enemy_Code.Attack;
        if (packet.Action_Code == (short)Tower_Code.Heal)
            code = (short)Enemy_Code.Heal;

        if (Type_Number == 402) // Full_Screen_Ice
            yield break;

        if (Target != null && Same_Target)
            Target.GetComponent<Enemy>().Enemy_Damage(temp_Damage, code, Critical_Rate, Critical_Damage, Target.name, Type_Number.ToString());
    }

    void Heal_All_Target(GameObject[] Target)
    {
        if (Target.Length == 0)
            return;

        bool Critical = false; float Final_Value = Heal_HP;
        for (int i = 0; i < Target.Length; i++)
        {
            if (Target[i] != null)
            {
                int Random_Number = Random.Range(1, 101);
                if (Random_Number < Critical_Rate)
                    Critical = true;

                if (Critical)
                    Final_Value = Heal_HP;

                Target[i].GetComponent<Enemy>().HP += Final_Value;

                if (Target[i].GetComponent<Enemy>().HP > Target[i].GetComponent<Enemy>().MAX_HP)
                    Target[i].GetComponent<Enemy>().MAX_HP = Target[i].GetComponent<Enemy>().HP;
            }
        }
        Server_Prepare_Heal_To_All_Client(Target);
    }

    IEnumerator Deactive_Object(GameObject obj, float Time, bool Active_or_Deactive)
    {
        yield return new WaitForSeconds(Time);
        obj.SetActive(Active_or_Deactive);
    }

    GameObject Get_Shot_Effect(int number)
    {
        GameObject Effect = null;
        switch (number)
        {
            case (101):
                Effect = Shot_Effect_101;
                break;
            case (102):
                Effect = Shot_Effect_102;
                break;
            case (103):
                Effect = Shot_Effect_103;
                break;
            case (104):
                Effect = Shot_Effect_104;
                break;
            case (105):
                Effect = Shot_Effect_105;
                break;
            case (201):
                Effect = Shot_Effect_201;
                break;
            case (202):
                Effect = Shot_Effect_202;
                break;
            case (203):
                Effect = Shot_Effect_203;
                break;
            case (204):
                Effect = Shot_Effect_204;
                break;
            case (205):
                Effect = Shot_Effect_205;
                break;
            case (206):
                Effect = Shot_Effect_206;
                break;
            case (301):
                Effect = Shot_Effect_301;
                break;
            case (302):
                Effect = Shot_Effect_302;
                break;
            case (303):
                Effect = Shot_Effect_303;
                break;
            case (304):
                Effect = Shot_Effect_304;
                break;
            case (305):
                Effect = Shot_Effect_305;
                break;
            case (401):
                Effect = Shot_Effect_401;
                break;
            case (402):
                Effect = Shot_Effect_402;
                break;
            case (403):
                Effect = Shot_Effect_403;
                break;
            case (404):
                Effect = Shot_Effect_404;
                break;
            case (405):
                Effect = Shot_Effect_405;
                break;
            case (406):
                Effect = Shot_Effect_406;
                break;
            case (501):
                Effect = Shot_Effect_501;
                break;
            case (502):
                Effect = Shot_Effect_502;
                break;
            case (503):
                Effect = Shot_Effect_503;
                break;
            case (504):
                Effect = Shot_Effect_504;
                break;
            case (505):
                Effect = Shot_Effect_505;
                break;
            case (506):
                Effect = Shot_Effect_506;
                break;
            case (507):
                Effect = Shot_Effect_507;
                break;
            case (601):
                Effect = Shot_Effect_601;
                break;
            case (602):
                Effect = Shot_Effect_602;
                break;
            case (603):
                Effect = Shot_Effect_603;
                break;
            case (604):
                Effect = Shot_Effect_604;
                break;
            case (605):
                Effect = Shot_Effect_605;
                break;
            case (606):
                Effect = Shot_Effect_606;
                break;
            case (607):
                Effect = Shot_Effect_607;
                break;
            case (608):
                Effect = Shot_Effect_608;
                break;
            case (609):
                Effect = Shot_Effect_609;
                break;
            case (610):
                Effect = Shot_Effect_610;
                break;
            case (611):
                Effect = Shot_Effect_611;
                break;
            case (612):
                Effect = Shot_Effect_612;
                break;
            case (701):
                Effect = Shot_Effect_701;
                break;
            case (702):
                Effect = Shot_Effect_702;
                break;
            case (703):
                Effect = Shot_Effect_703;
                break;
            case (704):
                Effect = Shot_Effect_704;
                break;
            case (705):
                Effect = Shot_Effect_705;
                break;
            case (706):
                Effect = Shot_Effect_706;
                break;
            case (707):
                Effect = Shot_Effect_707;
                break;
            case (708):
                Effect = Shot_Effect_708;
                break;
            case (709):
                Effect = Shot_Effect_709;
                break;
            case (710):
                Effect = Shot_Effect_710;
                break;
            case (711):
                Effect = Shot_Effect_711;
                break;
            case (712):
                Effect = Shot_Effect_712;
                break;
            case (713):
                Effect = Shot_Effect_713;
                break;
            case (714):
                Effect = Shot_Effect_714;
                break;
            case (715):
                Effect = Shot_Effect_715;
                break;
            case (716):
                Effect = Shot_Effect_716;
                break;
            case (717):
                Effect = Shot_Effect_717;
                break;
            case (7172):
                Effect = Shot_Effect_717B;
                break;
            case (718):
                Effect = Shot_Effect_718;
                break;
            case (719):
                Effect = Shot_Effect_719;
                break;
        }
        return Effect;
    }

    float Get_Effect_Time(float type_number) // this mean enemy body effect , fire . heal etc ...
    {
        float time = 0;
        switch (type_number)
        {
            case (406):
                time = 0.7f;
                break;
            case (101):
            case (105):
            case (204):
            case (302):
                time = 1.5f;
                break;
            case (102):
                time = 0.3f;
                break;
            case (103):
            case (104):
            case (403):
                time = 0f;
                break;
            case (201):
            case (205):
            case (712):
                time = 1.0f;
                break;
            case (606):
            case (714):
                time = 2.0f;
                break;
            case (203):
            case (206):
            case (301):
            case (305):
                time = Special_Effect_Time;
                break;
        }
        return time;
    }

    float Get_Shot_Effect_Time(float type_number)
    {
        float time = 0;
        switch (type_number)
        {
            case (102):
            case (608):
            case (612):
            case (707):
            case (708):
            case (719):
                time = 0.45f;
                break;
            case (610):
                time = 0.6f;
                break;
            case (101):
            case (103):
            case (204):
            case (302):
            case (303):
            case (401):
            case (505):
            case (506):
            case (507):
            case (609):
            case (704):
            case (715):
            case (716):
            case (718):
                time = 1.0f;
                break;
            case (404):
            case (406):
                time = 0.7f;
                break;
            case (104):
            case (202):
            case (203):
            case (301):
            case (402):
            case (501):
            case (502):
            case (711):
            case (713):
                time = 1.0f;
                break;
            case (403):
                time = 1.5f;
                break;
            case (105):
            case (201):
            case (205):
            case (206):
            case (602):
            case (606):
            case (607):
            case (705):
            case (709):
            case (710):
            case (712):
                time = 2.0f;
                break;
            case (603):
                time = 2.5f;
                break;
            case (706):
            case (717):
            case (7172):
                time = 3.0f;
                break;
            case (604):
            case (605):
                time = 5.0f;
                break;
        }
        return time;
    }

    public void Set_Group_Effect(int type_Number, bool Active_or_Deactive)
    {
        GameObject Effect = Get_Shot_Effect(type_Number);
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

    #region Search_Target

    GameObject[] Get_All_Team_Attacker_Protector_Obj_Form_Map_Manager()
    {
        string[] tag = Map_Manager.GetComponent<Map_Manager>().Get_Own_Attacker_Protector_Tag(Player_Number);
        GameObject[] Array = Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
        return Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
    }

    GameObject[] Get_All_Team_Obj_Form_Map_Manager()
    {
        string[] tag = Map_Manager.GetComponent<Map_Manager>().Get_Own_Tag(Player_Number);
        return Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
    }

    GameObject[] Get_All_Opponent_Obj_Form_Map_Manager() // Opponent Enemy || Attacker || Protector
    {
        string[] tag = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Tag(Player_Number);
        GameObject[] Array = Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
        return Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
    }

    GameObject[] Get_Opponent_Enemy_Obj_Form_Map_Manager() // Opponent Enemy Only
    {
        string[] tag = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Enemy_Tag(Player_Number);
        return Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
    }

    GameObject Search_Closest_Core_Enemy_In_Area(string Effect, GameObject[] _Obj_Array)
    {
        GameObject[] Enemy_Array = new GameObject[_Obj_Array.Length];
        bool m_Target_Effected = false;
        int number = 0;
        if (Effect != null)
        {
            for (int i = 0; i < Enemy_Array.Length; i++)
            {
                if (_Obj_Array[i] != null)
                {
                    if (Effect == "Resurrection")
                        m_Target_Effected = _Obj_Array[i].GetComponent<Enemy>().Resurrection;

                    if (Effect == "Treasure")
                        m_Target_Effected = _Obj_Array[i].GetComponent<Enemy>().Treasure;

                    if (Effect == "Armor")
                        m_Target_Effected = _Obj_Array[i].GetComponent<Enemy>().Armor;

                    if (!m_Target_Effected)
                    {
                        Enemy_Array[number] = _Obj_Array[i];
                        number++;
                    }
                }
            }
        }

        if (Effect == null)
            Enemy_Array = _Obj_Array;

        GameObject nearestEnemy = null;
        if (Effect != "Armor")
        {
            float Walked_Distance = 0; // Mathf.Infinity;
            foreach (GameObject enemy in Enemy_Array)
            {
                if (enemy != null)
                {
                    float Walked = enemy.GetComponent<Enemy>().Distance_from_Start_Point;
                    float distanceToEnemy = Vector3.Distance(Tower_Position, enemy.transform.position);

                    bool Enemy_Setup_Finish = enemy.GetComponent<Enemy>().Setup_Finish;
                    if (Enemy_Setup_Finish)
                    {
                        if (Walked > Walked_Distance && distanceToEnemy <= Distance)
                        {
                            Walked_Distance = Walked;
                            nearestEnemy = enemy;
                        }
                    }
                }
            }
        }

        if (Effect == "Armor")
        {
            float Nearest_Distance = Mathf.Infinity;
            foreach (GameObject enemy in Enemy_Array)
            {
                if (enemy != null)
                {
                    float Walked = enemy.GetComponent<Enemy>().Distance_from_Start_Point;
                    float distanceToEnemy = Vector3.Distance(Tower_Position, enemy.transform.position);

                    bool Enemy_Setup_Finish = enemy.GetComponent<Enemy>().Setup_Finish;
                    if (Enemy_Setup_Finish)
                    {
                        if (distanceToEnemy <= Distance)
                        {
                            if (distanceToEnemy < Nearest_Distance)
                            {
                                Nearest_Distance = distanceToEnemy;
                                nearestEnemy = enemy;
                            }
                        }
                    }
                }
            }
        }

        return nearestEnemy;
    }

    GameObject Search_Closest_Enemy(GameObject Obj, GameObject[] target_in_list, GameObject[] _Obj_Array)
    {
        GameObject[] enemies = _Obj_Array;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            bool Enemy_in_List = Checkh_Target_In_List(enemy, target_in_list);
            if (!Enemy_in_List && enemy != Obj)
            {
                float distanceToEnemy = Vector3.Distance(Obj.transform.position, enemy.transform.position);
                if (distanceToEnemy <= Distance)
                    nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    GameObject[] Get_and_Sort_Enemy_Obj(GameObject[] _Obj_Array)
    {
        GameObject[] enemies = _Obj_Array;
        int number = 0;

        float[] distance = new float[11];
        GameObject[] nearestEnemy = new GameObject[11];
        foreach (GameObject enemy in enemies)
        {
            bool Enemy_Setup_Finish = enemy.GetComponent<Enemy>().Setup_Finish;
            if (Enemy_Setup_Finish)
            {
                float distanceToEnemy = Vector3.Distance(Tower_Position, enemy.transform.position);
                if (distanceToEnemy <= Distance && number <= 10)
                {
                    nearestEnemy[number] = enemy;
                    distance[number] = distanceToEnemy;
                    number++;
                }
            }
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
                    tempobj = nearestEnemy[i + 1];
                    tempfloat = distance[i + 1];
                    nearestEnemy[i + 1] = nearestEnemy[i];
                    distance[i + 1] = distance[i];
                    nearestEnemy[i] = tempobj;
                    distance[i] = tempfloat;
                    Wait_Sorting = true;
                }
            }
        }
        return nearestEnemy;
    }

    GameObject Get_High_HP_Single_By_Array(GameObject[] Obj)
    {
        float HP = 0;
        float temp_HP;
        GameObject Highest_HP_Enemy = null;
        for (int i = 0; i < Obj.Length; i++)
        {
            temp_HP = Obj[i].GetComponent<Enemy>().HP;
            if (temp_HP > HP)
            {
                Highest_HP_Enemy = Obj[i];
            }
        }
        return Highest_HP_Enemy;
    }

    string Get_Target_Tag()
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

    string Get_Protector_Tag()
    {
        string Tag = null;
        if (Player_Number == 1)
            Tag = "Player01_Protector";
        if (Player_Number == 2)
            Tag = "Player02_Protector";
        if (Player_Number == 3)
            Tag = "Player03_Protector";
        if (Player_Number == 4)
            Tag = "Player04_Protector";
        return Tag;
    }

    bool Checkh_Target_In_List(GameObject _Obj, GameObject[] target_list)
    {
        bool In_List = false;
        for (int i = 0; i < target_list.Length; i++)
        {
            GameObject Test_List = target_list[i];
            if (Test_List == _Obj)
                In_List = true;
        }
        return In_List;
    }

    void Tower_Short_Cut()
    {
        GameObject[] temp = new GameObject[0];
        Get_Shot_Effect_Time(1); // Set Tower Shot Time show effect
        Update(); //
        Tower_Action(1, temp);
    }
    #endregion

    GameObject Get_Online_Player_Object_From_Object_Manager(int Number)
    {
        GameObject Object_Manager = GameObject.Find("Object_Manager");
        Player = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(Number);
        return Player;
    }

    public void Reset_Tower_To_Pool()
    {
        short slot_number = (short)Slot_Number;
        short player_Number = (short)Player_Number;

        //Debug.Log("Reset_Tower_To_Pool || " + slot_number + " || " + player_Number);

        Damage = 0; Speed = 0; Distance = 0; Add_Core_HP = 0; Core_Bouns_Rate = 0; Special_Effect_Time = 0; Basic_HP_For_Solider = 0;
        Basic_Attack_For_Solider = 0; Heal_HP = 0; Special_Rate_1 = 0; Special_Rate_2 = 0; Special_Rate_3 = 0; Chain_Connect_Number = 0;
        Counter_Attack = 0; Counter_Dead = 0; EXP = 0; Level = 1; Basic_Damage_Bouns = 0; EXP_Bouns = 0; Speed_Bouns = 0; Lucky_Bouns = 0;
        Critical_Rate = 0; Critical_Damage = 0; Timer = 0; Timer_Add_Core_HP = 0; Dragger = false; End_Drag = false;
        Player_Number = 0; Slot_Number = 0; Type_Number = 0; Combine_Up_Point = 0; Group_Number = 0; Player = null; Core = null;
        Tower_Controller = null; Map_Manager = null; Tower_Position = Vector3.zero; Summon_Bullet = false; Move_Bullet = false;
        Line_Render = false; Start_Dragging = false; Setup_Finish = false; collider_hit_Tower = false;
        One_VS_One = false; Two_VS_Two = false; Two_Cooperation = false; Four_Cooperation = false;
        Target_Tower = null; Lock = false; Timer2 = 0; Tower_Number = 0; in_Pool = false;
        Tower_To_Target_Info to_Target = new Tower_To_Target_Info(null, false, false, 0, 0, 0, 0, 0);
        Basic_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);
        Combine_Up_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);
        Level_Up_Detail = new Tower_Info.Tower_Info_Detail(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false);

        GameObject[] Array = new GameObject[]
        {
        Chain_Weapon_Point_Top, Chain_Weapon_Point_Right, Chain_Weapon_Point_Buttom, Chain_Weapon_Point_Left,
        Ground_01, Ground_02, Ground_03, Ground_04, Ground_05, Ground_06, Ground_07, Ground_08, Ground_09, Ground_10,
        Ground_11, Ground_12, Ground_13, Ground_14, Ground_15,
        Icon_101, Icon_102, Icon_103, Icon_104, Icon_105, Icon_106, Icon_107, Icon_108, Icon_109, Icon_110,
        Icon_201, Icon_202, Icon_203, Icon_204, Icon_205, Icon_206, Icon_207, Icon_208, Icon_209, Icon_210,
        Icon_301, Icon_302, Icon_303, Icon_304, Icon_305, Icon_306, Icon_307, Icon_308, Icon_309, Icon_310,
        Icon_401, Icon_402, Icon_403, Icon_404, Icon_405, Icon_406, Icon_407, Icon_408, Icon_409, Icon_410,
        Icon_501, Icon_502, Icon_503, Icon_504, Icon_505, Icon_506, Icon_507, Icon_508, Icon_509, Icon_510,
        Icon_601, Icon_602, Icon_603, Icon_604, Icon_605, Icon_606, Icon_607, Icon_608, Icon_609, Icon_610,
        Icon_611, Icon_612, Icon_613, Icon_614, Icon_615, Icon_616, Icon_617, Icon_618, Icon_619, Icon_620,
        Icon_701, Icon_702, Icon_703, Icon_704, Icon_705, Icon_706, Icon_707, Icon_708, Icon_709, Icon_710,
        Icon_711, Icon_712, Icon_713, Icon_714, Icon_715, Icon_716, Icon_717, Icon_718, Icon_719, Icon_720,
        Icon_721, Icon_722, Icon_723, Icon_724, Icon_725, Icon_726, Icon_727, Icon_728, Icon_729, Icon_730,
        Level_01, Level_02, Level_03, Level_04, Level_05, Level_06, Level_07, Level_08, Level_09, Level_10,
        Level_11, Level_12, Level_13, Level_14, Level_15,
        D_L01, D_L02, D_L03, D_L04, D_L05, D_L06, D_L07, D_L08, D_L09, D_L10, D_L11, D_L12, D_L13, D_L14, D_L15,
        Shot_Effect_101, Shot_Effect_102, Shot_Effect_103, Shot_Effect_104, Shot_Effect_105,
        Shot_Effect_201, Shot_Effect_202, Shot_Effect_203, Shot_Effect_204, Shot_Effect_205, Shot_Effect_206,
        Shot_Effect_301, Shot_Effect_302, Shot_Effect_303, Shot_Effect_304, Shot_Effect_305,
        Shot_Effect_401, Shot_Effect_402, Shot_Effect_403, Shot_Effect_404, Shot_Effect_405, Shot_Effect_406,
        Shot_Effect_501, Shot_Effect_502, Shot_Effect_503, Shot_Effect_504, Shot_Effect_505, Shot_Effect_506, Shot_Effect_507,
        Shot_Effect_601, Shot_Effect_602, Shot_Effect_603, Shot_Effect_604, Shot_Effect_605, Shot_Effect_606,
        Shot_Effect_607, Shot_Effect_608, Shot_Effect_609, Shot_Effect_610, Shot_Effect_611, Shot_Effect_612,Shot_Effect_613,
        Shot_Effect_701, Shot_Effect_702, Shot_Effect_703, Shot_Effect_704, Shot_Effect_705, Shot_Effect_706,
        Shot_Effect_707, Shot_Effect_708, Shot_Effect_709, Shot_Effect_710, Shot_Effect_711, Shot_Effect_712,
        Shot_Effect_713, Shot_Effect_714, Shot_Effect_715, Shot_Effect_716, Shot_Effect_717, Shot_Effect_717B,Shot_Effect_718,
        Shot_Effect_719,Shot_Effect_720,Shot_Effect_721
        };

        for (int i = 0; i < Array.Length; i++)
        {
            if (Array[i] != null)
                if (Array[i].activeSelf)
                    Array[i].SetActive(false);
        }

        if (isServer)
        {
            Server_Prepare_Reset_Local_Tower();
            Server_Tower_Set_InVisible();
        }
        if (!isServer)
        {
            Debug.Log("Tower || !isServer || " + !isServer);
            gameObject.name = "Tower_Object";
            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            if (pool_Manager)
            {
                GameObject Tower_Folder = pool_Manager.GetComponent<Pool_Manager>().Tower_Folder;
                if (Tower_Folder)
                    gameObject.transform.SetParent(Tower_Folder.transform);
            }
            Local_Tower_Set_InVisible(player_Number, slot_number);
        }
    }

    #region Local_Check_Variable_Empty // for Reconnect Game

    void Local_Check_Variable_Empty()
    {
        Debug.Log("Local_Check_Variable_Empty");
        bool Object_Variable_Empty = Check_Object_Variable_Empty();
        if (Object_Variable_Empty)
        {
            if (!GameObject.Find("Local_Manager"))
                return;
            GameObject Local_Player = GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Local_Player;
            if (!Local_Player) return;
            Local_Player.GetComponent<Player_Network>().Client_Reconnect_Game_Request_Tower_Refresh_Status(gameObject);
            return;
        }
        if (Player == null)
            Player = GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Local_Player;
    }

    bool Check_Object_Variable_Empty()
    {
        if (!Player) return true;
        if (Player_Number == 0) return true;
        if (Type_Number == 0) return true;
        return false;
    }

    public void Refresh_Status(NetworkConnection conn)
    {
        int Drag_Tower_Number = 0;
        Vector3 POS = transform.position;
        Target_ReFresh_Tower_Type(conn, Player_Number, Slot_Number, Type_Number, Combine_Up_Point, Level, Player, Summon_Bullet,
            Move_Bullet, Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time, Basic_HP_For_Solider, Heal_HP, Special_Rate_1,
            Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, Drag_Tower_Number, POS);
    }

    [TargetRpc]
    public void Target_ReFresh_Tower_Type(NetworkConnection conn, int player_Number, int slot_Number, int type_Number, int combine_point_up, int level, GameObject player,
        bool summon_bullet, bool move_bullet, bool line_render, float dmg, float spd, float add_core_hp, float special_effect_time,
        float basic_hp_for_solider, float heal_hp, float S_Rate_1, float S_Rate_2, float S_Rate_3, int critical_rate,
        int critical_damage, int Drag_Tower_Number, Vector3 tower_poisition)
    {
        Local_Update_Tower_Info(player_Number, slot_Number, type_Number, combine_point_up, Level, player, Summon_Bullet, Move_Bullet,
                Line_Render, Damage, Speed, Add_Core_HP, Special_Effect_Time, Basic_HP_For_Solider, Heal_HP, Special_Rate_1,
                Special_Rate_2, Special_Rate_3, Critical_Rate, Critical_Damage, Drag_Tower_Number, tower_poisition);
    }
    #endregion

    #region Visible

    public void Server_Tower_Set_Visible()
    {
        GetComponent<Tower>().enabled = true;
    }

    public void Server_Tower_Set_InVisible()
    {
        GetComponent<Tower>().enabled = false;
    }

    void Local_Tower_Set_Visible()
    {
        Debug.Log("Local_Tower_Set_Visible_Event || " + Player_Number + " || " + Slot_Number);
        if (Player_Number == 0 || Slot_Number == 0)
            return;
        Local_Tower_Set_Visible((short)Player_Number, (short) Slot_Number);
    }

    public void Local_Tower_Set_Visible(short player_Number, short slot_Number)
    {
        Debug.Log("Local_Tower_Set_Visible || " + Player_Number + " || " + Slot_Number + " || " + gameObject.name);
        string[] name = new string[] { "Ground", "Icon", "Level", "Shot_Effect", "Chain_Weapon_Point" };
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("transform || " + transform.gameObject.name);
            GameObject Child = transform.Find(name[i]).gameObject;
            Child.SetActive(true);
        }
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().detectCollisions = true;
        GetComponent<Tower>().enabled = true;
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        m_local_Manager.GetComponent<Local_Manager>().Set_Tower_Floor(player_Number, slot_Number, (short)Tower_Code.Inactive);
    }

    void Local_Tower_Set_InVisible(short player_number, short slot_Number)
    {
        transform.position = new Vector3(20, 0, 0);
        string[] name = new string[] { "Ground", "Icon", "Level", "Shot_Effect", "Chain_Weapon_Point" };
        for (int i = 0; i < 5; i++)
        {
            GameObject Child = transform.Find(name[i]).gameObject;
            Child.SetActive(false);
            if (name[i] == "Shot_Effect")
            {
                foreach (Transform Shot_Effect_Child in Child.transform)
                {
                    Shot_Effect_Child.gameObject.SetActive(false);
                }
            }
        }

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Tower>().enabled = false;
        //GameObject m_local_Manager = GameObject.Find("Local_Manager");
        //m_local_Manager.GetComponent<Local_Manager>().Set_Tower_Floor(player_number, slot_Number, (short)Tower_Code.Active);
    }
    #endregion

    public short Number_To_Enemy_Code_Type(int number)
    {
        switch (number)
        {
            case (1): return (short)Enemy_Code.Basic;
            case (2): return (short)Enemy_Code.Horse;
            case (3): return (short)Enemy_Code.Thief;
            case (4): return (short)Enemy_Code.Boomer;
            case (5): return (short)Enemy_Code.Devil;
            case (6): return (short)Enemy_Code.Dragon;
        }
        return 0;
    }

    public short Enemy_Code_To_Number_Type(short code)
    {
        switch (code)
        {
            case ((short)Enemy_Code.Basic): return 1;
            case ((short)Enemy_Code.Horse): return 2;
            case ((short)Enemy_Code.Thief): return 3;
            case ((short)Enemy_Code.Boomer): return 4;
            case ((short)Enemy_Code.Devil): return 5;
            case ((short)Enemy_Code.Dragon): return 6;
        }
        return 0;
    }

    void Fall_Enemy(Spwan_Enemy_Packet packet)
    {
        packet.Fall_Enemy_Helper = temp_Enemy_Helper;
        packet.Spawn_Code = (short)Enemy_Code.Fall_Spwan;

        if (packet.Speed == 0)
            packet.Speed = 1;

        bool Same_Target = packet.Same_Target;
        short Attacker_Protector_Code = packet.Attack_Protector_Code;

        if (Same_Target)
            packet.POS = packet.Fall_Target_Object.transform.position;

        if (packet.Attack_Protector_Code == (short)Enemy_Code.Attacker)
            Spwan_Attacker(packet);

        if (packet.Attack_Protector_Code == (short)Enemy_Code.Protector)
            Spwan_Protector(packet); // Fall_Protector
    }

    void Set_Fall_Enemy_Helper()
    {
        GameObject fall_Enemy_Helper = Instantiate(Fall_Enemy_Helper, Vector3.zero, Quaternion.identity);
        MasterServerToolkit.MasterServer.Info info = fall_Enemy_Helper.GetComponent<MasterServerToolkit.MasterServer.Info>();
        //info.Enemy_Path_Code;

        temp_Enemy_Helper = fall_Enemy_Helper;
    }
}

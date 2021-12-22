using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Enemy : NetworkBehaviour
{
    public bool _enemy, _protector, _attacker, _fall_attacker, _fall_protector;
    public GameObject loc_spwan, loc_1, loc_2, loc_3, loc_4, loc_5, loc_6, loc_7, loc_8, loc_9;
    public string local_loc_spwan, local_loc_1, local_loc_2, local_loc_3, local_loc_4, local_loc_5, local_loc_6, local_loc_7, local_loc_8, local_loc_9;
    public float Dist0, Dist1, Dist2, Dist3, Dist4, Dist5, Dist6, Dist7, Dist8, Dist9;

    public GameObject GM;
    public bool Original_Enemy;
    public int Player_Number, Original_Player_Number, Temp_Player_Number;
    public bool Enemy_object, Protector_Object, Attacker_Object, Control_Enemy_Object, Attacker_To_Enemy_Object;
    int _object_Type = 1, Devil_Type = 0;
    public bool Attacking = false, Pause = false, In_List = false;
    float Attacking_Timer = 0;
    public GameObject Attacking_Target_Enemy;
    public float HP;
    public float MAX_HP;
    public float Damage;
    public float Original_Speed, Speed, temp_speed;
    public int Gold;
    public bool Armor, No_Damage;
    int Armor_Rate;
    public bool Patroling = false; // while protector arrival Patrol Point will change to true , and start accept Protector_Control
    public bool Protector_Clockwise = false;
    public Transform Move_Point;
    public GameObject Move_Target_Point, Previous_Point, Move_Point_Temp;

    public bool Move = false, in_Pool;
    public bool Setup_Finish = false;
    bool Animation_Set = false, Local_Stop_Move_Rejoin_Wait_Setup_Finish = false;
    float Dead_Timer = 0;
    bool Dead = false;
    int Frame;
    NetworkConnection Conn;

    //public Vector3 POS;

    [Header("Client_Object")]
    public GameObject Client_Model;
    public GameObject Enemy_01, Protector_01, Horse_Attacker_01, Thief_01, Boomer_01;
    public GameObject Devil_01, Devil_02, Devil_03, Dragon_Head_Black;

    [Header("Client_Material")]
    public Material Enemy_Original;
    public Material Protector_Original, Attacker_Original;
    public Material Horse_Enemy_Original, Horse_Attacker_Original, Horse_Protector_Original, Horse_Original;
    public Material Thief_Enemy_Original, Thief_Attacker_Original, Thief_Protector_Original;
    public Material Boomer_Enemy_Original, Boomer_Attacker_Original;
    public Material Devil_01_Enemy_Original, Devil_01_Attacker_Original, Devil_01_Protector_Original;
    public Material Devil_02_Enemy_Original, Devil_02_Attacker_Original, Devil_02_Protector_Original;
    public Material Devil_03_Enemy_Original, Devil_03_Attacker_Original, Devil_03_Protector_Original;
    public Material Dragon_Head_Enemy, Dragon_Head_Attacker, Dragon_Head_Protector;

    [Header("Bar")]
    public GameObject HP_Bar;
    public GameObject Damage_Bar;
    public GameObject Gold_Bar;

    [Header("Other")]
    public float Distance_from_Last_Point = 0;
    public float Distance_from_Start_Point = 0;
    public Vector3 Last_Way_Point_POS;
    Animator Anim;

    public float Slow_Rate;
    public float Fear_Rate;
    public float Stun_Rate;
    public float Control_Rate;
    public float Attack_Bouns;
    public int Special_Effect_QTY;
    int Special_Effect_Balance_QTY;
    public float Special_Effect_Time;
    float Special_Effect_Timer;

    public GameObject Test;
    public GameObject Test_Tag_1, Test_Tag_2, Text_Type;
    public GameObject HP_Canvas_Text;
    public string Test_Where;

    [Header("Sub_Bullet")]
    public GameObject ground_Boom;

    [Header("Enemy_Effect")]
    public GameObject Slash_Effect_01_P, Slash_Effect_Rnaomd_Rotation;
    public GameObject Slash_Effect_02_P, Slash_Effect_03_P, Slash_Effect_01_E, Slash_Effect_02_E, Slash_Effect_03_E;
    public GameObject Slash_Effect_Horse, Devil_Effect_01, Devil_Effect_02, Devil_Effect_03;
    public GameObject Impact_Effect, Impact_Effect_01, Impact_Effect_02, Impact_Effect_03;
    public GameObject Heal_Effect_01, Poison_Effect_01, Wind_Effect_01, Resurrection_Effect;
    public GameObject Explosion_Effect_01, Fear_Effect_01, Debuff_Effect_01;
    public GameObject Thunder_Effect_01, Chain_Effect_01, Dragon_Head_Rush_Black, Dragon_Head_Rush_Red, Dragon_Head_Rush_White,
        Resurrection_HolyLight_Effect, Treasure_Effect, Heal_Effect_02;
    GameObject Stun_Effect_01, Control_Effect_01, Armor_Effect_01;
    public Material Original_Material, Horse_Material, Black_Material, Transfer_Material, Resurrect_Material;

    [Header("Boomer_Effect")]
    public GameObject Boomer_Effect;

    public bool Ice_01_Start, Fear_01_Start, Stun_01_Start, Control_01_Start, Start_Steal, Chain_01_Start, Chain_01, Resurrection;
    public float Ice_01_Time, Fear_01_Time, Stun_01_Time, Control_01_Time, Chain_01_Time;
    bool Impact_Start, Fire_01_Start, Wind_01_Start, Heal_01_Start, Poison_01_Start, Explosion_Effect_01_Start, Debuff_01_Start;
    bool Fire_01, Wind_01, Heal_01, Poison_01, Ice_01, Explosion_01, Fear_01, Stun_01, Debuff_01, Control_01;
    float Impact_Timer, Fire_01_Timer, Wind_01_Timer, Heal_01_Timer, Poison_01_Timer, Ice_01_Timer, Explosion_01_Timer, Fear_01_Timer, Stun_01_Timer, Debuff_01_Timer, Control_01_Timer, Chain_01_Timer;
    float Impact_Time, Fire_01_Time, Wind_01_Time, Heal_01_Time, Poison_01_Time, Explosion_01_Time, Debuff_01_Time;

    public bool Boomer, Boomed;
    bool Thief, Dragon_Bite, Start_Bite, Devil_Skill;
    float Bite_Timer, Max_Bite_HP, Bite_HP;
    public float Thief_Timer, Thief_Time, Thief_Gold_Rate, Thief_Steal_Rate, Thief_Steal_QTY;
    public GameObject Test_Canvas;

    //public float Distance_TempPoint_To_N_Point = 0, Distance_Enemy_To_N_Point = 0;
    public GameObject Test_Capture, Test_Resurrection;
    public GameObject Bullet_Devil_03;
    float Rate;

    [Header("Treasure")]
    public bool Treasure;
    public float Treasure_Rate;
    public GameObject Treasure_Box;

    float Old_HP;
    string Who_Attack;
    float Test_Timer = 0;
    public Material Test_Material_1, Test_Material_2, Test_Material_3;
    bool Test_Attacker;

    public short enemy_Code, op_room_Code, OP_Path_Code;

    public Material Black, White, Red;

    #region Set_Enemy
    public void Set_Enemy(GameObject gamemaster, bool enemy, bool protector, bool attacker, string Enemy_Name_For_Client,
        string Tag, int player_Enemy, float hp, float dmg, float speed, int gold, int Type, string where)
    {
        Test_Where = where;
        No_Damage = false;
        if (player_Enemy == 0)
            Debug.LogWarning("Set_Enemy || " + player_Enemy);
        //if (speed < 1)
        //    Debug.Log("speed || " + speed + " || " + where);
        Old_HP = hp;
        In_List = false;
        Resurrection = false;
        if (Type == 0)
            Debug.LogWarning("Type || " + Type + " || " + _object_Type + " || " + gameObject.name + " || Tag || " + gameObject.tag);
        //Debug.Log("enemy || " + enemy + " || protector || " + protector + " || attacker || " + attacker + " || Tag || " + Tag);
        MAX_HP = hp;
        Last_Way_Point_POS = transform.position;
        HP = hp;

        if (HP < 0)
            Debug.LogWarning("MAX_HP || " + MAX_HP + " || HP || " + HP + " || Name || " + Enemy_Name_For_Client + " || where || " + where);
        Gold = gold;
        Player_Number = player_Enemy;
        Original_Player_Number = player_Enemy;
        GM = gamemaster;
        op_room_Code = GM.GetComponent<GameMaster>().op_room_Code;
        gameObject.name = Enemy_Name_For_Client;
        gameObject.tag = Tag;
        Enemy_object = enemy;
        Protector_Object = protector;
        Attacker_Object = attacker;

        if (Enemy_object && !Protector_Object && !Attacker_Object)
            enemy_Code = (short)Enemy_Code.Enemy;

        if (!Enemy_object && Protector_Object && !Attacker_Object)
            enemy_Code = (short)Enemy_Code.Protector;

        if (!Enemy_object && !Protector_Object && Attacker_Object)
            enemy_Code = (short)Enemy_Code.Attacker;

        _object_Type = Type;
        if (_object_Type == 3)
            Thief = true;

        if (_object_Type == 5 && Devil_Type == 0)
        {
            Devil_Type = Random.Range(1, 4);
            Devil_Type = 2;
        }

        // Test Object
        if (Protector_Object)
        {
            string Obj_Name = gameObject.name;
            string Obj_Tag = gameObject.tag;
            if (Obj_Name.Contains("Enemy"))
                Debug.LogWarning("Protector_Object || Obj_Name || " + Obj_Name + " || " + Test_Where);
            if (Obj_Tag.Contains("Enemy"))
                Debug.LogWarning("Protector_Object || Obj_Tag || " + Obj_Tag + " || " + Test_Where);
        }

        Damage = dmg;
        Original_Speed = speed;
        Speed = Original_Speed;
        temp_speed = Speed;
        if (Move_Target_Point == null)
            Debug.Log("Move_Target_Point || " + Move_Target_Point + " || " + gameObject.name + gameObject.tag);
        Move_Point = Move_Target_Point.transform;
        transform.LookAt(Move_Target_Point.transform);
        Server_Prepare_Target_Set_Enemy_Rotation(transform, Move_Target_Point.transform.position);
        Move = true;
        Server_Prepare_Target_Set_Enemy_Type(enemy, protector, attacker, player_Enemy, hp, dmg,
            Move_Point.transform.position, Speed, Tag, Type, Devil_Type, Enemy_Name_For_Client);

        //Send_Camera_To_Test();
        Vector3 POS_Zero = Vector3.zero;
        Dead_Timer = 0;
        Dead = false;
        Pause = false;
        Attacking_Target_Enemy = null;
        Attacking = false;
        Attacking_Timer = 0;
        in_Pool = false;
        GetComponent<Enemy>().enabled = true;
        Setup_Finish = true;

        //if (original_Speed < 1 || m_speed < 1)
        //{
        //    Debug.Log("Ice_01 || " + ice_01 + " || Fear_01 || " + fear_01 + " || Control_01 || " + control_01 + " || Stun_01 || " + stun_01);
        //    Debug.Log("m_speed || " + m_speed + " || temp_speed || " + temp_speed + " || Original_Speed || " + Original_Speed);
        //}
    }

    public void Set_Rate(float value)
    {
        Rate = value + 5;
    }

    void Local_Set_Enemy_Type(bool enemy, bool protector, bool attacker, int player_number, float hp, float dmg,
        Vector3 target_point_pos, float speed, string tag, int Type, int devil_type, string Name)
    {
        string Obj_name = Name;

        In_List = false;
        Resurrection = false;
        if (Type == 0)
            Debug.LogWarning("Type || " + Type + " || " + _object_Type + " || " + gameObject.name + " || Tag || " + gameObject.tag);
        _object_Type = Type;
        if (Type == 3)
            Thief = true;
        if (Type == 4)
            Boomer = true;

        All_Effect_Deactive();
        _object_Type = Type;
        gameObject.tag = tag;
        Speed = speed;
        Move_Point_Temp = new GameObject();
        Move_Point_Temp.name = "Move_Point_Temp";
        Move_Point = Move_Point_Temp.transform;
        Move_Point.transform.position = target_point_pos;
        Move_Target_Point = Move_Point.gameObject;

        HP = hp;
        Damage = dmg;
        Player_Number = player_number;
        string Enemy_Name_for_Client = Name;
        Enemy_object = enemy;
        Protector_Object = protector;
        Attacker_Object = attacker;

        if (Enemy_object && !Protector_Object && !Attacker_Object)
            enemy_Code = (short)Enemy_Code.Enemy;

        if (!Enemy_object && Protector_Object && !Attacker_Object)
            enemy_Code = (short)Enemy_Code.Protector;

        if (!Enemy_object && !Protector_Object && Attacker_Object)
            enemy_Code = (short)Enemy_Code.Attacker;

        Devil_Type = devil_type;
        gameObject.name = Enemy_Name_for_Client;
        Set_Enemy_Type();
        Move = true;
        Dead_Timer = 0;
        Dead = false;
        Pause = false;

        string ParentName = null;
        if (Enemy_object)
            ParentName = "Enemy";
        if (Protector_Object)
            ParentName = "Protector";
        if (Attacker_Object)
            ParentName = "Attacker";

        in_Pool = false;
        GetComponent<Enemy>().enabled = true;
        HP_Canvas_Text = Client_Model.GetComponent<Object_Status>().HP_Canvas_Text;
        int hp_value = (int)HP;
        HP_Canvas_Text.GetComponent<Text>().text = HP.ToString();
        GameObject folder = GameObject.Find(ParentName);
        gameObject.transform.SetParent(folder.transform);
    }

    [Command]
    public void CmdTarget_Null_Change_Name()
    {
        gameObject.name = "Target is null";
    }

    void Set_Enemy_Type()
    {
        if (!isServer)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Client_Model = Check_Client_Model_Type();
            if (Client_Model == null)
                Debug.Log("Client_Model || _object_Type || " + _object_Type + " || Name || " + gameObject.name + " || Tag || " + gameObject.tag);
            GameObject Body = Client_Model.GetComponent<Object_Status>().Body;
            Original_Material = Get_Body_Material();
            Body.GetComponent<SkinnedMeshRenderer>().material = Original_Material;
            GameObject Horse = Client_Model.GetComponent<Object_Status>().Horse;
            if (Horse != null)
                Horse_Material = Horse.GetComponentInChildren<SkinnedMeshRenderer>().material;
            if (Client_Model == null)
                CmdTarget_Null_Change_Name();

            Client_Model.SetActive(true);
            Client_Model.GetComponent<Animator>().enabled = true;
            Anim = Client_Model.GetComponent<Animator>();
            Anim.enabled = true;
            Anim.SetTrigger("Walk");
            Setup_Finish = true;
        }
    }

    #endregion


    GameObject Check_Client_Model_Type()
    {
        GameObject[] Array = new GameObject[] { null,Enemy_01, Protector_01, Horse_Attacker_01 , Thief_01 , Boomer_01 ,
                Devil_01,Devil_02,Devil_03,Dragon_Head_Black};

        for (int i = 1; i < Array.Length; i++)
        {
            if (Array[i] != null)
            {
                if (Array[i].activeSelf)
                    Array[i].SetActive(false);
            }
        }

        GameObject _Object = null;
        if (Enemy_object)
            _Object = Get_Enemy_Object(_object_Type);

        if (Protector_Object)
            _Object = Get_Protector_Object(_object_Type);

        if (Attacker_Object)
            _Object = Get_Attacker_Object(_object_Type);

        return _Object;
    }

    GameObject Get_Enemy_Object(int number)
    {
        GameObject _Object = null;
        switch (number)
        {
            case (1): return Enemy_01;
            case (2): return Horse_Attacker_01;
            case (3): return Thief_01;
            case (4): return Boomer_01;
            case (5):
                if (Devil_Type == 1) return Devil_01;
                if (Devil_Type == 2) return Devil_02;
                if (Devil_Type == 3) return Devil_03;
                break;
            case (6):
                return Dragon_Head_Black;
        }
        return _Object;
    }

    GameObject Get_Protector_Object(int number)
    {
        GameObject _Object = null;
        switch (number)
        {
            case (1): return Protector_01;
            case (2): return Horse_Attacker_01;
            case (3): return Thief_01;
            case (4): return Boomer_01;
            case (5):
                if (Devil_Type == 1) return Devil_01;
                if (Devil_Type == 2) return Devil_02;
                if (Devil_Type == 3) return Devil_03;
                break;
            case (6): return Dragon_Head_Black;
        }
        return _Object;
    }

    GameObject Get_Attacker_Object(int number)
    {
        GameObject _Object = null;
        switch (number)
        {
            case (1): return Protector_01;
            case (2): return Horse_Attacker_01;
            case (3): return Thief_01;
            case (4): return Boomer_01;
            case (5):
                if (Devil_Type == 1) return Devil_01;
                if (Devil_Type == 2) return Devil_02;
                if (Devil_Type == 3) return Devil_03;
                break;
            case (6): return Dragon_Head_Black;
        }
        return _Object;
    }

    Material Get_Body_Material()
    {
        Material material = null;
        //1 Normal_Attacker , 2 Horse_Attacker , 3 Thief, 4 Boomer, 5 Devil
        if (Enemy_object)
        {
            if (_object_Type == 1)
                material = Enemy_Original;
            if (_object_Type == 2)
                material = Horse_Enemy_Original;
            if (_object_Type == 3)
                material = Thief_Enemy_Original;
            if (_object_Type == 4)
                material = Boomer_Enemy_Original;
            if (_object_Type == 5)
            {
                if (Devil_Type == 1)
                    material = Devil_01_Enemy_Original;
                if (Devil_Type == 2)
                    material = Devil_02_Enemy_Original;
                if (Devil_Type == 3)
                    material = Devil_03_Enemy_Original;
            }
            if (_object_Type == 6)
                material = Dragon_Head_Enemy;
        }
        if (Attacker_Object)
        {
            if (_object_Type == 1)
                material = Attacker_Original;
            if (_object_Type == 2)
                material = Horse_Attacker_Original;
            if (_object_Type == 3)
                material = Thief_Attacker_Original;
            if (_object_Type == 4)
                material = Boomer_Attacker_Original;
            if (_object_Type == 5)
            {
                if (Devil_Type == 1)
                    material = Devil_01_Attacker_Original;
                if (Devil_Type == 2)
                    material = Devil_02_Attacker_Original;
                if (Devil_Type == 3)
                    material = Devil_03_Attacker_Original;
            }
            if (_object_Type == 6)
                material = Dragon_Head_Attacker;
        }
        if (Protector_Object)
        {
            if (_object_Type == 1)
                material = Protector_Original;
            if (_object_Type == 2)
                material = Horse_Protector_Original;
            if (_object_Type == 3)
                material = Thief_Protector_Original;
            if (_object_Type == 4)
                material = null;
            if (_object_Type == 5)
            {
                if (Devil_Type == 1)
                    material = Devil_01_Protector_Original;
                if (Devil_Type == 2)
                    material = Devil_02_Protector_Original;
                if (Devil_Type == 3)
                    material = Devil_03_Protector_Original;
            }
            if (_object_Type == 7)
                material = Dragon_Head_Protector;
        }
        return material;
    }

    GameObject Get_Player(int number)
    {
        GameObject m_Player = null;
        switch (number)
        {
            case (1):
                m_Player = GM.GetComponent<GameMaster>().Player1;
                break;
            case (2):
                m_Player = GM.GetComponent<GameMaster>().Player2;
                break;
            case (3):
                m_Player = GM.GetComponent<GameMaster>().Player3;
                break;
            case (4):
                m_Player = GM.GetComponent<GameMaster>().Player4;
                break;
        }
        return m_Player;
    }

    void Update()
    {
        if (in_Pool)
        {
            bool enabled = GetComponent<Enemy>().enabled;
            Debug.Log("in_Pool_Update || enabled " + enabled);
            return;
        }

        if (!Setup_Finish)
        {
            if (HP < 0)
                Debug.LogWarning("!Setup_Finish || HP || " + HP);
            return;
        }

        if (isServer)
        {
            if (!Test.activeSelf)
                Test.SetActive(true);
            if (Test_Attacker)
            {
                Test_Timer += Time.deltaTime;
                if (Test_Timer >= 3)
                {
                    Test_Attacker = false;
                    Test_Timer = 0;
                    Test.GetComponent<MeshRenderer>().material = Test_Material_1;
                }
            }

            if (Enemy_object)
                GetComponent<MeshRenderer>().material = Test_Material_1;
            if (Attacker_Object)
                GetComponent<MeshRenderer>().material = Test_Material_2;
            if (Attacker_To_Enemy_Object)
                GetComponent<MeshRenderer>().material = Test_Material_3;
        }

        if (isServer && Pause)
            return;

        if (gameObject.tag != "Player01_Protector" && gameObject.tag != "Player02_Protector" &&
            gameObject.tag != "Player03_Protector" && gameObject.tag != "Player04_Protector")
        {
            float Distance = Vector3.Distance(transform.position, Last_Way_Point_POS);
            Distance_from_Start_Point = Distance_from_Last_Point + Distance;
        }
        #region HP || Dead
        if (Dead && isServer)
        {
            bool Dead_Times_Up = false;
            Dead_Timer += Time.deltaTime;
            if (Dead_Timer >= 1.0f && !Boomer)
                Dead_Times_Up = true;

            if (Dead_Timer >= 4.0f && Boomer)
                Dead_Times_Up = true;

            if (Dead_Times_Up)
            {
                Set_Object_To_Pool();
            }
            return;
        }

        if (!Dead && HP < 1 && isServer)
        {
            if (gameObject.tag == "Player01_Protector")
                GM.GetComponent<GameMaster>().Player1_Protector_Number--;
            if (gameObject.tag == "Player02_Protector")
                GM.GetComponent<GameMaster>().Player2_Protector_Number--;
            if (gameObject.tag == "Player03_Protector")
                GM.GetComponent<GameMaster>().Player3_Protector_Number--;
            if (gameObject.tag == "Player04_Protector")
                GM.GetComponent<GameMaster>().Player4_Protector_Number--;
            string Enemy_Tag = gameObject.tag;

            if (Original_Enemy)
                GM.GetComponent<GameMaster>().Enemy_Dead(Player_Number, Gold, (int)MAX_HP, Enemy_Tag, gameObject);

            if (Treasure)
            {
                Treasure = false;
                GM.GetComponent<GameMaster>().Treasure_Reward(Treasure_Rate, gameObject, Player_Number);
            }

            if (Resurrection)
            {
                Resurrection = false;
                HP = MAX_HP;
                Resurrect_Enemy(Player_Number);
                return;
            }
            //if (Protector_Object || Attacker_Object)
            //Debug.Log("Dead || " + gameObject.tag);
            gameObject.tag = "Dead";
            Dead = true;
            if (!Boomer)
                Server_Prepare_Target_Tell_Client_Dead();
            if (Boomer && !Boomed)
                Server_Boomer_Boom();
        }
        #endregion

        #region Ice_01_Start (Server)
        if (Ice_01_Start && isServer)
        {
            if (Speed >= Original_Speed)
            {
                float test_speed = temp_speed * ((100 - Slow_Rate) / 100);
                if (Speed > test_speed)
                {
                    Speed = test_speed;
                    temp_speed = Speed;
                    Server_Prepare_Target_Client_Update_Speed(Speed);
                }
            }
            if (!Ice_01)
            {
                Ice_01 = true;
                Ice_01_Start = false;
            }
        }
        if (Ice_01 && isServer)
        {
            Ice_01_Timer += Time.deltaTime;
            if (Ice_01_Timer > Ice_01_Time)
            {
                Ice_01_Timer = 0;
                Ice_01 = false;
                Speed = temp_speed;
                Server_Prepare_Target_Client_Update_Speed(Speed);
                //Debug.Log("Speed_2 || " + Speed);
            }
        }
        #endregion

        #region Fear
        if (Fear_01_Start && isServer)
        {
            int Random_Number = Random.Range(1, 101);
            if (Random_Number <= Fear_Rate)
            {
                Fear_01_Timer = 0;
                Fear_01 = true;
                Update_WayPoint();
                Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 1);
                Server_Prepare_Target_Set_Fear(true);
            }
            Fear_01_Start = false;
        }
        if (Fear_01 && isServer)
        {
            Fear_01_Timer += Time.deltaTime;
            if (Fear_01_Timer > Fear_01_Time)
            {
                Fear_01_Timer = 0;
                Fear_01 = false;
                Update_WayPoint();
                Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 2);
                Server_Prepare_Target_Set_Fear(false);
            }
        }
        #endregion

        #region Control
        if (Control_01_Start && isServer)
        {
            int Random_Number = Random.Range(1, 101);
            if (Random_Number <= Control_Rate)
            {
                Control_Enemy_Object = true;
                Control_01_Timer = 0;
                Control_01 = true;
                Server_Prepare_Target_Set_Control(true);
                Enemy_To_Control_Enemy();
            }
            Control_01_Start = false;
        }
        if (Control_01 && isServer)
        {
            Control_01_Timer += Time.deltaTime;
            if (Control_01_Timer > Control_01_Time)
            {
                Control_Enemy_Object = false;
                Control_01_Timer = 0;
                Control_01 = false;
                Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 4);
                Server_Prepare_Target_Set_Control(false);
                Attacker_To_Enemy();
            }
        }
        #endregion

        #region Stun_01_Start (Server)
        if (Stun_01_Start && isServer)
        {
            int Random_Number = Random.Range(1, 101);
            if (Random_Number <= Stun_Rate)
            {
                Stun_01_Timer = 0;
                Stun_01 = true;
                Move = false;
                Server_Prepare_Target_Set_Stun(true, Move, transform.position, Move_Point.position);
            }
            Stun_01_Start = false;
        }
        if (Stun_01 && isServer)
        {
            Stun_01_Timer += Time.deltaTime;
            if (Stun_01_Timer > Stun_01_Time)
            {
                Stun_01_Timer = 0;
                Stun_01 = false;
                Move = true;
                Server_Prepare_Target_Set_Stun(false, Move, transform.position, Move_Point.position);
            }
        }
        #endregion

        #region Chain_01_Start (Server)
        if (Chain_01_Start && isServer)
        {
            if (Chain_01)
                return;
            if (!Chain_01)
                Chain_01 = true;
            Chain_01_Start = false;
        }
        if (Chain_01 && isServer)
        {
            Chain_01_Timer += Time.deltaTime;
            if (Chain_01_Timer > Chain_01_Time)
            {
                Chain_01_Timer = 0;
                Chain_01 = false;
            }
        }
        #endregion

        #region Thief (Server)
        if (Thief)
        {
            Thief_Timer += Time.deltaTime;
            if (Thief_Timer > Thief_Time && Thief_Time != 0)
            {
                int Gold = (int)Thief_Gold_Rate;
                int Total_Gold_QTY_For_Gold_Bar = 0;
                if (Start_Steal)
                {
                    int Random_Number = Random.Range(1, 101);
                    if (Thief_Steal_Rate >= Random_Number)
                    {
                        GM.GetComponent<GameMaster>().Steal_Gold(Player_Number, Gold);
                        Total_Gold_QTY_For_Gold_Bar += Gold;
                    }
                }
                // Thief_Timer, Thief_Time, Thief_Gold_Rate, Thief_Steal_Rate, Thief_Steal_QTY
                Thief_Timer = 0;
                GM.GetComponent<GameMaster>().Add_Gold(Player_Number, Gold);
                Total_Gold_QTY_For_Gold_Bar += Gold;
                Instantiate_Gold_Bar(Total_Gold_QTY_For_Gold_Bar);
            }
        }
        #endregion

        if (isServer && !Dead)
        {
            // Enemy_object , Protector_Object , Attacker_Object
            GameObject _camera = GameObject.FindWithTag("MainCamera");
            Test_Tag_1.GetComponent<Text>().text = gameObject.tag;
            Test_Tag_2.GetComponent<Text>().text = Player_Number.ToString();
            Test_Canvas.transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

        }

        #region Server Move and Attack
        if (Move && isServer && !Dead && !in_Pool)
        {
            Update_WayPoint();
            transform.LookAt(Move_Point);
            float Distance_to_Move_Target_Point = Vector3.Distance(transform.position, Move_Point.position);

            if (Distance_to_Move_Target_Point < 0.1f && !in_Pool)
            {
                if (Enemy_object)
                    if (op_room_Code == (short)OP_Room_Code.Match1v1)
                        Get_1V1_Next_Enemy_Move_Target_Point(Move_Point);

                if (Protector_Object)
                    if (op_room_Code == (short)OP_Room_Code.Match1v1)
                        Get_1V1_Next_Protector_Move_Target_Point();

                if (Attacker_Object)
                {
                    bool Attacker_End_Point = Move_Point.gameObject.GetComponent<Object_Mount>().B_1V1_Attacker_End_Point;
                    if (Attacker_End_Point)
                    {
                        gameObject.GetComponent<Enemy>().Cancel_Special_Status(Original_Speed, "All");
                        gameObject.GetComponent<Enemy>().Server_Prepare_Target_Cancel_Special_Status(Original_Speed, "All");
                        if (op_room_Code == (short)OP_Room_Code.Match1v1)
                            GM.GetComponent<GameMaster>().Attacker_Change_To_Enemy(gameObject, Player_Number, HP, Damage, Speed, _object_Type);
                    }
                    if (!Attacker_End_Point)
                        Get_Next_Attacker_Move_Target_Point();
                }
                Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 5);
                Server_Prepare_Target_Set_Enemy_Rotation(transform, Move_Point.transform.position);
            }
            Vector3 dir = Move_Point.position - transform.position;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            if (Enemy_object || Protector_Object || Attacker_Object || Control_Enemy_Object && !in_Pool)
            {
                if (!Attacking_Target_Enemy)
                {
                    float distance = 0.75f;
                    if (Devil_Type == 1)
                        distance = 3.0f;
                    Attacking_Target_Enemy = Get_Opponent_By_Distance(distance);
                }
                if (Attacking_Target_Enemy && !Attacking && !Boomer)
                {
                    Attacking = true;
                    Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 6);
                }
                if (Attacking_Target_Enemy && Boomer && HP >= 1)
                {
                    Server_Boomer_Boom();
                }
            }
        }

        if (!Attacking_Target_Enemy && Attacking && isServer && !Dead && !in_Pool)
        {
            Attacking = false;
            Move = true;
            Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 7);
        }

        if (Attacking_Target_Enemy && Attacking && isServer && !Dead && !in_Pool)
        {
            if (Attacking_Timer == 0)
            {
                string time = Attacking_Timer.ToString();
                Fight(gameObject, Attacking_Target_Enemy);
            }
            Attacking_Timer += Time.deltaTime;
            if (Attacking_Timer >= 0.1f && !Animation_Set)
            {
                Check_Attacker_Attack();
                Animation_Set = true;
            }
            if (Attacking_Timer >= 1.1f)
            {
                Animation_Set = false;
                Attacking_Timer = 0;

                float current_HP = 99999;

                if (Attacking_Target_Enemy)
                    current_HP = Attacking_Target_Enemy.GetComponent<Enemy>().HP;

                Attacking_Target_Enemy = null;

                float distance = 0.75f;
                if (Devil_Type == 1)
                    distance = 3.0f;
                GameObject temp_Target_Enemy = Get_Opponent_By_Distance(distance);

                if (temp_Target_Enemy)
                    Attacking_Target_Enemy = temp_Target_Enemy;

                if (Start_Bite || Devil_Skill || !temp_Target_Enemy)
                {
                    Attacking = false;
                    string Reset_Animation_String = "Attack";
                    if (Start_Bite)
                        Reset_Animation_String = "Bite";
                    //Attacking_Target_Enemy.tag = "Untagged";
                    Start_Bite = false;
                    Move = true;
                    Attacking_Target_Enemy = null;
                    Server_Prepare_Target_Set_Enemy_Animation("Walk", Reset_Animation_String, true, " Dead current_HP " + current_HP);
                    Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 8);
                }

            }
        }

        if (Dragon_Bite)
        {
            Bite_Timer += Time.deltaTime;
            if (Bite_Timer >= 1)
            {
                Bite_Timer = 0;
                Bite_HP -= Damage;
                if (Bite_HP < 1)
                    Dragon_Bite = false;
            }
        }
        #endregion

        if (!isServer && Setup_Finish && !Local_Stop_Move_Rejoin_Wait_Setup_Finish)
        {
            #region Client_Check_Model_Any_Null
            if (Client_Model == null || Anim == null)
                Local_Check_Variable_Empty();

            #endregion
            if (Move && !Dead)
            {
                if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
                    Client_Model.GetComponent<Animator>().SetTrigger("Walk");
                transform.LookAt(Move_Point);
                float distance = Vector3.Distance(transform.position, Move_Point.transform.position);
                if (distance >= 0.2f)
                {
                    Vector3 dir = Move_Point.position - transform.position;
                    transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
                }
            }

            // bool Fire_01_Start, Wind_01_Start, Heal_01_Start, Poison_01_Start;
            #region Local Effect
            if (Impact_Start)
            {
                Check_Local_Effect_Time(Impact_Effect, Impact_Timer, Impact_Time, out Impact_Start, out Impact_Timer);
                //Impact_Timer += Time.deltaTime;
                //if (!Impact_Effect.activeSelf)
                //    Impact_Effect.SetActive(true);
                //if (Impact_Timer > Impact_Time)
                //{
                //    Impact_Start = false;
                //    Impact_Effect.SetActive(false);
                //    Impact_Timer = 0;
                //}
            }
            #endregion

            #region Fire_01_Effect
            if (Fire_01_Start)
            {
                GameObject Fire_Effect_01 = Client_Model.GetComponent<Object_Status>().Fire_Effect;
                Set_Local_Effect(Fire_Effect_01, Fire_01, "Fire_Effect_01", out Fire_01, out Fire_01_Start);
                //if (Fire_01)
                //    Reset_Effect("Fire_Effect_01", Fire_Effect_01);
                //if (!Fire_01)
                //    Fire_01 = true;
                //Fire_01_Start = false;
            }
            if (Fire_01)
            {
                GameObject Fire_Effect_01 = Client_Model.GetComponent<Object_Status>().Fire_Effect;
                Check_Local_Effect_Time(Fire_Effect_01, Fire_01_Timer, Fire_01_Time, out Fire_01, out Fire_01_Timer);

                //Fire_Effect_01.SetActive(true);
                //Fire_01_Timer += Time.deltaTime;
                //if (Fire_01_Timer > Fire_01_Time)
                //{
                //    Fire_01_Timer = 0;
                //    Fire_Effect_01.SetActive(false);
                //    Fire_01 = false;
                //}
            }
            #endregion

            #region Wind_01_Effect

            if (Wind_01_Start)
            {
                Set_Local_Effect(Wind_Effect_01, Wind_01, "Wind_Effect_01", out Wind_01, out Wind_01_Start);

                //if (Wind_01)
                //    Reset_Effect("Wind_Effect_01", Wind_Effect_01);
                //if (!Wind_01)
                //    Wind_01 = true;
                //Wind_01_Start = false;
            }
            if (Wind_01)
            {
                Check_Local_Effect_Time(Wind_Effect_01, Wind_01_Timer, Wind_01_Time, out Wind_01, out Wind_01_Timer);

                //Wind_Effect_01.SetActive(true);
                //Wind_01_Timer += Time.deltaTime;
                //if (Wind_01_Timer > Wind_01_Time)
                //{
                //    Wind_01_Timer = 0;
                //    Wind_Effect_01.SetActive(false);
                //    Wind_01 = false;
                //}

            }
            #endregion

            #region Heal_01_Effect
            if (Heal_01_Start)
            {
                Set_Local_Effect(Heal_Effect_01, Heal_01, "Heal_Effect_01", out Heal_01, out Heal_01_Start);
                //if (Heal_01)
                //    Reset_Effect("Heal_Effect_01", Heal_Effect_01);
                //if (!Heal_01)
                //    Heal_01 = true;
                //Heal_01_Start = false;
            }
            if (Heal_01)
            {
                Check_Local_Effect_Time(Heal_Effect_01, Heal_01_Timer, Heal_01_Time, out Heal_01, out Heal_01_Timer);
                //Heal_Effect_01.SetActive(true);
                //Heal_01_Timer += Time.deltaTime;
                //if (Heal_01_Timer > Heal_01_Time)
                //{
                //    Heal_01_Timer = 0;
                //    Heal_Effect_01.SetActive(false);
                //    Heal_01 = false;
                //}
            }
            #endregion

            #region Poison_01_Effect
            if (Poison_01_Start)
            {
                Set_Local_Effect(Poison_Effect_01, Poison_01, "Poison_Effect_01", out Poison_01, out Poison_01_Start);
                //if (Poison_01)
                //    Reset_Effect("Poison_Effect_01", Poison_Effect_01);
                //if (!Poison_01)
                //    Poison_01 = true;
                //Poison_01_Start = false;
            }
            if (Poison_01)
            {
                Check_Local_Effect_Time(Poison_Effect_01, Poison_01_Timer, Poison_01_Time, out Poison_01, out Poison_01_Timer);
                //Poison_Effect_01.SetActive(true);
                //Poison_01_Timer += Time.deltaTime;
                //if (Poison_01_Timer > Poison_01_Time)
                //{
                //    Poison_01_Timer = 0;
                //    Poison_Effect_01.SetActive(false);
                //    Poison_01 = false;
                //}
            }
            #endregion

            #region Ice_01_Effect
            if (Ice_01_Start)
            {
                GameObject Ice_Effect_01 = Client_Model.GetComponent<Object_Status>().Ice_Effect;
                Set_Local_Effect(Ice_Effect_01, Ice_01, "Ice_Effect_01", out Ice_01, out Ice_01_Start);
                //if (Ice_01)
                //    Reset_Effect("Ice_Effect_01", Ice_Effect_01);
                //if (!Ice_01)
                //    Ice_01 = true;
                //Ice_01_Start = false;
            }
            if (Ice_01)
            {
                GameObject Ice_Effect_01 = Client_Model.GetComponent<Object_Status>().Ice_Effect;
                Check_Local_Effect_Time(Ice_Effect_01, Ice_01_Timer, Ice_01_Time, out Ice_01, out Ice_01_Timer);
                //Ice_Effect_01.SetActive(true);
                //Ice_01_Timer += Time.deltaTime;
                //if (Ice_01_Timer > Ice_01_Time)
                //{
                //    Ice_01_Timer = 0;
                //    Ice_Effect_01.SetActive(false);
                //    Ice_01 = false;
                //}
            }
            #endregion

            #region Fear_01_Effect

            if (Fear_01)
                Debug.Log("Fear_01 || " + Fear_01);
            if (Fear_01)
                Fear_Effect_01.SetActive(true);
            if (!Fear_01)
                Fear_Effect_01.SetActive(false);
            #endregion

            #region Control_01_Effect
            if (Control_Effect_01 == null)
                Control_Effect_01 = Client_Model.GetComponent<Object_Status>().Control_Effect;
            if (Control_01)
                Control_Effect_01.SetActive(true);
            if (!Control_01)
                Control_Effect_01.SetActive(false);
            #endregion

            #region Stun_01_Effect
            if (Stun_Effect_01 == null)
                Stun_Effect_01 = Client_Model.GetComponent<Object_Status>().Stun_Effect;
            if (Stun_01)
            {
                Anim.enabled = false;
                Stun_Effect_01.SetActive(true);
            }

            if (!Stun_01)
            {
                Anim.enabled = true;
                Stun_Effect_01.SetActive(false);
            }

            #endregion

            #region Debuff_01_Effect
            if (Debuff_01_Start)
            {
                Set_Local_Effect(Debuff_Effect_01, Debuff_01, "Debuff_Effect_01", out Debuff_01, out Debuff_01_Start);
                //if (Debuff_01)
                //    Reset_Effect("Debuff_Effect_01", Debuff_Effect_01);
                //if (!Debuff_01)
                //    Debuff_01 = true;
                //Debuff_01_Start = false;
            }

            if (!Debuff_01)
            {
                Check_Local_Effect_Time(Debuff_Effect_01, Debuff_01_Timer, Debuff_01_Time, out Debuff_01, out Debuff_01_Timer);
                //Debuff_Effect_01.SetActive(true);
                //Debuff_01_Timer += Time.deltaTime;
                //if (Debuff_01_Timer > Debuff_01_Time)
                //{
                //    Debuff_01_Timer = 0;
                //    Debuff_Effect_01.SetActive(false);
                //    Debuff_01 = false;
                //}
            }
            #endregion

            #region Chain
            if (Chain_01_Start)
            {
                Check_Effect_Start(Chain_01_Start, Chain_01, out Chain_01_Start, out Chain_01);
                Change_Material(Black_Material, Black_Material);
            }

            if (Chain_01)
            {
                Chain_01_Timer += Time.deltaTime;
                Update_Effect_Timer(Chain_01_Timer, Chain_01_Time, Chain_Effect_01, out Chain_01_Timer, out Chain_01);
                if (!Chain_01)
                {
                    Change_Material(Original_Material, Horse_Material);
                }
            }
            #endregion

            #region Resurrection
            if (Resurrection)
            {
                GameObject resurrection_Effect = Client_Model.GetComponent<Object_Status>().Resurrection_Effect;
                if (!resurrection_Effect.activeSelf)
                    resurrection_Effect.SetActive(true);
            }
            #endregion

            if (Dead)
            {
                Dead_Timer += Time.deltaTime;
                if (Dead_Timer < 1.0f)
                {
                    Anim.enabled = false;
                    Dead_Change_Material();
                    //All_Effect_Deactive();
                }
            }
        }
    }

    void Server_Boomer_Boom()
    {
        Boomed = true;
        HP = 0;
        GameObject[] All_Near_Enemy = Get_All_Near_Enemy(transform.position);
        Damage_All_Near_Enemy(All_Near_Enemy, 0.1f, false, "null", "All"); // no enemy effect , effect name = null
        Server_Prepare_Target_Set_Enemy_Animation("Attack", "Walk", false, "Boomer");
    }

    void Check_Attacker_Attack()
    {
        if (Attacker_Object || Attacker_To_Enemy_Object)
        {
            //Enemy_object, Protector_Object, Attacker_Object, Control_Enemy_Object, Attacker_To_Enemy_Object
            int Owner = GM.GetComponent<GameMaster>().Enemy_Obj_Owner(Player_Number,
                Enemy_object, Protector_Object, Attacker_Object, Control_Enemy_Object, Attacker_To_Enemy_Object);
            GM.GetComponent<GameMaster>().Enemy_Attack_Counter(Owner);
        }
    }

    void Set_Local_Effect(GameObject Effect_Obj, bool Effect, string effect_Name, out bool outEffect, out bool Effect_Start)
    {
        outEffect = false;
        if (Effect)
            Reset_Effect(effect_Name, Effect_Obj);
        if (!Effect)
            outEffect = true;
        Effect_Start = false;
    }

    void Check_Local_Effect_Time(GameObject Effect_Obj, float m_Timer, float time, out bool Main_Effect, out float out_Timer)
    {
        out_Timer = m_Timer;
        Main_Effect = true;
        Effect_Obj.SetActive(true);
        m_Timer += Time.deltaTime;
        if (m_Timer > time)
        {
            out_Timer = 0;
            Effect_Obj.SetActive(false);
            Main_Effect = false;
        }
    }

    void Check_Effect_Start(bool Effect_Start, bool Effect, out bool m_Effect_Start, out bool m_Effect)
    {
        m_Effect = Effect;
        if (Effect)
            m_Effect = Effect;

        if (!Effect)
        {
            Effect = true;
            m_Effect = true;
        }
        m_Effect_Start = false;
    }

    void Update_Effect_Timer(float Timer, float time, GameObject Effect, out float m_timer, out bool Effect_On)
    {
        m_timer = Timer;
        Effect_On = true;
        if (Timer < time)
        {
            if (!Effect.activeSelf)
                Effect.SetActive(true);
        }
        if (Timer > time)
        {
            m_timer = 0;
            Effect_On = false;
            if (Effect.activeSelf)
                Effect.SetActive(false);
        }
    }

    void Update_WayPoint()
    {
        string Object_Tag = gameObject.tag;
        if (Object_Tag == "Player_01_Control_Enemy" || Object_Tag == "Player_02_Control_Enemy" ||
            Object_Tag == "Player_03_Control_Enemy" || Object_Tag == "Player_04_Control_Enemy" ||
            Object_Tag == "Enemy_01" || Object_Tag == "Enemy_02" || Object_Tag == "Enemy_03" || Object_Tag == "Enemy_04")
        {
            if (!Fear_01 && !Control_01)
                Move_Point = Move_Target_Point.transform;

            if (Fear_01 || Control_01)
                Move_Point = Previous_Point.transform;
        }
    }

    string Get_Enemy_Tag(bool Normal_Enemy_or_Control_Enemy)
    {
        string tag = null;
        string[] Normal_Enemy_Tag = new string[5];
        Normal_Enemy_Tag[1] = "Enemy_01";
        Normal_Enemy_Tag[2] = "Enemy_02";
        Normal_Enemy_Tag[3] = "Enemy_03";
        Normal_Enemy_Tag[4] = "Enemy_04";

        string[] Control_Enemy_Tag = new string[5];
        Control_Enemy_Tag[1] = "Player_01_Control_Enemy";
        Control_Enemy_Tag[2] = "Player_02_Control_Enemy";
        Control_Enemy_Tag[3] = "Player_03_Control_Enemy";
        Control_Enemy_Tag[4] = "Player_04_Control_Enemy";

        if (Normal_Enemy_or_Control_Enemy)
            tag = Normal_Enemy_Tag[Player_Number];
        if (!Normal_Enemy_or_Control_Enemy)
            tag = Control_Enemy_Tag[Player_Number];
        return tag;
    }

    void Cancel_Special_Status(float spd, string Name)
    {
        Speed = spd;
        if (Name == "Ice_01" || Name == "All")
        {
            Ice_01_Timer = 99;
            Speed = Original_Speed;
        }

        if (Name == "Fear_01" || Name == "All")
        {
            Fear_01_Timer = 99;
        }

        if (Name == "Control_01" || Name == "All")
        {
            Control_01_Timer = 99;
        }

        if (Name == "Stun_01" || Name == "All")
        {
            Stun_01_Timer = 99;
        }
    }

    void Change_Material(Material Body_Material, Material Horse_Material)
    {
        if (Client_Model == null) Local_Check_Variable_Empty();
        GameObject Body = Client_Model.GetComponent<Object_Status>().Body;
        if (Body != null)
            Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Body_Material;

        GameObject Horse = Client_Model.GetComponent<Object_Status>().Horse;
        if (Horse != null)
            Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Horse_Material;
    }

    void Dead_Change_Material()
    {
        if (Client_Model == null || Anim == null) Local_Check_Variable_Empty();
        Anim.enabled = false;
        GameObject Dead_Effect = Client_Model.GetComponent<Object_Status>().Dead_Effect;
        Dead_Effect.SetActive(true);

        Material Dead_Material = null;
        if (Enemy_object)
            Dead_Material = Client_Model.GetComponent<Object_Status>().Dead_Material_White;
        if (Protector_Object)
            Dead_Material = Client_Model.GetComponent<Object_Status>().Dead_Material_Black;
        if (Attacker_Object)
            Dead_Material = Client_Model.GetComponent<Object_Status>().Dead_Material_Red;

        GameObject Body = Client_Model.GetComponent<Object_Status>().Body;
        GameObject Horse = Client_Model.GetComponent<Object_Status>().Horse;
        Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Dead_Material;

        Material horse_material = null;

        if (Horse != null)
        {
            Horse.GetComponentInChildren<SkinnedMeshRenderer>().material = Dead_Material;
            horse_material = Horse.GetComponentInChildren<SkinnedMeshRenderer>().material;
        }

        Frame++;
        float Frame_Number = Frame * 0.02f;
        float value = Frame_Number;
        if (value >= 1)
            value = 1;
        Material dead_material = Body.GetComponentInChildren<SkinnedMeshRenderer>().material;

        dead_material.SetFloat("_Power", value);

        if (horse_material != null)
            horse_material.SetFloat("_Power", value);
    }

    void Fight(GameObject _Object_01, GameObject _Object_02)
    {
        float distance = Vector3.Distance(_Object_01.transform.position, _Object_02.transform.position);
        if (Devil_Type == 1 && distance >= 0.5f)
        {
            Move = false;
            Vector3 POS = Attacking_Target_Enemy.transform.position;
            GameObject[] All_Near_Enemy = Get_All_Near_Enemy(POS);
            Server_Prepare_Target_Set_Enemy_Rotation(transform, POS);
            Damage_All_Near_Enemy(All_Near_Enemy, 0.2f, true, "Explosion_Effect_01", "Devil_Type_1"); // false = no enemy effect
            Server_Prepare_Target_Set_Enemy_Animation("Attack", "Walk", false, "Fight || Devil_Type 1");
            Server_Prepare_Target_Set_Enemy_Effect(POS, 606); // Fire Pillar
            return;
        }

        if (Devil_Type == 2)
        {
            int Random_Number = Random.Range(1, 2);
            if (Rate >= Random_Number)
            {
                Move = false;
                Attacking = true;
                Devil_Skill = false;
                Vector3 POS = Attacking_Target_Enemy.transform.position;
                transform.LookAt(POS);
                Server_Prepare_Target_Set_Enemy_Rotation(transform, POS);
                Server_Prepare_Target_Set_Enemy_Animation("Attack", "Walk", false, "Fight || Devil_Type 2");
                Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 9);

                if (_Object_02.GetComponent<Enemy>().HP >= 1 && _Object_02.tag != "Untagged")
                {
                    _Object_02.GetComponent<Enemy>().Capture_Enemy();
                }
            }
            return;
        }

        if (_object_Type == 6 && !Dragon_Bite)
        {
            //Debug.Log("Damage || " + Damage);
            Start_Bite = true;
            Dragon_Bite = true;
            transform.LookAt(_Object_02.transform);
            Move = false;
            Attacking = true;

            Vector3 POS = Attacking_Target_Enemy.transform.position;
            Server_Prepare_Target_Set_Enemy_Rotation(transform, POS);
            Server_Prepare_Target_Set_Enemy_Animation("Bite", "Walk", false, "Fight || Devil_Type 6");
            Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 10);

            Vector3 Move_Point_POS = _Object_02.GetComponent<Enemy>().Move_Point.position;
            //_Object_02.tag = "Dead";
            _Object_02.transform.LookAt(_Object_01.transform);
            //_Object_02.GetComponent<Enemy>().Pause = true;
            _Object_02.GetComponent<Enemy>().Move = false;
            _Object_02.GetComponent<Enemy>().Server_Prepare_Target_Set_Run(Move, _Object_02.transform.position, Move_Point_POS, 11);
            //_Object_02.GetComponent<Enemy>().Original_Enemy = false;
            Max_Bite_HP = _Object_02.GetComponent<Enemy>().HP;
            Bite_HP = Max_Bite_HP;
            _Object_02.GetComponent<Enemy>().HP = 0;
            _Object_02.GetComponent<Enemy>().Dead_Timer = 1.5f;
            return;
        }

        Vector3 Object_01_POS = _Object_01.transform.position;
        Vector3 Object_02_POS = _Object_02.transform.position;

        _Object_01.transform.LookAt(_Object_02.transform);
        Server_Prepare_Target_Set_Enemy_Rotation(_Object_01.transform, Object_02_POS);
        _Object_01.GetComponent<Enemy>().Move = false;
        _Object_01.GetComponent<Enemy>().Attacking = true;

        string Enemy_Name = _Object_02.name;


        Server_Prepare_Target_Set_Enemy_Animation("Attack", "Walk", false, "Fight");

        bool Target_No_Damage = _Object_02.GetComponent<Enemy>().No_Damage;
        if (Target_No_Damage)
            return;

        short code = (short)Enemy_Code.Attack;

        //GameObject Enemy, float dmg, bool Heal_or_Damage, int C_Rate, int C_Damage, float time
        GM.GetComponent<GameMaster>().GM_Control_Enemy_Damage(Attacking_Target_Enemy, Damage, code, 10, 500, 1.0f, Enemy_Name, Enemy_Name);
    }

    public void Enemy_To_Control_Enemy()
    {
        if (HP < 1)
            return;

        Spwan_Enemy_Packet packet = Enemy_Obj_Packet();
        if (packet == null)
            return;

        GameMaster gm = GM.GetComponent<GameMaster>();
        short player_Number = packet.Player_Number;
        short Opponent_Number = gm.GM_Find_Opponent_Number(player_Number);

        string Tag = Enemy_To_Control_Enemy_Tag(Opponent_Number);
        tag = Tag;
        packet.Player_Number = Opponent_Number;
        packet.Source_Obj_Tag = Tag;

        Debug.Log("packet || " + packet.Tag + " || " + packet.Player_Number + " || " + HP);

        name = "Control_Enemy_" + Random.Range(1, 1001);

        if (!Attacking)
        {
            Vector3 POS = packet.Move_Target_Point.transform.position;
            Server_Prepare_Target_Set_Run(Move, transform.position, POS, 12);
        }
        Set_Enemy(GM, false, false, true, name, Tag, Opponent_Number, HP, Damage, Speed, 0, _object_Type, "Enemy_To_Control_Enemy");
    }

    public void Control_Enemy_To_Attacker()
    {
        if (HP < 1)
            return;

        string before_Tag = tag;
        int before_Player_Number = Player_Number;
        float before_DMG = Damage;
        float before_HP = HP;

        Spwan_Enemy_Packet packet = Enemy_Obj_Packet();
        if (packet == null)
            return;

        GameMaster gm = GM.GetComponent<GameMaster>();
        short player_Number = packet.Player_Number;
        short Opponent_Number = gm.GM_Find_Opponent_Number(player_Number);
        string Tag = Enemy_To_Control_Enemy_Tag(Opponent_Number);
        name = "Control_Enemy_" + Random.Range(1, 1001);

        if (!Attacking)
        {
            Vector3 POS = packet.Move_Target_Point.transform.position;
            Server_Prepare_Target_Set_Run(Move, transform.position, POS, 12);
        }

        Debug.Log("Control_Enemy_To_Attacker || " + before_Tag + " || " + before_Player_Number + " || " + before_DMG + " || "
            + before_HP + " || " + Tag + " || " + Opponent_Number + " || " + Damage + " || " + HP);

        Set_Enemy(GM, false, false, true, name, Tag, Opponent_Number, HP, Damage, Speed, 0, _object_Type, "Enemy_To_Control_Enemy");
    }

    void Attacker_To_Enemy()
    {
        if (HP < 1)
            return;

        string before_Tag = tag;
        int before_Player_Number = Player_Number;
        float before_DMG = Damage;
        float before_HP = HP;

        Spwan_Enemy_Packet packet = Enemy_Obj_Packet();
        if (packet == null)
            return;

        GameMaster gm = GM.GetComponent<GameMaster>();
        short player_Number = packet.Player_Number;
        short Opponent_Number = gm.GM_Find_Opponent_Number(player_Number);
        string Tag = Get_Enemy_Tag_By_Player_Number(Opponent_Number);
        name = "Attacker_To_Enemy";

        if (!Attacking)
        {
            Vector3 POS = packet.Move_Target_Point.transform.position;
            Server_Prepare_Target_Set_Run(Move, transform.position, POS, 12);
        }

        Debug.Log("Attacker_To_Enemy || " + before_Tag + " || " + before_Player_Number + " || " + before_DMG + " || "
    + before_HP + " || " + Tag + " || " + Opponent_Number + " || " + Damage + " || " + HP);

        Set_Enemy(GM, true, false, false, name, Tag, Opponent_Number, HP, Damage, Speed, 0, _object_Type, "Attacker_To_Enemy");
    }

    Spwan_Enemy_Packet Enemy_Obj_Packet()
    {
        tag = "Untagged";
        if (tag == "Dead")
            return null;

        Spwan_Enemy_Packet packet = new Spwan_Enemy_Packet();
        packet.Source_Obj = gameObject;
        packet.Player_Number = (short)Player_Number;
        packet.HP = MAX_HP;
        packet.Damage = Damage;
        packet.Speed = Original_Speed;
        packet.Type_Code = (short)_object_Type;
        packet.Attack_Protector_Code = enemy_Code;

        short player_Number = packet.Player_Number;

        GameMaster gm = GM.GetComponent<GameMaster>();
        GameObject Temp_Move_Point = Move_Target_Point;
        Move_Target_Point = Previous_Point;
        Previous_Point = Temp_Move_Point;
        Attacking_Target_Enemy = null;

        Cancel_Special_Status(Original_Speed, "All"); // Cancel all special status , e.g fear , control , ice ...
        Server_Prepare_Target_Cancel_Special_Status(Original_Speed, "All"); // Cancel all special status , e.g fear , control , ice ...
        packet.Move_Target_Point = Move_Target_Point;
        packet.Previous_Point = Previous_Point;
        return packet;
    }

    public void Capture_Enemy()
    {
        if (HP < 1)
            return;

        string before_Tag = tag;
        int before_Player_Number = Player_Number;
        float before_DMG = Damage;
        float before_HP = HP;
        string Tag = gameObject.tag;

        Spwan_Enemy_Packet packet = Enemy_Obj_Packet();
        if (packet == null)
            return;

        GameMaster gm = GM.GetComponent<GameMaster>();
        short player_Number = packet.Player_Number;
        short Opponent_Number = gm.GM_Find_Opponent_Number(player_Number);
        packet.Source_Obj_Tag = Tag;
        packet.Player_Number = Opponent_Number;
        Debug.Log("Tag || " + Tag);
        packet.Tag = Capture_Enemy_Tag(Opponent_Number, Tag);

        packet.POS = packet.Move_Target_Point.transform.position;
        HP = MAX_HP;

        No_Damage = true;
        GetComponent<Enemy>().enabled = false;
        Move = false;
        name = "Capture_Attacker " + Random.Range(1, 1001);

        Debug.Log("Capture_Enemy || " + before_Tag + " || " + before_Player_Number + " || " + before_DMG + " || "
        + before_HP + " || " + packet.Tag + " || " + Opponent_Number + " || " + Damage + " || " + HP);

        Server_Prepare_Target_Change_Material(1); // tell Client use transfer material , 1 Transfer
        Server_Prepare_Target_Set_Run(Move, transform.position, packet.POS, 12);
        GM.GetComponent<GameMaster>().Capture_Enemy(packet);
    }

    void Resurrect_Enemy(int player_Number)
    {
        GameMaster gm = GM.GetComponent<GameMaster>();
        short Opponent_Number = gm.GM_Find_Opponent_Number((short)player_Number);
        gameObject.tag = "Untagged";
        No_Damage = true;
        Attacking_Target_Enemy = null;
        Attacking = false;
        Attacking_Timer = 99;
        Start_Bite = false;
        Devil_Skill = false;
        HP = MAX_HP;

        string Tag = Player_Number_To_Attacker_Tag(Opponent_Number);
        Move = false;
        Cancel_Special_Status(Speed, "All"); // Cancel all special status , e.g fear , control , ice ...
        Server_Prepare_Target_Cancel_Special_Status(Speed, "All"); // Cancel all special status , e.g fear , control , ice ...
        Original_Enemy = false;

        GameObject[] Attacker_Point = gm.Get_Nearest_Object_By_Tag(gameObject, "Attacker_Point");
        GameObject move_point = gm.Calculate_Nearest_Point(Attacker_Point, gameObject, true); // Attacker_or_Protector ,true = Attacker

        Vector3 Obj2_Move_Point = move_point.transform.position;
        Server_Prepare_Target_Set_Run(Move, transform.position, Obj2_Move_Point, 13);

        if (MAX_HP < 0)
            Debug.LogWarning("Enemy Max_HP || " + MAX_HP);

        Server_Prepare_Target_Change_Material(2); // tell Client use transfer material , 1 Transfer , 2 Resurrect
        StartCoroutine(Send_Enemy_To_Attacker(3.0f, Tag, Opponent_Number)); // 1 Captrue , 2 Resurrect
    }

    string Capture_Enemy_Tag(short player_Number, string Tag)
    {
        bool Attacker = false, Enemy = false;
        if (Tag.Contains("Enemy"))
            Attacker = true;
        if (Tag.Contains("Attacker") || Tag.Contains("Protector"))
            Enemy = true;

        switch (player_Number)
        {
            case (1):
                if (Attacker) return "Player01_Attacker";
                if (Enemy) return "Enemy_01";
                break;
            case (2):
                if (Attacker) return "Player02_Attacker";
                if (Enemy) return "Enemy_02";
                break;
            case (3):
                if (Attacker) return "Player03_Attacker";
                if (Enemy) return "Enemy_03";
                break;
            case (4):
                if (Attacker) return "Player04_Attacker";
                if (Enemy) return "Enemy_04";
                break;
        }
        return null;
    }

    string Player_Number_To_Attacker_Tag(int player_Number)
    {
        string Tag = null;
        if (player_Number == 1)
            Tag = "Player01_Attacker";
        if (player_Number == 2)
            Tag = "Player02_Attacker";
        if (player_Number == 3)
            Tag = "Player03_Attacker";
        if (player_Number == 4)
            Tag = "Player04_Attacker";
        return Tag;
    }

    string Enemy_To_Control_Enemy_Tag(short player_Number)
    {
        switch (player_Number)
        {
            case (1): return "Player_01_Control_Enemy";
            case (2): return "Player_02_Control_Enemy";
            case (3): return "Player_03_Control_Enemy";
            case (4): return "Player_04_Control_Enemy";
        }
        return null;
    }

    string Get_Enemy_Tag_By_Player_Number(short player_Number)
    {
        switch (player_Number)
        {
            case (1): return "Enemy_01";
            case (2): return "Enemy_02";
            case (3): return "Enemy_03";
            case (4): return "Enemy_04";
        }
        return null;
    }

    IEnumerator Send_Enemy_To_Attacker(float Time, string Tag, short player_Number)
    {
        yield return new WaitForSeconds(Time);
        gameObject.tag = Tag;

        string test = "Send_Enemy_To_Attacker || Resurrect";
        string name = "Resurrect_Enemy";
        HP = MAX_HP;

        if (HP < 1 || MAX_HP < 1)
            Debug.LogWarning("hp || " + HP + " || max_hp || " + MAX_HP);

        Speed = Original_Speed;

        if (Speed < 1)
            Debug.LogWarning("Send_Enemy_To_Attacker_2 || speed || " + Speed + " || " + Original_Speed + " || " + Tag + " || " + HP
                + " || " + _object_Type);

        if (HP < 1)
            yield break;

        GM.GetComponent<GameMaster>().Get_Attacker_Point(gameObject);
        Set_Enemy(GM, false, false, true, name, Tag, (short)player_Number, HP, Damage, Original_Speed, Gold, _object_Type, test);
    }

    public void Reset_Run()
    {
        Vector3 target_point_pos = Move_Point.transform.position;
        Server_Prepare_Target_Set_Run(Move, transform.position, target_point_pos, 14);
    }

    void Boomer_Boom()
    {
        GameObject Effect = Get_Effect(506);
        Debug.Log("Boomer_Boom || " + Effect.activeSelf);
        Deactive_Enemy_Object(Effect, 0.2f, true);
        Deactive_Enemy_Object(Client_Model, 0.2f, false);
    }

    GameObject[] Get_All_Near_Enemy(Vector3 POS)
    {
        GameObject[] Near_Enemy = new GameObject[20];
        string[] Tag = GM.GetComponent<GameMaster>().Get_Opponent_Tag(Player_Number);
        GameObject[] All_Near_Enemy_Obj = GM.GetComponent<GameMaster>().Enemy_Tag_To_Enemy_Obj(Tag);
        int Number = 0;

        foreach (GameObject enemy in All_Near_Enemy_Obj)
        {
            float distanceToEnemy = Vector3.Distance(POS, enemy.transform.position);
            if (distanceToEnemy <= 0.5f)
            {
                Near_Enemy[Number] = enemy;
                Number++;
            }
        }
        return Near_Enemy;
    }

    bool Check_Opponent_Tag(GameObject enemy_Obj)
    {
        if (!enemy_Obj)
            return false;

        string[] tags = Get_Opponent_Tag();
        for (int i = 0; i < tags.Length; i++)
        {
            Debug.Log("tags || " + tags[i]);
            if (enemy_Obj.tag == tags[i])
                return true;
        }
        return false;
    }

    string[] Get_Opponent_Tag()
    {
        return GM.GetComponent<GameMaster>().Get_Opponent_Tag(Player_Number);
    }

    GameObject Get_Opponent_By_Distance(float distance)
    {
        string[] Tag = Get_Opponent_Tag();
        GameObject[] enemies = GM.GetComponent<GameMaster>().Enemy_Tag_To_Enemy_Obj(Tag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= distance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    #region Waypoint
    void Get_1V1_Next_Enemy_Move_Target_Point(Transform current_Point)
    {

        if (!GM) Local_Check_Variable_Empty();

        bool End_Point = false, Attacker_End_Point = false;
        if (op_room_Code == (short)OP_Room_Code.Match1v1)
        {
            Attacker_End_Point = current_Point.GetComponent<Object_Mount>().B_1V1_Attacker_End_Point;
            End_Point = current_Point.GetComponent<Object_Mount>().B_1V1_Enemy_End_Point;
        }

        if (op_room_Code == (short)OP_Room_Code.Match2op)
        {
            Attacker_End_Point = current_Point.GetComponent<Object_Mount>().B_2OP_Attacker_End_Point;
            End_Point = current_Point.GetComponent<Object_Mount>().B_2OP_Enemy_End_Point;
        }

        if (!End_Point && !Fear_01 && !Control_01)
        {
            Last_Way_Point_POS = Move_Point.transform.position;
            Distance_from_Last_Point = Move_Point.GetComponent<Object_Mount>().Distance_from_Start_Point;
        }

        if (End_Point)
        {
            if (Boomer)
            {
                Server_Boomer_Boom();
                return;
            }
            GameMaster gm = GM.GetComponent<GameMaster>();
            short point_player_number = current_Point.GetComponent<Object_Mount>().Player_Number;
            gm.Core_Damage(point_player_number, (int)HP);
            Set_Object_To_Pool();
            Server_Prepare_Target_Tell_Client_Dead();
            return;
        }

        if (!End_Point)
        {
            GameMaster gm = GM.GetComponent<GameMaster>();
            if (Fear_01 || Control_01)
            {
                GameObject Previous_Point_01 = gm.Get_Point_From_Object_Mount(current_Point.gameObject, "Enemy", (short)Enemy_Code.Previous_Point, 1, OP_Path_Code);
                GameObject Previous_Point_02 = gm.Get_Point_From_Object_Mount(current_Point.gameObject, "Enemy", (short)Enemy_Code.Previous_Point, 2, OP_Path_Code);
                Previous_Point = gm.Get_Point(Previous_Point_01, Previous_Point_02);
                Move_Target_Point = current_Point.gameObject;
            }
            if (!Fear_01 && !Control_01)
            {
                GameObject Next_Point_01 = gm.Get_Point_From_Object_Mount(current_Point.gameObject, "Enemy", (short)Enemy_Code.Next_Point, 1, OP_Path_Code);
                GameObject Next_Point_02 = gm.Get_Point_From_Object_Mount(current_Point.gameObject, "Enemy", (short)Enemy_Code.Next_Point, 2, OP_Path_Code);
                Previous_Point = current_Point.gameObject;
                Move_Target_Point = gm.Get_Point(Next_Point_01, Next_Point_02);
            }
        }

        if (op_room_Code == (short)OP_Room_Code.Match1v1)
        {
            // Attacker change to Enemy
            if (Attacker_End_Point)
            {
                All_Model_Deactive();
                All_Effect_Deactive();
                if (Speed < 1)
                    Debug.LogWarning("Enemy || " + Speed);
                Debug.Log("Attacker_End_Point || " + Player_Number + " || " + gameObject.tag);
                GM.GetComponent<GameMaster>().Attacker_Change_To_Enemy(gameObject, Player_Number, HP, Damage, Speed, _object_Type);
                if (Fear_01)
                    Fear_01 = false;
                if (Control_01)
                    Control_01 = false;
            }
        }

        if (!Attacker_End_Point)
            Update_WayPoint();

        Set_Test_Point(Move_Point.gameObject);
    }

    void Get_Next_Attacker_Move_Target_Point()
    {
        if (!GM) Local_Check_Variable_Empty();
        GameObject Next_Point_01 = GM.GetComponent<GameMaster>().Get_Point_From_Object_Mount(Move_Point.gameObject, "Attacker", (short)Enemy_Code.Next_Point, 1, OP_Path_Code);
        GameObject Next_Point_02 = GM.GetComponent<GameMaster>().Get_Point_From_Object_Mount(Move_Point.gameObject, "Attacker", (short)Enemy_Code.Next_Point, 2, OP_Path_Code);
        GameObject Next_Point = GM.GetComponent<GameMaster>().Get_Point(Next_Point_01, Next_Point_02);
        if (!Next_Point_01 && !Next_Point_02)
            Debug.Log("Next_Point || " + Next_Point);
        Previous_Point = Move_Target_Point;
        Move_Target_Point = Next_Point;
        Move_Point = Move_Target_Point.transform;
        Set_Test_Point(Move_Point.gameObject);
    }

    void Get_1V1_Next_Protector_Move_Target_Point()
    {
        GameObject Temp = null;
        bool Check_Patrol_Start_Point = false;
        if (!Check_Patrol_Start_Point && !Patroling)
        {
            Patroling = Move_Point.gameObject.GetComponent<Object_Mount>().B_1V1_Protector_Patrol_Point;
            if (Patroling)
                Check_Patrol_Start_Point = true;
        }

        if (Check_Patrol_Start_Point)
        {
            int Random_Number = Random.Range(1, 3);
            if (Random_Number == 1)
                Protector_Clockwise = true;
            if (Random_Number == 2)
                Protector_Clockwise = false;
        }

        if (Protector_Clockwise)
        {
            if (!Patroling)
                Patroling = Move_Point.gameObject.GetComponent<Object_Mount>().B_1V1_Protector_Patrol_Point;
            Previous_Point = Move_Point.gameObject;
            Temp = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point;
            if (Temp == null)
                Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Previous_Point.transform;
            if (Temp != null)
                Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point.transform;
            Move_Target_Point = Move_Point.gameObject;
            //Debug.Log("Name || 2 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
        }
        if (!Protector_Clockwise)
        {
            if (!Patroling)
                Patroling = Move_Point.gameObject.GetComponent<Object_Mount>().B_1V1_Protector_Patrol_Point;
            Previous_Point = Move_Point.gameObject;
            Temp = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Previous_Point;
            if (Temp == null)
            {
                if (Move_Point == null)
                    Debug.Log("Move_Point || " + Move_Point);

                if (Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point == null)
                    Debug.Log("Next_Point is null || " + Move_Point + " || " + gameObject.tag + " || Protector_Object || " +
                        Protector_Object + " || " + Test_Where);

                Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point.transform;
            }
            if (Temp != null)
            {
                Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Previous_Point.transform;
            }
            Move_Target_Point = Move_Point.gameObject;
            //Debug.Log("Name || 2 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
        }

        Server_Prepare_Target_Set_Patroling(Patroling);
        Move_Point = Move_Target_Point.transform;
        Set_Test_Point(Move_Point.gameObject);
        //Debug.Log("Name || 3 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
    }
    #endregion

    public void Spwan_Sub_Bullet(int Tower_Type, float damage, int critical_Rate, int critical_Damage)
    {
        Vector3 POS = Vector3.zero;
        GameObject Sub_Bullet = null;
        bool Get_From_Pool = false;
        float Random_X = Random.Range(0, 0.21f);
        float Random_Z = Random.Range(0, 0.21f);
        if (Tower_Type == 204)
        {
            POS = transform.position + new Vector3(Random_X, 0.01f, Random_Z);

            Sub_Bullet = ground_Boom;
            GameObject Ground_Boom = null;

            Ground_Boom = GM.GetComponent<GameMaster>().Get_Object_From_Pool_By_Tag("Ground_Boom");
            if (Ground_Boom)
                Get_From_Pool = true;
            if (!Ground_Boom)
            {
                Ground_Boom = Instantiate(Sub_Bullet, POS, Quaternion.identity);
                NetworkServer.Spawn(Ground_Boom);
            }
            Ground_Boom.GetComponent<Object_Status_Network>().Set_Ground_Boom(Damage, gameObject.tag, critical_Rate, critical_Damage, GM);
            Sub_Bullet = Ground_Boom;
        }
        //Conn, Get_From_Pool, Ground_Boom, POS, damage
        Server_Prepare_Target_Spawn_Sub_Bullet(Get_From_Pool, Sub_Bullet, POS, damage);
    }

    IEnumerator Destory_GameOBject(GameObject Obj, float Time)
    {
        yield return new WaitForSeconds(Time);
        Destroy(Obj);
    }

    void Send_Camera_To_Test()
    {
        GameObject Player = Get_Player(Player_Number);
        Server_Prepare_Target_Send_Camera_To_Test(Player);
    }

    #region Effect

    public void Local_Set_Enemy_Effect(int Tower_Type, float time)
    {
        GameObject Effect = Get_Effect(Tower_Type);
        if (Effect.activeSelf)
            Effect.SetActive(false);
        if (!Effect.activeSelf)
            Effect.SetActive(true);
        StartCoroutine(Deactive_Object(Effect, time, false));
    }

    public void Set_Enemy_Effect(int Tower_Type, float Time)
    {
        if (Client_Model == null)
        {
            Local_Check_Variable_Empty();
            return;
        }
        GameObject Effect = Get_Effect(Tower_Type);
        Set_Effect_Start(Tower_Type, true);
        Set_Effect_Time(Tower_Type, Time);
    }

    void Set_Effect_Start(int Type_Number, bool Active_or_Deactive)
    {
        switch (Type_Number)
        {
            case (101):
            case (204):
                Fire_01_Start = Active_or_Deactive;
                break;
            case (102):
                Wind_01_Start = Active_or_Deactive;
                break;
            case (105):
                Heal_01_Start = Active_or_Deactive;
                break;
            case (201):
                Poison_01_Start = Active_or_Deactive;
                break;
            case (203):
                Ice_01_Start = Active_or_Deactive;
                break;
            case (206):
                Fear_01_Start = Active_or_Deactive;
                break;
            case (301):
                Stun_01_Start = Active_or_Deactive;
                break;
            case (302):
                Debuff_01_Start = Active_or_Deactive;
                break;
        }
    }

    void Set_Effect_Time(int Type_Number, float time)
    {
        switch (Type_Number)
        {
            case (101):
            case (204):
                Fire_01_Time = time;
                break;
            case (102):
                Wind_01_Time = time;
                break;
            case (105):
            case (712):
                Heal_01_Time = time;
                break;
            case (201):
                Poison_01_Time = time;
                break;
            case (203):
                Ice_01_Time = time;
                break;
            case (206):
                Fear_01_Time = time;
                break;
            case (301):
                Stun_01_Time = time;
                break;
            case (302):
                Debuff_01_Time = time;
                break;
            case (707):
                Impact_Effect = Impact_Effect_02;
                Impact_Time = time;
                Impact_Start = true;
                break;
        }
    }

    void Set_Basic_Impact_Effect(short Attack_Code)
    {
        GameObject Effect = null;
        if (Client_Model == null) Local_Check_Variable_Empty();

        Effect = Impact_Effect;

        ParticleSystem effect = Effect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = effect.main;
        ParticleSystemRenderer material = Effect.GetComponent<ParticleSystemRenderer>();

        if (!Effect.activeSelf)
        {
            if (Attack_Code == (short)Enemy_Code.Normal_Attack)
            {
                main.startSpeed = new ParticleSystem.MinMaxCurve(1, 3);
                main.startSize = new ParticleSystem.MinMaxCurve(0.02f, 0.05f);
                main.maxParticles = 10;
            }

            if (Attack_Code == (short)Enemy_Code.Critical_Attack)
            {
                main.startSpeed = new ParticleSystem.MinMaxCurve(2, 4);
                main.startSize = new ParticleSystem.MinMaxCurve(0.2f, 0.3f);
                main.maxParticles = 30;
            }

            if (Enemy_object)
                material.material = White;

            if (Protector_Object)
                material.material = Black;

            if (Attacker_Object)
                material.material = Red;

            Effect.SetActive(true);
        }

        StartCoroutine(Deactive_Object(Effect, 0.21f, false));
    }

    public GameObject Get_Effect(int Type_Number)
    {
        GameObject Effect = null;
        if (Client_Model == null) Local_Check_Variable_Empty();

        switch (Type_Number)
        {
            case (0):
                Effect = Impact_Effect_01;
                break;
            case (1):
                if (Protector_Object || Attacker_Object)
                    Effect = Slash_Effect_01_P;
                if (Enemy_object)
                    Effect = Slash_Effect_01_E;
                break;
            case (2):
                Effect = Slash_Effect_Horse;
                break;
            case (3):
                Effect = Devil_Effect_01;
                break;
            case (4):
                Effect = Devil_Effect_02;
                break;
            case (5):
                Effect = Devil_Effect_03;
                break;
            case (101):
            case (204):
                GameObject Fire_Effect_01 = Client_Model.GetComponent<Object_Status>().Fire_Effect;
                Effect = Fire_Effect_01;
                break;
            case (102):
                Effect = Wind_Effect_01;
                break;
            case (105):
                Effect = Heal_Effect_01;
                break;
            case (201):
                Effect = Poison_Effect_01;
                break;
            case (203):
                GameObject Ice_Effect_01 = Client_Model.GetComponent<Object_Status>().Ice_Effect;
                Effect = Ice_Effect_01;
                break;
            case (206):
                Effect = Fear_Effect_01;
                break;
            case (301):
                GameObject Stun_Effect_01 = Client_Model.GetComponent<Object_Status>().Stun_Effect;
                Effect = Stun_Effect_01;
                break;
            case (302):
                Effect = Debuff_Effect_01;
                break;
            case (505):
                Effect = Slash_Effect_Rnaomd_Rotation;
                break;
            case (506):
                Effect = Boomer_Effect;
                break;
            case (707):
                Effect = Impact_Effect_02;
                break;
            case (712):
                Effect = Client_Model.GetComponent<Object_Status>().Heal_Effect_02;
                break;
            case (715):
                Effect = Client_Model.GetComponent<Object_Status>().Armor_Effect_01;
                break;
        }
        return Effect;
    }

    void Reset_Effect(string name, GameObject obj)
    {
        obj.SetActive(false);
        obj.SetActive(true);
        switch (name)
        {
            case ("Fire_Effect_01"):
                Fire_01_Timer = 0;
                Fire_01 = false;
                break;
            case ("Wind_Effect_01"):
                Wind_01_Timer = 0;
                Wind_01 = false;
                break;
            case ("Heal_Effect_01"):
                Heal_01_Timer = 0;
                Heal_01 = false;
                break;
            case ("Poison_Effect_01"):
                Poison_01_Timer = 0;
                Poison_01 = false;
                break;
            case ("Ice_Effect_01"):
                Ice_01_Timer = 0;
                Ice_01 = false;
                break;
            case ("Fear_Effect_01"):
                Fear_01_Timer = 0;
                Fear_01 = false;
                break;
            case ("Stun_Effect_01"):
                Stun_01_Timer = 0;
                Stun_01 = false;
                break;
            case ("Debuff_Effect_01"):
                Debuff_01_Timer = 0;
                Debuff_01 = false;
                break;
        }
    }

    #endregion

    public void Enemy_Animation(string Name, bool Active_or_Deactive, bool Self_or_Target)
    {
        switch (Name)
        {
            case ("Impact"):
                if (Self_or_Target)
                {
                    GameObject Impact_Effect = Get_Effect(0);
                    if (!Impact_Effect)
                        break;
                    Impact_Effect.SetActive(!Active_or_Deactive);
                    Impact_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Slash"):
                if (Self_or_Target)
                {
                    GameObject Slash_Effect = Get_Effect(1);
                    if (!Slash_Effect)
                        break;
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Horse_Slash"):
                if (Self_or_Target)
                {
                    GameObject Slash_Effect = Get_Effect(2);
                    if (!Slash_Effect)
                        break;
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Devil_01"):
                if (Self_or_Target)
                {
                    GameObject Slash_Effect = Get_Effect(3);
                    if (!Slash_Effect)
                        break;
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Devil_02"):
                if (Self_or_Target)
                {
                    GameObject Slash_Effect = Get_Effect(4);
                    if (!Slash_Effect)
                        break;
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Devil_03"):
                if (Self_or_Target)
                {
                    GameObject Slash_Effect = Get_Effect(5);
                    if (!Slash_Effect)
                        break;
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;
            case ("Dragon_Head_Rush"):
                if (Self_or_Target)
                {
                    GameObject Effect = null;
                    if (Enemy_object)
                        Effect = Dragon_Head_Rush_White;
                    if (Attacker_Object)
                        Effect = Dragon_Head_Rush_Red;
                    if (Protector_Object)
                        Effect = Dragon_Head_Rush_Black;
                    if (!Effect)
                        break;
                    Effect.SetActive(!Active_or_Deactive);
                    Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                break;
        }
    }

    public void Active_Or_Deactive_Object(string name, float timer, bool Active_or_Deactive)
    {
        Server_Prepare_Target_Active_or_Deactive_Effect(name, timer, Active_or_Deactive);
    }

    public void Deactive_Enemy_Object(GameObject obj, float Time, bool Active_or_Deactive)
    {
        if (In_List)
            return;
        if (obj.name == "Explosion_Smoke_02")
            Debug.Log("Explosion_Smoke_02 || " + Boomer_Effect.activeSelf);

        StartCoroutine(Deactive_Object(obj, Time, Active_or_Deactive));
    }

    IEnumerator Deactive_Object(GameObject obj, float Time, bool Active_or_Deactive)
    {
        if (In_List)
            yield break;
        yield return new WaitForSeconds(Time);
        if (Active_or_Deactive && obj.activeSelf)
            obj.SetActive(false);
        obj.SetActive(Active_or_Deactive);
    }

    #region Fear

    public void Enemy_Set_Fear()
    {
        Fear_01 = true; // for Test
        int Fear_Time_Test = Random.Range(1, 11);
        Fear_01_Time = Fear_Time_Test;
        Update_WayPoint();
        Server_Prepare_Target_Set_Run(Move, transform.position, Move_Point.position, 15);
    }

    #endregion

    #region Environment_Effect || Resurrect
    void Environment_Effect(int Type)
    {
        Vector3 POS = Vector3.zero;
        GameObject Effect = null;
        if (Type == 707)
        {
            All_Effect_Deactive();
            Effect = Resurrection_HolyLight_Effect;
            POS = new Vector3(transform.position.x, 1.4f, transform.position.z);

            if (Client_Model == null) Client_Model = Check_Client_Model_Type();
            GameObject Model = gameObject.GetComponent<Enemy>().Client_Model;
            GameObject Body = Model.GetComponent<Object_Status>().Body;
        }

        GameObject m_effect = Instantiate(Effect, POS, Quaternion.identity);
        Destroy(m_effect, 3.0f);
    }
    #endregion

    #region Damage and HP Bar

    public void Set_Armor_Rate(bool Buff_or_Debuff, float Amount, string Tower_Slot)
    {
        if (Buff_or_Debuff)
        {
            if (!Armor)
            {
                Armor = true;
                Server_Prepare_Target_Active_or_Deactive_Effect(715, true, Tower_Slot);
            }
            Armor_Rate += (int)Amount;
        }

        if (!Buff_or_Debuff)
            Armor_Rate -= (int)Amount;
        if (Armor_Rate >= 75)
            Armor_Rate = 75;
        if (Armor_Rate <= 0)
            Armor_Rate = 0;
    }

    void Damage_All_Near_Enemy(GameObject[] All_Near_Enemy, float time, bool Active_Effect, string Effect_Name, string who)
    {
        for (int i = 0; i < All_Near_Enemy.Length; i++)
        {
            if (All_Near_Enemy[i] != null)
            {
                string Name = All_Near_Enemy[i].name;
                StartCoroutine(Damage_After_Time(All_Near_Enemy[i], time, Name, who));
                if (Active_Effect)
                {
                    All_Near_Enemy[i].GetComponent<Enemy>().Active_Or_Deactive_Object("Explosion_Effect_01", 0.5f, true);
                }
            }
        }
    }

    IEnumerator Damage_After_Time(GameObject enemy, float Time, string Enemy_Name, string who)
    {
        if (Enemy_Name != enemy.name || enemy.tag == "Untagged")
            yield break;

        yield return new WaitForSeconds(Time);
        bool enemy_No_Damage = enemy.GetComponent<Enemy>().No_Damage;
        if (enemy_No_Damage)
            yield break;
        short Code = (short)Enemy_Code.Attack;
        GM.GetComponent<GameMaster>().GM_Control_Enemy_Damage(enemy, Damage, Code, 10, 500, 0, Enemy_Name, who);
    }

    public void Enemy_Damage(float dmg, short Code, int Critical_Rate, int Critical_Damage, string Enemy_Name, string who)
    {
        short Type_Code = 0;
        if (gameObject.name != Enemy_Name)
        {
            //Debug.LogWarning("gameObject.name != Enemy_Name");
            return;
        }

        if (Armor && Code == (short)Enemy_Code.Attack)
        {
            Armor = false;
            Server_Prepare_Target_Active_or_Deactive_Effect(715, false, who);
            return;
        }

        if (No_Damage)
            return;

        Who_Attack = who;
        if (In_List)
            return;

        bool Critical_Attack = false;
        float Base_DMG = 0;
        int Random_Number = Random.Range(1, 101);
        if (Random_Number < Critical_Rate)
            Critical_Attack = true;

        float Original_HP = HP;
        Vector3 POS = transform.position + new Vector3(0, 1.5f, 0);
        float Final_Damage = dmg;
        if (Code == (short)Enemy_Code.Attack && gameObject.name == Enemy_Name)
        {
            Base_DMG = (dmg / 100) * (100 - Armor_Rate);

            if (!Critical_Attack)
            {
                Final_Damage = Base_DMG;
                Type_Code = (short)Enemy_Code.Normal_Attack;
            }

            if (Critical_Attack)
            {
                Final_Damage = Base_DMG * (Critical_Damage / 100);
                Type_Code = (short)Enemy_Code.Critical_Attack;
            }

            HP -= Final_Damage;
        }

        if (Code == (short)Enemy_Code.Heal && gameObject.name == Enemy_Name)
        {
            HP += Final_Damage;
            Type_Code = (short)Enemy_Code.Heal;
        }

        Update_HP(Final_Damage, Original_HP, HP, Type_Code);
    }

    public void Local_Heal_HP_Bar(float value)
    {
        float Original_HP = HP;
        Vector3 POS = transform.position + new Vector3(0, 1.5f, 0);
        float after_HP = HP + value;
        short heal = (short)Enemy_Code.Heal;
        short Bar_code = (short)Enemy_Code.HP_Bar;
        Local_HP_Bar(Bar_code, POS, value, Original_HP, after_HP, heal);
    }

    void Local_HP_Bar(short Bar_Code, Vector3 POS, float Damage, float Original_HP, float after_HP, short attack_or_heal_code)
    {
        GameObject m_Pool_Manager = GameObject.Find("Pool_Manager");
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        if (!m_local_Manager)
            return;
        GameObject Camera = m_local_Manager.GetComponent<Local_Manager>().Get_Camera();
        if (Bar_Code == (short)Enemy_Code.HP_Bar)
        {
            int HP_Bar_List_Count = m_Pool_Manager.GetComponent<Pool_Manager>().HP_Bar_Pool.Count;
            Debug.Log("HP_Bar_Pool Count || " + HP_Bar_List_Count);
            GameObject hp_bar = m_local_Manager.GetComponent<Local_Manager>().Get_HP_Bar_Form_Pool();
            if (hp_bar != null)
            {
                hp_bar.transform.position = POS;
                hp_bar.SetActive(true);
            }
            if (hp_bar == null)
            {
                Debug.Log("Instantiate || hp_bar");
                hp_bar = Instantiate(HP_Bar, POS, Quaternion.identity);
            }

            //bool heal_or_damage, float Damage, float hp , float hp_after_action, GameObject Camera)
            hp_bar.GetComponent<Object_Status>().Set_HP_Bar(attack_or_heal_code, Damage, Original_HP, after_HP, Camera);
        }

        int DMG_Bar_List_Count = m_Pool_Manager.GetComponent<Pool_Manager>().Damage_Bar_Pool.Count;
        Debug.Log("DMG_Bar_List_Count || " + DMG_Bar_List_Count);
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
        damage_bar.GetComponent<Object_Status>().Set_Damage_Bar(attack_or_heal_code, Damage, Camera);
    }

    void Instantiate_Gold_Bar(float Gold)
    {
        Server_Prepare_Target_Send_Gold_Tag_To_All_Client(Gold);
    }

    void Update_HP(float Damage, float Original_HP, float after_HP, short code)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Local_Update_HP(Conn, Damage, Original_HP, after_HP, code);
        }
    }

    void Local_Update_HP(NetworkConnection Conn, float Damage, float Original_HP, float after_HP, short code)
    {
        Target_Local_Update_HP(Conn, Damage, Original_HP, after_HP, code);
    }

    [TargetRpc]
    public void Target_Local_Update_HP(NetworkConnection conn, float Damage, float Original_HP, float after_HP, short code)
    {
        HP = after_HP;
        string Type = null;

        if (Enemy_object)
            Type = "Enemy";
        if (Protector_Object)
            Type = "Protector";
        if (Attacker_Object)
            Type = "Attacker";

        if (Client_Model == null)
            Client_Model = Check_Client_Model_Type();
        if (Client_Model == null)
            return;

        if (HP_Canvas_Text == null)
            HP_Canvas_Text = Client_Model.GetComponent<Object_Status>().HP_Canvas_Text;

        HP_Canvas_Text.GetComponent<Text>().text = after_HP.ToString();

        // Damage Bar
        Vector3 POS = transform.position + new Vector3(0, 1.5f, 0);
        GameObject m_Pool_Manager = GameObject.Find("Pool_Manager");
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        if (!m_local_Manager)
            return;

        GameObject Camera = m_local_Manager.GetComponent<Local_Manager>().Get_Camera();

        int DMG_Bar_List_Count = m_Pool_Manager.GetComponent<Pool_Manager>().Damage_Bar_Pool.Count;
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

        damage_bar.GetComponent<Object_Status>().Set_Damage_Bar(code, Damage, Camera);

        Set_Basic_Impact_Effect(code);
    }

    #endregion

    public void Set_Test_Point_Name(string Name)
    {
        Server_Prepare_Target_Set_Test_Point_Name(Name);
    }

    public void Set_Test_Point(GameObject point)
    {
        string Name = point.gameObject.name;
        Server_Prepare_Target_Set_Test_Point_Name(Name);
        if (loc_spwan == null)
        {
            loc_spwan = point;
            return;
        }
        if (loc_1 == null)
        {
            loc_1 = point;
            return;
        }
        if (loc_2 == null)
        {
            loc_2 = point;
            return;
        }
        if (loc_3 == null)
        {
            loc_3 = point;
            return;
        }
        if (loc_4 == null)
        {
            loc_4 = point;
            return;
        }
        if (loc_5 == null)
        {
            loc_5 = point;
            return;
        }
        if (loc_6 == null)
        {
            loc_6 = point;
            return;
        }
        if (loc_7 == null)
        {
            loc_7 = point;
            return;
        }
        if (loc_8 == null)
        {
            loc_8 = point;
            return;
        }
        if (loc_9 == null)
        {
            loc_9 = point;
            return;
        }
    }

    void Short_Cut()
    {
        Server_Prepare_Target_Set_Enemy_Type(false, false, false, 0, 0, 0, Vector3.zero, Speed, null, 4, 0, null); // Local Set Enemy
        Update();
        Boomer_Boom();
        Fight(null, null);
        Dead_Change_Material();
    }

    #region Treasure

    public void Set_Treasure(float Rate)
    {
        Treasure_Rate = Rate;
        Treasure = true;
    }

    public void Tell_Client_Local_Treasure(int Type, int QTY)
    {
        Server_Prepare_Local_Treasure(Type, QTY);
    }

    #endregion

    #region Local_Check_Variable_Empty // for Reconnect Game

    void Local_Check_Variable_Empty()
    {
        bool Object_Variable_Empty = Check_Object_Variable_Empty();
        if (Object_Variable_Empty)
        {
            GameObject Local_Player = GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Local_Player;
            if (!Local_Player) return;
            Local_Player.GetComponent<Player_Network>().Client_Reconnect_Game_Request_Enemy_Refresh_Status(gameObject);
            return;
        }
        if (Client_Model == null)
            Client_Model = Check_Client_Model_Type();
        if (Anim == null)
            Anim = Client_Model.GetComponent<Animator>();

    }

    bool Check_Object_Variable_Empty()
    {
        if (!GM) return true;
        if (Player_Number == 0) return true;
        if (_object_Type == 0) return true;
        if (!Enemy_object && !Protector_Object && !Attacker_Object) return true;
        return false;
    }

    public void Refresh_Status(NetworkConnection conn)
    {
        bool enemy = Enemy_object;
        bool protector = Protector_Object;
        bool attacker = Attacker_Object;
        float hp = HP;
        float dmg = Damage;
        float speed = Speed;
        int player_number = Player_Number;
        int Type = _object_Type;
        int devil_type = Devil_Type;
        string Name = gameObject.name;
        string tag = gameObject.tag;
        Vector3 target_point_pos = Move_Target_Point.transform.position;

        Target_ReFresh_Enemy_Type(conn, enemy, protector, attacker, player_number, hp, dmg, target_point_pos, speed, tag, Type, devil_type, Name);
    }

    [TargetRpc]
    public void Target_ReFresh_Enemy_Type(NetworkConnection conn, bool enemy, bool protector, bool attacker, int player_number, float hp, float dmg,
        Vector3 target_point_pos, float speed, string tag, int Type, int devil_type, string Name)
    {
        Local_Set_Enemy_Type(enemy, protector, attacker, player_number, hp, dmg, target_point_pos, speed, tag, Type, devil_type, Name);
    }

    #endregion

    #region Server_To_Local Target_RPC

    void Server_Prepare_Local_Treasure(int Type, int QTY)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Local_Treasure(Conn, Type, QTY);
        }
    }

    [TargetRpc]
    void Target_Local_Treasure(NetworkConnection Conn, int Type, int QTY)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        GameObject treasure_Box = m_local_Manager.GetComponent<Local_Manager>().Get_Treasure_Box_Form_Pool();

        if (treasure_Box != null)
            treasure_Box.SetActive(true);

        if (treasure_Box == null)
            treasure_Box = Instantiate(Treasure_Box, transform.position, Quaternion.identity);

        treasure_Box.GetComponent<Object_Status>().Set_Treasure_Box(Type, QTY);
    }

    void Server_Prepare_Target_Send_Gold_Tag_To_All_Client(float Gold)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Send_Gold_Tag_To_All_Client(Conn, Gold);
        }
    }

    [TargetRpc]
    void Target_Send_Gold_Tag_To_All_Client(NetworkConnection Conn, float Gold)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        GameObject Camera = m_local_Manager.GetComponent<Local_Manager>().Get_Camera();
        Vector3 POS = transform.position + new Vector3(0, 1.5f, 0);
        GameObject gold_bar = Instantiate(Gold_Bar, POS, Quaternion.identity);
        gold_bar.GetComponent<Object_Status>().Set_Gold_Bar((int)Gold, Camera);
    }

    void Server_Prepare_Target_Clear_Enemy_All_Status()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Clear_Enemy_All_Status(Conn);
        }
    }

    [TargetRpc]
    void Target_Clear_Enemy_All_Status(NetworkConnection Conn)
    {
        Clear_Enemy_All_Status();
    }

    void Server_Prepare_Target_Deactive_Resurrection()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Deactive_Resurrection(Conn);
        }
    }

    [TargetRpc]
    void Target_Deactive_Resurrection(NetworkConnection Conn)
    {
        Resurrection = false;
    }

    void Server_Prepare_Target_Set_Enemy_Type(bool enemy, bool protector, bool attacker, int player_number, float hp, float dmg,
        Vector3 target_point_pos, float speed, string tag, int Type, int devil_type, string Name)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Enemy_Type(Conn, enemy, protector, attacker, player_number, hp, dmg, target_point_pos, speed,
                    tag, Type, devil_type, Name);
        }
    }

    [TargetRpc]
    public void Target_Set_Enemy_Type(NetworkConnection Conn, bool enemy, bool protector, bool attacker, int player_number, float hp,
        float dmg, Vector3 target_point_pos, float speed, string tag, int Type, int devil_type, string Name)
    {
        Local_Set_Enemy_Type(enemy, protector, attacker, player_number, hp, dmg, target_point_pos, speed, tag, Type, devil_type, Name);
    }

    void Server_Prepare_Target_Client_Update_Speed(float spd)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Client_Update_Speed(Conn, spd);
        }
    }

    [TargetRpc]
    void Target_Client_Update_Speed(NetworkConnection Conn, float spd)
    {
        Speed = spd;
    }

    void Server_Prepare_Target_Cancel_Special_Status(float spd, string Name)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Cancel_Special_Status(Conn, spd, Name);
        }
    }

    [TargetRpc]
    void Target_Cancel_Special_Status(NetworkConnection Conn, float spd, string Name)
    {
        Cancel_Special_Status(spd, Name);
    }

    void Server_Prepare_Target_Tell_Client_Dead()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Tell_Client_Dead(Conn);
        }
    }

    [TargetRpc]
    void Target_Tell_Client_Dead(NetworkConnection Conn)
    {
        Dead = true;
        if (Anim != null)
            Anim.Rebind();

        //if (Anim != null)
        //    Anim.enabled = false;
    }

    void Server_Prepare_Target_Set_HP(GameObject enemy, float hp, float dmg)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_HP(Conn, enemy, hp, dmg);
        }
    }

    [TargetRpc]
    void Target_Set_HP(NetworkConnection Conn, GameObject enemy, float hp, float dmg)
    {
        if (enemy == null)
            return;
        enemy.GetComponent<Enemy>().Damage = dmg;
        enemy.GetComponent<Enemy>().HP = hp;
    }

    public void Server_Prepare_Target_Set_Run(bool move, Vector3 POS, Vector3 target_point_pos, int Where)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();

        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Run(Conn, move, POS, target_point_pos, Where);
        }
    }

    [TargetRpc]
    public void Target_Set_Run(NetworkConnection Conn, bool move, Vector3 POS, Vector3 target_point_pos, int Where)
    {
        if (Pause && !gameObject.activeSelf)
        {
            gameObject.GetComponent<Enemy>().enabled = true;
            gameObject.SetActive(true);
        }

        Move = move;
        transform.position = POS;
        if (Move_Point != null)
            Move_Point.transform.position = target_point_pos;
        transform.LookAt(Move_Point);
    }

    void Server_Prepare_Target_Set_Stun(bool stun, bool move, Vector3 POS, Vector3 target_point_pos)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Stun(Conn, stun, move, POS, target_point_pos);
        }
    }

    [TargetRpc]
    void Target_Set_Stun(NetworkConnection Conn, bool stun, bool move, Vector3 POS, Vector3 target_point_pos)
    {
        Stun_01 = stun;
        Move = move;
        if (!Dead)
            transform.position = POS;
        if (Move_Point != null)
            Move_Point.transform.position = target_point_pos;
    }

    void Server_Prepare_Target_Set_Fear(bool fear)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Fear(Conn, fear);
        }
    }

    [TargetRpc]
    void Target_Set_Fear(NetworkConnection Conn, bool fear)
    {
        Fear_01 = fear;
    }

    void Server_Prepare_Target_Set_Control(bool control)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Control(Conn, control);
        }
    }

    [TargetRpc]
    void Target_Set_Control(NetworkConnection Conn, bool control)
    {
        Control_01 = control;
    }

    void Server_Prepare_Target_Set_Enemy_Animation(string Set_Animation, string Reset_Animation, bool move, string where)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Enemy_Animation(Conn, Set_Animation, Reset_Animation, move, where);
        }
    }

    [TargetRpc]
    public void Target_Set_Enemy_Animation(NetworkConnection Conn, string Set_Animation, string Reset_Animation, bool move, string where)
    {
        Move = move;

        if (Client_Model == null || Anim == null)
        {
            Local_Check_Variable_Empty();
            if (Client_Model == null || Anim == null)
                return;
        }

        if (Set_Animation != null && Client_Model.activeSelf)
            Client_Model.GetComponent<Animator>().SetTrigger(Set_Animation);

        if (Reset_Animation != null && Client_Model.activeSelf)
            Client_Model.GetComponent<Animator>().ResetTrigger(Reset_Animation);

        if (Set_Animation == "Attack" && Boomer && Client_Model.activeSelf)
            Boomer_Boom();
    }

    public void Server_Prepare_Target_Set_Enemy_Rotation(Transform m_Transform, Vector3 Look_This_POS)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Enemy_Rotation(Conn, m_Transform, Look_This_POS);
        }
    }

    [TargetRpc]
    public void Target_Set_Enemy_Rotation(NetworkConnection Conn, Transform m_Transform, Vector3 Look_This_POS)
    {
        m_Transform.LookAt(Look_This_POS);
    }

    void Server_Prepare_Target_Change_Material(int Type)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Change_Material(Conn, Type);
        }
    }

    [TargetRpc]
    void Target_Change_Material(NetworkConnection Conn, int Type) // 1 , Transfer // 2, Resurrect
    {
        if (Client_Model == null) Local_Check_Variable_Empty();

        GameObject Model = Client_Model;

        if (Model == null)
        {
            Debug.Log("Enemy_object || Enemy_object || " + Enemy_object + " || Protector_Object || " + Protector_Object
    + " || Attacker_Object || " + Attacker_Object + " || " + gameObject.name + " || " + Type + " || " + gameObject.tag);
            Cmd_Server_Obj_Name(gameObject);
            return;
        }

        GameObject Body = Model.GetComponent<Object_Status>().Body;
        if (Type == 1)
        {
            Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Transfer_Material;
            Model.GetComponent<Object_Status>().Enemy_Transfer = true;
        }
        if (Type == 2)
        {
            Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Resurrect_Material;
            Environment_Effect(707);
        }
    }

    [Command]
    void Cmd_Server_Obj_Name(GameObject _Obj)
    {
        Debug.Log("Cmd_Server_Obj_Name || " + _Obj.name);
        NetworkConnection My_Connection = GetComponent<NetworkIdentity>().connectionToClient;
        string name = gameObject.name;
        Target_Tell_Local_Obj_Name(My_Connection, name);
    }

    [TargetRpc]
    void Target_Tell_Local_Obj_Name(NetworkConnection Conn, string name) // 1 , Transfer // 2, Resurrect
    {
        Debug.Log("Target_Tell_Local_Obj_Name || " + name);
    }

    void Server_Prepare_Target_Set_Patroling(bool patroling)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Patroling(Conn, patroling);
        }
    }

    [TargetRpc]
    void Target_Set_Patroling(NetworkConnection Conn, bool patroling)
    {
        Patroling = patroling;
    }

    void Server_Prepare_Target_Spawn_Sub_Bullet(bool Get_From_Pool, GameObject Ground_Boom, Vector3 POS, float damage)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Spawn_Sub_Bullet(Conn, Get_From_Pool, Ground_Boom, POS, damage);
        }
    }

    [TargetRpc]
    void Target_Spawn_Sub_Bullet(NetworkConnection Conn, bool Get_From_Pool, GameObject Ground_Boom, Vector3 POS, float Damage)
    {
        GameObject m_Pool_Manager = GameObject.Find("Pool_Manager");
        if (Get_From_Pool)
            Ground_Boom = m_Pool_Manager.GetComponent<Pool_Manager>().Get_Object_From_Pool_By_Tag("Ground_Boom");
        if (Ground_Boom != null)
        {
            Ground_Boom.transform.position = POS;
            Ground_Boom.SetActive(true);
        }
        if (Ground_Boom == null)
        {
            Ground_Boom = Instantiate(ground_Boom, POS, Quaternion.identity);
        }

        Ground_Boom.GetComponent<Object_Status_Network>().Effect_01.SetActive(true);
        Ground_Boom.GetComponent<Object_Status_Network>().Damage = Damage;
    }

    void Server_Prepare_Target_Send_Camera_To_Test(GameObject Player)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Send_Camera_To_Test(Conn, Player);
        }
    }

    [TargetRpc]
    void Target_Send_Camera_To_Test(NetworkConnection Conn, GameObject Player)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        Local_Manager lm = m_local_Manager.GetComponent<Local_Manager>();
        GameObject camera = lm.Get_Camera((short)Player_Number);
        Transform tran = gameObject.transform;
        GameObject Test_Bar = tran.Find("Test_Bar").gameObject;
        Test_Bar.GetComponent<Object_Status>().Set_Face_Camera_Test(camera);
    }

    void Server_Prepare_Target_Set_Test_Point_Name(string Name)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Test_Point_Name(Conn, Name);
        }
    }

    [TargetRpc]
    void Target_Set_Test_Point_Name(NetworkConnection Conn, string Name)
    {
        if (local_loc_spwan == "Empty")
        {
            local_loc_spwan = Name;
            return;
        }
        if (local_loc_1 == "Empty")
        {
            local_loc_1 = Name;
            return;
        }
        if (local_loc_2 == "Empty")
        {
            local_loc_2 = Name;
            return;
        }
        if (local_loc_3 == "Empty")
        {
            local_loc_3 = Name;
            return;
        }
        if (local_loc_4 == "Empty")
        {
            local_loc_4 = Name;
            return;
        }
        if (local_loc_5 == "Empty")
        {
            local_loc_5 = Name;
            return;
        }
        if (local_loc_6 == "Empty")
        {
            local_loc_6 = Name;
            return;
        }
        if (local_loc_7 == "Empty")
        {
            local_loc_7 = Name;
            return;
        }
        if (local_loc_8 == "Empty")
        {
            local_loc_8 = Name;
            return;
        }
        if (local_loc_9 == "Empty")
        {
            local_loc_9 = Name;
            return;
        }
    }

    public void Server_Prepare_Target_Set_Enemy_Effect(int Tower_Type, float time, bool Active_or_Deactive)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Enemy_Effect(Conn, Tower_Type, time, Active_or_Deactive);
        }
    }

    [TargetRpc]
    public void Target_Set_Enemy_Effect(NetworkConnection Conn, int Tower_Type, float time, bool Active_or_Deactive)
    {
        GameObject Effect = Get_Effect(Tower_Type);
        Effect.SetActive(Active_or_Deactive);
        Deactive_Object(Effect, time, false);
    }

    void Server_Prepare_Target_Set_Enemy_Effect(Vector3 POS, int Type)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Spwan_Effect(Conn, POS, Type);
        }
    }

    [TargetRpc]
    void Target_Spwan_Effect(NetworkConnection Conn, Vector3 POS, int Type)
    {
        GameObject Effect = null;
        if (Type == 606)
        {
            Effect = Bullet_Devil_03;
        }
        GameObject Local_effect = Instantiate(Effect, POS, Quaternion.identity);
        StartCoroutine(Destory_GameOBject(Local_effect, 2.5f));
    }

    public void Server_Prepare_Target_Set_Special_Effect(int type_number)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Set_Special_Effect(Conn, type_number);
        }
    }

    [TargetRpc]
    public void Target_Set_Special_Effect(NetworkConnection Conn, int type_number)
    {
        if (type_number == 707)
            Resurrection = true;
        if (type_number == 708)
            Treasure_Effect.SetActive(true);
    }

    void Server_Prepare_Target_Active_or_Deactive_Effect(int Type_Number, bool Active_or_Deactive, string Tower_Slot)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Active_or_Deactive_Effect(Conn, Type_Number, Active_or_Deactive, Tower_Slot);
        }
    }

    [TargetRpc]
    void Target_Active_or_Deactive_Effect(NetworkConnection Conn, int Type_Number, bool Active_or_Deactive, string Tower_Slot)
    {
        if (Type_Number == 715)
            Armor = Active_or_Deactive;
        GameObject Effect = Get_Effect(Type_Number);
        if (Effect != null)
            Effect.SetActive(Active_or_Deactive);

        bool Test_Effect_Active = Effect.activeSelf;

        if (Armor && !Test_Effect_Active)
            Debug.LogWarning("Armor || " + Armor + " || Test_Effect_Active || " + Test_Effect_Active);
        if (!Armor && Test_Effect_Active)
            Debug.LogWarning("Armor || " + Armor + " || Test_Effect_Active || " + Test_Effect_Active);
    }

    void Server_Prepare_Target_Active_or_Deactive_Effect(string _obj, float timer, bool Active_or_Deactive)
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Active_or_Deactive_Object(Conn, _obj, timer, Active_or_Deactive);
        }
    }

    void Set_Object_To_Pool()
    {
        in_Pool = true;
        All_Model_Deactive();
        Clear_Enemy_All_Status();
        transform.position = new Vector3(20, 0, 0);
        List<GameObject> pool_list = GM.GetComponent<GameMaster>().Enemy;
        pool_list.Add(gameObject);
        Server_Prepare_Target_Local_Send_Obj_To_Pool();
        float _hp = HP;
        float _max_HP = MAX_HP;
        GetComponent<Enemy>().enabled = false;
    }

    void Clear_Enemy_All_Status()
    {
        Old_HP = 0; Who_Attack = null; In_List = true;
        Pause = true; Impact_Start = false; Impact_Time = 0; Impact_Timer = 0;
        Original_Enemy = false; gameObject.tag = "Untagged"; gameObject.name = "0";
        Enemy_object = false; Protector_Object = false; Attacker_Object = false; Control_Enemy_Object = false;
        Attacker_To_Enemy_Object = false; _object_Type = 0; Devil_Type = 0; Attacking = false; Dead = false;
        Attacking_Timer = 0; Attacking_Target_Enemy = null; HP = 0; MAX_HP = 0; Damage = 0; Original_Speed = 0; temp_speed = 0; Gold = 0;
        Speed = 0; Armor = false; Armor_Rate = 0; Patroling = false; Protector_Clockwise = false;
        Move_Target_Point = null; Previous_Point = null; Move = false; Setup_Finish = false; Animation_Set = false;
        Frame = 0; Conn = null; Client_Model = null; Distance_from_Last_Point = 0; Distance_from_Start_Point = 0;
        Last_Way_Point_POS = Vector3.zero; Anim = null; Slow_Rate = 0; Fear_Rate = 0; Stun_Rate = 0; Control_Rate = 0; Attack_Bouns = 0;
        Special_Effect_QTY = 0; Special_Effect_Balance_QTY = 0; Special_Effect_Time = 0; Special_Effect_Timer = 0;
        Ice_01_Start = false; Fear_01_Start = false; Stun_01_Start = false; Control_01_Start = false; Start_Steal = false;
        Chain_01_Start = false; Chain_01 = false; Fire_01_Start = false; Wind_01_Start = false; Heal_01_Start = false;
        Ice_01_Time = 0; Fear_01_Time = 0; Stun_01_Time = 0; Control_01_Time = 0; Chain_01_Time = 0;
        Poison_01_Start = false; Explosion_Effect_01_Start = false; Debuff_01_Start = false; Fire_01 = false; Wind_01 = false;
        Heal_01 = false; Poison_01 = false; Ice_01 = false; Explosion_01 = false; Fear_01 = false; Stun_01 = false; Debuff_01 = false;
        Control_01 = false; Boomer = false; Thief = false; Dragon_Bite = false; Start_Bite = false; Devil_Skill = false;
        Resurrection = false;
        Fire_01_Timer = 0; Wind_01_Timer = 0; Heal_01_Timer = 0; Poison_01_Timer = 0; Ice_01_Timer = 0; Explosion_01_Timer = 0;
        Fear_01_Timer = 0; Stun_01_Timer = 0; Debuff_01_Timer = 0; Control_01_Timer = 0; Chain_01_Timer = 0; Fire_01_Time = 0;
        Wind_01_Time = 0; Heal_01_Time = 0; Poison_01_Time = 0; Explosion_01_Time = 0; Debuff_01_Time = 0; Bite_Timer = 0;
        Max_Bite_HP = 0; Bite_HP = 0; Thief_Timer = 0; Thief_Time = 0; Thief_Gold_Rate = 0; Thief_Steal_Rate = 0; Thief_Steal_QTY = 0;
        Rate = 0;
        _enemy = false; _protector = false; _attacker = false; _fall_attacker = false; _fall_protector = false; loc_spwan = null;
        loc_1 = null; loc_2 = null; loc_3 = null; loc_4 = null; loc_5 = null; loc_6 = null; loc_7 = null; loc_8 = null; loc_9 = null;
    }

    void All_Model_Deactive()
    {
        if (Test_Capture != null)
            Test_Capture.SetActive(false);
        if (Test_Resurrection != null)
            Test_Resurrection.SetActive(false);
        GameObject[] Obj_Array = new GameObject[] { Enemy_01 , Protector_01 , Horse_Attacker_01 ,
            Thief_01,Boomer_01,Devil_01,Devil_02,Devil_03,Dragon_Head_Black };

        for (int i = 0; i < Obj_Array.Length; i++)
        {
            if (Obj_Array[i] != null)
            {
                if (Obj_Array[i].activeSelf)
                    Obj_Array[i].SetActive(false);
            }
        }
    }

    void All_Effect_Deactive()
    {
        if (isServer)
            return;

        GameObject[] Obj_Array = new GameObject[] { Slash_Effect_01_P,Slash_Effect_Rnaomd_Rotation,
        Slash_Effect_02_P,Slash_Effect_03_P,Slash_Effect_01_E,Slash_Effect_02_E,Slash_Effect_03_E,Slash_Effect_Horse,Devil_Effect_01,
        Devil_Effect_02,Devil_Effect_03,Impact_Effect_01,Impact_Effect_02,Impact_Effect_03,Heal_Effect_01,Poison_Effect_01,
        Wind_Effect_01,Explosion_Effect_01,Fear_Effect_01,Debuff_Effect_01,Thunder_Effect_01,Chain_Effect_01,Dragon_Head_Rush_Black,
        Dragon_Head_Rush_Red,Dragon_Head_Rush_White,Stun_Effect_01,Control_Effect_01,Bullet_Devil_03,Boomer_Effect, Resurrection_Effect,
        Treasure_Effect,Heal_Effect_02 ,Armor_Effect_01
        };

        GameObject[] Enemy_Array = Set_Object_Status(Enemy_01);
        GameObject[] Protector_Array = Set_Object_Status(Protector_01);
        GameObject[] Horse_Attacker_Array = Set_Object_Status(Horse_Attacker_01);
        GameObject[] Thief_Array = Set_Object_Status(Thief_01);
        GameObject[] Boomer_Array = Set_Object_Status(Boomer_01);
        GameObject[] Devil_01_Array = Set_Object_Status(Devil_01);
        GameObject[] Devil_02_Array = Set_Object_Status(Devil_02);
        GameObject[] Devil_03_Array = Set_Object_Status(Devil_03);
        GameObject[] Dragon_Head_Array = Set_Object_Status(Dragon_Head_Black);

        if_obj_Active_set_Deactive(Obj_Array);
        if_obj_Active_set_Deactive(Enemy_Array);
        if_obj_Active_set_Deactive(Protector_Array);
        if_obj_Active_set_Deactive(Horse_Attacker_Array);
        if_obj_Active_set_Deactive(Thief_Array);
        if_obj_Active_set_Deactive(Boomer_Array);
        if_obj_Active_set_Deactive(Devil_01_Array);
        if_obj_Active_set_Deactive(Devil_02_Array);
        if_obj_Active_set_Deactive(Devil_03_Array);
        if_obj_Active_set_Deactive(Dragon_Head_Array);

        GameObject[] Set_Object_Status(GameObject Body)
        {
            GameObject[] Object_Status_Array = new GameObject[] {
                //Body.GetComponent<Object_Status_Network>().Effect_01,
                //Body.GetComponent<Object_Status_Network>().Effect_02,
                Body.GetComponent<Object_Status>().Dead_Effect,
                Body.GetComponent<Object_Status>().Ice_Effect,
                Body.GetComponent<Object_Status>().Stun_Effect,
                Body.GetComponent<Object_Status>().Fire_Effect,
                Body.GetComponent<Object_Status>().Control_Effect,
                Body.GetComponent<Object_Status>().Resurrection_Effect,
                Body.GetComponent<Object_Status>().Heal_Effect_02,
                Body.GetComponent<Object_Status>().Armor_Effect_01
            };
            return Object_Status_Array;
        }

        void if_obj_Active_set_Deactive(GameObject[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    if (array[i].activeSelf)
                        array[i].SetActive(false);
                }
            }
        }
    }

    [TargetRpc]
    void Target_Active_or_Deactive_Object(NetworkConnection Conn, string _obj, float timer, bool Active_or_Deactive)
    {
        GameObject _object = null;
        if (_obj == "Explosion_Effect_01")
            _object = Explosion_Effect_01;
        if (Active_or_Deactive && _object != null)
        {
            _object.SetActive(false);
            StartCoroutine(Deactive_Object(_object, timer, false));
        }
        if (_object != null)
            _object.SetActive(Active_or_Deactive);
    }

    void Server_Prepare_Target_Local_Send_Obj_To_Pool()
    {
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            NetworkConnection Conn = GM.GetComponent<GameMaster>().Get_Player_Connection(i);
            if (Conn != null)
                Target_Local_Send_Obj_To_Pool(Conn);
        }
    }

    [TargetRpc]
    void Target_Local_Send_Obj_To_Pool(NetworkConnection Conn)
    {
        Local_Send_Obj_To_Pool();
    }

    public void Local_Send_Obj_To_Pool()
    {
        in_Pool = true;
        if (Move_Point_Temp != null)
            Destroy(Move_Point_Temp);
        All_Model_Deactive();
        All_Effect_Deactive();

        local_loc_spwan = "Empty";
        local_loc_1 = "Empty";
        local_loc_2 = "Empty";
        local_loc_3 = "Empty";
        local_loc_4 = "Empty";
        local_loc_5 = "Empty";
        local_loc_6 = "Empty";
        local_loc_7 = "Empty";
        local_loc_8 = "Empty";
        local_loc_9 = "Empty";
        Clear_Enemy_All_Status();
        transform.position = new Vector3(20, 0, 0);
        gameObject.GetComponent<Enemy>().enabled = false;
    }

    #endregion
}

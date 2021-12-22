using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Enemy_Backup : NetworkBehaviour
{
    public GameObject GM;
    public bool Original_Enemy;
    int Player_Number;
    public bool Enemy_object, Protector_Object, Attacker_Object, Attacker_To_Enemy_Object;
    int _object_Type = 1;
    public bool Attacking = false;
    float Attacking_Timer = 0;
    public GameObject Attacking_Target_Enemy;
    public float HP;
    float MAX_HP;
    public float Damage;
    public float Original_Speed;
    float temp_speed;
    public int Gold;
    float Speed;
    bool Armor;
    int Armor_Rate;
    public bool Patroling = false; // while protector arrival Patrol Point will change to true , and start accept Protector_Control
    public bool Protector_Clockwise = false;
    public GameObject Path;
    public Transform Move_Point;
    public GameObject Move_Target_Point;
    public GameObject Previous_Point;

    public bool Move = false;
    bool Setup_Finish = false;
    bool Animation_Set = false;
    float Dead_Timer = 0;
    bool Tell_Client_Dead = false;
    bool Dead = false;
    int Frame;
    NetworkConnection Conn;

    //public Vector3 POS;

    [Header("Client_Object")]
    public GameObject Client_Model;
    public GameObject Enemy_01;
    public GameObject Protector_01;
    public GameObject Attacker_01;
    public GameObject HP_Bar;
    public GameObject Damage_Bar;

    public float Distance_from_Last_Point = 0;
    public float Distance_from_Start_Point = 0;
    public Vector3 Last_Way_Point_POS;
    Animator Anim;

    public float Slow_Rate;
    public float Fear_Rate;
    public float Stun_Rate;
    public float Attack_Bouns;
    public int Special_Effect_QTY;
    int Special_Effect_Balance_QTY;
    public float Special_Effect_Time;
    float Special_Effect_Timer;

    public GameObject Test;
    public GameObject Text_Tag;
    public GameObject _Move;
    int Turn_Number = 0;

    [Header("Sub_Bullet")]
    public GameObject ground_Boom;

    [Header("Enemy_Effect")]
    public GameObject Slash_Effect_01_P;
    public GameObject Slash_Effect_02_P, Slash_Effect_03_P, Slash_Effect_01_E, Slash_Effect_02_E, Slash_Effect_03_E;
    public GameObject Impact_Effect_01, Impact_Effect_02, Impact_Effect_03;
    public GameObject Fire_Effect_01, Heal_Effect_01, Poison_Effect_01, Wind_Effect_01, Ice_Effect_01;
    public GameObject Explosion_Effect_01, Fear_Effect_01, Stun_Effect_01, Debuff_Effect_01;

    public bool Ice_01_Start, Fear_01_Start, Stun_01_Start;
    public float Ice_01_Time, Fear_01_Time, Stun_01_Time;
    bool Fire_01_Start, Wind_01_Start, Heal_01_Start, Poison_01_Start, Explosion_Effect_01_Start, Debuff_01_Start;
    bool Fire_01, Wind_01, Heal_01, Poison_01, Ice_01, Explosion_01, Fear_01, Stun_01, Debuff_01;
    float Fire_01_Timer, Wind_01_Timer, Heal_01_Timer, Poison_01_Timer, Ice_01_Timer, Explosion_01_Timer, Fear_01_Timer, Stun_01_Timer, Debuff_01_Timer;
    float Fire_01_Time, Wind_01_Time, Heal_01_Time, Poison_01_Time, Explosion_01_Time, Debuff_01_Time;

    string test_Name;
    public Material Material_Test_Enemy, Material_Test_Protector, Material_Test_Attacker;
    public GameObject Test_Canvas;

    #region Set_Enemy
    public void Set_Enemy(GameObject gamemaster, bool enemy, bool protector, bool attacker, bool attacker_to_enemy, string Enemy_Name_For_Client,
        string Tag, int player_Enemy, float hp, float dmg, float speed, int gold)
    {
        //Debug.Log("enemy || " + enemy + " || protector || " + protector + " || attacker || " + attacker + " || Tag || " + Tag);

        Last_Way_Point_POS = transform.position;
        HP = hp;
        Gold = gold;
        Player_Number = player_Enemy;
        GM = gamemaster;
        gameObject.name = Enemy_Name_For_Client;
        gameObject.tag = Tag;
        Enemy_object = enemy;
        Protector_Object = protector;
        Attacker_Object = attacker;
        Attacker_To_Enemy_Object = attacker_to_enemy;

        if (isServer)
        {
            gameObject.GetComponent<MeshRenderer>().material = Get_Material();
            Material Get_Material()
            {
                Material m = null;
                if (Enemy_object)
                    m = Material_Test_Enemy;
                if (Protector_Object)
                    m = Material_Test_Protector;
                if (Attacker_Object)
                    m = Material_Test_Attacker;
                return m;
            }
        }

        Damage = dmg;
        Original_Speed = speed;
        Speed = Original_Speed;
        temp_speed = Speed;
        Move_Point = Move_Target_Point.transform;
        transform.LookAt(Move_Target_Point.transform);
        Rpc_Set_Enemy_Rotation(transform, Move_Target_Point.transform.position);
        Move = true;
        Rpc_Set_Enemy_Type(enemy, protector, attacker, attacker_to_enemy, player_Enemy, hp, dmg, Move_Point.transform.position, Speed, Tag);
        //Send_Camera_To_Test();
        Vector3 POS_Zero = Vector3.zero;
    }

    [ClientRpc]
    public void Rpc_Set_Enemy_Type(bool enemy, bool protector, bool attacker, bool attacker_to_enemy, int player_number, float hp, float dmg,
        Vector3 target_point_pos, float speed, string tag)
    {
        gameObject.tag = tag;
        Speed = speed;
        GameObject Temp_Object = new GameObject();
        Move_Point = Temp_Object.transform;
        Move_Point.transform.position = target_point_pos;
        Move_Target_Point = Move_Point.gameObject;

        HP = hp;
        Damage = dmg;
        Player_Number = player_number;
        string Enemy_Name_for_Client = null;
        Enemy_object = enemy;
        Protector_Object = protector;
        Attacker_Object = attacker;
        Attacker_To_Enemy_Object = attacker_to_enemy;
        if (enemy)
            Enemy_Name_for_Client = "Enemy";
        if (protector)
            Enemy_Name_for_Client = "Protector";
        if (attacker)
            Enemy_Name_for_Client = "Attacker";

        gameObject.name = Enemy_Name_for_Client;
        Set_Enemy_Type();
        Move = true;
    }

    void Set_Enemy_Type()
    {
        if (!isServer)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Client_Model = Check_Enemy_Type();
            Client_Model.SetActive(true);
            Anim = Client_Model.GetComponent<Animator>();
            Anim.SetTrigger("Walk");
            Setup_Finish = true;
        }

        GameObject Check_Enemy_Type()
        {
            Enemy_01.SetActive(false);
            Protector_01.SetActive(false);
            Attacker_01.SetActive(false);

            GameObject _Object = null;
            if (Enemy_object && !Attacker_To_Enemy_Object)
            {
                _Object = Get_Enemy_Object(_object_Type);
            }
            if (Protector_Object)
            {
                _Object = Get_Protector_Object(_object_Type);
            }
            if (Attacker_Object || Attacker_To_Enemy_Object)
            {
                _Object = Get_Attacker_Object(_object_Type);
            }
            return _Object;
        }

        GameObject Get_Enemy_Object(int number)
        {
            GameObject _Object = null;
            switch (number)
            {
                case (1):
                    _Object = Enemy_01;
                    break;
            }
            return _Object;
        }

        GameObject Get_Protector_Object(int number)
        {
            GameObject _Object = null;
            switch (number)
            {
                case (1):
                    _Object = Protector_01;
                    break;
            }
            return _Object;
        }

        GameObject Get_Attacker_Object(int number)
        {
            GameObject _Object = null;
            switch (number)
            {
                case (1):
                    _Object = Attacker_01;
                    break;
            }
            return _Object;
        }
    }

    #endregion

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

        if (gameObject.tag != "Player01_Protector" && gameObject.tag != "Player02_Protector" &&
            gameObject.tag != "Player03_Protector" && gameObject.tag != "Player04_Protector")
        {
            float Distance = Vector3.Distance(transform.position, Last_Way_Point_POS);
            Distance_from_Start_Point = Distance_from_Last_Point + Distance;
        }
        #region HP || Dead
        if (Dead && isServer)
        {
            Dead_Timer += Time.deltaTime;
            if (Dead_Timer >= 1.2f)
                NetworkServer.Destroy(gameObject);
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
            gameObject.tag = "Dead";
            Dead = true;
            Rpc_Tell_Client_Dead();
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
                    //Debug.Log("Speed || " + Speed + " || temp_speed || " + temp_speed);
                    Rpc_Client_Update_Speed(Speed);
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
                Rpc_Client_Update_Speed(Speed);
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
                Rpc_Set_Run(Move, transform.position, Move_Point.position);
                Rpc_Set_Fear(true);
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
                Rpc_Set_Run(Move, transform.position, Move_Point.position);
                Rpc_Set_Fear(false);
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
                Rpc_Set_Stun(true, Move, transform.position, Move_Point.position);
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
                Rpc_Set_Stun(false, Move, transform.position, Move_Point.position);
            }
        }
        #endregion

        if (isServer && !Dead)
        {
            GameObject _camera = GameObject.FindWithTag("MainCamera");
            Text_Tag.GetComponent<Text>().text = HP.ToString();
            Test_Canvas.transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

        }

        if (Move && isServer && !Dead)
        {
            Update_WayPoint();
            transform.LookAt(Move_Point);
            float Distance_to_Move_Target_Point = Vector3.Distance(transform.position, Move_Point.position);

            if (Distance_to_Move_Target_Point < 0.1f)
            {
                if (Enemy_object)
                    Get_Next_Enemy_Move_Target_Point(Move_Point);

                if (Protector_Object)
                    Get_Next_Protector_Move_Target_Point();

                if (Attacker_Object)
                    GM.GetComponent<GameMaster>().Attacker_Change_To_Enemy(gameObject, Player_Number, HP, Damage, Speed, _object_Type);

                Rpc_Set_Run(Move, transform.position, Move_Point.position);
                //Rpc_Set_Enemy_Rotation(transform, Move_Point.transform.position);
            }
            Vector3 dir = Move_Point.position - transform.position;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            if (Protector_Object || Attacker_Object)
            {
                if (Attacking_Target_Enemy == null)
                {
                    Attacking_Target_Enemy = Get_Protector_Distance_To_Enemy();
                }
                if (Attacking_Target_Enemy != null && !Attacking)
                {
                    Attacking = true;
                    Rpc_Set_Run(Move, transform.position, Move_Point.position);
                }
            }
        }
        if (Attacking_Target_Enemy == null && Attacking && isServer && !Dead)
        {
            Attacking = false;
            Move = true;
            Rpc_Set_Run(Move, transform.position, Move_Point.position);
        }

        if (Attacking_Target_Enemy != null && Attacking && isServer && !Dead)
        {
            if (Attacking_Timer == 0)
                Fight(gameObject, Attacking_Target_Enemy);
            Attacking_Timer += Time.deltaTime;
            if (Attacking_Timer >= 0.1f && !Animation_Set)
            {
                Animation_Set = true;
            }
            if (Attacking_Timer >= 1.1f)
            {
                Animation_Set = false;
                Attacking_Timer = 0;

                //float Final_Damage = Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Damage(Damage, false, 10, 500);
                //float current_HP = Attacking_Target_Enemy.GetComponent<Enemy>().HP -= Final_Damage;
                float current_HP = Attacking_Target_Enemy.GetComponent<Enemy>().HP;
                if (current_HP < 1)
                {
                    Move = true;
                    Attacking = false;
                    Attacking_Target_Enemy = null;
                    Rpc_Set_Enemy_Animation("Walk", "Attack", true);
                    Rpc_Set_Run(Move, transform.position, Move_Point.position);
                }
            }
        }

        if (!isServer && Setup_Finish)
        {
            //transform.position = POS;
            string Name = null;
            string Name2 = null;
            bool Wait = Anim.GetCurrentAnimatorStateInfo(0).IsName("Wait");
            bool Attack = Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
            bool Walk = Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk");
            if (Wait)
                Name = "Wait";
            if (Attack)
                Name = "Attack";
            if (Walk)
                Name = "Walk";
            if (Move)
                Name2 = "Move";
            if (!Move)
                Name2 = "Not_Move";

            Text_Tag.GetComponent<Text>().text = HP.ToString();
            if (Wait && Move)
                Client_Model.GetComponent<Animator>().SetTrigger("Walk");

            _Move.GetComponent<Text>().text = Damage.ToString();

            if (Move && !Dead)
            {
                transform.LookAt(Move_Point);
                Vector3 dir = Move_Point.position - transform.position;
                transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            }

            // bool Fire_01_Start, Wind_01_Start, Heal_01_Start, Poison_01_Start;
            #region Fire_01_Effect
            if (Fire_01_Start)
            {
                if (Fire_01)
                    Reset_Effect("Fire_Effect_01", Fire_Effect_01);
                if (!Fire_01)
                    Fire_01 = true;
                Fire_01_Start = false;
            }
            if (Fire_01)
            {
                Fire_Effect_01.SetActive(true);
                Fire_01_Timer += Time.deltaTime;
                if (Fire_01_Timer > Fire_01_Time)
                {
                    Fire_01_Timer = 0;
                    Fire_Effect_01.SetActive(false);
                    Fire_01 = false;
                }
            }
            #endregion

            #region Wind_01_Effect
            if (Wind_01_Start)
            {
                if (Wind_01)
                    Reset_Effect("Wind_Effect_01", Wind_Effect_01);
                if (!Wind_01)
                    Wind_01 = true;
                Wind_01_Start = false;
            }
            if (Wind_01)
            {
                Wind_Effect_01.SetActive(true);
                Wind_01_Timer += Time.deltaTime;
                if (Wind_01_Timer > Wind_01_Time)
                {
                    Wind_01_Timer = 0;
                    Wind_Effect_01.SetActive(false);
                    Wind_01 = false;
                }
            }
            #endregion

            #region Heal_01_Effect
            if (Heal_01_Start)
            {
                if (Heal_01)
                    Reset_Effect("Heal_Effect_01", Heal_Effect_01);
                if (!Heal_01)
                    Heal_01 = true;
                Heal_01_Start = false;
            }
            if (Heal_01)
            {
                Heal_Effect_01.SetActive(true);
                Heal_01_Timer += Time.deltaTime;
                if (Heal_01_Timer > Heal_01_Time)
                {
                    Heal_01_Timer = 0;
                    Heal_Effect_01.SetActive(false);
                    Heal_01 = false;
                }
            }
            #endregion

            #region Poison_01_Effect
            if (Poison_01_Start)
            {
                if (Poison_01)
                    Reset_Effect("Poison_Effect_01", Poison_Effect_01);
                if (!Poison_01)
                    Poison_01 = true;
                Poison_01_Start = false;
            }
            if (Poison_01)
            {
                Poison_Effect_01.SetActive(true);
                Poison_01_Timer += Time.deltaTime;
                if (Poison_01_Timer > Poison_01_Time)
                {
                    Poison_01_Timer = 0;
                    Poison_Effect_01.SetActive(false);
                    Poison_01 = false;
                }
            }
            #endregion

            #region Ice_01_Effect
            if (Ice_01_Start)
            {
                if (Ice_01)
                    Reset_Effect("Ice_Effect_01", Ice_Effect_01);
                if (!Ice_01)
                    Ice_01 = true;
                Ice_01_Start = false;
            }
            if (Ice_01)
            {
                Ice_Effect_01.SetActive(true);
                Ice_01_Timer += Time.deltaTime;
                if (Ice_01_Timer > Ice_01_Time)
                {
                    Ice_01_Timer = 0;
                    Ice_Effect_01.SetActive(false);
                    Ice_01 = false;
                }
            }
            #endregion

            #region Fear_01_Effect
            if (Fear_01)
                Debug.Log("Fear_01 || " + Fear_01);
            Fear_Effect_01.SetActive(true);

            if (!Fear_01)
                Fear_Effect_01.SetActive(false);
            #endregion

            #region Stun_01_Effect
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
                if (Debuff_01)
                    Reset_Effect("Debuff_Effect_01", Debuff_Effect_01);
                if (!Debuff_01)
                    Debuff_01 = true;
                Debuff_01_Start = false;
            }

            if (!Debuff_01)
            {
                Debuff_Effect_01.SetActive(true);
                Debuff_01_Timer += Time.deltaTime;
                if (Debuff_01_Timer > Debuff_01_Time)
                {
                    Debuff_01_Timer = 0;
                    Debuff_Effect_01.SetActive(false);
                    Debuff_01 = false;
                }
            }
            #endregion

            if (Dead)
            {
                Dead_Timer += Time.deltaTime;
                if (Dead_Timer < 1.0f)
                    Dead_Change_Material();
                Reset_All_Effect();
            }
        }
    }

    void Update_WayPoint()
    {
        string Object_Tag = gameObject.tag;
        if (Object_Tag == "Enemy_01" || Object_Tag == "Enemy_02" || Object_Tag == "Enemy_03" || Object_Tag == "Enemy_04")
        {
            if (Move_Point.GetComponent<Object_Status>().Start_Point)
                return;
            if (!Fear_01)
                Move_Point = Move_Target_Point.transform;

            if (Fear_01)
                Move_Point = Previous_Point.transform;
        }
    }

    [ClientRpc]
    void Rpc_Client_Update_Speed(float spd)
    {
        Speed = spd;
    }

    [ClientRpc]
    void Rpc_Tell_Client_Dead()
    {
        //Debug.Log("Rpc_Tell_Client_Dead || " + Dead);
        Dead = true;
        Anim.enabled = false;
        Destroy(Move_Point.gameObject);
    }

    void Dead_Change_Material()
    {
        // Debug.Log("Rpc_Dead_Change_Material ");

        Anim.enabled = false;
        GameObject Dead_Effect = Client_Model.GetComponent<Object_Status>().Dead_Effect;
        Dead_Effect.SetActive(true);

        //Material Dead_Material = Client_Model.GetComponent<Object_Status>().Dead_Material;
        GameObject Body = Client_Model.GetComponent<Object_Status>().Body;

        //Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Dead_Material;
        if (Frame == 0)
            All_Effect_Deactive();
        if (Frame <= 30)
            Animate_Texture();
        Frame++;

        void Animate_Texture()
        {
            int index = (int)(Time.time * 30);
            index = index % (3 * 3);

            var size = new Vector2(1.0f / 3, 1.0f / 3);

            var uIndex = index % 3;
            var vIndex = index / 3;

            // build offset
            // v coordinate is the bottom of the image in opengl so we need to invert.
            var offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

            Body.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureOffset("_MainTex", offset);
            Body.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureScale("_MainTex", size);
        }
    }

    void Fight(GameObject _Object_01, GameObject _Object_02)
    {
        // _Object_01 = Protector or Attacker , _Object_02 = Enemy
        Vector3 Object_01_POS = _Object_01.transform.position;
        Vector3 Object_02_POS = _Object_02.transform.position;

        _Object_01.transform.LookAt(_Object_02.transform);
        Rpc_Set_Enemy_Rotation(_Object_01.transform, Object_02_POS);

        _Object_02.transform.LookAt(_Object_01.transform);
        _Object_02.GetComponent<Enemy>().Server_Prepare_Target_Set_Enemy_Rotation(_Object_02.transform, Object_01_POS);

        _Object_01.GetComponent<Enemy>().Move = false;
        _Object_02.GetComponent<Enemy>().Move = false;
        _Object_01.GetComponent<Enemy>().Attacking = true;
        _Object_02.GetComponent<Enemy>().Attacking = true;
        _Object_02.GetComponent<Enemy>().Attacking_Target_Enemy = _Object_01;

        short code = (short)Enemy_Code.Attack;

        //GameObject Enemy, float dmg, bool Heal_or_Damage, int C_Rate, int C_Damage, float time
        GM.GetComponent<GameMaster>().GM_Control_Enemy_Damage(gameObject, Damage, code, 10, 500, 1.0f, "null", "null");

        float dmg = Attacking_Target_Enemy.GetComponent<Enemy>().Damage;
        GM.GetComponent<GameMaster>().GM_Control_Enemy_Damage(Attacking_Target_Enemy, dmg, code, 10, 500, 1.0f, "null", "null");

        Rpc_Set_Enemy_Animation("Attack", "Walk", false);
    }

    [ClientRpc]
    void Rpc_Set_HP(GameObject enemy, float hp, float dmg)
    {
        if (enemy == null)
            return;
        enemy.GetComponent<Enemy>().Damage = dmg;
        enemy.GetComponent<Enemy>().HP = hp;
    }

    public void Reset_Run()
    {
        Vector3 target_point_pos = Move_Point.transform.position;
        Rpc_Set_Run(Move, transform.position, target_point_pos);
    }

    [ClientRpc]
    void Rpc_Set_Run(bool move, Vector3 POS, Vector3 target_point_pos)
    {
        Move = move;
        if (!Dead)
            transform.position = POS;
        if (Move_Point != null)
            Move_Point.transform.position = target_point_pos;
        transform.LookAt(Move_Point);
    }

    [ClientRpc]
    void Rpc_Set_Stun(bool stun, bool move, Vector3 POS, Vector3 target_point_pos)
    {
        Stun_01 = stun;
        Move = move;
        if (!Dead)
            transform.position = POS;
        if (Move_Point != null)
            Move_Point.transform.position = target_point_pos;
    }

    [ClientRpc]
    void Rpc_Set_Fear(bool fear)
    {
        Fear_01 = fear;
    }

    [ClientRpc]
    public void Rpc_Set_Enemy_Animation(string Set_Animation, string Reset_Animation, bool move)
    {
        //if (Set_Animation == "Attack")
        //    Set_Enemy_Effect(1, 0.5f);

        Move = move;

        if (Set_Animation != null)
        {
            Client_Model.GetComponent<Animator>().SetTrigger(Set_Animation);
        }
        if (Reset_Animation != null)
        {
            Client_Model.GetComponent<Animator>().ResetTrigger(Reset_Animation);
        }
    }

    [ClientRpc]
    public void Rpc_Set_Enemy_Rotation(Transform m_Transform, Vector3 Look_This_POS)
    {
        m_Transform.LookAt(Look_This_POS);
    }

    GameObject Get_Protector_Distance_To_Enemy()
    {
        string Enemy_Tag = null;
        string Object_Tag = gameObject.tag;
        if (Object_Tag == "Enemy_01" || Object_Tag == "Enemy_02" || Object_Tag == "Enemy_03" || Object_Tag == "Enemy_04")
            return null;

        if (Object_Tag == "Player01_Protector" || Object_Tag == "Player01_Attacker")
            Enemy_Tag = "Enemy_01";
        if (Object_Tag == "Player02_Protector" || Object_Tag == "Player02_Attacker")
            Enemy_Tag = "Enemy_02";

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy_Tag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= 0.5f)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    #region Waypoint
    void Get_Next_Enemy_Move_Target_Point(Transform current_Point)
    {
        bool Start_Point = current_Point.GetComponent<Object_Status>().Start_Point;
        bool End_Point = current_Point.GetComponent<Object_Status>().End_Point;

        if (!End_Point && !Fear_01)
        {
            Last_Way_Point_POS = Move_Point.transform.position;
            Distance_from_Last_Point = Move_Point.GetComponent<Object_Mount>().Distance_from_Start_Point;
        }

        if (End_Point)
        {
            Rpc_Tell_Client_Dead();
            NetworkServer.Destroy(gameObject);
            return;
        }

        if (!End_Point)
        {
            if (Fear_01)
            {
                Previous_Point = current_Point.gameObject.GetComponent<Object_Status>().Previous_WayPoint;
                Move_Target_Point = current_Point.gameObject;
            }
            if (!Fear_01)
            {
                Previous_Point = current_Point.gameObject;
                Move_Target_Point = current_Point.gameObject.GetComponent<Object_Status>().Next_WayPoint;
            }
        }

        if (Start_Point)
        {
            All_Effect_Deactive();
            GM.GetComponent<GameMaster>().Attacker_Change_To_Enemy(gameObject, Player_Number, HP, Damage, Speed, _object_Type);
            if (Fear_01)
                Fear_01 = false;
        }

        if (!Start_Point)
            Update_WayPoint();
    }

    void Get_Next_Protector_Move_Target_Point()
    {
        Turn_Number++;
        string Name = Random.Range(100, 1111).ToString();
        //Debug.Log("Name || 1 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
        bool Check_Patrol_Start_Point = false;
        string Move_Target_Point_Tag = Move_Point.tag;
        if (Move_Target_Point_Tag == "Patrol_Start_Point")
        {
            Patroling = true;
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
            Previous_Point = Move_Point.gameObject;
            Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Next_Point.transform;
            Move_Target_Point = Move_Point.gameObject;
            //Debug.Log("Name || 2 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
        }
        if (!Protector_Clockwise)
        {
            Previous_Point = Move_Point.gameObject;
            Move_Point = Move_Point.GetComponent<Object_Mount>().Obj_1V1_Protector_Previous_Point.transform;
            Move_Target_Point = Move_Point.gameObject;
            //Debug.Log("Name || 2 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
        }
        Move_Point = Move_Target_Point.transform;
        //Debug.Log("Name || 3 || Move_Point || " + Move_Point + " || Move_Target_Point || " + Move_Target_Point);
    }
    #endregion

    public void Spwan_Sub_Bullet(int Tower_Type, float damage, int critical_Rate, int critical_Damage)
    {
        Vector3 POS = Vector3.zero;
        GameObject Sub_Bullet = null;
        float Random_X = Random.Range(0, 0.21f);
        float Random_Z = Random.Range(0, 0.21f);
        if (Tower_Type == 204)
        {
            POS = transform.position + new Vector3(Random_X, 0.01f, Random_Z);
            Sub_Bullet = ground_Boom;
            GameObject Ground_Boom = Instantiate(Sub_Bullet, POS, Quaternion.identity);
            //Ground_Boom.GetComponent<Object_Status>().Set_Ground_Boom(Damage, gameObject.tag, critical_Rate, critical_Damage);
            NetworkServer.Spawn(Ground_Boom);
            Sub_Bullet = Ground_Boom;
        }
        //Rpc_Spawn_Sub_Bullet(Sub_Bullet, damage);
    }

    //[ClientRpc]
    //void Rpc_Spawn_Sub_Bullet(GameObject sub_bullet, float damage)
    //{
    //    sub_bullet.GetComponent<Object_Status>().Effect_01.SetActive(true);
    //    sub_bullet.GetComponent<Object_Status>().Damage = damage;
    //}

    void Send_Camera_To_Test()
    {
        GameObject Player = Get_Player(Player_Number);
        RpcSend_Camera_To_Test(Player);
    }

    [ClientRpc]
    void RpcSend_Camera_To_Test(GameObject Player)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        Local_Manager lm = m_local_Manager.GetComponent<Local_Manager>();
        GameObject camera = lm.Get_Camera((short)Player_Number);
        Transform tran = gameObject.transform;
        GameObject Test_Bar = tran.Find("Test_Bar").gameObject;
        Test_Bar.GetComponent<Object_Status>().Set_Face_Camera_Test(camera);
    }

    #region Effect
    public void Set_Enemy_Effect(int Tower_Type, float Time)
    {
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
        }
    }

    GameObject Get_Effect(int Type_Number)
    {
        GameObject Effect = null;
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
            case (101):
            case (204):
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
                Effect = Ice_Effect_01;
                break;
            case (206):
                Effect = Fear_Effect_01;
                break;
            case (301):
                Effect = Stun_Effect_01;
                break;
            case (302):
                Effect = Debuff_Effect_01;
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

    void All_Effect_Deactive()
    {
        if (isServer)
            return;
        Impact_Effect_01.SetActive(false);
        Slash_Effect_01_P.SetActive(false);
        Slash_Effect_01_E.SetActive(false);
        Fire_Effect_01.SetActive(false);
        Wind_Effect_01.SetActive(false);
        Heal_Effect_01.SetActive(false);
        Poison_Effect_01.SetActive(false);
        Ice_Effect_01.SetActive(false);
        Fear_Effect_01.SetActive(false);
        Stun_Effect_01.SetActive(false);
        Debuff_Effect_01.SetActive(false);
    }

    #endregion

    public void Enemy_Animation(string Name, bool Active_or_Deactive, bool Self_or_Target)
    {
        //    Debug.Log("Enemy_Animation || Name || " + Name + " || Active_or_Deactive || "
        //        + Active_or_Deactive + " || Self_or_Target || " + Self_or_Target + " || " + gameObject.name);

        switch (Name)
        {
            case ("Impact"):
                if (Self_or_Target)
                {
                    GameObject Impact_Effect = Get_Effect(0);
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
                    Slash_Effect.SetActive(!Active_or_Deactive);
                    Slash_Effect.SetActive(Active_or_Deactive);
                }
                if (!Self_or_Target && Attacking_Target_Enemy != null)
                {
                    Attacking_Target_Enemy.GetComponent<Enemy>().Enemy_Animation(Name, Active_or_Deactive, true);
                }
                break;

        }
    }

    public void Active_Or_Deactive_Object(string name, float timer, bool Active_or_Deactive)
    {
        Rpc_Active_or_Deactive_Object(name, timer, Active_or_Deactive);
    }

    [ClientRpc]
    void Rpc_Active_or_Deactive_Object(string _obj, float timer, bool Active_or_Deactive)
    {
        GameObject _object = null;

        if (_obj == "Explosion_Effect_01")
            _object = Explosion_Effect_01;
        if (Active_or_Deactive)
        {
            _object.SetActive(false);
            StartCoroutine(Deactive_Object(_object, timer, false));
        }
        _object.SetActive(Active_or_Deactive);
    }

    IEnumerator Deactive_Object(GameObject obj, float Time, bool Active_or_Deactive)
    {
        yield return new WaitForSeconds(Time);
        obj.SetActive(Active_or_Deactive);
    }

    void Reset_All_Effect()
    {
        Fire_Effect_01.SetActive(false);
        Wind_Effect_01.SetActive(false);
        Heal_Effect_01.SetActive(false);
        Poison_Effect_01.SetActive(false);
        Ice_Effect_01.SetActive(false);
    }

    #region Fear

    public void Enemy_Set_Fear()
    {
        Fear_01 = true; // for Test
        int Fear_Time_Test = Random.Range(1, 11);
        Fear_01_Time = Fear_Time_Test;
        Update_WayPoint();
        Rpc_Set_Run(Move, transform.position, Move_Point.position);
    }

    #endregion

    #region Damage and HP Bar

    public void Set_Armor_Rate(bool Buff_or_Debuff, float Amount)
    {
        if (Buff_or_Debuff)
            Armor_Rate += (int)Amount;
        if (!Buff_or_Debuff)
            Armor_Rate -= (int)Amount;
        if (Armor_Rate >= 75)
            Armor_Rate = 75;
        if (Armor_Rate <= 0)
            Armor_Rate = 0;
    }

    public float Enemy_Damage(float dmg, bool Heal_or_Damage, int Critical_Rate, int Critical_Damage)
    {
        bool Critical_Attack = false;
        float Base_DMG = 0;
        int Random_Number = Random.Range(1, 101);
        if (Random_Number < Critical_Rate)
            Critical_Attack = true;

        float Original_HP = HP;
        Vector3 POS = transform.position + new Vector3(0, 1.5f, 0);
        float Final_Damage = dmg;
        if (!Heal_or_Damage)
        {
            Base_DMG = (dmg / 100) * (100 - Armor_Rate);

            if (!Critical_Attack)
                Final_Damage = Base_DMG;

            if (Critical_Attack)
                Final_Damage = Base_DMG * (Critical_Damage / 100);

            HP -= Final_Damage;
        }

        if (Heal_or_Damage)
            HP += Final_Damage;

        if (HP >= 1)
        {
            Instantiate_HP_Bar(true, true, POS, Final_Damage, Original_HP, Heal_or_Damage, Critical_Attack); // True = heal , false = Damage
        }

        if (HP < 1)
            Instantiate_HP_Bar(false, true, POS, Final_Damage, Original_HP, Heal_or_Damage, Critical_Attack); // True = heal , false = Damage
        return Final_Damage;
    }

    void Instantiate_HP_Bar(bool HP_Bar, bool Damage_Bar, Vector3 POS, float damage, float Original_HP, bool Damage_or_Heal, bool Critical)
    {
        float after_HP = Original_HP - damage;
        GameObject Player = Get_Player(Player_Number);
        RpcSend_HP_Tag_To_All_Client(HP_Bar, Damage_Bar, POS, damage, Original_HP, after_HP, Player, Damage_or_Heal, Critical);
    }

    [ClientRpc]
    void RpcSend_HP_Tag_To_All_Client(bool _hp_bar, bool _damage_bar, Vector3 POS, float Damage, float Original_HP, float after_HP,
        GameObject Player, bool Damage_or_Heal, bool Critical)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        Local_Manager lm = m_local_Manager.GetComponent<Local_Manager>();
        GameObject camera = lm.Get_Camera((short)Player_Number);

        if (_hp_bar)
        {
            GameObject hp_bar = Instantiate(HP_Bar, POS, Quaternion.identity);
            //bool heal_or_damage, float Damage, float hp , float hp_after_action, GameObject Camera)
            //hp_bar.GetComponent<Object_Status>().Set_HP_Bar(Damage_or_Heal, Damage, Original_HP, after_HP, camera);
        }

        if (_damage_bar)
        {
            GameObject damage_bar = Instantiate(Damage_Bar, POS, Quaternion.identity);
            //bool heal_or_damage, bool Critical , float Damage, GameObject Camera
            //damage_bar.GetComponent<Object_Status>().Set_Damage_Bar(Damage_or_Heal, Critical, Damage, camera);
        }
    }
    #endregion
}

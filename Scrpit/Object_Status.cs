using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Object_Status : MonoBehaviour // MonoBehaviour // NetworkBehaviour
{
    GameObject Tower_Controller;

    public float Timer;
    Vector3 POS;
    GameObject _camera;

    #region Bullet
    bool Bullet;
    bool Ground_Bullet;
    public bool Chain_Weapon_Point, Local_Chain_Weapon_Point, Enemy_Transfer;
    public float Damage, value_X = 0.5f, value_Y = 0.5f;
    float Bullet_Speed;
    Vector3 Bullet_Target_POS;
    int Critical_Rate;
    int Critical_Damage;
    float Speed;

    public Material Dead_Material_White, Dead_Material_Black, Dead_Material_Red, Transfer_Material, Original_Material, Resurrect_Material;
    public GameObject Body;
    public GameObject Horse;
    public GameObject Dead_Effect, Ice_Effect, Stun_Effect, Fire_Effect, Control_Effect, Resurrection_Effect, Heal_Effect_02, Armor_Effect_01;

    public GameObject UI_Treasure_Box, Obj_1, Obj_2, Obj_3, Obj_4;

    string Tag;

    #endregion

    #region HP_Bar Damage_Bar
    bool HP_Bar;
    bool Damage_Bar;
    bool Heal_Or_Damage;
    public GameObject HP_Mask;
    public GameObject HP_Action;
    public GameObject HP_Balance;
    public GameObject HP_Border;
    public GameObject HP_Text;
    float HP_Current;
    float HP_After_Action;
    public Color Damage_HP;
    public Color Damage_Value;
    public Color Heal_HP;
    public Color Heal_Value;
    public Color Heal_Text_Color;
    public Color Damage_Text_Color;
    public Color Critical_Damage_Text_Color;

    bool Gold_Bar;
    int Gold_QTY, QTY;
    #endregion

    #region Way_Point
    public bool Start_Point;
    public bool End_Point;
    public GameObject Previous_WayPoint;
    public GameObject Next_WayPoint;
    #endregion

    public GameObject Core;
    public GameObject Core_HP_Text;

    public GameObject[] Tower_Slot_Array = new GameObject[25];
    public GameObject Tower_Slot_01, Tower_Slot_02, Tower_Slot_03, Tower_Slot_04, Tower_Slot_05, Tower_Slot_06;
    public GameObject Tower_Slot_07, Tower_Slot_08, Tower_Slot_09, Tower_Slot_10, Tower_Slot_11, Tower_Slot_12;
    public GameObject Tower_Slot_13, Tower_Slot_14, Tower_Slot_15, Tower_Slot_16, Tower_Slot_17, Tower_Slot_18;
    public GameObject Tower_Slot_19, Tower_Slot_20, Tower_Slot_21, Tower_Slot_22, Tower_Slot_23, Tower_Slot_24;

    public int[] Group1, Group2, Group3, Group4, Group5, Group6, Group7, Group8, Group9;

    public bool Face_Camera;

    #region Treasure
    public bool Tag_Face_To_Camera, Treasure_Bar;
    public GameObject Treasure_Bar_Obj, Treasure_Gold, Treasure_Diamond, Treasure_Token, Text_QTY_Tag;
    public GameObject HP_Canvas_Text;
    public int Type; // 1 = Gold , 2 = Diamond , 3 = Token;
    #endregion

    public void Set_Face_Camera_Test(GameObject Camera)
    {
        _camera = Camera;
        Face_Camera = true;
    }

    void Get_Main_Camera()
    {
        _camera = GameObject.FindWithTag("MainCamera");
    }

    public void Set_Bullet(bool bullet, float bullet_Speed, Vector3 bullet_target_POS)
    {
        Bullet_Speed = bullet_Speed;
        Bullet_Target_POS = bullet_target_POS;
        Bullet = bullet;
    }

    public void Set_Chain_Weapon_Point(float Dmg, string tag, int critical_Rate, int critical_Damage, float time, GameObject t_Controller)
    {
        Tower_Controller = t_Controller;
        Critical_Rate = critical_Rate;
        Critical_Damage = critical_Damage;
        Damage = Dmg;
        Tag = tag;
        Chain_Weapon_Point = true;
        Timer = time;
    }

    public void Set_Gold_Bar(int Gold, GameObject Camera)
    {
        GameObject HP_Directory = GameObject.Find("HP_Bar");
        gameObject.transform.SetParent(HP_Directory.transform);
        _camera = Camera;
        Gold_QTY = Gold;
        Speed = 2;
        POS = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
        int Width = 40;
        if (Damage < 1000) { Width = 40; }
        if (Damage >= 1000 && Damage <= 99999) { Width += 10; }
        if (Damage >= 100000 && Damage <= 9999999) { Width += 20; }
        if (Damage >= 10000000 && Damage <= 999999999) { Width += 30; }
        if (Damage >= 1000000000) { Width += 40; }

        HP_Text.GetComponent<Text>().text = "$ " + Gold_QTY.ToString();
        Gold_Bar = true;
    }

    public void Set_Damage_Bar(short code, float Damage, GameObject Camera)
    {
        GameObject HP_Directory = GameObject.Find("HP_Bar");
        gameObject.transform.SetParent(HP_Directory.transform);
        _camera = Camera;
        Damage = (int)Damage;

        float Height = 0;
        if (code == (short)Enemy_Code.Critical_Attack)
        {
            Height = 3.0f;
            Speed = 2.0f;
        }

        if (code == (short)Enemy_Code.Normal_Attack)
        {
            Height = 1.0f;
            Speed = 1.0f;
        }

        POS = new Vector3(transform.position.x, transform.position.y + Height, transform.position.z);
        int Width = 40;
        if (Damage < 1000) { Width = 40; }
        if (Damage >= 1000 && Damage <= 99999) { Width += 10; }
        if (Damage >= 100000 && Damage <= 9999999) { Width += 20; }
        if (Damage >= 10000000 && Damage <= 999999999) { Width += 30; }
        if (Damage >= 1000000000) { Width += 40; }
        HP_Text.GetComponent<Text>().text = Damage.ToString();

        if (code == (short)Enemy_Code.Heal)
            HP_Text.GetComponent<Text>().color = Heal_Text_Color;
        if (code == (short)Enemy_Code.Normal_Attack)
            HP_Text.GetComponent<Text>().color = Damage_Text_Color;
        if (code == (short)Enemy_Code.Critical_Attack)
            HP_Text.GetComponent<Text>().color = Critical_Damage_Text_Color;
        Damage_Bar = true;
    }

    public void Set_HP_Bar(short Code, float Damage, float hp_current, float hp_after_action, GameObject Camera)
    {
        if (hp_current <= 0)
            Destroy(gameObject);

        GameObject HP_Directory = GameObject.Find("HP_Bar");
        if (HP_Directory != null)
            gameObject.transform.SetParent(HP_Directory.transform);

        _camera = Camera;
        POS = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        if (Code == (short)Enemy_Code.Heal)
            Heal_Or_Damage = true;

        if (Code == (short)Enemy_Code.Attack)
            Heal_Or_Damage = false;


        HP_Current = hp_current;
        HP_After_Action = hp_after_action;

        Set_HP_Bar();
        HP_Bar = true;

        void Set_HP_Bar()
        {
            int Width = 40;
            if (HP_After_Action < 1000) { Width = 40; }
            if (HP_After_Action >= 1000 && HP_After_Action <= 99999) { Width += 10; }
            if (HP_After_Action >= 100000 && HP_After_Action <= 9999999) { Width += 20; }
            if (HP_After_Action >= 10000000 && HP_After_Action <= 999999999) { Width += 30; }
            if (HP_After_Action >= 1000000000) { Width += 40; }

            float before_Action_HP = HP_Current;
            float after_Action_HP = HP_After_Action;

            Image hp_Value = HP_Action.GetComponent<Image>();
            Image hp = HP_Balance.GetComponent<Image>();
            int HP = (int)HP_After_Action;
            if (HP < 0)
                HP = 0;
            HP_Text.GetComponent<Text>().text = HP.ToString();

            if (Heal_Or_Damage)
            {
                Width += 20;
                hp_Value.color = Heal_Value;
                hp.color = Heal_HP;
                Character_or_Enemy_Heal();
            }

            if (!Heal_Or_Damage)
            {
                Width += 10;
                hp_Value.color = Damage_HP;
                hp.color = Damage_Value;
                Character_or_Enemy_Damage();
            }

            if (Heal_Or_Damage)
            {
                HP_Text.GetComponent<Text>().text = HP.ToString();
            }
            HP_Text.GetComponent<Text>().fontSize = 18;

            Set_Width(Width);

            void Character_or_Enemy_Heal()
            {
                float max_HP = HP_After_Action;
                hp_Value.fillAmount = 1;
                hp.fillAmount = before_Action_HP / max_HP;
            }

            void Character_or_Enemy_Damage()
            {
                hp_Value.fillAmount = 1;
                if (after_Action_HP > 0)
                {
                    hp.fillAmount = after_Action_HP / before_Action_HP;
                }
                if (after_Action_HP < 0)
                {
                    hp.fillAmount = 0;
                }
            }

            void Set_Width(int width)
            {
                int height = 23;

                HP_Mask.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                HP_Action.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                HP_Balance.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                HP_Border.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                HP_Text.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            }
        }
    }

    void Update()
    {
        if (Bullet)
        {
            Vector3 dir = Bullet_Target_POS - transform.position;
            transform.Translate(dir.normalized * Bullet_Speed * Time.deltaTime, Space.World);
        }

        if (HP_Bar || Damage_Bar || Gold_Bar || Treasure_Bar)
        {
            if (HP_Bar)
                Speed = 0.5f;
            if (_camera == null)
                //Debug.Log("GameObject || " + gameObject.name + " || _camera || " + _camera + " || " + HP_Bar + " || " + Damage_Bar + " || " + Gold_Bar);

                if (_camera == null)
                    return;

            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

            Timer += Time.deltaTime;
            Vector3 dir = POS - transform.position;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            float Distance = Vector3.Distance(transform.position, POS);
            float bar_Time = 1;
            if (Treasure_Bar)
                bar_Time = 2;

            if (Timer >= bar_Time || Distance <= 0.3f)
            {
                Timer = 0;
                GameObject m_local_Manager = GameObject.Find("Local_Manager");
                if (HP_Bar)
                    m_local_Manager.GetComponent<Local_Manager>().Add_Obj_To_Pool_By_Time(gameObject, 0.0f, "HP_Bar");

                if (Damage_Bar)
                    m_local_Manager.GetComponent<Local_Manager>().Add_Obj_To_Pool_By_Time(gameObject, 0.0f, "Damage_Bar");

                if (!HP_Bar && !Damage_Bar)
                    Destroy(gameObject);
            }
        }

        if (Face_Camera)
        {
            if (_camera == null)
                Get_Main_Camera();

            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }

        if (Tag_Face_To_Camera)
        {
            if (_camera == null)
                Get_Main_Camera();

            Text_QTY_Tag.transform.LookAt(Text_QTY_Tag.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }

        if (Chain_Weapon_Point)
        {
            Transform Target = Chain_Weapon_Point_Check_Enemy();
            if (Target != null)
            {
                if (!Target.GetComponent<Enemy>().Chain_01_Start && !Target.GetComponent<Enemy>().Chain_01)
                {
                    if (!Local_Chain_Weapon_Point)
                    {
                        short code = (short)Enemy_Code.Attack;
                        Target.GetComponent<Enemy>().Enemy_Damage(Damage, code, Critical_Rate, Critical_Damage, Target.name, "Chain");
                        Tower_Controller.GetComponent<Tower_Controller>().Set_Tower_Counter(1, null);
                    }

                    Target.GetComponent<Enemy>().Chain_01_Start = true;
                    Target.GetComponent<Enemy>().Chain_01_Time = Timer;
                }
            }
        }

        if (Enemy_Transfer)
        {
            int uvAnimationTileX = 3;
            int uvAnimationTileY = 3;
            float framesPerSecond = 30;
            Animate_Texture(uvAnimationTileX, uvAnimationTileY, framesPerSecond);

            Timer += Time.deltaTime;
            if (Timer >= 1)
            {
                Timer = 0;
                Enemy_Transfer = false;
                Body.GetComponentInChildren<SkinnedMeshRenderer>().material = Original_Material;
                GameObject Main = transform.parent.gameObject;
                Main.GetComponent<Enemy>().Pause = false;
            }
        }
    }

    void Animate_Texture(int uvAnimationTileX, int uvAnimationTileY, float framesPerSecond)
    {
        // Calculate index
        int index = (int)(Time.time * framesPerSecond);
        // repeat when exhausting all frames
        index = index % (uvAnimationTileX * uvAnimationTileY);

        // Size of every tile
        var size = new Vector2(1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);

        // split into horizontal and vertical index
        var uIndex = index % uvAnimationTileX;
        var vIndex = index / uvAnimationTileX;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        var offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

        Body.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        Body.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
    }

    Transform Chain_Weapon_Point_Check_Enemy()
    {
        Transform nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag);
        float distanceToEnemy = 999;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= 0.2f)
            {
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }

    //string Name, bool Active_or_Deactive, bool Self_or_Target
    public void Active_Animation_Self(string name)
    {
        GameObject Main = transform.parent.gameObject;
        Main.GetComponent<Enemy>().Enemy_Animation(name, true, true);
    }

    public void Deactive_Animation_Self(string name)
    {
        GameObject Main = transform.parent.gameObject;
        Main.GetComponent<Enemy>().Enemy_Animation(name, false, true);
    }

    public void Active_Target_Animation(string name)
    {
        GameObject Main = transform.parent.gameObject;
        Main.GetComponent<Enemy>().Enemy_Animation(name, true, false);
    }

    public void Deactive_Target_Animation(string name)
    {
        GameObject Main = transform.parent.gameObject;
        Main.GetComponent<Enemy>().Enemy_Animation(name, false, false);
    }

    public GameObject[] Get_Tower_Slot_Array()
    {
        return new GameObject[] { null,Tower_Slot_01 , Tower_Slot_02, Tower_Slot_03,Tower_Slot_04,Tower_Slot_05,Tower_Slot_06,
        Tower_Slot_07,Tower_Slot_08,Tower_Slot_09,Tower_Slot_10,Tower_Slot_11,Tower_Slot_12,Tower_Slot_13,Tower_Slot_14,Tower_Slot_15,
        Tower_Slot_16,Tower_Slot_17,Tower_Slot_18,Tower_Slot_19,Tower_Slot_20,Tower_Slot_21,Tower_Slot_22,Tower_Slot_23,Tower_Slot_24};
    }

    public void Set_Group_Array()
    {
        Tower_Slot_Array = Get_Tower_Slot_Array();
        Group1 = Group_Array(1);
        Group2 = Group_Array(2);
        Group3 = Group_Array(3);
        Group4 = Group_Array(4);
        Group5 = Group_Array(5);
        Group6 = Group_Array(6);
        Group7 = Group_Array(7);
        Group8 = Group_Array(8);
        Group9 = Group_Array(9);

        int[] Group_Array(int Group_Number)
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

    public void Set_Treasure_Box(int type, int qty) // 1 = Gold , 2 = Diamond , 3 = Token;)
    {
        Type = type;
        QTY = qty;
        int Random_Number = Random.Range(10, 1001);
        gameObject.name = "Treasure_Box || " + Random_Number;

        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        _camera = m_local_Manager.GetComponent<Local_Manager>().Get_Camera();
        float POS_Z = _camera.transform.position.z;
        Vector3 pos = Vector3.zero;
        if (POS_Z > 0)
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        if (POS_Z < 0)
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        transform.LookAt(pos);
    }

    void Open_Treasure()
    {
        GameObject m_treasure_Bar = Instantiate(Treasure_Bar_Obj, transform.position, Quaternion.identity);
        m_treasure_Bar.GetComponent<Object_Status>().Set_Treasure_Bar(Type, QTY, gameObject.name);

        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        m_local_Manager.GetComponent<Local_Manager>().Add_Obj_To_Pool_By_Time(gameObject, 3.0f, "Treasure");
    }

    public void Set_Treasure_Bar(int type, int qty, string name)
    {
        Type = type;
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        _camera = m_local_Manager.GetComponent<Local_Manager>().Get_Camera();
        Speed = 1.0f;
        POS = new Vector3(transform.position.x, transform.position.y + 4.0f, transform.position.z);

        Treasure_Bar = true;
        if (type == 1)
            Treasure_Gold.SetActive(true);
        if (type == 2)
            Treasure_Diamond.SetActive(true);
        if (type == 3)
            Treasure_Token.SetActive(true);
        Text_QTY_Tag.GetComponent<Text>().text = qty.ToString();
        Text_QTY_Tag.GetComponent<Text>().color = Damage_Text_Color;
        Text_QTY_Tag.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);
    }

    public void Animation_Active_Obj(int Number)
    {
        Debug.Log("Animation_Active_Obj || " + Number);
        switch (Number)
        {
            case (1):
                Obj_1.SetActive(true);
                break;
            case (2):
                Obj_2.SetActive(true);
                break;
            case (3):
                Obj_3.SetActive(true);
                break;
            case (4):
                Obj_4.SetActive(true);
                break;
        }
    }

    public void Animation_Deactive_Obj(int Number)
    {
        switch (Number)
        {
            case (1):
                Obj_1.SetActive(false);
                break;
            case (2):
                Obj_2.SetActive(false);
                break;
            case (3):
                Obj_3.SetActive(false);
                break;
            case (4):
                Obj_4.SetActive(false);
                break;
        }
    }

    public void Treasure_Box_Fall_Animation_Finish()
    {
        GetComponent<Animator>().SetTrigger("Treasure_Box_Open");
    }

    public void Treasure_Box_Open_Animation_Finish()
    {
        GetComponent<Animator>().ResetTrigger("Treasure_Box_Open");
        UI_Treasure_Box.GetComponent<UI_Treasure_Box>().Treasure_Box_Open_Animation_Finish();
    }
}

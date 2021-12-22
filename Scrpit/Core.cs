using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    public int Level = 0, Power_Up = 0;
    public float Core_HP;
    public float Core_Basic_Damage;

    public float Core_Own_Add_HP_Per_Second; // Core Add HP Per_Second
    public float Core_Add_HP_Pre_Second_By_Tower; // Tower ADD HP to core per second

    public float Core_Provide_Damage_Attack_Rate_For_Bouns; // Tower can extra get Attack Rate from core

    public int Core_Exp;
    public bool Allow_Level_Up = false;
    public int Require_Exp_For_Level_Up = 1000;
    public float Core_Convert_Enemy_HP_To_Core_HP_Rate = 0.05f;
    public float Core_Convert_Enemy_Bouns_HP = 0;

    float Timer, Core_Shot_Timer;

    public GameObject Core_HP_Text, GM, Tower_Controller, Player_Obj;
    public short Player_Number;
    float distance = 3, Critical_Rate;
    int Critical_Damage;
    public GameObject Object_Manager;

    void Start()
    {
        if (Level == 0)
            Level = 1;
        if (Power_Up == 0)
            Power_Up = 1;
        Core_HP = 100;
        Core_Basic_Damage = 1;
        Core_Own_Add_HP_Per_Second = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        Core_Shot_Timer += Time.deltaTime;
        if (Timer >= 1)
        {
            Timer = 0;
            Core_HP += Core_Own_Add_HP_Per_Second;
        }

        if (Core_Exp >= Require_Exp_For_Level_Up)
        {
            Allow_Level_Up = true;
            Core_Level_Up();
        }

        if (Core_Shot_Timer >= 0.5)
        {
            Core_Shot_Damage();
        }
    }

    void Core_Shot_Damage()
    {
        GameObject Map_Manager = GM.GetComponent<GameMaster>().Map_Manager;
        string[] tag = Map_Manager.GetComponent<Map_Manager>().Find_Opponent_Enemy_Tag(Player_Number);
        GameObject[] Closest_Core_Enemy = Map_Manager.GetComponent<Map_Manager>().Search_Enemy_Obj_By_Tag_Array(tag);
        if (Closest_Core_Enemy.Length == 0)
            return;
        if (Closest_Core_Enemy[0] == null)
            return;
        Closest_Core_Enemy[0] = Search_Closest_Core_Enemy_In_Area(Closest_Core_Enemy);
        GameObject Closest_Enemy = Closest_Core_Enemy[0];

        if (!Closest_Enemy)
            return;

        if (Closest_Enemy)
            Core_Shot_Timer = 0;

        if (Critical_Damage == 0)
        {
            Critical_Damage = Object_Manager.GetComponent<Object_Manager>().Get_Player_profile(Player_Number).Critical_Damage;

            if (Critical_Damage < 100)
                Critical_Damage = 100;
        }

        Tower_Controller.GetComponent<Tower_Controller>().Set_Tower_Counter(1, null); // 1 Counter Attack

        if (Critical_Rate < 5)
            Critical_Rate = 5;

        if (Critical_Rate > 50)
            Critical_Rate = 50;

        float Final_Damage = Core_HP;
        //float Final_Damage = Core_Level_Damage()[Level] + Core_Level_Damage()[Level];

        // Send_Damage_To_Gamemaster for highest damage attack to enemy task 
        GM.GetComponent<GameMaster>().Set_High_Damage((short)Player_Number, (int)Final_Damage);
        // Send_Damage_To_Gamemaster for highest damage attack to enemy task 

        float Distance = Vector3.Distance(Closest_Enemy.transform.position, Closest_Enemy.transform.position);
        float time = Distance / 12;

        StartCoroutine(Damage_After_Time(time, Closest_Enemy, Final_Damage, (int)Critical_Rate, Critical_Damage, Closest_Enemy.name));
        short Core_Player_Number = Player_Number;
        short Total_Player_In_Match = GM.GetComponent<GameMaster>().Get_Total_Player_Number_In_Match();
        for (int i = 1; i < Total_Player_In_Match + 1; i++)
        {
            GameObject Player_OBj = Object_Manager.GetComponent<Object_Manager>().Get_Player_Obj(i);
            if (ReferenceEquals(Player_OBj, null))
                Player_OBj = null;
            if (Player_OBj != null && Closest_Enemy)
                Player_OBj.GetComponent<Player_Network>().Core_Shot_Enemy(Closest_Enemy, Core_Player_Number);
        }
    }

    IEnumerator Damage_After_Time(float Time, GameObject Closest_Enemy, float final_Damage, int c_rate, int c_Dmg, string name)
    {
        if (Closest_Enemy.name != name || Closest_Enemy.tag == "Untagged")
            yield break;

        yield return new WaitForSeconds(Time);
        short code = (short)Enemy_Code.Attack;
        Closest_Enemy.GetComponent<Enemy>().Enemy_Damage(final_Damage, code, c_rate, c_Dmg, Closest_Enemy.name, "Core");
    }

    public void Core_Level_Up()
    {
        if (Allow_Level_Up)
        {
            Allow_Level_Up = false;
            Level++;
            Core_Exp -= Require_Exp_For_Level_Up;
            Require_Exp_For_Level_Up = Require_Exp_For_Level_Up * 2;
            Core_Convert_Enemy_HP_To_Core_HP_Rate += 0.01f;
            Core_Own_Add_HP_Per_Second++;
        }
    }

    public void Core_Add_HP(float HP)
    {
        Core_HP += HP;
    }

    public void Core_Damage(float Damage_Value)
    {
        Core_HP -= Damage_Value;
    }

    public float[] Core_Level_Damage()
    {
        return new float[] { 0, 100, 200, 300, 400, 800, 1000, 1200, 1500, 2000, 2500, 3000, 4000, 5000, 8000 };
    }

    public float[] Core_Power_Damage()
    {
        return new float[] { 0, 0, 100, 200, 300, 400, 800, 1000, 1200, 1500, 2000, 2500, 3000, 4000 };
    }


    GameObject Search_Closest_Core_Enemy_In_Area(GameObject[] _Obj_Array)
    {
        GameObject nearestEnemy = null;
        float Walked_Distance = 0; // Mathf.Infinity;
        foreach (GameObject enemy in _Obj_Array)
        {
            if (enemy != null)
            {
                float Walked = enemy.GetComponent<Enemy>().Distance_from_Start_Point;
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                bool Enemy_Setup_Finish = enemy.GetComponent<Enemy>().Setup_Finish;
                if (Enemy_Setup_Finish)
                {
                    if (Walked > Walked_Distance && distanceToEnemy <= distance)
                    {
                        Walked_Distance = Walked;
                        nearestEnemy = enemy;
                    }
                }
            }
        }
        return nearestEnemy;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Object_Status_Network : NetworkBehaviour // MonoBehaviour // NetworkBehaviour
{
    #region Bullet
    bool Ground_Bullet;
    public bool Chain_Weapon_Point, Local_Chain_Weapon_Point, Enemy_Transfer;
    public float Damage, value_X = 0.5f, value_Y = 0.5f;
    float Bullet_Speed;
    Vector3 Bullet_Target_POS;
    int Critical_Rate, Critical_Damage;
    float Speed, Timer;

    public GameObject Effect_01, Effect_02;
    string Tag;
    GameObject GM;

    #endregion

    public void Set_Ground_Boom(float Dmg, string tag, int critical_Rate, int critical_Damage, GameObject gm)
    {
        GM = gm;
        Critical_Rate = critical_Rate;
        Critical_Damage = critical_Damage;
        Damage = Dmg;
        Tag = tag;
        Ground_Bullet = true;
    }

    void Ground_Boom_Check_Enemy()
    {
        Transform nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag);
        float distanceToEnemy = 999;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= 0.3f)
            {
                nearestEnemy = enemy.transform;
            }
        }

        if (nearestEnemy == null)
            return;
        Ground_Bullet = false;
        Rpc_Active_or_Deactive_Object(Effect_01, false);
        Rpc_Active_or_Deactive_Object(Effect_02, true);
        short Code = (short)Enemy_Code.Attack;
        nearestEnemy.GetComponent<Enemy>().Enemy_Damage(Damage, Code, Critical_Rate, Critical_Damage, nearestEnemy.name, "Ground_Boom"); // True = damage , false = heal
        nearestEnemy.GetComponent<Enemy>().Active_Or_Deactive_Object("Explosion_Effect_01", 0, true);
        StartCoroutine(Server_Send_Object_To_Pool(gameObject, 3.0f));
    }

    IEnumerator Server_Send_Object_To_Pool(GameObject _obj, float Timer)
    {
        yield return new WaitForSeconds(Timer);
        Reset_Ground_Boom();
        GM.GetComponent<GameMaster>().Ground_Boom_List.Add(gameObject);
        RPC_Send_Object_To_Pool();
    }

    [ClientRpc]
    void RPC_Send_Object_To_Pool()
    {
        Reset_Ground_Boom();
        Effect_01.SetActive(false);
        Effect_02.SetActive(false);
        GameObject m_Pool_Manager = GameObject.Find("Pool_Manager");
        m_Pool_Manager.GetComponent<Pool_Manager>().Ground_Boom_Pool.Add(gameObject);
    }

    void Reset_Ground_Boom()
    {
        Critical_Rate = 0;
        Critical_Damage = 0;
        Damage = 0;
        Tag = null;
        Ground_Bullet = false;
        GetComponent<Object_Status_Network>().enabled = false;
        gameObject.transform.position = new Vector3(0, 100, 0);
    }

    [ClientRpc]
    void Rpc_Active_or_Deactive_Object(GameObject obj_1, bool Active_or_Deactive_1)
    {
        obj_1.SetActive(Active_or_Deactive_1);
    }

    void Update()
    {
        if (Ground_Bullet)
        {
            Timer += Time.deltaTime;
            if (Timer >= 0.2)
            {
                Timer = 0;
                Ground_Boom_Check_Enemy();
            }
        }
    }
}

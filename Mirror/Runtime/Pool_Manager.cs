using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool_Manager : MonoBehaviour
{
    public GameObject Local_Manager_Obj;
    public GameObject Tower_Obj, Enemy_Obj, HP_Bar_Obj, Damage_Bar_OBj, Gold_Bar_Obj, Treasure_Bar_Obj, Treasure_Box_Obj, Bullet_Obj,
        Bullet_Line_Render_Obj, Ground_Boom_Obj;

    public GameObject Tower_Folder, Enemy_Folder, HP_Bar_Folder, Damage_Bar_Folder, Gold_Bar_Folder, Treasure_Bar_Folder,
        Treasure_Box_Folder, Bullet_Folder, Bullet_Line_Render_Folder, Ground_Boom_Folder;

    [HideInInspector]
    public List<GameObject> Tower_Pool, Enemy_Pool, HP_Bar_Pool, Damage_Bar_Pool, Gold_Bar_Pool, Treasure_Bar_Pool, Treasure_Box_Pool,
        Bullet_Pool, Bullet_Line_Render_Pool, Ground_Boom_Pool;

    public DateTime Timer;

    void Start()
    {
        Tower_Pool = new List<GameObject>();
        Enemy_Pool = new List<GameObject>();
        HP_Bar_Pool = new List<GameObject>();
        Damage_Bar_Pool = new List<GameObject>();
        Gold_Bar_Pool = new List<GameObject>();
        Treasure_Bar_Pool = new List<GameObject>();
        Treasure_Box_Pool = new List<GameObject>();
        Bullet_Pool = new List<GameObject>();
        Bullet_Line_Render_Pool = new List<GameObject>();
        Ground_Boom_Pool = new List<GameObject>();
        Debug.Log("Pool_Manager || Start");
        Create_List(Tower_Pool, Tower_Obj, 30, "Tower_Object", Tower_Folder);
        Create_List(Enemy_Pool, Enemy_Obj, 40, "Enemy", Enemy_Folder);
        Create_List(HP_Bar_Pool, HP_Bar_Obj, 30, "HP_Bar", HP_Bar_Folder);
        Create_List(Damage_Bar_Pool, Damage_Bar_OBj, 20, "Damage_Bar", Damage_Bar_Folder);
        Create_List(Gold_Bar_Pool, Gold_Bar_Obj, 20, "Gold_Bar", Gold_Bar_Folder);
        Create_List(Treasure_Bar_Pool, Treasure_Bar_Obj, 3, "Treasure_Bar", Treasure_Bar_Folder);
        Create_List(Treasure_Box_Pool, Treasure_Box_Obj, 1, "Treasure_Box", Treasure_Box_Folder);
        Create_List(Bullet_Pool, Bullet_Obj, 30, "Bullet", Bullet_Folder);
        Create_List(Bullet_Line_Render_Pool, Bullet_Line_Render_Obj, 10, "Lightning_Line_Render", Bullet_Line_Render_Folder);
        Create_List(Ground_Boom_Pool, Ground_Boom_Obj, 3, "Ground_Boom", Ground_Boom_Folder);
        DontDestroyOnLoad(gameObject);
        Debug.Log("Pool_Manager || End");
    }

    void Create_List(List<GameObject> list, GameObject Object, short QTY, string obj_Name, GameObject Folder)
    {
        GameObject Resources_obj = Resources.Load<GameObject>(obj_Name);
        GameObject obj = null;
        if (!Resources_obj)
            Resources_obj = Object;
        Vector3 POS = new Vector3(100, 0, 0);

        for (int i = 0; i < QTY + 1; i++)
        {
            obj = Instantiate(Resources_obj, POS, Quaternion.identity);
            obj.SetActive(false);
            list.Add(obj);
            obj.transform.SetParent(Folder.transform);
        }
    }

    public GameObject Get_Object_From_Pool_By_Tag(string Tag)
    {
        switch (Tag)
        {
            case ("Enemy"):
            case ("Enemy_01"):
            case ("Enemy_02"):
            case ("Enemy_03"):
            case ("Enemy_04"):
                GameObject Enemy_Obj = Get_Pool(Enemy_Pool);
                return Enemy_Obj;
            case ("Tower"):
            case ("Tower_01"):
            case ("Tower_02"):
            case ("Tower_03"):
            case ("Tower_04"):
                GameObject Tower_Obj = Get_Pool(Tower_Pool);
                return Tower_Obj;
            case ("Damage_Bar"):
                GameObject Damage_Bar_Obj = Get_Pool(Damage_Bar_Pool);
                return Damage_Bar_Obj;
            case ("Ground_Boom"):
                GameObject Ground_Boom_Obj = Get_Pool(Ground_Boom_Pool);
                return Ground_Boom_Obj;
            case ("Lightning_Line_Render"):
                GameObject Bullet_Line_Render_Obj = Get_Pool(Bullet_Line_Render_Pool);
                return Bullet_Line_Render_Obj;
        }
        return null;

        GameObject Get_Pool(List<GameObject> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i] != null)
                {
                    GameObject obj = List[i];
                    List.Remove(obj);
                    return obj;
                }
            }
            return null;
        }
    }

    public void Reset_Pool()
    {
        Tower_Pool = Set_Obj_To_List(Tower_Pool, Tower_Folder);
        Enemy_Pool = Set_Obj_To_List(Enemy_Pool, Enemy_Folder);
        HP_Bar_Pool = Set_Obj_To_List(HP_Bar_Pool, HP_Bar_Folder);
        Damage_Bar_Pool = Set_Obj_To_List(Damage_Bar_Pool, Damage_Bar_Folder);
        Gold_Bar_Pool = Set_Obj_To_List(Gold_Bar_Pool, Gold_Bar_Folder);
        Treasure_Bar_Pool = Set_Obj_To_List(Treasure_Bar_Pool, Treasure_Bar_Folder);
        Treasure_Box_Pool = Set_Obj_To_List(Treasure_Box_Pool, Treasure_Box_Folder);
        Bullet_Pool = Set_Obj_To_List(Bullet_Pool, Bullet_Folder);
        Bullet_Line_Render_Pool = Set_Obj_To_List(Bullet_Line_Render_Pool, Bullet_Line_Render_Folder);
        Ground_Boom_Pool = Set_Obj_To_List(Ground_Boom_Pool, Ground_Boom_Folder);

        int temp_number_3 = 0;
        foreach (Transform obj in Tower_Folder.transform)
        {
            temp_number_3++;
        }
        Debug.Log("temp_number_3 || " + temp_number_3);
    }

    public List<GameObject> Set_Obj_To_List(List<GameObject> list, GameObject folder)
    {
        list = new List<GameObject>();
        Vector3 POS = new Vector3(100, 0, 0);
        foreach (Transform obj in folder.transform)
        {
            obj.gameObject.SetActive(false);
            list.Add(obj.gameObject);
        }
        return list;
    }

    public List<GameObject> Get_Pool_List(string Tag)
    {
        switch (Tag)
        {
            case ("Enemy"):
            case ("Enemy_01"):
            case ("Enemy_02"):
            case ("Enemy_03"):
            case ("Enemy_04"):
                return Enemy_Pool;
            case ("Tower"):
            case ("Tower_01"):
            case ("Tower_02"):
            case ("Tower_03"):
            case ("Tower_04"):
                return Tower_Pool;
            case ("Damage_Bar"):
                return Damage_Bar_Pool;
            case ("Ground_Boom"):
                return Ground_Boom_Pool;
            case ("Lightning_Line_Render"):
                return Bullet_Line_Render_Pool;
        }
        return null;
    }

    public void Set_Obj_To_Folder_By_Name(string Tag, GameObject Obj)
    {
        switch (Tag)
        {
            case ("Tower"):
                Obj.transform.SetParent(Tower_Folder.transform);
                break;
            case ("Enemy"):
                Obj.transform.SetParent(Enemy_Folder.transform);
                break;
            case ("HP_Bar"):
                Obj.transform.SetParent(HP_Bar_Folder.transform);
                break;
            case ("Damage_Bar"):
                Obj.transform.SetParent(Damage_Bar_Folder.transform);
                break;
            case ("Gold_Bar"):
                Obj.transform.SetParent(Gold_Bar_Folder.transform);
                break;
            case ("Treasure_Bar"):
                Obj.transform.SetParent(Treasure_Bar_Folder.transform);
                break;
            case ("Treasure_Box"):
                Obj.transform.SetParent(Treasure_Box_Folder.transform);
                break;
            case ("Bullet"):
                Obj.transform.SetParent(Bullet_Folder.transform);
                break;
            case ("Lightning_Line_Render"):
                Obj.transform.SetParent(Bullet_Line_Render_Folder.transform);
                break;
            case ("Ground_Boom"):
                Obj.transform.SetParent(Ground_Boom_Folder.transform);
                break;
        }
    }

    public void Set_Obj_To_Pool_By_Name(string Tag, GameObject Obj)
    {
        switch (Tag)
        {
            case ("Tower"):
                Tower_Pool.Add(Obj);
                break;
            case ("Enemy"):
                Enemy_Pool.Add(Obj);
                break;
            case ("HP_Bar"):
                HP_Bar_Pool.Add(Obj);
                break;
            case ("Damage_Bar"):
                Damage_Bar_Pool.Add(Obj);
                break;
            case ("Gold_Bar"):
                Gold_Bar_Pool.Add(Obj);
                break;
            case ("Treasure_Bar"):
                Treasure_Bar_Pool.Add(Obj);
                break;
            case ("Treasure_Box"):
                Treasure_Box_Pool.Add(Obj);
                break;
            case ("Bullet"):
                Bullet_Pool.Add(Obj);
                break;
            case ("Lightning_Line_Render"):
                Bullet_Line_Render_Pool.Add(Obj);
                break;
            case ("Ground_Boom"):
                Ground_Boom_Pool.Add(Obj);
                break;
        }
        Set_Obj_To_Folder_By_Name(Tag, Obj);
    }
}

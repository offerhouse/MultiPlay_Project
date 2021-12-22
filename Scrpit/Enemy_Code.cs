using UnityEngine;

public enum Enemy_Code
{
    // Standard error code
    Error = 101,
    Spawn_None,
    Basic,
    Enemy,
    Protector,
    Attacker,
    Horse,
    Horse_Protector,
    Horse_Attacker,
    Thief,
    Boomer,
    Devil,
    Dragon,

    Object_Basic,
    Object_Protector,
    Object_Horse_Attacker,
    Object_Thief,
    Object_Boomer,
    /// <summary> Devil_Type_1 </summary>
    Object_Witch,
    /// <summary> Devil_Type_2 </summary>
    Object_Evil_Eyes,
    /// <summary> Devil_Type_3 </summary>
    Object_Demon,
    Object_Dragon_Head,

    Normal_Spwan,
    Fall_Spwan,

    OP_Path_1,
    OP_Path_2,
    OP_Path_3,
    OP_Path_4,
    OP_Path_Around_Map_1,
    OP_Path_Around_Map_2,
    OP_Path_Around_Map_3,
    OP_Path_Around_Map_4,
    Previous_Point,
    Next_Point,

    Heal,
    Attack,

    Normal_Walk,
    Around_Map_Walk,

    Normal_Attack,
    Critical_Attack,

    Damage_Bar,
    HP_Bar,
}

public class Spwan_Enemy_Packet
{
    public short Type_Code;
    public short Attack_Protector_Code;
    public short Spawn_Code;
    public short Player_Number;
    public float HP;
    public float Damage;
    public float Speed = 1;
    public bool Same_Target; // if Target is dead , fall on POS .
    public Vector3 POS;
    public GameObject Fall_Target_Object;
    public GameObject Source_Obj;
    public GameObject Tower;
    public GameObject Fall_Enemy_Helper;
    public string Source_Obj_Tag;
    public short Source_Obj_Attack_Protector_Code;
    public string Tag;
    public GameObject Move_Target_Point;
    public GameObject Previous_Point;
}

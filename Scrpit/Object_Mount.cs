using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Object_Mount : MonoBehaviour
{
    public float Distance_from_Start_Point;

    [Header("Enemy_Move_Point")]
    public GameObject Obj_1V1_Enemy_Previous_Point_01;
    public GameObject Obj_1V1_Enemy_Previous_Point_02;
    public GameObject Obj_1V1_Enemy_Next_Point_01;
    public GameObject Obj_1V1_Enemy_Next_Point_02;

    [Header("Protector_Move_Point")]
    public GameObject Obj_1V1_Protector_Previous_Point;
    public GameObject Obj_1V1_Protector_Next_Point;

    [Header("Attacker_Move_Point")]
    public GameObject Obj_1V1_Attacker_Previous_Point_01;
    public GameObject Obj_1V1_Attacker_Previous_Point_02;
    public GameObject Obj_1V1_Attacker_Next_Point_01;
    public GameObject Obj_1V1_Attacker_Next_Point_02;

    public bool B_1V1_Attacker_End_Point;
    public bool B_2OP_Attacker_End_Point;

    public bool B_1V1_Enemy_End_Point;
    public bool B_2OP_Enemy_End_Point;
    public bool B_1V1_Protector_Patrol_Point;

    [Header("Partol_Point to other player Map")]
    public bool Patrol_Point_1_Start;
    public bool Patrol_Point_1_END;
    public bool Patrol_Point_2_Start;
    public bool Patrol_Point_2_END;
    public bool OP_Enemy_Around_Map_01_Start;
    public bool OP_Enemy_Around_Map_01_End;
    public bool OP_Enemy_Around_Map_02_Start;
    public bool OP_Enemy_Around_Map_02_End;

    public GameObject Obj_2OP_Enemy_Previous_Point_01;
    public GameObject Obj_2OP_Enemy_Next_Point_01;
    public GameObject Obj_2OP_Enemy_Previous_Point_02;
    public GameObject Obj_2OP_Enemy_Next_Point_02;
    public GameObject Obj_2OP_Enemy_Around_Map_Previous_Point_01;
    public GameObject Obj_2OP_Enemy_Around_Map_Next_Point_01;
    public GameObject Obj_2OP_Enemy_Around_Map_Previous_Point_02;
    public GameObject Obj_2OP_Enemy_Around_Map_Next_Point_02;
    public GameObject Obj_2OP_Attacker_Previous_Point_01, Obj_2OP_Attacker_Next_Point_01;
    public GameObject Obj_2OP_Attacker_Previous_Point_02, Obj_2OP_Attacker_Next_Point_02;
    public GameObject Obj_2OP_Attacker_Around_Map_Previous_Point_01, Obj_2OP_Attacker_Around_Map_Next_Point_01;
    public GameObject Obj_2OP_Attacker_Around_Map_Previous_Point_02, Obj_2OP_Attacker_Around_Map_Next_Point_02;

    public short OP_Path_Code; // if 1 = player 1 , Spwan at Spwan_Point 1 , use Path 1 .

    [Header("Test")]
    public GameObject Test_Text;
    public short Player_Number;

    // Not Allow Start or Awake or Update !!!!!!
}

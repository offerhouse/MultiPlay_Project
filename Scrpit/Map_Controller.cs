using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Controller : MonoBehaviour
{
    public GameObject Map_Manager;
    public short S_1V1_Protector_Spawn_Point_QTY;
    public short S_2V2_Protector_Spwan_Point_QTY;
    public short S_2OP_Protector_Spwan_Point_QTY;
    public short S_4OP_Protector_Spwan_Point_QTY;
    public short S_1V1_Enemy_Spawn_Gate_QTY, S_2V2_Enemy_Spawn_Gate_QTY, S_2OP_Enemy_Spawn_Gate_QTY, S_4OP_Enemy_Spawn_Gate_QTY;
    public short Player_Number;
    public short op_room_Code;

    public GameObject Core;
    public GameObject Tower_Controller;
    public GameObject Enemy_Gate_01, Enemy_Gate_02, Enemy_Gate_03, Enemy_Gate_04, Enemy_Gate_05, Enemy_Gate_06, Enemy_Gate_07, Enemy_Gate_08;
    public GameObject Attacker_Spawn_Point_01, Attacker_Spawn_Point_02, Attacker_Spawn_Point_03, Attacker_Spawn_Point_04;
    public GameObject Protector_Spawn_Point_01, Protector_Spawn_Point_02, Protector_Spawn_Point_03, Protector_Spawn_Point_04;
    public GameObject OP_Enemy_Gate_01, OP_Enemy_Gate_02, OP_Enemy_Gate_03, OP_Enemy_Gate_04,
        OP_Attacker_Spawn_Point_01, OP_Attacker_Spawn_Point_02, OP_Attacker_Spawn_Point_03, OP_Attacker_Spawn_Point_04,
        OP_Protector_Spawn_Point_01, OP_Protector_Spawn_Point_02, OP_Protector_Spawn_Point_03, OP_Protector_Spawn_Point_04;

    public GameObject Portal_Point_1_Start, Portal_Point_1_End;
    public GameObject Portal_Point_2_Start, Portal_Point_2_End;
    public GameObject Portal_Point_3_Start, Portal_Point_3_End;
    public GameObject Portal_Point_4_Start, Portal_Point_4_End;

    public GameObject Around_Map_1_Start, Around_Map_1_End;
    public GameObject Around_Map_2_Start, Around_Map_2_End;
    public GameObject Around_Map_3_Start, Around_Map_3_End;
    public GameObject Around_Map_4_Start, Around_Map_4_End;

    public GameObject Get_Enemy_Gate_Point(int number)
    {
        GameObject _Gate_Point = null;
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                {
                    switch (number)
                    {
                        case (1): return Enemy_Gate_01;
                        case (2): return Enemy_Gate_02;
                        case (3): return Enemy_Gate_03;
                        case (4): return Enemy_Gate_04;
                        case (5): return Enemy_Gate_05;
                        case (6): return Enemy_Gate_06;
                        case (7): return Enemy_Gate_07;
                        case (8): return Enemy_Gate_08;
                    }
                    return _Gate_Point;
                }
            case ((short)OP_Room_Code.Match2op):
                {
                    switch (number)
                    {
                        case (1): return OP_Enemy_Gate_01;
                        case (2): return OP_Enemy_Gate_02;
                        case (3): return OP_Enemy_Gate_03;
                        case (4): return OP_Enemy_Gate_04;
                    }
                    return _Gate_Point;
                }
        }
        return _Gate_Point;
    }

    public GameObject Get_Protector_Spawn_Point(int number)
    {
        GameObject Spawn_Point = null;
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                {
                    switch (number)
                    {
                        case (1): return Protector_Spawn_Point_01;
                        case (2): return Protector_Spawn_Point_02;
                        case (3): return Protector_Spawn_Point_03;
                        case (4): return Protector_Spawn_Point_04;
                    }
                    return Spawn_Point;
                }
            case ((short)OP_Room_Code.Match2op):
                {
                    switch (number)
                    {
                        case (1): return OP_Protector_Spawn_Point_01;
                        case (2): return OP_Protector_Spawn_Point_02;
                        case (3): return OP_Protector_Spawn_Point_03;
                        case (4): return OP_Protector_Spawn_Point_04;
                    }
                    return Spawn_Point;
                }
        }
        return Spawn_Point;
    }

    public GameObject Get_Attacker_Spawn_Point(int number)
    {
        GameObject Attacker_Point = null;
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                {
                    switch (number)
                    {
                        case (1): return Attacker_Spawn_Point_01;
                        case (2): return Attacker_Spawn_Point_02;
                        case (3): return Attacker_Spawn_Point_03;
                        case (4): return Attacker_Spawn_Point_04;
                    }
                    return Attacker_Point;
                }
            case ((short)OP_Room_Code.Match2op):
                {
                    switch (number)
                    {
                        case (1): return OP_Attacker_Spawn_Point_01;
                        case (2): return OP_Attacker_Spawn_Point_02;
                        case (3): return OP_Attacker_Spawn_Point_03;
                        case (4): return OP_Attacker_Spawn_Point_04;
                    }
                    return Attacker_Point;
                }
        }
        return Attacker_Point;
    }

    // while enemy touch this point (protector spwan point = enemy_end_Point) , tell GM core Damage.
    public void Set_End_Point_Player_Number()
    {
        Set_Number(Protector_Spawn_Point_01, Player_Number);
        Set_Number(Protector_Spawn_Point_02, Player_Number);
        Set_Number(Protector_Spawn_Point_03, Player_Number);
        Set_Number(Protector_Spawn_Point_04, Player_Number);

        Set_Number(OP_Protector_Spawn_Point_01, Player_Number);
        Set_Number(OP_Protector_Spawn_Point_02, Player_Number);
        Set_Number(OP_Protector_Spawn_Point_03, Player_Number);
        Set_Number(OP_Protector_Spawn_Point_04, Player_Number);

        Set_Number(OP_Attacker_Spawn_Point_01, Player_Number);
        Set_Number(OP_Attacker_Spawn_Point_02, Player_Number);
        Set_Number(OP_Attacker_Spawn_Point_03, Player_Number);
        Set_Number(OP_Attacker_Spawn_Point_04, Player_Number);

        Set_Number(OP_Enemy_Gate_01, Player_Number);
        Set_Number(OP_Enemy_Gate_02, Player_Number);
        Set_Number(OP_Enemy_Gate_03, Player_Number);
        Set_Number(OP_Enemy_Gate_04, Player_Number);
        void Set_Number(GameObject Obj, short number)
        {
            if (Obj != null)
                Obj.GetComponent<Object_Mount>().Player_Number = number;
        }
    }

    short Get_Random_Number(short Max_Number)
    {
        return (short)Random.Range(1, Max_Number + 1);
    }

    public GameObject Get_1V1_Gate_Point()
    {
        short Enemy_Spawn_Gate_QTY = Get_Map_Controller_Enemy_Spwan_Point_QTY();

        int Player_Spawn_Number = Get_Random_Number(Enemy_Spawn_Gate_QTY);
        return Get_Enemy_Gate_Point(Player_Spawn_Number);
    }

    public short Get_Map_Controller_Enemy_Spwan_Point_QTY()
    {
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                return S_1V1_Enemy_Spawn_Gate_QTY;
            case ((short)OP_Room_Code.Match2v2):
                return S_2V2_Enemy_Spawn_Gate_QTY;
            case ((short)OP_Room_Code.Match2op):
                return S_2OP_Enemy_Spawn_Gate_QTY;
            case ((short)OP_Room_Code.Match4op):
                return S_4OP_Enemy_Spawn_Gate_QTY;
        }
        return 0;
    }


    public short Get_Map_Controller_Protector_Spwan_Point_QTY()
    {
        switch (op_room_Code)
        {
            case ((short)OP_Room_Code.Match1v1):
                return S_1V1_Protector_Spawn_Point_QTY;
            case ((short)OP_Room_Code.Match2v2):
                return S_2V2_Protector_Spwan_Point_QTY;
            case ((short)OP_Room_Code.Match2op):
                return S_2OP_Protector_Spwan_Point_QTY;
            case ((short)OP_Room_Code.Match4op):
                return S_4OP_Protector_Spwan_Point_QTY;
        }
        return 0;
    }
}

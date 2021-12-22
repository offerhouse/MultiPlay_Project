using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterServerToolkit.MasterServer;

public class Map_Manager : MonoBehaviour
{
    public GameObject Object_Manager;
    public short OP_Code;
    public GameObject Map;
    public bool One_VS_One;
    public bool Two_VS_Two;
    public bool Two_Cooperation;
    public bool Four_Cooperation;

    public void Setup_Map_Manager()
    {
        Debug.Log("Setup_Map_Manager");
        if (OP_Code == (short)MstMessageCodes.Queue1v1)
            One_VS_One = true;
        if (OP_Code == (short)MstMessageCodes.Queue2v2)
            Two_VS_Two = true;
        if (OP_Code == (short)MstMessageCodes.Queue2op)
            Two_Cooperation = true;
        if (OP_Code == (short)MstMessageCodes.Queue4op)
            Four_Cooperation = true;
    }

    public int[] Enemy_Number_Find_Owner_And_Parter(int Player_Number)
    {
        //Debug.Log("Enemy_Number_Find_Owner_And_Parter || " + One_VS_One + " || " + Two_VS_Two + " || " + Two_Cooperation + " || " + Four_Cooperation);
        int[] Parter = new int[] { 0, 0, 0, 0 };
        if (One_VS_One)
        {
            if (Player_Number == 1) Parter = new int[] { 2, 0, 0, 0 };
            if (Player_Number == 2) Parter = new int[] { 1, 0, 0, 0 };
        }
        if (Two_VS_Two)
        {
            if (Player_Number == 1 || Player_Number == 2)
                Parter = new int[] { 3, 4, 0, 0 };
            if (Player_Number == 3 || Player_Number == 4)
                Parter = new int[] { 1, 2, 0, 0 };
        }
        if (Two_Cooperation || Four_Cooperation)
            Parter = new int[] { 1, 2, 3, 4 };
        return Parter;
    }

    public int Find_Opponent_Number(int Player_Number)
    {
        int Opponent_Number = 0;
        if (One_VS_One)
        {
            if (Player_Number == 1) Opponent_Number = 2;
            if (Player_Number == 2) Opponent_Number = 1;
        }
        if (Two_VS_Two)
        {
            if (Player_Number == 1 || Player_Number == 2)
                Opponent_Number = Random.Range(1, 3);
            if (Player_Number == 3 || Player_Number == 4)
                Opponent_Number = Random.Range(3, 5);
        }
        if (Two_Cooperation || Four_Cooperation)
        {
            if (Player_Number == 1 || Player_Number == 2)
                Opponent_Number = Random.Range(3, 5);
            if (Player_Number == 3 || Player_Number == 4)
                Opponent_Number = Random.Range(1, 3);
        }
        return Opponent_Number;
    }

    public string[] Find_Opponent_Tag(int Player_Number)
    {
        string[] Tag = null;
        if (One_VS_One && Player_Number == 1)
            Tag = new string[] { "Enemy_02", "Player02_Protector", "Player02_Attacker" };
        if (One_VS_One && Player_Number == 2)
            Tag = new string[] { "Enemy_01", "Player01_Protector", "Player01_Attacker" };
        if (Two_VS_Two && (Player_Number == 1 || Player_Number == 2))
            Tag = new string[] { "Enemy_03", "Enemy_04", "Player03_Protector", "Player04_Protector", "Player03_Attacker", "Player04_Attacker" };
        if (Two_VS_Two && (Player_Number == 3 || Player_Number == 4))
            Tag = new string[] { "Enemy_01", "Enemy_02", "Player01_Protector", "Player02_Protector", "Player01_Attacker", "Player02_Attacker" };
        if (Two_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02" };
        if (Four_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02", "Enemy_03", "Enemy_04" };
        return Tag;
    }

    public string[] Find_Opponent_Enemy_Tag(int Player_Number)
    {
        string[] Tag = null;
        if (One_VS_One && Player_Number == 1)
            Tag = new string[] { "Enemy_02" };
        if (One_VS_One && Player_Number == 2)
            Tag = new string[] { "Enemy_01" };
        if (Two_VS_Two && (Player_Number == 1 || Player_Number == 2))
            Tag = new string[] { "Enemy_03", "Enemy_04" };
        if (Two_VS_Two && (Player_Number == 3 || Player_Number == 4))
            Tag = new string[] { "Enemy_01", "Enemy_02" };
        if (Two_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02" };
        if (Four_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02", "Enemy_03", "Enemy_04" };
        return Tag;
    }

    public string[] Get_Own_Attacker_Protector_Tag(int Player_Number)
    {
        string[] Tag = null;
        string P1 = "Player01_Protector", P2 = "Player02_Protector", P3 = "Player03_Protector", P4 = "Player04_Protector";
        string A1 = "Player01_Attacker", A2 = "Player02_Attacker", A3 = "Player03_Attacker", A4 = "Player04_Attacker";

        if (One_VS_One && Player_Number == 1)
            Tag = new string[] { A1, P1 };
        if (One_VS_One && Player_Number == 2)
            Tag = new string[] { A2, P2 };
        if (Two_VS_Two && (Player_Number == 1 || Player_Number == 2))
            Tag = new string[] { A1, A2, P1, P2 };
        if (Two_VS_Two && (Player_Number == 3 || Player_Number == 4))
            Tag = new string[] { A3, A4, P3, P4 };
        if (Two_Cooperation)
            Tag = new string[] { A1, A2, P1, P2 };
        if (Four_Cooperation)
            Tag = new string[] { A1, A2, A3, A4, P1, P2, P3, P4 };

        return Tag;
    }

    public string[] Get_Own_Tag(int player_number)
    {
        string[] Tag = null;
        string E1 = "Enemy_01", E2 = "Enemy_02", E3 = "Enemy_03", E4 = "Enemy_04";
        string P1 = "Player01_Protector", P2 = "Player02_Protector", P3 = "Player03_Protector", P4 = "Player04_Protector";
        string A1 = "Player01_Attacker", A2 = "Player02_Attacker", A3 = "Player03_Attacker", A4 = "Player04_Attacker";

        if (One_VS_One && player_number == 1)
            Tag = new string[] { E1, P1, A1 };
        if (One_VS_One && player_number == 2)
            Tag = new string[] { E2, P2, A2 };
        if (Two_VS_Two && (player_number == 1 || player_number == 2))
            Tag = new string[] { E1, E2, P1, P2, A1, A2 };
        if (Two_VS_Two && (player_number == 3 || player_number == 4))
            Tag = new string[] { E3, E4, P3, P4, A3, A4 };
        if (Two_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02" };
        if (Four_Cooperation)
            Tag = new string[] { "Enemy_01", "Enemy_02", "Enemy_03", "Enemy_04" };

        return Tag;
    }

    public int[] Tag_Find_Opponent(string Tag)
    {
        int[] Player_Number = new int[4];
        if (One_VS_One && (Tag == "Enemy_01" || Tag == "Player02_Protector"))
            Set_Player_Number(1, 0, 0, 0);
        if (One_VS_One && (Tag == "Enemy_02" || Tag == "Player01_Protector"))
            Set_Player_Number(2, 0, 0, 0);
        if (Two_VS_Two && (Tag == "Enemy_01" || Tag == "Enemy_02" || Tag == "Player03_Protector" || Tag == "Player04_Protector"))
            Set_Player_Number(1, 2, 0, 0);
        if (Two_VS_Two && (Tag == "Enemy_03" || Tag == "Enemy_04" || Tag == "Player01_Protector" || Tag == "Player02_Protector"))
            Set_Player_Number(3, 4, 0, 0);
        if (Two_Cooperation)
            Set_Player_Number(1, 2, 0, 0);
        if (Four_Cooperation)
            Set_Player_Number(1, 2, 3, 4);

        void Set_Player_Number(int A, int B, int C, int D)
        {
            Player_Number[0] = A;
            Player_Number[1] = B;
            Player_Number[2] = C;
            Player_Number[3] = D;
        }

        return Player_Number;
    }

    public int Enemy_Type_Find_Opponent(int Player_Number,
        bool Enemy_obj, bool Protector_Obj, bool Attacker_Obj, bool Con_Enemy_Obj, bool Atk_To_Enemy_Obj)
    {
        int player_number = 0;
        if (One_VS_One)
        {
            if (Player_Number == 1)
                player_number = Check_Type(2, 2, 2, 1, 2);

            if (Player_Number == 2)
                player_number = Check_Type(1, 1, 1, 2, 1);
        }

        if (Two_VS_Two)
        {
            if (Player_Number == 1 || Player_Number == 2)
                player_number = Check_Type(3, 3, 3, 1, 3);
            if (Player_Number == 3 || Player_Number == 4)
                player_number = Check_Type(1, 1, 1, 3, 1);
        }

        int Check_Type(int A, int B, int C, int D, int E)
        {
            int number = 0;
            if (Enemy_obj)
                number = A;
            if (Protector_Obj)
                number = B;
            if (Attacker_Obj)
                number = C;
            if (Con_Enemy_Obj)
                number = D;
            if (Con_Enemy_Obj)
                number = E;
            return number;
        }

        return player_number;
    }

    public int Enemy_Type_Find_Owner(int Player_Number,
        bool Enemy_obj, bool Protector_Obj, bool Attacker_Obj, bool Con_Enemy_Obj, bool Atk_To_Enemy_Obj)
    {
        int player_number = 0;
        if (One_VS_One)
        {
            if (Player_Number == 1)
                player_number = Check_Type(1, 1, 1, 2, 1);

            if (Player_Number == 2)
                player_number = Check_Type(2, 2, 2, 1, 2);
        }

        if (Two_VS_Two)
        {
            if (Player_Number == 1 || Player_Number == 2)
                player_number = Check_Type(1, 1, 1, 3, 1);
            if (Player_Number == 3 || Player_Number == 4)
                player_number = Check_Type(3, 3, 3, 1, 3);
        }

        int Check_Type(int A, int B, int C, int D, int E)
        {
            int number = 0;
            if (Enemy_obj)
                number = A;
            if (Protector_Obj)
                number = B;
            if (Attacker_Obj)
                number = C;
            if (Con_Enemy_Obj)
                number = D;
            if (Con_Enemy_Obj)
                number = E;
            return number;
        }

        return player_number;
    }

    public GameObject[] Search_Enemy_Obj_By_Tag_Array(string[] Obj_Tag_Array)
    {
        GameObject[] Enemy_01 = new GameObject[0];
        GameObject[] Enemy_02 = new GameObject[0];
        GameObject[] Enemy_03 = new GameObject[0];
        GameObject[] Enemy_04 = new GameObject[0];
        GameObject[] Protector_01 = new GameObject[0];
        GameObject[] Protector_02 = new GameObject[0];
        GameObject[] Protector_03 = new GameObject[0];
        GameObject[] Protector_04 = new GameObject[0];
        GameObject[] Attacker_01 = new GameObject[0];
        GameObject[] Attacker_02 = new GameObject[0];
        GameObject[] Attacker_03 = new GameObject[0];
        GameObject[] Attacker_04 = new GameObject[0];
        for (int i = 0; i < Obj_Tag_Array.Length; i++)
        {
            if (Obj_Tag_Array[i] == "Enemy_01")
                Enemy_01 = GameObject.FindGameObjectsWithTag("Enemy_01");
            if (Obj_Tag_Array[i] == "Enemy_02")
                Enemy_02 = GameObject.FindGameObjectsWithTag("Enemy_02");
            if (Obj_Tag_Array[i] == "Enemy_03")
                Enemy_03 = GameObject.FindGameObjectsWithTag("Enemy_03");
            if (Obj_Tag_Array[i] == "Enemy_04")
                Enemy_04 = GameObject.FindGameObjectsWithTag("Enemy_04");
            if (Obj_Tag_Array[i] == "Player01_Protector")
                Protector_01 = GameObject.FindGameObjectsWithTag("Player01_Protector");
            if (Obj_Tag_Array[i] == "Player02_Protector")
                Protector_02 = GameObject.FindGameObjectsWithTag("Player02_Protector");
            if (Obj_Tag_Array[i] == "Player03_Protector")
                Protector_03 = GameObject.FindGameObjectsWithTag("Player03_Protector");
            if (Obj_Tag_Array[i] == "Player04_Protector")
                Protector_04 = GameObject.FindGameObjectsWithTag("Player04_Protector");
            if (Obj_Tag_Array[i] == "Player01_Attacker")
                Attacker_01 = GameObject.FindGameObjectsWithTag("Player01_Attacker");
            if (Obj_Tag_Array[i] == "Player02_Attacker")
                Attacker_02 = GameObject.FindGameObjectsWithTag("Player02_Attacker");
            if (Obj_Tag_Array[i] == "Player03_Attacker")
                Attacker_03 = GameObject.FindGameObjectsWithTag("Player03_Attacker");
            if (Obj_Tag_Array[i] == "Player04_Attacker")
                Attacker_04 = GameObject.FindGameObjectsWithTag("Player04_Attacker");
        }

        int Array_1 = Enemy_01.Length, Array_2 = Enemy_02.Length, Array_3 = Enemy_03.Length, Array_4 = Enemy_04.Length;
        int Array_5 = Protector_01.Length, Array_6 = Protector_02.Length, Array_7 = Protector_03.Length, Array_8 = Protector_04.Length;
        int Array_9 = Attacker_01.Length, Array_10 = Attacker_02.Length, Array_11 = Attacker_03.Length, Array_12 = Attacker_04.Length;

        GameObject[] All_Enemy = new GameObject[Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7 + Array_8 +
            Array_9 + Array_10 + Array_11 + Array_12];

        Enemy_01.CopyTo(All_Enemy, 0);
        Enemy_02.CopyTo(All_Enemy, Array_1);
        Enemy_03.CopyTo(All_Enemy, Array_1 + Array_2);
        Enemy_04.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3);
        Protector_01.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4);
        Protector_02.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5);
        Protector_03.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6);
        Protector_04.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7);
        Attacker_01.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7 + Array_8);
        Attacker_02.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7 + Array_8 + Array_9);
        Attacker_03.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7 + Array_8 + Array_9 + Array_10);
        Attacker_04.CopyTo(All_Enemy, Array_1 + Array_2 + Array_3 + Array_4 + Array_5 + Array_6 + Array_7 + Array_8 + Array_9 + Array_10 + Array_11);

        int number = 0;
        for (int i = 0; i < All_Enemy.Length; i++)
        {
            if (All_Enemy[i] != null)
            {
                number++;
            }
        }

        GameObject[] New_Array = new GameObject[number];
        for (int i = 0; i < New_Array.Length; i++)
        {
            if (All_Enemy[i] != null)
            {
                New_Array[i] = All_Enemy[i];
            }
        }
        return New_Array;
    }
}

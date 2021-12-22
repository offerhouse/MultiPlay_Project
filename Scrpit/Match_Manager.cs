using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_Manager : MonoBehaviour
{
//    int Match_Number = 0;
//    public List<Match_Player> Match_List = new List<Match_Player>();
//    public List<GameObject> Player_Queue_List = new List<GameObject>();
//    public List<GameObject> Match_Server_Queue_List = new List<GameObject>();
//    public List<int> Used_Port_List = new List<int>();
//    public GameObject Player_Obj_1, Player_Obj_2, Player_Obj_3, Player_Obj_4;
//    public Player_Status.Player Player1, Player2, Player3, Player4;

//    public class Match_Player
//    {
//        public string IP;
//        public int Port_Number;
//        public GameObject Player_1, Player_2, Player_3, Player_4;
//        public string PlayerID_1, PlayerID_2, PlayerID_3, PlayerID_4;

//        public Match_Player(string ip, int port_Number, GameObject player_1, GameObject player_2, GameObject player_3, GameObject player_4,
//            string playerID_1, string playerID_2, string playerID_3, string playerID_4)
//        {
//            IP = ip;
//            Port_Number = port_Number;
//            Player_1 = player_1;
//            Player_2 = player_2;
//            Player_3 = player_3;
//            Player_4 = player_4;
//            PlayerID_1 = playerID_1;
//            PlayerID_2 = playerID_2;
//            PlayerID_3 = playerID_3;
//            PlayerID_4 = playerID_4;
//        }
//    }

//    public void Add_Player_To_Queue_List(GameObject Player)
//    {
//        bool Player_In_Queue_List = Check_Player_In_Queue_List(Player);
//        if (Player_In_Queue_List)
//            Player_Queue_List.Remove(Player);

//        Set_Player_To_Match_List(Player);
//    }

//    bool Check_Player_In_Queue_List(GameObject Player)
//    {
//        bool in_List = false;
//        string Player_ID = GameObj_To_ID_String(Player);
//        string Queue_ID = null;
//        foreach (GameObject Player_Queue in Player_Queue_List)
//        {
//            if (Player_Queue != null)
//            {
//                Player_ID = GameObj_To_ID_String(Player);
//                Queue_ID = GameObj_To_ID_String(Player_Queue);
//                if (Player_ID == Queue_ID && Player_ID != null && Queue_ID != null)
//                    in_List = true;
//            }
//        }
//        return in_List;
//    }

//    public void Player_Wait_Match(GameObject Player)
//    {
//        GameObject Queue_List = Get_Match_Server_Queue_List();

//    }

//    GameObject Get_Match_Server_Queue_List()
//    {
//        GameObject List = null;
//        foreach (GameObject Queue_List in Match_Server_Queue_List)
//        {
//            if (Queue_List != null)
//            {
//                List = Queue_List;
//                return List;
//            }
//        }
//        return List;
//    }

//    public void Set_Match_Server_To_Queue_List(GameObject match_server)
//    {
//        Match_Number++;
//        Debug.Log("Set_Match_Server_To_Queue_List || " + Match_Number);
//        Match_Server_Queue_List.Add(match_server);
//    }

//    public void Set_Player_To_Match_List(GameObject Player)
//    {
//        ushort Port = 6666;
//        string Player_ID = null;
//        bool in_List = false;
//        if (Player != null)
//            Player_ID = GameObj_To_ID_String(Player);

//        if (Player_ID != null)
//            in_List = Check_Player_In_Match(Player_ID);

//        if (!in_List)
//        {
//            foreach (GameObject Match_Server_Queue in Match_Server_Queue_List)
//            {
//                if (Match_Server_Queue != null)
//                {
//                    Match_Server_Queue.GetComponent<Player_Network>().Match_Server_Start_and_Set_Player(Port, Player, null, null, null);
//                }
//            }
//        }
//        Player.GetComponent<Player_Network>().Match_Ready_Change_Server(Port, 1);
//    }

//    bool Check_Player_In_Match(string player_id)
//    {
//        bool Player_id_in_Match = false;
//        Debug.Log("Match_List.Count || " + Match_List.Count);
//        if (Match_List.Count == 0)
//            return false;
//        foreach (Match_Player match in Match_List)
//        {
//            if (Check_Obj_A_Same_as_Obj_B_ID(match.Player_1, player_id) == true)
//                return true;

//            if (Check_Obj_A_Same_as_Obj_B_ID(match.Player_2, player_id) == true)
//                return true;

//            if (Check_Obj_A_Same_as_Obj_B_ID(match.Player_3, player_id) == true)
//                return true;

//            if (Check_Obj_A_Same_as_Obj_B_ID(match.Player_4, player_id) == true)
//                return true;
//        }
//        return Player_id_in_Match;
//    }

//    Match_Player Get_Player_In_Match(string player_id)
//    {
//        Match_Player m_Match = new Match_Player(null, 0, null, null, null, null, null, null, null, null);
//        foreach (Match_Player match in Match_List)
//        {
//            if (Check_Obj_A_Same_as_Obj_B_ID(m_Match.Player_1, player_id) == true)
//                m_Match = match;

//            if (Check_Obj_A_Same_as_Obj_B_ID(m_Match.Player_2, player_id) == true)
//                m_Match = match;

//            if (Check_Obj_A_Same_as_Obj_B_ID(m_Match.Player_3, player_id) == true)
//                m_Match = match;

//            if (Check_Obj_A_Same_as_Obj_B_ID(m_Match.Player_4, player_id) == true)
//                m_Match = match;
//        }
//        return m_Match;
//    }

//    bool Check_Obj_A_Same_as_Obj_B_ID(GameObject player_Obj, string m_player_id)
//    {
//        if (player_Obj == null)
//            return false;
//        string Player_ID = GameObj_To_ID_String(player_Obj);
//        if (Player_ID == m_player_id)
//            return true;
//        return false;
//    }

//    string GameObj_To_ID_String(GameObject player_Obj)
//    {
//        GameObject online_profile = player_Obj.GetComponent<Player_Network>().My_Online_Profile;
//        return null;
//        return online_profile.GetComponent<MasterServerToolkit.MasterServer.Examples.BasicProfile.Player_Status>().player.Player_ID;
//    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    GameObject Player;
    Player_Network player_Network;

    // Start is called before the first frame update
    void Start()
    {
        Search_Player();
    }

    void Search_Player()
    {
        if (GameObject.Find("Player") != null)
        {
            Player = GameObject.Find("Player");
            player_Network = Player.GetComponent<Player_Network>();
        }
    }

    public void Set_One_VS_One()
    {
        if (Player == null)
        {
            Search_Player();
        }
        if (Player != null)
        {
            player_Network.One_VS_One();
        }
    }

    public void All_Enemy_Fear()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy_01");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Enemy_Set_Fear();
            Debug.Log("Cmd_All_Enemy_Fear");
        }
    }
}

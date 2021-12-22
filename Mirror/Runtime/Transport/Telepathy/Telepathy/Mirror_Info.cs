using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror_Info : MonoBehaviour
{
    public bool PlayerReJoinGame = false, Spawn_Scene_Object_Finish = false;

    public void Set_Player_ReJoinGame()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerReJoinGame = true;
    }

    public void Cancel_Player_ReJoinGame()
    {
        PlayerReJoinGame = false;
    }
}

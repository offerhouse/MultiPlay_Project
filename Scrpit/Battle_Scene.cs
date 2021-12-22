using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Battle_Scene : NetworkBehaviour
{
    NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartClient();
    }

    [Command] // 
    public void CmdGet_Character_Status(string player_ID)
    {
        
    }



}

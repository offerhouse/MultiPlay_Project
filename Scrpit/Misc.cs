using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Misc : NetworkBehaviour
{

    public bool Auto_Change_Scene;
    public string Scene_Name;
    public string Player_Name;
    public string User_ID = null;
    public int Conn_ID;

    // Start is called before the first frame update
    void Start()
    {
        if (Auto_Change_Scene)
            Change_Scene();
    }

    void Change_Scene()
    {
        SceneManager.LoadScene(Scene_Name);
    }

    Vector3 POS;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown("a"))
            {
                transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
                POS = transform.position;
                Cmd_update_transform_postion(POS);
            }

            if (Input.GetKeyDown("d"))
            {
                transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
                POS = transform.position;
                Cmd_update_transform_postion(POS);
            }
            if (Input.GetKeyDown("w"))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                POS = transform.position;
                Cmd_update_transform_postion(POS);
            }
            if (Input.GetKeyDown("s"))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
                POS = transform.position;
                Cmd_update_transform_postion(POS);
            }
        }

        if (isServer)
        {
            string USER_ID = GetComponent<NetworkIdentity>().User_ID;
            Rpc_Update_USER_ID(USER_ID);
        }
    }

    [ClientRpc]
    void Rpc_Update_USER_ID(string user_ID)
    {
        User_ID = user_ID;
        GetComponent<NetworkIdentity>().User_ID = user_ID;
    }

    [Command]
    void Cmd_update_transform_postion(Vector3 POS)
    {
        Debug.Log("target_transform || " + POS);
        transform.position = POS;
        Rpc_Update_POS(transform, POS);
    }

    [ClientRpc]
    void Rpc_Update_POS(Transform target_transform, Vector3 POS)
    {
        target_transform.transform.position = POS;
        Debug.Log("target_transform || " + POS);
    }

}

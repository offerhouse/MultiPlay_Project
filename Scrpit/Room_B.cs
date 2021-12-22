using UnityEngine;
using Mirror;
using MasterServerToolkit.Bridges.MirrorNetworking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Networking;
using MasterServerToolkit.Utils;
using System;

public class Room_B : BaseClientBehaviour
{
    public GameObject Text_Port;
    public GameObject NetworkManager;
    public GameObject Room_Server;
    public GameObject player;
    public event Action OnDisconnectedEvent;


    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager == null)
            Set_NetworkManager();

        if (Room_Server == null)
            Set_Room_Server();

        if (NetworkManager != null)
        {
            NetworkManager m = NetworkManager.GetComponent<NetworkManager>();
        }

        if (Room_Server != null)
            Run_Room_Server();
    }

    void Set_NetworkManager()
    {
        NetworkManager = GameObject.Find("-- MIRROR_NETWORK_MANAGER");
    }

    void Set_Room_Server()
    {
        Room_Server = GameObject.Find("-- MIRROR_ROOM_SERVER");
    }

    void Run_Room_Server()
    {
        short OP_Code = Room_Server.GetComponent<RoomServerManager>().OP_Code;
    }

    public void Test_Network_Manager()
    {
        Debug.Log("Test_Network_Manager || " + NetworkManager);
        if (NetworkManager == null)
            Set_NetworkManager();

        if (player == null)
            player = FindLocalNetworkPlayer();

        if (NetworkManager == null)
            Set_NetworkManager();

        if (NetworkManager != null)
        {
            ushort port = NetworkManager.GetComponent<TelepathyTransport>().port;
            Text_Port.GetComponent<Text>().text = port.ToString();
            Debug.Log("Room_B || " + port);
        }
    }

    GameObject FindLocalNetworkPlayer()
    {
        GameObject local_player = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            bool localplayer = player.GetComponent<NetworkIdentity>().isLocalPlayer;
            if (localplayer)
                local_player = player;
        }
        return local_player;
    }

    public void Disconnect_Room()
    {

        Destroy_Server_Obj("-- MIRROR_ROOM_CLIENT");
        Destroy_Server_Obj("-- MIRROR_ROOM_SERVER");

        NetworkManager manager = NetworkManager.GetComponent<NetworkManager>();
        manager.StopClient();

        ScenesLoader.LoadSceneByName("Client", (progressValue) =>
        {
            Mst.Events.Invoke(MstEventKeys.showLoadingInfo, $"Loading scene {Mathf.RoundToInt(progressValue * 100f)}% ... Please wait!");
        }, null);

        void Destroy_Server_Obj(string Name)
        {
            GameObject _obj = GameObject.Find(Name);
            Destroy(_obj);
        }
    }

}


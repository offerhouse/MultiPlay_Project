using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public class Auto_Server : NetworkBehaviour
    {
        public GameObject NetworkManager;
        NetworkManager manager;
        public GameObject Auto_Client;
        public bool Match_Server;

        void Start()
        {
            //NetworkManager = GameObject.Find("NetWorkManager");
            //manager = NetworkManager.GetComponent<NetworkManager>();
            //manager.StartServer();
            //Destroy(gameObject, 3f);
            //Destroy(Auto_Client);
        }
    }
}

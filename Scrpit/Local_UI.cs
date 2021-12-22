using MasterServerToolkit.Networking;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.UI;
using System.Collections.Generic;
using System;
using UnityEngine;
using MasterServerToolkit.Bridges.MirrorNetworking;

public class Local_UI : BaseClientBehaviour
{
    public GameObject Client;
    public int Room_ID;
    MasterServerToolkit.Bridges.MirrorNetworking.RoomClientManager rcm;


    protected override void Start()
    {
        Debug.Log("Local_UI");

        GameObject UI_Main = GameObject.Find("Main_UI");

        float alpha = UI_Main.GetComponent<CanvasGroup>().alpha;
        bool interactable = UI_Main.GetComponent<CanvasGroup>().interactable;
        bool blocksRaycasts = UI_Main.GetComponent<CanvasGroup>().blocksRaycasts;

        GameObject temp_client = GameObject.Find("--ROOM_CLIENT");
        if (!temp_client)
            Client.SetActive(true);

        if (temp_client)
        {
            Destroy(Client);
            Client = temp_client;
        }
        
        Debug.Log("Local_UI || " + alpha + " || " + interactable + " || " + blocksRaycasts);
    }

    public void Find_Game()
    {
        /// Local
        //Mst.Client.Connection.SendMessage((short)MstMessageCodes.Find_Game, (status, message) =>
        //{
        //    Debug.Log("message || " + message.AsString());

        //});
    }

    public void Add_Player_To_Pool()
    {
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Queue1v1);
    }

    public void Short_Cut()
    {
        GameObject Short_Cut = null;
        //Short_Cut.GetComponent<MatchmakerModule>().Check_UserName_In_Dict(1,null); // Queue1v1Handler();
        //Short_Cut.GetComponent<SpawnersModule>().MakeMatch_Handler(); // Set and open new room
        //Short_Cut.GetComponent<MatchmakingBehaviour>().StartMatch(null); // Local_Join_Game_1v1Handler(); join opend game room
        //Short_Cut.GetComponent<SpawnerController>().SpawnProcessRequestHandler(); // SpawnProcessRequestHandler
        //Short_Cut.GetComponent<SpawnerController>().SpawnRequestHandler(null,null); // Set Args

        //Short_Cut.GetComponent<MstSpawnersServer>().NotifyProcessStarted(0,0,null,null); // send messgae ProcessStarted
        //Short_Cut.GetComponent<SpawnersModule>().SetProcessStartedRequestHandler(); // while recvied message ProcessStarted will run this
        //Short_Cut.GetComponent<MstSpawnersServer>().NotifyProcessStarted(0, 0, null, null); // < error . no op code in customs option

        Spwan_Enemy_Packet packet = new Spwan_Enemy_Packet();
        Short_Cut.GetComponent<GameMaster>().Spwan_Protector(packet);
    }

    public void Check_proFile_List()
    {
        MasterServerToolkit.Examples.BasicProfile.DemoProfilesBehaviour profileManager;
        profileManager = GameObject.FindObjectOfType<MasterServerToolkit.Examples.BasicProfile.DemoProfilesBehaviour>();
        ObservableProfile Profile = profileManager.Profile;

        string ID = "60c7e8fe80ee5c875851ddf8";
    }

    private void Update()
    {

    }
}

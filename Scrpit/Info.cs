using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using MasterServerToolkit.Networking;

namespace MasterServerToolkit.MasterServer
{
    public class Info : MonoBehaviour
    {
        public GameObject Canvas;
        public int Number;
        public GameObject Base_Rock, Icon;
        public Texture Original_Image, Dim_Image, Image_01, Image_02, Image_03, Image_04, Image_05, Image_06, Image_07, Image_08;
        public string Code;
        public GameObject Target_Object;
        public GameObject Fill_QTY_Text, Fill_Bar;
        public GameObject Multi_Use_Obj; // many other object will use this . don't change .
        public GameObject Tower_Base, Tower_Icon; // for open Treasure Box 
        public GameObject Level_Text, Exp_Bar, EXP_Text, Next_Level_EXP_Text, Divider,
            Level_Up_Coins_Text, Level_Up_Coins_Shadow_Text, Level_UP_Image_Obj, Level_UP_Text, Level_UP_Shadow_Text;
        public short Tower_Code;
        AsyncOperation asyncLoad;
        public bool Fall_Enemy_Helper;
        public string Tag, Name;
        public short Enemy_Path_Code;
        public GameObject Source_Obj, Target_Obj;

        public void Tower_Select_Canvas_Slot_Button()
        {
            Canvas.GetComponent<UI_Tower_Select_Canvas>().Check_Selected_Slot(gameObject, Number);
        }

        public void Change_Desk()
        {
            Canvas.GetComponent<UI_Tower_Select_Canvas>().Change_Desk(Number);
        }

        public void UI_Battle_Change_Desk()
        {
            Canvas.GetComponent<UI_Battle_Canvas>().Change_Desk(Number);
        }

        public void UI_Shop_Button(string Code)
        {
            Shop_Code code = (Shop_Code)System.Enum.Parse(typeof(Shop_Code), Code);
            short button_code = (short)code;
            GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Click_Shop_Button(button_code);
        }

        public void UI_Confirm_Back_To_Main_Scene()
        {
            //Delete_Network_Object();
            Destroy(Multi_Use_Obj);
        }

        private void Update()
        {

        }

        public void Open_Shop_Canvas_Button(int number)
        {
            Debug.Log("Open_Shop_Canvas_Button");
            Player_Status.Player player = GameObject.Find("Local_ProFile").GetComponent<Player_Status>().player;
            int Player_Level = player.Player_Level;
            Target_Object.SetActive(true); // Target_Object = Treasure_Box Dialog canvas;
            Target_Object.GetComponent<UI_Treasure_Box>().Treasure_Box_Dialog_Setup((short)number, (short)Player_Level);
        }

        public void Open_UI_Tower_Info()
        {
            GameObject Main_UI_Object = GameObject.Find("Main_UI");
            GameObject UI_Tower_Select_Canvas_Obj = Main_UI_Object.GetComponent<UI_Main>().UI_Tower_Select_Canvas;
            UI_Tower_Select_Canvas UI = UI_Tower_Select_Canvas_Obj.GetComponent<UI_Tower_Select_Canvas>();
            int Tower_Number = Main_UI_Object.GetComponent<Tower_Available>().Tower_Code_To_Tower_Type(Tower_Code);
            UI.Open_UI_Tower_Info(Tower_Number);
        }

        public void Tower_Level_UP()
        {
            GameObject Local_ProFile = GameObject.Find("Local_ProFile");
            Player_Status.Player player = Local_ProFile.GetComponent<Player_Status>().player;
            GameObject Main_UI_Object = GameObject.Find("Main_UI");
            Tower_Info info = Main_UI_Object.GetComponent<Tower_Info>();
            Tower_Available ta = Main_UI_Object.GetComponent<Tower_Available>();
            int Tower_Number = ta.Tower_Code_To_Tower_Type(Tower_Code);
            int Tower_Level = info.Get_Tower_Level_Form_PlayerStatus(Tower_Number, player);
            int Level_UP_Require_Coins = ta.Get_Tower_Require_Gold_Level_UP(Tower_Level);

            int Gold = player.Gold;
            Debug.Log("Tower_Level_UP || Tower_Code || " + Tower_Code + " || " + Gold + " || " + Level_UP_Require_Coins);
            if (Gold < Level_UP_Require_Coins)
                return;

            RoomOptions Room_Option = new RoomOptions();
            Room_Option.OP_Code = Tower_Code;
            Mst.Client.Connection.SendMessage((short)MstMessageCodes.Tower_Level_UP, Room_Option);
        }

        public void End_Game_Canvas()
        {
            Debug.Log("End_Game_Canvas");
            GetComponent<Canvas>().sortingOrder = 1;
            DontDestroyOnLoad(gameObject);

            Quit_Game();

            //asyncLoad = SceneManager.LoadSceneAsync("00_Login_Scene");
            StartCoroutine(Check_Load_Scene_Finish());

        }

        IEnumerator Check_Load_Scene_Finish()
        {
            Debug.Log("Check_Load_Scene_Finish");
            //SceneManager.LoadScene("00_Login_Scene");

            //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("00_Login_Scene");

            Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Scene_Name || " + scene.name);
            while (scene.name != "00_Login_Scene")
            {
                scene = SceneManager.GetActiveScene();
                Debug.Log("scene.name || " + scene.name);
                yield return null;
            }

            Debug.Log("Check_Load_Scene_Finish_End || " + scene.name);

            GameObject UI_Main = GameObject.Find("Main_UI");
            Debug.Log("Active_UI_Main_2 || " + UI_Main);

            UI_Main.GetComponent<CanvasGroup>().alpha = 1;
            UI_Main.GetComponent<CanvasGroup>().interactable = true;
            UI_Main.GetComponent<CanvasGroup>().blocksRaycasts = true;

            Multi_Use_Obj.SetActive(true); // End_Game_Canvas_Confirm_Button

            GameObject pool_Manager = GameObject.Find("Pool_Manager");
            GameObject Tower_Folder = pool_Manager.GetComponent<Pool_Manager>().Tower_Folder;
            int temp_number = 0;
            foreach (Transform obj in Tower_Folder.transform)
            {
                temp_number++;
            }
            Debug.Log("temp_number || " + temp_number);
        }

        public void Quit_Game()
        {
            Debug.Log("Quit_Game_Info");
            //NetworkManager network_Manager = GameObject.Find("--NETWORK_MANAGER").GetComponent<NetworkManager>();
            //network_Manager.StopClient();

            //GameObject Client = GameObject.Find("--ROOM_CLIENT");
            //if (Client)
            //{

            //    Bridges.MirrorNetworking.RoomClientManager rcm = Client.GetComponent<Bridges.MirrorNetworking.RoomClientManager>();
            //    rcm.Local_Disconnect();
            //}

            //RoomClientManager.Disconnect();

            // NetworkClient.Shutdown(); OLD

            NetworkClient.Disconnect();
            GameObject Client = GameObject.Find("--ROOM_CLIENT");
            Destroy(Client);
            //Find_and_Destroy_GameOBject("-- MIRROR_ROOM_SERVER");
            //Find_and_Destroy_GameOBject("-- MIRROR_ROOM_CLIENT");
            //Find_and_Destroy_GameOBject("Local_ProFile");
            //RoomClientManager.Disconnect();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                Destroy(player);
            }

            void Find_and_Destroy_GameOBject(string name)
            {
                GameObject obj = GameObject.Find(name);
                if (obj != null)
                    Destroy(obj);
            }
        }

        public void Delete_Network_Object()
        {
            //Find_and_Destroy_GameOBject("-- MIRROR_NETWORK_MANAGER"); < Old mirror and Mst verion
            //Find_and_Destroy_GameOBject("-- MIRROR_ROOM_SERVER");
            //Find_and_Destroy_GameOBject("-- MIRROR_ROOM_CLIENT");
            //Find_and_Destroy_GameOBject("Local_ProFile");

            void Find_and_Destroy_GameOBject(string name)
            {
                GameObject obj = GameObject.Find(name);
                if (obj != null)
                    Destroy(obj);
            }
        }
    }
}

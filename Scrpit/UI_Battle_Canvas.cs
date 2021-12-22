using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MasterServerToolkit.MasterServer;

public class UI_Battle_Canvas : MonoBehaviour
{
    public GameObject Main_UI_Obj, UserName_Obj, Level_Obj, Exp_Obj, Gold_Obj, Diamond_Obj, Token1_Obj, Token2_Obj;
    public GameObject Desk_Icon_01_Obj, Desk_Icon_02_Obj, Desk_Icon_03_Obj, Desk_Icon_04_Obj, Desk_Icon_05_Obj,
        Desk_Rock_Obj_1, Desk_Rock_Obj_2, Desk_Rock_Obj_3, Desk_Rock_Obj_4, Desk_Rock_Obj_5;
    public UnityEvent Update_Property_Finish_Event;
    UI_Main UI_Main;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Check_UI_Main_Setup_Finish", 0.0f, 1.0f);
        if (Main_UI_Obj)
        {
            UI_Main = Main_UI_Obj.GetComponent<UI_Main>();
            UI_Main.Update_Property_Finish_Event.AddListener(Update_Property_Finish);
            //demoProfilesBehaviour.OnPropertyUpdatedEvent += UpdateProperty;
        }
    }

    void Check_UI_Main_Setup_Finish()
    {
        if (!Main_UI_Obj.GetComponent<UI_Main>().Player_Status_Setup_Finish)
            return;

        if (!Main_UI_Obj.GetComponent<UI_Main>().UI_Main_Setup_Finish)
            return;

        if (Main_UI_Obj.GetComponent<UI_Main>().UI_Main_Setup_Finish &&
            GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            Setup_Start();
    }

    public void Setup_Start()
    {
        Debug.Log("UI_Battle_Canvas || Setup_Start");
        Setup_Profile_Detail();
        CancelInvoke("Check_UI_Main_Setup_Finish");
    }

    void Update_Property_Finish()
    {
        if (!GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            return;
        Debug.Log("UI_Battle_Canvas || Update_Property_Finish");
        Setup_Profile_Detail();
    }

    void Setup_Profile_Detail()
    {
        Player_Status.Player player = null;
        GameObject local_Profile = GameObject.Find("Local_ProFile");
        if (local_Profile != null)
            player = local_Profile.GetComponent<Player_Status>().player;

        UserName_Obj.GetComponent<Text>().text = player.DisplayName;
        Level_Obj.GetComponent<Text>().text = player.Player_Level.ToString();
        Exp_Obj.GetComponent<Text>().text = player.Player_EXP.ToString();
        Gold_Obj.GetComponent<Text>().text = player.Gold.ToString();
        Diamond_Obj.GetComponent<Text>().text = player.Diamond.ToString();
        Token1_Obj.GetComponent<Text>().text = player.Token_01.ToString();
        Token2_Obj.GetComponent<Text>().text = player.Token_02.ToString();

        int current_Desk = player.Current_Desk;
        Set_Desk(current_Desk);
    }

    void Set_Desk(int Desk_Number)
    {
        GameObject local_Profile = GameObject.Find("Local_ProFile");
        int[] current_desk = local_Profile.GetComponent<Player_Status>().Get_Current_Tower_Desk(Desk_Number);
        UI_Tower_Select_Canvas canvas = Main_UI_Obj.GetComponent<UI_Main>().UI_Tower_Select_Canvas.GetComponent<UI_Tower_Select_Canvas>();

        Tower_Available ta = Main_UI_Obj.GetComponent<Tower_Available>();
        short Tower_Code_1 = ta.Tower_Type_To_Tower_Code((short)current_desk[1]);
        short Tower_Code_2 = ta.Tower_Type_To_Tower_Code((short)current_desk[2]);
        short Tower_Code_3 = ta.Tower_Type_To_Tower_Code((short)current_desk[3]);
        short Tower_Code_4 = ta.Tower_Type_To_Tower_Code((short)current_desk[4]);
        short Tower_Code_5 = ta.Tower_Type_To_Tower_Code((short)current_desk[5]);

        UI_Image_Info Image_Info = Main_UI_Obj.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();

        Desk_Icon_01_Obj.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Tower_Code_1);
        Desk_Icon_02_Obj.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Tower_Code_2);
        Desk_Icon_03_Obj.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Tower_Code_3);
        Desk_Icon_04_Obj.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Tower_Code_4);
        Desk_Icon_05_Obj.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Tower_Code_5);

        Desk_Rock_Obj_1.GetComponent<RawImage>().texture = canvas.Get_Rock_Base_Texture(canvas.Get_Base_Number(current_desk[1]));
        Desk_Rock_Obj_2.GetComponent<RawImage>().texture = canvas.Get_Rock_Base_Texture(canvas.Get_Base_Number(current_desk[2]));
        Desk_Rock_Obj_3.GetComponent<RawImage>().texture = canvas.Get_Rock_Base_Texture(canvas.Get_Base_Number(current_desk[3]));
        Desk_Rock_Obj_4.GetComponent<RawImage>().texture = canvas.Get_Rock_Base_Texture(canvas.Get_Base_Number(current_desk[4]));
        Desk_Rock_Obj_5.GetComponent<RawImage>().texture = canvas.Get_Rock_Base_Texture(canvas.Get_Base_Number(current_desk[5]));
    }

    public void Change_Desk(int Desk_Number)
    {
        GameObject local_Profile = GameObject.Find("Local_ProFile");
        local_Profile.GetComponent<Player_Status>().player.Current_Desk = Desk_Number;
        Set_Desk(Desk_Number);
    }
}

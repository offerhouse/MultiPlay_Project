using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterServerToolkit.Examples.BasicProfile;
using MasterServerToolkit.Networking;
using MasterServerToolkit.MasterServer;

public class UI_Treasure_Box : MonoBehaviour
{
    public GameObject Main_UI, Treasure_Box_Scene, Main_Camera, Treasure_Box_Scene_Camera;
    public GameObject Current_Box, Box_0, Box_1, Box_2, Box_3, Box_4, Box_5, Box_6, Box_7, Box_8, Box_9;
    public GameObject Tower_L1, Tower_L2, Tower_L3, Tower_L4, Tower_L5, Tower_L6, Tower_L7, Tower_L8;
    public GameObject Gold, Diamond, Token_1, Token_2, Token_3, Currency, Price, Treasure_Box_Icon, OK_Button, Reward_Canvas;
    short Treasure_Box_Level, Currency_Code;
    int Require_Resource;
    public List<Shop_Code_Packet> packet = new List<Shop_Code_Packet>();

    public void Treasure_Box_Dialog_Setup(short Box_Level, short Player_Level)
    {
        Main_UI.GetComponent<Canvas>().enabled = false;
        Treasure_Box_Level = Box_Level;
        Treasure_Box_Open_Canvas_Setup(Box_Level);
        GameObject Local_Profile = GameObject.Find("Local_ProFile");
        Treasure_Box_Info info = Local_Profile.GetComponent<Treasure_Box_Info>();
        List<Treasure_Box_Dialog_Packet> List = info.Get_Treasure_Box_Info(Box_Level);
        Treasure_Box_Dialog_Packet packet_Tower_1 = List[0];
        Treasure_Box_Dialog_Packet packet_Tower_2 = List[1];
        Treasure_Box_Dialog_Packet packet_Tower_3 = List[2];
        Treasure_Box_Dialog_Packet packet_Tower_4 = List[3];
        Treasure_Box_Dialog_Packet packet_Tower_5 = List[4];
        Treasure_Box_Dialog_Packet packet_Tower_6 = List[5];
        Treasure_Box_Dialog_Packet packet_Tower_7 = List[6];
        Treasure_Box_Dialog_Packet packet_Gold = List[7];
        Treasure_Box_Dialog_Packet packet_Diamond = List[8];
        Treasure_Box_Dialog_Packet packet_Token_1 = List[9];
        Treasure_Box_Dialog_Packet packet_Token_2 = List[10];

        Set_Dialog_Detail(Tower_L1, packet_Tower_1);
        Set_Dialog_Detail(Tower_L2, packet_Tower_2);
        Set_Dialog_Detail(Tower_L3, packet_Tower_3);
        Set_Dialog_Detail(Tower_L4, packet_Tower_4);
        Set_Dialog_Detail(Tower_L5, packet_Tower_5);
        Set_Dialog_Detail(Tower_L6, packet_Tower_6);
        Set_Dialog_Detail(Tower_L7, packet_Tower_7);
        Set_Dialog_Detail(Gold, packet_Gold);
        Set_Dialog_Detail(Diamond, packet_Diamond);
        Set_Dialog_Detail(Token_1, packet_Token_1);
        Set_Dialog_Detail(Token_2, packet_Token_2);

        info.Set_Current_Box_Currency_and_Price(Treasure_Box_Level, out short currency_Code, out int Resource);
        Currency_Code = currency_Code;
        Require_Resource = Resource;
        Set_Texture();
        bool Enought_Resource = Check_Enough_Resource_To_Open_Box();
        Debug.Log("Currency_Code || " + Currency_Code + " || " + Require_Resource + " || " + Enought_Resource);
        OK_Button.GetComponent<Button>().interactable = Enought_Resource;

        void Set_Dialog_Detail(GameObject obj, Treasure_Box_Dialog_Packet packet)
        {
            obj.SetActive(packet.Active);
            if (!packet.Active)
                return;

            GameObject QTY_Text = obj.transform.Find("QTY_Text").gameObject;
            if (packet.Shop_Code_Sell_or_Lucky_Draw == (short)Shop_Code.Direct_Buy)
            {
                if (packet.Shop_Code_Item_Type == (short)Shop_Code.Gold)
                {
                    int Level_QTY = Player_Level * packet.QTY;
                    QTY_Text.GetComponent<Text>().text = " X " + Level_QTY.ToString();
                }

                if (packet.Shop_Code_Item_Type != (short)Shop_Code.Gold)
                    QTY_Text.GetComponent<Text>().text = " X " + packet.QTY.ToString();
            }

            if (packet.Shop_Code_Sell_or_Lucky_Draw == (short)Shop_Code.Lucky_Draw)
            {
                QTY_Text.GetComponent<Text>().text = " % " + packet.Lucky_Draw_Chance.ToString();
            }
        }
    }

    void Set_Texture()
    {
        UI_Image_Info image_info = Main_UI.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
        Texture currency_texture = image_info.Get_Texture_By_Code(Currency_Code);
        Texture treasure_texture = image_info.Get_Treasure_Box_Texture(Treasure_Box_Level);
        Currency.GetComponent<RawImage>().texture = currency_texture;
        Treasure_Box_Icon.GetComponent<RawImage>().texture = treasure_texture;
        Price.GetComponent<Text>().text = Require_Resource.ToString();
    }

    public void Treasure_Box_Open_Canvas_Setup(short Box_Level)
    {
        switch (Box_Level)
        {
            case (1): Current_Box = Box_1; break;
            case (2): Current_Box = Box_2; break;
            case (3): Current_Box = Box_3; break;
            case (4): Current_Box = Box_4; break;
            case (5): Current_Box = Box_5; break;
            case (6): Current_Box = Box_6; break;
            case (7): Current_Box = Box_7; break;
            case (8): Current_Box = Box_8; break;
            case (9): Current_Box = Box_9; break;
        }
    }

    public void Drop_Treasure_Box_Animation()
    {
        Current_Box.SetActive(true);
    }

    bool Check_Enough_Resource_To_Open_Box()
    {
        GameObject Local_Profile = GameObject.Find("Local_ProFile");
        Player_Status.Player player = Local_Profile.GetComponent<Player_Status>().player;
        int QTY = 0;
        if (Currency_Code == (short)Shop_Code.Diamond)
            QTY = player.Diamond;

        if (Currency_Code == (short)Shop_Code.Token_01)
            QTY = player.Token_01;

        if (Currency_Code == (short)Shop_Code.Token_02)
            QTY = player.Token_02;

        if (Currency_Code == (short)Shop_Code.Token_03)
            QTY = player.Token_03;

        if (QTY >= Require_Resource)
            return true;
        return false;
    }

    public void Open_Treasure_Box()
    {
        Debug.Log("Open_Treasure_Box || " + Treasure_Box_Level);
        GameObject Local_Profile = GameObject.Find("Local_ProFile");
        Treasure_Box_Info info = Local_Profile.GetComponent<Treasure_Box_Info>();
        info.Set_Current_Box_Currency_and_Price(Treasure_Box_Level, out short currency_Code, out int Resource);
        Currency_Code = currency_Code;
        Require_Resource = Resource;

        bool Enought_Resource = Check_Enough_Resource_To_Open_Box();
        if (!Enought_Resource)
            return;
        RoomOptions Room_Option = new RoomOptions();
        Room_Option.OP_Code = Treasure_Box_Level;
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Open_Treasure, Room_Option, Open_Treasure_Callback);
    }

    public void Cancel_Treasure_Box()
    {
        GetComponent<Animator>().SetTrigger("Treasure_Box_Dialog_Close");
    }

    public void Close_Dialog_And_Active_Treasure_Scene()
    {
        Treasure_Box_Scene.SetActive(true);
        Main_Camera.SetActive(false);
        Treasure_Box_Scene_Camera.SetActive(true);
    }

    void Open_Treasure_Callback(ResponseStatus status, IIncomingMessage response)
    {
        Debug.Log("Open_Treasure_Callback");
        List<Shop_Code_Packet> Reward = new List<Shop_Code_Packet>();

        // Set IncomingMessage to Packet
        if (status == ResponseStatus.Success)
        {
            RoomOptions room = response.Deserialize(new RoomOptions());

            Dictionary<string, string> room_dict = room.CustomOptions.ToDictionary();
            foreach (KeyValuePair<string, string> item in room_dict)
            {
                Shop_Code_Packet packet = new Shop_Code_Packet();
                int.TryParse(item.Key, out int m_Shop_Code);
                int.TryParse(item.Value, out int QTY);
                packet.Shop_Code = (short)m_Shop_Code;
                packet.QTY = QTY;
                Reward.Add(packet);
            }

            Reward_Canvas.SetActive(true);
            Reward_Canvas.GetComponent<UI_Reward_Canvas>().Setup_Reward_Canvas(Reward);
            Reward_Canvas.GetComponent<UI_Reward_Canvas>().Treasure_Box_Scene_Camera = Treasure_Box_Scene_Camera;
            Reward_Canvas.GetComponent<UI_Reward_Canvas>().Main_Camera = Main_Camera;
            Reward_Canvas.SetActive(false);

            GetComponent<Animator>().SetTrigger("Treasure_Box_Dialog_Close");
            Treasure_Box_Scene.SetActive(true);
            Main_Camera.SetActive(false);
            Treasure_Box_Scene_Camera.SetActive(true);
        }
    }

    public void Treasure_Box_Open_Animation_Finish()
    {
        Reward_Canvas.GetComponent<UI_Reward_Canvas>().BG_Image.SetActive(true);
        Reward_Canvas.GetComponent<UI_Reward_Canvas>().Item.SetActive(true);
        Reward_Canvas.GetComponent<Animator>().SetTrigger("Open");

        Vector3 POS = Current_Box.transform.position;
        Current_Box.transform.position = POS + new Vector3(0, 20, 0);
        Current_Box.SetActive(false);
        gameObject.SetActive(false);
    }
}

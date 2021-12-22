using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterServerToolkit.Networking;
using MasterServerToolkit.MasterServer;
using UnityEngine.UI;

public class UI_Reward_Canvas : MonoBehaviour
{
    public GameObject Main_UI, Treasure_Scene, Treasure_Box_Dialog, BG_Image, Item, Treasure_Box_Scene_Camera, Main_Camera;
    public GameObject Tower_L1, Tower_L2, Tower_L3, Tower_L4, Tower_L5, Tower_L6, Tower_L7, Tower_L8;
    public GameObject Gold, Diamond, Token_1, Token_2, Token_3, OK_Button, Confetti_Effect;
    public List<Shop_Code_Packet> Reward = new List<Shop_Code_Packet>();

    public void Setup_Reward_Canvas(List<Shop_Code_Packet> reward)
    {
        Reward = reward;
    }

    public void Start_Reward_Canvas()
    {
        Confetti_Effect.SetActive(true);
        for (short i = 0; i < Reward.Count; i++)
        {
            Shop_Code_Packet packet = Reward[i];
            short shop_code = packet.Shop_Code;
            int QTY = packet.QTY;
            bool is_Tower = false;

            Tower_Available ta = Main_UI.GetComponent<Tower_Available>();
            short tower_Code = ta.Tower_Code_To_Tower_Type(shop_code); short Tower_Level = 0;
            if (tower_Code > 0)
                is_Tower = true;

            if (is_Tower)
            {
                string temp_Text = tower_Code.ToString().Substring(0, 1);
                short.TryParse(temp_Text, out Tower_Level);
            }

            if (shop_code == (short)Shop_Code.Gold)
                Set_Texture_and_Text_To_Obj(Gold, shop_code, QTY, is_Tower, Tower_Level);

            if (shop_code == (short)Shop_Code.Diamond)
                Set_Texture_and_Text_To_Obj(Diamond, shop_code, QTY, is_Tower, Tower_Level);

            if (shop_code == (short)Shop_Code.Token_01)
                Set_Texture_and_Text_To_Obj(Token_1, shop_code, QTY, is_Tower, Tower_Level);

            if (shop_code == (short)Shop_Code.Token_02)
                Set_Texture_and_Text_To_Obj(Token_2, shop_code, QTY, is_Tower, Tower_Level);

            if (shop_code == (short)Shop_Code.Token_03)
                Set_Texture_and_Text_To_Obj(Token_3, shop_code, QTY, is_Tower, Tower_Level);

            if (is_Tower)
                Set_Texture_and_Text_To_Obj(Get_Tower_Button(i), shop_code, QTY, is_Tower, Tower_Level);
        }
    }

    GameObject Get_Tower_Button(short number)
    {
        number += 1; // because List Start from 0
        //Debug.Log("Get_Tower_Button || " + number);
        switch (number)
        {
            case (1): return Tower_L1;
            case (2): return Tower_L2;
            case (3): return Tower_L3;
            case (4): return Tower_L4;
            case (5): return Tower_L5;
            case (6): return Tower_L6;
            case (7): return Tower_L7;
            case (8): return Tower_L8;
        }
        return null;
    }

    void Set_Texture_and_Text_To_Obj(GameObject Obj, short shop_Code, int QTY, bool is_Tower, short Tower_Level)
    {
        Obj.SetActive(true);
        UI_Image_Info image_info = Main_UI.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
        if (is_Tower)
        {
            GameObject Rock_Icon_Obj = Obj.transform.Find("Icon").gameObject;
            Texture Rock_Base_Texture = image_info.Get_Rock_Base_Texture(Tower_Level);
            Texture Rock_Icon_Texture = image_info.Get_Texture_By_Code(shop_Code);

            Obj.GetComponent<RawImage>().texture = Rock_Base_Texture;
            Rock_Icon_Obj.GetComponent<RawImage>().texture = Rock_Icon_Texture;
        }
        GameObject Tower_QTY = Obj.transform.Find("QTY_Text").gameObject;
        Tower_QTY.GetComponent<Text>().text = QTY.ToString();
    }

    void Reset_Reward_canvas()
    {
        Confetti_Effect.SetActive(false);
        for (short i = 1; i < 9; i++)
        {
            GameObject _Obj = Get_Tower_Button(i);
            if (_Obj)
            {
                _Obj.GetComponent<RawImage>().texture = null;
                GameObject Rock_Icon_Obj = _Obj.transform.Find("Icon").gameObject;
                Rock_Icon_Obj.GetComponent<RawImage>().texture = null;
                Deactive_Obj_and_Clear_Text(_Obj);
            }
        }

        Deactive_Obj_and_Clear_Text(Gold);
        Deactive_Obj_and_Clear_Text(Diamond);
        Deactive_Obj_and_Clear_Text(Token_1);
        Deactive_Obj_and_Clear_Text(Token_2);
        Deactive_Obj_and_Clear_Text(Token_3);

        void Deactive_Obj_and_Clear_Text(GameObject _Obj)
        {
            if (_Obj)
            {
                GameObject Tower_QTY = _Obj.transform.Find("QTY_Text").gameObject;
                Tower_QTY.GetComponent<Text>().text = null;
                _Obj.SetActive(false);
            }
        }
    }

    public void Close_Reward_Canvas()
    {
        Debug.Log("Close_Reward_Canvas");
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void Finish_Reward()
    {
        Debug.Log("Close_Reward_Canvas");
        Confetti_Effect.SetActive(false);
        Treasure_Scene.SetActive(false);
        Treasure_Box_Dialog.SetActive(false);
        Reset_Reward_canvas();
        Treasure_Box_Scene_Camera.SetActive(false);
        Main_Camera.SetActive(true);
        Main_UI.GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(false);
    }

}

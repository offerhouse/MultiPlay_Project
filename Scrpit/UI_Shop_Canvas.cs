using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MasterServerToolkit.MasterServer;

public class UI_Shop_Canvas : MonoBehaviour
{
    public bool Client;
    public Texture Gold, Diamond, Token_1, Token_2;
    UI_Main UI_Main;
    UI_Image_Info Image_Info;
    public GameObject Main_UI_Object;
    public GameObject Current_Canvas;

    public UnityEvent Update_Property_Finish_Event;

    public GameObject Special_Sell, InGame_Sell_1, InGame_Sell_2, InGame_Sell_3, InGame_Sell_4, InGame_Sell_5, InGame_Sell_6,
        InGame_Sell_7, InGame_Sell_8, InGame_Currency_1, InGame_Currency_2, InGame_Currency_3, InGame_Currency_4, InGame_Currency_5,
        InGame_Currency_6, InGame_Currency_7, InGame_Currency_8,
        InGame_Bor_1, InGame_Bor_2, InGame_Bor_3, InGame_Bor_4, InGame_Bor_5, InGame_Bor_6, InGame_Bor_7, InGame_Bor_8,
        Sell_Gold_1, Sell_Gold_2, Sell_Gold_3, Sell_Diamond_1, Sell_Diamond_2, Sell_Diamond_3,
        Sell_Token1_1, Sell_Token1_2, Sell_Token1_3,
        Sell_Token2_1, Sell_Token2_2, Sell_Token2_3, Sell_Chest_1, Sell_Chest_2, Sell_Chest_3,

        InGame_Rock_Base_1, InGame_Rock_Base_2, InGame_Rock_Base_3, InGame_Rock_Base_4, InGame_Rock_Base_5, InGame_Rock_Base_6,
        InGame_Rock_Base_7, InGame_Rock_Base_8,
        InGame_Sell_1_Text, InGame_Sell_2_Text, InGame_Sell_3_Text, InGame_Sell_4_Text, InGame_Sell_5_Text, InGame_Sell_6_Text,
        InGame_Sell_7_Text, InGame_Sell_8_Text,
        InGame_Price_1_Text, InGame_Price_2_Text, InGame_Price_3_Text, InGame_Price_4_Text, InGame_Price_5_Text, InGame_Price_6_Text,
        InGame_Price_7_Text, InGame_Price_8_Text,

        Ex_Border1, Ex_Border2, Ex_Border3, Ex_Border4, Ex_Border5, Ex_Border6,
        Ex_Icon1, Ex_Icon2, Ex_Icon3, Ex_Icon4, Ex_Icon5, Ex_Icon6,
        Ex_QTY1, Ex_QTY2, Ex_QTY3, Ex_QTY4, Ex_QTY5, Ex_QTY6,
        Ex_Price1, Ex_Price2, Ex_Price3, Ex_Price4, Ex_Price5, Ex_Price6,
        Ex_Currency1, Ex_Currency2, Ex_Currency3, Ex_Currency4, Ex_Currency5, Ex_Currency6,

        Sell_Gold_1_Text, Sell_Gold_2_Text, Sell_Gold_3_Text,
        Sell_Diamond_1_Text, Sell_Diamond_2_Text, Sell_Diamond_3_Text,
        Sell_Token1_1_Text, Sell_Token1_2_Text, Sell_Token1_3_Text,
        Sell_Token2_1_Text, Sell_Token2_2_Text, Sell_Token2_3_Text,
        Sell_Chest_1_Text, Sell_Chest_2_Text, Sell_Chest_3_Text;

    public GameObject Test_Text;

    // Start is called before the first frame update
    void Start()
    {
        if (Client)
            InvokeRepeating("Check_UI_Main_Setup_Finish", 0.0f, 1.0f);
        InvokeRepeating("Check_and_Refresh_Shop", 0.0f, 1.0f);
        if (Main_UI_Object)
        {
            UI_Main = Main_UI_Object.GetComponent<UI_Main>();
            UI_Main.Update_Property_Finish_Event.AddListener(Update_Property_Finish);
            Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
            //demoProfilesBehaviour.OnPropertyUpdatedEvent += UpdateProperty;
        }
    }

    void Check_UI_Main_Setup_Finish()
    {
        if (!Main_UI_Object.GetComponent<UI_Main>().Player_Status_Setup_Finish)
            return;

        if (!Main_UI_Object.GetComponent<UI_Main>().UI_Main_Setup_Finish)
            return;

        if (Main_UI_Object.GetComponent<UI_Main>().UI_Main_Setup_Finish &&
                GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            Setup_Start();
    }

    public void Setup_Start()
    {
        Debug.Log("UI_Shop_Canvas || Setup_Start");
        #region InGame_Sell
        Player_Status.Player player = GameObject.Find("Local_ProFile").GetComponent<Player_Status>().player;
        int[] code = new int[4]; GameObject[] Obj = new GameObject[6];
        code = new int[] { player.InGame_Sell_1, player.InGame_QTY_1, player.InGame_Currency_1, player.InGame_Price_1, player.InGame_Sold_1 };
        Obj = new GameObject[] { InGame_Sell_1, InGame_Sell_1_Text, InGame_Currency_1, InGame_Price_1_Text, InGame_Bor_1, InGame_Rock_Base_1 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_2, player.InGame_QTY_2, player.InGame_Currency_2, player.InGame_Price_2, player.InGame_Sold_2 };
        Obj = new GameObject[] { InGame_Sell_2, InGame_Sell_2_Text, InGame_Currency_2, InGame_Price_2_Text, InGame_Bor_2, InGame_Rock_Base_2 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_3, player.InGame_QTY_3, player.InGame_Currency_3, player.InGame_Price_3, player.InGame_Sold_3 };
        Obj = new GameObject[] { InGame_Sell_3, InGame_Sell_3_Text, InGame_Currency_3, InGame_Price_3_Text, InGame_Bor_3, InGame_Rock_Base_3 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_4, player.InGame_QTY_4, player.InGame_Currency_4, player.InGame_Price_4, player.InGame_Sold_4 };
        Obj = new GameObject[] { InGame_Sell_4, InGame_Sell_4_Text, InGame_Currency_4, InGame_Price_4_Text, InGame_Bor_4, InGame_Rock_Base_4 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_5, player.InGame_QTY_5, player.InGame_Currency_5, player.InGame_Price_5, player.InGame_Sold_5 };
        Obj = new GameObject[] { InGame_Sell_5, InGame_Sell_5_Text, InGame_Currency_5, InGame_Price_5_Text, InGame_Bor_5, InGame_Rock_Base_5 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_6, player.InGame_QTY_6, player.InGame_Currency_6, player.InGame_Price_6, player.InGame_Sold_6 };
        Obj = new GameObject[] { InGame_Sell_6, InGame_Sell_6_Text, InGame_Currency_6, InGame_Price_6_Text, InGame_Bor_6, InGame_Rock_Base_6 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_7, player.InGame_QTY_7, player.InGame_Currency_7, player.InGame_Price_7, player.InGame_Sold_7 };
        Obj = new GameObject[] { InGame_Sell_7, InGame_Sell_7_Text, InGame_Currency_7, InGame_Price_7_Text, InGame_Bor_7, InGame_Rock_Base_7 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        code = new int[] { player.InGame_Sell_8, player.InGame_QTY_8, player.InGame_Currency_8, player.InGame_Price_8, player.InGame_Sold_8 };
        Obj = new GameObject[] { InGame_Sell_8, InGame_Sell_8_Text, InGame_Currency_8, InGame_Price_8_Text, InGame_Bor_8, InGame_Rock_Base_8 };
        Set_Detail_To_UI_InGame_Sell(code, Obj);

        void Set_Detail_To_UI_InGame_Sell(int[] m_code, GameObject[] m_obj)
        {
            //Debug.Log("Set_Detail_To_UI_InGame_Sell");
            if (!Image_Info)
                Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();

            short Shop_Code = 0; short QTY = 0; short Price = 0; short Currency = 0; short SoldOut = 0; short Level_Number;
            Shop_Code = (short)m_code[0];
            QTY = (short)m_code[1];
            Currency = (short)m_code[2];
            Price = (short)m_code[3];
            SoldOut = (short)m_code[4];

            //Debug.Log("m_code || " + m_code[0] + " || " + m_code[1] + " || " + m_code[2] + " || " + m_code[3] + " || " + m_code[4]);

            GameObject shop_info_obj = Main_UI_Object.GetComponent<UI_Main>().Shop_Info;
            //Debug.Log("shop_info_obj || activeSelf || " + shop_info_obj.activeSelf);
            Shop_Info shop_info = shop_info_obj.GetComponent<Shop_Info>();

            string string_Code = shop_info.ShopCode_To_String(Shop_Code);
            if (string_Code.Length > 4)
                string_Code = string_Code.Substring(0, 5);

            bool Tower = false;
            if (string_Code == "Tower")
                Tower = true;

            Level_Number = Get_Level_By_Code(Shop_Code, QTY);
            if (SoldOut > 0) // Sold_Out , Dim
            {
                if (Tower)
                    m_obj[5].SetActive(true);

                m_obj[0].GetComponent<RawImage>().texture = Image_Info.Get_Dim_Texture_By_Code(Shop_Code);
                m_obj[4].GetComponent<RawImage>().texture = Image_Info.Get_InGame_Sell_Border_Texture(0); // 0 is dim
                m_obj[5].GetComponent<RawImage>().texture = Image_Info.Get_Rock_Dim_Texture(Level_Number);
            }

            if (SoldOut == 0)
            {
                if (!Tower)
                    m_obj[5].SetActive(false);

                m_obj[0].GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Shop_Code);
                m_obj[4].GetComponent<RawImage>().texture = Image_Info.Get_InGame_Sell_Border_Texture(Level_Number);
                m_obj[5].GetComponent<RawImage>().texture = Image_Info.Get_Rock_Base_Texture(Level_Number);
            }

            m_obj[1].GetComponent<Text>().text = QTY.ToString();

            if (Currency == 0) // Free
                m_obj[2].SetActive(false);

            if (Currency > 0) // Not Free
            {
                m_obj[2].SetActive(true);
                if (SoldOut > 0) // Sold out
                    m_obj[2].GetComponent<RawImage>().texture = Image_Info.Get_Dim_Small_Currency_Texture(Currency);

                if (SoldOut == 0)
                    m_obj[2].GetComponent<RawImage>().texture = Image_Info.Get_Small_Currency_Texture(Currency);
            }
            if (Price == 0)
                m_obj[3].SetActive(false);
            if (Price > 0)
            {
                m_obj[3].SetActive(true);
                m_obj[3].GetComponent<Text>().text = Price.ToString();
            }
        }
        #endregion

        Debug.Log("player.Exchange_1 || " + player.Exchange_1);

        #region Exchange_Shop
        code = new int[] { player.Exchange_1, player.Exchange_QTY_1, player.Exchange_Currency_1, player.Exchange_Price_1, player.Exchange_Sold_1 };
        Obj = new GameObject[] { Ex_Border1, Ex_Icon1, Ex_QTY1, Ex_Price1, Ex_Currency1 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);
        code = new int[] { player.Exchange_2, player.Exchange_QTY_2, player.Exchange_Currency_2, player.Exchange_Price_2, player.Exchange_Sold_2 };
        Obj = new GameObject[] { Ex_Border2, Ex_Icon2, Ex_QTY2, Ex_Price2, Ex_Currency2 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);
        code = new int[] { player.Exchange_3, player.Exchange_QTY_3, player.Exchange_Currency_3, player.Exchange_Price_3, player.Exchange_Sold_3 };
        Obj = new GameObject[] { Ex_Border3, Ex_Icon3, Ex_QTY3, Ex_Price3, Ex_Currency3 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);
        code = new int[] { player.Exchange_4, player.Exchange_QTY_4, player.Exchange_Currency_4, player.Exchange_Price_4, player.Exchange_Sold_4 };
        Obj = new GameObject[] { Ex_Border4, Ex_Icon4, Ex_QTY4, Ex_Price4, Ex_Currency4 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);
        code = new int[] { player.Exchange_5, player.Exchange_QTY_5, player.Exchange_Currency_5, player.Exchange_Price_5, player.Exchange_Sold_5 };
        Obj = new GameObject[] { Ex_Border5, Ex_Icon5, Ex_QTY5, Ex_Price5, Ex_Currency5 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);
        code = new int[] { player.Exchange_6, player.Exchange_QTY_6, player.Exchange_Currency_6, player.Exchange_Price_6, player.Exchange_Sold_6 };
        Obj = new GameObject[] { Ex_Border6, Ex_Icon6, Ex_QTY6, Ex_Price6, Ex_Currency6 };
        Set_Detail_To_UI_Exchange_Shop(code, Obj);

        void Set_Detail_To_UI_Exchange_Shop(int[] m_code, GameObject[] m_obj)
        {
            if (!Image_Info)
                Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();

            short Shop_Code = 0; int QTY = 0; int Price = 0; short Currency = 0; short SoldOut = 0; short Level_Number = 1;
            Shop_Code = (short)m_code[0];
            QTY = m_code[1];
            Currency = (short)m_code[2];
            Price = m_code[3];
            SoldOut = (short)m_code[4];
            Level_Number = Get_Exchange_Level_By_Code(Shop_Code, (short)QTY);

            GameObject obj_Border = null, obj_Icon = null, obj_QTY = null, obj_Price = null, obj_Currency = null;
            obj_Border = m_obj[0];
            obj_Icon = m_obj[1];
            obj_QTY = m_obj[2];
            obj_Price = m_obj[3];
            obj_Currency = m_obj[4];
            Shop_Info shop_info = UI_Main.GetComponent<UI_Main>().Shop_Info.GetComponent<Shop_Info>();

            //Debug.Log("Shop_Code || " + Shop_Code + " || Price || " + Price + " || " + Level_Number);
            if (SoldOut > 0) // Sold_Out , Dim
            {
                obj_Icon.GetComponent<RawImage>().texture = Image_Info.Get_Dim_Texture_By_Code(Shop_Code);
                obj_Border.GetComponent<RawImage>().texture = Image_Info.Get_Exchange_Sell_Border_Texture(0); // 0 is dim
                obj_Currency.GetComponent<RawImage>().texture = Image_Info.Get_Dim_Small_Currency_Texture(Currency);
            }

            if (SoldOut == 0)
            {
                obj_Icon.GetComponent<RawImage>().texture = Image_Info.Get_Texture_By_Code(Shop_Code);
                obj_Border.GetComponent<RawImage>().texture = Image_Info.Get_Exchange_Sell_Border_Texture(Level_Number);
                obj_Currency.GetComponent<RawImage>().texture = Image_Info.Get_Small_Currency_Texture(Currency);
            }

            obj_QTY.GetComponent<Text>().text = QTY.ToString();
            obj_Price.GetComponent<Text>().text = Price.ToString();
        }
        #endregion

        CancelInvoke("Check_UI_Main_Setup_Finish");
    }

    void Update_Property_Finish()
    {
        if (!GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            return;
        //Debug.Log("UI_Shop_Canvas || Update_Property_Finish");
    }

    short Get_Exchange_Level_By_Code(short Code_Number, short QTY)
    {
        short Level_Number = 1;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold):
                if (QTY < 5000) return Level_Number = 1;
                if (QTY > 5001 && QTY < 20001) return Level_Number = 2;
                if (QTY > 20000) return Level_Number = 3;
                break;
            case ((short)Shop_Code.Diamond):
                if (QTY < 31) return Level_Number = 2;
                if (QTY > 30 && QTY < 51) return Level_Number = 3;
                if (QTY > 50 && QTY < 101) return Level_Number = 4;
                if (QTY > 100 && QTY < 151) return Level_Number = 5;
                if (QTY > 150 && QTY < 200) return Level_Number = 6;
                break;
            case ((short)Shop_Code.Token_01):
                if (QTY == 1) return Level_Number = 3;
                if (QTY > 2 && QTY < 6) return Level_Number = 4;
                if (QTY > 5 && QTY < 11) return Level_Number = 5;
                if (QTY > 10) return Level_Number = 6;
                break;
            case ((short)Shop_Code.Token_02):
                if (QTY == 1) return Level_Number = 5;
                if (QTY > 1 && QTY < 11) return Level_Number = 6;
                if (QTY > 10) return Level_Number = 7;
                break;
        }
        return Level_Number;
    }

    short Get_Level_By_Code(short Code_Number, short QTY)
    {
        short Level_Number = 1;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold):
                if (QTY < 2000) return Level_Number = 1;
                if (QTY > 2000) return Level_Number = 2;
                break;
            case ((short)Shop_Code.Diamond):
                if (QTY < 29) return Level_Number = 2;
                if (QTY > 30) return Level_Number = 3;
                break;
            case ((short)Shop_Code.Token_01):
                if (QTY < 2) return Level_Number = 3;
                if (QTY == 2) return Level_Number = 4;
                if (QTY > 2) return Level_Number = 5;
                break;
            case ((short)Shop_Code.Token_02):
                if (QTY == 1) return Level_Number = 7;
                if (QTY > 1) return Level_Number = 8;
                break;
            case ((short)Shop_Code.Token_03): return Level_Number = 1;
            case ((short)Shop_Code.Token_04): return Level_Number = 1;
            case ((short)Shop_Code.Token_05): return Level_Number = 1;
            case ((short)Shop_Code.Token_06): return Level_Number = 1;
            case ((short)Shop_Code.Token_07): return Level_Number = 1;
            case ((short)Shop_Code.Token_08): return Level_Number = 1;
            case ((short)Shop_Code.Token_09): return Level_Number = 1;
            case ((short)Shop_Code.Token_10): return Level_Number = 1;
            case ((short)Shop_Code.Chest_1): return Level_Number = 1;
            case ((short)Shop_Code.Chest_2): return Level_Number = 1;
            case ((short)Shop_Code.Chest_3): return Level_Number = 1;
            case ((short)Shop_Code.Chest_4): return Level_Number = 2;
            case ((short)Shop_Code.Chest_5): return Level_Number = 3;
            case ((short)Shop_Code.Chest_6): return Level_Number = 4;
            case ((short)Shop_Code.Chest_7): return Level_Number = 5;
            case ((short)Shop_Code.Chest_8): return Level_Number = 6;
            case ((short)Shop_Code.Chest_9): return Level_Number = 7;
            case ((short)Shop_Code.Chest_10): return Level_Number = 8;
        }

        if (!UI_Main)
            UI_Main = Main_UI_Object.GetComponent<UI_Main>();

        Shop_Info shop_info = UI_Main.Shop_Info.GetComponent<Shop_Info>();

        string string_Code = shop_info.ShopCode_To_String(Code_Number);
        string Temp_Code = string_Code;
        if (string_Code.Length >= 5)
            string_Code = string_Code.Substring(0, 5);

        if (string_Code == "Tower")
        {
            string_Code = Temp_Code.Substring(6, 1);
            Level_Number = short.Parse(string_Code);
        }
        return Level_Number;
    }

    void Check_and_Refresh_Shop()
    {
        if (!Main_UI_Object.GetComponent<UI_Main>().UI_Main_Setup_Finish)
            return;

        Player_Status player_status = GameObject.Find("Local_ProFile").GetComponent<Player_Status>();
        if (!player_status.Player_Status_Load_Finish)
            return;

        Player_Status.Player player = GameObject.Find("Local_ProFile").GetComponent<Player_Status>().player;

        int Last_Refresh_Sell_Time = player.Last_Refresh_Sell_Time;
        int Last_Refresh_Exchange_Time = player.Last_Refresh_Exchange_Time;

        GameObject Shop_Info_Obj = UI_Main.GetComponent<UI_Main>().Shop_Info;
        Shop_Info shop_info = Shop_Info_Obj.GetComponent<Shop_Info>();

        System.DateTime last_Refresh_Sell_Time = shop_info.Convert_Int_to_DateTime(Last_Refresh_Sell_Time);
        System.DateTime last_Refresh_Exchange_Time = shop_info.Convert_Int_to_DateTime(Last_Refresh_Exchange_Time);

        System.DateTime Next_Update = last_Refresh_Exchange_Time.AddHours(2);

        System.TimeSpan diff = Next_Update - System.DateTime.UtcNow;

        Test_Text.GetComponent<Text>().text = "0" + diff.Hours.ToString() + " : " + diff.Minutes.ToString() + " : " + diff.Seconds.ToString();
        if (System.DateTime.UtcNow > Next_Update) //////
            player_status.Refresh_Exchange_Shop();

        if (System.DateTime.UtcNow.Day != last_Refresh_Sell_Time.Day)
        {
            Shop_Code code = (Shop_Code)System.Enum.Parse(typeof(Shop_Code), "Refresh_InGame_Sell_Button");
            short button_code = (short)code;
            GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Click_Shop_Button(button_code);
        }
    }
}

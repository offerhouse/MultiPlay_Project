using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MasterServerToolkit.MasterServer;

public class UI_Tower_Info : MonoBehaviour
{
    public int Tower_Number;
    public GameObject Main_Canvas;
    public GameObject Local_Profile, Level, Exp, ATK, Speed, Distance, Add_Core, Special_Effect, Level_Up, Combine_Up, Power_UP,
        Icon_Image, Rock_Image, UI_Image_Info, UI_Tower_Select_Canvas, EXP_Bar;

    public GameObject Combine_Up_Button, Combine_Down_Button, Power_Up_Button, Power_Down_Button, Core_HP_Up, Core_HP_Down;
    public GameObject Level_Up_Text, Core_HP_Text, Combine_Up_Text, Power_Up_Text, Level_UP_Allow_Button, Level_UP_Not_Allow_Button;

    Tower_Info tower_info;
    Player_Status.Player player;
    int Tower_Level = 1, Core_HP_Value = 100, tower_level_point = 0, combine_up_point = 1, power_up_point = 1;
    float Basic_Damage, ATK_Value, Speed_Value, Dis_Value, Add_Core_HP_Value;

    public void Setup_UI_Tower_Info()
    {
        Tower_Available ta = Main_Canvas.GetComponent<Tower_Available>();
        tower_info = Main_Canvas.GetComponent<Tower_Info>();
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");

        player = Local_Profile.GetComponent<Player_Status>().player;

        Tower_Level = tower_info.Get_Tower_Level_Form_PlayerStatus(Tower_Number, player);
        int Tower_EXP = tower_info.Get_Tower_Exp_Form_PlayerStatus(Tower_Number, player);

        int Tower_Level_Start_EXP = ta.Set_Tower_Require_EXP_Level_UP(Tower_Level);
        int Tower_Next_Level_Require_EXP = ta.Set_Tower_Require_EXP_Level_UP(Tower_Level + 1);
        int This_Level_to_Next_Level_Require_Exp = Tower_Next_Level_Require_EXP - Tower_Level_Start_EXP;
        int Last_Level_Balance_Exp = Tower_EXP - Tower_Level_Start_EXP;

        //////
        if (tower_level_point == 0)
            tower_level_point = Tower_Level;

        int Require_EXP_Level_UP = ta.Set_Tower_Require_EXP_Level_UP(Tower_Level);

        Check_Tower_Status();
        Set_Text(Last_Level_Balance_Exp, This_Level_to_Next_Level_Require_Exp);
        Set_Tower_Image();
        Set_Exp_Bar(Tower_EXP, Tower_Next_Level_Require_EXP);
    }

    void Check_Tower_Status()
    {
        Tower_Info.Tower_Info_Detail Basic_Detail = tower_info.Tower_Basic_Info(Tower_Number);
        Tower_Info.Tower_Info_Detail Combine_Up_Detail = tower_info.Tower_Comine_Up_Detail(Tower_Number);
        Tower_Info.Tower_Info_Detail Level_Up_Detail = tower_info.Tower_Level_Up_Detail(Tower_Number);
        Tower_Info.Tower_Info_Detail Power_Up_Detail = tower_info.Tower_Comine_Up_Detail(Tower_Number);
        Basic_Damage = Update_Basic_Value(Basic_Detail.Damage, Combine_Up_Detail.Damage, Power_Up_Detail.Damage, Level_Up_Detail.Damage);
        float Core_HP = Core_HP_Value;

        ATK_Value = Calculate_Basic_Damage();
        Debug.Log("Basic_Detail.Damage || " + Basic_Detail.Damage + " || " + ATK_Value);
        Speed_Value = Update_Basic_Value(Basic_Detail.Speed, Combine_Up_Detail.Speed, Power_Up_Detail.Speed, Level_Up_Detail.Speed);
        Dis_Value = Update_Basic_Value(Basic_Detail.Distance, Combine_Up_Detail.Distance, Power_Up_Detail.Distance, Level_Up_Detail.Distance);
        Add_Core_HP_Value = Update_Basic_Value(Basic_Detail.Add_Core, Combine_Up_Detail.Add_Core, Power_Up_Detail.Add_Core, Level_Up_Detail.Add_Core);

        float Calculate_Basic_Damage()
        {
            float Core_Bouns_Damage_Rate = Update_Basic_Value(Basic_Detail.Core_Bouns_Rate, Combine_Up_Detail.Core_Bouns_Rate, Power_Up_Detail.Core_Bouns_Rate, Level_Up_Detail.Core_Bouns_Rate);
            Debug.Log("Core_Bouns_Damage_Rate || " + Core_Bouns_Damage_Rate);
            float dmg = Basic_Damage * Core_Bouns_Damage_Rate * (Core_HP_Value / 100);
            return dmg;
        }

        float Update_Basic_Value(float Basic, float Combine_Up, float Power_UP, float Level)
        {
            float Basic_Value = Basic;
            float Combine_Value = Combine_Up * (combine_up_point - 1);
            float Power_Up_Value = Power_UP * (power_up_point - 1);
            float Level_Value = Level * (tower_level_point - 1);
            float value = Basic_Value + Combine_Value + Power_Up_Value + Level_Value;
            return value;
        }
    }

    public void Close_UI_Tower_Info()
    {
        Main_Canvas.GetComponent<UI_Swipe_Canvas>().enabled = true;
        gameObject.SetActive(false);
    }

    void Set_Text(int Last_Level_Balance_Exp, int This_Level_to_Next_Level_Require_Exp)
    {
        Level.GetComponent<Text>().text = Tower_Level.ToString();

        string EXP_Value = Last_Level_Balance_Exp + " / " + This_Level_to_Next_Level_Require_Exp;
        Exp.GetComponent<TMP_Text>().text = EXP_Value;
        if (ATK_Value == 0)
            ATK.GetComponent<TMP_Text>().text = " - ";
        if (ATK_Value != 0)
            ATK.GetComponent<TMP_Text>().text = ATK_Value.ToString();
        Speed.GetComponent<TMP_Text>().text = Speed_Value.ToString();
        Distance.GetComponent<TMP_Text>().text = Dis_Value.ToString();
        Add_Core.GetComponent<TMP_Text>().text = Add_Core_HP_Value.ToString();
        Level_Up_Text.GetComponent<TMP_Text>().text = tower_level_point.ToString();
        Combine_Up_Text.GetComponent<TMP_Text>().text = "Lv. " + combine_up_point.ToString();
        Power_Up_Text.GetComponent<TMP_Text>().text = "Lv. " + power_up_point.ToString();
        Core_HP_Text.GetComponent<TMP_Text>().text = Core_HP_Value.ToString();
        EXP_Bar.GetComponent<Image>().fillAmount = (float)Last_Level_Balance_Exp / (float)This_Level_to_Next_Level_Require_Exp;
    }

    void Set_Tower_Image()
    {
        string Code = "Tower_" + Tower_Number;
        Shop_Code code = (Shop_Code)System.Enum.Parse(typeof(Shop_Code), Code);
        short Tower_Code = (short)code;
        Texture Tower_Texture = UI_Image_Info.GetComponent<UI_Image_Info>().Get_Texture_By_Code(Tower_Code);
        int Base_Number = UI_Tower_Select_Canvas.GetComponent<UI_Tower_Select_Canvas>().Get_Base_Number(Tower_Number);
        Texture Base_Texture = UI_Tower_Select_Canvas.GetComponent<UI_Tower_Select_Canvas>().Get_Rock_Base_Texture(Base_Number);
        Icon_Image.GetComponent<RawImage>().texture = Tower_Texture;
        Rock_Image.GetComponent<RawImage>().texture = Base_Texture;
    }

    void Set_Exp_Bar(int Tower_EXP, int Tower_Next_Level_Require_EXP)
    {
        bool Level_UP = Tower_EXP >= Tower_Next_Level_Require_EXP;
        Tower_Available ta = Main_Canvas.GetComponent<Tower_Available>();
        int Level_UP_Require_Coins = ta.Get_Tower_Require_Gold_Level_UP(Tower_Level);
        GameObject Level_Up_Coins_Text = null;
        if (Level_UP)
        {
            short Tower_Code = ta.Tower_Type_To_Tower_Code((short)Tower_Number);
            Level_UP_Allow_Button.GetComponent<Info>().Tower_Code = Tower_Code;
            Level_UP_Allow_Button.SetActive(true);
            Level_UP_Not_Allow_Button.SetActive(false);
            Level_Up_Coins_Text = Level_UP_Allow_Button.GetComponent<Info>().Level_Up_Coins_Text;
        }
        if (!Level_UP)
        {
            Level_UP_Allow_Button.SetActive(false);
            Level_UP_Not_Allow_Button.SetActive(true);
            Level_Up_Coins_Text = Level_UP_Not_Allow_Button.GetComponent<Info>().Level_Up_Coins_Text;
        }
        Level_Up_Coins_Text.GetComponent<Text>().text = Level_UP_Require_Coins.ToString();
    }

    public void Set_Tower_Level_UP()
    {
        if (tower_level_point == 20)
            return;
        if (tower_level_point < 20) tower_level_point++;

        Setup_UI_Tower_Info();
    }

    public void Set_Tower_Level_Down()
    {
        if (tower_level_point == 1)
            return;
        if (tower_level_point > 1) tower_level_point--;

        Setup_UI_Tower_Info();
    }

    public void Set_Combine_Up()
    {
        Debug.Log("Set_Combine_Up");
        if (combine_up_point == 15)
            return;
        if (combine_up_point < 15) combine_up_point++;

        Setup_UI_Tower_Info();
    }

    public void Set_Combine_Down()
    {
        Debug.Log("Set_Combine_Down");
        if (combine_up_point == 1)
            return;
        if (combine_up_point > 1) combine_up_point--;

        Setup_UI_Tower_Info();
    }

    public void Set_Power_Up()
    {
        Debug.Log("Set_Power_Up");
        if (power_up_point == 15)
            return;
        if (power_up_point < 15) power_up_point++;

        Setup_UI_Tower_Info();
    }

    public void Set_Power_Down()
    {
        Debug.Log("Set_Power_Down");
        Debug.Log("power_up_point_1 || " + power_up_point);
        if (power_up_point == 1)
            return;
        Debug.Log("power_up_point_2 || " + power_up_point);
        if (power_up_point > 1) power_up_point--;

        Setup_UI_Tower_Info();
    }

    public void Set_Core_Up()
    {
        Debug.Log("Set_Core_Up");
        Core_HP_Value += 100;

        Setup_UI_Tower_Info();
    }

    public void Set_Core_Down()
    {
        Debug.Log("Core_HP_Value_1 || " + Core_HP_Value);
        if (Core_HP_Value <= 100)
            return;

        Core_HP_Value -= 100;
        if (Core_HP_Value <= 100)
            Core_HP_Value = 100;
        Debug.Log("Core_HP_Value_2 || " + Core_HP_Value);
        Setup_UI_Tower_Info();
    }
}

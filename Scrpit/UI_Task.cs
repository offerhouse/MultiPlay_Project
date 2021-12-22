using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MasterServerToolkit.MasterServer;

public class UI_Task : MonoBehaviour
{
    public GameObject Main_UI_Object;
    UI_Main UI_Main;
    public GameObject Reward_2, Reward_3, Bouns_Reward_2, Bouns_Reward_3;

    public GameObject Task_1_1_Obj, Task_1_2_Obj, Task_1_3_Obj, Task_2_1_Obj, Task_2_2_Obj, Task_2_3_Obj,
        Task_3_1_Obj, Task_3_2_Obj, Task_3_3_Obj, Task_4_1_Obj, Task_4_2_Obj, Task_4_3_Obj, Task_5_1_Obj, Task_5_2_Obj, Task_5_3_Obj,
        Task_6_1_Obj, Task_6_2_Obj, Task_6_3_Obj, Task_7_1_Obj, Task_7_2_Obj, Task_7_3_Obj, Task_8_1_Obj, Task_8_2_Obj, Task_8_3_Obj,
        Task_9_1_Obj, Task_9_2_Obj, Task_9_3_Obj;

    public GameObject Task_1_1, Task_1_2, Task_1_3, Task_2_1, Task_2_2, Task_2_3, Task_3_1, Task_3_2, Task_3_3,
        Task_4_1, Task_4_2, Task_4_3, Task_5_1, Task_5_2, Task_5_3, Task_6_1, Task_6_2, Task_6_3,
        Task_7_1, Task_7_2, Task_7_3, Task_8_1, Task_8_2, Task_8_3, Task_9_1, Task_9_2, Task_9_3;

    public GameObject Reward_Buttom_1_1, Reward_Buttom_1_2, Reward_Buttom_Level_1, Reward_Buttom_2_1, Reward_Buttom_2_2,
        Reward_Buttom_Level_2, Reward_Buttom_3_1, Reward_Buttom_3_2, Reward_Buttom_Level_3, Reward_Buttom_4_1, Reward_Buttom_4_2,
        Reward_Buttom_Level_4, Reward_Buttom_5_1, Reward_Buttom_5_2, Reward_Buttom_Level_5, Reward_Buttom_6_1, Reward_Buttom_6_2,
        Reward_Buttom_Level_6, Reward_Buttom_7_1, Reward_Buttom_7_2, Reward_Buttom_Level_7, Reward_Buttom_8_1, Reward_Buttom_8_2,
        Reward_Buttom_Level_8, Reward_Buttom_9_1, Reward_Buttom_9_2, Reward_Buttom_Level_9;

    public GameObject Task_Line_Set_1, Task_Line_Set_2, Task_Line_Set_3, Task_Line_Set_4, Task_Line_Set_5, Task_Line_Set_6,
        Task_Line_Set_7, Task_Line_Set_8, Task_Line_Set_9;

    public UnityEvent Update_Property_Finish_Event;
    public Color Original_Color, Dim_Color;

    public class Task_Line_Packet
    {
        public GameObject Task_Line;
        public bool Task_QTY_1_Ready = false;
        public bool Task_QTY_2_Ready = false;
        public bool Task_QTY_3_Ready = false;
        public bool Reward_1_and_2 = false;
        public bool Reward_2_and_3 = false;
        public bool Reward_Level = false;
    }

    void Start()
    {
        Debug.Log("UI_Task || Start");
        UI_Main = Main_UI_Object.GetComponent<UI_Main>();
        UI_Main.Update_Property_Finish_Event.AddListener(Update_Property_Finish);
        InvokeRepeating("Check_UI_Main_Setup_Finish", 0.0f, 1.0f);
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
        Get_Profile();
        CancelInvoke("Check_UI_Main_Setup_Finish");
    }

    void Update_Property_Finish()
    {
        if (!GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            return;
        Get_Profile();
    }

    void Get_Profile()
    {
        Debug.Log("UI_Task || Get_Profile");
        Player_Status.Player Player;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        Player = Local_ProFile.GetComponent<Player_Status>().player;

        short Daily_Level_Finish = Check_Level_Finish((short)Task_Code.Daily_Task, Player);
        short Week_Level_Finish = Check_Level_Finish((short)Task_Code.Week_Task, Player);

        //Debug.Log("UI_Task || Daily_Level_Finish || " + Daily_Level_Finish + " || " + Week_Level_Finish);
        //int D_Task_Code_1 = Player.D_Task_1_Type;
        //int D_Task_Code_2 = Player.D_Task_2_Type;
        //int D_Task_Code_3 = Player.D_Task_3_Type;

        //int D_Task_QTY_1 = Player.D_Task_1_QTY;
        //int D_Task_QTY_2 = Player.D_Task_2_QTY;
        //int D_Task_QTY_3 = Player.D_Task_3_QTY;

        //int W_Task_Code_1 = Player.W_Task_1_Type;
        //int W_Task_Code_2 = Player.W_Task_2_Type;
        //int W_Task_Code_3 = Player.W_Task_3_Type;

        //int W_Task_QTY_1 = Player.W_Task_1_QTY;
        //int W_Task_QTY_2 = Player.W_Task_2_QTY;
        //int W_Task_QTY_3 = Player.W_Task_3_QTY;

        //Debug.Log("UI_Task || D_Task_Code || " + D_Task_Code_1 + " || " + D_Task_Code_2 + " || " + D_Task_Code_3);
        //Debug.Log("UI_Task || D_Task_QTY || " + D_Task_QTY_1 + " || " + D_Task_QTY_2 + " || " + D_Task_QTY_3);
        //Debug.Log("UI_Task || W_Task_Code || " + W_Task_Code_1 + " || " + W_Task_Code_2 + " || " + W_Task_Code_3);
        //Debug.Log("UI_Task || W_Task_QTY || " + W_Task_QTY_1 + " || " + W_Task_QTY_2 + " || " + W_Task_QTY_3);

        Set_Task_Buttom(Daily_Level_Finish, (short)Task_Code.Daily_Task);
        Set_Bouns_Buttom_Color(Daily_Level_Finish, (short)Task_Code.Daily_Task);
        Set_Bouns_Buttom_Color(Daily_Level_Finish, (short)Task_Code.Daily_Task);
        Set_Bouns_Buttom_Color(Daily_Level_Finish, (short)Task_Code.Daily_Task);
        Set_Task_Text((short)Task_Code.Daily_Task);
        Set_Task_Fill_Bar((short)Task_Code.Daily_Task);
        Set_Task_Fill_Bar_Text((short)Task_Code.Daily_Task);
        Set_Text_Active(Daily_Level_Finish);

        void Set_Task_Text(short Daily_or_Week_Task_Code)
        {
            if (Daily_or_Week_Task_Code == (short)Task_Code.Daily_Task)
            {
                Get_Task_Txt_Obj(Daily_Level_Finish, 1).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.D_Task_1_Type, Daily_Level_Finish, Player.D_Task_1_QTY);

                Get_Task_Txt_Obj(Daily_Level_Finish, 2).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.D_Task_2_Type, Daily_Level_Finish, Player.D_Task_2_QTY);
                Get_Task_Txt_Obj(Daily_Level_Finish, 3).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.D_Task_3_Type, Daily_Level_Finish, Player.D_Task_3_QTY);
            }
            if (Daily_or_Week_Task_Code == (short)Task_Code.Week_Task)
            {
                Get_Task_Txt_Obj(Week_Level_Finish, 1).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.W_Task_1_Type, Week_Level_Finish, Player.W_Task_1_QTY);
                Get_Task_Txt_Obj(Week_Level_Finish, 2).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.W_Task_2_Type, Week_Level_Finish, Player.W_Task_2_QTY);
                Get_Task_Txt_Obj(Week_Level_Finish, 3).GetComponent<Text>().text = GetComponent<Task_Info>().Get_Task_Text_Info(
                    (short)Player.W_Task_3_Type, Week_Level_Finish, Player.W_Task_3_QTY);
            }
        }

        void Set_Task_Fill_Bar(short Daily_or_Week_Task_Code)
        {
            if (Daily_or_Week_Task_Code == (short)Task_Code.Daily_Task)
            {
                Debug.Log("Player.D_Task_Current_QTY_1 || " + Player.D_Task_Current_QTY_1 + " || " + Player.D_Task_1_QTY);

                GameObject Fill_Bar_1 = Get_Task_Obj(Daily_Level_Finish, 1).GetComponent<Info>().Fill_Bar;
                GameObject Fill_Bar_2 = Get_Task_Obj(Daily_Level_Finish, 2).GetComponent<Info>().Fill_Bar;
                GameObject Fill_Bar_3 = Get_Task_Obj(Daily_Level_Finish, 3).GetComponent<Info>().Fill_Bar;
                Fill_Bar_1.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.D_Task_Current_QTY_1, Player.D_Task_1_QTY);
                Fill_Bar_2.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.D_Task_Current_QTY_2, Player.D_Task_2_QTY);
                Fill_Bar_3.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.D_Task_Current_QTY_3, Player.D_Task_3_QTY);
            }
            if (Daily_or_Week_Task_Code == (short)Task_Code.Week_Task)
            {
                GameObject Fill_Bar_1 = Get_Task_Obj(Week_Level_Finish, 1).GetComponent<Info>().Fill_Bar;
                GameObject Fill_Bar_2 = Get_Task_Obj(Week_Level_Finish, 2).GetComponent<Info>().Fill_Bar;
                GameObject Fill_Bar_3 = Get_Task_Obj(Week_Level_Finish, 3).GetComponent<Info>().Fill_Bar;
                Fill_Bar_1.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.W_Task_Current_QTY_1, Player.W_Task_1_QTY);
                Fill_Bar_2.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.W_Task_Current_QTY_2, Player.W_Task_2_QTY);
                Fill_Bar_3.GetComponent<Image>().fillAmount = Get_Fill_Amount(Player.W_Task_Current_QTY_3, Player.W_Task_3_QTY);
            }

            float Get_Fill_Amount(float A, float B)
            {
                if (A <= 0 && B <= 0)
                    return 0;
                return A / B;
            }
        }

        void Set_Task_Fill_Bar_Text(short Daily_or_Week_Task_Code)
        {
            if (Daily_or_Week_Task_Code == (short)Task_Code.Daily_Task)
            {
                GameObject Fill_Bar_Text1 = Get_Task_Obj(Daily_Level_Finish, 1).GetComponent<Info>().Fill_QTY_Text;
                GameObject Fill_Bar_Text2 = Get_Task_Obj(Daily_Level_Finish, 2).GetComponent<Info>().Fill_QTY_Text;
                GameObject Fill_Bar_Text3 = Get_Task_Obj(Daily_Level_Finish, 3).GetComponent<Info>().Fill_QTY_Text;
                Fill_Bar_Text1.GetComponent<Text>().text = Player.D_Task_Current_QTY_1.ToString() + " / " + Player.D_Task_1_QTY.ToString();
                Fill_Bar_Text2.GetComponent<Text>().text = Player.D_Task_Current_QTY_2.ToString() + " / " + Player.D_Task_2_QTY.ToString();
                Fill_Bar_Text3.GetComponent<Text>().text = Player.D_Task_Current_QTY_3.ToString() + " / " + Player.D_Task_3_QTY.ToString();
            }
            if (Daily_or_Week_Task_Code == (short)Task_Code.Week_Task)
            {
                GameObject Fill_Bar_Text1 = Get_Task_Obj(Week_Level_Finish, 1).GetComponent<Info>().Fill_QTY_Text;
                GameObject Fill_Bar_Text2 = Get_Task_Obj(Week_Level_Finish, 2).GetComponent<Info>().Fill_QTY_Text;
                GameObject Fill_Bar_Text3 = Get_Task_Obj(Week_Level_Finish, 3).GetComponent<Info>().Fill_QTY_Text;
                Fill_Bar_Text1.GetComponent<Text>().text = Player.W_Task_Current_QTY_1.ToString() + " / " + Player.W_Task_1_QTY.ToString();
                Fill_Bar_Text2.GetComponent<Text>().text = Player.W_Task_Current_QTY_2.ToString() + " / " + Player.W_Task_2_QTY.ToString();
                Fill_Bar_Text3.GetComponent<Text>().text = Player.W_Task_Current_QTY_3.ToString() + " / " + Player.W_Task_3_QTY.ToString();
            }
        }

        //D_Reward_1_1_Claim
        //D_Reward_1_2_Claim
        //D_Reward_Level_1_Claim
        //D_Reward_2_1_Claim
        //D_Reward_2_2_Claim
        //D_Reward_Level_2_Claim
        //D_Reward_3_1_Claim
        //D_Reward_3_2_Claim
        //D_Reward_Level_3_Claim
        //D_Reward_4_1_Claim
        //D_Reward_4_2_Claim
        //D_Reward_Level_4_Claim
        //D_Reward_5_1_Claim
        //D_Reward_5_2_Claim
        //D_Reward_Level_5_Claim
        //D_Reward_6_1_Claim
        //D_Reward_6_2_Claim
        //D_Reward_Level_6_Claim
        //D_Reward_7_1_Claim
        //D_Reward_7_2_Claim
        //D_Reward_Level_7_Claim
        //D_Reward_8_1_Claim
        //D_Reward_8_2_Claim
        //D_Reward_Level_8_Claim
        //D_Reward_9_1_Claim
        //D_Reward_9_2_Claim
        //D_Reward_Level_9_Claim
        //W_RewarW_1_1_Claim
        //W_RewarW_1_2_Claim
        //W_RewarW_Level_1_Claim
        //W_RewarW_2_1_Claim
        //W_RewarW_2_2_Claim
        //W_RewarW_Level_2_Claim
        //W_RewarW_3_1_Claim
        //W_RewarW_3_2_Claim
        //W_RewarW_Level_3_Claim
        //W_RewarW_4_1_Claim
        //W_RewarW_4_2_Claim
        //W_RewarW_Level_4_Claim
        //W_RewarW_5_1_Claim
        //W_RewarW_5_2_Claim
        //W_RewarW_Level_5_Claim
        //W_RewarW_6_1_Claim
        //W_RewarW_6_2_Claim
        //W_RewarW_Level_6_Claim
        //W_RewarW_7_1_Claim
        //W_RewarW_7_2_Claim
        //W_RewarW_Level_7_Claim
        //W_RewarW_8_1_Claim
        //W_RewarW_8_2_Claim
        //W_RewarW_Level_8_Claim
        //W_RewarW_9_1_Claim
        //W_RewarW_9_2_Claim
        //W_RewarW_Level_9_Claim
    }

    void Set_Bouns_Buttom_Color(short Level_Finish, short Daily_or_Week_Task)
    {
        short[] reward = null;
        bool Task_QTY_1_Ready = false, Task_QTY_2_Ready = false, Task_QTY_3_Ready = false;
        bool Task_Combine_1_Ready = false; bool Task_Combine_2_Ready = false;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                GameObject Bouns_Obj = Get_Bouns_Obj((short)i, (short)j);
                if (i == Level_Finish)
                {
                    reward = GetComponent<Task_Info>().Get_Task_Bouns_Reward(Daily_or_Week_Task, Level_Finish, false);
                    if (j == 1)
                    {
                        short task_code1 = Get_Task_Code(Daily_or_Week_Task, Level_Finish, 1);
                        short QTY1 = (short)GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Task, task_code1, Level_Finish)[0];
                        short Player_Task_QTY_1 = (short)Get_Player_Task_QTY(Daily_or_Week_Task, 1);
                        short task_code2 = Get_Task_Code(Daily_or_Week_Task, Level_Finish, 2);
                        short QTY2 = (short)GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Task, task_code2, Level_Finish)[0];
                        short Player_Task_QTY_2 = (short)Get_Player_Task_QTY(Daily_or_Week_Task, 1);

                        if (Player_Task_QTY_1 >= QTY1)
                            Task_QTY_1_Ready = true;
                        if (Player_Task_QTY_2 >= QTY2)
                            Task_QTY_2_Ready = true;

                        Check_Reward(reward, Bouns_Obj);
                        if (Player_Task_QTY_1 >= QTY1 && Player_Task_QTY_2 >= QTY2)
                        {
                            Task_Combine_1_Ready = true;
                            Bouns_Obj.GetComponent<Image>().color = Original_Color;
                        }
                        if (Player_Task_QTY_1 < QTY1 && Player_Task_QTY_2 < QTY2)
                            Bouns_Obj.GetComponent<Image>().color = Dim_Color;
                    }
                    if (j == 2)
                    {
                        short task_code2 = Get_Task_Code(Daily_or_Week_Task, Level_Finish, 2);
                        short QTY2 = (short)GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Task, task_code2, Level_Finish)[0];
                        short Player_Task_QTY_2 = (short)Get_Player_Task_QTY(Daily_or_Week_Task, 2);
                        short task_code3 = Get_Task_Code(Daily_or_Week_Task, Level_Finish, 3);
                        short QTY3 = (short)GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Task, task_code3, Level_Finish)[0];
                        short Player_Task_QTY_3 = (short)Get_Player_Task_QTY(Daily_or_Week_Task, 3);

                        if (Player_Task_QTY_3 >= QTY3)
                            Task_QTY_3_Ready = true;

                        Check_Reward(reward, Bouns_Obj);
                        if (Player_Task_QTY_2 >= QTY2 && Player_Task_QTY_3 >= QTY3)
                        {
                            Task_Combine_2_Ready = true;
                            Bouns_Obj.GetComponent<Image>().color = Original_Color;
                        }
                        if (Player_Task_QTY_2 < QTY2 && Player_Task_QTY_3 < QTY3)
                            Bouns_Obj.GetComponent<Image>().color = Dim_Color;
                    }

                    if (j == 3)
                    {
                        Check_Reward(reward, Bouns_Obj);
                        if (Task_Combine_1_Ready && Task_Combine_2_Ready)
                            Bouns_Obj.GetComponent<Image>().color = Original_Color;
                        if (!Task_Combine_1_Ready || !Task_Combine_2_Ready)
                            Bouns_Obj.GetComponent<Image>().color = Dim_Color;
                    }

                    Task_Line_Packet packet = new Task_Line_Packet();
                    packet.Task_Line = Get_Task_Line_Set(i);
                    packet.Task_QTY_1_Ready = Task_QTY_1_Ready;
                    packet.Task_QTY_2_Ready = Task_QTY_2_Ready;
                    packet.Task_QTY_3_Ready = Task_QTY_3_Ready;
                    packet.Reward_1_and_2 = Reward_Check_Claim(Daily_or_Week_Task, i, (short)Task_Code.Reward_1_and_2);
                    packet.Reward_2_and_3 = Reward_Check_Claim(Daily_or_Week_Task, i, (short)Task_Code.Reward_2_and_3);
                    packet.Reward_Level = Reward_Check_Claim(Daily_or_Week_Task, i, (short)Task_Code.Reward_Level);

                    if (packet.Task_Line)
                        Set_Task_Line(packet);
                }
                if (i != Level_Finish)
                {
                    Bouns_Obj.GetComponent<Image>().color = Dim_Color;
                    Bouns_Obj.transform.Find("Bouns_Reward_2").gameObject.SetActive(false);
                    Bouns_Obj.transform.Find("Bouns_Reward_3").gameObject.SetActive(false);

                    Task_Line_Packet packet = new Task_Line_Packet();
                    packet.Task_Line = Get_Task_Line_Set(i);

                    if (packet.Task_Line)
                        Set_Task_Line(packet);
                }
            }
        }

        void Check_Reward(short[] m_reward, GameObject Bouns_Reward_Obj)
        {
            short number = 0, GOLD = 0, Diamond = 0, Bouns = 0, Bouns_Type = 0;
            for (int i = 0; i < m_reward.Length; i++)
            {
                if (i == 0 && m_reward[i] != 0)
                    GOLD = m_reward[i];

                if (i == 1 && m_reward[i] != 0)
                    Diamond = m_reward[i];

                if (i == 2 && m_reward[i] != 0)
                {
                    Bouns = m_reward[i];
                    Bouns_Type = (short)i;
                }
                if (i == 3 && m_reward[i] != 0)
                {
                    Bouns = m_reward[i];
                    Bouns_Type = (short)i;
                }
                if (i == 4 && m_reward[i] != 0)
                {
                    Bouns = m_reward[i];
                    Bouns_Type = (short)i;
                }
                if (i == 5 && m_reward[i] != 0)
                {
                    Bouns = m_reward[i];
                    Bouns_Type = (short)i;
                }
                if (m_reward[i] != 0)
                    number++;

                if (number == 2)
                {
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_3").gameObject.SetActive(false);
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_2").gameObject.SetActive(true);
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_2/Reward_Gold/Text").GetComponent<Text>().text = GOLD.ToString();
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_2/Reward_Diamond/Text").GetComponent<Text>().text = Diamond.ToString();
                }
                if (number == 3)
                {
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_2").gameObject.SetActive(false);
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_3").gameObject.SetActive(true);
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_3/Reward_Gold/Text").GetComponent<Text>().text = GOLD.ToString();
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_3/Reward_Diamond/Text").GetComponent<Text>().text = Diamond.ToString();
                    Bouns_Reward_Obj.transform.Find("Bouns_Reward_3/Reward_3/Text").GetComponent<Text>().text = Bouns.ToString();
                }
            }
        }
    }

    bool Reward_Check_Claim(short Task_Code_Weekly_or_Daily, int Level_Number, short Task_Code_Reward_Type)
    {
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        Player_Status.Player Player = Local_ProFile.GetComponent<Player_Status>().player;

        int[] Array = new int[10];
        if (Task_Code_Weekly_or_Daily == (short)Task_Code.Daily_Task)
        {
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_1_and_2)
            {
                Array = new int[] {0, Player.D_Reward_1_1_Claim, Player.D_Reward_2_1_Claim, Player.D_Reward_3_1_Claim,
                    Player.D_Reward_4_1_Claim, Player.D_Reward_5_1_Claim, Player.D_Reward_6_1_Claim, Player.D_Reward_7_1_Claim,
                    Player.D_Reward_8_1_Claim, Player.D_Reward_9_1_Claim };
            }
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_2_and_3)
            {
                Array = new int[] {0, Player.D_Reward_1_2_Claim, Player.D_Reward_2_2_Claim, Player.D_Reward_3_2_Claim,
                    Player.D_Reward_4_2_Claim, Player.D_Reward_5_2_Claim, Player.D_Reward_6_2_Claim, Player.D_Reward_7_2_Claim,
                    Player.D_Reward_8_2_Claim, Player.D_Reward_9_2_Claim };
            }
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_Level)
            {
                Array = new int[] {0, Player.D_Reward_Level_1_Claim, Player.D_Reward_Level_2_Claim, Player.D_Reward_Level_3_Claim,
                    Player.D_Reward_Level_4_Claim, Player.D_Reward_Level_5_Claim, Player.D_Reward_Level_6_Claim,
                    Player.D_Reward_Level_7_Claim, Player.D_Reward_Level_8_Claim, Player.D_Reward_Level_9_Claim };
            }
        }
        if (Task_Code_Weekly_or_Daily == (short)Task_Code.Week_Task)
        {
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_1_and_2)
            {
                Array = new int[] {0, Player.W_RewarW_1_1_Claim, Player.W_RewarW_2_1_Claim, Player.W_RewarW_3_1_Claim,
                    Player.W_RewarW_4_1_Claim, Player.W_RewarW_5_1_Claim, Player.W_RewarW_6_1_Claim, Player.W_RewarW_7_1_Claim,
                    Player.W_RewarW_8_1_Claim, Player.W_RewarW_9_1_Claim };
            }
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_2_and_3)
            {
                Array = new int[] {0, Player.W_RewarW_1_2_Claim, Player.W_RewarW_2_2_Claim, Player.W_RewarW_3_2_Claim,
                    Player.W_RewarW_4_2_Claim, Player.W_RewarW_5_2_Claim, Player.W_RewarW_6_2_Claim, Player.W_RewarW_7_2_Claim,
                    Player.W_RewarW_8_2_Claim, Player.W_RewarW_9_2_Claim };
            }
            if (Task_Code_Reward_Type == (short)Task_Code.Reward_Level)
            {
                Array = new int[] {0, Player.W_RewarW_Level_1_Claim, Player.W_RewarW_Level_2_Claim, Player.W_RewarW_Level_3_Claim,
                    Player.W_RewarW_Level_4_Claim, Player.W_RewarW_Level_5_Claim, Player.W_RewarW_Level_6_Claim,
                    Player.W_RewarW_Level_7_Claim, Player.W_RewarW_Level_8_Claim, Player.W_RewarW_Level_9_Claim };
            }
        }
        int Claim_or_Not = Array[Level_Number];
        if (Claim_or_Not == 1)
            return true;
        return false;
    }

    GameObject Get_Bouns_Obj(short Level_Finish, short Task_Number)
    {
        GameObject[] Obj = null;
        GameObject Task_Obj = null;
        switch (Task_Number)
        {
            case (1):
                Obj = new GameObject[] { Reward_Buttom_1_1, Reward_Buttom_2_1,Reward_Buttom_3_1,Reward_Buttom_4_1,
            Reward_Buttom_5_1,Reward_Buttom_6_1,Reward_Buttom_7_1,Reward_Buttom_8_1,Reward_Buttom_9_1 };
                break;
            case (2):
                Obj = new GameObject[] { Reward_Buttom_1_2, Reward_Buttom_2_2,Reward_Buttom_3_2,Reward_Buttom_4_2,
            Reward_Buttom_5_2,Reward_Buttom_6_2,Reward_Buttom_7_2,Reward_Buttom_8_2,Reward_Buttom_9_2 };
                break;
            case (3):
                Obj = new GameObject[] { Reward_Buttom_Level_1, Reward_Buttom_Level_2,Reward_Buttom_Level_3,Reward_Buttom_Level_4,
            Reward_Buttom_Level_5,Reward_Buttom_Level_6,Reward_Buttom_Level_7,Reward_Buttom_Level_8,Reward_Buttom_Level_9 };
                break;
        }
        Task_Obj = Obj[Level_Finish];
        return Task_Obj;
    }

    GameObject Get_Task_Line_Set(int Task_Level_Number)
    {
        switch (Task_Level_Number)
        {
            case (1): return Task_Line_Set_1;
            case (2): return Task_Line_Set_2;
            case (3): return Task_Line_Set_3;
            case (4): return Task_Line_Set_4;
            case (5): return Task_Line_Set_5;
            case (6): return Task_Line_Set_6;
            case (7): return Task_Line_Set_7;
            case (8): return Task_Line_Set_8;
            case (9): return Task_Line_Set_9;
        }
        return null;
    }

    void Set_Task_Line(Task_Line_Packet packet)
    {
        // Task_Vertical_Line
        // Task_Line_1_(1_and_2)
        // Task_Line_1_(2_and_3)
        // Task_Line_1_(Bouns_1_and_2)

        GameObject Task_Line_Obj = packet.Task_Line;
        bool Task_QTY_1_Ready = packet.Task_QTY_1_Ready;
        bool Task_QTY_2_Ready = packet.Task_QTY_2_Ready;
        bool Task_QTY_3_Ready = packet.Task_QTY_3_Ready;
        bool Reward_1_and_2 = packet.Reward_1_and_2;
        bool Reward_2_and_3 = packet.Reward_2_and_3;
        bool Reward_Level = packet.Reward_Level;

        GameObject Task_Line_1_and_2 = Task_Line_Obj.transform.Find("Task_Line_1_and_2").gameObject;
        GameObject Task_Line_2_and_3 = Task_Line_Obj.transform.Find("Task_Line_2_and_3").gameObject;
        GameObject Task_Line___Bouns = Task_Line_Obj.transform.Find("Task_Line___Bouns").gameObject;

        Check_Task_Finish_and_Reward_Claim(Task_Line_1_and_2, Task_QTY_1_Ready, Task_QTY_2_Ready, Reward_1_and_2);
        Check_Task_Finish_and_Reward_Claim(Task_Line_2_and_3, Task_QTY_2_Ready, Task_QTY_3_Ready, Reward_2_and_3);
        Check_Task_Finish_and_Reward_Claim(Task_Line___Bouns, Reward_1_and_2, Reward_2_and_3, Reward_Level);

        void Check_Task_Finish_and_Reward_Claim(GameObject task_line_obj, bool Task_A, bool Task_B, bool Reward_Claim)
        {
            bool Left = Task_A, Right = Task_B, Full = false, All_Dim = false;
            if (Task_A && Task_B)
            {
                Left = false;
                Right = false;
                Full = true;
            }
            if (!Task_A && !Task_B)
            {
                Full = false;
                All_Dim = true;
            }
            task_line_obj.transform.Find("Task_Line_Grow_Full").gameObject.SetActive(Full);
            task_line_obj.transform.Find("Task_Line_Dim_Right").gameObject.SetActive(Right);
            task_line_obj.transform.Find("Task_Line_Dim_Left").gameObject.SetActive(Left);
            task_line_obj.transform.Find("Task_Line_Dim").gameObject.SetActive(All_Dim);
        }
    }

    void Set_Text_Active(short Level_Finish)
    {
        Debug.Log("Level_Finish || " + Level_Finish);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                GameObject Text_Obj = Get_Task_Txt_Obj((short)i, (short)j);
                if (i != Level_Finish)
                    Text_Obj.SetActive(false);
            }
        }
    }

    void Set_Task_Buttom(short Level_Finish, short Daily_or_Week_Code)
    {
        int[] reward = null;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                GameObject Task_Obj = Get_Task_Obj((short)i, (short)j);
                if (i == Level_Finish)
                {
                    Task_Obj.GetComponent<Image>().color = Original_Color;
                    short task_code = Get_Task_Code(Daily_or_Week_Code, Level_Finish, (short)j);
                    reward = GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Code, task_code, Level_Finish);
                    Task_Obj.transform.Find("Reward_2").gameObject.SetActive(true);
                    Task_Obj.transform.Find("Reward_2/Reward_Gold/Text").GetComponent<Text>().text = reward[1].ToString();
                    Task_Obj.transform.Find("Reward_2/Reward_Diamond/Text").GetComponent<Text>().text = reward[2].ToString();
                }

                if (i != Level_Finish)
                {
                    Task_Obj.GetComponent<Image>().color = Dim_Color;
                    Task_Obj.transform.Find("Reward_2").gameObject.SetActive(false);
                }
            }
        }
    }

    int Get_Player_Task_QTY(short Daily_or_Week_Code, short Number)
    {
        Player_Status.Player Player;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        Player = Local_ProFile.GetComponent<Player_Status>().player;
        int QTY = 0;

        if ((short)Task_Code.Daily_Task == Daily_or_Week_Code && Number == 1)
            QTY = Player.D_Task_1_QTY;
        if ((short)Task_Code.Daily_Task == Daily_or_Week_Code && Number == 2)
            QTY = Player.D_Task_2_QTY;
        if ((short)Task_Code.Daily_Task == Daily_or_Week_Code && Number == 3)
            QTY = Player.D_Task_3_QTY;

        if ((short)Task_Code.Week_Task == Daily_or_Week_Code && Number == 1)
            QTY = Player.W_Task_1_QTY;
        if ((short)Task_Code.Week_Task == Daily_or_Week_Code && Number == 2)
            QTY = Player.W_Task_2_QTY;
        if ((short)Task_Code.Week_Task == Daily_or_Week_Code && Number == 3)
            QTY = Player.W_Task_3_QTY;

        return QTY;
    }

    short Get_Task_Code(short Daily_or_Week_Code, short Level_Finish, short Number)
    {
        short task_code = 0;
        Player_Status.Player Player;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        Player = Local_ProFile.GetComponent<Player_Status>().player;

        //Debug.Log("Daily_or_Week_Code || " + Daily_or_Week_Code + " || " + Number + " || " + Player.D_Task_1_Type);
        if (Daily_or_Week_Code == (short)Task_Code.Daily_Task)
        {
            if (Number == 1)
                task_code = (short)Player.D_Task_1_Type;
            if (Number == 2)
                task_code = (short)Player.D_Task_2_Type;
            if (Number == 3)
                task_code = (short)Player.D_Task_3_Type;
        }

        if (Daily_or_Week_Code == (short)Task_Code.Week_Task)
        {
            if (Number == 1)
                task_code = (short)Player.W_Task_1_Type;
            if (Number == 2)
                task_code = (short)Player.W_Task_2_Type;
            if (Number == 3)
                task_code = (short)Player.W_Task_3_Type;
        }
        return task_code;
    }

    //int[] Get_Task_Info(short Daily_or_Week_Code, short Level_Finish , short Number)
    //{

    //    Debug.Log("Get_Task_Info || " + Daily_or_Week_Code + " || " + task_code + " || " + Level_Finish);
    //    Task_Reward = GetComponent<Task_Info>().Get_Task_Info(Daily_or_Week_Code, task_code, Level_Finish);
    //    return Task_Reward;
    //}

    GameObject Get_Task_Obj(short Level_Finish, short Task_Number)
    {
        GameObject[] Obj = null;
        GameObject Task_Obj = null;
        switch (Task_Number)
        {
            case (1):
                Obj = new GameObject[] { Task_1_1_Obj, Task_2_1_Obj, Task_3_1_Obj, Task_4_1_Obj, Task_5_1_Obj, Task_6_1_Obj, Task_7_1_Obj,
                    Task_8_1_Obj, Task_9_1_Obj };
                break;
            case (2):
                Obj = new GameObject[] { Task_1_2_Obj, Task_2_2_Obj, Task_3_2_Obj, Task_4_2_Obj, Task_5_2_Obj, Task_6_2_Obj, Task_7_2_Obj,
                    Task_8_2_Obj, Task_9_2_Obj };
                break;
            case (3):
                Obj = new GameObject[] { Task_1_3_Obj, Task_2_3_Obj, Task_3_3_Obj, Task_4_3_Obj, Task_5_3_Obj, Task_6_3_Obj, Task_7_3_Obj,
                    Task_8_3_Obj, Task_9_3_Obj };
                break;
        }
        Task_Obj = Obj[Level_Finish];
        return Task_Obj;
    }

    GameObject Get_Task_Txt_Obj(short Level_Finish, short Task_Number)
    {
        GameObject[] Obj = new GameObject[0];
        GameObject Task_Obj = null;
        switch (Task_Number)
        {
            case (1):
                Obj = new GameObject[] { Task_1_1, Task_2_1, Task_3_1, Task_4_1, Task_5_1, Task_6_1, Task_7_1, Task_8_1, Task_9_1 };
                break;
            case (2):
                Obj = new GameObject[] { Task_1_2, Task_2_2, Task_3_2, Task_4_2, Task_5_2, Task_6_2, Task_7_2, Task_8_2, Task_9_2 };
                break;
            case (3):
                Obj = new GameObject[] { Task_1_3, Task_2_3, Task_3_3, Task_4_3, Task_5_3, Task_6_3, Task_7_3, Task_8_3, Task_9_3 };
                break;
        }
        Task_Obj = Obj[Level_Finish];
        return Task_Obj;
    }

    short Check_Level_Finish(short Daily_Week_Code, Player_Status.Player Player)
    {
        int[] Code = new int[0];
        short Level_Finish = 0;
        if ((short)Task_Code.Daily_Task == Daily_Week_Code) // Daily
        {
            Code = new int[] { 0, Player.D_Reward_Level_1_Claim , Player.D_Reward_Level_2_Claim , Player.D_Reward_Level_3_Claim ,
                Player.D_Reward_Level_4_Claim , Player.D_Reward_Level_5_Claim , Player.D_Reward_Level_6_Claim ,
                Player.D_Reward_Level_7_Claim , Player.D_Reward_Level_8_Claim , Player.D_Reward_Level_9_Claim };

        }
        if ((short)Task_Code.Week_Task == Daily_Week_Code) // weekly
        {
            Code = new int[] { 0, Player.W_RewarW_Level_1_Claim , Player.W_RewarW_Level_2_Claim , Player.W_RewarW_Level_3_Claim ,
                Player.W_RewarW_Level_4_Claim , Player.W_RewarW_Level_5_Claim , Player.W_RewarW_Level_6_Claim ,
                Player.W_RewarW_Level_7_Claim , Player.W_RewarW_Level_8_Claim , Player.W_RewarW_Level_9_Claim };
        }
        for (short i = 0; i < Code.Length; i++)
        {
            if (Code[i] > 0)
                Level_Finish = i;
        }
        return Level_Finish;
    }

    void Check_Reward_Active()
    {

    }

    void Check_Reward_Ready()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

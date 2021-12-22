using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject Local_Player;
    public GameObject Button_Mask_01, Button_Mask_02, Button_Mask_03, Button_Mask_04, Button_Mask_05;
    public GameObject Tower_Gold_Text, Desk_01_Level_Text, Desk_02_Level_Text, Desk_03_Level_Text, Desk_04_Level_Text, Desk_05_Level_Text;
    public GameObject Tower_Gold_Desk_01, Tower_Gold_Desk_02, Tower_Gold_Desk_03, Tower_Gold_Desk_04, Tower_Gold_Desk_05;
    public GameObject Player_Gold_Text, Icon_01, Icon_02, Icon_03, Icon_04, Icon_05;
    int Desk_Lv_01, Desk_Lv_02, Desk_Lv_03, Desk_Lv_04, Desk_Lv_05;
    public float Desk_01_Exp, Desk_02_Exp, Desk_03_Exp, Desk_04_Exp, Desk_05_Exp;

    public GameObject My_Map;
    public GameObject Map_Point_01, Map_Point_02, Map_Point_03, Map_Point_04;

    public Texture Icon_101, Icon_102, Icon_103, Icon_104, Icon_105, Icon_106, Icon_107, Icon_108, Icon_109, Icon_110;
    public Texture Icon_201, Icon_202, Icon_203, Icon_204, Icon_205, Icon_206, Icon_207, Icon_208, Icon_209, Icon_210;
    public Texture Icon_301, Icon_302, Icon_303, Icon_304, Icon_305, Icon_306, Icon_307, Icon_308, Icon_309, Icon_310;
    public Texture Icon_401, Icon_402, Icon_403, Icon_404, Icon_405, Icon_406, Icon_407, Icon_408, Icon_409, Icon_410;
    public Texture Icon_501, Icon_502, Icon_503, Icon_504, Icon_505, Icon_506, Icon_507, Icon_508, Icon_509, Icon_510;
    public Texture Icon_601, Icon_602, Icon_603, Icon_604, Icon_605, Icon_606, Icon_607, Icon_608, Icon_609, Icon_610;
    public Texture Icon_611, Icon_612, Icon_613, Icon_614, Icon_615, Icon_616, Icon_617, Icon_618, Icon_619, Icon_620;
    public Texture Icon_701, Icon_702, Icon_703, Icon_704, Icon_705, Icon_706, Icon_707, Icon_708, Icon_709, Icon_710;
    public Texture Icon_711, Icon_712, Icon_713, Icon_714, Icon_715, Icon_716, Icon_717, Icon_718, Icon_719, Icon_720;
    public Texture Icon_721, Icon_722, Icon_723, Icon_724, Icon_725, Icon_726, Icon_727, Icon_728, Icon_729, Icon_730;

    public Texture[] Icon100, Icon200, Icon300, Icon400, Icon500, Icon600, Icon610, Icon700, Icon710, Icon720;

    public void Set_Desk_Icon(short Desk_1, short Desk_2, short Desk_3, short Desk_4, short Desk_5)
    {
        Debug.Log("Set_Desk_Icon || " + Desk_1 + " || " + Desk_2 + " || " + Desk_3 + " || " + Desk_4 + " || " + Desk_5);
        Texture m_icon = Get_Icon(Desk_1);
        Icon_01.GetComponent<RawImage>().texture = m_icon;
        m_icon = Get_Icon(Desk_2);
        Icon_02.GetComponent<RawImage>().texture = m_icon;
        m_icon = Get_Icon(Desk_3);
        Icon_03.GetComponent<RawImage>().texture = m_icon;
        m_icon = Get_Icon(Desk_4);
        Icon_04.GetComponent<RawImage>().texture = m_icon;
        m_icon = Get_Icon(Desk_5);
        Icon_05.GetComponent<RawImage>().texture = m_icon;

        Texture Get_Icon(int Desk_Type_Number)
        {
            Texture icon = null;
            Icon100 = new Texture[] { null, Icon_101, Icon_102, Icon_103, Icon_104, Icon_105, Icon_106, Icon_107, Icon_108, Icon_109, Icon_110 };
            Icon200 = new Texture[] { null, Icon_201, Icon_202, Icon_203, Icon_204, Icon_205, Icon_206, Icon_207, Icon_208, Icon_209, Icon_210 };
            Icon300 = new Texture[] { null, Icon_301, Icon_302, Icon_303, Icon_304, Icon_305, Icon_306, Icon_307, Icon_308, Icon_309, Icon_310 };
            Icon400 = new Texture[] { null, Icon_401, Icon_402, Icon_403, Icon_404, Icon_405, Icon_406, Icon_407, Icon_408, Icon_409, Icon_410 };
            Icon500 = new Texture[] { null, Icon_501, Icon_502, Icon_503, Icon_504, Icon_505, Icon_506, Icon_507, Icon_508, Icon_509, Icon_510 };
            Icon600 = new Texture[] { null, Icon_601, Icon_602, Icon_603, Icon_604, Icon_605, Icon_606, Icon_607, Icon_608, Icon_609, Icon_610 };
            Icon610 = new Texture[] { null, Icon_611, Icon_612, Icon_613, Icon_614, Icon_615, Icon_616, Icon_617, Icon_618, Icon_619, Icon_620 };
            Icon700 = new Texture[] { null, Icon_701, Icon_702, Icon_703, Icon_704, Icon_705, Icon_706, Icon_707, Icon_708, Icon_709, Icon_710 };
            Icon710 = new Texture[] { null, Icon_711, Icon_712, Icon_713, Icon_714, Icon_715, Icon_716, Icon_717, Icon_718, Icon_719, Icon_720 };
            Icon720 = new Texture[] { null, Icon_721, Icon_722, Icon_723, Icon_724, Icon_725, Icon_726, Icon_727, Icon_728, Icon_729, Icon_730 };

            if (Desk_Type_Number >= 100 && Desk_Type_Number <= 110)
                icon = get_Texture(Icon100, Desk_Type_Number - 100);
            if (Desk_Type_Number >= 200 && Desk_Type_Number <= 210)
                icon = get_Texture(Icon200, Desk_Type_Number - 200);
            if (Desk_Type_Number >= 300 && Desk_Type_Number <= 310)
                icon = get_Texture(Icon300, Desk_Type_Number - 300);
            if (Desk_Type_Number >= 400 && Desk_Type_Number <= 410)
                icon = get_Texture(Icon400, Desk_Type_Number - 400);
            if (Desk_Type_Number >= 500 && Desk_Type_Number <= 510)
                icon = get_Texture(Icon500, Desk_Type_Number - 500);
            if (Desk_Type_Number >= 600 && Desk_Type_Number <= 610)
                icon = get_Texture(Icon600, Desk_Type_Number - 600);
            if (Desk_Type_Number >= 611 && Desk_Type_Number <= 620)
                icon = get_Texture(Icon610, Desk_Type_Number - 610);
            if (Desk_Type_Number >= 700 && Desk_Type_Number <= 710)
                icon = get_Texture(Icon700, Desk_Type_Number - 700);
            if (Desk_Type_Number >= 711 && Desk_Type_Number <= 720)
                icon = get_Texture(Icon710, Desk_Type_Number - 710);
            if (Desk_Type_Number >= 721 && Desk_Type_Number <= 730)
                icon = get_Texture(Icon720, Desk_Type_Number - 720);

            return icon;

            Texture get_Texture(Texture[] Icon, int number)
            {
                Texture m_Icon = null;
                m_Icon = Icon[number];
                return m_Icon;
            }
        }

    }

    const short SpawnMessageType = 1000;

    //public void Make_1vs1_Map()
    //{
    //    GameObject m_local_Manager = GameObject.Find("Local_Manager");
    //    GameObject player = m_local_Manager.GetComponent<Local_Manager>().Local_Player_01;
    //    player.GetComponent<Player_Network>().One_VS_One();
    //    player.GetComponent<Player_Network>().Player_UI = gameObject;
    //    GameObject Map = Instantiate(My_Map, Map_Point_01.transform.position, Quaternion.identity);

    //    m_local_Manager.GetComponent<Local_Manager>().Player_UI = gameObject;
    //    m_local_Manager.GetComponent<Local_Manager>().Player_01_Local_Map = Map;
    //    m_local_Manager.GetComponent<Local_Manager>().One_VS_One = true;

    //    m_local_Manager.GetComponent<Local_Manager>().Set_Camera(1, GameObject.Find("Player_Camera_01"));
    //    m_local_Manager.GetComponent<Local_Manager>().Set_Camera(2, GameObject.Find("Player_Camera_02"));
    //    m_local_Manager.GetComponent<Local_Manager>().Set_Camera(3, GameObject.Find("Player_Camera_03"));
    //    m_local_Manager.GetComponent<Local_Manager>().Set_Camera(4, GameObject.Find("Player_Camera_04"));
    //}

    void OnEnable()
    {
        MasterServerToolkit.Bridges.MirrorNetworking.RoomClientManager.load_Offline += Quit_Game;
    }
    
    void OnDisable()
    {
        MasterServerToolkit.Bridges.MirrorNetworking.RoomClientManager.load_Offline -= Quit_Game;
    }

    public void Create_Tower()
    {
        Debug.Log("Create_Tower");
        for (int i = 0; i < 16; i++)
        {
            float time = i * 0.1f;
            StartCoroutine(create_tower_by_time(time));
        }
    }
    
    public void Tower_Power_Up(int desk_number)
    {
        Debug.Log("desk_number || " + desk_number);
        if (!Local_Player)
            Find_Local_Player();
        if (Local_Player)
            Local_Player.GetComponent<Player_Network>().Tower_Desk_Power_Up(desk_number);
    }

    IEnumerator create_tower_by_time(float time)
    {
        yield return new WaitForSeconds(time);
        if (!Local_Player)
            Find_Local_Player();
        if (Local_Player)
            Local_Player.GetComponent<Player_Network>().Create_Tower();
    }

    public void All_Enemy_Fear()
    {
        if (!Local_Player)
            Find_Local_Player();
        if (Local_Player)
            Local_Player.GetComponent<Player_Network>().All_Enemy_Fear();
    }

    public void Update_Desk_Level(int Desk_Level_1, int Desk_Level_2, int Desk_Level_3, int Desk_Level_4, int Desk_Level_5)
    {
        Debug.Log("Update_Desk_Level || " + Desk_Level_1 + " || " + Desk_Level_2 + " || " + Desk_Level_3 + " || " +
            Desk_Level_4 + " || " + Desk_Level_5);
        Desk_Lv_01 = Desk_Level_1;
        Desk_Lv_02 = Desk_Level_2;
        Desk_Lv_03 = Desk_Level_3;
        Desk_Lv_04 = Desk_Level_4;
        Desk_Lv_05 = Desk_Level_5;
        Desk_01_Level_Text.GetComponent<Text>().text = Desk_Level_1.ToString();
        Desk_02_Level_Text.GetComponent<Text>().text = Desk_Level_2.ToString();
        Desk_03_Level_Text.GetComponent<Text>().text = Desk_Level_3.ToString();
        Desk_04_Level_Text.GetComponent<Text>().text = Desk_Level_4.ToString();
        Desk_05_Level_Text.GetComponent<Text>().text = Desk_Level_5.ToString();

        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        int tower_gold_Desk_01 = m_local_Manager.GetComponent<Local_Manager>().Tower_Level_Up_Gold[Desk_Lv_01];
        int tower_gold_Desk_02 = m_local_Manager.GetComponent<Local_Manager>().Tower_Level_Up_Gold[Desk_Lv_02];
        int tower_gold_Desk_03 = m_local_Manager.GetComponent<Local_Manager>().Tower_Level_Up_Gold[Desk_Lv_03];
        int tower_gold_Desk_04 = m_local_Manager.GetComponent<Local_Manager>().Tower_Level_Up_Gold[Desk_Lv_04];
        int tower_gold_Desk_05 = m_local_Manager.GetComponent<Local_Manager>().Tower_Level_Up_Gold[Desk_Lv_05];
        Tower_Gold_Desk_01.GetComponent<Text>().text = tower_gold_Desk_01.ToString();
        Tower_Gold_Desk_02.GetComponent<Text>().text = tower_gold_Desk_02.ToString();
        Tower_Gold_Desk_03.GetComponent<Text>().text = tower_gold_Desk_03.ToString();
        Tower_Gold_Desk_04.GetComponent<Text>().text = tower_gold_Desk_04.ToString();
        Tower_Gold_Desk_05.GetComponent<Text>().text = tower_gold_Desk_05.ToString();
    }

    public void Update_Gold_and_Desk_Exp(int Gold, int Create_Tower_Gold, float Desk_1, float Desk_2, float Desk_3, float Desk_4, float Desk_5)
    {
        Player_Gold_Text.GetComponent<Text>().text = Gold.ToString();
        Tower_Gold_Text.GetComponent<Text>().text = Create_Tower_Gold.ToString();
        Desk_01_Exp = Desk_1;
        Desk_02_Exp = Desk_2;
        Desk_03_Exp = Desk_3;
        Desk_04_Exp = Desk_4;
        Desk_05_Exp = Desk_5;

        GameObject m_local_Manager = GameObject.Find("Local_Manager");

        Button_Mask_01.GetComponent<Image>().fillAmount = Get_Fill_Amount(Desk_Lv_01, Desk_01_Exp);
        Button_Mask_02.GetComponent<Image>().fillAmount = Get_Fill_Amount(Desk_Lv_02, Desk_02_Exp);
        Button_Mask_03.GetComponent<Image>().fillAmount = Get_Fill_Amount(Desk_Lv_03, Desk_03_Exp);
        Button_Mask_04.GetComponent<Image>().fillAmount = Get_Fill_Amount(Desk_Lv_04, Desk_04_Exp);
        Button_Mask_05.GetComponent<Image>().fillAmount = Get_Fill_Amount(Desk_Lv_05, Desk_05_Exp);

        float Get_Fill_Amount(int Desk_Lv, float Desk_Exp)
        {
            int Previous_Level_Exp_Desk = m_local_Manager.GetComponent<Local_Manager>().Tower_Exp[Desk_Lv - 1];
            int Require_Exp_Desk = m_local_Manager.GetComponent<Local_Manager>().Tower_Exp[Desk_Lv];
            int this_level_require_exp_desk = Require_Exp_Desk - Previous_Level_Exp_Desk;
            int this_level_current_exp_desk = (int)Desk_Exp - Previous_Level_Exp_Desk;
            return (float)this_level_current_exp_desk / (float)this_level_require_exp_desk;
        }
    }

    public void Update_Gold(int Gold)
    {
        Player_Gold_Text.GetComponent<Text>().text = Gold.ToString();
    }

    void Find_Local_Player()
    {
        GameObject Local_Manager = GameObject.Find("Local_Manager");
        Local_Player = Local_Manager.GetComponent<Local_Manager>().Get_Local_Player();
    }
    
    public void Quit_Game()
    {
        GameObject Local_Player = GameObject.Find("Local_Manager").GetComponent<Local_Manager>().Local_Player;
        Debug.Log("Local_Player || " + Local_Player);
        Local_Player.GetComponent<Player_Network>().Local_End_Game();
    }
}

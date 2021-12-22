using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MasterServerToolkit.MasterServer;

public class UI_Tower_Select_Canvas : MonoBehaviour, IPointerClickHandler //,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Serializable
    UI_Main UI_Main;
    public GameObject Main_UI_Object;
    public GameObject Current_Canvas;
    public GameObject Selected_Icon, Selected_Slot_Icon;
    public GameObject T_Slot1, T_Slot2, T_Slot3, T_Slot4, T_Slot5;
    public GameObject Line_Object;

    UI_Image_Info Image_Info;

    int Selected_Allow_Used_Tower, Current_Desk, Selected_Slot;
    int[] Original_Desk_1, Original_Desk_2, Original_Desk_3, Original_Desk_4, Original_Desk_5, Original_Desk_6, Original_Desk_7, Original_Desk_8;
    int[] New_Desk_1, New_Desk_2, New_Desk_3, New_Desk_4, New_Desk_5, New_Desk_6, New_Desk_7, New_Desk_8;

    public bool Main_UI, Tower_Select_UI, Shop_UI, Award_UI, Other_Battle_UI;
    public GameObject Main_Canvas, Tower_Select_Canvas, Shop_Canvas, Award_Canvas, Other_Battle_Canvas, UI_Tower_Info;

    float Max_Height, Max_Buttom;

    public float Swipe_Speed = 50;
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.2f;

    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;
    private DateTime StartFingerDownTime;
    private Vector2 ClickDown_Position, ClickUp_Position;
    private Vector2 ClickDown_Canvas_Position;
    public Color Level_UP_Color, Level_UP_Shadow_Color, Level_UP_Not_Allow, Level_UP_Shadow_Not_Allow;
    public Texture Level_UP_Texture, Level_UP_Not_Allow_Texture;
    public bool Mouse_Down = false, SwipeUp = false, SwipeRight = false, SwipeDown = false, SwipeLeft = false, Short_Drag = false;

    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;
    public UnityEvent Update_Property_Finish_Event;

    public GameObject Desk_01, Desk_02, Desk_03, Desk_04, Desk_05, Desk_06, Desk_07, Desk_08;

    public GameObject Button101, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110;
    public GameObject Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120;
    public GameObject Button201, Button202, Button203, Button204, Button205, Button206, Button207, Button208, Button209, Button210;
    public GameObject Button211, Button212, Button213, Button214, Button215, Button216, Button217, Button218, Button219, Button220;
    public GameObject Button301, Button302, Button303, Button304, Button305, Button306, Button307, Button308, Button309, Button310;
    public GameObject Button311, Button312, Button313, Button314, Button315, Button316, Button317, Button318, Button319, Button320;
    public GameObject Button401, Button402, Button403, Button404, Button405, Button406, Button407, Button408, Button409, Button410;
    public GameObject Button411, Button412, Button413, Button414, Button415, Button416, Button417, Button418, Button419, Button420;
    public GameObject Button501, Button502, Button503, Button504, Button505, Button506, Button507, Button508, Button509, Button510;
    public GameObject Button511, Button512, Button513, Button514, Button515, Button516, Button517, Button518, Button519, Button520;
    public GameObject Button601, Button602, Button603, Button604, Button605, Button606, Button607, Button608, Button609, Button610;
    public GameObject Button611, Button612, Button613, Button614, Button615, Button616, Button617, Button618, Button619, Button620;
    public GameObject Button621, Button622, Button623, Button624, Button625, Button626, Button627, Button628, Button629, Button630;
    public GameObject Button701, Button702, Button703, Button704, Button705, Button706, Button707, Button708, Button709, Button710;
    public GameObject Button711, Button712, Button713, Button714, Button715, Button716, Button717, Button718, Button719, Button720;
    public GameObject Button721, Button722, Button723, Button724, Button725, Button726, Button727, Button728, Button729, Button730;
    public GameObject Button731, Button732, Button733, Button734, Button735, Button736, Button737, Button738, Button739, Button740;
    public GameObject Button741, Button742, Button743, Button744, Button745, Button746, Button747, Button748, Button749, Button750;

    //public bool Tower101, Tower102, Tower103, Tower104, Tower105, Tower106, Tower107, Tower108, Tower109, Tower110;
    //public bool Tower111, Tower112, Tower113, Tower114, Tower115, Tower116, Tower117, Tower118, Tower119, Tower120;
    //public bool Tower201, Tower202, Tower203, Tower204, Tower205, Tower206, Tower207, Tower208, Tower209, Tower210;
    //public bool Tower211, Tower212, Tower213, Tower214, Tower215, Tower216, Tower217, Tower218, Tower219, Tower220;
    //public bool Tower301, Tower302, Tower303, Tower304, Tower305, Tower306, Tower307, Tower308, Tower309, Tower310;
    //public bool Tower311, Tower312, Tower313, Tower314, Tower315, Tower316, Tower317, Tower318, Tower319, Tower320;
    //public bool Tower401, Tower402, Tower403, Tower404, Tower405, Tower406, Tower407, Tower408, Tower409, Tower410;
    //public bool Tower411, Tower412, Tower413, Tower414, Tower415, Tower416, Tower417, Tower418, Tower419, Tower420;
    //public bool Tower501, Tower502, Tower503, Tower504, Tower505, Tower506, Tower507, Tower508, Tower509, Tower510;
    //public bool Tower511, Tower512, Tower513, Tower514, Tower515, Tower516, Tower517, Tower518, Tower519, Tower520;
    //public bool Tower601, Tower602, Tower603, Tower604, Tower605, Tower606, Tower607, Tower608, Tower609, Tower610;
    //public bool Tower611, Tower612, Tower613, Tower614, Tower615, Tower616, Tower617, Tower618, Tower619, Tower620;
    //public bool Tower621, Tower622, Tower623, Tower624, Tower625, Tower626, Tower627, Tower628, Tower629, Tower630;
    //public bool Tower701, Tower702, Tower703, Tower704, Tower705, Tower706, Tower707, Tower708, Tower709, Tower710;
    //public bool Tower711, Tower712, Tower713, Tower714, Tower715, Tower716, Tower717, Tower718, Tower719, Tower720;
    //public bool Tower721, Tower722, Tower723, Tower724, Tower725, Tower726, Tower727, Tower728, Tower729, Tower730;
    //public bool Tower731, Tower732, Tower733, Tower734, Tower735, Tower736, Tower737, Tower738, Tower739, Tower740;
    //public bool Tower741, Tower742, Tower743, Tower744, Tower745, Tower746, Tower747, Tower748, Tower749, Tower750;

    public List<Tower_Available.Check_Tower_Available_Local> Available_InGame = new List<Tower_Available.Check_Tower_Available_Local>();
    public List<Tower_Available.Check_Tower_Available_Local> Available_ToPlayer = new List<Tower_Available.Check_Tower_Available_Local>();
    public List<Tower_Available.Check_Tower_Available_Local> Not_Available_InGame = new List<Tower_Available.Check_Tower_Available_Local>();
    #endregion

    private void Start()
    {
        Current_Canvas = Tower_Select_Canvas;
        Current_Canvas.transform.localPosition = new Vector2(0, -50f);

        Max_Height = 2400f;
        Max_Buttom = 100f;
        Selected_Icon.SetActive(false);

        InvokeRepeating("Check_UI_Main_Setup_Finish", 0.0f, 1.0f);
        if (Main_UI_Object)
        {
            Debug.Log("UI_Tower_Select_Canvas || Start");
            UI_Main = Main_UI_Object.GetComponent<UI_Main>();
            UI_Main.Update_Property_Finish_Event.AddListener(Update_Property_Finish);
            Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
            //demoProfilesBehaviour.OnPropertyUpdatedEvent += UpdateProperty;
        }
    }

    public void Setup_Start()
    {
        Debug.Log("UI_Tower_Select_Canvas || Setup_Start");
        Setup_Available_Tower();
        Check_and_Set_Button();
        Start_Desk_Slot();
        Update_Original_Desk();
        if (UI_Tower_Info.activeSelf)
            UI_Tower_Info.GetComponent<UI_Tower_Info>().Setup_UI_Tower_Info();
        CancelInvoke("Check_UI_Main_Setup_Finish");
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

    void Check_and_Set_Button()
    {
        Debug.Log("Set_Tower_Set_UI");
        Vector2 pos = new Vector2(0, 0);
        int Col_number = 1, Row_number = 1;

        Tower_Available ta = Main_UI_Object.GetComponent<Tower_Available>();
        Tower_Info info = Main_UI_Object.GetComponent<Tower_Info>();
        Player_Status.Player player = null;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        if (Local_ProFile != null)
            player = Local_ProFile.GetComponent<Player_Status>().player;

        for (int i = 0; i < Available_ToPlayer.Count; i++)
        {
            bool Tower_Available_In_Game = Available_ToPlayer[i].Tower_Available_In_Game;
            int Tower_Number = Available_ToPlayer[i].Tower_Number;
            short Tower_Code = ta.Tower_Type_To_Tower_Code((short)Tower_Number);
            int Tower_Level = info.Get_Tower_Level_Form_PlayerStatus(Tower_Number, player);

            if (Tower_Available_In_Game)
            {
                bool Tower_Available_Player = Available_ToPlayer[i].Tower_Available_Player;
                if (Tower_Available_Player)
                {
                    GameObject Button = Available_ToPlayer[i].Button;
                    GameObject Item = Button.transform.Find("Item").gameObject;
                    Item.SetActive(true);
                    GameObject Level_Text = Item.GetComponent<Info>().Level_Text;
                    GameObject Exp_Bar = Item.GetComponent<Info>().Exp_Bar;
                    GameObject Exp_Text = Item.GetComponent<Info>().EXP_Text;
                    GameObject Next_Level_EXP_Text = Item.GetComponent<Info>().Next_Level_EXP_Text;
                    GameObject Divider = Item.GetComponent<Info>().Divider;
                    GameObject Level_UP_Coins_Text = Item.GetComponent<Info>().Level_Up_Coins_Text;
                    GameObject Level_UP_Coins_Shadow_Text = Item.GetComponent<Info>().Level_Up_Coins_Shadow_Text;
                    GameObject Level_UP_Image_Obj = Item.GetComponent<Info>().Level_UP_Image_Obj;
                    GameObject Level_UP_Text = Item.GetComponent<Info>().Level_UP_Text;
                    GameObject Level_UP_Shadow_Text = Item.GetComponent<Info>().Level_UP_Shadow_Text;

                    int Tower_Exp = info.Get_Tower_Exp_Form_PlayerStatus(Tower_Number, player);
                    int Tower_Level_Start_EXP = ta.Set_Tower_Require_EXP_Level_UP(Tower_Level);
                    int Tower_Next_Level_Require_EXP = ta.Set_Tower_Require_EXP_Level_UP(Tower_Level + 1);
                    float This_Level_to_Next_Level_Require_Exp = Tower_Next_Level_Require_EXP - Tower_Level_Start_EXP;
                    float Last_Level_Balance_Exp = Tower_Exp - Tower_Level_Start_EXP;

                    Level_Text.GetComponent<Text>().text = Tower_Level.ToString();

                    bool Level_UP = Tower_Exp >= Tower_Next_Level_Require_EXP;
                    if (Level_UP)
                    {
                        Level_UP_Image_Obj.SetActive(true);
                        Exp_Bar.SetActive(false);
                        Exp_Text.SetActive(false);
                        Next_Level_EXP_Text.SetActive(false);
                        Divider.SetActive(false);

                        int Level_UP_Require_Coins = ta.Get_Tower_Require_Gold_Level_UP(Tower_Level);
                        int Gold = player.Gold;
                        Texture Texture = Level_UP_Texture;
                        Color Text_Color = Level_UP_Color;
                        Color Text_Shadow_Color = Level_UP_Shadow_Color;

                        if (Gold < Level_UP_Require_Coins)
                        {

                            Texture = Level_UP_Not_Allow_Texture;
                            Text_Color = Level_UP_Not_Allow;
                            Text_Shadow_Color = Level_UP_Shadow_Not_Allow;
                        }

                        Level_UP_Image_Obj.GetComponent<RawImage>().texture = Texture;
                        Level_UP_Coins_Text.GetComponent<Text>().color = Text_Color;
                        Level_UP_Coins_Shadow_Text.GetComponent<Text>().color = Text_Shadow_Color;
                        Level_UP_Text.GetComponent<Text>().color = Text_Color;
                        Level_UP_Shadow_Text.GetComponent<Text>().color = Text_Shadow_Color;

                        Level_UP_Coins_Text.GetComponent<Text>().text = Level_UP_Require_Coins.ToString();
                        Level_UP_Coins_Shadow_Text.GetComponent<Text>().text = Level_UP_Require_Coins.ToString();
                        Level_UP_Image_Obj.GetComponent<Info>().Tower_Code = Tower_Code;
                    }

                    if (!Level_UP)
                    {
                        Level_UP_Image_Obj.SetActive(false);
                        Exp_Bar.SetActive(true);
                        Exp_Text.SetActive(true);
                        Next_Level_EXP_Text.SetActive(true);
                        Divider.SetActive(true);

                        Exp_Bar.GetComponent<Image>().fillAmount = Last_Level_Balance_Exp / This_Level_to_Next_Level_Require_Exp;
                        Exp_Text.GetComponent<Text>().text = Last_Level_Balance_Exp.ToString();
                        Next_Level_EXP_Text.GetComponent<Text>().text = This_Level_to_Next_Level_Require_Exp.ToString();
                    }

                    Button.SetActive(true);
                    pos = Get_POS(Col_number, Row_number);
                    Button.GetComponent<RectTransform>().anchoredPosition = pos;
                    Col_number++;
                    if (Col_number >= 6)
                    {
                        Col_number = 1;
                        Row_number++;
                    }
                    Set_Slot_Icon(Tower_Number, Button);
                }
            }
        }

        Col_number = 1;
        Row_number += 1;

        pos = Get_POS(Col_number, Row_number);
        Line_Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos.y);

        Col_number = 1;
        Row_number += 1;

        // Tower not Available to Player // Tower EXP = 0
        for (int i = 0; i < Available_InGame.Count; i++)
        {
            if (Available_InGame[i] != null)
            {
                GameObject Button = Available_InGame[i].Button;

                Button.SetActive(true);
                GameObject Item = Button.transform.Find("Item").gameObject;
                Item.SetActive(false);
                int Tower_Number = Available_InGame[i].Tower_Number;
                Set_Slot_Icon(Tower_Number, Button);

                pos = Get_POS(Col_number, Row_number);
                Button.GetComponent<RectTransform>().anchoredPosition = pos;
                Col_number++;
                if (Col_number >= 6)
                {
                    Col_number = 1;
                    Row_number++;
                }
            }
        }

        for (int i = 0; i < Not_Available_InGame.Count; i++)
        {
            if (Not_Available_InGame[i] != null)
                if (Not_Available_InGame[i].Button != null)
                    Not_Available_InGame[i].Button.SetActive(false);
        }

        Vector2 Get_POS(int Col, int Row)
        {
            int x = Get_Button_X_Pos(Col);
            int y = Get_Button_Y_POS(Row);
            return new Vector2(x, y);
        }

        int Get_Button_X_Pos(int Col)
        {
            int POS = 0;
            if (Col == 1) POS = -280;
            if (Col == 2) POS = -140;
            if (Col == 3) POS = 0;
            if (Col == 4) POS = 140;
            if (Col == 5) POS = 280;
            return POS;
        }

        int Get_Button_Y_POS(int Row)
        {
            return (Row * -220) + 220;
        }

    }

    public bool Check_Available_ToPlayer(int number)
    {
        bool available_to_player = false;
        for (int i = 0; i < Available_ToPlayer.Count; i++)
        {
            if (Available_ToPlayer[i] != null)
                if (number == Available_ToPlayer[i].Tower_Number)
                {
                    return available_to_player = Available_ToPlayer[i].Tower_Available_Player;
                }
        }
        return available_to_player;
    }

    public void Setup_Available_Tower()
    {
        Available_InGame = new List<Tower_Available.Check_Tower_Available_Local>();
        Available_ToPlayer = new List<Tower_Available.Check_Tower_Available_Local>();
        Not_Available_InGame = new List<Tower_Available.Check_Tower_Available_Local>();

        Debug.Log("Setup_Available_Tower");
        bool available_InGame = false, available_ToPlayer = false; GameObject Button = null;

        Tower_Available ta = Main_UI_Object.GetComponent<Tower_Available>();
        List<int> Check_Tower_Number_List = ta.Check_Tower_Number_List();

        for (int i = 0; i < Check_Tower_Number_List.Count; i++)
        {
            int number = Check_Tower_Number_List[i];
            Tower_Available.Check_Tower_Available_Local local = new Tower_Available.Check_Tower_Available_Local();
            local.Tower_Number = number;

            available_InGame = ta.Get_Tower_Available_In_Game(number);
            Button = Get_Local_Tower_Button(number);
            local.Button = Button;
            if (available_InGame)
            {
                local.Tower_Available_In_Game = available_InGame;
                available_ToPlayer = Get_Player_Available_This_Tower(number);

                if (available_ToPlayer)
                {
                    local.Tower_Available_Player = available_ToPlayer;
                    Available_ToPlayer.Add(local);
                }
                if (!available_ToPlayer)
                    Available_InGame.Add(local);
            }

            if (!available_InGame)
                Not_Available_InGame.Add(local);
        }
    }

    public Texture Get_Rock_Base_Texture(int number)
    {
        Texture Base_Rock = null;
        if (!Image_Info)
            Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
        if (number == 1) return Base_Rock = Image_Info.Base_Rock_01;
        if (number == 2) return Base_Rock = Image_Info.Base_Rock_02;
        if (number == 3) return Base_Rock = Image_Info.Base_Rock_03;
        if (number == 4) return Base_Rock = Image_Info.Base_Rock_04;
        if (number == 5) return Base_Rock = Image_Info.Base_Rock_05;
        if (number == 6) return Base_Rock = Image_Info.Base_Rock_06;
        if (number == 7) return Base_Rock = Image_Info.Base_Rock_07;

        return Base_Rock;
    }

    public Texture Get_Rock_Dim_Texture(int number)
    {
        Texture Base_Rock = null;
        if (!Image_Info)
            Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();
        if (number == 1) return Base_Rock = Image_Info.Base_Dim_01;
        if (number == 2) return Base_Rock = Image_Info.Base_Dim_02;
        if (number == 3) return Base_Rock = Image_Info.Base_Dim_03;
        if (number == 4) return Base_Rock = Image_Info.Base_Dim_04;
        if (number == 5) return Base_Rock = Image_Info.Base_Dim_05;
        if (number == 6) return Base_Rock = Image_Info.Base_Dim_06;
        if (number == 7) return Base_Rock = Image_Info.Base_Dim_07;

        return Base_Rock;
    }

    //#region Swipe Canvas
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwipeUp = false;
            SwipeDown = false;
            Mouse_Down = true;
            ClickDown_Position = Input.mousePosition;
            this.fingerDown = Input.mousePosition;
            this.fingerUp = Input.mousePosition;
            this.fingerDownTime = DateTime.Now;
            StartFingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClickUp_Position = Input.mousePosition;
            this.fingerDown = Input.mousePosition;
            this.fingerUpTime = DateTime.Now;
            //this.CheckSwipe();
            Mouse_Down = false;
            Short_Drag = false;
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                ClickDown_Position = touch.position;
                this.fingerDown = touch.position;
                this.fingerUp = touch.position;
                this.fingerDownTime = DateTime.Now;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                ClickUp_Position = touch.position;
                this.fingerDown = touch.position;
                this.fingerUpTime = DateTime.Now;
                //this.CheckSwipe();
            }
        }

        float duration = (float)DateTime.Now.Subtract(StartFingerDownTime).TotalSeconds;
        if (Mouse_Down && duration > this.timeThreshold)
        {
            Mouse_Down = false;
            Short_Drag = true;
        }

        //if (SwipeUp)
        //{
        //    Vector2 POS = new Vector3(0, 1250);
        //    Swipe_Canvas(POS, Max_Height, out SwipeUp);
        //}
        //if (SwipeDown)
        //{
        //    Vector2 POS = new Vector3(0, -50);
        //    Swipe_Canvas(POS, Max_Buttom, out SwipeDown);
        //}
    }

    //void Swipe_Canvas(Vector2 POS, float Max_Value, out bool Allow_Swipe)
    //{
    //    Allow_Swipe = true;
    //    if (Swipe_Speed == 0)
    //        Swipe_Speed = Get_Power();

    //    Swipe_Speed -= Time.deltaTime;
    //    float move_Power = Swipe_Speed * Time.deltaTime;
    //    Current_Canvas.transform.localPosition = Vector2.Lerp(Current_Canvas.transform.localPosition, POS, move_Power);

    //    float Distance = Vector2.Distance(Current_Canvas.transform.localPosition, POS);

    //    if (Swipe_Speed < 0.2f)
    //    {
    //        Swipe_Speed = 0;
    //        Allow_Swipe = false;
    //    }

    //    if (Distance < 20)
    //    {
    //        Swipe_Speed = 0;
    //        Allow_Swipe = false;
    //        Current_Canvas.transform.localPosition = POS;
    //    }

    //    float Get_Power()
    //    {
    //        float Up_And_Down_Distance = ClickDown_Position.y - ClickUp_Position.y;
    //        float Power = Up_And_Down_Distance / 100;
    //        return Mathf.Abs(Power);
    //    }
    //}

    //private void CheckSwipe()
    //{
    //    float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
    //    if (duration < this.timeThreshold)
    //    {
    //        float deltaX = this.fingerDown.x - this.fingerUp.x;
    //        float deltaY = this.fingerDown.y - this.fingerUp.y;
    //        if (Mathf.Abs(deltaX) > this.swipeThreshold)
    //        {
    //            if (deltaX > 0 && deltaX > deltaY)
    //            {
    //                this.OnSwipeRight.Invoke();
    //                Debug.Log("right || " + gameObject.name);
    //            }
    //            else if (deltaX < 0 && deltaX > deltaY)
    //            {
    //                this.OnSwipeLeft.Invoke();
    //                Debug.Log("left || " + gameObject.name);
    //            }
    //        }

    //        if (Mathf.Abs(deltaY) > this.swipeThreshold)
    //        {
    //            if (deltaY > 0 && deltaY > deltaX)
    //            {
    //                //this.OnSwipeUp.Invoke();
    //                SwipeUp = true;
    //                Debug.Log("up || " + gameObject.name);
    //            }
    //            else if (deltaY < 0 && deltaY > deltaX)
    //            {
    //                //this.OnSwipeDown.Invoke();
    //                SwipeDown = true;
    //                Debug.Log("down || " + gameObject.name);
    //            }
    //        }

    //        this.fingerUp = this.fingerDown;
    //    }
    //}

    //void Swipe_Up()
    //{
    //    SwipeUp = true;
    //}

    //void Swipe_Down()
    //{
    //    SwipeDown = true;
    //}
    //#endregion

    #region about Drag and Click Tower

    public void OnPointerClick(PointerEventData eventData)
    {
        float duration = (float)DateTime.Now.Subtract(StartFingerDownTime).TotalSeconds;

        float distanceX = ClickDown_Position.x - Input.mousePosition.x;
        float distanceY = ClickDown_Position.y - Input.mousePosition.y;
        float x = Mathf.Abs(distanceX);
        float y = Mathf.Abs(distanceY);

        if (x >= 50 || y >= 50)
            return;

        Debug.Log("duration || " + duration);

        if (duration > 0.5f)
            return;

        int tap = eventData.clickCount;

        if (tap == 1)
        {
            int Tower_Number = 0;
            GameObject obj = null;
            if (eventData.pointerCurrentRaycast.gameObject != null)
                obj = eventData.pointerCurrentRaycast.gameObject;

            Debug.Log("1_Click || " + obj);
            if (obj.GetComponent<Info>() != null)
                Tower_Number = obj.GetComponent<Info>().Number;
            Debug.Log("Tower_Number || " + Tower_Number + " || " + obj.name);

            bool Avaiable_ToPlayer = Check_Available_ToPlayer(Tower_Number);
            if (Avaiable_ToPlayer)
            {

                Selected_Allow_Used_Tower = Tower_Number;
                Selected_Icon.SetActive(true);
                Selected_Icon.transform.localPosition = obj.transform.localPosition;

                if (Selected_Slot != 0)
                {
                    int[] Desk = Get_New_Desk_By_Number(Current_Desk);

                    bool Swiped = Check_Current_Desk_Same_Tower_Will_Swipe(Selected_Slot, Selected_Allow_Used_Tower, out Desk);
                    if (Swiped)
                    {
                        Debug.Log("Swiped");
                        Desk[Selected_Slot] = Selected_Allow_Used_Tower;
                        Set_New_Desk(Current_Desk, Desk);
                        Desk = Get_New_Desk_By_Number(Current_Desk);
                        Reset_Selected_Slot();
                        Local_Update_Desk_Slot();
                    }
                }
            }
            if (!Avaiable_ToPlayer)
            {
                Selected_Allow_Used_Tower = 0;
                Selected_Icon.SetActive(false);
            }
        }
        if (tap == 2)
        {
            GameObject obj = null;
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                obj = eventData.pointerCurrentRaycast.gameObject;
                if (obj.tag == "Tower_Select_Button")
                {
                    int Tower_Number = obj.GetComponent<Info>().Number;
                    Open_UI_Tower_Info(Tower_Number);
                }

                Debug.Log("tap == 2 || obj || " + obj.name);
            }
        }
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    ClickDown_Canvas_Position = Current_Canvas.transform.localPosition;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (eventData == null)
    //        return;

    //    Drag and Move Canvas

    //    if (Short_Drag)
    //    {
    //        float distance = eventData.position.y - ClickDown_Position.y;
    //        float Y = ClickDown_Canvas_Position.y + distance; ;

    //        if (Y >= 1250)
    //            Y = 1250;
    //        if (Y <= -50)
    //            Y = -50;

    //        Current_Canvas.transform.localPosition = new Vector2(0, Y);
    //    }
    //}

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    #endregion

    public void Check_Selected_Slot(GameObject Obj, int Slot_Number)
    {
        Selected_Slot = Slot_Number;
        if (Selected_Allow_Used_Tower == 0)
        {
            Selected_Slot_Icon.SetActive(true);
            Selected_Slot_Icon.transform.localPosition = Obj.transform.localPosition;
            return;
        }

        if (Selected_Allow_Used_Tower != 0)
        {
            bool Avaiable_ToPlayer = Check_Available_ToPlayer(Selected_Allow_Used_Tower);
            if (Avaiable_ToPlayer)
            {
                int[] Desk = Get_New_Desk_By_Number(Current_Desk);
                bool Swiped = Check_Current_Desk_Same_Tower_Will_Swipe(Selected_Slot, Selected_Allow_Used_Tower, out Desk);
                if (!Swiped)
                    Desk[Selected_Slot] = Selected_Allow_Used_Tower;

                Debug.Log("Check_Selected_Slot");
                Set_New_Desk(Current_Desk, Desk);
                Reset_Selected_Slot();
                Local_Update_Desk_Slot();
            }
        }
    }

    void Start_Desk_Slot()
    {
        Player_Status.Player player = null;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        if (Local_ProFile != null)
            player = Local_ProFile.GetComponent<Player_Status>().player;

        Current_Desk = player.Current_Desk;
        int[] Desk = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(Current_Desk);
        Set_Slot_Icon(Desk[1], T_Slot1);
        Set_Slot_Icon(Desk[2], T_Slot2);
        Set_Slot_Icon(Desk[3], T_Slot3);
        Set_Slot_Icon(Desk[4], T_Slot4);
        Set_Slot_Icon(Desk[5], T_Slot5);
    }

    void Local_Update_Desk_Slot()
    {
        int[] Desk = Get_New_Desk_By_Number(Current_Desk);
        Set_Slot_Icon(Desk[1], T_Slot1);
        Set_Slot_Icon(Desk[2], T_Slot2);
        Set_Slot_Icon(Desk[3], T_Slot3);
        Set_Slot_Icon(Desk[4], T_Slot4);
        Set_Slot_Icon(Desk[5], T_Slot5);
    }

    void Set_Slot_Icon(int Tower_Number, GameObject Slot_Obj)
    {
        int Base_number = Get_Base_Number(Tower_Number);
        Texture base_Texture = null;
        Tower_Available ta = Main_UI_Object.GetComponent<Tower_Available>();
        short Tower_Code = ta.Tower_Type_To_Tower_Code((short)Tower_Number);
        if (!Image_Info)
            Image_Info = Main_UI_Object.GetComponent<UI_Main>().UI_Image_Info.GetComponent<UI_Image_Info>();

        bool Avaiable_ToPlayer = Check_Available_ToPlayer(Tower_Number);

        Texture icon_Texture = null;
        GameObject base_Rock = Slot_Obj.GetComponent<Info>().Base_Rock;
        GameObject icon = Slot_Obj.GetComponent<Info>().Icon;

        if (Avaiable_ToPlayer)
        {
            base_Texture = Get_Rock_Base_Texture(Base_number);
            icon_Texture = Image_Info.Get_Texture_By_Code(Tower_Code);
        }

        if (!Avaiable_ToPlayer)
        {
            base_Texture = Get_Rock_Dim_Texture(Base_number);
            icon_Texture = Image_Info.Get_Dim_Texture_By_Code(Tower_Code);
        }

        base_Rock.GetComponent<RawImage>().texture = base_Texture;
        icon.GetComponent<RawImage>().texture = icon_Texture;
    }

    public int Get_Base_Number(int tower_number)
    {
        int base_number = 0;
        if (tower_number > 100 && tower_number < 200)
            base_number = 1;
        if (tower_number > 200 && tower_number < 300)
            base_number = 2;
        if (tower_number > 300 && tower_number < 400)
            base_number = 3;
        if (tower_number > 400 && tower_number < 500)
            base_number = 4;
        if (tower_number > 500 && tower_number < 600)
            base_number = 5;
        if (tower_number > 600 && tower_number < 700)
            base_number = 6;
        if (tower_number > 700 && tower_number < 800)
            base_number = 7;
        return base_number;
    }

    void Update_Original_Desk() // Last update from Server
    {
        Player_Status.Player player = null;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        if (Local_ProFile != null)
            player = Local_ProFile.GetComponent<Player_Status>().player;

        New_Desk_1 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(1);
        New_Desk_2 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(2);
        New_Desk_3 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(3);
        New_Desk_4 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(4);
        New_Desk_5 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(5);
        New_Desk_6 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(6);
        New_Desk_7 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(7);
        New_Desk_8 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(8);

        Original_Desk_1 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(1);
        Original_Desk_2 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(2);
        Original_Desk_3 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(3);
        Original_Desk_4 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(4);
        Original_Desk_5 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(5);
        Original_Desk_6 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(6);
        Original_Desk_7 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(7);
        Original_Desk_8 = Local_ProFile.GetComponent<Player_Status>().Get_Current_Tower_Desk(8);
    }

    bool Check_Current_Desk_Same_Tower_Will_Swipe(int Slot, int Tower_Number, out int[] current_Desk)
    {
        bool Swipe = false;
        current_Desk = Get_New_Desk_By_Number(Current_Desk);
        int Current_Slot_Tower = current_Desk[Slot];
        for (int i = 1; i < 6; i++)
        {
            if (i != Slot)
            {
                if (current_Desk[i] == Tower_Number)
                {
                    Swipe = true;
                    current_Desk[i] = Current_Slot_Tower;
                    current_Desk[Slot] = Tower_Number;
                }
            }
        }
        return Swipe;
    }

    int[] Get_New_Desk_By_Number(int Desk_Number)
    {
        int[] Desk = new int[6];
        if (Desk_Number == 1) return Desk = New_Desk_1;
        if (Desk_Number == 2) return Desk = New_Desk_2;
        if (Desk_Number == 3) return Desk = New_Desk_3;
        if (Desk_Number == 4) return Desk = New_Desk_4;
        if (Desk_Number == 5) return Desk = New_Desk_5;
        if (Desk_Number == 6) return Desk = New_Desk_6;
        if (Desk_Number == 7) return Desk = New_Desk_7;
        if (Desk_Number == 8) return Desk = New_Desk_8;
        return Desk;
    }

    int[] Get_Original_Desk_By_Number(int Desk_Number)
    {
        int[] Desk = new int[6];
        if (Desk_Number == 1) return Desk = Original_Desk_1;
        if (Desk_Number == 2) return Desk = Original_Desk_2;
        if (Desk_Number == 3) return Desk = Original_Desk_3;
        if (Desk_Number == 4) return Desk = Original_Desk_4;
        if (Desk_Number == 5) return Desk = Original_Desk_5;
        if (Desk_Number == 6) return Desk = Original_Desk_6;
        if (Desk_Number == 7) return Desk = Original_Desk_7;
        if (Desk_Number == 8) return Desk = Original_Desk_8;
        return Desk;
    }

    void Set_New_Desk(int Desk_Number, int[] Desk)
    {
        Debug.Log("Set_New_Desk");
        if (Desk_Number == 1) New_Desk_1 = Desk;
        if (Desk_Number == 2) New_Desk_2 = Desk;
        if (Desk_Number == 3) New_Desk_3 = Desk;
        if (Desk_Number == 4) New_Desk_4 = Desk;
        if (Desk_Number == 5) New_Desk_5 = Desk;
        if (Desk_Number == 6) New_Desk_6 = Desk;
        if (Desk_Number == 7) New_Desk_7 = Desk;
        if (Desk_Number == 8) New_Desk_8 = Desk;
    }

    void Reset_Selected_Slot()
    {
        Selected_Slot = 0;
        Selected_Allow_Used_Tower = 0;
        Selected_Slot_Icon.SetActive(false);
        Selected_Icon.SetActive(false);
    }

    List<List<short>> Compare_Original_Desk_And_New_Desk()
    {
        List<short> Desk_Slot_Tower;
        List<List<short>> All_Desk = new List<List<short>>();

        for (int i = 1; i < 9; i++) // current desk
        {
            int[] original_Desk = Get_Original_Desk_By_Number(i);
            int[] new_Desk = Get_New_Desk_By_Number(i);

            for (int j = 1; j < 6; j++) // current slot
            {
                if (original_Desk[j] != new_Desk[j])
                {
                    Desk_Slot_Tower = new List<short>();
                    Desk_Slot_Tower.Add((short)i); // current desk
                    Desk_Slot_Tower.Add((short)j); // current slot
                    Desk_Slot_Tower.Add((short)new_Desk[j]); // modified Tower
                    All_Desk.Add(Desk_Slot_Tower);
                }
            }
        }
        return All_Desk;
    }

    public void Update_Desk()
    {
        List<List<short>> Modified_Desk = Compare_Original_Desk_And_New_Desk();

        Player_Status player;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");

        if (Local_ProFile != null)
        {
            player = Local_ProFile.GetComponent<Player_Status>();
            player.Update_Desk((short)Current_Desk, Modified_Desk);
        }
    }

    public void Change_Desk(int number)
    {
        Current_Desk = number;
        int[] Desk = Get_New_Desk_By_Number(number);

        Set_Slot_Icon(Desk[1], T_Slot1);
        Set_Slot_Icon(Desk[2], T_Slot2);
        Set_Slot_Icon(Desk[3], T_Slot3);
        Set_Slot_Icon(Desk[4], T_Slot4);
        Set_Slot_Icon(Desk[5], T_Slot5);
    }

    void Update_Property_Finish()
    {
        if (!GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            return;
        Debug.Log("UI_Tower_Select_Canvas || Update_Property_Finish");
        Setup_Available_Tower();
        Update_Original_Desk();
    }

    public void Open_UI_Tower_Info(int Tower_Number)
    {
        Main_Canvas.GetComponent<UI_Swipe_Canvas>().enabled = false;
        UI_Tower_Info.GetComponent<UI_Tower_Info>().Tower_Number = Tower_Number;
        UI_Tower_Info.SetActive(true);
        UI_Tower_Info.GetComponent<UI_Tower_Info>().Setup_UI_Tower_Info();
    }

    public bool Get_Player_Available_This_Tower(int number)
    {
        Player_Status.Player player = null;
        bool tower_available = false;
        GameObject Local_ProFile = GameObject.Find("Local_ProFile");
        if (Local_ProFile != null)
            player = Local_ProFile.GetComponent<Player_Status>().player;
        if (player == null)
            return tower_available;

        if (number == 101 && player.Tower_101_Level >= 1) return tower_available = true;
        if (number == 102 && player.Tower_102_Level >= 1) return tower_available = true;
        if (number == 103 && player.Tower_103_Level >= 1) return tower_available = true;
        if (number == 104 && player.Tower_104_Level >= 1) return tower_available = true;
        if (number == 105 && player.Tower_105_Level >= 1) return tower_available = true;
        if (number == 106 && player.Tower_106_Level >= 1) return tower_available = true;
        if (number == 107 && player.Tower_107_Level >= 1) return tower_available = true;
        if (number == 108 && player.Tower_108_Level >= 1) return tower_available = true;
        if (number == 109 && player.Tower_109_Level >= 1) return tower_available = true;
        if (number == 110 && player.Tower_110_Level >= 1) return tower_available = true;
        if (number == 111 && player.Tower_111_Level >= 1) return tower_available = true;
        if (number == 112 && player.Tower_112_Level >= 1) return tower_available = true;
        if (number == 113 && player.Tower_113_Level >= 1) return tower_available = true;
        if (number == 114 && player.Tower_114_Level >= 1) return tower_available = true;
        if (number == 115 && player.Tower_115_Level >= 1) return tower_available = true;
        if (number == 116 && player.Tower_116_Level >= 1) return tower_available = true;
        if (number == 117 && player.Tower_117_Level >= 1) return tower_available = true;
        if (number == 118 && player.Tower_118_Level >= 1) return tower_available = true;
        if (number == 119 && player.Tower_119_Level >= 1) return tower_available = true;
        if (number == 120 && player.Tower_120_Level >= 1) return tower_available = true;
        if (number == 201 && player.Tower_201_Level >= 1) return tower_available = true;
        if (number == 202 && player.Tower_202_Level >= 1) return tower_available = true;
        if (number == 203 && player.Tower_203_Level >= 1) return tower_available = true;
        if (number == 204 && player.Tower_204_Level >= 1) return tower_available = true;
        if (number == 205 && player.Tower_205_Level >= 1) return tower_available = true;
        if (number == 206 && player.Tower_206_Level >= 1) return tower_available = true;
        if (number == 207 && player.Tower_207_Level >= 1) return tower_available = true;
        if (number == 208 && player.Tower_208_Level >= 1) return tower_available = true;
        if (number == 209 && player.Tower_209_Level >= 1) return tower_available = true;
        if (number == 210 && player.Tower_210_Level >= 1) return tower_available = true;
        if (number == 211 && player.Tower_211_Level >= 1) return tower_available = true;
        if (number == 212 && player.Tower_212_Level >= 1) return tower_available = true;
        if (number == 213 && player.Tower_213_Level >= 1) return tower_available = true;
        if (number == 214 && player.Tower_214_Level >= 1) return tower_available = true;
        if (number == 215 && player.Tower_215_Level >= 1) return tower_available = true;
        if (number == 216 && player.Tower_216_Level >= 1) return tower_available = true;
        if (number == 217 && player.Tower_217_Level >= 1) return tower_available = true;
        if (number == 218 && player.Tower_218_Level >= 1) return tower_available = true;
        if (number == 219 && player.Tower_219_Level >= 1) return tower_available = true;
        if (number == 220 && player.Tower_220_Level >= 1) return tower_available = true;

        if (number == 301 && player.Tower_301_Level >= 1) return tower_available = true;
        if (number == 302 && player.Tower_302_Level >= 1) return tower_available = true;
        if (number == 303 && player.Tower_303_Level >= 1) return tower_available = true;
        if (number == 304 && player.Tower_304_Level >= 1) return tower_available = true;
        if (number == 305 && player.Tower_305_Level >= 1) return tower_available = true;
        if (number == 306 && player.Tower_306_Level >= 1) return tower_available = true;
        if (number == 307 && player.Tower_307_Level >= 1) return tower_available = true;
        if (number == 308 && player.Tower_308_Level >= 1) return tower_available = true;
        if (number == 309 && player.Tower_309_Level >= 1) return tower_available = true;
        if (number == 310 && player.Tower_310_Level >= 1) return tower_available = true;
        if (number == 311 && player.Tower_311_Level >= 1) return tower_available = true;
        if (number == 312 && player.Tower_312_Level >= 1) return tower_available = true;
        if (number == 313 && player.Tower_313_Level >= 1) return tower_available = true;
        if (number == 314 && player.Tower_314_Level >= 1) return tower_available = true;
        if (number == 315 && player.Tower_315_Level >= 1) return tower_available = true;
        if (number == 316 && player.Tower_316_Level >= 1) return tower_available = true;
        if (number == 317 && player.Tower_317_Level >= 1) return tower_available = true;
        if (number == 318 && player.Tower_318_Level >= 1) return tower_available = true;
        if (number == 319 && player.Tower_319_Level >= 1) return tower_available = true;
        if (number == 320 && player.Tower_320_Level >= 1) return tower_available = true;

        if (number == 401 && player.Tower_401_Level >= 1) return tower_available = true;
        if (number == 402 && player.Tower_402_Level >= 1) return tower_available = true;
        if (number == 403 && player.Tower_403_Level >= 1) return tower_available = true;
        if (number == 404 && player.Tower_404_Level >= 1) return tower_available = true;
        if (number == 405 && player.Tower_405_Level >= 1) return tower_available = true;
        if (number == 406 && player.Tower_406_Level >= 1) return tower_available = true;
        if (number == 407 && player.Tower_407_Level >= 1) return tower_available = true;
        if (number == 408 && player.Tower_408_Level >= 1) return tower_available = true;
        if (number == 409 && player.Tower_409_Level >= 1) return tower_available = true;
        if (number == 410 && player.Tower_410_Level >= 1) return tower_available = true;
        if (number == 411 && player.Tower_411_Level >= 1) return tower_available = true;
        if (number == 412 && player.Tower_412_Level >= 1) return tower_available = true;
        if (number == 413 && player.Tower_413_Level >= 1) return tower_available = true;
        if (number == 414 && player.Tower_414_Level >= 1) return tower_available = true;
        if (number == 415 && player.Tower_415_Level >= 1) return tower_available = true;
        if (number == 416 && player.Tower_416_Level >= 1) return tower_available = true;
        if (number == 417 && player.Tower_417_Level >= 1) return tower_available = true;
        if (number == 418 && player.Tower_418_Level >= 1) return tower_available = true;
        if (number == 419 && player.Tower_419_Level >= 1) return tower_available = true;
        if (number == 420 && player.Tower_420_Level >= 1) return tower_available = true;

        if (number == 501 && player.Tower_501_Level >= 1) return tower_available = true;
        if (number == 502 && player.Tower_502_Level >= 1) return tower_available = true;
        if (number == 503 && player.Tower_503_Level >= 1) return tower_available = true;
        if (number == 504 && player.Tower_504_Level >= 1) return tower_available = true;
        if (number == 505 && player.Tower_505_Level >= 1) return tower_available = true;
        if (number == 506 && player.Tower_506_Level >= 1) return tower_available = true;
        if (number == 507 && player.Tower_507_Level >= 1) return tower_available = true;
        if (number == 508 && player.Tower_508_Level >= 1) return tower_available = true;
        if (number == 509 && player.Tower_509_Level >= 1) return tower_available = true;
        if (number == 510 && player.Tower_510_Level >= 1) return tower_available = true;
        if (number == 511 && player.Tower_511_Level >= 1) return tower_available = true;
        if (number == 512 && player.Tower_512_Level >= 1) return tower_available = true;
        if (number == 513 && player.Tower_513_Level >= 1) return tower_available = true;
        if (number == 514 && player.Tower_514_Level >= 1) return tower_available = true;
        if (number == 515 && player.Tower_515_Level >= 1) return tower_available = true;
        if (number == 516 && player.Tower_516_Level >= 1) return tower_available = true;
        if (number == 517 && player.Tower_517_Level >= 1) return tower_available = true;
        if (number == 518 && player.Tower_518_Level >= 1) return tower_available = true;
        if (number == 519 && player.Tower_519_Level >= 1) return tower_available = true;
        if (number == 520 && player.Tower_520_Level >= 1) return tower_available = true;

        if (number == 601 && player.Tower_601_Level >= 1) return tower_available = true;
        if (number == 602 && player.Tower_602_Level >= 1) return tower_available = true;
        if (number == 603 && player.Tower_603_Level >= 1) return tower_available = true;
        if (number == 604 && player.Tower_604_Level >= 1) return tower_available = true;
        if (number == 605 && player.Tower_605_Level >= 1) return tower_available = true;
        if (number == 606 && player.Tower_606_Level >= 1) return tower_available = true;
        if (number == 607 && player.Tower_607_Level >= 1) return tower_available = true;
        if (number == 608 && player.Tower_608_Level >= 1) return tower_available = true;
        if (number == 609 && player.Tower_609_Level >= 1) return tower_available = true;
        if (number == 610 && player.Tower_610_Level >= 1) return tower_available = true;
        if (number == 611 && player.Tower_611_Level >= 1) return tower_available = true;
        if (number == 612 && player.Tower_612_Level >= 1) return tower_available = true;
        if (number == 613 && player.Tower_613_Level >= 1) return tower_available = true;
        if (number == 614 && player.Tower_614_Level >= 1) return tower_available = true;
        if (number == 615 && player.Tower_615_Level >= 1) return tower_available = true;
        if (number == 616 && player.Tower_616_Level >= 1) return tower_available = true;
        if (number == 617 && player.Tower_617_Level >= 1) return tower_available = true;
        if (number == 618 && player.Tower_618_Level >= 1) return tower_available = true;
        if (number == 619 && player.Tower_619_Level >= 1) return tower_available = true;
        if (number == 620 && player.Tower_620_Level >= 1) return tower_available = true;
        if (number == 621 && player.Tower_621_Level >= 1) return tower_available = true;
        if (number == 622 && player.Tower_622_Level >= 1) return tower_available = true;
        if (number == 623 && player.Tower_623_Level >= 1) return tower_available = true;
        if (number == 624 && player.Tower_624_Level >= 1) return tower_available = true;
        if (number == 625 && player.Tower_625_Level >= 1) return tower_available = true;
        if (number == 626 && player.Tower_626_Level >= 1) return tower_available = true;
        if (number == 627 && player.Tower_627_Level >= 1) return tower_available = true;
        if (number == 628 && player.Tower_628_Level >= 1) return tower_available = true;
        if (number == 629 && player.Tower_629_Level >= 1) return tower_available = true;
        if (number == 630 && player.Tower_630_Level >= 1) return tower_available = true;

        if (number == 701 && player.Tower_701_Level >= 1) return tower_available = true;
        if (number == 702 && player.Tower_702_Level >= 1) return tower_available = true;
        if (number == 703 && player.Tower_703_Level >= 1) return tower_available = true;
        if (number == 704 && player.Tower_704_Level >= 1) return tower_available = true;
        if (number == 705 && player.Tower_705_Level >= 1) return tower_available = true;
        if (number == 706 && player.Tower_706_Level >= 1) return tower_available = true;
        if (number == 707 && player.Tower_707_Level >= 1) return tower_available = true;
        if (number == 708 && player.Tower_708_Level >= 1) return tower_available = true;
        if (number == 709 && player.Tower_709_Level >= 1) return tower_available = true;
        if (number == 710 && player.Tower_710_Level >= 1) return tower_available = true;
        if (number == 711 && player.Tower_711_Level >= 1) return tower_available = true;
        if (number == 712 && player.Tower_712_Level >= 1) return tower_available = true;
        if (number == 713 && player.Tower_713_Level >= 1) return tower_available = true;
        if (number == 714 && player.Tower_714_Level >= 1) return tower_available = true;
        if (number == 715 && player.Tower_715_Level >= 1) return tower_available = true;
        if (number == 716 && player.Tower_716_Level >= 1) return tower_available = true;
        if (number == 717 && player.Tower_717_Level >= 1) return tower_available = true;
        if (number == 718 && player.Tower_718_Level >= 1) return tower_available = true;
        if (number == 719 && player.Tower_719_Level >= 1) return tower_available = true;
        if (number == 720 && player.Tower_720_Level >= 1) return tower_available = true;
        if (number == 721 && player.Tower_721_Level >= 1) return tower_available = true;
        if (number == 722 && player.Tower_722_Level >= 1) return tower_available = true;
        if (number == 723 && player.Tower_723_Level >= 1) return tower_available = true;
        if (number == 724 && player.Tower_724_Level >= 1) return tower_available = true;
        if (number == 725 && player.Tower_725_Level >= 1) return tower_available = true;
        if (number == 726 && player.Tower_726_Level >= 1) return tower_available = true;
        if (number == 727 && player.Tower_727_Level >= 1) return tower_available = true;
        if (number == 728 && player.Tower_728_Level >= 1) return tower_available = true;
        if (number == 729 && player.Tower_729_Level >= 1) return tower_available = true;
        if (number == 730 && player.Tower_730_Level >= 1) return tower_available = true;
        if (number == 731 && player.Tower_731_Level >= 1) return tower_available = true;
        if (number == 732 && player.Tower_732_Level >= 1) return tower_available = true;
        if (number == 733 && player.Tower_733_Level >= 1) return tower_available = true;
        if (number == 734 && player.Tower_734_Level >= 1) return tower_available = true;
        if (number == 735 && player.Tower_735_Level >= 1) return tower_available = true;
        if (number == 736 && player.Tower_736_Level >= 1) return tower_available = true;
        if (number == 737 && player.Tower_737_Level >= 1) return tower_available = true;
        if (number == 738 && player.Tower_738_Level >= 1) return tower_available = true;
        if (number == 739 && player.Tower_739_Level >= 1) return tower_available = true;
        if (number == 740 && player.Tower_740_Level >= 1) return tower_available = true;
        if (number == 741 && player.Tower_741_Level >= 1) return tower_available = true;
        if (number == 742 && player.Tower_742_Level >= 1) return tower_available = true;
        if (number == 743 && player.Tower_743_Level >= 1) return tower_available = true;
        if (number == 744 && player.Tower_744_Level >= 1) return tower_available = true;
        if (number == 745 && player.Tower_745_Level >= 1) return tower_available = true;
        if (number == 746 && player.Tower_746_Level >= 1) return tower_available = true;
        if (number == 747 && player.Tower_747_Level >= 1) return tower_available = true;
        if (number == 748 && player.Tower_748_Level >= 1) return tower_available = true;
        if (number == 749 && player.Tower_749_Level >= 1) return tower_available = true;
        if (number == 750 && player.Tower_750_Level >= 1) return tower_available = true;
        return tower_available;
    } // player tower level over 1 = can use this tower 

    public GameObject Get_Local_Tower_Button(int number)
    {
        GameObject Button = null;
        if (number == 101) return Button101;
        if (number == 102) return Button102;
        if (number == 103) return Button103;
        if (number == 104) return Button104;
        if (number == 105) return Button105;
        if (number == 106) return Button106;
        if (number == 107) return Button107;
        if (number == 108) return Button108;
        if (number == 109) return Button109;
        if (number == 110) return Button110;
        if (number == 111) return Button111;
        if (number == 112) return Button112;
        if (number == 113) return Button113;
        if (number == 114) return Button114;
        if (number == 115) return Button115;
        if (number == 116) return Button116;
        if (number == 117) return Button117;
        if (number == 118) return Button118;
        if (number == 119) return Button119;
        if (number == 120) return Button120;
        if (number == 201) return Button201;
        if (number == 202) return Button202;
        if (number == 203) return Button203;
        if (number == 204) return Button204;
        if (number == 205) return Button205;
        if (number == 206) return Button206;
        if (number == 207) return Button207;
        if (number == 208) return Button208;
        if (number == 209) return Button209;
        if (number == 210) return Button210;
        if (number == 211) return Button211;
        if (number == 212) return Button212;
        if (number == 213) return Button213;
        if (number == 214) return Button214;
        if (number == 215) return Button215;
        if (number == 216) return Button216;
        if (number == 217) return Button217;
        if (number == 218) return Button218;
        if (number == 219) return Button219;
        if (number == 220) return Button220;
        if (number == 301) return Button301;
        if (number == 302) return Button302;
        if (number == 303) return Button303;
        if (number == 304) return Button304;
        if (number == 305) return Button305;
        if (number == 306) return Button306;
        if (number == 307) return Button307;
        if (number == 308) return Button308;
        if (number == 309) return Button309;
        if (number == 310) return Button310;
        if (number == 311) return Button311;
        if (number == 312) return Button312;
        if (number == 313) return Button313;
        if (number == 314) return Button314;
        if (number == 315) return Button315;
        if (number == 316) return Button316;
        if (number == 317) return Button317;
        if (number == 318) return Button318;
        if (number == 319) return Button319;
        if (number == 320) return Button320;
        if (number == 401) return Button401;
        if (number == 402) return Button402;
        if (number == 403) return Button403;
        if (number == 404) return Button404;
        if (number == 405) return Button405;
        if (number == 406) return Button406;
        if (number == 407) return Button407;
        if (number == 408) return Button408;
        if (number == 409) return Button409;
        if (number == 410) return Button410;
        if (number == 411) return Button411;
        if (number == 412) return Button412;
        if (number == 413) return Button413;
        if (number == 414) return Button414;
        if (number == 415) return Button415;
        if (number == 416) return Button416;
        if (number == 417) return Button417;
        if (number == 418) return Button418;
        if (number == 419) return Button419;
        if (number == 420) return Button420;
        if (number == 501) return Button501;
        if (number == 502) return Button502;
        if (number == 503) return Button503;
        if (number == 504) return Button504;
        if (number == 505) return Button505;
        if (number == 506) return Button506;
        if (number == 507) return Button507;
        if (number == 508) return Button508;
        if (number == 509) return Button509;
        if (number == 510) return Button510;
        if (number == 511) return Button511;
        if (number == 512) return Button512;
        if (number == 513) return Button513;
        if (number == 514) return Button514;
        if (number == 515) return Button515;
        if (number == 516) return Button516;
        if (number == 517) return Button517;
        if (number == 518) return Button518;
        if (number == 519) return Button519;
        if (number == 520) return Button520;
        if (number == 601) return Button601;
        if (number == 602) return Button602;
        if (number == 603) return Button603;
        if (number == 604) return Button604;
        if (number == 605) return Button605;
        if (number == 606) return Button606;
        if (number == 607) return Button607;
        if (number == 608) return Button608;
        if (number == 609) return Button609;
        if (number == 610) return Button610;
        if (number == 611) return Button611;
        if (number == 612) return Button612;
        if (number == 613) return Button613;
        if (number == 614) return Button614;
        if (number == 615) return Button615;
        if (number == 616) return Button616;
        if (number == 617) return Button617;
        if (number == 618) return Button618;
        if (number == 619) return Button619;
        if (number == 620) return Button620;
        if (number == 621) return Button621;
        if (number == 622) return Button622;
        if (number == 623) return Button623;
        if (number == 624) return Button624;
        if (number == 625) return Button625;
        if (number == 626) return Button626;
        if (number == 627) return Button627;
        if (number == 628) return Button628;
        if (number == 629) return Button629;
        if (number == 630) return Button630;
        if (number == 701) return Button701;
        if (number == 702) return Button702;
        if (number == 703) return Button703;
        if (number == 704) return Button704;
        if (number == 705) return Button705;
        if (number == 706) return Button706;
        if (number == 707) return Button707;
        if (number == 708) return Button708;
        if (number == 709) return Button709;
        if (number == 710) return Button710;
        if (number == 711) return Button711;
        if (number == 712) return Button712;
        if (number == 713) return Button713;
        if (number == 714) return Button714;
        if (number == 715) return Button715;
        if (number == 716) return Button716;
        if (number == 717) return Button717;
        if (number == 718) return Button718;
        if (number == 719) return Button719;
        if (number == 720) return Button720;
        if (number == 721) return Button721;
        if (number == 722) return Button722;
        if (number == 723) return Button723;
        if (number == 724) return Button724;
        if (number == 725) return Button725;
        if (number == 726) return Button726;
        if (number == 727) return Button727;
        if (number == 728) return Button728;
        if (number == 729) return Button729;
        if (number == 730) return Button730;
        if (number == 731) return Button731;
        if (number == 732) return Button732;
        if (number == 733) return Button733;
        if (number == 734) return Button734;
        if (number == 735) return Button735;
        if (number == 736) return Button736;
        if (number == 737) return Button737;
        if (number == 738) return Button738;
        if (number == 739) return Button739;
        if (number == 740) return Button740;
        if (number == 741) return Button741;
        if (number == 742) return Button742;
        if (number == 743) return Button743;
        if (number == 744) return Button744;
        if (number == 745) return Button745;
        if (number == 746) return Button746;
        if (number == 747) return Button747;
        if (number == 748) return Button748;
        if (number == 749) return Button749;
        if (number == 750) return Button750;
        return Button;
    }
}

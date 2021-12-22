using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Swipe_Canvas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public GameObject Current_Canvas;
    public GameObject Battle_Canvas, Tower_Canvas, Tower_Canvas_Up_Part, Shop_Canvas, Task_Canvas, Leftmost_Canvas, Rightmost_Canvas;
    public float Battle_Max_Height, Battle_Max_Buttom, Tower_Max_Height, Tower_Max_Buttom, Shop_Max_Height, Shop_Max_Buttom,
        Task_Max_Height, Task_Max_Buttom, Canvas_X;

    public bool Battle_Canvas_Move, Tower_Canvas_Move, Shop_Canvas_Move, Task_Canvas_Move;
    public float Max_Height, Max_Buttom;

    public float Swipe_Speed = 50;
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;
    float x_Pos;

    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;
    private DateTime StartFingerDownTime;
    private Vector2 ClickDown_Position, ClickUp_Position;
    private Vector2 ClickDown_Canvas_Position;

    public bool Mouse_Down = false, SwipeUp = false, SwipeRight = false, SwipeDown = false, SwipeLeft = false, Short_Drag = false;

    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;
    public UnityEvent Update_Property_Finish_Event;


    // Start is called before the first frame update
    void Start()
    {
        Canvas_X = 0;
        x_Pos = 0;
        Current_Canvas.transform.localPosition = new Vector2(x_Pos, 0f);
    }

    #region Swipe Canvas
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
            this.CheckSwipe();
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
                this.CheckSwipe();
            }
        }

        float duration = (float)DateTime.Now.Subtract(StartFingerDownTime).TotalSeconds;
        if (Mouse_Down && duration > this.timeThreshold)
        {
            Mouse_Down = false;
            Short_Drag = true;
        }

        if (SwipeUp)
        {
            Vector2 POS = new Vector2(x_Pos, Max_Height);
            //if (Current_Canvas == Tower_Canvas)
            //    POS = new Vector2(x_Pos, Max_Height - Tower_Child_Canvas.transform.localPosition.y);
            Swipe_Canvas(POS, Max_Height, out SwipeUp);
        }
        if (SwipeDown)
        {
            Vector2 POS = new Vector2(x_Pos, Max_Buttom);
            //if (Current_Canvas == Tower_Canvas)
            //    POS = new Vector2(x_Pos, Max_Height - Tower_Child_Canvas.transform.localPosition.y);
            Swipe_Canvas(POS, Max_Buttom, out SwipeDown);
        }
        if (SwipeRight)
        {
            Change_Canvas(true, out SwipeRight);
        }
        if (SwipeLeft)
        {
            Change_Canvas(false, out SwipeLeft);
        }
    }

    void Change_Canvas(bool Right_or_Left, out bool Allow_Swipe)
    {
        Allow_Swipe = true;
        if (Swipe_Speed == 0)
            Swipe_Speed = 5;

        if (Battle_Canvas_Move)
            Move_Canvas(Battle_Canvas, Canvas_X, out Battle_Canvas_Move);
        if (Tower_Canvas_Move)
            Move_Canvas(Tower_Canvas, Canvas_X + 1440, out Tower_Canvas_Move);
        if (Shop_Canvas_Move)
            Move_Canvas(Shop_Canvas, Canvas_X + 720, out Shop_Canvas_Move);
        if (Task_Canvas_Move)
            Move_Canvas(Task_Canvas, Canvas_X - 720, out Task_Canvas_Move);

        if (!Battle_Canvas_Move && !Tower_Canvas_Move && !Shop_Canvas_Move && !Task_Canvas_Move)
        {
            Allow_Swipe = false;
            Swipe_Speed = 0;
        }

        void Move_Canvas(GameObject canvas, float x_pos, out bool Canvas_Allow_Move)
        {
            Canvas_Allow_Move = true;

            Vector2 POS = new Vector2(x_pos, canvas.transform.localPosition.y);
            canvas.transform.localPosition = Vector2.Lerp(canvas.transform.localPosition, POS, 0.1f);
            Vector2 current_pos = new Vector2(canvas.transform.localPosition.x, 0);
            Vector2 target_pos = new Vector2(x_pos, 0);
            float Distance = Vector2.Distance(current_pos, target_pos);

            if (Distance < 10)
            {
                Canvas_Allow_Move = false;
                canvas.transform.localPosition = POS;
            }
        }
    }

    void Swipe_Canvas(Vector2 POS, float Max_Value, out bool Allow_Swipe)
    {
        Allow_Swipe = true;
        if (Swipe_Speed == 0)
            Swipe_Speed = Get_Power();

        Swipe_Speed -= Time.deltaTime;
        float move_Power = Swipe_Speed * Time.deltaTime;
        Current_Canvas.transform.localPosition = Vector2.Lerp(Current_Canvas.transform.localPosition, POS, move_Power);

        float Distance = Vector2.Distance(Current_Canvas.transform.localPosition, POS);

        if (Swipe_Speed < 0.2f)
        {
            Swipe_Speed = 0;
            Allow_Swipe = false;
        }

        if (Distance < 20)
        {
            Swipe_Speed = 0;
            Allow_Swipe = false;
            Current_Canvas.transform.localPosition = POS;
        }

        float Get_Power()
        {
            float Up_And_Down_Distance = ClickDown_Position.y - ClickUp_Position.y;
            float Power = Up_And_Down_Distance / 100;
            return Mathf.Abs(Power);
        }
    }

    private void CheckSwipe()
    {
        float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
        if (duration < this.timeThreshold)
        {
            float deltaX = this.fingerDown.x - this.fingerUp.x;
            float deltaY = this.fingerDown.y - this.fingerUp.y;

            float X = Mathf.Abs(deltaX);
            float Y = Mathf.Abs(deltaY);

            if (Mathf.Abs(deltaX) > this.swipeThreshold)
            {
                if (deltaX > 0 && X > Y)
                {
                    //this.OnSwipeRight.Invoke();
                    if (Current_Canvas == Leftmost_Canvas)
                        return;
                    Canvas_X += 720;
                    SwipeRight = true;
                    if (Current_Canvas == Tower_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Shop_Canvas, Shop_Max_Height, Shop_Max_Buttom);

                    else if (Current_Canvas == Shop_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Battle_Canvas, Battle_Max_Height, Battle_Max_Buttom);

                    else if (Current_Canvas == Battle_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Task_Canvas, Task_Max_Height, Task_Max_Buttom);

                    Battle_Canvas_Move = true; Tower_Canvas_Move = true; Shop_Canvas_Move = true; Task_Canvas_Move = true;
                }
                else if (deltaX < 0 && X > Y)
                {
                    //this.OnSwipeLeft.Invoke();
                    if (Current_Canvas == Rightmost_Canvas)
                        return;
                    Canvas_X -= 720;
                    SwipeLeft = true;
                    if (Current_Canvas == Task_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Battle_Canvas, Battle_Max_Height, Battle_Max_Buttom);
                    else if (Current_Canvas == Battle_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Shop_Canvas, Shop_Max_Height, Shop_Max_Buttom);
                    else if (Current_Canvas == Shop_Canvas)
                        Set_Canvas_and_Max_Height_and_Buttom(Tower_Canvas, Tower_Max_Height, Tower_Max_Buttom);

                    Battle_Canvas_Move = true; Tower_Canvas_Move = true; Shop_Canvas_Move = true; Task_Canvas_Move = true;
                }

                void Set_Canvas_and_Max_Height_and_Buttom(GameObject canvas, float max_Height, float max_Buttom)
                {
                    Current_Canvas = canvas;
                    if (Current_Canvas == Tower_Canvas)
                        Tower_Canvas_Up_Part.SetActive(true);

                    if (Current_Canvas != Tower_Canvas)
                        Tower_Canvas_Up_Part.SetActive(false);

                    Max_Height = max_Height;
                    Max_Buttom = max_Buttom;
                }
            }

            if (Mathf.Abs(deltaY) > this.swipeThreshold)
            {
                if (deltaY > 0 && Y > X)
                {
                    //this.OnSwipeUp.Invoke();
                    Debug.LogWarning("UP");
                    SwipeUp = true;
                }
                else if (deltaY < 0 && Y > X)
                {
                    //this.OnSwipeDown.Invoke();
                    Debug.LogWarning("Down");
                    SwipeDown = true;
                }
            }

            this.fingerUp = this.fingerDown;
        }
    }

    void Swipe_Up()
    {
        SwipeUp = true;
    }

    void Swipe_Down()
    {
        SwipeDown = true;
    }
    #endregion


    #region about Drag Canvas

    public void OnPointerClick(PointerEventData eventData)
    {
        float duration = (float)DateTime.Now.Subtract(StartFingerDownTime).TotalSeconds;

        float distanceX = ClickDown_Position.x - Input.mousePosition.x;
        float distanceY = ClickDown_Position.y - Input.mousePosition.y;
        float x = Mathf.Abs(distanceX);
        float y = Mathf.Abs(distanceY);

        if (x >= 50 || y >= 50)
            return;

        if (duration > 0.5f)
            return;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ClickDown_Canvas_Position = Current_Canvas.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        if (Short_Drag)
        {
            float distance = eventData.position.y - ClickDown_Position.y;
            float Y = ClickDown_Canvas_Position.y + distance; ;

            if (Y >= Max_Height)
                Y = Max_Height;
            if (Y <= Max_Buttom)
                Y = Max_Buttom;

            //Debug.Log("ClickDown_Canvas_Position.y || " + ClickDown_Canvas_Position.y);
            //Debug.Log("ClickDown_Position.y || " + ClickDown_Position.y);
            //Debug.Log("eventData.y || " + eventData.position.y);
            //Debug.Log("Short_Drag || " + Y);
            Current_Canvas.transform.localPosition = new Vector2(0, Y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    #endregion

}

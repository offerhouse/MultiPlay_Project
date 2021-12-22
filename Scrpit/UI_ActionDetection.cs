using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;

public class UI_ActionDetection : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject Current_Canvas;
    public GameObject Current_Move_Canvas;
    public GameObject Main_Canvas;
    public GameObject Main_Move_Canvas;
    public GameObject Tower_Select_Canvas;
    public GameObject Tower_Move_Select_Canvas;
    public GameObject Shop_Canvas;
    public GameObject Shop_Move_Canvas;
    public GameObject Award_Canvas;
    public GameObject Award_Move_Canvas;
    public GameObject Other_Battle_Canvas;
    public GameObject Other_Battle_Move_Canvas;

    float Max_Height, Max_Buttom, Height_Buffer, Buttom_Buffer;

    public float Swipe_Speed = 50;
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;

    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;
    private DateTime StartFingerDownTime;
    private Vector2 ClickDown_Position, ClickUp_Position, Drag_POS;
    private Vector2 Down_Canvas_Position, Up_Canvas_Position, Drag_Canvas_Position;

    public bool Mouse_Down = false, SwipeUp = false, SwipeRight = false, SwipeDown = false, SwipeLeft = false;

    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;

    private void Start()
    {
        Current_Canvas = Tower_Select_Canvas;
        Get_Current_Canvas_Max_Height_and_Buttom(out Max_Height, out Max_Buttom);
    }

    void Get_Current_Canvas_Max_Height_and_Buttom(out float Max_Height, out float Max_Buttom)
    {
        Debug.Log("Get_Current_Canvas_Max_Height_and_Buttom");
        Max_Height = 0; Max_Buttom = 0;
        if (Current_Canvas == Tower_Select_Canvas)
        {
            Current_Move_Canvas = Tower_Move_Select_Canvas;
            Current_Move_Canvas.transform.localPosition = new Vector2(0, -50f);

            Max_Height = 2400f;
            Max_Buttom = 100f;
            Height_Buffer = -20;
            Buttom_Buffer = -2300;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        if (Mouse_Down)
            if (Mouse_Down && duration > this.timeThreshold)
                Mouse_Down = false;

        if (SwipeUp)
        {
            Vector2 POS = new Vector3(0, 1250);
            Swipe_Canvas(POS, Max_Height, out SwipeUp);
        }
        if (SwipeDown)
        {
            Vector2 POS = new Vector3(0, -50);
            Swipe_Canvas(POS, Max_Buttom, out SwipeDown);
        }
    }

    void Swipe_Canvas(Vector2 POS, float Max_Value, out bool Allow_Swipe)
    {
        Allow_Swipe = true;
        if (Swipe_Speed == 0)
            Swipe_Speed = Get_Power();

        Swipe_Speed -= Time.deltaTime;

        Current_Move_Canvas.transform.localPosition = Vector2.Lerp(Current_Move_Canvas.transform.localPosition, POS, Swipe_Speed * Time.deltaTime);

        float Distance = Vector2.Distance(Current_Move_Canvas.transform.localPosition, POS);

        if (Swipe_Speed < 0.2f)
        {
            Swipe_Speed = 0;
            Allow_Swipe = false;
        }

        if (Distance < 20)
        {
            Swipe_Speed = 0;
            Allow_Swipe = false;
            Current_Move_Canvas.transform.localPosition = POS;
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
            if (Mathf.Abs(deltaX) > this.swipeThreshold)
            {
                if (deltaX > 0)
                {
                    this.OnSwipeRight.Invoke();
                    Debug.Log("right");
                }
                else if (deltaX < 0)
                {
                    this.OnSwipeLeft.Invoke();
                    Debug.Log("left");
                }
            }

            float deltaY = fingerDown.y - fingerUp.y;
            if (Mathf.Abs(deltaY) > this.swipeThreshold)
            {
                if (deltaY > 0)
                {
                    //this.OnSwipeUp.Invoke();
                    SwipeUp = true;
                    Debug.Log("up");
                }
                else if (deltaY < 0)
                {
                    //this.OnSwipeDown.Invoke();
                    SwipeDown = true;
                    Debug.Log("down");
                }
            }
            this.fingerUp = this.fingerDown;
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    void Swipe_Up()
    {
        SwipeUp = true;
    }

    void Swipe_Down()
    {
        SwipeDown = true;
    }

    #region about Drag Tower
    public void OnBeginDrag(PointerEventData eventData)
    {
        //m_collider.enabled = false;
        //Dragger = true;
        //Start_Dragging = true;

        Debug.Log("OnBeginDrag || " + ClickDown_Position);
        Down_Canvas_Position = Current_Move_Canvas.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData != null)
            Debug.Log("eventData || " + eventData.position);

        if (eventData == null)
            return;

        // Vector3.up makes it move in the world x/z plane.

        Debug.Log("OnDrag || " + Drag_POS);

        float distance = eventData.position.y - ClickDown_Position.y;
        float Y = Down_Canvas_Position.y + distance; ;

        if (Y >= 1200)
            Y = 1200;
        if (Y <= -50)
            Y = -50;

        Current_Move_Canvas.transform.localPosition = new Vector2(0, Y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //m_collider.enabled = true;
        //StartCoroutine("Check_Collider_Other_Tower");
    }

    public void Reset_Position()
    {
        //transform.position = Tower_Position;
    }

    IEnumerator Check_Collider_Other_Tower()
    {
        yield return new WaitForSeconds(0.3f);
        //if (!collider_hit_Tower)
        //{
        //    Reset_Position();
        //    Dragger = false;
        //    End_Drag = false;
        //    GameObject m_local_Manager = GameObject.Find("Local_Manager");
        //    m_local_Manager.GetComponent<Local_Manager>().End_Drag_Reset_Tower(gameObject, Type_Number, Combine_Up_Point);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<Tower>())
            return;

        //if (other.GetComponent<Tower>().Dragger)
        //    Drag_is_Dragger = true;

    }
    #endregion

}

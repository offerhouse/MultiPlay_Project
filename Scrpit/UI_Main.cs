using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Main : MonoBehaviour
{
    public bool UI_Main_Setup_Finish = false;
    public bool Player_Status_Setup_Finish = false;
    public GameObject UI_Battle_Canvas, UI_Tower_Select_Canvas, UI_Task_Canvas, UI_Shop_Canvas;

    public GameObject UI_Image_Info, Shop_Info, MasterCanvas;

    public UnityEvent Update_Property_Finish_Event;


    public void Update_Property_Finish()
    {
        if (!UI_Main_Setup_Finish)
            return;
        if (!GameObject.Find("Local_ProFile").GetComponent<Player_Status>().Player_Status_Load_Finish)
            return;
        Debug.Log("UI_Main Update_Property_Finish");
        Set_UI();
        Update_Property_Finish_Event?.Invoke();
    }

    public void Set_UI()
    {
        UI_Main_Setup_Finish = true;
    }

    public void Enable_Canvas()
    {
        Debug.Log("Enable_Canvas || " + gameObject.name);
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        MasterCanvas.SetActive(false);

        float alpha = GetComponent<CanvasGroup>().alpha;
        bool interactable = GetComponent<CanvasGroup>().interactable;
        bool blocksRaycasts = GetComponent<CanvasGroup>().blocksRaycasts;

        Debug.Log("Enable_Canvas_2 || " + alpha + " || " + interactable + " || " + blocksRaycasts);

    }

    public void Disable_Canvas()
    {
        Debug.Log("Disable_Canvas || " + gameObject.name);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}

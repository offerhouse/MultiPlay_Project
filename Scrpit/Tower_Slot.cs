using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Slot : MonoBehaviour
{
    public int Tower_Slot_Number;
    public int Group_Number;

    public GameObject Tower_Floor;

    public GameObject Neighbor_Top;
    public GameObject Neighbor_Right;
    public GameObject Neighbor_Buttom;
    public GameObject Neighbor_Left;

    public GameObject Road_Slot_Top;
    public GameObject Road_Slot_Right;
    public GameObject Road_Slot_Buttom;
    public GameObject Road_Slot_Left;

    [Header("Effect")]
    public GameObject Chain_Effect_Top;
    public GameObject Chain_Effect_Right;
    public GameObject Chain_Effect_Buttom;
    public GameObject Chain_Effect_Left;
    public GameObject Chain_Pillar_Top, Chain_Pillar_Right, Chain_Pillar_Buttom, Chain_Pillar_Left;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public float Timer;
    public GameObject Animation_Obj;
    int Step_Number = 0;
    Animator Anim;
    string Animation_Trigger;

    // Start is called before the first frame update
    void Start()
    {
        Anim = Animation_Obj.GetComponent<Animator>();
    }

    void Next_Step()
    {
        Anim.ResetTrigger(Animation_Trigger);
        Animation_Trigger = "Step_" + Step_Number.ToString();
        Step_Number += 1;
        Anim.SetTrigger(Animation_Trigger);
    }
}

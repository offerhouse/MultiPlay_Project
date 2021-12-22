using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Capture : MonoBehaviour
{

    public void Screen_Cap()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel.png");
    }

}

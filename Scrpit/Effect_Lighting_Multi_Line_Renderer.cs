using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Lighting_Multi_Line_Renderer : MonoBehaviour
{
    GameObject pool_Manager;
    public bool Strat_Light = false;

    Vector3 Start_POS;
    Vector3 End_POS;

    public GameObject Start_Point;
    public GameObject Target;
    public GameObject Line_Renderer_Main;
    public GameObject Effect_01;
    public GameObject Ground_Effect_01;
    LineRenderer Light_Main;
    public float Line_Render_Fix_Width = 0;

    public Material Material_01;
    public Material Material_02;
    public Material Material_03;
    public Material Material_04;

    public bool Animation_Texture;
    public float Line_Width;
    public float FramesPerSecond;
    float Timer;
    public float time = 1.0f;

    public float Random_Width_1, Random_Width_2;

    public void Active_Effect_01()
    {
        Effect_01.SetActive(true);
    }

    public void Set_Light_And_Start(GameObject start_point, GameObject target, int Type_Number)
    {
        Start_Point = start_point;
        Target = target;
        Light_Main = Line_Renderer_Main.GetComponent<LineRenderer>();
        Reset_Line();
        Start_POS = Start_Point.transform.position;
        Effect_01.transform.position = Start_POS;
        End_POS = target.transform.position;
        Ground_Effect_01.SetActive(true);
        Ground_Effect_01.transform.position = End_POS;

        Debug.Log("Line_Render_Set || Type_Number || " + Type_Number + " || " + Start_POS + " || " + End_POS + " || " 
            + target.transform.position);

        Set_Material();
        Strat_Light = true;

        pool_Manager = GameObject.Find("Pool_Manager");
    }

    public void Reset_Line()
    {
        Strat_Light = false;
        Light_Main.positionCount = 0;
        Set_Material();
    }

    void Set_Material()
    {
        int number = Random.Range(1, 5);
        switch (number)
        {
            case (1):
                Light_Main.material = Material_01;
                break;
            case (2):
                Light_Main.material = Material_02;
                break;
            case (3):
                Light_Main.material = Material_03;
                break;
            case (4):
                Light_Main.material = Material_04;
                break;
            default:
                Light_Main.material = Material_01;
                break;
        }
    }

    void Update()
    {
        if (Strat_Light)
        {
            if (Target == null || Start_Point == null)
                Destroy(gameObject);
            
            if (Target != null)
                End_POS = Target.transform.position;
            //Vector3 End_POS = End_Point.transform.localPosition;

            Debug.Log("Line_Render || " + Start_POS + " || " + End_POS);

            Light_Main.positionCount = 2;
            Light_Main.SetPosition(0, Start_POS);
            Light_Main.SetPosition(1, End_POS);

            float Distance = Vector3.Distance(Start_POS, End_POS);
            float Random_Width = Random.Range(Random_Width_1, Random_Width_2);
            if (Line_Render_Fix_Width == 0)
            {
                float width = Distance / (2 * Random_Width);
                Light_Main.startWidth = width;
                Light_Main.endWidth = width;
            }
            if (Line_Render_Fix_Width != 0)
            {
                Light_Main.startWidth = Line_Render_Fix_Width;
                Light_Main.endWidth = Line_Render_Fix_Width;
            }
            if (Animation_Texture)
            {
                int uvAnimationTileX = 3;
                int uvAnimationTileY = 3;
                float framesPerSecond = FramesPerSecond;

                Animate_Texture(uvAnimationTileX, uvAnimationTileY, framesPerSecond, Line_Renderer_Main);
            }

            Timer += Time.deltaTime;
            if (Timer > 0.5f)
                Light_Main.enabled = false;

            if (Timer > time)
            {
                Strat_Light = false;
                Timer = 0;
                pool_Manager.GetComponent<Pool_Manager>().Set_Obj_To_Pool_By_Name("Lightning_Line_Render", gameObject);
                GetComponent<Effect_Lighting_Multi_Line_Renderer>().enabled = false;
            }
        }
    }

    void Animate_Texture(int uvAnimationTileX, int uvAnimationTileY, float framesPerSecond, GameObject Line_Object)
    {
        // Calculate index
        int index = (int)(Time.time * framesPerSecond);
        // repeat when exhausting all frames
        index = index % (uvAnimationTileX * uvAnimationTileY);

        // Size of every tile
        var size = new Vector2(0.5f / uvAnimationTileX, 0.5f / uvAnimationTileY);

        // split into horizontal and vertical index
        var uIndex = index % uvAnimationTileX;
        var vIndex = index / uvAnimationTileX;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        var offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

        Line_Object.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        Line_Object.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
    }
}

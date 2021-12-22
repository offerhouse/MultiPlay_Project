using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Target;
    Vector3 POS;
    int Type;
    float Speed;
    bool Ready = false;
    bool Move_Bullet = false;
    bool Line_Render = false;
    string Enemy_Name = null;

    LineRenderer lineRenderer;
    Vector3 Start_Pos_Line_Render;
    int Frame_Number = 0;
    Local_Manager local_Manager;
    List<GameObject> Bullet_list, Fall_Explosion_Effect;

    bool Render_Lighiting = false;

    float Count_Render_Frame;

    public int End_Number;
    public int Damage;
    public float Lighting_Stun_Time;

    public int Character_Level;

    GameObject Line_Render_Object;
    GameObject Effect;

    // Line_Render_Snail

    public bool Strat_Light = false;
    GameObject Start_Point;
    GameObject End_Point;

    public Material Material_01;
    public Material Material_02;
    public Material Material_03;
    public Material Material_04;

    public bool Animation_Texture;
    public float Line_Width;
    public float FramesPerSecond;

    public GameObject Bullet_101, Bullet_102, Bullet_103, Bullet_104, Bullet_105;
    public GameObject Bullet_201, Bullet_202, Bullet_203, Bullet_204, Bullet_205, Bullet_206;
    public GameObject Bullet_301, Bullet_302, Bullet_303, Bullet_304, Bullet_305;
    public GameObject Bullet_401, Bullet_402, Bullet_403, Bullet_404, Bullet_405, Bullet_406;
    public GameObject Bullet_501, Bullet_502, Bullet_503, Bullet_504, Bullet_505, Bullet_506, Bullet_507;
    public GameObject Bullet_601, Bullet_602, Bullet_603, Bullet_604, Bullet_605, Bullet_606, Bullet_607, Bullet_608, Bullet_609, Bullet_610;
    public GameObject Bullet_611, Bullet_612;
    public GameObject Bullet_701, Bullet_702, Bullet_703, Bullet_704, Bullet_705, Bullet_706, Bullet_707, Bullet_708, Bullet_709, Bullet_710;
    public GameObject Bullet_711, Bullet_712, Bullet_713, Bullet_714, Bullet_715, Bullet_716, Bullet_717, Bullet_718, Bullet_719, Bullet_720;

    public GameObject Effect_101, Effect_102, Effect_103, Effect_104, Effect_105;
    public GameObject Effect_201, Effect_202, Effect_203, Effect_204, Effect_205, Effect_206;
    public GameObject Effect_301, Effect_302, Effect_303, Effect_304, Effect_305;
    public GameObject Effect_401, Effect_402, Effect_403, Effect_404, Effect_405, Effect_406;
    public GameObject Effect_501, Effect_502, Effect_503, Effect_504, Effect_505, Effect_506, Effect_507;
    public GameObject Effect_601, Effect_602, Effect_603, Effect_604, Effect_605, Effect_606, Effect_607, Effect_608, Effect_609, Effect_610;
    public GameObject Effect_611, Effect_612;
    public GameObject Effect_701, Effect_702, Effect_703, Effect_704, Effect_705, Effect_706, Effect_707, Effect_708, Effect_709, Effect_710;
    public GameObject Effect_711, Effect_712, Effect_713, Effect_714, Effect_715, Effect_716, Effect_717, Effect_718, Effect_719, Effect_720;

    public void Set_Bullet(GameObject target, int type, bool move_bullet, bool line_render, float speed, string enemy_name)
    {
        GameObject m_local_Manager = GameObject.Find("Local_Manager");
        local_Manager = m_local_Manager.GetComponent<Local_Manager>();
        GameObject pool_Manager = GameObject.Find("Pool_Manager");
        Bullet_list = pool_Manager.GetComponent<Pool_Manager>().Bullet_Pool;
        Fall_Explosion_Effect = m_local_Manager.GetComponent<Local_Manager>().Fall_Explosion_Effect;

        GameObject m_bullet = GameObject.Find("Bullet");
        gameObject.transform.SetParent(m_bullet.transform);

        //Debug.Log("Set_Bullet || type || " + type + " || speed || " + speed + " || line_render || " + line_render);
        //Debug.Log("POS || " + transform.position);
        Target = target;
        Type = type;
        Move_Bullet = move_bullet;
        Line_Render = line_render;
        Speed = speed;
        Ready = true;
        //Debug.Log("Set_Bullet || " + gameObject.name + " || " + type);
        GameObject bullet_Model = Get_Bullet(type);
        if (bullet_Model)
            bullet_Model.SetActive(true);
        POS = target.transform.position;
        Enemy_Name = enemy_name;
        if (Line_Render)
        {
            Render_Lighiting = true;
            Start_Pos_Line_Render = Vector3.zero;
            Line_Render_Object = Get_Bullet(type);
            lineRenderer = Line_Render_Object.GetComponent<LineRenderer>();
        }
        //Debug.Log("Set_Bullet_End");
    }

    GameObject Get_Bullet(int type)
    {
        switch (type)
        {
            case (101): return Bullet_101;
            case (102): return Bullet_102;
            case (103): return Bullet_103;
            case (104): return Bullet_104;
            case (105): return Bullet_105;
            case (201): return Bullet_201;
            case (202): return Bullet_202;
            case (203): return Bullet_203;
            case (204): return Bullet_204;
            case (205): return Bullet_205;
            case (206): return Bullet_206;
            case (301): return Bullet_301;
            case (302): return Bullet_302;
            case (303): return Bullet_303;
            case (304): return Bullet_304;
            case (305): return Bullet_305;
            case (401): return Bullet_401;
            case (402): return Bullet_402;
            case (403): return Bullet_403;
            case (404): return Bullet_404;
            case (405): return Bullet_405;
            case (406): return Bullet_406;
            case (501): return Bullet_501;
            case (502): return Bullet_502;
            case (503): return Bullet_503;
            case (504): return Bullet_504;
            case (505): return Bullet_505;
            case (506): return Bullet_506;
            case (507): return Bullet_507;
            case (601): return Bullet_601;
            case (602): return Bullet_602;
            case (603): return Bullet_603;
            case (604): return Bullet_604;
            case (605): return Bullet_605;
            case (606): return Bullet_606;
            case (607): return Bullet_607;
            case (608): return Bullet_608;
            case (609): return Bullet_609;
            case (610): return Bullet_610;
            case (611): return Bullet_611;
            case (612): return Bullet_612;
            case (701): return Bullet_701;
            case (702): return Bullet_702;
            case (703): return Bullet_703;
            case (704): return Bullet_704;
            case (705): return Bullet_705;
            case (706): return Bullet_706;
            case (707): return Bullet_707;
            case (708): return Bullet_708;
            case (709): return Bullet_709;
            case (710): return Bullet_710;
            case (711): return Bullet_711;
            case (712): return Bullet_712;
            case (713): return Bullet_713;
            case (714): return Bullet_714;
            case (715): return Bullet_715;
            case (716): return Bullet_716;
            case (717): return Bullet_717;
            case (718): return Bullet_718;
            case (719): return Bullet_719;
            case (720): return Bullet_720;
        }
        return null;
    }

    GameObject Get_Effect(int type)
    {
        switch (type)
        {
            case (101): return Effect_101;
            case (102): return Effect_102;
            case (103): return Effect_103;
            case (104): return Effect_104;
            case (105): return Effect_105;
            case (201): return Effect_201;
            case (202): return Effect_202;
            case (203): return Effect_203;
            case (204): return Effect_204;
            case (205): return Effect_205;
            case (206): return Effect_206;
            case (301): return Effect_301;
            case (302): return Effect_302;
            case (303): return Effect_303;
            case (304): return Effect_304;
            case (305): return Effect_305;
            case (401): return Effect_401;
            case (402): return Effect_402;
            case (403): return Effect_403;
            case (404): return Effect_404;
            case (405): return Effect_405;
            case (406): return Effect_406;
            case (501): return Effect_501;
            case (502): return Effect_502;
            case (503): return Effect_503;
            case (504): return Effect_504;
            case (505): return Effect_505;
            case (506): return Effect_506;
            case (507): return Effect_507;
            case (601): return Effect_601;
            case (602): return Effect_602;
            case (603): return Effect_603;
            case (604): return Effect_604;
            case (605): return Effect_605;
            case (606): return Effect_606;
            case (607): return Effect_607;
            case (608): return Effect_608;
            case (609): return Effect_609;
            case (610): return Effect_610;
            case (611): return Effect_611;
            case (612): return Effect_612;
            case (701): return Effect_701;
            case (702): return Effect_702;
            case (703): return Effect_703;
            case (704): return Effect_704;
            case (705): return Effect_705;
            case (706): return Effect_706;
            case (707): return Effect_707;
            case (708): return Effect_708;
            case (709): return Effect_709;
            case (710): return Effect_710;
            case (711): return Effect_711;
            case (712): return Effect_712;
            case (713): return Effect_713;
            case (714): return Effect_714;
            case (715): return Effect_715;
            case (716): return Effect_716;
            case (717): return Effect_717;
            case (718): return Effect_718;
            case (719): return Effect_719;
            case (720): return Effect_720;
        }
        return null;
    }

    private void Update()
    {
        if (Ready && Move_Bullet)
        {
            if (Target != null && Target.name == Enemy_Name)
                POS = Target.transform.position;

            Vector3 dir = POS - transform.position;
            transform.LookAt(POS);
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            float Distance = Vector3.Distance(transform.position, POS);

            if (Distance <= 1.0f && Distance > 0.3f)
                Speed = 10;

            if (Distance <= 0.3f)
            {
                Speed = 0;
                GameObject Effect = Get_Effect(Type);
                if (Type == 403 || Type == 503 || Type == 609) // Fall Attacker || Protector 
                {
                    Effect = Get_Effect(403);
                    POS = new Vector3(transform.position.x, 0f, transform.position.z);
                    GameObject effect = null;
                    if (Fall_Explosion_Effect.Count != 0)
                    {
                        effect = local_Manager.Get_Fall_Explosion_Effect_Form_Pool();
                        effect.transform.position = POS;
                        effect.SetActive(true);
                    }

                    if (!effect)
                    {
                        effect = Instantiate(Effect, POS, Quaternion.identity);
                        GameObject m_effect = GameObject.Find("Effect");
                        effect.transform.SetParent(m_effect.transform);
                    }

                    Vector3 Bullet_Look_POS = new Vector3(POS.x, 3.0f, POS.z);
                    effect.transform.LookAt(Bullet_Look_POS);
                    local_Manager.Add_Obj_To_Pool_By_Time(effect, 5.0f, "Explosion_Effect");
                }

                if (Effect != null && (Type != 403 && Type != 503 && Type != 609))
                {
                    POS = new Vector3(POS.x, 0.5f, POS.z);
                    GameObject m_Effect = Instantiate(Effect, POS, Quaternion.identity);
                    Destroy(m_Effect, 5f);
                }

                //Debug.Log("Bullet_Destroy Distance || " + Distance);
                Reset_to_Pool();
            }
        }

        if (Ready && Line_Render)
        {
            Debug.Log("Line_Render || " + Line_Render);
            if (Render_Lighiting)
            {
                Frame_Number++;
                Set_Line_Render(Frame_Number);
                End_Number = Frame_Number;
            }
            if (End_Number >= 9)
            {
                Render_Lighiting = false;
                lineRenderer.enabled = false;
                Reset_to_Pool();
            }
        }

        if (Strat_Light)
        {
            Debug.Log("Strat_Light || " + gameObject.name);
            Vector3 POS = new Vector3(Start_Point.transform.localPosition.x, Start_Point.transform.position.y, Start_Point.transform.localPosition.z);
            Vector3 Start_POS = POS;
            Vector3 End_POS = End_Point.transform.localPosition;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Start_POS);
            lineRenderer.SetPosition(1, End_POS);

            float Distance = Vector3.Distance(Start_POS, End_POS);
            float Random_Width = Random.Range(0.8f, 1.1f);
            float width = Distance / (2 * Random_Width);
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            if (Animation_Texture)
            {
                int uvAnimationTileX = 3;
                int uvAnimationTileY = 3;
                float framesPerSecond = FramesPerSecond;

                Animate_Texture(uvAnimationTileX, uvAnimationTileY, framesPerSecond);
            }
        }
    }

    void Set_Line_Render(int frame_number)
    {
        float Point_01_X = Start_Pos_Line_Render.x;
        float Point_01_Y = Start_Pos_Line_Render.y;
        float Point_01_Z = Start_Pos_Line_Render.z;
        int Count_Number = Set_Position_Count(frame_number);
        lineRenderer.positionCount = Count_Number;
        lineRenderer.SetPosition(0, Start_Pos_Line_Render);
        lineRenderer.SetPosition(1, Set_Vector3(Start_Pos_Line_Render, -2.0f));
        if (frame_number >= 1) { lineRenderer.SetPosition(2, Set_Vector3(Start_Pos_Line_Render, -4.0f)); }
        if (frame_number >= 2) { lineRenderer.SetPosition(3, Set_Vector3(Start_Pos_Line_Render, -6.0f)); }
        if (frame_number >= 3) { lineRenderer.SetPosition(4, Set_Vector3(Start_Pos_Line_Render, -8.0f)); }
        if (frame_number >= 4) { lineRenderer.SetPosition(5, Set_Vector3(Start_Pos_Line_Render, -9.0f)); }
        if (frame_number >= 5) { lineRenderer.SetPosition(6, Set_Vector3(Start_Pos_Line_Render, -10.0f)); }
        if (frame_number >= 6)
        {
            lineRenderer.SetPosition(7, Start_Pos_Line_Render + new Vector3(0, -10f, 0));
        }

        int uvAnimationTileX = 3;
        int uvAnimationTileY = 3;
        float framesPerSecond = 30.0f;

        Animate_Texture(uvAnimationTileX, uvAnimationTileY, framesPerSecond);

        Vector3 Set_Vector3(Vector3 start_pos, float Y_Number)
        {
            Vector3 POS = Vector3.zero;
            float random_number_X = Random.Range(-0.25f, 0.26f);
            float random_number_Y = Random.Range(-0.25f, 0.26f);
            float random_number_Z = Random.Range(-0.25f, 0.26f);
            POS = new Vector3(start_pos.x + random_number_X, start_pos.y + random_number_Y + Y_Number, start_pos.z + random_number_Z);
            return POS;
        }
    }

    int Set_Position_Count(int number)
    {
        int Count_number = 0;
        if (number >= 1) { Count_number = 3; }
        if (number >= 2) { Count_number = 4; }
        if (number >= 3) { Count_number = 5; }
        if (number >= 4) { Count_number = 6; }
        if (number >= 5) { Count_number = 7; }
        if (number >= 6) { Count_number = 8; }
        //if (number >= 8) { Count_number = 9; }
        //if (number >= 9) { Count_number = 10; }
        //if (number >= 10) { Count_number = 11; }
        //if (number >= 11) { Count_number = 12; }
        return Count_number;
    }

    void Animate_Texture(int uvAnimationTileX, int uvAnimationTileY, float framesPerSecond)
    {
        // Calculate index
        int index = (int)(Time.time * framesPerSecond);
        // repeat when exhausting all frames
        index = index % (uvAnimationTileX * uvAnimationTileY);

        // Size of every tile
        var size = new Vector2(1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);

        // split into horizontal and vertical index
        var uIndex = index % uvAnimationTileX;
        var vIndex = index / uvAnimationTileX;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        var offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

        Line_Render_Object.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        Line_Render_Object.GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
    }

    public void Set_Light_And_Start(GameObject start_Point, GameObject end_Point, float Damage)
    {
        lineRenderer = Line_Render_Object.GetComponent<LineRenderer>();
        Reset_Line();
        Start_Point = start_Point;
        End_Point = end_Point;

        Set_Material();
        Strat_Light = true;
    }

    public void Reset_Line()
    {
        Strat_Light = false;
        lineRenderer.positionCount = 0;
        Set_Material();
    }

    void Set_Material()
    {
        int number = Random.Range(1, 5);
        switch (number)
        {
            case (1):
                lineRenderer.material = Material_01;
                break;
            case (2):
                lineRenderer.material = Material_02;
                break;
            case (3):
                lineRenderer.material = Material_03;
                break;
            case (4):
                lineRenderer.material = Material_04;
                break;
            default:
                lineRenderer.material = Material_01;
                break;
        }
    }

    public void Send_To_Pool_By_Time(float Time)
    {
        StartCoroutine(Send_To_Pool(Time));
    }

    IEnumerator Send_To_Pool(float Time)
    {
        yield return new WaitForSeconds(Time);
        Reset_to_Pool();
    }

    public void Reset_to_Pool()
    {
        transform.rotation = Quaternion.identity;
        Target = null; POS = Vector3.zero; Type = 0; Speed = 0; Ready = false; Move_Bullet = false; Line_Render = false; Enemy_Name = null;
        Start_Pos_Line_Render = Vector3.zero; Frame_Number = 0; Render_Lighiting = false; Count_Render_Frame = 0; End_Number = 0;
        Damage = 0; Lighting_Stun_Time = 0; Character_Level = 0; Line_Render_Object = null; Effect = null; Strat_Light = false;
        Start_Point = null; End_Point = null; Material_01 = null; Material_02 = null; Material_03 = null; Material_04 = null;
        Animation_Texture = false; Line_Width = 0; FramesPerSecond = 0;

        GameObject[] Bullet_Array = new GameObject[]{
            Bullet_101,Bullet_102,Bullet_103,Bullet_104,Bullet_105,Bullet_201 , Bullet_202,Bullet_203,Bullet_204,Bullet_205,Bullet_206 ,
            Bullet_301,Bullet_302,Bullet_303,Bullet_304,Bullet_305 ,Bullet_401,Bullet_402,Bullet_403,Bullet_404,Bullet_405,Bullet_406 ,
            Bullet_501,Bullet_502,Bullet_503,Bullet_504,Bullet_505,Bullet_506 , Bullet_507 , Bullet_601,Bullet_602,Bullet_603 ,
            Bullet_604,Bullet_605,Bullet_606,Bullet_607,Bullet_608,Bullet_609,Bullet_610,Bullet_611,Bullet_612 , Bullet_701,Bullet_702,
            Bullet_703,Bullet_704,Bullet_705,Bullet_706 , Bullet_707,Bullet_708,Bullet_709,Bullet_710 , Bullet_711,Bullet_712,
            Bullet_713,Bullet_714,Bullet_715,Bullet_716 , Bullet_717,Bullet_718,Bullet_719,Bullet_720};

        for (int i = 0; i < Bullet_Array.Length; i++)
        {
            if (Bullet_Array[i] != null)
                if (Bullet_Array[i].activeSelf)
                    Bullet_Array[i].SetActive(false);
        }

        Bullet_list.Add(gameObject);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Info : MonoBehaviour
{
    public Tower_Info_Detail Basic_Detail;
    public Tower_Info_Detail Combine_Up_Detail;
    public Tower_Info_Detail Level_Up_Detail;

    public class Tower_Info_Detail
    {
        public float Damage;
        public float Speed;
        public float Distance;
        public float Add_Core;
        public float Core_Bouns_Rate; // Core Rate to Bouns Dmage , Bouns Money ... etc
        public float Special_Effect_Time;
        public float Basic_HP_For_Solider;
        public float Heal_HP;
        public float Special_Rate_1;
        public float Special_Rate_2;
        public float Special_Rate_3;
        public bool Summon_Bullet;
        public bool Move_Bullet;
        public bool Line_Render;

        public Tower_Info_Detail(float damage, float speed, float distance, float add_core_rate, float
            bouns_rate_from_core, float special_effect_time, float hp, float heal_hp, float rate_1, float rate_2, float rate_3,
            bool summon_bullet, bool move_bullet, bool line_render)
        {
            Damage = damage;
            Speed = speed;
            Distance = distance;
            Add_Core = add_core_rate;
            Core_Bouns_Rate = bouns_rate_from_core;
            Special_Effect_Time = special_effect_time;
            Basic_HP_For_Solider = hp;
            Heal_HP = heal_hp;
            Special_Rate_1 = rate_1;
            Special_Rate_2 = rate_2;
            Special_Rate_3 = rate_3;
            Summon_Bullet = summon_bullet;
            Move_Bullet = move_bullet;
            Line_Render = line_render;
        }
    }

    public Tower_Info_Detail Tower_Basic_Info(int number)
    {
        Tower_Info_Detail tower = null;
        switch (number)
        {
            case (101): tower = new Tower_Info_Detail(20, 3, 2, 1, 1, 3, 0, 0, 0, 0, 0, true, true, false); break;
            case (102): tower = new Tower_Info_Detail(10, 0.5f, 2, 1, 0.5f, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (103): tower = new Tower_Info_Detail(30, 5, 5, 1, 1.5f, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (104): tower = new Tower_Info_Detail(30, 5, 0, 1, 5, 0, 100, 0, 3, 3, 0, false, false, false); break;
            case (105): tower = new Tower_Info_Detail(0, 3, 5, 3, 3, 0, 0, 30, 0, 0, 0, false, false, false); break;

            case (201): tower = new Tower_Info_Detail(5, 3, 2, 2, 2, 10, 0, 0, 0, 0, 0, true, true, false); break;
            case (202): tower = new Tower_Info_Detail(30, 3, 0, 10, 10, 0, 100, 0, 5, 5, 0, false, false, false); break;
            case (203): tower = new Tower_Info_Detail(10, 3, 2, 1, 1, 10, 0, 0, 30, 0, 0, true, true, false); break;
            case (204): tower = new Tower_Info_Detail(20, 6, 2, 2, 2, 1, 0, 0, 2, 0, 0, true, true, false); break;
            case (205): tower = new Tower_Info_Detail(80, 10, 2, 2, 3, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (206): tower = new Tower_Info_Detail(5, 3, 2, 2, 1, 3, 0, 0, 10, 0, 0, true, true, false); break;

            case (301): tower = new Tower_Info_Detail(5, 3, 2, 2, 1, 1, 0, 0, 20, 0, 0, true, true, false); break;
            case (302): tower = new Tower_Info_Detail(5, 3, 2, 2, 2, 0, 0, 0, 3, 0, 0, true, true, false); break;
            case (303): tower = new Tower_Info_Detail(10, 3, 2, 2, 2, 0, 0, 0, 3, 0, 0, true, false, true); break;
            case (304): tower = new Tower_Info_Detail(0, 10, 0, 5, 1, 0, 0, 0, 5, 0, 0, false, false, false); break;
            case (305): tower = new Tower_Info_Detail(5, 3, 2, 2, 2, 3, 0, 0, 10, 0, 0, true, true, false); break;

            case (401): tower = new Tower_Info_Detail(15, 10, 0, 2, 1, 0, 0, 0, 0, 0, 0, true, false, true); break;
            case (402): tower = new Tower_Info_Detail(5, 10, 0, 2, 0.5f, 10, 0, 0, 15, 0, 0, false, false, false); break;
            case (403): tower = new Tower_Info_Detail(30, 5, 99, 1, 4, 1, 100, 0, 3, 2, 0, true, true, false); break;
            case (404): tower = new Tower_Info_Detail(0, 3, 0, 10, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (405): tower = new Tower_Info_Detail(0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (406): tower = new Tower_Info_Detail(50, 5, 99, 2, 2, 0, 0, 0, 0, 0, 0, true, false, false); break;

            case (501): tower = new Tower_Info_Detail(50, 5, 0, 8, 10, 0, 200, 0, 7, 7, 0, false, false, false); break;
            case (502): tower = new Tower_Info_Detail(50, 8, 0, 1, 5, 0, 200, 0, 5, 5, 0, false, false, false); break;
            case (503): tower = new Tower_Info_Detail(30, 3, 99, 8, 8, 1, 100, 0, 5, 5, 0, true, true, false); break;
            case (504): tower = new Tower_Info_Detail(0, 5, 0, 3, 0.1f, 1, 1, 0, 1, 10, 0, false, false, false); break;
            case (505): tower = new Tower_Info_Detail(5, 3, 2, 2, 2, 3, 0, 0, 1, 0, 0, false, false, false); break;
            case (506): tower = new Tower_Info_Detail(200, 5, 0, 2, 5, 0, 10, 0, 10, 1, 0, false, false, false); break;
            case (507): tower = new Tower_Info_Detail(100, 3, 99, 2, 2, 0, 0, 0, 0, 0, 0, true, false, true); break;

            case (601): tower = new Tower_Info_Detail(5, 0.5f, 0, 1, 1, 0, 0, 0, 0.3f, 0, 0, false, false, false); break;
            case (602): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 100, 10, 0, false, false, false); break;
            case (603): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 20, 10, 0, false, false, false); break;
            case (604): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, false, false, false); break;
            case (605): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 10, 0, 0, false, false, false); break;
            case (606): tower = new Tower_Info_Detail(80, 8, 0, 4, 10, 0, 300, 0, 9, 9, 0, false, false, false); break;
            case (607): tower = new Tower_Info_Detail(80, 12, 0, 1, 5, 0, 300, 0, 7, 7, 0, false, false, false); break;
            case (608): tower = new Tower_Info_Detail(80, 8, 99, 4, 10, 0, 300, 0, 9, 9, 0, false, false, false); break;
            case (609): tower = new Tower_Info_Detail(50, 5, 99, 6, 8, 0, 200, 0, 6, 6, 0, true, true, false); break;
            case (610): tower = new Tower_Info_Detail(5, 3, 2, 1, 1, 0, 0, 0, 10, 0, 0, true, true, false); break;
            case (611): tower = new Tower_Info_Detail(5, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (612): tower = new Tower_Info_Detail(100, 3, 3, 2, 2, 0, 0, 0, 0, 0, 0, true, false, false); break;

            case (701): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, false, false, false); break;
            case (702): tower = new Tower_Info_Detail(0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, false, false, false); break;
            case (703): tower = new Tower_Info_Detail(5, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (704): tower = new Tower_Info_Detail(0, 1, 0, 20, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (705): tower = new Tower_Info_Detail(0, 1, 0, 10, 1, 0, 0, 0, 10, 0, 0, false, false, false); break;
            case (706): tower = new Tower_Info_Detail(0, 1, 0, 10, 0, 0, 0, 0, 50, 0, 0, false, false, false); break;
            case (707): tower = new Tower_Info_Detail(5, 3, 3, 2, 2, 0, 0, 0, 1, 0, 0, true, true, false); break;
            case (708): tower = new Tower_Info_Detail(5, 3, 3, 2, 2, 0, 0, 0, 0.5f, 0, 0, true, true, false); break;
            case (709): tower = new Tower_Info_Detail(0, 0, 0, 3, 3, 0, 0, 0, 15, 10, 0, false, false, false); break;
            case (710): tower = new Tower_Info_Detail(0, 0, 0, 3, 3, 0, 0, 0, 15, 10, 0, false, false, false); break;
            case (711): tower = new Tower_Info_Detail(0, 1, 0, 3, 3, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (712): tower = new Tower_Info_Detail(0, 10, 0, 3, 3, 0, 0, 30, 0, 0, 0, false, false, false); break;
            case (713): tower = new Tower_Info_Detail(0, 0, 0, 3, 3, 0, 400, 0, 100, 9, 9, false, false, false); break;
            case (714): tower = new Tower_Info_Detail(0, 0, 0, 3, 3, 0, 400, 0, 20, 9, 9, false, false, false); break;
            case (715): tower = new Tower_Info_Detail(5, 1, 3, 3, 3, 0, 0, 100, 10, 10, 0, false, false, false); break;
            case (716): tower = new Tower_Info_Detail(0, 0, 0, 1, 0, 0, 0, 0, 500, 0, 0, false, false, false); break;
            case (717): tower = new Tower_Info_Detail(0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (718): tower = new Tower_Info_Detail(0, 3, 0, 3, 3, 0, 0, 0, 1, 1, 0, false, false, false); break;
            case (719): tower = new Tower_Info_Detail(30, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, true, true, false); break;
        }
        return tower;
        // DMG , speed , distance , add core/s , bouns rate from core , Special effect time , HP , Heal HP , rate1 , rate2, rate3
    }

    public Tower_Info_Detail Tower_Comine_Up_Detail(int number)
    {
        Tower_Info_Detail tower = null;
        switch (number)
        {
            case (101): tower = new Tower_Info_Detail(20, -0.1f, 0.1f, 1, 1, 0.1f, 0, 0, 0, 0, 0, true, true, false); break;
            case (102): tower = new Tower_Info_Detail(10, -0.01f, 0.1f, 1, 0.5f, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (103): tower = new Tower_Info_Detail(30, -0.1f, 0.1f, 1, 1, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (104): tower = new Tower_Info_Detail(30, -0.1f, 0, 1, 1, 0, 100, 0, 1, 1, 0, false, false, false); break;
            case (105): tower = new Tower_Info_Detail(0, -0.1f, 0.1f, 2, 2, 0, 0, 100, 0, 0, 0, false, false, false); break;

            case (201): tower = new Tower_Info_Detail(1, -0.1f, 0.1f, 1, 1, 1, 0, 0, 0, 0, 0, true, true, false); break;
            case (202): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 2, 0, 130, 0, 2, 2, 0, false, false, false); break;
            case (203): tower = new Tower_Info_Detail(10, -0.1f, 0.1f, 1, 1, 1, 0, 0, 2, 0, 0, true, true, false); break;
            case (204): tower = new Tower_Info_Detail(20, -0.1f, 0.1f, 1, 1, 0.1f, 0, 0, 0.1f, 0, 0, true, true, false); break;
            case (205): tower = new Tower_Info_Detail(80, -0.2f, 0.1f, 2, 2, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (206): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 0.2f, 0.1f, 0.2f, 0, 0, 1, 0, 0, true, true, false); break;

            case (301): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 0.2f, 0.1f, 0.1f, 0, 0, 1, 0, 0, true, true, false); break;
            case (302): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 0.2f, 0.1f, 0, 0, 0, 1, 0, 0, true, true, false); break;
            case (303): tower = new Tower_Info_Detail(10, -0.1f, 0.1f, 0.2f, 0.1f, 0, 0, 0, 1, 0, 0, true, false, true); break;
            case (304): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 1, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (305): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 0.2f, 0.1f, 0.2f, 0, 0, 2, 0, 0, true, true, false); break;

            case (401): tower = new Tower_Info_Detail(15, -0.1f, 0, 1, 0.5f, 0, 0, 0, 0, 0, 0, true, false, true); break;
            case (402): tower = new Tower_Info_Detail(5, -0.1f, 0, 1, 0.5f, 1, 0, 0, 2, 0, 0, false, false, false); break;
            case (403): tower = new Tower_Info_Detail(30, -0.1f, 0, 1, 1, 0, 100, 0, 1, 1, 0, false, false, false); break;
            case (404): tower = new Tower_Info_Detail(0, -0.1f, 0, 3, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (405): tower = new Tower_Info_Detail(0, -0.1f, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (406): tower = new Tower_Info_Detail(50, -0.1f, 0.1f, 2, 2, 0, 0, 0, 0, 0, 0, true, false, true); break;

            case (501): tower = new Tower_Info_Detail(50, -0.1f, 0, 1, 1, 0, 230, 0, 2, 2, 0, false, false, false); break;
            case (502): tower = new Tower_Info_Detail(50, -0.1f, 0, 0.8f, 0.8f, 0, 200, 0, 1, 1, 0, false, false, false); break;
            case (503): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 2, 0, 130, 0, 2, 2, 0, false, false, false); break;
            case (504): tower = new Tower_Info_Detail(0, -0.1f, 0, 1, 0.1f, -0.03f, 0, 0, 1, 2, 0, false, false, false); break;
            case (505): tower = new Tower_Info_Detail(1, -0.1f, 0.1f, 2, 2, 0.5f, 0, 0, 0.1f, 0, 0, false, false, false); break;
            case (506): tower = new Tower_Info_Detail(200, -0.1f, 0, 1, 1, 0, 10, 0, 3, 1, 0, false, false, false); break;
            case (507): tower = new Tower_Info_Detail(100, -0.1f, 0, 2, 2, 0, 0, 0, 0, 0, 0, true, false, true); break;

            case (601): tower = new Tower_Info_Detail(5, 0, 0, 1, 1, 0, 0, 0, 0.03f, 0, 0, false, false, false); break;
            case (602): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, -1, 20, 0, false, false, false); break;
            case (603): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, -1, 20, 0, false, false, false); break;
            case (604): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (605): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (606): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.8f, 0.8f, 0, 300, 0, 3, 3, 0, false, false, false); break;
            case (607): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.6f, 0.6f, 0, 100, 0, 1, 1, 0, false, false, false); break;
            case (608): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.8f, 0.8f, 0, 300, 0, 3, 3, 0, false, false, false); break;
            case (609): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 1, 0, 230, 0, 2, 2, 0, true, true, false); break;
            case (610): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0, 0, 0, 1, 0, 0, true, true, false); break;
            case (611): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 1, 1, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (612): tower = new Tower_Info_Detail(100, -0.1f, 0.1f, 2, 1, 0, 0, 0, 0, 0, 0, true, false, false); break;

            case (701): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (702): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (703): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 1, 1, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (704): tower = new Tower_Info_Detail(0, -0.03f, 0, 5, 0, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (705): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 2, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (706): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 0, 0, 0, 0, -1, 0, 0, false, false, false); break;
            case (707): tower = new Tower_Info_Detail(5, -0.05f, 0.1f, 2, 2, 0, 0, 0, 0.5f, 0, 0, true, true, false); break;
            case (708): tower = new Tower_Info_Detail(5, -0.05f, 0.1f, 2, 2, 0, 0, 0, 0.3f, 0, 0, true, true, false); break;
            case (709): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 0, 0, -0.5f, 1, 0, false, false, false); break;
            case (710): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 0, 0, -0.5f, 1, 0, false, false, false); break;
            case (711): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 2, 0, 0, 0, -0.02f, 0, 0, false, false, false); break;
            case (712): tower = new Tower_Info_Detail(0, -0.1f, 0, 2, 2, 0, 0, 30, 0, 0, 0, false, false, false); break;
            case (713): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 400, 0, -1, 2, 2, false, false, false); break;
            case (714): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 400, 0, -1, 2, 2, false, false, false); break;
            case (715): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 2, 0, 0, 100, 10, 10, 0, false, false, false); break;
            case (716): tower = new Tower_Info_Detail(0, 0, 0, 1, 0, 0, 0, 0, -10, 0, 0, false, false, false); break;
            case (717): tower = new Tower_Info_Detail(0, -1.0f, 0, 3, 3, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (718): tower = new Tower_Info_Detail(0, -0.1f, 0, 2, 2, 0, 0, 0, -0.03f, 1, 0, false, false, false); break;
            case (719): tower = new Tower_Info_Detail(30, -0.1f, 2, 2, 2, 0, 0, 0, 0, 0, 0, true, true, false); break;
        }
        return tower;
        // DMG , speed , distance , add core/s , bouns rate from core , Special effect time , HP , Heal HP , rate1 , rate2, rate3
    }

    public Tower_Info_Detail Tower_Level_Up_Detail(int number)
    {
        Tower_Info_Detail tower = null;
        switch (number)
        {
            case (101): tower = new Tower_Info_Detail(20, -0.1f, 0.1f, 1, 1, 0.1f, 0, 0, 0, 0, 0, true, true, false); break;
            case (102): tower = new Tower_Info_Detail(10, -0.01f, 0.1f, 1, 0.5f, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (103): tower = new Tower_Info_Detail(30, -0.1f, 0.1f, 1, 1.5f, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (104): tower = new Tower_Info_Detail(30, -0.1f, 0, 1, 1, 0, 100, 0, 1, 1, 0, false, false, false); break;
            case (105): tower = new Tower_Info_Detail(0, -0.1f, 0.1f, 3, 1, 0, 0, 30, 0, 0, 0, false, false, false); break;

            case (201): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0.5f, 0, 0, 0, 0, 0, true, true, false); break;
            case (202): tower = new Tower_Info_Detail(30, -0.1f, 0, 3, 2, 0, 100, 0, 5, 5, 0, false, false, false); break;
            case (203): tower = new Tower_Info_Detail(10, -0.1f, 0.1f, 1, 1, 0.5f, 0, 0, 1, 0, 0, true, true, false); break;
            case (204): tower = new Tower_Info_Detail(20, -0.1f, 0.1f, 2, 1, 0.1f, 0, 0, 2, 0, 0, true, true, false); break;
            case (205): tower = new Tower_Info_Detail(80, -0.3f, 0.1f, 2, 1, 0, 0, 0, 0, 0, 0, true, false, false); break;
            case (206): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0.1f, 0, 0, 1, 0, 0, true, true, false); break;

            case (301): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0.1f, 0, 0, 1, 0, 0, true, true, false); break;
            case (302): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0, 0, 0, 1, 0, 0, true, true, false); break;
            case (303): tower = new Tower_Info_Detail(10, -0.1f, 0.1f, 2, 1, 0, 0, 0, 1, 0, 0, true, false, true); break;
            case (304): tower = new Tower_Info_Detail(0, -0.03f, 0, 5, 1, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (305): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0.1f, 0, 0, 1, 0, 0, true, true, false); break;

            case (401): tower = new Tower_Info_Detail(15, -0.1f, 0, 2, 1, 0, 0, 0, 0, 0, 0, true, false, true); break;
            case (402): tower = new Tower_Info_Detail(5, -0.1f, 0, 2, 0.5f, 1, 0, 0, 15, 0, 0, false, false, false); break;
            case (403): tower = new Tower_Info_Detail(30, -0.1f, 0, 1, 1, 0.1f, 100, 0, 1, 1, 0, true, false, false); break;
            case (404): tower = new Tower_Info_Detail(0, -0.1f, 0, 10, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (405): tower = new Tower_Info_Detail(0, -0.1f, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (406): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 1, 0, 0, 0, 0, 0, 0, true, false, true); break;

            case (501): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 2, 0, 230, 0, 5, 5, 0, false, false, false); break;
            case (502): tower = new Tower_Info_Detail(50, -0.1f, 0, 0.8f, 0.8f, 0, 200, 0, 1, 1, 0, false, false, false); break;
            case (503): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 2, 0, 130, 0, 2, 2, 0, true, false, false); break;
            case (504): tower = new Tower_Info_Detail(0, -0.1f, 0, 1, 0.1f, -0.03f, 0, 0, 1, 2, 0, false, false, false); break;
            case (505): tower = new Tower_Info_Detail(1, -0.1f, 0.1f, 2, 2, 0.5f, 0, 0, 0.1f, 0, 0, false, false, false); break;
            case (506): tower = new Tower_Info_Detail(200, -0.1f, 0, 1, 1, 0, 10, 0, 3, 1, 0, false, false, false); break;
            case (507): tower = new Tower_Info_Detail(100, -0.1f, 0, 2, 2, 0, 0, 0, 0, 0, 0, true, false, true); break;

            case (601): tower = new Tower_Info_Detail(5, 0, 0, 1, 1, 0, 0, 0, 0.03f, 0, 0, false, false, false); break;
            case (602): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, -1, 20, 0, false, false, false); break;
            case (603): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, -1, 20, 0, false, false, false); break;
            case (604): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (605): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, false, false, false); break;
            case (606): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.8f, 0.8f, 0, 300, 0, 3, 3, 0, false, false, false); break;
            case (607): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.6f, 0.6f, 0, 100, 0, 1, 1, 0, false, false, false); break;
            case (608): tower = new Tower_Info_Detail(80, -0.1f, 0, 0.8f, 0.8f, 0, 300, 0, 3, 3, 0, false, false, false); break;
            case (609): tower = new Tower_Info_Detail(50, -0.1f, 0, 2, 1, 0, 230, 0, 2, 2, 0, true, true, false); break;
            case (610): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 1, 0, 0, 0, 1, 0, 0, true, true, false); break;
            case (611): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 1, 1, 0, 0, 0, 0, 0, 0, true, true, false); break;
            case (612): tower = new Tower_Info_Detail(100, -0.1f, 0.1f, 2, 1, 0, 0, 0, 0, 0, 0, true, false, false); break;

            case (701): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (702): tower = new Tower_Info_Detail(0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, false, false, false); break;
            case (703): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 1, 1, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (704): tower = new Tower_Info_Detail(0, -0.03f, 0, 5, 0, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (705): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 2, 0, 0, 0, 1, 0, 0, false, false, false); break;
            case (706): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 0, 0, 0, 0, -1, 0, 0, false, false, false); break;
            case (707): tower = new Tower_Info_Detail(5, -0.05f, 0.1f, 2, 2, 0, 0, 0, 0.5f, 0, 0, true, true, false); break;
            case (708): tower = new Tower_Info_Detail(5, -0.05f, 0.1f, 2, 2, 0, 0, 0, 0.3f, 0, 0, true, true, false); break;
            case (709): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 0, 0, -0.5f, 1, 0, false, false, false); break;
            case (710): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 0, 0, -0.5f, 1, 0, false, false, false); break;
            case (711): tower = new Tower_Info_Detail(0, -0.03f, 0, 2, 2, 0, 0, 0, -0.02f, 0, 0, false, false, false); break;
            case (712): tower = new Tower_Info_Detail(0, -0.1f, 0, 2, 2, 0, 0, 30, 0, 0, 0, false, false, false); break;
            case (713): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 400, 0, -1, 2, 2, false, false, false); break;
            case (714): tower = new Tower_Info_Detail(0, 0, 0, 2, 2, 0, 400, 0, -1, 2, 2, false, false, false); break;
            case (715): tower = new Tower_Info_Detail(5, -0.1f, 0.1f, 2, 2, 0, 0, 100, 10, 10, 0, false, false, false); break;
            case (716): tower = new Tower_Info_Detail(0, 0, 0, 1, 0, 0, 0, 0, -10, 0, 0, false, false, false); break;
            case (717): tower = new Tower_Info_Detail(0, -1.0f, 0, 3, 3, 0, 0, 0, 0, 0, 0, false, false, false); break;
            case (718): tower = new Tower_Info_Detail(0, -0.1f, 0, 2, 2, 0, 0, 0, -0.03f, 1, 0, false, false, false); break;
            case (719): tower = new Tower_Info_Detail(30, -0.1f, 2, 2, 2, 0, 0, 0, 0, 0, 0, true, true, false); break;
        }
        return tower;
        // DMG , speed , distance , add core/s , bouns rate from core , Special effect time , HP , Heal HP , rate1 , rate2, rate3
    }

    public int Get_Tower_Level_Form_PlayerStatus(int tower_Number, Player_Status.Player player)
    {
        int level = 0;
        switch (tower_Number)
        {
            case (101): return player.Tower_101_Level;
            case (102): return player.Tower_102_Level;
            case (103): return player.Tower_103_Level;
            case (104): return player.Tower_104_Level;
            case (105): return player.Tower_105_Level;
            case (106): return player.Tower_106_Level;
            case (107): return player.Tower_107_Level;
            case (108): return player.Tower_108_Level;
            case (109): return player.Tower_109_Level;
            case (110): return player.Tower_110_Level;
            case (111): return player.Tower_111_Level;
            case (112): return player.Tower_112_Level;
            case (113): return player.Tower_113_Level;
            case (114): return player.Tower_114_Level;
            case (115): return player.Tower_115_Level;
            case (116): return player.Tower_116_Level;
            case (117): return player.Tower_117_Level;
            case (118): return player.Tower_118_Level;
            case (119): return player.Tower_119_Level;
            case (120): return player.Tower_120_Level;

            case (201): return player.Tower_201_Level;
            case (202): return player.Tower_202_Level;
            case (203): return player.Tower_203_Level;
            case (204): return player.Tower_204_Level;
            case (205): return player.Tower_205_Level;
            case (206): return player.Tower_206_Level;
            case (207): return player.Tower_207_Level;
            case (208): return player.Tower_208_Level;
            case (209): return player.Tower_209_Level;
            case (210): return player.Tower_210_Level;
            case (211): return player.Tower_211_Level;
            case (212): return player.Tower_212_Level;
            case (213): return player.Tower_213_Level;
            case (214): return player.Tower_214_Level;
            case (215): return player.Tower_215_Level;
            case (216): return player.Tower_216_Level;
            case (217): return player.Tower_217_Level;
            case (218): return player.Tower_218_Level;
            case (219): return player.Tower_219_Level;
            case (220): return player.Tower_220_Level;

            case (301): return player.Tower_301_Level;
            case (302): return player.Tower_302_Level;
            case (303): return player.Tower_303_Level;
            case (304): return player.Tower_304_Level;
            case (305): return player.Tower_305_Level;
            case (306): return player.Tower_306_Level;
            case (307): return player.Tower_307_Level;
            case (308): return player.Tower_308_Level;
            case (309): return player.Tower_309_Level;
            case (310): return player.Tower_310_Level;
            case (311): return player.Tower_311_Level;
            case (312): return player.Tower_312_Level;
            case (313): return player.Tower_313_Level;
            case (314): return player.Tower_314_Level;
            case (315): return player.Tower_315_Level;
            case (316): return player.Tower_316_Level;
            case (317): return player.Tower_317_Level;
            case (318): return player.Tower_318_Level;
            case (319): return player.Tower_319_Level;
            case (320): return player.Tower_320_Level;

            case (401): return player.Tower_401_Level;
            case (402): return player.Tower_402_Level;
            case (403): return player.Tower_403_Level;
            case (404): return player.Tower_404_Level;
            case (405): return player.Tower_405_Level;
            case (406): return player.Tower_406_Level;
            case (407): return player.Tower_407_Level;
            case (408): return player.Tower_408_Level;
            case (409): return player.Tower_409_Level;
            case (410): return player.Tower_410_Level;
            case (411): return player.Tower_411_Level;
            case (412): return player.Tower_412_Level;
            case (413): return player.Tower_413_Level;
            case (414): return player.Tower_414_Level;
            case (415): return player.Tower_415_Level;
            case (416): return player.Tower_416_Level;
            case (417): return player.Tower_417_Level;
            case (418): return player.Tower_418_Level;
            case (419): return player.Tower_419_Level;
            case (420): return player.Tower_420_Level;

            case (501): return player.Tower_501_Level;
            case (502): return player.Tower_502_Level;
            case (503): return player.Tower_503_Level;
            case (504): return player.Tower_504_Level;
            case (505): return player.Tower_505_Level;
            case (506): return player.Tower_506_Level;
            case (507): return player.Tower_507_Level;
            case (508): return player.Tower_508_Level;
            case (509): return player.Tower_509_Level;
            case (510): return player.Tower_510_Level;
            case (511): return player.Tower_511_Level;
            case (512): return player.Tower_512_Level;
            case (513): return player.Tower_513_Level;
            case (514): return player.Tower_514_Level;
            case (515): return player.Tower_515_Level;
            case (516): return player.Tower_516_Level;
            case (517): return player.Tower_517_Level;
            case (518): return player.Tower_518_Level;
            case (519): return player.Tower_519_Level;
            case (520): return player.Tower_520_Level;

            case (601): return player.Tower_601_Level;
            case (602): return player.Tower_602_Level;
            case (603): return player.Tower_603_Level;
            case (604): return player.Tower_604_Level;
            case (605): return player.Tower_605_Level;
            case (606): return player.Tower_606_Level;
            case (607): return player.Tower_607_Level;
            case (608): return player.Tower_608_Level;
            case (609): return player.Tower_609_Level;
            case (610): return player.Tower_610_Level;
            case (611): return player.Tower_611_Level;
            case (612): return player.Tower_612_Level;
            case (613): return player.Tower_613_Level;
            case (614): return player.Tower_614_Level;
            case (615): return player.Tower_615_Level;
            case (616): return player.Tower_616_Level;
            case (617): return player.Tower_617_Level;
            case (618): return player.Tower_618_Level;
            case (619): return player.Tower_619_Level;
            case (620): return player.Tower_620_Level;
            case (621): return player.Tower_621_Level;
            case (622): return player.Tower_622_Level;
            case (623): return player.Tower_623_Level;
            case (624): return player.Tower_624_Level;
            case (625): return player.Tower_625_Level;
            case (626): return player.Tower_626_Level;
            case (627): return player.Tower_627_Level;
            case (628): return player.Tower_628_Level;
            case (629): return player.Tower_629_Level;
            case (630): return player.Tower_630_Level;

            case (701): return player.Tower_701_Level;
            case (702): return player.Tower_702_Level;
            case (703): return player.Tower_703_Level;
            case (704): return player.Tower_704_Level;
            case (705): return player.Tower_705_Level;
            case (706): return player.Tower_706_Level;
            case (707): return player.Tower_707_Level;
            case (708): return player.Tower_708_Level;
            case (709): return player.Tower_709_Level;
            case (710): return player.Tower_710_Level;
            case (711): return player.Tower_711_Level;
            case (712): return player.Tower_712_Level;
            case (713): return player.Tower_713_Level;
            case (714): return player.Tower_714_Level;
            case (715): return player.Tower_715_Level;
            case (716): return player.Tower_716_Level;
            case (717): return player.Tower_717_Level;
            case (718): return player.Tower_718_Level;
            case (719): return player.Tower_719_Level;
            case (720): return player.Tower_720_Level;
            case (721): return player.Tower_721_Level;
            case (722): return player.Tower_722_Level;
            case (723): return player.Tower_723_Level;
            case (724): return player.Tower_724_Level;
            case (725): return player.Tower_725_Level;
            case (726): return player.Tower_726_Level;
            case (727): return player.Tower_727_Level;
            case (728): return player.Tower_728_Level;
            case (729): return player.Tower_729_Level;
            case (730): return player.Tower_730_Level;
            case (731): return player.Tower_731_Level;
            case (732): return player.Tower_732_Level;
            case (733): return player.Tower_733_Level;
            case (734): return player.Tower_734_Level;
            case (735): return player.Tower_735_Level;
            case (736): return player.Tower_736_Level;
            case (737): return player.Tower_737_Level;
            case (738): return player.Tower_738_Level;
            case (739): return player.Tower_739_Level;
            case (740): return player.Tower_740_Level;
            case (741): return player.Tower_741_Level;
            case (742): return player.Tower_742_Level;
            case (743): return player.Tower_743_Level;
            case (744): return player.Tower_744_Level;
            case (745): return player.Tower_745_Level;
            case (746): return player.Tower_746_Level;
            case (747): return player.Tower_747_Level;
            case (748): return player.Tower_748_Level;
            case (749): return player.Tower_749_Level;
            case (750): return player.Tower_750_Level;
        }
        return level;
    }

    public int Get_Tower_Exp_Form_PlayerStatus(int tower_Number, Player_Status.Player player)
    {
        int EXP = 0;
        switch (tower_Number)
        {
            case (101): return player.Tower_101_EXP;
            case (102): return player.Tower_102_EXP;
            case (103): return player.Tower_103_EXP;
            case (104): return player.Tower_104_EXP;
            case (105): return player.Tower_105_EXP;
            case (106): return player.Tower_106_EXP;
            case (107): return player.Tower_107_EXP;
            case (108): return player.Tower_108_EXP;
            case (109): return player.Tower_109_EXP;
            case (110): return player.Tower_110_EXP;
            case (111): return player.Tower_111_EXP;
            case (112): return player.Tower_112_EXP;
            case (113): return player.Tower_113_EXP;
            case (114): return player.Tower_114_EXP;
            case (115): return player.Tower_115_EXP;
            case (116): return player.Tower_116_EXP;
            case (117): return player.Tower_117_EXP;
            case (118): return player.Tower_118_EXP;
            case (119): return player.Tower_119_EXP;
            case (120): return player.Tower_120_EXP;

            case (201): return player.Tower_201_EXP;
            case (202): return player.Tower_202_EXP;
            case (203): return player.Tower_203_EXP;
            case (204): return player.Tower_204_EXP;
            case (205): return player.Tower_205_EXP;
            case (206): return player.Tower_206_EXP;
            case (207): return player.Tower_207_EXP;
            case (208): return player.Tower_208_EXP;
            case (209): return player.Tower_209_EXP;
            case (210): return player.Tower_210_EXP;
            case (211): return player.Tower_211_EXP;
            case (212): return player.Tower_212_EXP;
            case (213): return player.Tower_213_EXP;
            case (214): return player.Tower_214_EXP;
            case (215): return player.Tower_215_EXP;
            case (216): return player.Tower_216_EXP;
            case (217): return player.Tower_217_EXP;
            case (218): return player.Tower_218_EXP;
            case (219): return player.Tower_219_EXP;
            case (220): return player.Tower_220_EXP;

            case (301): return player.Tower_301_EXP;
            case (302): return player.Tower_302_EXP;
            case (303): return player.Tower_303_EXP;
            case (304): return player.Tower_304_EXP;
            case (305): return player.Tower_305_EXP;
            case (306): return player.Tower_306_EXP;
            case (307): return player.Tower_307_EXP;
            case (308): return player.Tower_308_EXP;
            case (309): return player.Tower_309_EXP;
            case (310): return player.Tower_310_EXP;
            case (311): return player.Tower_311_EXP;
            case (312): return player.Tower_312_EXP;
            case (313): return player.Tower_313_EXP;
            case (314): return player.Tower_314_EXP;
            case (315): return player.Tower_315_EXP;
            case (316): return player.Tower_316_EXP;
            case (317): return player.Tower_317_EXP;
            case (318): return player.Tower_318_EXP;
            case (319): return player.Tower_319_EXP;
            case (320): return player.Tower_320_EXP;

            case (401): return player.Tower_401_EXP;
            case (402): return player.Tower_402_EXP;
            case (403): return player.Tower_403_EXP;
            case (404): return player.Tower_404_EXP;
            case (405): return player.Tower_405_EXP;
            case (406): return player.Tower_406_EXP;
            case (407): return player.Tower_407_EXP;
            case (408): return player.Tower_408_EXP;
            case (409): return player.Tower_409_EXP;
            case (410): return player.Tower_410_EXP;
            case (411): return player.Tower_411_EXP;
            case (412): return player.Tower_412_EXP;
            case (413): return player.Tower_413_EXP;
            case (414): return player.Tower_414_EXP;
            case (415): return player.Tower_415_EXP;
            case (416): return player.Tower_416_EXP;
            case (417): return player.Tower_417_EXP;
            case (418): return player.Tower_418_EXP;
            case (419): return player.Tower_419_EXP;
            case (420): return player.Tower_420_EXP;

            case (501): return player.Tower_501_EXP;
            case (502): return player.Tower_502_EXP;
            case (503): return player.Tower_503_EXP;
            case (504): return player.Tower_504_EXP;
            case (505): return player.Tower_505_EXP;
            case (506): return player.Tower_506_EXP;
            case (507): return player.Tower_507_EXP;
            case (508): return player.Tower_508_EXP;
            case (509): return player.Tower_509_EXP;
            case (510): return player.Tower_510_EXP;
            case (511): return player.Tower_511_EXP;
            case (512): return player.Tower_512_EXP;
            case (513): return player.Tower_513_EXP;
            case (514): return player.Tower_514_EXP;
            case (515): return player.Tower_515_EXP;
            case (516): return player.Tower_516_EXP;
            case (517): return player.Tower_517_EXP;
            case (518): return player.Tower_518_EXP;
            case (519): return player.Tower_519_EXP;
            case (520): return player.Tower_520_EXP;

            case (601): return player.Tower_601_EXP;
            case (602): return player.Tower_602_EXP;
            case (603): return player.Tower_603_EXP;
            case (604): return player.Tower_604_EXP;
            case (605): return player.Tower_605_EXP;
            case (606): return player.Tower_606_EXP;
            case (607): return player.Tower_607_EXP;
            case (608): return player.Tower_608_EXP;
            case (609): return player.Tower_609_EXP;
            case (610): return player.Tower_610_EXP;
            case (611): return player.Tower_611_EXP;
            case (612): return player.Tower_612_EXP;
            case (613): return player.Tower_613_EXP;
            case (614): return player.Tower_614_EXP;
            case (615): return player.Tower_615_EXP;
            case (616): return player.Tower_616_EXP;
            case (617): return player.Tower_617_EXP;
            case (618): return player.Tower_618_EXP;
            case (619): return player.Tower_619_EXP;
            case (620): return player.Tower_620_EXP;
            case (621): return player.Tower_621_EXP;
            case (622): return player.Tower_622_EXP;
            case (623): return player.Tower_623_EXP;
            case (624): return player.Tower_624_EXP;
            case (625): return player.Tower_625_EXP;
            case (626): return player.Tower_626_EXP;
            case (627): return player.Tower_627_EXP;
            case (628): return player.Tower_628_EXP;
            case (629): return player.Tower_629_EXP;
            case (630): return player.Tower_630_EXP;

            case (701): return player.Tower_701_EXP;
            case (702): return player.Tower_702_EXP;
            case (703): return player.Tower_703_EXP;
            case (704): return player.Tower_704_EXP;
            case (705): return player.Tower_705_EXP;
            case (706): return player.Tower_706_EXP;
            case (707): return player.Tower_707_EXP;
            case (708): return player.Tower_708_EXP;
            case (709): return player.Tower_709_EXP;
            case (710): return player.Tower_710_EXP;
            case (711): return player.Tower_711_EXP;
            case (712): return player.Tower_712_EXP;
            case (713): return player.Tower_713_EXP;
            case (714): return player.Tower_714_EXP;
            case (715): return player.Tower_715_EXP;
            case (716): return player.Tower_716_EXP;
            case (717): return player.Tower_717_EXP;
            case (718): return player.Tower_718_EXP;
            case (719): return player.Tower_719_EXP;
            case (720): return player.Tower_720_EXP;
            case (721): return player.Tower_721_EXP;
            case (722): return player.Tower_722_EXP;
            case (723): return player.Tower_723_EXP;
            case (724): return player.Tower_724_EXP;
            case (725): return player.Tower_725_EXP;
            case (726): return player.Tower_726_EXP;
            case (727): return player.Tower_727_EXP;
            case (728): return player.Tower_728_EXP;
            case (729): return player.Tower_729_EXP;
            case (730): return player.Tower_730_EXP;
            case (731): return player.Tower_731_EXP;
            case (732): return player.Tower_732_EXP;
            case (733): return player.Tower_733_EXP;
            case (734): return player.Tower_734_EXP;
            case (735): return player.Tower_735_EXP;
            case (736): return player.Tower_736_EXP;
            case (737): return player.Tower_737_EXP;
            case (738): return player.Tower_738_EXP;
            case (739): return player.Tower_739_EXP;
            case (740): return player.Tower_740_EXP;
            case (741): return player.Tower_741_EXP;
            case (742): return player.Tower_742_EXP;
            case (743): return player.Tower_743_EXP;
            case (744): return player.Tower_744_EXP;
            case (745): return player.Tower_745_EXP;
            case (746): return player.Tower_746_EXP;
            case (747): return player.Tower_747_EXP;
            case (748): return player.Tower_748_EXP;
            case (749): return player.Tower_749_EXP;
            case (750): return player.Tower_750_EXP;
        }
        return EXP;
    }
}

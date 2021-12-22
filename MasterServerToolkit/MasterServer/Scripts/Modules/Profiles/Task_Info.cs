using UnityEngine;
using MasterServerToolkit.MasterServer;

namespace MasterServerToolkit.MasterServer
{
    public class Task_Info : MonoBehaviour
    {
        public void Start_Set_Task(ObservableServerProfile Profile, bool Daily_or_Week)
        {
            short[] task_Code = null, task_qty = null;
            if (Daily_or_Week)
            {
                Check_Duplicate_Task(Profile, (short)Task_Code.Daily_Task, out task_Code, out task_qty);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_Type).Set(task_Code[0]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_Type).Set(task_Code[1]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_Type).Set(task_Code[2]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_QTY).Set(task_qty[0]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_QTY).Set(task_qty[1]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_QTY).Set(task_qty[2]);
            }

            if (!Daily_or_Week)
            {
                Check_Duplicate_Task(Profile, (short)Task_Code.Daily_Task, out task_Code, out task_qty);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_Type).Set(task_Code[0]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_Type).Set(task_Code[1]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_Type).Set(task_Code[2]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_QTY).Set(task_qty[0]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_QTY).Set(task_qty[1]);
                Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_QTY).Set(task_qty[2]);
            }
        }

        void Check_Duplicate_Task(ObservableServerProfile Profile, short daily_week_code, out short[] Task_Code, out short[] QTY)
        {
            bool Re_Check = false;
            Task_Code = new short[3]; QTY = new short[3]; short[] qty = new short[3]; short[] task = new short[3];
            for (short i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    task = Set_Task(Profile, daily_week_code);
                    Task_Code[0] = task[0]; QTY[0] = task[1];
                }
                if (i == 1)
                {
                    task = Set_Task(Profile, daily_week_code);
                    Task_Code[1] = task[0]; QTY[1] = task[1];
                    if (Task_Code[0] == task[0])
                        Re_Check = true;
                    while (Task_Code[0] == task[0])
                    {
                        task = Set_Task(Profile, daily_week_code);
                        Task_Code[1] = task[0]; QTY[1] = task[1];
                        if (Task_Code[0] != task[0])
                            Re_Check = false;
                    }
                }
                if (i == 2)
                {
                    task = Set_Task(Profile, daily_week_code);
                    Task_Code[2] = task[0];
                    QTY[2] = task[1];
                    if (Task_Code[0] == task[0] || Task_Code[1] == task[0])
                        Re_Check = true;
                    while (Re_Check)
                    {
                        task = Set_Task(Profile, daily_week_code);
                        Task_Code[2] = task[0]; QTY[2] = task[1];
                        if (Task_Code[0] != task[0] && Task_Code[1] != task[0])
                            Re_Check = false;
                    }
                }
            }
        }

        public short[] Set_Task(ObservableServerProfile Profile, short daily_week_code)
        {
            short[] return_task_info = null;
            short task_Level = Check_Task_Level_Finish(Profile, daily_week_code);
            short task_Code = Random_Get_Task(task_Level); // Week
            int[] array = Get_Task_Info(daily_week_code, task_Code, task_Level); // Week
            short task_QTY = (short)array[0];
            return_task_info = new short[] { task_Code, task_QTY };

            return return_task_info;
        }

        public short Random_Get_Task(short Level_Finish)
        {
            short Random_Number = (short)Random.Range(1, 16);
            short Task_Number = 0;
            switch (Random_Number)
            {
                case (1): Task_Number = (short)Task_Code.Play_1V1_Task; break;
                case (2): Task_Number = (short)Task_Code.Play_2OP_Task; break;
                case (3): Task_Number = (short)Task_Code.Win_Task; break;
                case (4): Task_Number = (short)Task_Code.Highest_Attack_Point_Task; break;
                case (5): Task_Number = (short)Task_Code.Enemy_Kill_Task; break;
                case (6): Task_Number = (short)Task_Code.OP_Passed_Wave_Task; break;
                case (7): Task_Number = (short)Task_Code.Use_Money_Task; break;
                case (8): Task_Number = (short)Task_Code.Earn_Diamond_Task; break;
                case (9): Task_Number = (short)Task_Code.Winning_Streak_Task; break;
                case (10): Task_Number = (short)Task_Code.Open_Cheest_Task; break;
                case (11): Task_Number = (short)Task_Code.Tower_Level_Up_Task; break;
                case (12): Task_Number = (short)Task_Code.High_Combine_Tower_Point_Task; break;
                case (13): Task_Number = (short)Task_Code.Kill_Over_HP_Enemy_Task; break;
                case (14): Task_Number = (short)Task_Code.Buy_In_Shop_Task; break;
                case (15): Task_Number = (short)Task_Code.Fresh_Shop_Task; break;
            }
            return Task_Number;
        }

        public int[] Get_Task_Info(short Daily_or_Week_Task, short task_Code, short Level_Finish)
        {
            int[] Reward = null;
            int QTY = 0; int[] array = null, Gold_Array = null, Diamond_Array = null;
            int Gold, Diamond;

            //Debug.Log("Get_Task_Info || " + Daily_or_Week_Task + " || " + task_Code + " || " + Level_Finish);

            switch (Daily_or_Week_Task)
            {
                case ((short)Task_Code.Daily_Task):
                    {
                        switch (task_Code)
                        {
                            case ((short)Task_Code.Play_1V1_Task):
                                array = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                                Gold_Array = new int[] { 1000, 1000, 2000, 2000, 3500, 3500, 4000, 4000, 6000 };
                                Diamond_Array = new int[] { 7, 7, 15, 15, 20, 20, 30, 30, 40 };
                                break;
                            case ((short)Task_Code.Play_2OP_Task):
                                array = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                                Gold_Array = new int[] { 1000, 1000, 2000, 2000, 3500, 3500, 4000, 4000, 4000 };
                                Diamond_Array = new int[] { 7, 7, 15, 15, 20, 20, 20, 20, 30 };
                                break;
                            case ((short)Task_Code.Win_Task):
                                array = new int[] { 1, 1, 2, 2, 3, 3, 3, 3, 3 };
                                Gold_Array = new int[] { 1200, 1200, 2000, 2000, 3500, 3500, 4000, 4000, 4000 };
                                Diamond_Array = new int[] { 8, 8, 16, 16, 24, 24, 24, 24, 32 };
                                break;
                            case ((short)Task_Code.Highest_Attack_Point_Task):
                                array = new int[] { 3000, 3000, 5000, 5000, 10000, 10000, 20000, 20000, 30000 };
                                Gold_Array = new int[] { 1000, 1000, 2000, 2000, 3500, 3500, 4000, 4000, 5000 };
                                Diamond_Array = new int[] { 7, 7, 15, 15, 22, 22, 40, 40, 50 };
                                break;
                            case ((short)Task_Code.Enemy_Kill_Task):
                                array = new int[] { 300, 300, 500, 500, 800, 800, 2000, 2000, 3000 };
                                Gold_Array = new int[] { 1000, 1000, 2000, 2000, 3500, 3500, 4000, 4000, 7000 };
                                Diamond_Array = new int[] { 6, 6, 10, 10, 20, 20, 40, 40, 55 };
                                break;
                            case ((short)Task_Code.OP_Passed_Wave_Task):
                                array = new int[] { 30, 30, 40, 40, 50, 50, 60, 60, 70 };
                                Gold_Array = new int[] { 1000, 1000, 2000, 2000, 3500, 3500, 4000, 4000, 7000 };
                                Diamond_Array = new int[] { 12, 12, 25, 25, 35, 35, 45, 45, 55 };
                                break;
                            case ((short)Task_Code.Use_Money_Task):
                                array = new int[] { 10000, 10000, 30000, 30000, 40000, 40000, 50000, 50000, 80000 };
                                Gold_Array = new int[] { 1800, 1800, 2400, 2400, 5000, 5000, 7777, 7777, 8888 };
                                Diamond_Array = new int[] { 10, 10, 30, 30, 40, 40, 50, 50, 60 };
                                break;
                            case ((short)Task_Code.Earn_Diamond_Task):
                                array = new int[] { 30, 30, 40, 40, 50, 50, 30, 30, 60 };
                                Gold_Array = new int[] { 4000, 4000, 5000, 5000, 6000, 6000, 6000, 6000, 6000 };
                                Diamond_Array = new int[] { 30, 30, 40, 40, 50, 50, 50, 50, 60 };
                                break;
                            case ((short)Task_Code.Winning_Streak_Task):
                                array = new int[] { 2, 2, 3, 3, 3, 3, 2, 2, 3 };
                                Gold_Array = new int[] { 3000, 3000, 4000, 4000, 6000, 6000, 3000, 3000, 4000 };
                                Diamond_Array = new int[] { 15, 15, 25, 25, 50, 50, 30, 30, 50 };
                                break;
                            case ((short)Task_Code.Open_Cheest_Task):
                                array = new int[] { 1, 1, 2, 2, 3, 3, 3, 3, 3 };
                                Gold_Array = new int[] { 1000, 1000, 1000, 1000, 4000, 4000, 6000, 6000, 8000 };
                                Diamond_Array = new int[] { 7, 7, 22, 22, 30, 30, 50, 50, 80 };
                                break;
                            case ((short)Task_Code.Tower_Level_Up_Task):
                                array = new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 3 };
                                Gold_Array = new int[] { 1000, 1000, 1000, 1000, 4000, 4000, 6000, 6000, 8000 };
                                Diamond_Array = new int[] { 8, 8, 15, 15, 20, 20, 25, 25, 30 };
                                break;
                            case ((short)Task_Code.High_Combine_Tower_Point_Task):
                                array = new int[] { 5, 5, 6, 6, 7, 7, 9, 9, 11 };
                                Gold_Array = new int[] { 1000, 1000, 1000, 1000, 2500, 2500, 5000, 5000, 8000 };
                                Diamond_Array = new int[] { 7, 7, 10, 10, 15, 15, 30, 30, 50 };
                                break;
                            case ((short)Task_Code.Kill_Over_HP_Enemy_Task):
                                array = new int[] { 30000, 30000, 50000, 50000, 70000, 70000, 100000, 100000, 200000 };
                                Gold_Array = new int[] { 1000, 1000, 1000, 1000, 3000, 3000, 5000, 5000, 8000 };
                                Diamond_Array = new int[] { 7, 7, 15, 15, 20, 20, 40, 40, 60 };
                                break;
                            case ((short)Task_Code.Buy_In_Shop_Task):
                                array = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
                                Gold_Array = new int[] { 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200 };
                                Diamond_Array = new int[] { 8, 8, 12, 12, 20, 20, 25, 25, 30 };
                                break;
                            case ((short)Task_Code.Fresh_Shop_Task):
                                array = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2 };
                                Gold_Array = new int[] { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 };
                                Diamond_Array = new int[] { 7, 10, 12, 14, 16, 18, 20, 22, 24 };
                                break;
                        }
                    }
                    break;

                case ((short)Task_Code.Week_Task):
                    {
                        switch (task_Code)
                        {
                            case ((short)Task_Code.Play_1V1_Task):
                                array = new int[] { 5, 5, 7, 7, 10, 10, 7, 7, 10 };
                                Gold_Array = new int[] { 3000, 3000, 6000, 6000, 9000, 9000, 12000, 12000, 12000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                            case ((short)Task_Code.Play_2OP_Task):
                                array = new int[] { 10, 10, 10, 10, 15, 15, 10, 10, 10 };
                                Gold_Array = new int[] { 3000, 3000, 6000, 6000, 9000, 9000, 12000, 12000, 12000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                            case ((short)Task_Code.Win_Task):
                                array = new int[] { 7, 7, 7, 7, 10, 10, 7, 7, 10 };
                                Gold_Array = new int[] { 3000, 3000, 6000, 6000, 9000, 9000, 12000, 12000, 12000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                            case ((short)Task_Code.Highest_Attack_Point_Task):
                                array = new int[] { 5000, 5000, 10000, 10000, 25000, 25000, 50000, 50000, 88888 };
                                Gold_Array = new int[] { 3000, 3000, 6000, 6000, 9000, 9000, 12000, 12000, 12000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                            case ((short)Task_Code.Enemy_Kill_Task):
                                array = new int[] { 1000, 1000, 3000, 3000, 12000, 12000, 30000, 30000, 50000 };
                                Gold_Array = new int[] { 4500, 4500, 6000, 6000, 9000, 9000, 12000, 12000, 12000 };
                                Diamond_Array = new int[] { 20, 20, 30, 30, 60, 60, 60, 60, 60 };
                                break;
                            case ((short)Task_Code.OP_Passed_Wave_Task):
                                array = new int[] { 50, 50, 60, 60, 70, 70, 80, 80, 90 };
                                Gold_Array = new int[] { 5000, 5000, 6000, 6000, 10000, 10000, 20000, 20000, 20000 };
                                Diamond_Array = new int[] { 30, 30, 40, 40, 80, 80, 100, 100, 100 };
                                break;
                            case ((short)Task_Code.Use_Money_Task):
                                array = new int[] { 10000, 10000, 20000, 20000, 30000, 30000, 40000, 40000, 50000 };
                                Gold_Array = new int[] { 3000, 3000, 4000, 4000, 8000, 8000, 10000, 10000, 10000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                            case ((short)Task_Code.Earn_Diamond_Task):
                                array = new int[] { 100, 100, 200, 200, 300, 300, 200, 200, 300 };
                                Gold_Array = new int[] { 5000, 5000, 8000, 8000, 10000, 10000, 10000, 10000, 10000 };
                                Diamond_Array = new int[] { 30, 30, 60, 60, 100, 100, 100, 100, 200 };
                                break;
                            case ((short)Task_Code.Winning_Streak_Task):
                                array = new int[] { 2, 2, 2, 2, 3, 3, 3, 3, 4 };
                                Gold_Array = new int[] { 3000, 3000, 4000, 4000, 8000, 8000, 8000, 8000, 8000 };
                                Diamond_Array = new int[] { 15, 15, 20, 20, 80, 80, 100, 100, 100 };
                                break;
                            case ((short)Task_Code.Open_Cheest_Task):
                                array = new int[] { 5, 5, 5, 5, 6, 6, 6, 6, 7 };
                                Gold_Array = new int[] { 3000, 3000, 4000, 4000, 6000, 6000, 6000, 6000, 6000 };
                                Diamond_Array = new int[] { 15, 15, 22, 22, 50, 50, 70, 70, 70 };
                                break;
                            case ((short)Task_Code.Tower_Level_Up_Task):
                                array = new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 3 };
                                Gold_Array = new int[] { 3000, 3000, 4000, 4000, 4000, 4000, 6000, 6000, 8000 };
                                Diamond_Array = new int[] { 15, 15, 20, 20, 20, 20, 30, 30, 40 };
                                break;
                            case ((short)Task_Code.High_Combine_Tower_Point_Task):
                                array = new int[] { 7, 7, 9, 9, 11, 11, 13, 13, 15 };
                                Gold_Array = new int[] { 1500, 1500, 3000, 3000, 5000, 5000, 10000, 10000, 15000 };
                                Diamond_Array = new int[] { 10, 10, 22, 22, 50, 50, 100, 100, 150 };
                                break;
                            case ((short)Task_Code.Kill_Over_HP_Enemy_Task):
                                array = new int[] { 100000, 100000, 200000, 200000, 4000000, 400000, 1000000, 1000000, 2000000 };
                                Gold_Array = new int[] { 3000, 3000, 5000, 5000, 10000, 10000, 10000, 10000, 20000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 100, 100, 100, 100, 200 };
                                break;
                            case ((short)Task_Code.Buy_In_Shop_Task):
                                array = new int[] { 5, 5, 10, 10, 10, 10, 20, 20, 20 };
                                Gold_Array = new int[] { 2000, 2000, 6000, 6000, 10000, 10000, 10000, 10000, 10000 };
                                Diamond_Array = new int[] { 20, 20, 30, 30, 100, 100, 100, 100, 100 };
                                break;
                            case ((short)Task_Code.Fresh_Shop_Task):
                                array = new int[] { 6, 6, 9, 9, 12, 12, 12, 12, 12 };
                                Gold_Array = new int[] { 2000, 2000, 3000, 3000, 6000, 6000, 6000, 6000, 6000 };
                                Diamond_Array = new int[] { 20, 20, 30, 30, 60, 60, 60, 60, 60 };
                                break;
                            case ((short)Task_Code.Finish_Daily_Task_Task):
                                array = new int[] { 6, 6, 9, 9, 12, 12, 12, 12, 12 };
                                Gold_Array = new int[] { 3000, 3000, 6000, 6000, 9000, 9000, 9000, 9000, 9000 };
                                Diamond_Array = new int[] { 15, 15, 30, 30, 50, 50, 50, 50, 50 };
                                break;
                        }
                        break;
                    }
            }
            QTY = array[Level_Finish];
            Gold = Gold_Array[Level_Finish];
            Diamond = Diamond_Array[Level_Finish];
            Reward = new int[] { QTY, Gold, Diamond };
            string Test = Get_Task_Text_Info(task_Code, Level_Finish, QTY);
            //Debug.Log("Test || " + Test);
            return Reward;
        }

        public string Get_Task_Text_Info(short task_Code, short Level_Finish, int QTY)
        {
            string Task_Info_Text = null;
            switch (task_Code)
            {
                case ((short)Task_Code.Play_1V1_Task):
                    Task_Info_Text = "玩1vs1對決" + QTY + "次";
                    break;
                case ((short)Task_Code.Play_2OP_Task):
                    Task_Info_Text = "玩合作模式" + QTY + "次";
                    break;
                case ((short)Task_Code.Win_Task):
                    Task_Info_Text = "勝利" + QTY + "次";
                    break;
                case ((short)Task_Code.Highest_Attack_Point_Task):
                    Task_Info_Text = "單次攻擊力超過" + QTY;
                    break;
                case ((short)Task_Code.Enemy_Kill_Task):
                    Task_Info_Text = "殺敵累積超過" + QTY;
                    break;
                case ((short)Task_Code.OP_Passed_Wave_Task):
                    Task_Info_Text = "合作模式通過 " + QTY + " 回合";
                    break;
                case ((short)Task_Code.Use_Money_Task):
                    Task_Info_Text = "花費 " + QTY + " 金幣";
                    break;
                case ((short)Task_Code.Earn_Diamond_Task):
                    Task_Info_Text = "賺取 " + QTY + " 鑽石";
                    break;
                case ((short)Task_Code.Winning_Streak_Task):
                    Task_Info_Text = "連勝 " + QTY + " 次";
                    break;
                case ((short)Task_Code.Open_Cheest_Task):
                    Task_Info_Text = "打開任何寶箱" + QTY + "次";
                    break;
                case ((short)Task_Code.Tower_Level_Up_Task):
                    Task_Info_Text = "升級任何塔" + QTY + "次";
                    break;
                case ((short)Task_Code.High_Combine_Tower_Point_Task):
                    Task_Info_Text = "遊戲中合成" + QTY + "級";
                    break;
                case ((short)Task_Code.Kill_Over_HP_Enemy_Task):
                    Task_Info_Text = "消滅HP" + QTY + "敵人";
                    break;
                case ((short)Task_Code.Buy_In_Shop_Task):
                    Task_Info_Text = "商店消費 " + QTY + " 次";
                    break;
                case ((short)Task_Code.Fresh_Shop_Task):
                    Task_Info_Text = "更新商店 " + QTY + " 次";
                    break;
                case ((short)Task_Code.Daily_Task):
                    Task_Info_Text = "完成每日任務 " + QTY + " 次";
                    break;
            }
            return Task_Info_Text;
        }

        public short[] Get_Task_Bouns_Reward(short Daily_or_Week_Task, short Level_Finish, bool Level_Finish_Task)
        {
            short[] Reward = null, gold = null, diamond = null, token1 = null, token2 = null, s_chest = null, f_chest = null;
            short Gold = 0, Diamond = 0, Token_1 = 0, Token_2 = 0, S_Chest = 0, F_Chest = 0;

            switch (Daily_or_Week_Task)
            {
                case ((short)Task_Code.Daily_Task):
                    {
                        if (!Level_Finish_Task)
                        {
                            gold = new short[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000 };
                            diamond = new short[] { 10, 15, 20, 25, 30, 35, 40, 45, 50, 60 };
                            s_chest = new short[] { 0, 0, 0, 0, 0, 0, 0, 1, 1 };
                        }
                        if (Level_Finish_Task)
                        {
                            gold = new short[] { 3000, 4000, 6000, 7000, 8000, 9000, 10000, 11000, 13000, 15000 };
                            diamond = new short[] { 20, 30, 40, 50, 60, 70, 80, 90, 100, 120 };
                            s_chest = new short[] { 0, 0, 0, 0, 0, 0, 1, 1, 0 };
                            f_chest = new short[] { 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                        }
                    }
                    break;
                case ((short)Task_Code.Week_Task):
                    {
                        if (!Level_Finish_Task)
                        {
                            gold = new short[] { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000 };
                            diamond = new short[] { 20, 30, 40, 50, 60, 70, 80, 100, 110, 120 };
                            token1 = new short[] { 0, 0, 0, 0, 0, 0, 1, 0, 0 };
                            s_chest = new short[] { 0, 0, 0, 0, 0, 0, 0, 1, 1 };
                        }
                        if (Level_Finish_Task)
                        {
                            gold = new short[] { 3000, 4000, 6000, 7000, 8000, 9000, 10000, 11000, 13000, 15000 };
                            diamond = new short[] { 20, 30, 40, 50, 60, 70, 80, 100, 110, 120 };
                            token2 = new short[] { 0, 0, 0, 0, 0, 0, 1, 0, 0 };
                            f_chest = new short[] { 0, 0, 0, 0, 0, 0, 0, 1, 1 };
                        }
                        break;
                    }
            }

            Gold = exchange_Reward(gold, Level_Finish);
            Diamond = exchange_Reward(diamond, Level_Finish);
            Token_1 = exchange_Reward(token1, Level_Finish);
            Token_2 = exchange_Reward(token2, Level_Finish);
            S_Chest = exchange_Reward(s_chest, Level_Finish);
            F_Chest = exchange_Reward(f_chest, Level_Finish);
            Reward = new short[] { Gold, Diamond, Token_1, Token_2, S_Chest, F_Chest };

            short exchange_Reward(short[] check_Array, short Level_Number)
            {
                short QTY = 0;
                if (check_Array == null)
                    return 0;
                if (check_Array != null)
                    QTY = check_Array[Level_Number];

                return QTY;
            }

            return Reward;
        }

        public bool Check_Task_Assign(ObservableServerProfile Profile, short Daily_or_Week_Task)
        {
            bool Empty_Task = false;
            if ((short)Task_Code.Daily_Task == Daily_or_Week_Task)
            {
                int Task_1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_Type).GetValue();
                int Task_2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_Type).GetValue();
                int Task_3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_Type).GetValue();
                if (Task_1 == 0 && Task_2 == 0 & Task_3 == 0)
                    return Empty_Task = true;
            }
            if ((short)Task_Code.Week_Task == Daily_or_Week_Task) // weekly
            {
                int W_Task_1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_Type).GetValue();
                int W_Task_2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_Type).GetValue();
                int W_Task_3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_Type).GetValue();
                if (W_Task_1 == 0 && W_Task_2 == 0 && W_Task_3 == 0)
                    return Empty_Task = true;
            }
            return Empty_Task;
        }

        public short Check_Task_Level_Finish(ObservableServerProfile Profile, short Daily_Week_Code)
        {
            short[] Code = new short[0];
            short Level_Finish = 0;
            if ((short)Task_Code.Daily_Task == Daily_Week_Code)
            {
                Code = new short[] { 0,
                    (short)MstProFilePropertyCode.D_Reward_Level_1_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_2_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_3_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_4_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_5_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_6_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_7_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_8_Claim ,
                    (short)MstProFilePropertyCode.D_Reward_Level_9_Claim };

            }
            if ((short)Task_Code.Week_Task == Daily_Week_Code) // weekly
            {
                Code = new short[] { 0,
                    (short)MstProFilePropertyCode.W_RewarW_Level_1_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_2_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_3_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_4_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_5_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_6_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_7_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_8_Claim ,
                    (short)MstProFilePropertyCode.W_RewarW_Level_9_Claim };
            }
            for (short i = 1; i < Code.Length; i++)
            {
                if (Profile.GetProperty<ObservableInt>(Code[i]).GetValue() > 0)
                    Level_Finish = i;
            }
            Debug.Log("Level_Finish || " + Level_Finish);
            return Level_Finish;
        }
    }
}

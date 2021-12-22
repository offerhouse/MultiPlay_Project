using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace MasterServerToolkit.MasterServer
{
    public class Shop_Info : MonoBehaviour
    {
        public short[] Set_InGame_Sell_Info(short Level_Number)
        {
            short[] Type_QTY_Price_and_Currency = new short[4];
            if (Level_Number == 1)
            {
                short[] Free_Gift = First_Slot_Free_Gift();
                Type_QTY_Price_and_Currency[0] = Free_Gift[0];
                Type_QTY_Price_and_Currency[1] = Free_Gift[1];
                Type_QTY_Price_and_Currency[2] = 0;
                Type_QTY_Price_and_Currency[3] = 0;
            }

            if (Level_Number >= 2)
            {
                short[] Type_Price_and_Currency = Set_Tower_Sell_Slot(Level_Number);
                Type_QTY_Price_and_Currency[0] = Get_Tower_Code(Type_Price_and_Currency[0]);
                Type_QTY_Price_and_Currency[1] = 1;
                Type_QTY_Price_and_Currency[2] = Type_Price_and_Currency[1];
                Type_QTY_Price_and_Currency[3] = Type_Price_and_Currency[2];
            }

            Debug.Log("Type_QTY_Price_and_Currency || " + Type_QTY_Price_and_Currency[0] + " || " + +Type_QTY_Price_and_Currency[1]
                + " || " + +Type_QTY_Price_and_Currency[2] + " || " + +Type_QTY_Price_and_Currency[3]);
            return Type_QTY_Price_and_Currency;
        }

        short[] First_Slot_Free_Gift()
        {
            short[] return_type_qty = null;
            int number = UnityEngine.Random.Range(1, 1001);
            if (number <= 700)
            {
                short[] Gold = new short[] { 300, 500, 1000, 2000 };
                return_type_qty = new short[] { (short)Shop_Code.Gold, Get_Value(Gold) };
            }
            if (number > 701 && number < 999)
            {
                short[] Diamond = new short[] { 10, 10, 10, 10, 10, 20, 20, 20, 20, 30, 50 };
                return_type_qty = new short[] { (short)Shop_Code.Diamond, Get_Value(Diamond) };
            }
            if (number == 999)
            {
                short[] Token1 = new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 };
                return_type_qty = new short[] { (short)Shop_Code.Token_01, Get_Value(Token1) };
            }
            if (number == 1000)
            {
                short[] Token2 = new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 };
                return_type_qty = new short[] { (short)Shop_Code.Token_01, Get_Value(Token2) };
            }

            return return_type_qty;

            short Get_Value(short[] Check)
            {
                short value = 0;
                int Random_Number = UnityEngine.Random.Range(0, Check.Length);
                for (int i = 0; i < Check.Length; i++)
                {
                    value = Check[Random_Number];
                }
                return value;
            }
        }

        short[] Set_Tower_Sell_Slot(short Level)
        {
            short[] Level_Array = Get_InGame_Sell_Level(Level);
            int Random_Number = UnityEngine.Random.Range(0, Level_Array.Length);
            short Final_Level = Level_Array[Random_Number];
            List<short> Tower_Array = Get_Available_Tower(Final_Level);
            short[] Price_and_Currency = Get_InGame_Sell_Price_and_Currency(Final_Level);
            int Random_Tower_Number = UnityEngine.Random.Range(0, Tower_Array.Count);
            short[] Tower_Price_and_Currency = new short[3];
            Tower_Price_and_Currency[0] = Tower_Array[Random_Tower_Number];
            Tower_Price_and_Currency[1] = Price_and_Currency[0];
            Tower_Price_and_Currency[2] = Price_and_Currency[1];

            Debug.Log("Tower_Price_and_Currency || " + Tower_Price_and_Currency[0] + " || " + Tower_Price_and_Currency[1] + " || " +
                Tower_Price_and_Currency[2]);
            return Tower_Price_and_Currency;
        }

        short[] Get_InGame_Sell_Level(short Level_Number)
        {
            short[] Level_Range = new short[3];
            if (Level_Number == 2)
                Level_Range = new short[] { 1 };
            if (Level_Number == 3)
                Level_Range = new short[] { 1, 1, 2 };
            if (Level_Number == 4)
                Level_Range = new short[] { 1, 2, 2, 3, 3, 3, 3, 4 };
            if (Level_Number == 5)
                Level_Range = new short[] { 1, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5 };
            if (Level_Number == 6)
                Level_Range = new short[] { 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6 };
            if (Level_Number == 7)
                Level_Range = new short[] { 1, 2, 3, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7 };
            if (Level_Number == 8)
                Level_Range = new short[] { 1, 2, 3, 4, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7 };
            return Level_Range;
        }

        short[] Get_InGame_Sell_Price_and_Currency(short Level_Number)
        {
            short[] Price_and_Currency = new short[2];
            short[] Price = new short[1]; short[] Currency = new short[1];
            short Gold = (short)Shop_Code.Gold;
            short Diamond = (short)Shop_Code.Diamond;
            short Token1 = (short)Shop_Code.Token_01;
            short Token2 = (short)Shop_Code.Token_02;
            if (Level_Number == 1)
            {
                Price = new short[] { 500, 500, 500, 500, 10 };
                Currency = new short[] { Gold, Gold, Gold, Gold, Diamond };
            }
            if (Level_Number == 2)
            {
                Price = new short[] { 1000, 1000, 1000, 1000, 20 };
                Currency = new short[] { Gold, Gold, Gold, Gold, Diamond };
            }
            if (Level_Number == 3)
            {
                Price = new short[] { 2000, 2000, 2000, 40 };
                Currency = new short[] { Gold, Gold, Gold, Diamond };
            }
            if (Level_Number == 4)
            {
                Price = new short[] { 4000, 4000, 4000, 80 };
                Currency = new short[] { Gold, Gold, Gold, Diamond };
            }
            if (Level_Number == 5)
            {
                Price = new short[] { 8000, 160, 160, 1 };
                Currency = new short[] { Gold, Diamond, Diamond, Token1 };
            }
            if (Level_Number == 6)
            {
                Price = new short[] { 16000, 320, 320, 320, 320, 2, 2 };
                Currency = new short[] { Gold, Diamond, Diamond, Diamond, Diamond, Token1, Token1 };
            }
            if (Level_Number == 7)
            {
                Price = new short[] { 32000, 640, 640, 640, 640, 4, 4, 4, 1, 1, 1 };
                Currency = new short[] { Gold, Diamond, Diamond, Diamond, Diamond, Token1, Token1, Token1, Token2, Token2, Token2 };
            }
            int Random_Number = UnityEngine.Random.Range(0, Price.Length);
            Price_and_Currency[0] = Price[Random_Number];
            Price_and_Currency[1] = Currency[Random_Number];
            return Price_and_Currency;
        }

        List<short> Get_Available_Tower(short Number)
        {
            List<short> Available_Tower = new List<short>();
            if (Number == 1) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_1;
            if (Number == 2) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_2;
            if (Number == 3) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_3;
            if (Number == 4) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_4;
            if (Number == 5) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_5;
            if (Number == 6) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_6;
            if (Number == 7) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_7;
            if (Number == 8) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_8;
            if (Number == 9) Available_Tower = GetComponent<Tower_Available>().Server_Tower_Available_Level_9;
            return Available_Tower;
        }

        short Get_Tower_Code(short number)
        {
            short Tower_Code = 0;
            switch (number)
            {
                case (101): Tower_Code = (short)Shop_Code.Tower_101; break;
                case (102): Tower_Code = (short)Shop_Code.Tower_102; break;
                case (103): Tower_Code = (short)Shop_Code.Tower_103; break;
                case (104): Tower_Code = (short)Shop_Code.Tower_104; break;
                case (105): Tower_Code = (short)Shop_Code.Tower_105; break;
                case (106): Tower_Code = (short)Shop_Code.Tower_106; break;
                case (107): Tower_Code = (short)Shop_Code.Tower_107; break;
                case (108): Tower_Code = (short)Shop_Code.Tower_108; break;
                case (109): Tower_Code = (short)Shop_Code.Tower_109; break;
                case (110): Tower_Code = (short)Shop_Code.Tower_110; break;
                case (111): Tower_Code = (short)Shop_Code.Tower_111; break;
                case (112): Tower_Code = (short)Shop_Code.Tower_112; break;
                case (113): Tower_Code = (short)Shop_Code.Tower_113; break;
                case (114): Tower_Code = (short)Shop_Code.Tower_114; break;
                case (115): Tower_Code = (short)Shop_Code.Tower_115; break;
                case (116): Tower_Code = (short)Shop_Code.Tower_116; break;
                case (117): Tower_Code = (short)Shop_Code.Tower_117; break;
                case (118): Tower_Code = (short)Shop_Code.Tower_118; break;
                case (119): Tower_Code = (short)Shop_Code.Tower_119; break;
                case (120): Tower_Code = (short)Shop_Code.Tower_120; break;

                case (201): Tower_Code = (short)Shop_Code.Tower_201; break;
                case (202): Tower_Code = (short)Shop_Code.Tower_202; break;
                case (203): Tower_Code = (short)Shop_Code.Tower_203; break;
                case (204): Tower_Code = (short)Shop_Code.Tower_204; break;
                case (205): Tower_Code = (short)Shop_Code.Tower_205; break;
                case (206): Tower_Code = (short)Shop_Code.Tower_206; break;
                case (207): Tower_Code = (short)Shop_Code.Tower_207; break;
                case (208): Tower_Code = (short)Shop_Code.Tower_208; break;
                case (209): Tower_Code = (short)Shop_Code.Tower_209; break;
                case (210): Tower_Code = (short)Shop_Code.Tower_210; break;
                case (211): Tower_Code = (short)Shop_Code.Tower_211; break;
                case (212): Tower_Code = (short)Shop_Code.Tower_212; break;
                case (213): Tower_Code = (short)Shop_Code.Tower_213; break;
                case (214): Tower_Code = (short)Shop_Code.Tower_214; break;
                case (215): Tower_Code = (short)Shop_Code.Tower_215; break;
                case (216): Tower_Code = (short)Shop_Code.Tower_216; break;
                case (217): Tower_Code = (short)Shop_Code.Tower_217; break;
                case (218): Tower_Code = (short)Shop_Code.Tower_218; break;
                case (219): Tower_Code = (short)Shop_Code.Tower_219; break;
                case (220): Tower_Code = (short)Shop_Code.Tower_220; break;

                case (301): Tower_Code = (short)Shop_Code.Tower_301; break;
                case (302): Tower_Code = (short)Shop_Code.Tower_302; break;
                case (303): Tower_Code = (short)Shop_Code.Tower_303; break;
                case (304): Tower_Code = (short)Shop_Code.Tower_304; break;
                case (305): Tower_Code = (short)Shop_Code.Tower_305; break;
                case (306): Tower_Code = (short)Shop_Code.Tower_306; break;
                case (307): Tower_Code = (short)Shop_Code.Tower_307; break;
                case (308): Tower_Code = (short)Shop_Code.Tower_308; break;
                case (309): Tower_Code = (short)Shop_Code.Tower_309; break;
                case (310): Tower_Code = (short)Shop_Code.Tower_310; break;
                case (311): Tower_Code = (short)Shop_Code.Tower_311; break;
                case (312): Tower_Code = (short)Shop_Code.Tower_312; break;
                case (313): Tower_Code = (short)Shop_Code.Tower_313; break;
                case (314): Tower_Code = (short)Shop_Code.Tower_314; break;
                case (315): Tower_Code = (short)Shop_Code.Tower_315; break;
                case (316): Tower_Code = (short)Shop_Code.Tower_316; break;
                case (317): Tower_Code = (short)Shop_Code.Tower_317; break;
                case (318): Tower_Code = (short)Shop_Code.Tower_318; break;
                case (319): Tower_Code = (short)Shop_Code.Tower_319; break;
                case (320): Tower_Code = (short)Shop_Code.Tower_320; break;

                case (401): Tower_Code = (short)Shop_Code.Tower_401; break;
                case (402): Tower_Code = (short)Shop_Code.Tower_402; break;
                case (403): Tower_Code = (short)Shop_Code.Tower_403; break;
                case (404): Tower_Code = (short)Shop_Code.Tower_404; break;
                case (405): Tower_Code = (short)Shop_Code.Tower_405; break;
                case (406): Tower_Code = (short)Shop_Code.Tower_406; break;
                case (407): Tower_Code = (short)Shop_Code.Tower_407; break;
                case (408): Tower_Code = (short)Shop_Code.Tower_408; break;
                case (409): Tower_Code = (short)Shop_Code.Tower_409; break;
                case (410): Tower_Code = (short)Shop_Code.Tower_410; break;
                case (411): Tower_Code = (short)Shop_Code.Tower_411; break;
                case (412): Tower_Code = (short)Shop_Code.Tower_412; break;
                case (413): Tower_Code = (short)Shop_Code.Tower_413; break;
                case (414): Tower_Code = (short)Shop_Code.Tower_414; break;
                case (415): Tower_Code = (short)Shop_Code.Tower_415; break;
                case (416): Tower_Code = (short)Shop_Code.Tower_416; break;
                case (417): Tower_Code = (short)Shop_Code.Tower_417; break;
                case (418): Tower_Code = (short)Shop_Code.Tower_418; break;
                case (419): Tower_Code = (short)Shop_Code.Tower_419; break;
                case (420): Tower_Code = (short)Shop_Code.Tower_420; break;

                case (501): Tower_Code = (short)Shop_Code.Tower_501; break;
                case (502): Tower_Code = (short)Shop_Code.Tower_502; break;
                case (503): Tower_Code = (short)Shop_Code.Tower_503; break;
                case (504): Tower_Code = (short)Shop_Code.Tower_504; break;
                case (505): Tower_Code = (short)Shop_Code.Tower_505; break;
                case (506): Tower_Code = (short)Shop_Code.Tower_506; break;
                case (507): Tower_Code = (short)Shop_Code.Tower_507; break;
                case (508): Tower_Code = (short)Shop_Code.Tower_508; break;
                case (509): Tower_Code = (short)Shop_Code.Tower_509; break;
                case (510): Tower_Code = (short)Shop_Code.Tower_510; break;
                case (511): Tower_Code = (short)Shop_Code.Tower_511; break;
                case (512): Tower_Code = (short)Shop_Code.Tower_512; break;
                case (513): Tower_Code = (short)Shop_Code.Tower_513; break;
                case (514): Tower_Code = (short)Shop_Code.Tower_514; break;
                case (515): Tower_Code = (short)Shop_Code.Tower_515; break;
                case (516): Tower_Code = (short)Shop_Code.Tower_516; break;
                case (517): Tower_Code = (short)Shop_Code.Tower_517; break;
                case (518): Tower_Code = (short)Shop_Code.Tower_518; break;
                case (519): Tower_Code = (short)Shop_Code.Tower_519; break;
                case (520): Tower_Code = (short)Shop_Code.Tower_520; break;

                case (601): Tower_Code = (short)Shop_Code.Tower_601; break;
                case (602): Tower_Code = (short)Shop_Code.Tower_602; break;
                case (603): Tower_Code = (short)Shop_Code.Tower_603; break;
                case (604): Tower_Code = (short)Shop_Code.Tower_604; break;
                case (605): Tower_Code = (short)Shop_Code.Tower_605; break;
                case (606): Tower_Code = (short)Shop_Code.Tower_606; break;
                case (607): Tower_Code = (short)Shop_Code.Tower_607; break;
                case (608): Tower_Code = (short)Shop_Code.Tower_608; break;
                case (609): Tower_Code = (short)Shop_Code.Tower_609; break;
                case (610): Tower_Code = (short)Shop_Code.Tower_610; break;
                case (611): Tower_Code = (short)Shop_Code.Tower_611; break;
                case (612): Tower_Code = (short)Shop_Code.Tower_612; break;
                case (613): Tower_Code = (short)Shop_Code.Tower_613; break;
                case (614): Tower_Code = (short)Shop_Code.Tower_614; break;
                case (615): Tower_Code = (short)Shop_Code.Tower_615; break;
                case (616): Tower_Code = (short)Shop_Code.Tower_616; break;
                case (617): Tower_Code = (short)Shop_Code.Tower_617; break;
                case (618): Tower_Code = (short)Shop_Code.Tower_618; break;
                case (619): Tower_Code = (short)Shop_Code.Tower_619; break;
                case (620): Tower_Code = (short)Shop_Code.Tower_620; break;
                case (621): Tower_Code = (short)Shop_Code.Tower_621; break;
                case (622): Tower_Code = (short)Shop_Code.Tower_622; break;
                case (623): Tower_Code = (short)Shop_Code.Tower_623; break;
                case (624): Tower_Code = (short)Shop_Code.Tower_624; break;
                case (625): Tower_Code = (short)Shop_Code.Tower_625; break;
                case (626): Tower_Code = (short)Shop_Code.Tower_626; break;
                case (627): Tower_Code = (short)Shop_Code.Tower_627; break;
                case (628): Tower_Code = (short)Shop_Code.Tower_628; break;
                case (629): Tower_Code = (short)Shop_Code.Tower_629; break;
                case (630): Tower_Code = (short)Shop_Code.Tower_630; break;

                case (701): Tower_Code = (short)Shop_Code.Tower_701; break;
                case (702): Tower_Code = (short)Shop_Code.Tower_702; break;
                case (703): Tower_Code = (short)Shop_Code.Tower_703; break;
                case (704): Tower_Code = (short)Shop_Code.Tower_704; break;
                case (705): Tower_Code = (short)Shop_Code.Tower_705; break;
                case (706): Tower_Code = (short)Shop_Code.Tower_706; break;
                case (707): Tower_Code = (short)Shop_Code.Tower_707; break;
                case (708): Tower_Code = (short)Shop_Code.Tower_708; break;
                case (709): Tower_Code = (short)Shop_Code.Tower_709; break;
                case (710): Tower_Code = (short)Shop_Code.Tower_710; break;
                case (711): Tower_Code = (short)Shop_Code.Tower_711; break;
                case (712): Tower_Code = (short)Shop_Code.Tower_712; break;
                case (713): Tower_Code = (short)Shop_Code.Tower_713; break;
                case (714): Tower_Code = (short)Shop_Code.Tower_714; break;
                case (715): Tower_Code = (short)Shop_Code.Tower_715; break;
                case (716): Tower_Code = (short)Shop_Code.Tower_716; break;
                case (717): Tower_Code = (short)Shop_Code.Tower_717; break;
                case (718): Tower_Code = (short)Shop_Code.Tower_718; break;
                case (719): Tower_Code = (short)Shop_Code.Tower_719; break;
                case (720): Tower_Code = (short)Shop_Code.Tower_720; break;
                case (721): Tower_Code = (short)Shop_Code.Tower_721; break;
                case (722): Tower_Code = (short)Shop_Code.Tower_722; break;
                case (723): Tower_Code = (short)Shop_Code.Tower_723; break;
                case (724): Tower_Code = (short)Shop_Code.Tower_724; break;
                case (725): Tower_Code = (short)Shop_Code.Tower_725; break;
                case (726): Tower_Code = (short)Shop_Code.Tower_726; break;
                case (727): Tower_Code = (short)Shop_Code.Tower_727; break;
                case (728): Tower_Code = (short)Shop_Code.Tower_728; break;
                case (729): Tower_Code = (short)Shop_Code.Tower_729; break;
                case (730): Tower_Code = (short)Shop_Code.Tower_730; break;
                case (731): Tower_Code = (short)Shop_Code.Tower_731; break;
                case (732): Tower_Code = (short)Shop_Code.Tower_732; break;
                case (733): Tower_Code = (short)Shop_Code.Tower_733; break;
                case (734): Tower_Code = (short)Shop_Code.Tower_734; break;
                case (735): Tower_Code = (short)Shop_Code.Tower_735; break;
                case (736): Tower_Code = (short)Shop_Code.Tower_736; break;
                case (737): Tower_Code = (short)Shop_Code.Tower_737; break;
                case (738): Tower_Code = (short)Shop_Code.Tower_738; break;
                case (739): Tower_Code = (short)Shop_Code.Tower_739; break;
                case (740): Tower_Code = (short)Shop_Code.Tower_740; break;
                case (741): Tower_Code = (short)Shop_Code.Tower_741; break;
                case (742): Tower_Code = (short)Shop_Code.Tower_742; break;
                case (743): Tower_Code = (short)Shop_Code.Tower_743; break;
                case (744): Tower_Code = (short)Shop_Code.Tower_744; break;
                case (745): Tower_Code = (short)Shop_Code.Tower_745; break;
                case (746): Tower_Code = (short)Shop_Code.Tower_746; break;
                case (747): Tower_Code = (short)Shop_Code.Tower_747; break;
                case (748): Tower_Code = (short)Shop_Code.Tower_748; break;
                case (749): Tower_Code = (short)Shop_Code.Tower_749; break;
                case (750): Tower_Code = (short)Shop_Code.Tower_750; break;

            }
            return Tower_Code;
        }

        public ObservableInt Get_Property_by_String(ObservableServerProfile profile, string Code)
        {
            short code = (short)(MstProFilePropertyCode)Enum.Parse(typeof(MstProFilePropertyCode), Code);
            var property = profile.GetProperty<ObservableInt>(code);
            return property;
        }

        public void Click_InGame_Refresh_Button(ObservableServerProfile profile, short Code)
        {
            Debug.Log("Click_InGame_Refresh_Button");
            var Last_Refresh_Sell_Time = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Sell_Time);
            int Last_Refresh_Sell_Time_Value = Last_Refresh_Sell_Time.GetValue();

            DateTime last_Refresh_Sell_Time = GetComponent<Shop_Info>().Convert_Int_to_DateTime(Last_Refresh_Sell_Time_Value);

            if (last_Refresh_Sell_Time.Date != DateTime.UtcNow.Date)
            {
                Start_Create_Sell();
                return;
            }

            // Refresh_Sell
            long two_hours = 72000000000;

            if (DateTime.UtcNow.Ticks >= last_Refresh_Sell_Time.Ticks + two_hours)
            {
                Start_Create_Sell();
                return;
            }

            var refresh_InGame_Sell = GetComponent<Shop_Info>().Get_Property_by_String(profile, "Refresh_InGame_Sell");

            int Refresh_InGame_Sell = refresh_InGame_Sell.GetValue();
            if (Refresh_InGame_Sell == 0)
            {
                refresh_InGame_Sell.Set(1);
                Start_Create_Sell();
            }

            void Start_Create_Sell()
            {
                int Time_Now = Covert_DateTime_to_Int(DateTime.UtcNow);
                Last_Refresh_Sell_Time.Set(Time_Now);
                Create_InGame_Sell(profile);
            }
        }

        public void Click_InGame_Button(ObservableServerProfile profile, short Button_Number)
        {
            Debug.Log("Click_InGame_Button");
            var InGame_Sold = GetComponent<Shop_Info>().Get_Property_by_String(profile, "InGame_Sold_" + Button_Number);
            var InGame_Sell = GetComponent<Shop_Info>().Get_Property_by_String(profile, "InGame_Sell_" + Button_Number);
            var InGame_QTY = GetComponent<Shop_Info>().Get_Property_by_String(profile, "InGame_QTY_" + Button_Number);
            var InGame_Price = GetComponent<Shop_Info>().Get_Property_by_String(profile, "InGame_Price_" + Button_Number);
            var InGame_Currency = GetComponent<Shop_Info>().Get_Property_by_String(profile, "InGame_Currency_" + Button_Number);

            if (InGame_Sold.GetValue() > 0)
                return;

            if (InGame_Price.GetValue() == 0)
            {
                string string_Item_Code = ShopCode_To_String((short)InGame_Sell.GetValue());
                var Property_Item = Get_Property_by_String(profile, string_Item_Code);

                Property_Item.Add(InGame_QTY.GetValue());
                InGame_Sold.Set(1);
                return;
            }

            string string_Code = ShopCode_To_String((short)InGame_Currency.GetValue());
            var Property_Currency = Get_Property_by_String(profile, string_Code);
            if (Property_Currency.TryTake(InGame_Price.GetValue()))
            {
                string string_Item_Code = ShopCode_To_String((short)InGame_Sell.GetValue());
                var Property_Item = Get_Property_by_String(profile, string_Item_Code);

                Property_Item.Add(InGame_QTY.GetValue());
                InGame_Sold.Set(1);
            }

            GetComponent<Tower_Available>().Server_Check_Tower_Level_0_To_Level_1(profile);
        }

        public void Create_InGame_Sell(ObservableServerProfile Profile)
        {
            for (short i = 1; i < 8; i++)
            {
                short[] Type_and_QTY_Price_and_Currency = Set_InGame_Sell_Info(i);

                var Sell = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Sell_" + i);
                var QTY = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_QTY_" + i);
                var Price = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Price_" + i);
                var Currency = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Currency_" + i);
                var Item_Sold = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Sold_" + i);

                Sell.Set(Type_and_QTY_Price_and_Currency[0]);
                QTY.Set(Type_and_QTY_Price_and_Currency[1]);
                Price.Set(Type_and_QTY_Price_and_Currency[2]);
                Currency.Set(Type_and_QTY_Price_and_Currency[3]);
                Item_Sold.Set(0);
            }
        }

        public DateTime Convert_Int_to_DateTime(int time)
        {
            string Last_Login_Time_String = time.ToString();
            int temp_year = int.Parse(Last_Login_Time_String.Substring(0, 2)) + 2000;
            short year = (short)temp_year;
            short month = Covert_String_Time(Last_Login_Time_String, 2);
            short date = Covert_String_Time(Last_Login_Time_String, 4);
            short hour = Covert_String_Time(Last_Login_Time_String, 6);
            short min = Covert_String_Time(Last_Login_Time_String, 8);
            return new DateTime(year, month, date, hour, min, 00);

            short Covert_String_Time(string QTY, short start_pos)
            {
                return short.Parse(QTY.Substring(start_pos, 2));
            }
        }

        public int Covert_DateTime_to_Int(DateTime time)
        {
            int Year = time.Year - 2000;
            int Month = time.Month;
            int Date = time.Day;
            int Hour = time.Hour;
            int Minute = time.Minute;
            DateTime Today = new DateTime(Year + 2000, Month, Date);
            string Time = Convert_Time(Year) + Convert_Time(Month) + Convert_Time(Date) + Convert_Time(Hour) + Convert_Time(Minute);
            return int.Parse(Time);
            string Convert_Time(int number)
            {
                string time_text = number.ToString();
                if (number < 10)
                    time_text = "0" + time_text;
                return time_text;
            }
        }

        public string ShopCode_To_String(short code)
        {
            string string_code = null;
            if (code == (short)Shop_Code.Gold) string_code = "Gold";
            if (code == (short)Shop_Code.Diamond) string_code = "Diamond";
            if (code == (short)Shop_Code.Token_01) string_code = "Token_01";
            if (code == (short)Shop_Code.Token_02) string_code = "Token_02";
            if (code == (short)Shop_Code.Token_03) string_code = "Token_03";
            if (code == (short)Shop_Code.Token_04) string_code = "Token_04";
            if (code == (short)Shop_Code.Token_05) string_code = "Token_05";
            if (code == (short)Shop_Code.Token_06) string_code = "Token_06";
            if (code == (short)Shop_Code.Token_07) string_code = "Token_07";
            if (code == (short)Shop_Code.Token_08) string_code = "Token_08";
            if (code == (short)Shop_Code.Token_09) string_code = "Token_09";
            if (code == (short)Shop_Code.Token_10) string_code = "Token_10";
            if (code == (short)Shop_Code.Chest_1) string_code = "Chest_1";
            if (code == (short)Shop_Code.Chest_2) string_code = "Chest_2";
            if (code == (short)Shop_Code.Chest_3) string_code = "Chest_3";
            if (code == (short)Shop_Code.Chest_4) string_code = "Chest_4";
            if (code == (short)Shop_Code.Chest_5) string_code = "Chest_5";
            if (code == (short)Shop_Code.Chest_6) string_code = "Chest_6";
            if (code == (short)Shop_Code.Chest_7) string_code = "Chest_7";
            if (code == (short)Shop_Code.Chest_8) string_code = "Chest_8";
            if (code == (short)Shop_Code.Chest_9) string_code = "Chest_9";
            if (code == (short)Shop_Code.Chest_10) string_code = "Chest_10";
            if (code == (short)Shop_Code.Tower_101) string_code = "Tower_101_EXP";
            if (code == (short)Shop_Code.Tower_102) string_code = "Tower_102_EXP";
            if (code == (short)Shop_Code.Tower_103) string_code = "Tower_103_EXP";
            if (code == (short)Shop_Code.Tower_104) string_code = "Tower_104_EXP";
            if (code == (short)Shop_Code.Tower_105) string_code = "Tower_105_EXP";
            if (code == (short)Shop_Code.Tower_106) string_code = "Tower_106_EXP";
            if (code == (short)Shop_Code.Tower_107) string_code = "Tower_107_EXP";
            if (code == (short)Shop_Code.Tower_108) string_code = "Tower_108_EXP";
            if (code == (short)Shop_Code.Tower_109) string_code = "Tower_109_EXP";
            if (code == (short)Shop_Code.Tower_110) string_code = "Tower_110_EXP";
            if (code == (short)Shop_Code.Tower_111) string_code = "Tower_111_EXP";
            if (code == (short)Shop_Code.Tower_112) string_code = "Tower_112_EXP";
            if (code == (short)Shop_Code.Tower_113) string_code = "Tower_113_EXP";
            if (code == (short)Shop_Code.Tower_114) string_code = "Tower_114_EXP";
            if (code == (short)Shop_Code.Tower_115) string_code = "Tower_115_EXP";
            if (code == (short)Shop_Code.Tower_116) string_code = "Tower_116_EXP";
            if (code == (short)Shop_Code.Tower_117) string_code = "Tower_117_EXP";
            if (code == (short)Shop_Code.Tower_118) string_code = "Tower_118_EXP";
            if (code == (short)Shop_Code.Tower_119) string_code = "Tower_119_EXP";
            if (code == (short)Shop_Code.Tower_120) string_code = "Tower_120_EXP";
            if (code == (short)Shop_Code.Tower_201) string_code = "Tower_201_EXP";
            if (code == (short)Shop_Code.Tower_202) string_code = "Tower_202_EXP";
            if (code == (short)Shop_Code.Tower_203) string_code = "Tower_203_EXP";
            if (code == (short)Shop_Code.Tower_204) string_code = "Tower_204_EXP";
            if (code == (short)Shop_Code.Tower_205) string_code = "Tower_205_EXP";
            if (code == (short)Shop_Code.Tower_206) string_code = "Tower_206_EXP";
            if (code == (short)Shop_Code.Tower_207) string_code = "Tower_207_EXP";
            if (code == (short)Shop_Code.Tower_208) string_code = "Tower_208_EXP";
            if (code == (short)Shop_Code.Tower_209) string_code = "Tower_209_EXP";
            if (code == (short)Shop_Code.Tower_210) string_code = "Tower_210_EXP";
            if (code == (short)Shop_Code.Tower_211) string_code = "Tower_211_EXP";
            if (code == (short)Shop_Code.Tower_212) string_code = "Tower_212_EXP";
            if (code == (short)Shop_Code.Tower_213) string_code = "Tower_213_EXP";
            if (code == (short)Shop_Code.Tower_214) string_code = "Tower_214_EXP";
            if (code == (short)Shop_Code.Tower_215) string_code = "Tower_215_EXP";
            if (code == (short)Shop_Code.Tower_216) string_code = "Tower_216_EXP";
            if (code == (short)Shop_Code.Tower_217) string_code = "Tower_217_EXP";
            if (code == (short)Shop_Code.Tower_218) string_code = "Tower_218_EXP";
            if (code == (short)Shop_Code.Tower_219) string_code = "Tower_219_EXP";
            if (code == (short)Shop_Code.Tower_220) string_code = "Tower_220_EXP";
            if (code == (short)Shop_Code.Tower_301) string_code = "Tower_301_EXP";
            if (code == (short)Shop_Code.Tower_302) string_code = "Tower_302_EXP";
            if (code == (short)Shop_Code.Tower_303) string_code = "Tower_303_EXP";
            if (code == (short)Shop_Code.Tower_304) string_code = "Tower_304_EXP";
            if (code == (short)Shop_Code.Tower_305) string_code = "Tower_305_EXP";
            if (code == (short)Shop_Code.Tower_306) string_code = "Tower_306_EXP";
            if (code == (short)Shop_Code.Tower_307) string_code = "Tower_307_EXP";
            if (code == (short)Shop_Code.Tower_308) string_code = "Tower_308_EXP";
            if (code == (short)Shop_Code.Tower_309) string_code = "Tower_309_EXP";
            if (code == (short)Shop_Code.Tower_310) string_code = "Tower_310_EXP";
            if (code == (short)Shop_Code.Tower_311) string_code = "Tower_311_EXP";
            if (code == (short)Shop_Code.Tower_312) string_code = "Tower_312_EXP";
            if (code == (short)Shop_Code.Tower_313) string_code = "Tower_313_EXP";
            if (code == (short)Shop_Code.Tower_314) string_code = "Tower_314_EXP";
            if (code == (short)Shop_Code.Tower_315) string_code = "Tower_315_EXP";
            if (code == (short)Shop_Code.Tower_316) string_code = "Tower_316_EXP";
            if (code == (short)Shop_Code.Tower_317) string_code = "Tower_317_EXP";
            if (code == (short)Shop_Code.Tower_318) string_code = "Tower_318_EXP";
            if (code == (short)Shop_Code.Tower_319) string_code = "Tower_319_EXP";
            if (code == (short)Shop_Code.Tower_320) string_code = "Tower_320_EXP";
            if (code == (short)Shop_Code.Tower_401) string_code = "Tower_401_EXP";
            if (code == (short)Shop_Code.Tower_402) string_code = "Tower_402_EXP";
            if (code == (short)Shop_Code.Tower_403) string_code = "Tower_403_EXP";
            if (code == (short)Shop_Code.Tower_404) string_code = "Tower_404_EXP";
            if (code == (short)Shop_Code.Tower_405) string_code = "Tower_405_EXP";
            if (code == (short)Shop_Code.Tower_406) string_code = "Tower_406_EXP";
            if (code == (short)Shop_Code.Tower_407) string_code = "Tower_407_EXP";
            if (code == (short)Shop_Code.Tower_408) string_code = "Tower_408_EXP";
            if (code == (short)Shop_Code.Tower_409) string_code = "Tower_409_EXP";
            if (code == (short)Shop_Code.Tower_410) string_code = "Tower_410_EXP";
            if (code == (short)Shop_Code.Tower_411) string_code = "Tower_411_EXP";
            if (code == (short)Shop_Code.Tower_412) string_code = "Tower_412_EXP";
            if (code == (short)Shop_Code.Tower_413) string_code = "Tower_413_EXP";
            if (code == (short)Shop_Code.Tower_414) string_code = "Tower_414_EXP";
            if (code == (short)Shop_Code.Tower_415) string_code = "Tower_415_EXP";
            if (code == (short)Shop_Code.Tower_416) string_code = "Tower_416_EXP";
            if (code == (short)Shop_Code.Tower_417) string_code = "Tower_417_EXP";
            if (code == (short)Shop_Code.Tower_418) string_code = "Tower_418_EXP";
            if (code == (short)Shop_Code.Tower_419) string_code = "Tower_419_EXP";
            if (code == (short)Shop_Code.Tower_420) string_code = "Tower_420_EXP";
            if (code == (short)Shop_Code.Tower_501) string_code = "Tower_501_EXP";
            if (code == (short)Shop_Code.Tower_502) string_code = "Tower_502_EXP";
            if (code == (short)Shop_Code.Tower_503) string_code = "Tower_503_EXP";
            if (code == (short)Shop_Code.Tower_504) string_code = "Tower_504_EXP";
            if (code == (short)Shop_Code.Tower_505) string_code = "Tower_505_EXP";
            if (code == (short)Shop_Code.Tower_506) string_code = "Tower_506_EXP";
            if (code == (short)Shop_Code.Tower_507) string_code = "Tower_507_EXP";
            if (code == (short)Shop_Code.Tower_508) string_code = "Tower_508_EXP";
            if (code == (short)Shop_Code.Tower_509) string_code = "Tower_509_EXP";
            if (code == (short)Shop_Code.Tower_510) string_code = "Tower_510_EXP";
            if (code == (short)Shop_Code.Tower_511) string_code = "Tower_511_EXP";
            if (code == (short)Shop_Code.Tower_512) string_code = "Tower_512_EXP";
            if (code == (short)Shop_Code.Tower_513) string_code = "Tower_513_EXP";
            if (code == (short)Shop_Code.Tower_514) string_code = "Tower_514_EXP";
            if (code == (short)Shop_Code.Tower_515) string_code = "Tower_515_EXP";
            if (code == (short)Shop_Code.Tower_516) string_code = "Tower_516_EXP";
            if (code == (short)Shop_Code.Tower_517) string_code = "Tower_517_EXP";
            if (code == (short)Shop_Code.Tower_518) string_code = "Tower_518_EXP";
            if (code == (short)Shop_Code.Tower_519) string_code = "Tower_519_EXP";
            if (code == (short)Shop_Code.Tower_520) string_code = "Tower_520_EXP";
            if (code == (short)Shop_Code.Tower_601) string_code = "Tower_601_EXP";
            if (code == (short)Shop_Code.Tower_602) string_code = "Tower_602_EXP";
            if (code == (short)Shop_Code.Tower_603) string_code = "Tower_603_EXP";
            if (code == (short)Shop_Code.Tower_604) string_code = "Tower_604_EXP";
            if (code == (short)Shop_Code.Tower_605) string_code = "Tower_605_EXP";
            if (code == (short)Shop_Code.Tower_606) string_code = "Tower_606_EXP";
            if (code == (short)Shop_Code.Tower_607) string_code = "Tower_607_EXP";
            if (code == (short)Shop_Code.Tower_608) string_code = "Tower_608_EXP";
            if (code == (short)Shop_Code.Tower_609) string_code = "Tower_609_EXP";
            if (code == (short)Shop_Code.Tower_610) string_code = "Tower_610_EXP";
            if (code == (short)Shop_Code.Tower_611) string_code = "Tower_611_EXP";
            if (code == (short)Shop_Code.Tower_612) string_code = "Tower_612_EXP";
            if (code == (short)Shop_Code.Tower_613) string_code = "Tower_613_EXP";
            if (code == (short)Shop_Code.Tower_614) string_code = "Tower_614_EXP";
            if (code == (short)Shop_Code.Tower_615) string_code = "Tower_615_EXP";
            if (code == (short)Shop_Code.Tower_616) string_code = "Tower_616_EXP";
            if (code == (short)Shop_Code.Tower_617) string_code = "Tower_617_EXP";
            if (code == (short)Shop_Code.Tower_618) string_code = "Tower_618_EXP";
            if (code == (short)Shop_Code.Tower_619) string_code = "Tower_619_EXP";
            if (code == (short)Shop_Code.Tower_620) string_code = "Tower_620_EXP";
            if (code == (short)Shop_Code.Tower_621) string_code = "Tower_621_EXP";
            if (code == (short)Shop_Code.Tower_622) string_code = "Tower_622_EXP";
            if (code == (short)Shop_Code.Tower_623) string_code = "Tower_623_EXP";
            if (code == (short)Shop_Code.Tower_624) string_code = "Tower_624_EXP";
            if (code == (short)Shop_Code.Tower_625) string_code = "Tower_625_EXP";
            if (code == (short)Shop_Code.Tower_626) string_code = "Tower_626_EXP";
            if (code == (short)Shop_Code.Tower_627) string_code = "Tower_627_EXP";
            if (code == (short)Shop_Code.Tower_628) string_code = "Tower_628_EXP";
            if (code == (short)Shop_Code.Tower_629) string_code = "Tower_629_EXP";
            if (code == (short)Shop_Code.Tower_630) string_code = "Tower_630_EXP";
            if (code == (short)Shop_Code.Tower_701) string_code = "Tower_701_EXP";
            if (code == (short)Shop_Code.Tower_702) string_code = "Tower_702_EXP";
            if (code == (short)Shop_Code.Tower_703) string_code = "Tower_703_EXP";
            if (code == (short)Shop_Code.Tower_704) string_code = "Tower_704_EXP";
            if (code == (short)Shop_Code.Tower_705) string_code = "Tower_705_EXP";
            if (code == (short)Shop_Code.Tower_706) string_code = "Tower_706_EXP";
            if (code == (short)Shop_Code.Tower_707) string_code = "Tower_707_EXP";
            if (code == (short)Shop_Code.Tower_708) string_code = "Tower_708_EXP";
            if (code == (short)Shop_Code.Tower_709) string_code = "Tower_709_EXP";
            if (code == (short)Shop_Code.Tower_710) string_code = "Tower_710_EXP";
            if (code == (short)Shop_Code.Tower_711) string_code = "Tower_711_EXP";
            if (code == (short)Shop_Code.Tower_712) string_code = "Tower_712_EXP";
            if (code == (short)Shop_Code.Tower_713) string_code = "Tower_713_EXP";
            if (code == (short)Shop_Code.Tower_714) string_code = "Tower_714_EXP";
            if (code == (short)Shop_Code.Tower_715) string_code = "Tower_715_EXP";
            if (code == (short)Shop_Code.Tower_716) string_code = "Tower_716_EXP";
            if (code == (short)Shop_Code.Tower_717) string_code = "Tower_717_EXP";
            if (code == (short)Shop_Code.Tower_718) string_code = "Tower_718_EXP";
            if (code == (short)Shop_Code.Tower_719) string_code = "Tower_719_EXP";
            if (code == (short)Shop_Code.Tower_720) string_code = "Tower_720_EXP";
            if (code == (short)Shop_Code.Tower_721) string_code = "Tower_721_EXP";
            if (code == (short)Shop_Code.Tower_722) string_code = "Tower_722_EXP";
            if (code == (short)Shop_Code.Tower_723) string_code = "Tower_723_EXP";
            if (code == (short)Shop_Code.Tower_724) string_code = "Tower_724_EXP";
            if (code == (short)Shop_Code.Tower_725) string_code = "Tower_725_EXP";
            if (code == (short)Shop_Code.Tower_726) string_code = "Tower_726_EXP";
            if (code == (short)Shop_Code.Tower_727) string_code = "Tower_727_EXP";
            if (code == (short)Shop_Code.Tower_728) string_code = "Tower_728_EXP";
            if (code == (short)Shop_Code.Tower_729) string_code = "Tower_729_EXP";
            if (code == (short)Shop_Code.Tower_730) string_code = "Tower_730_EXP";
            if (code == (short)Shop_Code.Tower_731) string_code = "Tower_731_EXP";
            if (code == (short)Shop_Code.Tower_732) string_code = "Tower_732_EXP";
            if (code == (short)Shop_Code.Tower_733) string_code = "Tower_733_EXP";
            if (code == (short)Shop_Code.Tower_734) string_code = "Tower_734_EXP";
            if (code == (short)Shop_Code.Tower_735) string_code = "Tower_735_EXP";
            if (code == (short)Shop_Code.Tower_736) string_code = "Tower_736_EXP";
            if (code == (short)Shop_Code.Tower_737) string_code = "Tower_737_EXP";
            if (code == (short)Shop_Code.Tower_738) string_code = "Tower_738_EXP";
            if (code == (short)Shop_Code.Tower_739) string_code = "Tower_739_EXP";
            if (code == (short)Shop_Code.Tower_740) string_code = "Tower_740_EXP";
            if (code == (short)Shop_Code.Tower_741) string_code = "Tower_741_EXP";
            if (code == (short)Shop_Code.Tower_742) string_code = "Tower_742_EXP";
            if (code == (short)Shop_Code.Tower_743) string_code = "Tower_743_EXP";
            if (code == (short)Shop_Code.Tower_744) string_code = "Tower_744_EXP";
            if (code == (short)Shop_Code.Tower_745) string_code = "Tower_745_EXP";
            if (code == (short)Shop_Code.Tower_746) string_code = "Tower_746_EXP";
            if (code == (short)Shop_Code.Tower_747) string_code = "Tower_747_EXP";
            if (code == (short)Shop_Code.Tower_748) string_code = "Tower_748_EXP";
            if (code == (short)Shop_Code.Tower_749) string_code = "Tower_749_EXP";
            if (code == (short)Shop_Code.Tower_750) string_code = "Tower_750_EXP";

            return string_code;
        }

        public void Create_Exhchange_Shop(ObservableServerProfile Profile)
        {
            for (short i = 1; i < 7; i++)
            {
                int[] Type_and_QTY_Currency_and_Price = Set_Exchange_Shop(i);

                var Sell = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_" + i);
                var QTY = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_QTY_" + i);
                var Currency = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Currency_" + i);
                var Price = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Price_" + i);
                var Sold_Out = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Sold_" + i);

                Sell.Set(Type_and_QTY_Currency_and_Price[0]);
                QTY.Set(Type_and_QTY_Currency_and_Price[1]);
                Currency.Set(Type_and_QTY_Currency_and_Price[2]);
                Price.Set(Type_and_QTY_Currency_and_Price[3]);
                Sold_Out.Set(0);
            }

            int[] Set_Exchange_Shop(short Level_Number)
            {
                int[] Exchange_Shop = null;
                int m_level = 0;
                if (Level_Number == 1)
                    m_level = 1;
                if (Level_Number == 2)
                    m_level = (short)UnityEngine.Random.Range(1, 2);
                if (Level_Number == 3)
                    m_level = (short)UnityEngine.Random.Range(1, 3);
                if (Level_Number == 4)
                    m_level = (short)UnityEngine.Random.Range(2, 4);
                if (Level_Number == 5)
                    m_level = (short)UnityEngine.Random.Range(3, 5);
                if (Level_Number == 6)
                    m_level = (short)UnityEngine.Random.Range(4, 6);

                Exchange_Shop = Set_Level_Exchange(m_level);
                return Exchange_Shop;
                int[] Set_Level_Exchange(int Level)
                {
                    int[] Array = new int[5];
                    int[] Sell_Type = new int[20]; int[] Sell_QTY = new int[20]; int[] Currency = new int[20]; int[] Price = new int[20];
                    short g = (short)Shop_Code.Gold; short d = (short)Shop_Code.Diamond; short t1 = (short)Shop_Code.Token_01; short t2 = (short)Shop_Code.Token_02;
                    short c1 = (short)Shop_Code.Chest_1; short c2 = (short)Shop_Code.Chest_2; short c3 = (short)Shop_Code.Chest_3; short c4 = (short)Shop_Code.Chest_4;

                    switch (Level)
                    {
                        case (1):
                        case (2):
                            Sell_Type = new int[] { g, g, g, g, g, g, g, g, g, g, d, d, d, d, d, d, d, d, d, d };
                            Sell_QTY = new int[] { 1000, 3000, 5000, 8000, 10000, 10000, 15000, 15000, 20000, 20000, 30, 30, 30, 50, 50, 50, 80, 80, 100, 100 };
                            Currency = new int[] { d, d, d, d, d, d, d, d, d, d, g, g, g, g, g, g, g, g, g, g };
                            Price = new int[] { 1, 3, 5, 7, 9, 10, 14, 15, 18, 20, 30000, 30000, 30000, 50000, 50000, 50000, 80000, 80000, 100000, 100000 };
                            break;
                        case (3):
                        case (4):
                            Sell_Type = new int[] { d, d, d, d, d, d, d, d, d, d, t1, t1, t1, t1, t1, t1, t1, t1, t1, t1 };
                            Sell_QTY = new int[] { 80, 80, 80, 100, 100, 100, 150, 150, 150, 200, 1, 3, 3, 3, 5, 5, 5, 7, 8, 10 };
                            Currency = new int[] { g, g, t1, g, t1, t1, g, g, t1, g, g, g, d, d, g, d, d, g, d, d };
                            Price = new int[] { 80000, 80000, 4, 100000, 5, 5, 150000, 150000, 7, 200000, 20000, 55000, 55, 60, 100000, 100, 95, 150000, 160, 200 };
                            break;
                        case (5):
                            Sell_Type = new int[] { t1, t1, t1, t1, t1, t1, t1, t1, t1, t1, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2 };
                            Sell_QTY = new int[] { 1, 2, 2, 2, 3, 3, 5, 5, 8, 10, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3 };
                            Currency = new int[] { g, g, d, d, g, d, g, d, d, d, g, d, d, t1, g, d, d, t1, t1, d };
                            Price = new int[] { 20000, 40000, 40, 38, 60000, 60, 100000, 90, 150, 200, 200000, 200, 190, 10, 400000, 400, 380, 20, 18, 600 };
                            break;
                        case (6):
                            Sell_Type = new int[] { t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2, t2 };
                            Sell_QTY = new int[] { 3, 3, 5, 5, 5, 5, 8, 8, 10, 10, 10, 10, 15, 15, 20, 20, 20, 20, 30, 30 };
                            Currency = new int[] { d, d, d, d, d, d, d, d, d, d, d, d, d, d, d, d, d, d, d, d };
                            Price = new int[] { 600, 600, 1000, 1000, 900, 900, 1500, 1500, 2000, 2000, 1800, 1800, 2700, 2700, 4000, 4000, 3600, 3600, 6000, 6000 };
                            break;
                    }
                    int Number = UnityEngine.Random.Range(0, 20);
                    Array[0] = Sell_Type[Number];
                    Array[1] = Sell_QTY[Number];
                    Array[2] = Currency[Number];
                    Array[3] = Price[Number];
                    Array[4] = Level;
                    return Array;
                }
            }
        }

        public void Click_Exchange_Button(ObservableServerProfile Profile, short Button_Number)
        {
            Debug.Log("Click_Exchange_Button || " + Button_Number);
            var Sell = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_" + Button_Number);
            var QTY = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_QTY_" + Button_Number);
            var Currency = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Currency_" + Button_Number);
            var Price = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Price_" + Button_Number);
            var Sold_Out = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "Exchange_Sold_" + Button_Number);

            if (Sold_Out.GetValue() > 0)
                return;

            string Currency_Code_String = ShopCode_To_String((short)Currency.GetValue());
            var Property_Currency = Get_Property_by_String(Profile, Currency_Code_String);
            if (Property_Currency.TryTake(Price.GetValue()))
            {
                string string_Item_Code = ShopCode_To_String((short)Sell.GetValue());
                var Property_Item = Get_Property_by_String(Profile, string_Item_Code);

                Property_Item.Add(QTY.GetValue());
                Sold_Out.Set(1);
            }
        }
    }
}

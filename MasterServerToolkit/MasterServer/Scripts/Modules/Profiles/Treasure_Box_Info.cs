using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterServerToolkit.MasterServer
{
    public class Treasure_Box_Info : MonoBehaviour
    {
        public List<Shop_Code_Packet> Server_Open_Treasure_Box(short Box_Level, short Player_Level)
        {
            List<Shop_Code_Packet> Reward = new List<Shop_Code_Packet>();
            List<Treasure_Box_Dialog_Packet> List = Get_Treasure_Box_Info(Box_Level);
            Treasure_Box_Dialog_Packet temp_packet = new Treasure_Box_Dialog_Packet();
            bool is_Tower = false;
            short Tower_Level = 0;
            short Shop_Code_Item_Type = 0;
            short Shop_Code_Sell_or_Lucky_Draw = 0;
            float Lucky_Draw_Chance = 0;
            int QTY = 0;
            for (int i = 0; i < List.Count; i++)
            {
                Shop_Code_Packet packet = new Shop_Code_Packet();
                bool Inventory = false;
                temp_packet = List[i];
                is_Tower = temp_packet.is_Tower;
                Tower_Level = temp_packet.Tower_Level;
                Shop_Code_Item_Type = temp_packet.Shop_Code_Item_Type;
                Shop_Code_Sell_or_Lucky_Draw = temp_packet.Shop_Code_Sell_or_Lucky_Draw;
                Lucky_Draw_Chance = temp_packet.Lucky_Draw_Chance;
                QTY = temp_packet.QTY;

                if (Shop_Code_Sell_or_Lucky_Draw == (short)Shop_Code.Lucky_Draw)
                    Inventory = Check_Lucky_Draw(Lucky_Draw_Chance);

                if (Shop_Code_Sell_or_Lucky_Draw == (short)Shop_Code.Direct_Buy)
                    Inventory = true;

                if (is_Tower && Inventory)
                {
                    List<short> Level_Tower_Available_List = GetComponent<Tower_Available>().Get_Tower_Available_By_Level(Tower_Level);
                    short Tower_Code = Random_Get_Tower_By_Level_List(Level_Tower_Available_List);
                    packet.Shop_Code = Tower_Code;
                    packet.QTY = QTY;
                    Reward.Add(packet);
                }
                if (!is_Tower && Inventory && Shop_Code_Item_Type != (short)Shop_Code.Gold)
                {
                    packet.Shop_Code = Shop_Code_Item_Type;
                    packet.QTY = QTY;
                    Reward.Add(packet);
                }
                if (!is_Tower && Inventory && Shop_Code_Item_Type == (short)Shop_Code.Gold)
                {
                    packet.Shop_Code = Shop_Code_Item_Type;
                    packet.QTY = QTY * Player_Level;
                    Reward.Add(packet);
                }
            }
            return Reward;
        }

        bool Check_Lucky_Draw(float Draw_Chance)
        {
            float Random_Number = Random.Range(0, 101);
            if (Draw_Chance >= Random_Number)
                return true;

            return false;
        }

        public List<Treasure_Box_Dialog_Packet> Get_Treasure_Box_Info(short Box_Level)
        {
            bool Active = true; bool Deactive = false; bool isTower = true; bool Not_Tower = false;
            short Empty_Shop_Code = 3000; short Direct_Sell = (short)Shop_Code.Direct_Buy; short Lucky_Draw = (short)Shop_Code.Lucky_Draw;
            short Not_Draw = 0;
            short gold = (short)Shop_Code.Gold; short diamond = (short)Shop_Code.Diamond;
            short token_01 = (short)Shop_Code.Token_01;
            short token_02 = (short)Shop_Code.Token_02;

            List<Treasure_Box_Dialog_Packet> List = new List<Treasure_Box_Dialog_Packet>();

            Treasure_Box_Dialog_Packet packet1 = Set_Dialog_Packet(Active, isTower, 1, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet2 = Set_Dialog_Packet(Active, isTower, 2, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet3 = Set_Dialog_Packet(Active, isTower, 3, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet4 = Set_Dialog_Packet(Active, isTower, 4, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet5 = Set_Dialog_Packet(Active, isTower, 5, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet6 = Set_Dialog_Packet(Active, isTower, 6, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
            Treasure_Box_Dialog_Packet packet7 = new Treasure_Box_Dialog_Packet();
            Treasure_Box_Dialog_Packet Gold = new Treasure_Box_Dialog_Packet();
            Treasure_Box_Dialog_Packet Diamond = new Treasure_Box_Dialog_Packet();
            Treasure_Box_Dialog_Packet Token1 = new Treasure_Box_Dialog_Packet();
            Treasure_Box_Dialog_Packet Token2 = new Treasure_Box_Dialog_Packet();

            if (Box_Level == 0)
            {   // Soul 100 1 box
                packet6 = Set_Dialog_Packet(Active, isTower, 6, Empty_Shop_Code, Lucky_Draw, 1, 1);
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Lucky_Draw, 1, 1);
                Gold = Set_Dialog_Packet(Active, Not_Tower, 0, gold, Direct_Sell, Not_Draw, 100);
                Diamond = Set_Dialog_Packet(Active, Not_Tower, 0, diamond, Direct_Sell, Not_Draw, 5);
                Token1 = Set_Dialog_Packet(Active, Not_Tower, 0, token_01, Lucky_Draw, 1, 1);
                Token2 = Set_Dialog_Packet(Active, Not_Tower, 0, token_02, Lucky_Draw, 0.1f, 1);
            }
            if (Box_Level == 1)
            {   // Diamond 1000 1 box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Lucky_Draw, 30, 1);
                Gold = Set_Dialog_Packet(Active, Not_Tower, 0, gold, Direct_Sell, Not_Draw, 200);
                Diamond = Set_Dialog_Packet(Active, Not_Tower, 0, diamond, Direct_Sell, Not_Draw, 30);
                Token1 = Set_Dialog_Packet(Active, Not_Tower, 0, token_01, Lucky_Draw, 50, 1);
                Token2 = Set_Dialog_Packet(Active, Not_Tower, 0, token_02, Lucky_Draw, 1, 1);
            }
            if (Box_Level >= 2)
            {   // 1500 diamond 1 box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Lucky_Draw, 50, 1);
                Gold = Set_Dialog_Packet(Active, Not_Tower, 0, gold, Direct_Sell, Not_Draw, 500);
                Diamond = Set_Dialog_Packet(Active, Not_Tower, 0, diamond, Direct_Sell, Not_Draw, 100);
                Token1 = Set_Dialog_Packet(Active, Not_Tower, 0, token_01, Direct_Sell, Not_Draw, 1);
                Token2 = Set_Dialog_Packet(Active, Not_Tower, 0, token_02, Lucky_Draw, 10, 1);
            }
            if (Box_Level >= 3)
            {   // 3000 diamond 1 box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
                Gold = Set_Dialog_Packet(Active, Not_Tower, 0, gold, Direct_Sell, Not_Draw, 3500);
                Diamond = Set_Dialog_Packet(Active, Not_Tower, 0, diamond, Direct_Sell, Not_Draw, 500);
                Token1 = Set_Dialog_Packet(Active, Not_Tower, 0, token_01, Lucky_Draw, 50, 1);
                Token2 = Set_Dialog_Packet(Active, Not_Tower, 0, token_02, Lucky_Draw, 10, 1);
            }
            if (Box_Level >= 4)
            {   // 3000 diamond 1 box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
                Gold = Set_Dialog_Packet(Active, Not_Tower, 0, gold, Direct_Sell, Not_Draw, 3000);
                Diamond = Set_Dialog_Packet(Active, Not_Tower, 0, diamond, Direct_Sell, Not_Draw, 100);
                Token1 = Set_Dialog_Packet(Active, Not_Tower, 0, token_01, Direct_Sell, Not_Draw, 20);
                Token2 = Set_Dialog_Packet(Active, Not_Tower, 0, token_02, Direct_Sell, Not_Draw, 1);
            }
            if (Box_Level >= 5)
            {   // 3000 diamond 1 box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Direct_Sell, Not_Draw, 2);
                Gold = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Diamond = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Token1 = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Token2 = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
            }
            if (Box_Level >= 6)
            {   // 1 x Token_02 = 1 Box
                packet7 = Set_Dialog_Packet(Active, isTower, 7, Empty_Shop_Code, Direct_Sell, Not_Draw, 1);
                Gold = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Diamond = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Token1 = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
                Token2 = Set_Dialog_Packet(Deactive, Not_Tower, 0, 0, 0, 0, 0);
            }
            if (Box_Level >= 7)
            {

            }
            if (Box_Level >= 8)
            {

            }
            if (Box_Level >= 9)
            {

            }
            List.Add(packet1);
            List.Add(packet2);
            List.Add(packet3);
            List.Add(packet4);
            List.Add(packet5);
            List.Add(packet6);
            List.Add(packet7);
            List.Add(Gold);
            List.Add(Diamond);
            List.Add(Token1);
            List.Add(Token2);
            return List;

            //bool active, bool isTower, short Tower_Level, short Shop_Code_item_Type, short shop_code_sell_or_Lucky , short QTY
            Treasure_Box_Dialog_Packet Set_Dialog_Packet(bool active, bool _isTower, short Level, short type, short sell, float chance,
                short QTY)
            {
                Treasure_Box_Dialog_Packet packet = new Treasure_Box_Dialog_Packet();
                packet.Active = active;
                packet.is_Tower = _isTower;
                packet.Tower_Level = Level;
                packet.Shop_Code_Item_Type = type;
                packet.Shop_Code_Sell_or_Lucky_Draw = sell;
                packet.Lucky_Draw_Chance = chance;
                packet.QTY = QTY;
                return packet;
            }
        }

        short Random_Get_Tower_By_Level_List(List<short> m_List)
        {
            int Random_Number = Random.Range(0, m_List.Count);
            short Tower_Code = m_List[Random_Number];
            return (short)Tower_Code;
        }

        public void Set_Current_Box_Currency_and_Price(short Treasure_Box_Level, out short Currency_Code, out int Require_Resource)
        {
            short currency_Code = 0;
            int price = 0;
            switch (Treasure_Box_Level)
            {
                case (0):
                    currency_Code = (short)Shop_Code.Token_03;
                    price = 100;
                    break;
                case (1):
                    currency_Code = (short)Shop_Code.Diamond;
                    price = 1000;
                    break;
                case (2):
                    currency_Code = (short)Shop_Code.Diamond;
                    price = 1500;
                    break;
                case (3):
                case (4):
                case (5):
                    currency_Code = (short)Shop_Code.Diamond;
                    price = 3000;
                    break;
                case (6):
                    currency_Code = (short)Shop_Code.Token_02;
                    price = 1;
                    break;
            }
            Currency_Code = currency_Code;
            Require_Resource = price;
        }

    }
}

using MasterServerToolkit.Networking;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Games;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Mirror;
using MasterServerToolkit.Examples.BasicProfile;

public class Player_Status : MonoBehaviour
{
    public bool ReJoinGame = false;
    public GameObject Player_Object;
    public GameObject GM;
    public GameObject Tower_Controller;
    public GameObject Pool_Manager;
    public Player player;
    public GameObject Text_Obj;
    public GameObject UI_Main;
    public bool Player_Status_Load_Finish;
    public DemoProfilesBehaviour demoProfilesBehaviour;

    [Serializable]
    public class Player
    {
        public string DisplayName { get; set; }
        public int Player_EXP { get; set; }
        public int Player_Level { get; set; }
        public int Diamond { get; set; }
        public int Gold { get; set; }
        public int Token_01 { get; set; }
        public int Token_02 { get; set; }
        public int Token_03 { get; set; }
        public int Token_04 { get; set; }
        public int Token_05 { get; set; }
        public int Token_06 { get; set; }
        public int Token_07 { get; set; }
        public int Token_08 { get; set; }
        public int Token_09 { get; set; }
        public int Token_10 { get; set; }

        public int Tower_101_Level { get; set; }
        public int Tower_101_EXP { get; set; }
        public int Tower_102_Level { get; set; }
        public int Tower_102_EXP { get; set; }
        public int Tower_103_Level { get; set; }
        public int Tower_103_EXP { get; set; }
        public int Tower_104_Level { get; set; }
        public int Tower_104_EXP { get; set; }
        public int Tower_105_Level { get; set; }
        public int Tower_105_EXP { get; set; }
        public int Tower_106_Level { get; set; }
        public int Tower_106_EXP { get; set; }
        public int Tower_107_Level { get; set; }
        public int Tower_107_EXP { get; set; }
        public int Tower_108_Level { get; set; }
        public int Tower_108_EXP { get; set; }
        public int Tower_109_Level { get; set; }
        public int Tower_109_EXP { get; set; }
        public int Tower_110_Level { get; set; }
        public int Tower_110_EXP { get; set; }
        public int Tower_111_Level { get; set; }
        public int Tower_111_EXP { get; set; }
        public int Tower_112_Level { get; set; }
        public int Tower_112_EXP { get; set; }
        public int Tower_113_Level { get; set; }
        public int Tower_113_EXP { get; set; }
        public int Tower_114_Level { get; set; }
        public int Tower_114_EXP { get; set; }
        public int Tower_115_Level { get; set; }
        public int Tower_115_EXP { get; set; }
        public int Tower_116_Level { get; set; }
        public int Tower_116_EXP { get; set; }
        public int Tower_117_Level { get; set; }
        public int Tower_117_EXP { get; set; }
        public int Tower_118_Level { get; set; }
        public int Tower_118_EXP { get; set; }
        public int Tower_119_Level { get; set; }
        public int Tower_119_EXP { get; set; }
        public int Tower_120_Level { get; set; }
        public int Tower_120_EXP { get; set; }

        public int Tower_201_Level { get; set; }
        public int Tower_201_EXP { get; set; }
        public int Tower_202_Level { get; set; }
        public int Tower_202_EXP { get; set; }
        public int Tower_203_Level { get; set; }
        public int Tower_203_EXP { get; set; }
        public int Tower_204_Level { get; set; }
        public int Tower_204_EXP { get; set; }
        public int Tower_205_Level { get; set; }
        public int Tower_205_EXP { get; set; }
        public int Tower_206_Level { get; set; }
        public int Tower_206_EXP { get; set; }
        public int Tower_207_Level { get; set; }
        public int Tower_207_EXP { get; set; }
        public int Tower_208_Level { get; set; }
        public int Tower_208_EXP { get; set; }
        public int Tower_209_Level { get; set; }
        public int Tower_209_EXP { get; set; }
        public int Tower_210_Level { get; set; }
        public int Tower_210_EXP { get; set; }
        public int Tower_211_Level { get; set; }
        public int Tower_211_EXP { get; set; }
        public int Tower_212_Level { get; set; }
        public int Tower_212_EXP { get; set; }
        public int Tower_213_Level { get; set; }
        public int Tower_213_EXP { get; set; }
        public int Tower_214_Level { get; set; }
        public int Tower_214_EXP { get; set; }
        public int Tower_215_Level { get; set; }
        public int Tower_215_EXP { get; set; }
        public int Tower_216_Level { get; set; }
        public int Tower_216_EXP { get; set; }
        public int Tower_217_Level { get; set; }
        public int Tower_217_EXP { get; set; }
        public int Tower_218_Level { get; set; }
        public int Tower_218_EXP { get; set; }
        public int Tower_219_Level { get; set; }
        public int Tower_219_EXP { get; set; }
        public int Tower_220_Level { get; set; }
        public int Tower_220_EXP { get; set; }

        public int Tower_301_Level { get; set; }
        public int Tower_301_EXP { get; set; }
        public int Tower_302_Level { get; set; }
        public int Tower_302_EXP { get; set; }
        public int Tower_303_Level { get; set; }
        public int Tower_303_EXP { get; set; }
        public int Tower_304_Level { get; set; }
        public int Tower_304_EXP { get; set; }
        public int Tower_305_Level { get; set; }
        public int Tower_305_EXP { get; set; }
        public int Tower_306_Level { get; set; }
        public int Tower_306_EXP { get; set; }
        public int Tower_307_Level { get; set; }
        public int Tower_307_EXP { get; set; }
        public int Tower_308_Level { get; set; }
        public int Tower_308_EXP { get; set; }
        public int Tower_309_Level { get; set; }
        public int Tower_309_EXP { get; set; }
        public int Tower_310_Level { get; set; }
        public int Tower_310_EXP { get; set; }
        public int Tower_311_Level { get; set; }
        public int Tower_311_EXP { get; set; }
        public int Tower_312_Level { get; set; }
        public int Tower_312_EXP { get; set; }
        public int Tower_313_Level { get; set; }
        public int Tower_313_EXP { get; set; }
        public int Tower_314_Level { get; set; }
        public int Tower_314_EXP { get; set; }
        public int Tower_315_Level { get; set; }
        public int Tower_315_EXP { get; set; }
        public int Tower_316_Level { get; set; }
        public int Tower_316_EXP { get; set; }
        public int Tower_317_Level { get; set; }
        public int Tower_317_EXP { get; set; }
        public int Tower_318_Level { get; set; }
        public int Tower_318_EXP { get; set; }
        public int Tower_319_Level { get; set; }
        public int Tower_319_EXP { get; set; }
        public int Tower_320_Level { get; set; }
        public int Tower_320_EXP { get; set; }

        public int Tower_401_Level { get; set; }
        public int Tower_401_EXP { get; set; }
        public int Tower_402_Level { get; set; }
        public int Tower_402_EXP { get; set; }
        public int Tower_403_Level { get; set; }
        public int Tower_403_EXP { get; set; }
        public int Tower_404_Level { get; set; }
        public int Tower_404_EXP { get; set; }
        public int Tower_405_Level { get; set; }
        public int Tower_405_EXP { get; set; }
        public int Tower_406_Level { get; set; }
        public int Tower_406_EXP { get; set; }
        public int Tower_407_Level { get; set; }
        public int Tower_407_EXP { get; set; }
        public int Tower_408_Level { get; set; }
        public int Tower_408_EXP { get; set; }
        public int Tower_409_Level { get; set; }
        public int Tower_409_EXP { get; set; }
        public int Tower_410_Level { get; set; }
        public int Tower_410_EXP { get; set; }
        public int Tower_411_Level { get; set; }
        public int Tower_411_EXP { get; set; }
        public int Tower_412_Level { get; set; }
        public int Tower_412_EXP { get; set; }
        public int Tower_413_Level { get; set; }
        public int Tower_413_EXP { get; set; }
        public int Tower_414_Level { get; set; }
        public int Tower_414_EXP { get; set; }
        public int Tower_415_Level { get; set; }
        public int Tower_415_EXP { get; set; }
        public int Tower_416_Level { get; set; }
        public int Tower_416_EXP { get; set; }
        public int Tower_417_Level { get; set; }
        public int Tower_417_EXP { get; set; }
        public int Tower_418_Level { get; set; }
        public int Tower_418_EXP { get; set; }
        public int Tower_419_Level { get; set; }
        public int Tower_419_EXP { get; set; }
        public int Tower_420_Level { get; set; }
        public int Tower_420_EXP { get; set; }

        public int Tower_501_Level { get; set; }
        public int Tower_501_EXP { get; set; }
        public int Tower_502_Level { get; set; }
        public int Tower_502_EXP { get; set; }
        public int Tower_503_Level { get; set; }
        public int Tower_503_EXP { get; set; }
        public int Tower_504_Level { get; set; }
        public int Tower_504_EXP { get; set; }
        public int Tower_505_Level { get; set; }
        public int Tower_505_EXP { get; set; }
        public int Tower_506_Level { get; set; }
        public int Tower_506_EXP { get; set; }
        public int Tower_507_Level { get; set; }
        public int Tower_507_EXP { get; set; }
        public int Tower_508_Level { get; set; }
        public int Tower_508_EXP { get; set; }
        public int Tower_509_Level { get; set; }
        public int Tower_509_EXP { get; set; }
        public int Tower_510_Level { get; set; }
        public int Tower_510_EXP { get; set; }
        public int Tower_511_Level { get; set; }
        public int Tower_511_EXP { get; set; }
        public int Tower_512_Level { get; set; }
        public int Tower_512_EXP { get; set; }
        public int Tower_513_Level { get; set; }
        public int Tower_513_EXP { get; set; }
        public int Tower_514_Level { get; set; }
        public int Tower_514_EXP { get; set; }
        public int Tower_515_Level { get; set; }
        public int Tower_515_EXP { get; set; }
        public int Tower_516_Level { get; set; }
        public int Tower_516_EXP { get; set; }
        public int Tower_517_Level { get; set; }
        public int Tower_517_EXP { get; set; }
        public int Tower_518_Level { get; set; }
        public int Tower_518_EXP { get; set; }
        public int Tower_519_Level { get; set; }
        public int Tower_519_EXP { get; set; }
        public int Tower_520_Level { get; set; }
        public int Tower_520_EXP { get; set; }

        public int Tower_601_Level { get; set; }
        public int Tower_601_EXP { get; set; }
        public int Tower_602_Level { get; set; }
        public int Tower_602_EXP { get; set; }
        public int Tower_603_Level { get; set; }
        public int Tower_603_EXP { get; set; }
        public int Tower_604_Level { get; set; }
        public int Tower_604_EXP { get; set; }
        public int Tower_605_Level { get; set; }
        public int Tower_605_EXP { get; set; }
        public int Tower_606_Level { get; set; }
        public int Tower_606_EXP { get; set; }
        public int Tower_607_Level { get; set; }
        public int Tower_607_EXP { get; set; }
        public int Tower_608_Level { get; set; }
        public int Tower_608_EXP { get; set; }
        public int Tower_609_Level { get; set; }
        public int Tower_609_EXP { get; set; }
        public int Tower_610_Level { get; set; }
        public int Tower_610_EXP { get; set; }
        public int Tower_611_Level { get; set; }
        public int Tower_611_EXP { get; set; }
        public int Tower_612_Level { get; set; }
        public int Tower_612_EXP { get; set; }
        public int Tower_613_Level { get; set; }
        public int Tower_613_EXP { get; set; }
        public int Tower_614_Level { get; set; }
        public int Tower_614_EXP { get; set; }
        public int Tower_615_Level { get; set; }
        public int Tower_615_EXP { get; set; }
        public int Tower_616_Level { get; set; }
        public int Tower_616_EXP { get; set; }
        public int Tower_617_Level { get; set; }
        public int Tower_617_EXP { get; set; }
        public int Tower_618_Level { get; set; }
        public int Tower_618_EXP { get; set; }
        public int Tower_619_Level { get; set; }
        public int Tower_619_EXP { get; set; }
        public int Tower_620_Level { get; set; }
        public int Tower_620_EXP { get; set; }
        public int Tower_621_Level { get; set; }
        public int Tower_621_EXP { get; set; }
        public int Tower_622_Level { get; set; }
        public int Tower_622_EXP { get; set; }
        public int Tower_623_Level { get; set; }
        public int Tower_623_EXP { get; set; }
        public int Tower_624_Level { get; set; }
        public int Tower_624_EXP { get; set; }
        public int Tower_625_Level { get; set; }
        public int Tower_625_EXP { get; set; }
        public int Tower_626_Level { get; set; }
        public int Tower_626_EXP { get; set; }
        public int Tower_627_Level { get; set; }
        public int Tower_627_EXP { get; set; }
        public int Tower_628_Level { get; set; }
        public int Tower_628_EXP { get; set; }
        public int Tower_629_Level { get; set; }
        public int Tower_629_EXP { get; set; }
        public int Tower_630_Level { get; set; }
        public int Tower_630_EXP { get; set; }

        public int Tower_701_Level { get; set; }
        public int Tower_701_EXP { get; set; }
        public int Tower_702_Level { get; set; }
        public int Tower_702_EXP { get; set; }
        public int Tower_703_Level { get; set; }
        public int Tower_703_EXP { get; set; }
        public int Tower_704_Level { get; set; }
        public int Tower_704_EXP { get; set; }
        public int Tower_705_Level { get; set; }
        public int Tower_705_EXP { get; set; }
        public int Tower_706_Level { get; set; }
        public int Tower_706_EXP { get; set; }
        public int Tower_707_Level { get; set; }
        public int Tower_707_EXP { get; set; }
        public int Tower_708_Level { get; set; }
        public int Tower_708_EXP { get; set; }
        public int Tower_709_Level { get; set; }
        public int Tower_709_EXP { get; set; }
        public int Tower_710_Level { get; set; }
        public int Tower_710_EXP { get; set; }
        public int Tower_711_Level { get; set; }
        public int Tower_711_EXP { get; set; }
        public int Tower_712_Level { get; set; }
        public int Tower_712_EXP { get; set; }
        public int Tower_713_Level { get; set; }
        public int Tower_713_EXP { get; set; }
        public int Tower_714_Level { get; set; }
        public int Tower_714_EXP { get; set; }
        public int Tower_715_Level { get; set; }
        public int Tower_715_EXP { get; set; }
        public int Tower_716_Level { get; set; }
        public int Tower_716_EXP { get; set; }
        public int Tower_717_Level { get; set; }
        public int Tower_717_EXP { get; set; }
        public int Tower_718_Level { get; set; }
        public int Tower_718_EXP { get; set; }
        public int Tower_719_Level { get; set; }
        public int Tower_719_EXP { get; set; }
        public int Tower_720_Level { get; set; }
        public int Tower_720_EXP { get; set; }
        public int Tower_721_Level { get; set; }
        public int Tower_721_EXP { get; set; }
        public int Tower_722_Level { get; set; }
        public int Tower_722_EXP { get; set; }
        public int Tower_723_Level { get; set; }
        public int Tower_723_EXP { get; set; }
        public int Tower_724_Level { get; set; }
        public int Tower_724_EXP { get; set; }
        public int Tower_725_Level { get; set; }
        public int Tower_725_EXP { get; set; }
        public int Tower_726_Level { get; set; }
        public int Tower_726_EXP { get; set; }
        public int Tower_727_Level { get; set; }
        public int Tower_727_EXP { get; set; }
        public int Tower_728_Level { get; set; }
        public int Tower_728_EXP { get; set; }
        public int Tower_729_Level { get; set; }
        public int Tower_729_EXP { get; set; }
        public int Tower_730_Level { get; set; }
        public int Tower_730_EXP { get; set; }
        public int Tower_731_Level { get; set; }
        public int Tower_731_EXP { get; set; }
        public int Tower_732_Level { get; set; }
        public int Tower_732_EXP { get; set; }
        public int Tower_733_Level { get; set; }
        public int Tower_733_EXP { get; set; }
        public int Tower_734_Level { get; set; }
        public int Tower_734_EXP { get; set; }
        public int Tower_735_Level { get; set; }
        public int Tower_735_EXP { get; set; }
        public int Tower_736_Level { get; set; }
        public int Tower_736_EXP { get; set; }
        public int Tower_737_Level { get; set; }
        public int Tower_737_EXP { get; set; }
        public int Tower_738_Level { get; set; }
        public int Tower_738_EXP { get; set; }
        public int Tower_739_Level { get; set; }
        public int Tower_739_EXP { get; set; }
        public int Tower_740_Level { get; set; }
        public int Tower_740_EXP { get; set; }
        public int Tower_741_Level { get; set; }
        public int Tower_741_EXP { get; set; }
        public int Tower_742_Level { get; set; }
        public int Tower_742_EXP { get; set; }
        public int Tower_743_Level { get; set; }
        public int Tower_743_EXP { get; set; }
        public int Tower_744_Level { get; set; }
        public int Tower_744_EXP { get; set; }
        public int Tower_745_Level { get; set; }
        public int Tower_745_EXP { get; set; }
        public int Tower_746_Level { get; set; }
        public int Tower_746_EXP { get; set; }
        public int Tower_747_Level { get; set; }
        public int Tower_747_EXP { get; set; }
        public int Tower_748_Level { get; set; }
        public int Tower_748_EXP { get; set; }
        public int Tower_749_Level { get; set; }
        public int Tower_749_EXP { get; set; }
        public int Tower_750_Level { get; set; }
        public int Tower_750_EXP { get; set; }

        public int Statue_01_Level { get; set; }
        public int Statue_01_EXP { get; set; }
        public int Statue_02_Level { get; set; }
        public int Statue_02_EXP { get; set; }
        public int Statue_03_Level { get; set; }
        public int Statue_03_EXP { get; set; }
        public int Statue_04_Level { get; set; }
        public int Statue_04_EXP { get; set; }
        public int Statue_05_Level { get; set; }
        public int Statue_05_EXP { get; set; }
        public int Statue_06_Level { get; set; }
        public int Statue_06_EXP { get; set; }
        public int Statue_07_Level { get; set; }
        public int Statue_07_EXP { get; set; }
        public int Statue_08_Level { get; set; }
        public int Statue_08_EXP { get; set; }
        public int Statue_09_Level { get; set; }
        public int Statue_09_EXP { get; set; }
        public int Statue_10_Level { get; set; }
        public int Statue_10_EXP { get; set; }
        public int Statue_11_Level { get; set; }
        public int Statue_11_EXP { get; set; }
        public int Statue_12_Level { get; set; }
        public int Statue_12_EXP { get; set; }
        public int Statue_13_Level { get; set; }
        public int Statue_13_EXP { get; set; }
        public int Statue_14_Level { get; set; }
        public int Statue_14_EXP { get; set; }
        public int Statue_15_Level { get; set; }
        public int Statue_15_EXP { get; set; }
        public int Statue_16_Level { get; set; }
        public int Statue_16_EXP { get; set; }
        public int Statue_17_Level { get; set; }
        public int Statue_17_EXP { get; set; }
        public int Statue_18_Level { get; set; }
        public int Statue_18_EXP { get; set; }
        public int Statue_19_Level { get; set; }
        public int Statue_19_EXP { get; set; }
        public int Statue_20_Level { get; set; }
        public int Statue_20_EXP { get; set; }
        public int Statue_21_Level { get; set; }
        public int Statue_21_EXP { get; set; }
        public int Statue_22_Level { get; set; }
        public int Statue_22_EXP { get; set; }
        public int Statue_23_Level { get; set; }
        public int Statue_23_EXP { get; set; }
        public int Statue_24_Level { get; set; }
        public int Statue_24_EXP { get; set; }
        public int Statue_25_Level { get; set; }
        public int Statue_25_EXP { get; set; }
        public int Statue_26_Level { get; set; }
        public int Statue_26_EXP { get; set; }
        public int Statue_27_Level { get; set; }
        public int Statue_27_EXP { get; set; }
        public int Statue_28_Level { get; set; }
        public int Statue_28_EXP { get; set; }
        public int Statue_29_Level { get; set; }
        public int Statue_29_EXP { get; set; }
        public int Statue_30_Level { get; set; }
        public int Statue_30_EXP { get; set; }

        public int Desk_1_01 { get; set; }
        public int Desk_1_02 { get; set; }
        public int Desk_1_03 { get; set; }
        public int Desk_1_04 { get; set; }
        public int Desk_1_05 { get; set; }

        public int Desk_2_01 { get; set; }
        public int Desk_2_02 { get; set; }
        public int Desk_2_03 { get; set; }
        public int Desk_2_04 { get; set; }
        public int Desk_2_05 { get; set; }

        public int Desk_3_01 { get; set; }
        public int Desk_3_02 { get; set; }
        public int Desk_3_03 { get; set; }
        public int Desk_3_04 { get; set; }
        public int Desk_3_05 { get; set; }

        public int Desk_4_01 { get; set; }
        public int Desk_4_02 { get; set; }
        public int Desk_4_03 { get; set; }
        public int Desk_4_04 { get; set; }
        public int Desk_4_05 { get; set; }

        public int Desk_5_01 { get; set; }
        public int Desk_5_02 { get; set; }
        public int Desk_5_03 { get; set; }
        public int Desk_5_04 { get; set; }
        public int Desk_5_05 { get; set; }

        public int Desk_6_01 { get; set; }
        public int Desk_6_02 { get; set; }
        public int Desk_6_03 { get; set; }
        public int Desk_6_04 { get; set; }
        public int Desk_6_05 { get; set; }

        public int Desk_7_01 { get; set; }
        public int Desk_7_02 { get; set; }
        public int Desk_7_03 { get; set; }
        public int Desk_7_04 { get; set; }
        public int Desk_7_05 { get; set; }

        public int Desk_8_01 { get; set; }
        public int Desk_8_02 { get; set; }
        public int Desk_8_03 { get; set; }
        public int Desk_8_04 { get; set; }
        public int Desk_8_05 { get; set; }

        public int Selected_Map { get; set; }
        public int Map_01 { get; set; }
        public int Map_02 { get; set; }
        public int Map_03 { get; set; }
        public int Map_04 { get; set; }
        public int Map_05 { get; set; }
        public int Map_06 { get; set; }
        public int Map_07 { get; set; }
        public int Map_08 { get; set; }
        public int Map_09 { get; set; }
        public int Map_10 { get; set; }
        public int Map_11 { get; set; }
        public int Map_12 { get; set; }
        public int Map_13 { get; set; }
        public int Map_14 { get; set; }
        public int Map_15 { get; set; }

        public int ClaimReward_101 { get; set; }
        public int ClaimReward_102 { get; set; }
        public int ClaimReward_103 { get; set; }
        public int ClaimReward_104 { get; set; }
        public int ClaimReward_105 { get; set; }
        public int ClaimReward_106 { get; set; }
        public int ClaimReward_107 { get; set; }
        public int ClaimReward_108 { get; set; }
        public int ClaimReward_109 { get; set; }
        public int ClaimReward_110 { get; set; }
        public int ClaimReward_111 { get; set; }
        public int ClaimReward_112 { get; set; }
        public int ClaimReward_113 { get; set; }
        public int ClaimReward_114 { get; set; }
        public int ClaimReward_115 { get; set; }
        public int ClaimReward_116 { get; set; }
        public int ClaimReward_117 { get; set; }
        public int ClaimReward_118 { get; set; }
        public int ClaimReward_119 { get; set; }
        public int ClaimReward_120 { get; set; }
        public int ClaimReward_121 { get; set; }
        public int ClaimReward_122 { get; set; }
        public int ClaimReward_123 { get; set; }
        public int ClaimReward_124 { get; set; }
        public int ClaimReward_125 { get; set; }
        public int ClaimReward_126 { get; set; }
        public int ClaimReward_127 { get; set; }
        public int ClaimReward_128 { get; set; }
        public int ClaimReward_129 { get; set; }
        public int ClaimReward_130 { get; set; }
        public int ClaimReward_131 { get; set; }
        public int ClaimReward_132 { get; set; }
        public int ClaimReward_133 { get; set; }
        public int ClaimReward_134 { get; set; }
        public int ClaimReward_135 { get; set; }
        public int ClaimReward_136 { get; set; }
        public int ClaimReward_137 { get; set; }
        public int ClaimReward_138 { get; set; }
        public int ClaimReward_139 { get; set; }
        public int ClaimReward_140 { get; set; }
        public int ClaimReward_141 { get; set; }
        public int ClaimReward_142 { get; set; }
        public int ClaimReward_143 { get; set; }
        public int ClaimReward_144 { get; set; }
        public int ClaimReward_145 { get; set; }
        public int ClaimReward_146 { get; set; }
        public int ClaimReward_147 { get; set; }
        public int ClaimReward_148 { get; set; }
        public int ClaimReward_149 { get; set; }
        public int ClaimReward_150 { get; set; }

        public int ClaimReward_201 { get; set; }
        public int ClaimReward_202 { get; set; }
        public int ClaimReward_203 { get; set; }
        public int ClaimReward_204 { get; set; }
        public int ClaimReward_205 { get; set; }
        public int ClaimReward_206 { get; set; }
        public int ClaimReward_207 { get; set; }
        public int ClaimReward_208 { get; set; }
        public int ClaimReward_209 { get; set; }
        public int ClaimReward_210 { get; set; }
        public int ClaimReward_211 { get; set; }
        public int ClaimReward_212 { get; set; }
        public int ClaimReward_213 { get; set; }
        public int ClaimReward_214 { get; set; }
        public int ClaimReward_215 { get; set; }
        public int ClaimReward_216 { get; set; }
        public int ClaimReward_217 { get; set; }
        public int ClaimReward_218 { get; set; }
        public int ClaimReward_219 { get; set; }
        public int ClaimReward_220 { get; set; }
        public int ClaimReward_221 { get; set; }
        public int ClaimReward_222 { get; set; }
        public int ClaimReward_223 { get; set; }
        public int ClaimReward_224 { get; set; }
        public int ClaimReward_225 { get; set; }
        public int ClaimReward_226 { get; set; }
        public int ClaimReward_227 { get; set; }
        public int ClaimReward_228 { get; set; }
        public int ClaimReward_229 { get; set; }
        public int ClaimReward_230 { get; set; }
        public int ClaimReward_231 { get; set; }
        public int ClaimReward_232 { get; set; }
        public int ClaimReward_233 { get; set; }
        public int ClaimReward_234 { get; set; }
        public int ClaimReward_235 { get; set; }
        public int ClaimReward_236 { get; set; }
        public int ClaimReward_237 { get; set; }
        public int ClaimReward_238 { get; set; }
        public int ClaimReward_239 { get; set; }
        public int ClaimReward_240 { get; set; }
        public int ClaimReward_241 { get; set; }
        public int ClaimReward_242 { get; set; }
        public int ClaimReward_243 { get; set; }
        public int ClaimReward_244 { get; set; }
        public int ClaimReward_245 { get; set; }
        public int ClaimReward_246 { get; set; }
        public int ClaimReward_247 { get; set; }
        public int ClaimReward_248 { get; set; }
        public int ClaimReward_249 { get; set; }
        public int ClaimReward_250 { get; set; }

        public int ClaimReward_301 { get; set; }
        public int ClaimReward_302 { get; set; }
        public int ClaimReward_303 { get; set; }
        public int ClaimReward_304 { get; set; }
        public int ClaimReward_305 { get; set; }
        public int ClaimReward_306 { get; set; }
        public int ClaimReward_307 { get; set; }
        public int ClaimReward_308 { get; set; }
        public int ClaimReward_309 { get; set; }
        public int ClaimReward_310 { get; set; }
        public int ClaimReward_311 { get; set; }
        public int ClaimReward_312 { get; set; }
        public int ClaimReward_313 { get; set; }
        public int ClaimReward_314 { get; set; }
        public int ClaimReward_315 { get; set; }
        public int ClaimReward_316 { get; set; }
        public int ClaimReward_317 { get; set; }
        public int ClaimReward_318 { get; set; }
        public int ClaimReward_319 { get; set; }
        public int ClaimReward_320 { get; set; }
        public int ClaimReward_321 { get; set; }
        public int ClaimReward_322 { get; set; }
        public int ClaimReward_323 { get; set; }
        public int ClaimReward_324 { get; set; }
        public int ClaimReward_325 { get; set; }
        public int ClaimReward_326 { get; set; }
        public int ClaimReward_327 { get; set; }
        public int ClaimReward_328 { get; set; }
        public int ClaimReward_329 { get; set; }
        public int ClaimReward_330 { get; set; }

        public int ClaimReward_401 { get; set; }
        public int ClaimReward_402 { get; set; }
        public int ClaimReward_403 { get; set; }
        public int ClaimReward_404 { get; set; }
        public int ClaimReward_405 { get; set; }
        public int ClaimReward_406 { get; set; }
        public int ClaimReward_407 { get; set; }
        public int ClaimReward_408 { get; set; }
        public int ClaimReward_409 { get; set; }
        public int ClaimReward_410 { get; set; }
        public int ClaimReward_411 { get; set; }
        public int ClaimReward_412 { get; set; }
        public int ClaimReward_413 { get; set; }
        public int ClaimReward_414 { get; set; }
        public int ClaimReward_415 { get; set; }
        public int ClaimReward_416 { get; set; }
        public int ClaimReward_417 { get; set; }
        public int ClaimReward_418 { get; set; }
        public int ClaimReward_419 { get; set; }
        public int ClaimReward_420 { get; set; }
        public int ClaimReward_421 { get; set; }
        public int ClaimReward_422 { get; set; }
        public int ClaimReward_423 { get; set; }
        public int ClaimReward_424 { get; set; }
        public int ClaimReward_425 { get; set; }
        public int ClaimReward_426 { get; set; }
        public int ClaimReward_427 { get; set; }
        public int ClaimReward_428 { get; set; }
        public int ClaimReward_429 { get; set; }
        public int ClaimReward_430 { get; set; }

        public int ClaimReward_501 { get; set; }
        public int ClaimReward_502 { get; set; }
        public int ClaimReward_503 { get; set; }
        public int ClaimReward_504 { get; set; }
        public int ClaimReward_505 { get; set; }
        public int ClaimReward_506 { get; set; }
        public int ClaimReward_507 { get; set; }
        public int ClaimReward_508 { get; set; }
        public int ClaimReward_509 { get; set; }
        public int ClaimReward_510 { get; set; }
        public int ClaimReward_511 { get; set; }
        public int ClaimReward_512 { get; set; }
        public int ClaimReward_513 { get; set; }
        public int ClaimReward_514 { get; set; }
        public int ClaimReward_515 { get; set; }
        public int ClaimReward_516 { get; set; }
        public int ClaimReward_517 { get; set; }
        public int ClaimReward_518 { get; set; }
        public int ClaimReward_519 { get; set; }
        public int ClaimReward_520 { get; set; }
        public int ClaimReward_521 { get; set; }
        public int ClaimReward_522 { get; set; }
        public int ClaimReward_523 { get; set; }
        public int ClaimReward_524 { get; set; }
        public int ClaimReward_525 { get; set; }
        public int ClaimReward_526 { get; set; }
        public int ClaimReward_527 { get; set; }
        public int ClaimReward_528 { get; set; }
        public int ClaimReward_529 { get; set; }
        public int ClaimReward_530 { get; set; }

        public int ClaimReward_601 { get; set; }
        public int ClaimReward_602 { get; set; }
        public int ClaimReward_603 { get; set; }
        public int ClaimReward_604 { get; set; }
        public int ClaimReward_605 { get; set; }
        public int ClaimReward_606 { get; set; }
        public int ClaimReward_607 { get; set; }
        public int ClaimReward_608 { get; set; }
        public int ClaimReward_609 { get; set; }
        public int ClaimReward_610 { get; set; }
        public int ClaimReward_611 { get; set; }
        public int ClaimReward_612 { get; set; }
        public int ClaimReward_613 { get; set; }
        public int ClaimReward_614 { get; set; }
        public int ClaimReward_615 { get; set; }
        public int ClaimReward_616 { get; set; }
        public int ClaimReward_617 { get; set; }
        public int ClaimReward_618 { get; set; }
        public int ClaimReward_619 { get; set; }
        public int ClaimReward_620 { get; set; }
        public int ClaimReward_621 { get; set; }
        public int ClaimReward_622 { get; set; }
        public int ClaimReward_623 { get; set; }
        public int ClaimReward_624 { get; set; }
        public int ClaimReward_625 { get; set; }
        public int ClaimReward_626 { get; set; }
        public int ClaimReward_627 { get; set; }
        public int ClaimReward_628 { get; set; }
        public int ClaimReward_629 { get; set; }
        public int ClaimReward_630 { get; set; }

        public int ClaimReward_701 { get; set; }
        public int ClaimReward_702 { get; set; }
        public int ClaimReward_703 { get; set; }
        public int ClaimReward_704 { get; set; }
        public int ClaimReward_705 { get; set; }
        public int ClaimReward_706 { get; set; }
        public int ClaimReward_707 { get; set; }
        public int ClaimReward_708 { get; set; }
        public int ClaimReward_709 { get; set; }
        public int ClaimReward_710 { get; set; }
        public int ClaimReward_711 { get; set; }
        public int ClaimReward_712 { get; set; }
        public int ClaimReward_713 { get; set; }
        public int ClaimReward_714 { get; set; }
        public int ClaimReward_715 { get; set; }
        public int ClaimReward_716 { get; set; }
        public int ClaimReward_717 { get; set; }
        public int ClaimReward_718 { get; set; }
        public int ClaimReward_719 { get; set; }
        public int ClaimReward_720 { get; set; }
        public int ClaimReward_721 { get; set; }
        public int ClaimReward_722 { get; set; }
        public int ClaimReward_723 { get; set; }
        public int ClaimReward_724 { get; set; }
        public int ClaimReward_725 { get; set; }
        public int ClaimReward_726 { get; set; }
        public int ClaimReward_727 { get; set; }
        public int ClaimReward_728 { get; set; }
        public int ClaimReward_729 { get; set; }
        public int ClaimReward_730 { get; set; }

        public int Selected_Icon { get; set; }
        public int Icon_01 { get; set; }
        public int Icon_02 { get; set; }
        public int Icon_03 { get; set; }
        public int Icon_04 { get; set; }
        public int Icon_05 { get; set; }
        public int Icon_06 { get; set; }
        public int Icon_07 { get; set; }
        public int Icon_08 { get; set; }
        public int Icon_09 { get; set; }
        public int Icon_10 { get; set; }
        public int Icon_11 { get; set; }
        public int Icon_12 { get; set; }
        public int Icon_13 { get; set; }
        public int Icon_14 { get; set; }
        public int Icon_15 { get; set; }
        public int Icon_16 { get; set; }
        public int Icon_17 { get; set; }
        public int Icon_18 { get; set; }
        public int Icon_19 { get; set; }
        public int Icon_20 { get; set; }
        public int Icon_21 { get; set; }
        public int Icon_22 { get; set; }
        public int Icon_23 { get; set; }
        public int Icon_24 { get; set; }
        public int Icon_25 { get; set; }
        public int Icon_26 { get; set; }
        public int Icon_27 { get; set; }
        public int Icon_28 { get; set; }
        public int Icon_29 { get; set; }
        public int Icon_30 { get; set; }
        public int Icon_31 { get; set; }
        public int Icon_32 { get; set; }
        public int Icon_33 { get; set; }
        public int Icon_34 { get; set; }
        public int Icon_35 { get; set; }
        public int Icon_36 { get; set; }
        public int Icon_37 { get; set; }
        public int Icon_38 { get; set; }
        public int Icon_39 { get; set; }
        public int Icon_40 { get; set; }

        public int Selected_Emoji_01 { get; set; }
        public int Selected_Emoji_02 { get; set; }
        public int Selected_Emoji_03 { get; set; }
        public int Selected_Emoji_04 { get; set; }
        public int Selected_Emoji_05 { get; set; }

        public int Emoji_01 { get; set; }
        public int Emoji_02 { get; set; }
        public int Emoji_03 { get; set; }
        public int Emoji_04 { get; set; }
        public int Emoji_05 { get; set; }
        public int Emoji_06 { get; set; }
        public int Emoji_07 { get; set; }
        public int Emoji_08 { get; set; }
        public int Emoji_09 { get; set; }
        public int Emoji_10 { get; set; }
        public int Emoji_11 { get; set; }
        public int Emoji_12 { get; set; }
        public int Emoji_13 { get; set; }
        public int Emoji_14 { get; set; }
        public int Emoji_15 { get; set; }
        public int Emoji_16 { get; set; }
        public int Emoji_17 { get; set; }
        public int Emoji_18 { get; set; }
        public int Emoji_19 { get; set; }
        public int Emoji_20 { get; set; }
        public int Emoji_21 { get; set; }
        public int Emoji_22 { get; set; }
        public int Emoji_23 { get; set; }
        public int Emoji_24 { get; set; }
        public int Emoji_25 { get; set; }
        public int Emoji_26 { get; set; }
        public int Emoji_27 { get; set; }
        public int Emoji_28 { get; set; }
        public int Emoji_29 { get; set; }
        public int Emoji_30 { get; set; }

        public int Current_Desk { get; set; }
        public int Timer_01 { get; set; }
        public int Timer_02 { get; set; }
        public int Timer_03 { get; set; }
        public int Timer_04 { get; set; }
        public int Timer_05 { get; set; }
        public int Critical_Damage { get; set; }

        public int D_Task_1_Type { get; set; }
        public int D_Task_2_Type { get; set; }
        public int D_Task_3_Type { get; set; }

        public int D_Task_1_QTY { get; set; }
        public int D_Task_2_QTY { get; set; }
        public int D_Task_3_QTY { get; set; }

        public int D_Task_Current_QTY_1 { get; set; }
        public int D_Task_Current_QTY_2 { get; set; }
        public int D_Task_Current_QTY_3 { get; set; }

        public int W_Task_1_Type { get; set; }
        public int W_Task_2_Type { get; set; }
        public int W_Task_3_Type { get; set; }

        public int W_Task_1_QTY { get; set; }
        public int W_Task_2_QTY { get; set; }
        public int W_Task_3_QTY { get; set; }

        public int W_Task_Current_QTY_1 { get; set; }
        public int W_Task_Current_QTY_2 { get; set; }
        public int W_Task_Current_QTY_3 { get; set; }

        public int D_Reward_1_1_Claim { get; set; }
        public int D_Reward_1_2_Claim { get; set; }
        public int D_Reward_Level_1_Claim { get; set; }
        public int D_Reward_2_1_Claim { get; set; }
        public int D_Reward_2_2_Claim { get; set; }
        public int D_Reward_Level_2_Claim { get; set; }
        public int D_Reward_3_1_Claim { get; set; }
        public int D_Reward_3_2_Claim { get; set; }
        public int D_Reward_Level_3_Claim { get; set; }
        public int D_Reward_4_1_Claim { get; set; }
        public int D_Reward_4_2_Claim { get; set; }
        public int D_Reward_Level_4_Claim { get; set; }
        public int D_Reward_5_1_Claim { get; set; }
        public int D_Reward_5_2_Claim { get; set; }
        public int D_Reward_Level_5_Claim { get; set; }
        public int D_Reward_6_1_Claim { get; set; }
        public int D_Reward_6_2_Claim { get; set; }
        public int D_Reward_Level_6_Claim { get; set; }
        public int D_Reward_7_1_Claim { get; set; }
        public int D_Reward_7_2_Claim { get; set; }
        public int D_Reward_Level_7_Claim { get; set; }
        public int D_Reward_8_1_Claim { get; set; }
        public int D_Reward_8_2_Claim { get; set; }
        public int D_Reward_Level_8_Claim { get; set; }
        public int D_Reward_9_1_Claim { get; set; }
        public int D_Reward_9_2_Claim { get; set; }
        public int D_Reward_Level_9_Claim { get; set; }
        public int W_RewarW_1_1_Claim { get; set; }
        public int W_RewarW_1_2_Claim { get; set; }
        public int W_RewarW_Level_1_Claim { get; set; }
        public int W_RewarW_2_1_Claim { get; set; }
        public int W_RewarW_2_2_Claim { get; set; }
        public int W_RewarW_Level_2_Claim { get; set; }
        public int W_RewarW_3_1_Claim { get; set; }
        public int W_RewarW_3_2_Claim { get; set; }
        public int W_RewarW_Level_3_Claim { get; set; }
        public int W_RewarW_4_1_Claim { get; set; }
        public int W_RewarW_4_2_Claim { get; set; }
        public int W_RewarW_Level_4_Claim { get; set; }
        public int W_RewarW_5_1_Claim { get; set; }
        public int W_RewarW_5_2_Claim { get; set; }
        public int W_RewarW_Level_5_Claim { get; set; }
        public int W_RewarW_6_1_Claim { get; set; }
        public int W_RewarW_6_2_Claim { get; set; }
        public int W_RewarW_Level_6_Claim { get; set; }
        public int W_RewarW_7_1_Claim { get; set; }
        public int W_RewarW_7_2_Claim { get; set; }
        public int W_RewarW_Level_7_Claim { get; set; }
        public int W_RewarW_8_1_Claim { get; set; }
        public int W_RewarW_8_2_Claim { get; set; }
        public int W_RewarW_Level_8_Claim { get; set; }
        public int W_RewarW_9_1_Claim { get; set; }
        public int W_RewarW_9_2_Claim { get; set; }
        public int W_RewarW_Level_9_Claim { get; set; }

        public int Last_Refresh_Sell_Time { get; set; }
        public int Last_Refresh_Exchange_Time { get; set; }
        public int Refresh_InGame_Sell { get; set; }
        public int Refresh_Exchange { get; set; }
        public int Special_Sell_01 { get; set; }
        public int Special_Sell_02 { get; set; }
        public int Special_Sell_03 { get; set; }
        public int Special_Sell_04 { get; set; }
        public int Special_Sell_05 { get; set; }
        public int Special_Sell_06 { get; set; }
        public int Special_Sell_07 { get; set; }
        public int Special_Sell_08 { get; set; }
        public int Special_Sell_09 { get; set; }
        public int Special_Sell_10 { get; set; }
        public int Special_Sell_11 { get; set; }
        public int Special_Sell_12 { get; set; }
        public int Special_Sell_13 { get; set; }
        public int Special_Sell_14 { get; set; }

        public int Special_QTY_01 { get; set; }
        public int Special_QTY_02 { get; set; }
        public int Special_QTY_03 { get; set; }
        public int Special_QTY_04 { get; set; }
        public int Special_QTY_05 { get; set; }
        public int Special_QTY_06 { get; set; }
        public int Special_QTY_07 { get; set; }
        public int Special_QTY_08 { get; set; }
        public int Special_QTY_09 { get; set; }
        public int Special_QTY_10 { get; set; }
        public int Special_QTY_11 { get; set; }
        public int Special_QTY_12 { get; set; }
        public int Special_QTY_13 { get; set; }
        public int Special_QTY_14 { get; set; }

        public int InGame_Sell_1 { get; set; }
        public int InGame_Sell_2 { get; set; }
        public int InGame_Sell_3 { get; set; }
        public int InGame_Sell_4 { get; set; }
        public int InGame_Sell_5 { get; set; }
        public int InGame_Sell_6 { get; set; }
        public int InGame_Sell_7 { get; set; }
        public int InGame_Sell_8 { get; set; }
        public int InGame_QTY_1 { get; set; }
        public int InGame_QTY_2 { get; set; }
        public int InGame_QTY_3 { get; set; }
        public int InGame_QTY_4 { get; set; }
        public int InGame_QTY_5 { get; set; }
        public int InGame_QTY_6 { get; set; }
        public int InGame_QTY_7 { get; set; }
        public int InGame_QTY_8 { get; set; }
        public int InGame_Price_1 { get; set; }
        public int InGame_Price_2 { get; set; }
        public int InGame_Price_3 { get; set; }
        public int InGame_Price_4 { get; set; }
        public int InGame_Price_5 { get; set; }
        public int InGame_Price_6 { get; set; }
        public int InGame_Price_7 { get; set; }
        public int InGame_Price_8 { get; set; }
        public int InGame_Currency_1 { get; set; }
        public int InGame_Currency_2 { get; set; }
        public int InGame_Currency_3 { get; set; }
        public int InGame_Currency_4 { get; set; }
        public int InGame_Currency_5 { get; set; }
        public int InGame_Currency_6 { get; set; }
        public int InGame_Currency_7 { get; set; }
        public int InGame_Currency_8 { get; set; }
        public int InGame_Sold_1 { get; set; }
        public int InGame_Sold_2 { get; set; }
        public int InGame_Sold_3 { get; set; }
        public int InGame_Sold_4 { get; set; }
        public int InGame_Sold_5 { get; set; }
        public int InGame_Sold_6 { get; set; }
        public int InGame_Sold_7 { get; set; }
        public int InGame_Sold_8 { get; set; }

        public int Exchange_1 { get; set; }
        public int Exchange_2 { get; set; }
        public int Exchange_3 { get; set; }
        public int Exchange_4 { get; set; }
        public int Exchange_5 { get; set; }
        public int Exchange_6 { get; set; }
        public int Exchange_7 { get; set; }
        public int Exchange_8 { get; set; }
        public int Exchange_QTY_1 { get; set; }
        public int Exchange_QTY_2 { get; set; }
        public int Exchange_QTY_3 { get; set; }
        public int Exchange_QTY_4 { get; set; }
        public int Exchange_QTY_5 { get; set; }
        public int Exchange_QTY_6 { get; set; }
        public int Exchange_QTY_7 { get; set; }
        public int Exchange_QTY_8 { get; set; }
        public int Exchange_Price_1 { get; set; }
        public int Exchange_Price_2 { get; set; }
        public int Exchange_Price_3 { get; set; }
        public int Exchange_Price_4 { get; set; }
        public int Exchange_Price_5 { get; set; }
        public int Exchange_Price_6 { get; set; }
        public int Exchange_Price_7 { get; set; }
        public int Exchange_Price_8 { get; set; }
        public int Exchange_Currency_1 { get; set; }
        public int Exchange_Currency_2 { get; set; }
        public int Exchange_Currency_3 { get; set; }
        public int Exchange_Currency_4 { get; set; }
        public int Exchange_Currency_5 { get; set; }
        public int Exchange_Currency_6 { get; set; }
        public int Exchange_Currency_7 { get; set; }
        public int Exchange_Currency_8 { get; set; }
        public int Exchange_Sold_1 { get; set; }
        public int Exchange_Sold_2 { get; set; }
        public int Exchange_Sold_3 { get; set; }
        public int Exchange_Sold_4 { get; set; }
        public int Exchange_Sold_5 { get; set; }
        public int Exchange_Sold_6 { get; set; }
        public int Exchange_Sold_7 { get; set; }
        public int Exchange_Sold_8 { get; set; }

        public int Pack_Sell_1 { get; set; }
        public int Pack_Sell_2 { get; set; }
        public int Pack_Sell_3 { get; set; }
        public int Pack_Sell_4 { get; set; }
        public int Pack_Sell_5 { get; set; }
        public int Pack_Sell_6 { get; set; }
        public int Pack_Sell_7 { get; set; }
        public int Pack_Sell_8 { get; set; }
        public int Pack_QTY_1 { get; set; }
        public int Pack_QTY_2 { get; set; }
        public int Pack_QTY_3 { get; set; }
        public int Pack_QTY_4 { get; set; }
        public int Pack_QTY_5 { get; set; }
        public int Pack_QTY_6 { get; set; }
        public int Pack_QTY_7 { get; set; }
        public int Pack_QTY_8 { get; set; }
        public int Pack_Price_1 { get; set; }
        public int Pack_Price_2 { get; set; }
        public int Pack_Price_3 { get; set; }
        public int Pack_Price_4 { get; set; }
        public int Pack_Price_5 { get; set; }
        public int Pack_Price_6 { get; set; }
        public int Pack_Price_7 { get; set; }
        public int Pack_Price_8 { get; set; }
        public int Pack_Currency_1 { get; set; }
        public int Pack_Currency_2 { get; set; }
        public int Pack_Currency_3 { get; set; }
        public int Pack_Currency_4 { get; set; }
        public int Pack_Currency_5 { get; set; }
        public int Pack_Currency_6 { get; set; }
        public int Pack_Currency_7 { get; set; }
        public int Pack_Currency_8 { get; set; }
    }

    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Debug.Log("Player_Status || Start");

        if (!UI_Main)
        {
            GameObject ui = GameObject.Find("Main_UI");
            UI_Main = ui;
        }

        if (!demoProfilesBehaviour)
            demoProfilesBehaviour = GameObject.FindObjectOfType<DemoProfilesBehaviour>();

        if (demoProfilesBehaviour)
            demoProfilesBehaviour.Load_Profile_FinishEvent.AddListener(ClientGetProfile);

        GameObject pool_manager = GameObject.Find("Pool_Manager");
        GameObject obj;
        if (!pool_manager && Pool_Manager)
        {
            obj = Instantiate(Pool_Manager, Vector3.zero, Quaternion.identity);
            obj.name = "Pool_Manager";
        }
    }

    public void Set_Local_Profile(ObservableProfile profile)
    {
        player.DisplayName = profile.GetProperty<ObservableString>((short)MstProFilePropertyCode.DisplayName).GetValue();
        player.Player_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Player_EXP).GetValue();
        player.Player_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Player_Level).GetValue();
        player.Diamond = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Diamond).GetValue();
        player.Gold = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).GetValue();
        player.Token_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_01).GetValue();
        player.Token_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_02).GetValue();
        player.Token_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_03).GetValue();
        player.Token_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_04).GetValue();
        player.Token_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_05).GetValue();
        player.Token_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_06).GetValue();
        player.Token_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_07).GetValue();
        player.Token_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_08).GetValue();
        player.Token_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_09).GetValue();
        player.Token_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_10).GetValue();
        player.Tower_101_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_101_Level).GetValue();
        player.Tower_101_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_101_EXP).GetValue();
        player.Tower_102_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_102_Level).GetValue();
        player.Tower_102_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_102_EXP).GetValue();
        player.Tower_103_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_103_Level).GetValue();
        player.Tower_103_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_103_EXP).GetValue();
        player.Tower_104_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_104_Level).GetValue();
        player.Tower_104_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_104_EXP).GetValue();
        player.Tower_105_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_105_Level).GetValue();
        player.Tower_105_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_105_EXP).GetValue();
        player.Tower_106_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_106_Level).GetValue();
        player.Tower_106_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_106_EXP).GetValue();
        player.Tower_107_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_107_Level).GetValue();
        player.Tower_107_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_107_EXP).GetValue();
        player.Tower_108_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_108_Level).GetValue();
        player.Tower_108_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_108_EXP).GetValue();
        player.Tower_109_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_109_Level).GetValue();
        player.Tower_109_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_109_EXP).GetValue();
        player.Tower_110_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_110_Level).GetValue();
        player.Tower_110_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_110_EXP).GetValue();
        player.Tower_111_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_111_Level).GetValue();
        player.Tower_111_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_111_EXP).GetValue();
        player.Tower_112_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_112_Level).GetValue();
        player.Tower_112_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_112_EXP).GetValue();
        player.Tower_113_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_113_Level).GetValue();
        player.Tower_113_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_113_EXP).GetValue();
        player.Tower_114_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_114_Level).GetValue();
        player.Tower_114_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_114_EXP).GetValue();
        player.Tower_115_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_115_Level).GetValue();
        player.Tower_115_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_115_EXP).GetValue();
        player.Tower_116_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_116_Level).GetValue();
        player.Tower_116_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_116_EXP).GetValue();
        player.Tower_117_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_117_Level).GetValue();
        player.Tower_117_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_117_EXP).GetValue();
        player.Tower_118_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_118_Level).GetValue();
        player.Tower_118_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_118_EXP).GetValue();
        player.Tower_119_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_119_Level).GetValue();
        player.Tower_119_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_119_EXP).GetValue();
        player.Tower_120_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_120_Level).GetValue();
        player.Tower_120_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_120_EXP).GetValue();
        player.Tower_201_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_201_Level).GetValue();
        player.Tower_201_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_201_EXP).GetValue();
        player.Tower_202_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_202_Level).GetValue();
        player.Tower_202_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_202_EXP).GetValue();
        player.Tower_203_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_203_Level).GetValue();
        player.Tower_203_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_203_EXP).GetValue();
        player.Tower_204_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_204_Level).GetValue();
        player.Tower_204_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_204_EXP).GetValue();
        player.Tower_205_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_205_Level).GetValue();
        player.Tower_205_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_205_EXP).GetValue();
        player.Tower_206_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_206_Level).GetValue();
        player.Tower_206_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_206_EXP).GetValue();
        player.Tower_207_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_207_Level).GetValue();
        player.Tower_207_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_207_EXP).GetValue();
        player.Tower_208_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_208_Level).GetValue();
        player.Tower_208_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_208_EXP).GetValue();
        player.Tower_209_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_209_Level).GetValue();
        player.Tower_209_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_209_EXP).GetValue();
        player.Tower_210_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_210_Level).GetValue();
        player.Tower_210_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_210_EXP).GetValue();
        player.Tower_211_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_211_Level).GetValue();
        player.Tower_211_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_211_EXP).GetValue();
        player.Tower_212_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_212_Level).GetValue();
        player.Tower_212_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_212_EXP).GetValue();
        player.Tower_213_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_213_Level).GetValue();
        player.Tower_213_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_213_EXP).GetValue();
        player.Tower_214_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_214_Level).GetValue();
        player.Tower_214_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_214_EXP).GetValue();
        player.Tower_215_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_215_Level).GetValue();
        player.Tower_215_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_215_EXP).GetValue();
        player.Tower_216_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_216_Level).GetValue();
        player.Tower_216_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_216_EXP).GetValue();
        player.Tower_217_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_217_Level).GetValue();
        player.Tower_217_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_217_EXP).GetValue();
        player.Tower_218_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_218_Level).GetValue();
        player.Tower_218_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_218_EXP).GetValue();
        player.Tower_219_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_219_Level).GetValue();
        player.Tower_219_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_219_EXP).GetValue();
        player.Tower_220_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_220_Level).GetValue();
        player.Tower_220_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_220_EXP).GetValue();
        player.Tower_301_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_301_Level).GetValue();
        player.Tower_301_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_301_EXP).GetValue();
        player.Tower_302_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_302_Level).GetValue();
        player.Tower_302_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_302_EXP).GetValue();
        player.Tower_303_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_303_Level).GetValue();
        player.Tower_303_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_303_EXP).GetValue();
        player.Tower_304_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_304_Level).GetValue();
        player.Tower_304_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_304_EXP).GetValue();
        player.Tower_305_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_305_Level).GetValue();
        player.Tower_305_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_305_EXP).GetValue();
        player.Tower_306_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_306_Level).GetValue();
        player.Tower_306_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_306_EXP).GetValue();
        player.Tower_307_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_307_Level).GetValue();
        player.Tower_307_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_307_EXP).GetValue();
        player.Tower_308_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_308_Level).GetValue();
        player.Tower_308_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_308_EXP).GetValue();
        player.Tower_309_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_309_Level).GetValue();
        player.Tower_309_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_309_EXP).GetValue();
        player.Tower_310_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_310_Level).GetValue();
        player.Tower_310_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_310_EXP).GetValue();
        player.Tower_311_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_311_Level).GetValue();
        player.Tower_311_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_311_EXP).GetValue();
        player.Tower_312_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_312_Level).GetValue();
        player.Tower_312_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_312_EXP).GetValue();
        player.Tower_313_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_313_Level).GetValue();
        player.Tower_313_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_313_EXP).GetValue();
        player.Tower_314_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_314_Level).GetValue();
        player.Tower_314_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_314_EXP).GetValue();
        player.Tower_315_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_315_Level).GetValue();
        player.Tower_315_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_315_EXP).GetValue();
        player.Tower_316_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_316_Level).GetValue();
        player.Tower_316_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_316_EXP).GetValue();
        player.Tower_317_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_317_Level).GetValue();
        player.Tower_317_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_317_EXP).GetValue();
        player.Tower_318_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_318_Level).GetValue();
        player.Tower_318_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_318_EXP).GetValue();
        player.Tower_319_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_319_Level).GetValue();
        player.Tower_319_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_319_EXP).GetValue();
        player.Tower_320_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_320_Level).GetValue();
        player.Tower_320_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_320_EXP).GetValue();
        player.Tower_401_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_401_Level).GetValue();
        player.Tower_401_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_401_EXP).GetValue();
        player.Tower_402_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_402_Level).GetValue();
        player.Tower_402_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_402_EXP).GetValue();
        player.Tower_403_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_403_Level).GetValue();
        player.Tower_403_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_403_EXP).GetValue();
        player.Tower_404_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_404_Level).GetValue();
        player.Tower_404_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_404_EXP).GetValue();
        player.Tower_405_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_405_Level).GetValue();
        player.Tower_405_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_405_EXP).GetValue();
        player.Tower_406_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_406_Level).GetValue();
        player.Tower_406_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_406_EXP).GetValue();
        player.Tower_407_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_407_Level).GetValue();
        player.Tower_407_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_407_EXP).GetValue();
        player.Tower_408_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_408_Level).GetValue();
        player.Tower_408_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_408_EXP).GetValue();
        player.Tower_409_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_409_Level).GetValue();
        player.Tower_409_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_409_EXP).GetValue();
        player.Tower_410_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_410_Level).GetValue();
        player.Tower_410_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_410_EXP).GetValue();
        player.Tower_411_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_411_Level).GetValue();
        player.Tower_411_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_411_EXP).GetValue();
        player.Tower_412_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_412_Level).GetValue();
        player.Tower_412_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_412_EXP).GetValue();
        player.Tower_413_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_413_Level).GetValue();
        player.Tower_413_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_413_EXP).GetValue();
        player.Tower_414_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_414_Level).GetValue();
        player.Tower_414_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_414_EXP).GetValue();
        player.Tower_415_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_415_Level).GetValue();
        player.Tower_415_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_415_EXP).GetValue();
        player.Tower_416_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_416_Level).GetValue();
        player.Tower_416_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_416_EXP).GetValue();
        player.Tower_417_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_417_Level).GetValue();
        player.Tower_417_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_417_EXP).GetValue();
        player.Tower_418_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_418_Level).GetValue();
        player.Tower_418_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_418_EXP).GetValue();
        player.Tower_419_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_419_Level).GetValue();
        player.Tower_419_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_419_EXP).GetValue();
        player.Tower_420_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_420_Level).GetValue();
        player.Tower_420_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_420_EXP).GetValue();
        player.Tower_501_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_501_Level).GetValue();
        player.Tower_501_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_501_EXP).GetValue();
        player.Tower_502_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_502_Level).GetValue();
        player.Tower_502_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_502_EXP).GetValue();
        player.Tower_503_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_503_Level).GetValue();
        player.Tower_503_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_503_EXP).GetValue();
        player.Tower_504_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_504_Level).GetValue();
        player.Tower_504_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_504_EXP).GetValue();
        player.Tower_505_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_505_Level).GetValue();
        player.Tower_505_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_505_EXP).GetValue();
        player.Tower_506_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_506_Level).GetValue();
        player.Tower_506_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_506_EXP).GetValue();
        player.Tower_507_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_507_Level).GetValue();
        player.Tower_507_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_507_EXP).GetValue();
        player.Tower_508_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_508_Level).GetValue();
        player.Tower_508_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_508_EXP).GetValue();
        player.Tower_509_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_509_Level).GetValue();
        player.Tower_509_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_509_EXP).GetValue();
        player.Tower_510_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_510_Level).GetValue();
        player.Tower_510_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_510_EXP).GetValue();
        player.Tower_511_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_511_Level).GetValue();
        player.Tower_511_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_511_EXP).GetValue();
        player.Tower_512_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_512_Level).GetValue();
        player.Tower_512_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_512_EXP).GetValue();
        player.Tower_513_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_513_Level).GetValue();
        player.Tower_513_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_513_EXP).GetValue();
        player.Tower_514_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_514_Level).GetValue();
        player.Tower_514_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_514_EXP).GetValue();
        player.Tower_515_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_515_Level).GetValue();
        player.Tower_515_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_515_EXP).GetValue();
        player.Tower_516_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_516_Level).GetValue();
        player.Tower_516_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_516_EXP).GetValue();
        player.Tower_517_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_517_Level).GetValue();
        player.Tower_517_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_517_EXP).GetValue();
        player.Tower_518_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_518_Level).GetValue();
        player.Tower_518_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_518_EXP).GetValue();
        player.Tower_519_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_519_Level).GetValue();
        player.Tower_519_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_519_EXP).GetValue();
        player.Tower_520_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_520_Level).GetValue();
        player.Tower_520_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_520_EXP).GetValue();
        player.Tower_601_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_601_Level).GetValue();
        player.Tower_601_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_601_EXP).GetValue();
        player.Tower_602_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_602_Level).GetValue();
        player.Tower_602_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_602_EXP).GetValue();
        player.Tower_603_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_603_Level).GetValue();
        player.Tower_603_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_603_EXP).GetValue();
        player.Tower_604_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_604_Level).GetValue();
        player.Tower_604_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_604_EXP).GetValue();
        player.Tower_605_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_605_Level).GetValue();
        player.Tower_605_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_605_EXP).GetValue();
        player.Tower_606_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_606_Level).GetValue();
        player.Tower_606_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_606_EXP).GetValue();
        player.Tower_607_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_607_Level).GetValue();
        player.Tower_607_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_607_EXP).GetValue();
        player.Tower_608_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_608_Level).GetValue();
        player.Tower_608_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_608_EXP).GetValue();
        player.Tower_609_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_609_Level).GetValue();
        player.Tower_609_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_609_EXP).GetValue();
        player.Tower_610_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_610_Level).GetValue();
        player.Tower_610_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_610_EXP).GetValue();
        player.Tower_611_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_611_Level).GetValue();
        player.Tower_611_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_611_EXP).GetValue();
        player.Tower_612_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_612_Level).GetValue();
        player.Tower_612_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_612_EXP).GetValue();
        player.Tower_613_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_613_Level).GetValue();
        player.Tower_613_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_613_EXP).GetValue();
        player.Tower_614_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_614_Level).GetValue();
        player.Tower_614_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_614_EXP).GetValue();
        player.Tower_615_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_615_Level).GetValue();
        player.Tower_615_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_615_EXP).GetValue();
        player.Tower_616_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_616_Level).GetValue();
        player.Tower_616_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_616_EXP).GetValue();
        player.Tower_617_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_617_Level).GetValue();
        player.Tower_617_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_617_EXP).GetValue();
        player.Tower_618_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_618_Level).GetValue();
        player.Tower_618_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_618_EXP).GetValue();
        player.Tower_619_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_619_Level).GetValue();
        player.Tower_619_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_619_EXP).GetValue();
        player.Tower_620_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_620_Level).GetValue();
        player.Tower_620_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_620_EXP).GetValue();
        player.Tower_621_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_621_Level).GetValue();
        player.Tower_621_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_621_EXP).GetValue();
        player.Tower_622_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_622_Level).GetValue();
        player.Tower_622_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_622_EXP).GetValue();
        player.Tower_623_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_623_Level).GetValue();
        player.Tower_623_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_623_EXP).GetValue();
        player.Tower_624_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_624_Level).GetValue();
        player.Tower_624_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_624_EXP).GetValue();
        player.Tower_625_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_625_Level).GetValue();
        player.Tower_625_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_625_EXP).GetValue();
        player.Tower_626_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_626_Level).GetValue();
        player.Tower_626_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_626_EXP).GetValue();
        player.Tower_627_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_627_Level).GetValue();
        player.Tower_627_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_627_EXP).GetValue();
        player.Tower_628_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_628_Level).GetValue();
        player.Tower_628_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_628_EXP).GetValue();
        player.Tower_629_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_629_Level).GetValue();
        player.Tower_629_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_629_EXP).GetValue();
        player.Tower_630_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_630_Level).GetValue();
        player.Tower_630_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_630_EXP).GetValue();
        player.Tower_701_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_701_Level).GetValue();
        player.Tower_701_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_701_EXP).GetValue();
        player.Tower_702_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_702_Level).GetValue();
        player.Tower_702_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_702_EXP).GetValue();
        player.Tower_703_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_703_Level).GetValue();
        player.Tower_703_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_703_EXP).GetValue();
        player.Tower_704_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_704_Level).GetValue();
        player.Tower_704_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_704_EXP).GetValue();
        player.Tower_705_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_705_Level).GetValue();
        player.Tower_705_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_705_EXP).GetValue();
        player.Tower_706_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_706_Level).GetValue();
        player.Tower_706_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_706_EXP).GetValue();
        player.Tower_707_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_707_Level).GetValue();
        player.Tower_707_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_707_EXP).GetValue();
        player.Tower_708_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_708_Level).GetValue();
        player.Tower_708_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_708_EXP).GetValue();
        player.Tower_709_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_709_Level).GetValue();
        player.Tower_709_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_709_EXP).GetValue();
        player.Tower_710_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_710_Level).GetValue();
        player.Tower_710_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_710_EXP).GetValue();
        player.Tower_711_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_711_Level).GetValue();
        player.Tower_711_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_711_EXP).GetValue();
        player.Tower_712_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_712_Level).GetValue();
        player.Tower_712_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_712_EXP).GetValue();
        player.Tower_713_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_713_Level).GetValue();
        player.Tower_713_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_713_EXP).GetValue();
        player.Tower_714_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_714_Level).GetValue();
        player.Tower_714_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_714_EXP).GetValue();
        player.Tower_715_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_715_Level).GetValue();
        player.Tower_715_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_715_EXP).GetValue();
        player.Tower_716_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_716_Level).GetValue();
        player.Tower_716_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_716_EXP).GetValue();
        player.Tower_717_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_717_Level).GetValue();
        player.Tower_717_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_717_EXP).GetValue();
        player.Tower_718_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_718_Level).GetValue();
        player.Tower_718_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_718_EXP).GetValue();
        player.Tower_719_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_719_Level).GetValue();
        player.Tower_719_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_719_EXP).GetValue();
        player.Tower_720_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_720_Level).GetValue();
        player.Tower_720_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_720_EXP).GetValue();
        player.Tower_721_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_721_Level).GetValue();
        player.Tower_721_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_721_EXP).GetValue();
        player.Tower_722_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_722_Level).GetValue();
        player.Tower_722_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_722_EXP).GetValue();
        player.Tower_723_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_723_Level).GetValue();
        player.Tower_723_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_723_EXP).GetValue();
        player.Tower_724_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_724_Level).GetValue();
        player.Tower_724_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_724_EXP).GetValue();
        player.Tower_725_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_725_Level).GetValue();
        player.Tower_725_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_725_EXP).GetValue();
        player.Tower_726_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_726_Level).GetValue();
        player.Tower_726_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_726_EXP).GetValue();
        player.Tower_727_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_727_Level).GetValue();
        player.Tower_727_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_727_EXP).GetValue();
        player.Tower_728_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_728_Level).GetValue();
        player.Tower_728_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_728_EXP).GetValue();
        player.Tower_729_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_729_Level).GetValue();
        player.Tower_729_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_729_EXP).GetValue();
        player.Tower_730_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_730_Level).GetValue();
        player.Tower_730_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_730_EXP).GetValue();
        player.Tower_731_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_731_Level).GetValue();
        player.Tower_731_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_731_EXP).GetValue();
        player.Tower_732_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_732_Level).GetValue();
        player.Tower_732_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_732_EXP).GetValue();
        player.Tower_733_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_733_Level).GetValue();
        player.Tower_733_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_733_EXP).GetValue();
        player.Tower_734_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_734_Level).GetValue();
        player.Tower_734_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_734_EXP).GetValue();
        player.Tower_735_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_735_Level).GetValue();
        player.Tower_735_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_735_EXP).GetValue();
        player.Tower_736_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_736_Level).GetValue();
        player.Tower_736_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_736_EXP).GetValue();
        player.Tower_737_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_737_Level).GetValue();
        player.Tower_737_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_737_EXP).GetValue();
        player.Tower_738_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_738_Level).GetValue();
        player.Tower_738_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_738_EXP).GetValue();
        player.Tower_739_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_739_Level).GetValue();
        player.Tower_739_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_739_EXP).GetValue();
        player.Tower_740_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_740_Level).GetValue();
        player.Tower_740_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_740_EXP).GetValue();
        player.Tower_741_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_741_Level).GetValue();
        player.Tower_741_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_741_EXP).GetValue();
        player.Tower_742_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_742_Level).GetValue();
        player.Tower_742_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_742_EXP).GetValue();
        player.Tower_743_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_743_Level).GetValue();
        player.Tower_743_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_743_EXP).GetValue();
        player.Tower_744_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_744_Level).GetValue();
        player.Tower_744_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_744_EXP).GetValue();
        player.Tower_745_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_745_Level).GetValue();
        player.Tower_745_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_745_EXP).GetValue();
        player.Tower_746_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_746_Level).GetValue();
        player.Tower_746_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_746_EXP).GetValue();
        player.Tower_747_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_747_Level).GetValue();
        player.Tower_747_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_747_EXP).GetValue();
        player.Tower_748_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_748_Level).GetValue();
        player.Tower_748_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_748_EXP).GetValue();
        player.Tower_749_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_749_Level).GetValue();
        player.Tower_749_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_749_EXP).GetValue();
        player.Tower_750_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_750_Level).GetValue();
        player.Tower_750_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Tower_750_EXP).GetValue();
        player.Statue_01_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_01_Level).GetValue();
        player.Statue_01_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_01_EXP).GetValue();
        player.Statue_02_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_02_Level).GetValue();
        player.Statue_02_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_02_EXP).GetValue();
        player.Statue_03_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_03_Level).GetValue();
        player.Statue_03_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_03_EXP).GetValue();
        player.Statue_04_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_04_Level).GetValue();
        player.Statue_04_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_04_EXP).GetValue();
        player.Statue_05_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_05_Level).GetValue();
        player.Statue_05_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_05_EXP).GetValue();
        player.Statue_06_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_06_Level).GetValue();
        player.Statue_06_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_06_EXP).GetValue();
        player.Statue_07_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_07_Level).GetValue();
        player.Statue_07_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_07_EXP).GetValue();
        player.Statue_08_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_08_Level).GetValue();
        player.Statue_08_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_08_EXP).GetValue();
        player.Statue_09_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_09_Level).GetValue();
        player.Statue_09_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_09_EXP).GetValue();
        player.Statue_10_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_10_Level).GetValue();
        player.Statue_10_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_10_EXP).GetValue();
        player.Statue_11_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_11_Level).GetValue();
        player.Statue_11_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_11_EXP).GetValue();
        player.Statue_12_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_12_Level).GetValue();
        player.Statue_12_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_12_EXP).GetValue();
        player.Statue_13_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_13_Level).GetValue();
        player.Statue_13_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_13_EXP).GetValue();
        player.Statue_14_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_14_Level).GetValue();
        player.Statue_14_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_14_EXP).GetValue();
        player.Statue_15_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_15_Level).GetValue();
        player.Statue_15_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_15_EXP).GetValue();
        player.Statue_16_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_16_Level).GetValue();
        player.Statue_16_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_16_EXP).GetValue();
        player.Statue_17_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_17_Level).GetValue();
        player.Statue_17_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_17_EXP).GetValue();
        player.Statue_18_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_18_Level).GetValue();
        player.Statue_18_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_18_EXP).GetValue();
        player.Statue_19_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_19_Level).GetValue();
        player.Statue_19_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_19_EXP).GetValue();
        player.Statue_20_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_20_Level).GetValue();
        player.Statue_20_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_20_EXP).GetValue();
        player.Statue_21_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_21_Level).GetValue();
        player.Statue_21_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_21_EXP).GetValue();
        player.Statue_22_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_22_Level).GetValue();
        player.Statue_22_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_22_EXP).GetValue();
        player.Statue_23_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_23_Level).GetValue();
        player.Statue_23_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_23_EXP).GetValue();
        player.Statue_24_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_24_Level).GetValue();
        player.Statue_24_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_24_EXP).GetValue();
        player.Statue_25_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_25_Level).GetValue();
        player.Statue_25_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_25_EXP).GetValue();
        player.Statue_26_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_26_Level).GetValue();
        player.Statue_26_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_26_EXP).GetValue();
        player.Statue_27_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_27_Level).GetValue();
        player.Statue_27_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_27_EXP).GetValue();
        player.Statue_28_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_28_Level).GetValue();
        player.Statue_28_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_28_EXP).GetValue();
        player.Statue_29_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_29_Level).GetValue();
        player.Statue_29_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_29_EXP).GetValue();
        player.Statue_30_Level = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_30_Level).GetValue();
        player.Statue_30_EXP = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Statue_30_EXP).GetValue();
        player.Desk_1_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_01).GetValue();
        player.Desk_1_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_02).GetValue();
        player.Desk_1_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_03).GetValue();
        player.Desk_1_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_04).GetValue();
        player.Desk_1_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_05).GetValue();
        player.Desk_2_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_01).GetValue();
        player.Desk_2_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_02).GetValue();
        player.Desk_2_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_03).GetValue();
        player.Desk_2_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_04).GetValue();
        player.Desk_2_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_2_05).GetValue();
        player.Desk_3_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_01).GetValue();
        player.Desk_3_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_02).GetValue();
        player.Desk_3_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_03).GetValue();
        player.Desk_3_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_04).GetValue();
        player.Desk_3_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_3_05).GetValue();
        player.Desk_4_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_01).GetValue();
        player.Desk_4_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_02).GetValue();
        player.Desk_4_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_03).GetValue();
        player.Desk_4_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_04).GetValue();
        player.Desk_4_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_4_05).GetValue();
        player.Desk_5_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_01).GetValue();
        player.Desk_5_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_02).GetValue();
        player.Desk_5_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_03).GetValue();
        player.Desk_5_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_04).GetValue();
        player.Desk_5_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_5_05).GetValue();
        player.Desk_6_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_01).GetValue();
        player.Desk_6_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_02).GetValue();
        player.Desk_6_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_03).GetValue();
        player.Desk_6_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_04).GetValue();
        player.Desk_6_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_6_05).GetValue();
        player.Desk_7_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_01).GetValue();
        player.Desk_7_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_02).GetValue();
        player.Desk_7_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_03).GetValue();
        player.Desk_7_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_04).GetValue();
        player.Desk_7_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_7_05).GetValue();
        player.Desk_8_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_01).GetValue();
        player.Desk_8_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_02).GetValue();
        player.Desk_8_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_03).GetValue();
        player.Desk_8_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_04).GetValue();
        player.Desk_8_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_8_05).GetValue();
        player.Selected_Map = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Map).GetValue();
        player.Map_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_01).GetValue();
        player.Map_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_02).GetValue();
        player.Map_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_03).GetValue();
        player.Map_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_04).GetValue();
        player.Map_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_05).GetValue();
        player.Map_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_06).GetValue();
        player.Map_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_07).GetValue();
        player.Map_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_08).GetValue();
        player.Map_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_09).GetValue();
        player.Map_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_10).GetValue();
        player.Map_11 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_11).GetValue();
        player.Map_12 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_12).GetValue();
        player.Map_13 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_13).GetValue();
        player.Map_14 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_14).GetValue();
        player.Map_15 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Map_15).GetValue();
        player.ClaimReward_101 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_101).GetValue();
        player.ClaimReward_102 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_102).GetValue();
        player.ClaimReward_103 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_103).GetValue();
        player.ClaimReward_104 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_104).GetValue();
        player.ClaimReward_105 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_105).GetValue();
        player.ClaimReward_106 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_106).GetValue();
        player.ClaimReward_107 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_107).GetValue();
        player.ClaimReward_108 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_108).GetValue();
        player.ClaimReward_109 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_109).GetValue();
        player.ClaimReward_110 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_110).GetValue();
        player.ClaimReward_111 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_111).GetValue();
        player.ClaimReward_112 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_112).GetValue();
        player.ClaimReward_113 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_113).GetValue();
        player.ClaimReward_114 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_114).GetValue();
        player.ClaimReward_115 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_115).GetValue();
        player.ClaimReward_116 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_116).GetValue();
        player.ClaimReward_117 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_117).GetValue();
        player.ClaimReward_118 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_118).GetValue();
        player.ClaimReward_119 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_119).GetValue();
        player.ClaimReward_120 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_120).GetValue();
        player.ClaimReward_121 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_121).GetValue();
        player.ClaimReward_122 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_122).GetValue();
        player.ClaimReward_123 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_123).GetValue();
        player.ClaimReward_124 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_124).GetValue();
        player.ClaimReward_125 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_125).GetValue();
        player.ClaimReward_126 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_126).GetValue();
        player.ClaimReward_127 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_127).GetValue();
        player.ClaimReward_128 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_128).GetValue();
        player.ClaimReward_129 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_129).GetValue();
        player.ClaimReward_130 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_130).GetValue();
        player.ClaimReward_131 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_131).GetValue();
        player.ClaimReward_132 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_132).GetValue();
        player.ClaimReward_133 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_133).GetValue();
        player.ClaimReward_134 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_134).GetValue();
        player.ClaimReward_135 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_135).GetValue();
        player.ClaimReward_136 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_136).GetValue();
        player.ClaimReward_137 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_137).GetValue();
        player.ClaimReward_138 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_138).GetValue();
        player.ClaimReward_139 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_139).GetValue();
        player.ClaimReward_140 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_140).GetValue();
        player.ClaimReward_141 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_141).GetValue();
        player.ClaimReward_142 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_142).GetValue();
        player.ClaimReward_143 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_143).GetValue();
        player.ClaimReward_144 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_144).GetValue();
        player.ClaimReward_145 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_145).GetValue();
        player.ClaimReward_146 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_146).GetValue();
        player.ClaimReward_147 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_147).GetValue();
        player.ClaimReward_148 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_148).GetValue();
        player.ClaimReward_149 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_149).GetValue();
        player.ClaimReward_150 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_150).GetValue();
        player.ClaimReward_201 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_201).GetValue();
        player.ClaimReward_202 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_202).GetValue();
        player.ClaimReward_203 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_203).GetValue();
        player.ClaimReward_204 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_204).GetValue();
        player.ClaimReward_205 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_205).GetValue();
        player.ClaimReward_206 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_206).GetValue();
        player.ClaimReward_207 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_207).GetValue();
        player.ClaimReward_208 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_208).GetValue();
        player.ClaimReward_209 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_209).GetValue();
        player.ClaimReward_210 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_210).GetValue();
        player.ClaimReward_211 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_211).GetValue();
        player.ClaimReward_212 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_212).GetValue();
        player.ClaimReward_213 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_213).GetValue();
        player.ClaimReward_214 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_214).GetValue();
        player.ClaimReward_215 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_215).GetValue();
        player.ClaimReward_216 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_216).GetValue();
        player.ClaimReward_217 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_217).GetValue();
        player.ClaimReward_218 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_218).GetValue();
        player.ClaimReward_219 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_219).GetValue();
        player.ClaimReward_220 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_220).GetValue();
        player.ClaimReward_221 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_221).GetValue();
        player.ClaimReward_222 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_222).GetValue();
        player.ClaimReward_223 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_223).GetValue();
        player.ClaimReward_224 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_224).GetValue();
        player.ClaimReward_225 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_225).GetValue();
        player.ClaimReward_226 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_226).GetValue();
        player.ClaimReward_227 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_227).GetValue();
        player.ClaimReward_228 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_228).GetValue();
        player.ClaimReward_229 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_229).GetValue();
        player.ClaimReward_230 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_230).GetValue();
        player.ClaimReward_231 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_231).GetValue();
        player.ClaimReward_232 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_232).GetValue();
        player.ClaimReward_233 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_233).GetValue();
        player.ClaimReward_234 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_234).GetValue();
        player.ClaimReward_235 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_235).GetValue();
        player.ClaimReward_236 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_236).GetValue();
        player.ClaimReward_237 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_237).GetValue();
        player.ClaimReward_238 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_238).GetValue();
        player.ClaimReward_239 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_239).GetValue();
        player.ClaimReward_240 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_240).GetValue();
        player.ClaimReward_241 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_241).GetValue();
        player.ClaimReward_242 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_242).GetValue();
        player.ClaimReward_243 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_243).GetValue();
        player.ClaimReward_244 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_244).GetValue();
        player.ClaimReward_245 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_245).GetValue();
        player.ClaimReward_246 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_246).GetValue();
        player.ClaimReward_247 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_247).GetValue();
        player.ClaimReward_248 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_248).GetValue();
        player.ClaimReward_249 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_249).GetValue();
        player.ClaimReward_250 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_250).GetValue();
        player.ClaimReward_301 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_301).GetValue();
        player.ClaimReward_302 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_302).GetValue();
        player.ClaimReward_303 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_303).GetValue();
        player.ClaimReward_304 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_304).GetValue();
        player.ClaimReward_305 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_305).GetValue();
        player.ClaimReward_306 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_306).GetValue();
        player.ClaimReward_307 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_307).GetValue();
        player.ClaimReward_308 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_308).GetValue();
        player.ClaimReward_309 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_309).GetValue();
        player.ClaimReward_310 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_310).GetValue();
        player.ClaimReward_311 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_311).GetValue();
        player.ClaimReward_312 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_312).GetValue();
        player.ClaimReward_313 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_313).GetValue();
        player.ClaimReward_314 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_314).GetValue();
        player.ClaimReward_315 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_315).GetValue();
        player.ClaimReward_316 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_316).GetValue();
        player.ClaimReward_317 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_317).GetValue();
        player.ClaimReward_318 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_318).GetValue();
        player.ClaimReward_319 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_319).GetValue();
        player.ClaimReward_320 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_320).GetValue();
        player.ClaimReward_321 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_321).GetValue();
        player.ClaimReward_322 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_322).GetValue();
        player.ClaimReward_323 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_323).GetValue();
        player.ClaimReward_324 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_324).GetValue();
        player.ClaimReward_325 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_325).GetValue();
        player.ClaimReward_326 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_326).GetValue();
        player.ClaimReward_327 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_327).GetValue();
        player.ClaimReward_328 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_328).GetValue();
        player.ClaimReward_329 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_329).GetValue();
        player.ClaimReward_330 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_330).GetValue();
        player.ClaimReward_401 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_401).GetValue();
        player.ClaimReward_402 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_402).GetValue();
        player.ClaimReward_403 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_403).GetValue();
        player.ClaimReward_404 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_404).GetValue();
        player.ClaimReward_405 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_405).GetValue();
        player.ClaimReward_406 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_406).GetValue();
        player.ClaimReward_407 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_407).GetValue();
        player.ClaimReward_408 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_408).GetValue();
        player.ClaimReward_409 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_409).GetValue();
        player.ClaimReward_410 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_410).GetValue();
        player.ClaimReward_411 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_411).GetValue();
        player.ClaimReward_412 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_412).GetValue();
        player.ClaimReward_413 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_413).GetValue();
        player.ClaimReward_414 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_414).GetValue();
        player.ClaimReward_415 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_415).GetValue();
        player.ClaimReward_416 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_416).GetValue();
        player.ClaimReward_417 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_417).GetValue();
        player.ClaimReward_418 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_418).GetValue();
        player.ClaimReward_419 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_419).GetValue();
        player.ClaimReward_420 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_420).GetValue();
        player.ClaimReward_421 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_421).GetValue();
        player.ClaimReward_422 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_422).GetValue();
        player.ClaimReward_423 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_423).GetValue();
        player.ClaimReward_424 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_424).GetValue();
        player.ClaimReward_425 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_425).GetValue();
        player.ClaimReward_426 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_426).GetValue();
        player.ClaimReward_427 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_427).GetValue();
        player.ClaimReward_428 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_428).GetValue();
        player.ClaimReward_429 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_429).GetValue();
        player.ClaimReward_430 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_430).GetValue();
        player.ClaimReward_501 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_501).GetValue();
        player.ClaimReward_502 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_502).GetValue();
        player.ClaimReward_503 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_503).GetValue();
        player.ClaimReward_504 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_504).GetValue();
        player.ClaimReward_505 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_505).GetValue();
        player.ClaimReward_506 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_506).GetValue();
        player.ClaimReward_507 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_507).GetValue();
        player.ClaimReward_508 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_508).GetValue();
        player.ClaimReward_509 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_509).GetValue();
        player.ClaimReward_510 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_510).GetValue();
        player.ClaimReward_511 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_511).GetValue();
        player.ClaimReward_512 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_512).GetValue();
        player.ClaimReward_513 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_513).GetValue();
        player.ClaimReward_514 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_514).GetValue();
        player.ClaimReward_515 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_515).GetValue();
        player.ClaimReward_516 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_516).GetValue();
        player.ClaimReward_517 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_517).GetValue();
        player.ClaimReward_518 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_518).GetValue();
        player.ClaimReward_519 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_519).GetValue();
        player.ClaimReward_520 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_520).GetValue();
        player.ClaimReward_521 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_521).GetValue();
        player.ClaimReward_522 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_522).GetValue();
        player.ClaimReward_523 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_523).GetValue();
        player.ClaimReward_524 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_524).GetValue();
        player.ClaimReward_525 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_525).GetValue();
        player.ClaimReward_526 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_526).GetValue();
        player.ClaimReward_527 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_527).GetValue();
        player.ClaimReward_528 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_528).GetValue();
        player.ClaimReward_529 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_529).GetValue();
        player.ClaimReward_530 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_530).GetValue();
        player.ClaimReward_601 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_601).GetValue();
        player.ClaimReward_602 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_602).GetValue();
        player.ClaimReward_603 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_603).GetValue();
        player.ClaimReward_604 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_604).GetValue();
        player.ClaimReward_605 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_605).GetValue();
        player.ClaimReward_606 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_606).GetValue();
        player.ClaimReward_607 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_607).GetValue();
        player.ClaimReward_608 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_608).GetValue();
        player.ClaimReward_609 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_609).GetValue();
        player.ClaimReward_610 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_610).GetValue();
        player.ClaimReward_611 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_611).GetValue();
        player.ClaimReward_612 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_612).GetValue();
        player.ClaimReward_613 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_613).GetValue();
        player.ClaimReward_614 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_614).GetValue();
        player.ClaimReward_615 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_615).GetValue();
        player.ClaimReward_616 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_616).GetValue();
        player.ClaimReward_617 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_617).GetValue();
        player.ClaimReward_618 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_618).GetValue();
        player.ClaimReward_619 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_619).GetValue();
        player.ClaimReward_620 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_620).GetValue();
        player.ClaimReward_621 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_621).GetValue();
        player.ClaimReward_622 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_622).GetValue();
        player.ClaimReward_623 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_623).GetValue();
        player.ClaimReward_624 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_624).GetValue();
        player.ClaimReward_625 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_625).GetValue();
        player.ClaimReward_626 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_626).GetValue();
        player.ClaimReward_627 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_627).GetValue();
        player.ClaimReward_628 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_628).GetValue();
        player.ClaimReward_629 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_629).GetValue();
        player.ClaimReward_630 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_630).GetValue();
        player.ClaimReward_701 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_701).GetValue();
        player.ClaimReward_702 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_702).GetValue();
        player.ClaimReward_703 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_703).GetValue();
        player.ClaimReward_704 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_704).GetValue();
        player.ClaimReward_705 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_705).GetValue();
        player.ClaimReward_706 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_706).GetValue();
        player.ClaimReward_707 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_707).GetValue();
        player.ClaimReward_708 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_708).GetValue();
        player.ClaimReward_709 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_709).GetValue();
        player.ClaimReward_710 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_710).GetValue();
        player.ClaimReward_711 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_711).GetValue();
        player.ClaimReward_712 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_712).GetValue();
        player.ClaimReward_713 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_713).GetValue();
        player.ClaimReward_714 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_714).GetValue();
        player.ClaimReward_715 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_715).GetValue();
        player.ClaimReward_716 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_716).GetValue();
        player.ClaimReward_717 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_717).GetValue();
        player.ClaimReward_718 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_718).GetValue();
        player.ClaimReward_719 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_719).GetValue();
        player.ClaimReward_720 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_720).GetValue();
        player.ClaimReward_721 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_721).GetValue();
        player.ClaimReward_722 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_722).GetValue();
        player.ClaimReward_723 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_723).GetValue();
        player.ClaimReward_724 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_724).GetValue();
        player.ClaimReward_725 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_725).GetValue();
        player.ClaimReward_726 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_726).GetValue();
        player.ClaimReward_727 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_727).GetValue();
        player.ClaimReward_728 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_728).GetValue();
        player.ClaimReward_729 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_729).GetValue();
        player.ClaimReward_730 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.ClaimReward_730).GetValue();
        player.Selected_Icon = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Icon).GetValue();
        player.Icon_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_01).GetValue();
        player.Icon_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_02).GetValue();
        player.Icon_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_03).GetValue();
        player.Icon_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_04).GetValue();
        player.Icon_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_05).GetValue();
        player.Icon_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_06).GetValue();
        player.Icon_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_07).GetValue();
        player.Icon_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_08).GetValue();
        player.Icon_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_09).GetValue();
        player.Icon_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_10).GetValue();
        player.Icon_11 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_11).GetValue();
        player.Icon_12 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_12).GetValue();
        player.Icon_13 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_13).GetValue();
        player.Icon_14 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_14).GetValue();
        player.Icon_15 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_15).GetValue();
        player.Icon_16 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_16).GetValue();
        player.Icon_17 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_17).GetValue();
        player.Icon_18 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_18).GetValue();
        player.Icon_19 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_19).GetValue();
        player.Icon_20 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_20).GetValue();
        player.Icon_21 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_21).GetValue();
        player.Icon_22 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_22).GetValue();
        player.Icon_23 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_23).GetValue();
        player.Icon_24 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_24).GetValue();
        player.Icon_25 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_25).GetValue();
        player.Icon_26 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_26).GetValue();
        player.Icon_27 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_27).GetValue();
        player.Icon_28 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_28).GetValue();
        player.Icon_29 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_29).GetValue();
        player.Icon_30 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_30).GetValue();
        player.Icon_31 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_31).GetValue();
        player.Icon_32 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_32).GetValue();
        player.Icon_33 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_33).GetValue();
        player.Icon_34 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_34).GetValue();
        player.Icon_35 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_35).GetValue();
        player.Icon_36 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_36).GetValue();
        player.Icon_37 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_37).GetValue();
        player.Icon_38 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_38).GetValue();
        player.Icon_39 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_39).GetValue();
        player.Icon_40 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Icon_40).GetValue();
        player.Selected_Emoji_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Emoji_01).GetValue();
        player.Selected_Emoji_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Emoji_02).GetValue();
        player.Selected_Emoji_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Emoji_03).GetValue();
        player.Selected_Emoji_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Emoji_04).GetValue();
        player.Selected_Emoji_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Selected_Emoji_05).GetValue();
        player.Emoji_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_01).GetValue();
        player.Emoji_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_02).GetValue();
        player.Emoji_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_03).GetValue();
        player.Emoji_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_04).GetValue();
        player.Emoji_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_05).GetValue();
        player.Emoji_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_06).GetValue();
        player.Emoji_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_07).GetValue();
        player.Emoji_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_08).GetValue();
        player.Emoji_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_09).GetValue();
        player.Emoji_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_10).GetValue();
        player.Emoji_11 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_11).GetValue();
        player.Emoji_12 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_12).GetValue();
        player.Emoji_13 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_13).GetValue();
        player.Emoji_14 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_14).GetValue();
        player.Emoji_15 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_15).GetValue();
        player.Emoji_16 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_16).GetValue();
        player.Emoji_17 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_17).GetValue();
        player.Emoji_18 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_18).GetValue();
        player.Emoji_19 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_19).GetValue();
        player.Emoji_20 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_20).GetValue();
        player.Emoji_21 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_21).GetValue();
        player.Emoji_22 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_22).GetValue();
        player.Emoji_23 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_23).GetValue();
        player.Emoji_24 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_24).GetValue();
        player.Emoji_25 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_25).GetValue();
        player.Emoji_26 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_26).GetValue();
        player.Emoji_27 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_27).GetValue();
        player.Emoji_28 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_28).GetValue();
        player.Emoji_29 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_29).GetValue();
        player.Emoji_30 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Emoji_30).GetValue();
        player.Current_Desk = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Current_Desk).GetValue();
        player.Timer_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_01).GetValue();
        player.Timer_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_02).GetValue();
        player.Timer_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_03).GetValue();
        player.Timer_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_04).GetValue();
        player.Timer_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_05).GetValue(); // Last_Login
        player.Critical_Damage = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Critical_Damage).GetValue();

        player.D_Task_1_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_Type).GetValue();
        player.D_Task_2_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_Type).GetValue();
        player.D_Task_3_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_Type).GetValue();
        player.D_Task_1_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_QTY).GetValue();
        player.D_Task_2_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_QTY).GetValue();
        player.D_Task_3_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_QTY).GetValue();
        player.D_Task_Current_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_1).GetValue();
        player.D_Task_Current_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_2).GetValue();
        player.D_Task_Current_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_3).GetValue();
        player.W_Task_1_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_Type).GetValue();
        player.W_Task_2_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_Type).GetValue();
        player.W_Task_3_Type = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_Type).GetValue();
        player.W_Task_1_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_QTY).GetValue();
        player.W_Task_2_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_QTY).GetValue();
        player.W_Task_3_QTY = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_QTY).GetValue();
        player.W_Task_Current_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_1).GetValue();
        player.W_Task_Current_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_2).GetValue();
        player.W_Task_Current_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_3).GetValue();
        player.D_Reward_1_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_1_1_Claim).GetValue();
        player.D_Reward_1_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_1_2_Claim).GetValue();
        player.D_Reward_Level_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_1_Claim).GetValue();
        player.D_Reward_2_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_2_1_Claim).GetValue();
        player.D_Reward_2_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_2_2_Claim).GetValue();
        player.D_Reward_Level_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_2_Claim).GetValue();
        player.D_Reward_3_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_3_1_Claim).GetValue();
        player.D_Reward_3_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_3_2_Claim).GetValue();
        player.D_Reward_Level_3_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_3_Claim).GetValue();
        player.D_Reward_4_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_4_1_Claim).GetValue();
        player.D_Reward_4_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_4_2_Claim).GetValue();
        player.D_Reward_Level_4_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_4_Claim).GetValue();
        player.D_Reward_5_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_5_1_Claim).GetValue();
        player.D_Reward_5_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_5_2_Claim).GetValue();
        player.D_Reward_Level_5_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_5_Claim).GetValue();
        player.D_Reward_6_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_6_1_Claim).GetValue();
        player.D_Reward_6_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_6_2_Claim).GetValue();
        player.D_Reward_Level_6_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_6_Claim).GetValue();
        player.D_Reward_7_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_7_1_Claim).GetValue();
        player.D_Reward_7_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_7_2_Claim).GetValue();
        player.D_Reward_Level_7_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_7_Claim).GetValue();
        player.D_Reward_8_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_8_1_Claim).GetValue();
        player.D_Reward_8_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_8_2_Claim).GetValue();
        player.D_Reward_Level_8_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_8_Claim).GetValue();
        player.D_Reward_9_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_9_1_Claim).GetValue();
        player.D_Reward_9_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_9_2_Claim).GetValue();
        player.D_Reward_Level_9_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Reward_Level_9_Claim).GetValue();
        player.W_RewarW_1_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_1_1_Claim).GetValue();
        player.W_RewarW_1_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_1_2_Claim).GetValue();
        player.W_RewarW_Level_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_1_Claim).GetValue();
        player.W_RewarW_2_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_2_1_Claim).GetValue();
        player.W_RewarW_2_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_2_2_Claim).GetValue();
        player.W_RewarW_Level_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_2_Claim).GetValue();
        player.W_RewarW_3_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_3_1_Claim).GetValue();
        player.W_RewarW_3_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_3_2_Claim).GetValue();
        player.W_RewarW_Level_3_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_3_Claim).GetValue();
        player.W_RewarW_4_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_4_1_Claim).GetValue();
        player.W_RewarW_4_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_4_2_Claim).GetValue();
        player.W_RewarW_Level_4_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_4_Claim).GetValue();
        player.W_RewarW_5_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_5_1_Claim).GetValue();
        player.W_RewarW_5_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_5_2_Claim).GetValue();
        player.W_RewarW_Level_5_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_5_Claim).GetValue();
        player.W_RewarW_6_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_6_1_Claim).GetValue();
        player.W_RewarW_6_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_6_2_Claim).GetValue();
        player.W_RewarW_Level_6_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_6_Claim).GetValue();
        player.W_RewarW_7_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_7_1_Claim).GetValue();
        player.W_RewarW_7_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_7_2_Claim).GetValue();
        player.W_RewarW_Level_7_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_7_Claim).GetValue();
        player.W_RewarW_8_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_8_1_Claim).GetValue();
        player.W_RewarW_8_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_8_2_Claim).GetValue();
        player.W_RewarW_Level_8_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_8_Claim).GetValue();
        player.W_RewarW_9_1_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_9_1_Claim).GetValue();
        player.W_RewarW_9_2_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_9_2_Claim).GetValue();
        player.W_RewarW_Level_9_Claim = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_RewarW_Level_9_Claim).GetValue();

        player.Last_Refresh_Sell_Time = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Sell_Time).GetValue();
        player.Last_Refresh_Exchange_Time = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Exchange_Time).GetValue();
        player.Refresh_InGame_Sell = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Refresh_InGame_Sell).GetValue();
        player.Refresh_Exchange = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Refresh_Exchange).GetValue();
        player.Special_Sell_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_01).GetValue();
        player.Special_Sell_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_02).GetValue();
        player.Special_Sell_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_03).GetValue();
        player.Special_Sell_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_04).GetValue();
        player.Special_Sell_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_05).GetValue();
        player.Special_Sell_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_06).GetValue();
        player.Special_Sell_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_07).GetValue();
        player.Special_Sell_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_08).GetValue();
        player.Special_Sell_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_09).GetValue();
        player.Special_Sell_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_10).GetValue();
        player.Special_Sell_11 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_11).GetValue();
        player.Special_Sell_12 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_12).GetValue();
        player.Special_Sell_13 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_13).GetValue();
        player.Special_Sell_14 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_Sell_14).GetValue();
        player.Special_QTY_01 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_01).GetValue();
        player.Special_QTY_02 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_02).GetValue();
        player.Special_QTY_03 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_03).GetValue();
        player.Special_QTY_04 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_04).GetValue();
        player.Special_QTY_05 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_05).GetValue();
        player.Special_QTY_06 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_06).GetValue();
        player.Special_QTY_07 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_07).GetValue();
        player.Special_QTY_08 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_08).GetValue();
        player.Special_QTY_09 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_09).GetValue();
        player.Special_QTY_10 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_10).GetValue();
        player.Special_QTY_11 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_11).GetValue();
        player.Special_QTY_12 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_12).GetValue();
        player.Special_QTY_13 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_13).GetValue();
        player.Special_QTY_14 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Special_QTY_14).GetValue();
        player.InGame_Sell_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_1).GetValue();
        player.InGame_Sell_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_2).GetValue();
        player.InGame_Sell_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_3).GetValue();
        player.InGame_Sell_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_4).GetValue();
        player.InGame_Sell_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_5).GetValue();
        player.InGame_Sell_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_6).GetValue();
        player.InGame_Sell_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_7).GetValue();
        player.InGame_Sell_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sell_8).GetValue();
        player.InGame_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_1).GetValue();
        player.InGame_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_2).GetValue();
        player.InGame_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_3).GetValue();
        player.InGame_QTY_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_4).GetValue();
        player.InGame_QTY_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_5).GetValue();
        player.InGame_QTY_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_6).GetValue();
        player.InGame_QTY_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_7).GetValue();
        player.InGame_QTY_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_QTY_8).GetValue();
        player.InGame_Price_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_1).GetValue();
        player.InGame_Price_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_2).GetValue();
        player.InGame_Price_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_3).GetValue();
        player.InGame_Price_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_4).GetValue();
        player.InGame_Price_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_5).GetValue();
        player.InGame_Price_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_6).GetValue();
        player.InGame_Price_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_7).GetValue();
        player.InGame_Price_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Price_8).GetValue();
        player.InGame_Currency_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_1).GetValue();
        player.InGame_Currency_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_2).GetValue();
        player.InGame_Currency_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_3).GetValue();
        player.InGame_Currency_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_4).GetValue();
        player.InGame_Currency_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_5).GetValue();
        player.InGame_Currency_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_6).GetValue();
        player.InGame_Currency_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_7).GetValue();
        player.InGame_Currency_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Currency_8).GetValue();
        player.InGame_Sold_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_1).GetValue();
        player.InGame_Sold_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_2).GetValue();
        player.InGame_Sold_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_3).GetValue();
        player.InGame_Sold_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_4).GetValue();
        player.InGame_Sold_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_5).GetValue();
        player.InGame_Sold_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_6).GetValue();
        player.InGame_Sold_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_7).GetValue();
        player.InGame_Sold_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.InGame_Sold_8).GetValue();
        player.Exchange_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_1).GetValue();
        player.Exchange_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_2).GetValue();
        player.Exchange_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_3).GetValue();
        player.Exchange_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_4).GetValue();
        player.Exchange_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_5).GetValue();
        player.Exchange_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_6).GetValue();
        player.Exchange_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_7).GetValue();
        player.Exchange_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_8).GetValue();
        player.Exchange_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_1).GetValue();
        player.Exchange_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_2).GetValue();
        player.Exchange_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_3).GetValue();
        player.Exchange_QTY_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_4).GetValue();
        player.Exchange_QTY_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_5).GetValue();
        player.Exchange_QTY_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_6).GetValue();
        player.Exchange_QTY_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_7).GetValue();
        player.Exchange_QTY_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_QTY_8).GetValue();
        player.Exchange_Price_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_1).GetValue();
        player.Exchange_Price_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_2).GetValue();
        player.Exchange_Price_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_3).GetValue();
        player.Exchange_Price_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_4).GetValue();
        player.Exchange_Price_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_5).GetValue();
        player.Exchange_Price_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_6).GetValue();
        player.Exchange_Price_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_7).GetValue();
        player.Exchange_Price_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Price_8).GetValue();
        player.Exchange_Currency_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_1).GetValue();
        player.Exchange_Currency_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_2).GetValue();
        player.Exchange_Currency_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_3).GetValue();
        player.Exchange_Currency_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_4).GetValue();
        player.Exchange_Currency_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_5).GetValue();
        player.Exchange_Currency_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_6).GetValue();
        player.Exchange_Currency_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_7).GetValue();
        player.Exchange_Currency_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Currency_8).GetValue();
        player.Exchange_Sold_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_1).GetValue();
        player.Exchange_Sold_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_2).GetValue();
        player.Exchange_Sold_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_3).GetValue();
        player.Exchange_Sold_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_4).GetValue();
        player.Exchange_Sold_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_5).GetValue();
        player.Exchange_Sold_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_6).GetValue();
        player.Exchange_Sold_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_7).GetValue();
        player.Exchange_Sold_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Exchange_Sold_8).GetValue();

        player.Pack_Sell_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_1).GetValue();
        player.Pack_Sell_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_2).GetValue();
        player.Pack_Sell_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_3).GetValue();
        player.Pack_Sell_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_4).GetValue();
        player.Pack_Sell_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_5).GetValue();
        player.Pack_Sell_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_6).GetValue();
        player.Pack_Sell_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_7).GetValue();
        player.Pack_Sell_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Sell_8).GetValue();
        player.Pack_QTY_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_1).GetValue();
        player.Pack_QTY_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_2).GetValue();
        player.Pack_QTY_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_3).GetValue();
        player.Pack_QTY_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_4).GetValue();
        player.Pack_QTY_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_5).GetValue();
        player.Pack_QTY_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_6).GetValue();
        player.Pack_QTY_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_7).GetValue();
        player.Pack_QTY_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_QTY_8).GetValue();
        player.Pack_Price_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_1).GetValue();
        player.Pack_Price_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_2).GetValue();
        player.Pack_Price_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_3).GetValue();
        player.Pack_Price_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_4).GetValue();
        player.Pack_Price_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_5).GetValue();
        player.Pack_Price_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_6).GetValue();
        player.Pack_Price_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_7).GetValue();
        player.Pack_Price_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Price_8).GetValue();
        player.Pack_Currency_1 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_1).GetValue();
        player.Pack_Currency_2 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_2).GetValue();
        player.Pack_Currency_3 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_3).GetValue();
        player.Pack_Currency_4 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_4).GetValue();
        player.Pack_Currency_5 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_5).GetValue();
        player.Pack_Currency_6 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_6).GetValue();
        player.Pack_Currency_7 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_7).GetValue();
        player.Pack_Currency_8 = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Pack_Currency_8).GetValue();
    }

    void UpdateProperty(short key, IObservableProperty property)
    {
        switch (key)
        {
            case ((short)MstProFilePropertyCode.DisplayName): player.DisplayName = property.CastTo<ObservableString>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Player_EXP): player.Player_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Player_Level): player.Player_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Diamond): player.Diamond = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Gold): player.Gold = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_01): player.Token_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_02): player.Token_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_03): player.Token_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_04): player.Token_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_05): player.Token_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_06): player.Token_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_07): player.Token_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_08): player.Token_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_09): player.Token_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Token_10): player.Token_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_101_Level): player.Tower_101_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_101_EXP): player.Tower_101_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_102_Level): player.Tower_102_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_102_EXP): player.Tower_102_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_103_Level): player.Tower_103_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_103_EXP): player.Tower_103_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_104_Level): player.Tower_104_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_104_EXP): player.Tower_104_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_105_Level): player.Tower_105_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_105_EXP): player.Tower_105_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_106_Level): player.Tower_106_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_106_EXP): player.Tower_106_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_107_Level): player.Tower_107_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_107_EXP): player.Tower_107_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_108_Level): player.Tower_108_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_108_EXP): player.Tower_108_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_109_Level): player.Tower_109_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_109_EXP): player.Tower_109_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_110_Level): player.Tower_110_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_110_EXP): player.Tower_110_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_111_Level): player.Tower_111_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_111_EXP): player.Tower_111_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_112_Level): player.Tower_112_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_112_EXP): player.Tower_112_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_113_Level): player.Tower_113_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_113_EXP): player.Tower_113_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_114_Level): player.Tower_114_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_114_EXP): player.Tower_114_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_115_Level): player.Tower_115_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_115_EXP): player.Tower_115_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_116_Level): player.Tower_116_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_116_EXP): player.Tower_116_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_117_Level): player.Tower_117_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_117_EXP): player.Tower_117_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_118_Level): player.Tower_118_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_118_EXP): player.Tower_118_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_119_Level): player.Tower_119_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_119_EXP): player.Tower_119_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_120_Level): player.Tower_120_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_120_EXP): player.Tower_120_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_201_Level): player.Tower_201_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_201_EXP): player.Tower_201_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_202_Level): player.Tower_202_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_202_EXP): player.Tower_202_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_203_Level): player.Tower_203_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_203_EXP): player.Tower_203_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_204_Level): player.Tower_204_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_204_EXP): player.Tower_204_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_205_Level): player.Tower_205_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_205_EXP): player.Tower_205_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_206_Level): player.Tower_206_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_206_EXP): player.Tower_206_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_207_Level): player.Tower_207_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_207_EXP): player.Tower_207_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_208_Level): player.Tower_208_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_208_EXP): player.Tower_208_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_209_Level): player.Tower_209_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_209_EXP): player.Tower_209_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_210_Level): player.Tower_210_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_210_EXP): player.Tower_210_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_211_Level): player.Tower_211_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_211_EXP): player.Tower_211_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_212_Level): player.Tower_212_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_212_EXP): player.Tower_212_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_213_Level): player.Tower_213_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_213_EXP): player.Tower_213_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_214_Level): player.Tower_214_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_214_EXP): player.Tower_214_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_215_Level): player.Tower_215_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_215_EXP): player.Tower_215_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_216_Level): player.Tower_216_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_216_EXP): player.Tower_216_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_217_Level): player.Tower_217_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_217_EXP): player.Tower_217_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_218_Level): player.Tower_218_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_218_EXP): player.Tower_218_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_219_Level): player.Tower_219_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_219_EXP): player.Tower_219_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_220_Level): player.Tower_220_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_220_EXP): player.Tower_220_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_301_Level): player.Tower_301_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_301_EXP): player.Tower_301_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_302_Level): player.Tower_302_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_302_EXP): player.Tower_302_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_303_Level): player.Tower_303_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_303_EXP): player.Tower_303_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_304_Level): player.Tower_304_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_304_EXP): player.Tower_304_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_305_Level): player.Tower_305_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_305_EXP): player.Tower_305_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_306_Level): player.Tower_306_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_306_EXP): player.Tower_306_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_307_Level): player.Tower_307_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_307_EXP): player.Tower_307_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_308_Level): player.Tower_308_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_308_EXP): player.Tower_308_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_309_Level): player.Tower_309_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_309_EXP): player.Tower_309_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_310_Level): player.Tower_310_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_310_EXP): player.Tower_310_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_311_Level): player.Tower_311_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_311_EXP): player.Tower_311_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_312_Level): player.Tower_312_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_312_EXP): player.Tower_312_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_313_Level): player.Tower_313_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_313_EXP): player.Tower_313_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_314_Level): player.Tower_314_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_314_EXP): player.Tower_314_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_315_Level): player.Tower_315_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_315_EXP): player.Tower_315_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_316_Level): player.Tower_316_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_316_EXP): player.Tower_316_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_317_Level): player.Tower_317_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_317_EXP): player.Tower_317_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_318_Level): player.Tower_318_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_318_EXP): player.Tower_318_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_319_Level): player.Tower_319_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_319_EXP): player.Tower_319_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_320_Level): player.Tower_320_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_320_EXP): player.Tower_320_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_401_Level): player.Tower_401_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_401_EXP): player.Tower_401_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_402_Level): player.Tower_402_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_402_EXP): player.Tower_402_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_403_Level): player.Tower_403_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_403_EXP): player.Tower_403_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_404_Level): player.Tower_404_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_404_EXP): player.Tower_404_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_405_Level): player.Tower_405_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_405_EXP): player.Tower_405_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_406_Level): player.Tower_406_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_406_EXP): player.Tower_406_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_407_Level): player.Tower_407_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_407_EXP): player.Tower_407_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_408_Level): player.Tower_408_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_408_EXP): player.Tower_408_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_409_Level): player.Tower_409_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_409_EXP): player.Tower_409_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_410_Level): player.Tower_410_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_410_EXP): player.Tower_410_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_411_Level): player.Tower_411_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_411_EXP): player.Tower_411_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_412_Level): player.Tower_412_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_412_EXP): player.Tower_412_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_413_Level): player.Tower_413_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_413_EXP): player.Tower_413_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_414_Level): player.Tower_414_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_414_EXP): player.Tower_414_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_415_Level): player.Tower_415_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_415_EXP): player.Tower_415_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_416_Level): player.Tower_416_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_416_EXP): player.Tower_416_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_417_Level): player.Tower_417_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_417_EXP): player.Tower_417_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_418_Level): player.Tower_418_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_418_EXP): player.Tower_418_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_419_Level): player.Tower_419_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_419_EXP): player.Tower_419_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_420_Level): player.Tower_420_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_420_EXP): player.Tower_420_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_501_Level): player.Tower_501_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_501_EXP): player.Tower_501_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_502_Level): player.Tower_502_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_502_EXP): player.Tower_502_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_503_Level): player.Tower_503_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_503_EXP): player.Tower_503_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_504_Level): player.Tower_504_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_504_EXP): player.Tower_504_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_505_Level): player.Tower_505_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_505_EXP): player.Tower_505_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_506_Level): player.Tower_506_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_506_EXP): player.Tower_506_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_507_Level): player.Tower_507_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_507_EXP): player.Tower_507_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_508_Level): player.Tower_508_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_508_EXP): player.Tower_508_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_509_Level): player.Tower_509_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_509_EXP): player.Tower_509_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_510_Level): player.Tower_510_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_510_EXP): player.Tower_510_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_511_Level): player.Tower_511_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_511_EXP): player.Tower_511_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_512_Level): player.Tower_512_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_512_EXP): player.Tower_512_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_513_Level): player.Tower_513_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_513_EXP): player.Tower_513_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_514_Level): player.Tower_514_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_514_EXP): player.Tower_514_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_515_Level): player.Tower_515_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_515_EXP): player.Tower_515_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_516_Level): player.Tower_516_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_516_EXP): player.Tower_516_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_517_Level): player.Tower_517_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_517_EXP): player.Tower_517_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_518_Level): player.Tower_518_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_518_EXP): player.Tower_518_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_519_Level): player.Tower_519_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_519_EXP): player.Tower_519_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_520_Level): player.Tower_520_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_520_EXP): player.Tower_520_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_601_Level): player.Tower_601_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_601_EXP): player.Tower_601_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_602_Level): player.Tower_602_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_602_EXP): player.Tower_602_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_603_Level): player.Tower_603_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_603_EXP): player.Tower_603_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_604_Level): player.Tower_604_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_604_EXP): player.Tower_604_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_605_Level): player.Tower_605_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_605_EXP): player.Tower_605_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_606_Level): player.Tower_606_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_606_EXP): player.Tower_606_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_607_Level): player.Tower_607_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_607_EXP): player.Tower_607_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_608_Level): player.Tower_608_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_608_EXP): player.Tower_608_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_609_Level): player.Tower_609_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_609_EXP): player.Tower_609_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_610_Level): player.Tower_610_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_610_EXP): player.Tower_610_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_611_Level): player.Tower_611_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_611_EXP): player.Tower_611_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_612_Level): player.Tower_612_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_612_EXP): player.Tower_612_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_613_Level): player.Tower_613_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_613_EXP): player.Tower_613_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_614_Level): player.Tower_614_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_614_EXP): player.Tower_614_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_615_Level): player.Tower_615_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_615_EXP): player.Tower_615_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_616_Level): player.Tower_616_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_616_EXP): player.Tower_616_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_617_Level): player.Tower_617_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_617_EXP): player.Tower_617_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_618_Level): player.Tower_618_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_618_EXP): player.Tower_618_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_619_Level): player.Tower_619_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_619_EXP): player.Tower_619_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_620_Level): player.Tower_620_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_620_EXP): player.Tower_620_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_621_Level): player.Tower_621_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_621_EXP): player.Tower_621_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_622_Level): player.Tower_622_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_622_EXP): player.Tower_622_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_623_Level): player.Tower_623_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_623_EXP): player.Tower_623_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_624_Level): player.Tower_624_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_624_EXP): player.Tower_624_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_625_Level): player.Tower_625_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_625_EXP): player.Tower_625_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_626_Level): player.Tower_626_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_626_EXP): player.Tower_626_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_627_Level): player.Tower_627_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_627_EXP): player.Tower_627_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_628_Level): player.Tower_628_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_628_EXP): player.Tower_628_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_629_Level): player.Tower_629_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_629_EXP): player.Tower_629_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_630_Level): player.Tower_630_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_630_EXP): player.Tower_630_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_701_Level): player.Tower_701_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_701_EXP): player.Tower_701_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_702_Level): player.Tower_702_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_702_EXP): player.Tower_702_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_703_Level): player.Tower_703_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_703_EXP): player.Tower_703_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_704_Level): player.Tower_704_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_704_EXP): player.Tower_704_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_705_Level): player.Tower_705_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_705_EXP): player.Tower_705_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_706_Level): player.Tower_706_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_706_EXP): player.Tower_706_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_707_Level): player.Tower_707_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_707_EXP): player.Tower_707_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_708_Level): player.Tower_708_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_708_EXP): player.Tower_708_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_709_Level): player.Tower_709_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_709_EXP): player.Tower_709_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_710_Level): player.Tower_710_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_710_EXP): player.Tower_710_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_711_Level): player.Tower_711_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_711_EXP): player.Tower_711_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_712_Level): player.Tower_712_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_712_EXP): player.Tower_712_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_713_Level): player.Tower_713_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_713_EXP): player.Tower_713_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_714_Level): player.Tower_714_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_714_EXP): player.Tower_714_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_715_Level): player.Tower_715_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_715_EXP): player.Tower_715_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_716_Level): player.Tower_716_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_716_EXP): player.Tower_716_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_717_Level): player.Tower_717_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_717_EXP): player.Tower_717_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_718_Level): player.Tower_718_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_718_EXP): player.Tower_718_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_719_Level): player.Tower_719_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_719_EXP): player.Tower_719_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_720_Level): player.Tower_720_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_720_EXP): player.Tower_720_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_721_Level): player.Tower_721_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_721_EXP): player.Tower_721_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_722_Level): player.Tower_722_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_722_EXP): player.Tower_722_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_723_Level): player.Tower_723_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_723_EXP): player.Tower_723_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_724_Level): player.Tower_724_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_724_EXP): player.Tower_724_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_725_Level): player.Tower_725_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_725_EXP): player.Tower_725_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_726_Level): player.Tower_726_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_726_EXP): player.Tower_726_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_727_Level): player.Tower_727_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_727_EXP): player.Tower_727_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_728_Level): player.Tower_728_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_728_EXP): player.Tower_728_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_729_Level): player.Tower_729_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_729_EXP): player.Tower_729_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_730_Level): player.Tower_730_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_730_EXP): player.Tower_730_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_731_Level): player.Tower_731_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_731_EXP): player.Tower_731_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_732_Level): player.Tower_732_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_732_EXP): player.Tower_732_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_733_Level): player.Tower_733_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_733_EXP): player.Tower_733_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_734_Level): player.Tower_734_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_734_EXP): player.Tower_734_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_735_Level): player.Tower_735_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_735_EXP): player.Tower_735_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_736_Level): player.Tower_736_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_736_EXP): player.Tower_736_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_737_Level): player.Tower_737_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_737_EXP): player.Tower_737_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_738_Level): player.Tower_738_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_738_EXP): player.Tower_738_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_739_Level): player.Tower_739_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_739_EXP): player.Tower_739_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_740_Level): player.Tower_740_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_740_EXP): player.Tower_740_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_741_Level): player.Tower_741_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_741_EXP): player.Tower_741_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_742_Level): player.Tower_742_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_742_EXP): player.Tower_742_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_743_Level): player.Tower_743_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_743_EXP): player.Tower_743_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_744_Level): player.Tower_744_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_744_EXP): player.Tower_744_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_745_Level): player.Tower_745_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_745_EXP): player.Tower_745_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_746_Level): player.Tower_746_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_746_EXP): player.Tower_746_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_747_Level): player.Tower_747_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_747_EXP): player.Tower_747_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_748_Level): player.Tower_748_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_748_EXP): player.Tower_748_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_749_Level): player.Tower_749_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_749_EXP): player.Tower_749_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_750_Level): player.Tower_750_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Tower_750_EXP): player.Tower_750_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_01_Level): player.Statue_01_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_01_EXP): player.Statue_01_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_02_Level): player.Statue_02_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_02_EXP): player.Statue_02_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_03_Level): player.Statue_03_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_03_EXP): player.Statue_03_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_04_Level): player.Statue_04_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_04_EXP): player.Statue_04_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_05_Level): player.Statue_05_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_05_EXP): player.Statue_05_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_06_Level): player.Statue_06_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_06_EXP): player.Statue_06_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_07_Level): player.Statue_07_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_07_EXP): player.Statue_07_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_08_Level): player.Statue_08_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_08_EXP): player.Statue_08_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_09_Level): player.Statue_09_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_09_EXP): player.Statue_09_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_10_Level): player.Statue_10_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_10_EXP): player.Statue_10_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_11_Level): player.Statue_11_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_11_EXP): player.Statue_11_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_12_Level): player.Statue_12_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_12_EXP): player.Statue_12_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_13_Level): player.Statue_13_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_13_EXP): player.Statue_13_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_14_Level): player.Statue_14_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_14_EXP): player.Statue_14_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_15_Level): player.Statue_15_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_15_EXP): player.Statue_15_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_16_Level): player.Statue_16_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_16_EXP): player.Statue_16_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_17_Level): player.Statue_17_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_17_EXP): player.Statue_17_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_18_Level): player.Statue_18_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_18_EXP): player.Statue_18_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_19_Level): player.Statue_19_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_19_EXP): player.Statue_19_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_20_Level): player.Statue_20_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_20_EXP): player.Statue_20_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_21_Level): player.Statue_21_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_21_EXP): player.Statue_21_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_22_Level): player.Statue_22_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_22_EXP): player.Statue_22_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_23_Level): player.Statue_23_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_23_EXP): player.Statue_23_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_24_Level): player.Statue_24_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_24_EXP): player.Statue_24_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_25_Level): player.Statue_25_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_25_EXP): player.Statue_25_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_26_Level): player.Statue_26_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_26_EXP): player.Statue_26_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_27_Level): player.Statue_27_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_27_EXP): player.Statue_27_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_28_Level): player.Statue_28_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_28_EXP): player.Statue_28_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_29_Level): player.Statue_29_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_29_EXP): player.Statue_29_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_30_Level): player.Statue_30_Level = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Statue_30_EXP): player.Statue_30_EXP = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_1_01): player.Desk_1_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_1_02): player.Desk_1_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_1_03): player.Desk_1_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_1_04): player.Desk_1_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_1_05): player.Desk_1_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_2_01): player.Desk_2_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_2_02): player.Desk_2_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_2_03): player.Desk_2_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_2_04): player.Desk_2_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_2_05): player.Desk_2_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_3_01): player.Desk_3_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_3_02): player.Desk_3_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_3_03): player.Desk_3_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_3_04): player.Desk_3_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_3_05): player.Desk_3_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_4_01): player.Desk_4_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_4_02): player.Desk_4_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_4_03): player.Desk_4_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_4_04): player.Desk_4_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_4_05): player.Desk_4_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_5_01): player.Desk_5_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_5_02): player.Desk_5_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_5_03): player.Desk_5_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_5_04): player.Desk_5_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_5_05): player.Desk_5_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_6_01): player.Desk_6_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_6_02): player.Desk_6_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_6_03): player.Desk_6_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_6_04): player.Desk_6_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_6_05): player.Desk_6_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_7_01): player.Desk_7_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_7_02): player.Desk_7_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_7_03): player.Desk_7_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_7_04): player.Desk_7_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_7_05): player.Desk_7_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_8_01): player.Desk_8_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_8_02): player.Desk_8_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_8_03): player.Desk_8_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_8_04): player.Desk_8_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Desk_8_05): player.Desk_8_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Map): player.Selected_Map = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_01): player.Map_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_02): player.Map_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_03): player.Map_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_04): player.Map_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_05): player.Map_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_06): player.Map_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_07): player.Map_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_08): player.Map_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_09): player.Map_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_10): player.Map_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_11): player.Map_11 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_12): player.Map_12 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_13): player.Map_13 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_14): player.Map_14 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Map_15): player.Map_15 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_101): player.ClaimReward_101 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_102): player.ClaimReward_102 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_103): player.ClaimReward_103 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_104): player.ClaimReward_104 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_105): player.ClaimReward_105 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_106): player.ClaimReward_106 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_107): player.ClaimReward_107 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_108): player.ClaimReward_108 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_109): player.ClaimReward_109 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_110): player.ClaimReward_110 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_111): player.ClaimReward_111 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_112): player.ClaimReward_112 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_113): player.ClaimReward_113 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_114): player.ClaimReward_114 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_115): player.ClaimReward_115 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_116): player.ClaimReward_116 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_117): player.ClaimReward_117 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_118): player.ClaimReward_118 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_119): player.ClaimReward_119 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_120): player.ClaimReward_120 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_121): player.ClaimReward_121 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_122): player.ClaimReward_122 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_123): player.ClaimReward_123 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_124): player.ClaimReward_124 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_125): player.ClaimReward_125 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_126): player.ClaimReward_126 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_127): player.ClaimReward_127 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_128): player.ClaimReward_128 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_129): player.ClaimReward_129 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_130): player.ClaimReward_130 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_131): player.ClaimReward_131 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_132): player.ClaimReward_132 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_133): player.ClaimReward_133 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_134): player.ClaimReward_134 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_135): player.ClaimReward_135 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_136): player.ClaimReward_136 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_137): player.ClaimReward_137 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_138): player.ClaimReward_138 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_139): player.ClaimReward_139 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_140): player.ClaimReward_140 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_141): player.ClaimReward_141 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_142): player.ClaimReward_142 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_143): player.ClaimReward_143 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_144): player.ClaimReward_144 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_145): player.ClaimReward_145 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_146): player.ClaimReward_146 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_147): player.ClaimReward_147 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_148): player.ClaimReward_148 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_149): player.ClaimReward_149 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_150): player.ClaimReward_150 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_201): player.ClaimReward_201 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_202): player.ClaimReward_202 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_203): player.ClaimReward_203 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_204): player.ClaimReward_204 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_205): player.ClaimReward_205 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_206): player.ClaimReward_206 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_207): player.ClaimReward_207 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_208): player.ClaimReward_208 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_209): player.ClaimReward_209 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_210): player.ClaimReward_210 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_211): player.ClaimReward_211 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_212): player.ClaimReward_212 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_213): player.ClaimReward_213 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_214): player.ClaimReward_214 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_215): player.ClaimReward_215 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_216): player.ClaimReward_216 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_217): player.ClaimReward_217 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_218): player.ClaimReward_218 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_219): player.ClaimReward_219 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_220): player.ClaimReward_220 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_221): player.ClaimReward_221 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_222): player.ClaimReward_222 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_223): player.ClaimReward_223 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_224): player.ClaimReward_224 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_225): player.ClaimReward_225 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_226): player.ClaimReward_226 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_227): player.ClaimReward_227 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_228): player.ClaimReward_228 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_229): player.ClaimReward_229 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_230): player.ClaimReward_230 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_231): player.ClaimReward_231 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_232): player.ClaimReward_232 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_233): player.ClaimReward_233 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_234): player.ClaimReward_234 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_235): player.ClaimReward_235 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_236): player.ClaimReward_236 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_237): player.ClaimReward_237 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_238): player.ClaimReward_238 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_239): player.ClaimReward_239 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_240): player.ClaimReward_240 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_241): player.ClaimReward_241 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_242): player.ClaimReward_242 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_243): player.ClaimReward_243 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_244): player.ClaimReward_244 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_245): player.ClaimReward_245 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_246): player.ClaimReward_246 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_247): player.ClaimReward_247 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_248): player.ClaimReward_248 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_249): player.ClaimReward_249 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_250): player.ClaimReward_250 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_301): player.ClaimReward_301 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_302): player.ClaimReward_302 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_303): player.ClaimReward_303 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_304): player.ClaimReward_304 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_305): player.ClaimReward_305 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_306): player.ClaimReward_306 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_307): player.ClaimReward_307 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_308): player.ClaimReward_308 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_309): player.ClaimReward_309 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_310): player.ClaimReward_310 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_311): player.ClaimReward_311 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_312): player.ClaimReward_312 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_313): player.ClaimReward_313 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_314): player.ClaimReward_314 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_315): player.ClaimReward_315 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_316): player.ClaimReward_316 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_317): player.ClaimReward_317 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_318): player.ClaimReward_318 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_319): player.ClaimReward_319 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_320): player.ClaimReward_320 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_321): player.ClaimReward_321 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_322): player.ClaimReward_322 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_323): player.ClaimReward_323 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_324): player.ClaimReward_324 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_325): player.ClaimReward_325 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_326): player.ClaimReward_326 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_327): player.ClaimReward_327 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_328): player.ClaimReward_328 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_329): player.ClaimReward_329 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_330): player.ClaimReward_330 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_401): player.ClaimReward_401 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_402): player.ClaimReward_402 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_403): player.ClaimReward_403 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_404): player.ClaimReward_404 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_405): player.ClaimReward_405 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_406): player.ClaimReward_406 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_407): player.ClaimReward_407 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_408): player.ClaimReward_408 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_409): player.ClaimReward_409 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_410): player.ClaimReward_410 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_411): player.ClaimReward_411 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_412): player.ClaimReward_412 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_413): player.ClaimReward_413 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_414): player.ClaimReward_414 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_415): player.ClaimReward_415 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_416): player.ClaimReward_416 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_417): player.ClaimReward_417 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_418): player.ClaimReward_418 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_419): player.ClaimReward_419 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_420): player.ClaimReward_420 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_421): player.ClaimReward_421 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_422): player.ClaimReward_422 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_423): player.ClaimReward_423 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_424): player.ClaimReward_424 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_425): player.ClaimReward_425 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_426): player.ClaimReward_426 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_427): player.ClaimReward_427 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_428): player.ClaimReward_428 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_429): player.ClaimReward_429 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_430): player.ClaimReward_430 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_501): player.ClaimReward_501 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_502): player.ClaimReward_502 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_503): player.ClaimReward_503 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_504): player.ClaimReward_504 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_505): player.ClaimReward_505 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_506): player.ClaimReward_506 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_507): player.ClaimReward_507 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_508): player.ClaimReward_508 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_509): player.ClaimReward_509 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_510): player.ClaimReward_510 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_511): player.ClaimReward_511 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_512): player.ClaimReward_512 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_513): player.ClaimReward_513 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_514): player.ClaimReward_514 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_515): player.ClaimReward_515 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_516): player.ClaimReward_516 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_517): player.ClaimReward_517 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_518): player.ClaimReward_518 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_519): player.ClaimReward_519 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_520): player.ClaimReward_520 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_521): player.ClaimReward_521 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_522): player.ClaimReward_522 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_523): player.ClaimReward_523 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_524): player.ClaimReward_524 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_525): player.ClaimReward_525 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_526): player.ClaimReward_526 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_527): player.ClaimReward_527 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_528): player.ClaimReward_528 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_529): player.ClaimReward_529 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_530): player.ClaimReward_530 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_601): player.ClaimReward_601 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_602): player.ClaimReward_602 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_603): player.ClaimReward_603 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_604): player.ClaimReward_604 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_605): player.ClaimReward_605 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_606): player.ClaimReward_606 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_607): player.ClaimReward_607 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_608): player.ClaimReward_608 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_609): player.ClaimReward_609 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_610): player.ClaimReward_610 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_611): player.ClaimReward_611 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_612): player.ClaimReward_612 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_613): player.ClaimReward_613 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_614): player.ClaimReward_614 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_615): player.ClaimReward_615 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_616): player.ClaimReward_616 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_617): player.ClaimReward_617 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_618): player.ClaimReward_618 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_619): player.ClaimReward_619 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_620): player.ClaimReward_620 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_621): player.ClaimReward_621 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_622): player.ClaimReward_622 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_623): player.ClaimReward_623 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_624): player.ClaimReward_624 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_625): player.ClaimReward_625 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_626): player.ClaimReward_626 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_627): player.ClaimReward_627 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_628): player.ClaimReward_628 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_629): player.ClaimReward_629 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_630): player.ClaimReward_630 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_701): player.ClaimReward_701 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_702): player.ClaimReward_702 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_703): player.ClaimReward_703 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_704): player.ClaimReward_704 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_705): player.ClaimReward_705 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_706): player.ClaimReward_706 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_707): player.ClaimReward_707 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_708): player.ClaimReward_708 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_709): player.ClaimReward_709 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_710): player.ClaimReward_710 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_711): player.ClaimReward_711 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_712): player.ClaimReward_712 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_713): player.ClaimReward_713 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_714): player.ClaimReward_714 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_715): player.ClaimReward_715 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_716): player.ClaimReward_716 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_717): player.ClaimReward_717 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_718): player.ClaimReward_718 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_719): player.ClaimReward_719 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_720): player.ClaimReward_720 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_721): player.ClaimReward_721 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_722): player.ClaimReward_722 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_723): player.ClaimReward_723 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_724): player.ClaimReward_724 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_725): player.ClaimReward_725 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_726): player.ClaimReward_726 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_727): player.ClaimReward_727 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_728): player.ClaimReward_728 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_729): player.ClaimReward_729 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.ClaimReward_730): player.ClaimReward_730 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Icon): player.Selected_Icon = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_01): player.Icon_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_02): player.Icon_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_03): player.Icon_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_04): player.Icon_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_05): player.Icon_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_06): player.Icon_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_07): player.Icon_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_08): player.Icon_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_09): player.Icon_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_10): player.Icon_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_11): player.Icon_11 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_12): player.Icon_12 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_13): player.Icon_13 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_14): player.Icon_14 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_15): player.Icon_15 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_16): player.Icon_16 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_17): player.Icon_17 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_18): player.Icon_18 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_19): player.Icon_19 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_20): player.Icon_20 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_21): player.Icon_21 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_22): player.Icon_22 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_23): player.Icon_23 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_24): player.Icon_24 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_25): player.Icon_25 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_26): player.Icon_26 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_27): player.Icon_27 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_28): player.Icon_28 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_29): player.Icon_29 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_30): player.Icon_30 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_31): player.Icon_31 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_32): player.Icon_32 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_33): player.Icon_33 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_34): player.Icon_34 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_35): player.Icon_35 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_36): player.Icon_36 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_37): player.Icon_37 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_38): player.Icon_38 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_39): player.Icon_39 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Icon_40): player.Icon_40 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Emoji_01): player.Selected_Emoji_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Emoji_02): player.Selected_Emoji_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Emoji_03): player.Selected_Emoji_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Emoji_04): player.Selected_Emoji_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Selected_Emoji_05): player.Selected_Emoji_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_01): player.Emoji_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_02): player.Emoji_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_03): player.Emoji_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_04): player.Emoji_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_05): player.Emoji_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_06): player.Emoji_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_07): player.Emoji_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_08): player.Emoji_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_09): player.Emoji_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_10): player.Emoji_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_11): player.Emoji_11 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_12): player.Emoji_12 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_13): player.Emoji_13 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_14): player.Emoji_14 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_15): player.Emoji_15 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_16): player.Emoji_16 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_17): player.Emoji_17 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_18): player.Emoji_18 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_19): player.Emoji_19 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_20): player.Emoji_20 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_21): player.Emoji_21 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_22): player.Emoji_22 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_23): player.Emoji_23 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_24): player.Emoji_24 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_25): player.Emoji_25 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_26): player.Emoji_26 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_27): player.Emoji_27 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_28): player.Emoji_28 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_29): player.Emoji_29 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Emoji_30): player.Emoji_30 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Current_Desk): player.Current_Desk = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Timer_01): player.Timer_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Timer_02): player.Timer_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Timer_03): player.Timer_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Timer_04): player.Timer_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Timer_05): player.Timer_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Critical_Damage): player.Critical_Damage = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_1_Type): player.D_Task_1_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_2_Type): player.D_Task_2_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_3_Type): player.D_Task_3_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_1_QTY): player.D_Task_1_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_2_QTY): player.D_Task_2_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_3_QTY): player.D_Task_3_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_Current_QTY_1): player.D_Task_Current_QTY_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_Current_QTY_2): player.D_Task_Current_QTY_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Task_Current_QTY_3): player.D_Task_Current_QTY_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_1_Type): player.W_Task_1_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_2_Type): player.W_Task_2_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_3_Type): player.W_Task_3_Type = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_1_QTY): player.W_Task_1_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_2_QTY): player.W_Task_2_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_3_QTY): player.W_Task_3_QTY = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_Current_QTY_1): player.W_Task_Current_QTY_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_Current_QTY_2): player.W_Task_Current_QTY_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_Task_Current_QTY_3): player.W_Task_Current_QTY_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_1_1_Claim): player.D_Reward_1_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_1_2_Claim): player.D_Reward_1_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_1_Claim): player.D_Reward_Level_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_2_1_Claim): player.D_Reward_2_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_2_2_Claim): player.D_Reward_2_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_2_Claim): player.D_Reward_Level_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_3_1_Claim): player.D_Reward_3_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_3_2_Claim): player.D_Reward_3_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_3_Claim): player.D_Reward_Level_3_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_4_1_Claim): player.D_Reward_4_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_4_2_Claim): player.D_Reward_4_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_4_Claim): player.D_Reward_Level_4_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_5_1_Claim): player.D_Reward_5_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_5_2_Claim): player.D_Reward_5_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_5_Claim): player.D_Reward_Level_5_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_6_1_Claim): player.D_Reward_6_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_6_2_Claim): player.D_Reward_6_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_6_Claim): player.D_Reward_Level_6_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_7_1_Claim): player.D_Reward_7_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_7_2_Claim): player.D_Reward_7_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_7_Claim): player.D_Reward_Level_7_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_8_1_Claim): player.D_Reward_8_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_8_2_Claim): player.D_Reward_8_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_8_Claim): player.D_Reward_Level_8_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_9_1_Claim): player.D_Reward_9_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_9_2_Claim): player.D_Reward_9_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.D_Reward_Level_9_Claim): player.D_Reward_Level_9_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_1_1_Claim): player.W_RewarW_1_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_1_2_Claim): player.W_RewarW_1_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_1_Claim): player.W_RewarW_Level_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_2_1_Claim): player.W_RewarW_2_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_2_2_Claim): player.W_RewarW_2_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_2_Claim): player.W_RewarW_Level_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_3_1_Claim): player.W_RewarW_3_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_3_2_Claim): player.W_RewarW_3_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_3_Claim): player.W_RewarW_Level_3_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_4_1_Claim): player.W_RewarW_4_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_4_2_Claim): player.W_RewarW_4_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_4_Claim): player.W_RewarW_Level_4_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_5_1_Claim): player.W_RewarW_5_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_5_2_Claim): player.W_RewarW_5_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_5_Claim): player.W_RewarW_Level_5_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_6_1_Claim): player.W_RewarW_6_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_6_2_Claim): player.W_RewarW_6_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_6_Claim): player.W_RewarW_Level_6_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_7_1_Claim): player.W_RewarW_7_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_7_2_Claim): player.W_RewarW_7_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_7_Claim): player.W_RewarW_Level_7_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_8_1_Claim): player.W_RewarW_8_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_8_2_Claim): player.W_RewarW_8_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_8_Claim): player.W_RewarW_Level_8_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_9_1_Claim): player.W_RewarW_9_1_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_9_2_Claim): player.W_RewarW_9_2_Claim = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.W_RewarW_Level_9_Claim): player.W_RewarW_Level_9_Claim = property.CastTo<ObservableInt>().GetValue(); break;

            case ((short)MstProFilePropertyCode.Last_Refresh_Sell_Time): player.Last_Refresh_Sell_Time = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Last_Refresh_Exchange_Time): player.Last_Refresh_Exchange_Time = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Refresh_InGame_Sell): player.Refresh_InGame_Sell = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Refresh_Exchange): player.Refresh_Exchange = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_01): player.Special_Sell_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_02): player.Special_Sell_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_03): player.Special_Sell_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_04): player.Special_Sell_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_05): player.Special_Sell_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_06): player.Special_Sell_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_07): player.Special_Sell_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_08): player.Special_Sell_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_09): player.Special_Sell_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_10): player.Special_Sell_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_11): player.Special_Sell_11 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_12): player.Special_Sell_12 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_13): player.Special_Sell_13 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_Sell_14): player.Special_Sell_14 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_01): player.Special_QTY_01 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_02): player.Special_QTY_02 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_03): player.Special_QTY_03 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_04): player.Special_QTY_04 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_05): player.Special_QTY_05 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_06): player.Special_QTY_06 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_07): player.Special_QTY_07 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_08): player.Special_QTY_08 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_09): player.Special_QTY_09 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_10): player.Special_QTY_10 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_11): player.Special_QTY_11 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_12): player.Special_QTY_12 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_13): player.Special_QTY_13 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Special_QTY_14): player.Special_QTY_14 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_1): player.InGame_Sell_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_2): player.InGame_Sell_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_3): player.InGame_Sell_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_4): player.InGame_Sell_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_5): player.InGame_Sell_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_6): player.InGame_Sell_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_7): player.InGame_Sell_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sell_8): player.InGame_Sell_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_1): player.InGame_QTY_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_2): player.InGame_QTY_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_3): player.InGame_QTY_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_4): player.InGame_QTY_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_5): player.InGame_QTY_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_6): player.InGame_QTY_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_7): player.InGame_QTY_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_QTY_8): player.InGame_QTY_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_1): player.InGame_Price_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_2): player.InGame_Price_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_3): player.InGame_Price_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_4): player.InGame_Price_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_5): player.InGame_Price_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_6): player.InGame_Price_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_7): player.InGame_Price_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Price_8): player.InGame_Price_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_1): player.InGame_Currency_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_2): player.InGame_Currency_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_3): player.InGame_Currency_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_4): player.InGame_Currency_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_5): player.InGame_Currency_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_6): player.InGame_Currency_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_7): player.InGame_Currency_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Currency_8): player.InGame_Currency_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_1): player.InGame_Sold_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_2): player.InGame_Sold_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_3): player.InGame_Sold_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_4): player.InGame_Sold_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_5): player.InGame_Sold_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_6): player.InGame_Sold_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_7): player.InGame_Sold_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.InGame_Sold_8): player.InGame_Sold_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_1): player.Exchange_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_2): player.Exchange_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_3): player.Exchange_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_4): player.Exchange_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_5): player.Exchange_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_6): player.Exchange_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_7): player.Exchange_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_8): player.Exchange_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_1): player.Exchange_QTY_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_2): player.Exchange_QTY_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_3): player.Exchange_QTY_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_4): player.Exchange_QTY_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_5): player.Exchange_QTY_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_6): player.Exchange_QTY_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_7): player.Exchange_QTY_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_QTY_8): player.Exchange_QTY_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_1): player.Exchange_Price_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_2): player.Exchange_Price_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_3): player.Exchange_Price_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_4): player.Exchange_Price_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_5): player.Exchange_Price_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_6): player.Exchange_Price_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_7): player.Exchange_Price_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Price_8): player.Exchange_Price_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_1): player.Exchange_Currency_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_2): player.Exchange_Currency_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_3): player.Exchange_Currency_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_4): player.Exchange_Currency_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_5): player.Exchange_Currency_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_6): player.Exchange_Currency_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_7): player.Exchange_Currency_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Currency_8): player.Exchange_Currency_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_1): player.Exchange_Sold_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_2): player.Exchange_Sold_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_3): player.Exchange_Sold_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_4): player.Exchange_Sold_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_5): player.Exchange_Sold_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_6): player.Exchange_Sold_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_7): player.Exchange_Sold_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Exchange_Sold_8): player.Exchange_Sold_8 = property.CastTo<ObservableInt>().GetValue(); break;

            case ((short)MstProFilePropertyCode.Pack_Sell_1): player.Pack_Sell_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_2): player.Pack_Sell_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_3): player.Pack_Sell_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_4): player.Pack_Sell_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_5): player.Pack_Sell_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_6): player.Pack_Sell_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_7): player.Pack_Sell_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Sell_8): player.Pack_Sell_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_1): player.Pack_QTY_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_2): player.Pack_QTY_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_3): player.Pack_QTY_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_4): player.Pack_QTY_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_5): player.Pack_QTY_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_6): player.Pack_QTY_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_7): player.Pack_QTY_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_QTY_8): player.Pack_QTY_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_1): player.Pack_Price_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_2): player.Pack_Price_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_3): player.Pack_Price_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_4): player.Pack_Price_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_5): player.Pack_Price_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_6): player.Pack_Price_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_7): player.Pack_Price_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Price_8): player.Pack_Price_8 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_1): player.Pack_Currency_1 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_2): player.Pack_Currency_2 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_3): player.Pack_Currency_3 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_4): player.Pack_Currency_4 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_5): player.Pack_Currency_5 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_6): player.Pack_Currency_6 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_7): player.Pack_Currency_7 = property.CastTo<ObservableInt>().GetValue(); break;
            case ((short)MstProFilePropertyCode.Pack_Currency_8): player.Pack_Currency_8 = property.CastTo<ObservableInt>().GetValue(); break;
        }

        if (!UI_Main)
            UI_Main = GameObject.Find("Main_UI");
        UI_Main.SetActive(true);
    }

    public int[] Get_Current_Tower_Desk(int Desk_Number)
    {
        int[] Desk = new int[6];
        switch (Desk_Number)
        {
            case (1): Desk = new int[6] { 0, player.Desk_1_01, player.Desk_1_02, player.Desk_1_03, player.Desk_1_04, player.Desk_1_05 }; break;
            case (2): Desk = new int[6] { 0, player.Desk_2_01, player.Desk_2_02, player.Desk_2_03, player.Desk_2_04, player.Desk_2_05 }; break;
            case (3): Desk = new int[6] { 0, player.Desk_3_01, player.Desk_3_02, player.Desk_3_03, player.Desk_3_04, player.Desk_3_05 }; break;
            case (4): Desk = new int[6] { 0, player.Desk_4_01, player.Desk_4_02, player.Desk_4_03, player.Desk_4_04, player.Desk_4_05 }; break;
            case (5): Desk = new int[6] { 0, player.Desk_5_01, player.Desk_5_02, player.Desk_5_03, player.Desk_5_04, player.Desk_5_05 }; break;
            case (6): Desk = new int[6] { 0, player.Desk_6_01, player.Desk_6_02, player.Desk_6_03, player.Desk_6_04, player.Desk_6_05 }; break;
            case (7): Desk = new int[6] { 0, player.Desk_7_01, player.Desk_7_02, player.Desk_7_03, player.Desk_7_04, player.Desk_7_05 }; break;
            case (8): Desk = new int[6] { 0, player.Desk_8_01, player.Desk_8_02, player.Desk_8_03, player.Desk_8_04, player.Desk_8_05 }; break;
        }
        return Desk;
    }

    public int Get_Tower_Level(int tower_Number)
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

    public void Set_Tower_Desk(int Desk_Number, int[] Desk_Array)
    {
        switch (Desk_Number)
        {
            case (1):
                player.Desk_1_01 = Desk_Array[1];
                player.Desk_1_02 = Desk_Array[2];
                player.Desk_1_03 = Desk_Array[3];
                player.Desk_1_04 = Desk_Array[4];
                player.Desk_1_05 = Desk_Array[5];
                break;
            case (2):
                player.Desk_2_01 = Desk_Array[1];
                player.Desk_2_02 = Desk_Array[2];
                player.Desk_2_03 = Desk_Array[3];
                player.Desk_2_04 = Desk_Array[4];
                player.Desk_2_05 = Desk_Array[5];
                break;
            case (3):
                player.Desk_3_01 = Desk_Array[1];
                player.Desk_3_02 = Desk_Array[2];
                player.Desk_3_03 = Desk_Array[3];
                player.Desk_3_04 = Desk_Array[4];
                player.Desk_3_05 = Desk_Array[5];
                break;
            case (4):
                player.Desk_4_01 = Desk_Array[1];
                player.Desk_4_02 = Desk_Array[2];
                player.Desk_4_03 = Desk_Array[3];
                player.Desk_4_04 = Desk_Array[4];
                player.Desk_4_05 = Desk_Array[5];
                break;
            case (5):
                player.Desk_5_01 = Desk_Array[1];
                player.Desk_5_02 = Desk_Array[2];
                player.Desk_5_03 = Desk_Array[3];
                player.Desk_5_04 = Desk_Array[4];
                player.Desk_5_05 = Desk_Array[5];
                break;
            case (6):
                player.Desk_6_01 = Desk_Array[1];
                player.Desk_6_02 = Desk_Array[2];
                player.Desk_6_03 = Desk_Array[3];
                player.Desk_6_04 = Desk_Array[4];
                player.Desk_6_05 = Desk_Array[5];
                break;
            case (7):
                player.Desk_7_01 = Desk_Array[1];
                player.Desk_7_02 = Desk_Array[2];
                player.Desk_7_03 = Desk_Array[3];
                player.Desk_7_04 = Desk_Array[4];
                player.Desk_7_05 = Desk_Array[5];
                break;
            case (8):
                player.Desk_8_01 = Desk_Array[1];
                player.Desk_8_02 = Desk_Array[2];
                player.Desk_8_03 = Desk_Array[3];
                player.Desk_8_04 = Desk_Array[4];
                player.Desk_8_05 = Desk_Array[5];
                break;
        }
    }

    public void Active_UI_Main()
    {
        Debug.Log("Active_UI_Main_1 || " + UI_Main);
        if (!UI_Main)
            UI_Main = GameObject.Find("Main_UI");
        Debug.Log("Active_UI_Main_2 || " + UI_Main);
        UI_Main.SetActive(true);
        UI_Main.GetComponent<UI_Main>().Enable_Canvas();
    }

    public void ClientGetProfile()
    {
        Debug.Log("ClientGetProfile || Player_Status_Load_Finish || " + Player_Status_Load_Finish);

        DemoProfilesBehaviour profileManager = GameObject.FindObjectOfType<DemoProfilesBehaviour>();
        if (!profileManager)
            return;

        ObservableProfile Profile = profileManager.Profile;

        int gold = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).GetValue();
        if (Text_Obj != null)
            Text_Obj.GetComponent<Text>().text = gold.ToString();

        Set_Local_Profile(Profile);

        Active_UI_Main();
        UI_Main.GetComponent<UI_Main>().Set_UI();

        Debug.Log("Player_Last_Refresh_Time || " + player.Last_Refresh_Exchange_Time);

        Player_Status_Load_Finish = true;
        if (Player_Status_Load_Finish)
        {
            Debug.Log("Player_Status_Load_Finish || " + Player_Status_Load_Finish);
            UI_Main.GetComponent<UI_Main>().Player_Status_Setup_Finish = true;
            UI_Main.GetComponent<UI_Main>().UI_Battle_Canvas.GetComponent<UI_Battle_Canvas>().Setup_Start();
            UI_Main.GetComponent<UI_Main>().UI_Tower_Select_Canvas.GetComponent<UI_Tower_Select_Canvas>().Setup_Start();
            UI_Main.GetComponent<UI_Main>().UI_Task_Canvas.GetComponent<UI_Task>().Setup_Start();
            UI_Main.GetComponent<UI_Main>().UI_Shop_Canvas.GetComponent<UI_Shop_Canvas>().Setup_Start();
        }
    }

    public void Add_Gold()
    {
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Client_Get_Profile, (status, response) =>
        {
            if (status == ResponseStatus.Success)
            {
                DemoProfilesBehaviour profileManager = GameObject.FindObjectOfType<DemoProfilesBehaviour>();
                ObservableProfile Profile = profileManager.Profile;
                Profile.FromBytes(response.AsBytes());
                int gold = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).GetValue();
                Debug.Log("Gold_Added || " + gold);
            }
        });
    }

    public void Update_Desk(short Current_Desk)
    {
        if (Current_Desk == player.Current_Desk)
            return;

        RoomOptions Room_Option = new RoomOptions();
        if (Current_Desk != player.Current_Desk)
            Room_Option.CustomOptions.Add("Current_Desk", Current_Desk);

        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Update_Desk, Room_Option, OnDeskUpdateResponseCallback);
    }

    public void Update_Desk(short Current_Desk, List<List<short>> Modified_Desk)
    {
        Debug.Log("Modified_Desk || " + Modified_Desk.Count);
        RoomOptions Room_Option = new RoomOptions();

        if (Modified_Desk.Count == 0)
            return;

        if (Current_Desk != player.Current_Desk)
            Room_Option.CustomOptions.Add("Current_Desk", Current_Desk);

        for (int i = 0; i < Modified_Desk.Count; i++)
        {
            List<short> info = Modified_Desk[i];
            short current_desk = info[0];
            short current_slot = info[1];
            short modified_Tower = info[2];
            string Key = current_desk.ToString() + current_slot.ToString();
            Room_Option.CustomOptions.Add(Key, modified_Tower.ToString());
        }

        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Update_Desk, Room_Option, OnDeskUpdateResponseCallback);
    }

    void OnDeskUpdateResponseCallback(ResponseStatus status, IIncomingMessage response)
    {
        if (status == ResponseStatus.Success)
        {

            MasterServerToolkit.Examples.BasicProfile.DemoProfilesBehaviour profileManager;
            profileManager = GameObject.FindObjectOfType<MasterServerToolkit.Examples.BasicProfile.DemoProfilesBehaviour>();
            ObservableProfile Profile = profileManager.Profile;

            int D1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_01).GetValue();
            int D2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_02).GetValue();
            int D3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_03).GetValue();
            int D4 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_04).GetValue();
            int D5 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Desk_1_05).GetValue();

            Debug.Log("D__ " + D1 + " || " + D2 + " || " + D3 + " || " + D4 + " || " + D5);

            Debug.Log("ResponseStatus.Success ? || " + status);
        }
        else
        {

        }
    }

    public void Click_Shop_Button(short Button_Code)
    {
        RoomOptions Room_Option = new RoomOptions();
        Room_Option.OP_Code = Button_Code;
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Click_Shop_Button, Room_Option, Shop_Button_Call_Back);
    }

    public void Add_Player_To_Pool(string OP_Code)
    {
        if (OP_Code == "1V1")
            Mst.Client.Connection.SendMessage((short)MstMessageCodes.Queue1v1);
    }

    public void Refresh_Exchange_Shop()
    {
        RoomOptions Room_Option = new RoomOptions();
        Room_Option.OP_Code = (short)Shop_Code.Refresh_InGame_Sell_Button;
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Refresh_Exchange, Room_Option);
    }

    public void Check_Player_In_Game()
    {
        Mst.Client.Connection.SendMessage((short)MstMessageCodes.Check_Player_In_Game, CheckPlayerInGame_Callback);
    }

    void CheckPlayerInGame_Callback(ResponseStatus status, IIncomingMessage response)
    {
        if (status == ResponseStatus.Default)
        {
            //not_in_game
            Debug.Log("CheckPlayerInGame_Callback || Not in game");
        }
        else
        {
            Debug.Log("CheckPlayerInGame_Callback || Rejoin_Game || " + gameObject.name);
            ReJoinGame = true;
        }
    }

    void Shop_Button_Call_Back(ResponseStatus status, IIncomingMessage response)
    {
        if (status == ResponseStatus.Success)
        {

        }
        else
        {

        }
    }
}

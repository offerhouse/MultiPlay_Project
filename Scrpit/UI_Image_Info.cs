using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Image_Info : MonoBehaviour
{
    public Texture T_Icon101, T_Icon102, T_Icon103, T_Icon104, T_Icon105, T_Icon106, T_Icon107, T_Icon108, T_Icon109, T_Icon110;
    public Texture T_Icon111, T_Icon112, T_Icon113, T_Icon114, T_Icon115, T_Icon116, T_Icon117, T_Icon118, T_Icon119, T_Icon120;
    public Texture T_Icon201, T_Icon202, T_Icon203, T_Icon204, T_Icon205, T_Icon206, T_Icon207, T_Icon208, T_Icon209, T_Icon210;
    public Texture T_Icon211, T_Icon212, T_Icon213, T_Icon214, T_Icon215, T_Icon216, T_Icon217, T_Icon218, T_Icon219, T_Icon220;
    public Texture T_Icon301, T_Icon302, T_Icon303, T_Icon304, T_Icon305, T_Icon306, T_Icon307, T_Icon308, T_Icon309, T_Icon310;
    public Texture T_Icon311, T_Icon312, T_Icon313, T_Icon314, T_Icon315, T_Icon316, T_Icon317, T_Icon318, T_Icon319, T_Icon320;
    public Texture T_Icon401, T_Icon402, T_Icon403, T_Icon404, T_Icon405, T_Icon406, T_Icon407, T_Icon408, T_Icon409, T_Icon410;
    public Texture T_Icon411, T_Icon412, T_Icon413, T_Icon414, T_Icon415, T_Icon416, T_Icon417, T_Icon418, T_Icon419, T_Icon420;
    public Texture T_Icon501, T_Icon502, T_Icon503, T_Icon504, T_Icon505, T_Icon506, T_Icon507, T_Icon508, T_Icon509, T_Icon510;
    public Texture T_Icon511, T_Icon512, T_Icon513, T_Icon514, T_Icon515, T_Icon516, T_Icon517, T_Icon518, T_Icon519, T_Icon520;
    public Texture T_Icon601, T_Icon602, T_Icon603, T_Icon604, T_Icon605, T_Icon606, T_Icon607, T_Icon608, T_Icon609, T_Icon610;
    public Texture T_Icon611, T_Icon612, T_Icon613, T_Icon614, T_Icon615, T_Icon616, T_Icon617, T_Icon618, T_Icon619, T_Icon620;
    public Texture T_Icon621, T_Icon622, T_Icon623, T_Icon624, T_Icon625, T_Icon626, T_Icon627, T_Icon628, T_Icon629, T_Icon630;
    public Texture T_Icon701, T_Icon702, T_Icon703, T_Icon704, T_Icon705, T_Icon706, T_Icon707, T_Icon708, T_Icon709, T_Icon710;
    public Texture T_Icon711, T_Icon712, T_Icon713, T_Icon714, T_Icon715, T_Icon716, T_Icon717, T_Icon718, T_Icon719, T_Icon720;
    public Texture T_Icon721, T_Icon722, T_Icon723, T_Icon724, T_Icon725, T_Icon726, T_Icon727, T_Icon728, T_Icon729, T_Icon730;
    public Texture T_Icon731, T_Icon732, T_Icon733, T_Icon734, T_Icon735, T_Icon736, T_Icon737, T_Icon738, T_Icon739, T_Icon740;
    public Texture T_Icon741, T_Icon742, T_Icon743, T_Icon744, T_Icon745, T_Icon746, T_Icon747, T_Icon748, T_Icon749, T_Icon750;

    public Texture T_Dim101, T_Dim102, T_Dim103, T_Dim104, T_Dim105, T_Dim106, T_Dim107, T_Dim108, T_Dim109, T_Dim110;
    public Texture T_Dim111, T_Dim112, T_Dim113, T_Dim114, T_Dim115, T_Dim116, T_Dim117, T_Dim118, T_Dim119, T_Dim120;
    public Texture T_Dim201, T_Dim202, T_Dim203, T_Dim204, T_Dim205, T_Dim206, T_Dim207, T_Dim208, T_Dim209, T_Dim210;
    public Texture T_Dim211, T_Dim212, T_Dim213, T_Dim214, T_Dim215, T_Dim216, T_Dim217, T_Dim218, T_Dim219, T_Dim220;
    public Texture T_Dim301, T_Dim302, T_Dim303, T_Dim304, T_Dim305, T_Dim306, T_Dim307, T_Dim308, T_Dim309, T_Dim310;
    public Texture T_Dim311, T_Dim312, T_Dim313, T_Dim314, T_Dim315, T_Dim316, T_Dim317, T_Dim318, T_Dim319, T_Dim320;
    public Texture T_Dim401, T_Dim402, T_Dim403, T_Dim404, T_Dim405, T_Dim406, T_Dim407, T_Dim408, T_Dim409, T_Dim410;
    public Texture T_Dim411, T_Dim412, T_Dim413, T_Dim414, T_Dim415, T_Dim416, T_Dim417, T_Dim418, T_Dim419, T_Dim420;
    public Texture T_Dim501, T_Dim502, T_Dim503, T_Dim504, T_Dim505, T_Dim506, T_Dim507, T_Dim508, T_Dim509, T_Dim510;
    public Texture T_Dim511, T_Dim512, T_Dim513, T_Dim514, T_Dim515, T_Dim516, T_Dim517, T_Dim518, T_Dim519, T_Dim520;
    public Texture T_Dim601, T_Dim602, T_Dim603, T_Dim604, T_Dim605, T_Dim606, T_Dim607, T_Dim608, T_Dim609, T_Dim610;
    public Texture T_Dim611, T_Dim612, T_Dim613, T_Dim614, T_Dim615, T_Dim616, T_Dim617, T_Dim618, T_Dim619, T_Dim620;
    public Texture T_Dim621, T_Dim622, T_Dim623, T_Dim624, T_Dim625, T_Dim626, T_Dim627, T_Dim628, T_Dim629, T_Dim630;
    public Texture T_Dim701, T_Dim702, T_Dim703, T_Dim704, T_Dim705, T_Dim706, T_Dim707, T_Dim708, T_Dim709, T_Dim710;
    public Texture T_Dim711, T_Dim712, T_Dim713, T_Dim714, T_Dim715, T_Dim716, T_Dim717, T_Dim718, T_Dim719, T_Dim720;
    public Texture T_Dim721, T_Dim722, T_Dim723, T_Dim724, T_Dim725, T_Dim726, T_Dim727, T_Dim728, T_Dim729, T_Dim730;
    public Texture T_Dim731, T_Dim732, T_Dim733, T_Dim734, T_Dim735, T_Dim736, T_Dim737, T_Dim738, T_Dim739, T_Dim740;
    public Texture T_Dim741, T_Dim742, T_Dim743, T_Dim744, T_Dim745, T_Dim746, T_Dim747, T_Dim748, T_Dim749, T_Dim750;

    public Texture Base_Rock_01, Base_Rock_02, Base_Rock_03, Base_Rock_04, Base_Rock_05, Base_Rock_06, Base_Rock_07;
    public Texture Base_Dim_01, Base_Dim_02, Base_Dim_03, Base_Dim_04, Base_Dim_05, Base_Dim_06, Base_Dim_07;


    public Texture InGame_Sell_1_Image, InGame_Sell_2_Image, InGame_Sell_3_Image, InGame_Sell_4_Image, InGame_Sell_5_Image,
    InGame_Sell_6_Image, InGame_Sell_7_Image, InGame_Sell_8_Image, Exchange1_Image, Exchange2_Image, Exchange3_Image, Exchange4_Image,
        Exchange5_Image, Exchange6_Image, Sell_Gold_1_Image, Sell_Gold_2_Image, Sell_Gold_3_Image, Sell_Diamond_1_Image,
        Sell_Diamond_2_Image, Sell_Diamond_3_Image, Sell_Token1_1_Image, Sell_Token1_2_Image, Sell_Token1_3_Image, Sell_Token2_1_Image,
        Sell_Token2_2_Image, Sell_Token2_3_Image, Sell_Chest_1_Image, Sell_Chest_2_Image, Sell_Chest_3_Image;

    public Texture InGame_Sell_1_Dim, InGame_Sell_2_Dim, InGame_Sell_3_Dim, InGame_Sell_4_Dim, InGame_Sell_5_Dim, InGame_Sell_6_Dim,
        InGame_Sell_7_Dim, InGame_Sell_8_Dim, Exchange1_Dim, Exchange2_Dim, Exchange3_Dim, Exchange4_Dim, Exchange5_Dim, Exchange6_Dim,
        Sell_Gold_1_Dim, Sell_Gold_2_Dim, Sell_Gold_3_Dim, Sell_Diamond_1_Dim, Sell_Diamond_2_Dim, Sell_Diamond_3_Dim,
        Sell_Token1_1_Dim, Sell_Token1_2_Dim, Sell_Token1_3_Dim, Sell_Token2_1_Dim, Sell_Token2_2_Dim, Sell_Token2_3_Dim,
        Sell_Chest_1_Dim, Sell_Chest_2_Dim, Sell_Chest_3_Dim;

    public Texture Gold, Gold_Dim, Gold_Small, Gold_Small_Dim, Diamond, Diamond_Dim, Diamond_Small, Diamond_Small_Dim,
        Token1, Token1_Dim, Token1_Small, Token1_Small_Dim, Token2, Token2_Dim, Token2_Small, Token2_Small_Dim,
        Token3, Token3_Dim, Token3_Small, Token3_Small_Dim, Token4, Token4_Dim, Token4_Small, Token4_Small_Dim,
        Token5, Token5_Dim, Token5_Small, Token5_Small_Dim, Token6, Token6_Dim, Token6_Small, Token6_Small_Dim,
        Token7, Token7_Dim, Token7_Small, Token7_Small_Dim, Token8, Token8_Dim, Token8_Small, Token8_Small_Dim,
        Token9, Token9_Dim, Token9_Small, Token9_Small_Dim, Token10, Token10_Dim, Token10_Small, Token10_Small_Dim,
        Chest_1, Chest_1_Dim, Chest_1_Small, Chest_1_Small_Dim, Chest_2, Chest_2_Dim, Chest_2_Small, Chest_2_Small_Dim,
        Chest_3, Chest_3_Dim, Chest_3_Small, Chest_3_Small_Dim, Chest_4, Chest_4_Dim, Chest_4_Small, Chest_4_Small_Dim,
        Chest_5, Chest_5_Dim, Chest_5_Small, Chest_5_Small_Dim, Chest_6, Chest_6_Dim, Chest_6_Small, Chest_6_Small_Dim,
        Chest_7, Chest_7_Dim, Chest_7_Small, Chest_7_Small_Dim, Chest_8, Chest_8_Dim, Chest_8_Small, Chest_8_Small_Dim,
        Chest_9, Chest_9_Dim, Chest_9_Small, Chest_9_Small_Dim, Chest_10, Chest_10_Dim, Chest_10_Small, Chest_10_Small_Dim;

    public Texture InGame_Border_Lv1, InGame_Border_Lv2, InGame_Border_Lv3, InGame_Border_Lv4, InGame_Border_Lv5, InGame_Border_Lv6,
        InGame_Border_Lv7, InGame_Border_Lv8, InGame_Border_Dim;

    public Texture Ex_Border_Lv1, Ex_Border_Lv2, Ex_Border_Lv3, Ex_Border_Lv4, Ex_Border_Lv5, Ex_Border_Lv6, Ex_Border_Lv7,
        Ex_Border_Lv8, Ex_Border_Dim;

    public Texture Treasure_Box_00, Treasure_Box_01, Treasure_Box_02, Treasure_Box_03, Treasure_Box_04, Treasure_Box_05,
        Treasure_Box_06, Treasure_Box_07, Treasure_Box_08, Treasure_Box_09;

    public Texture Get_Texture_By_Code(short Code_Number)
    {
        Texture texture = null;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold): texture = Gold; break;
            case ((short)Shop_Code.Diamond): texture = Diamond; break;
            case ((short)Shop_Code.Token_01): texture = Token1; break;
            case ((short)Shop_Code.Token_02): texture = Token2; break;
            case ((short)Shop_Code.Token_03): texture = Token3; break;
            case ((short)Shop_Code.Token_04): texture = Token4; break;
            case ((short)Shop_Code.Token_05): texture = Token5; break;
            case ((short)Shop_Code.Token_06): texture = Token6; break;
            case ((short)Shop_Code.Token_07): texture = Token7; break;
            case ((short)Shop_Code.Token_08): texture = Token8; break;
            case ((short)Shop_Code.Token_09): texture = Token9; break;
            case ((short)Shop_Code.Token_10): texture = Token10; break;
            case ((short)Shop_Code.Chest_1): texture = Chest_1; break;
            case ((short)Shop_Code.Chest_2): texture = Chest_2; break;
            case ((short)Shop_Code.Chest_3): texture = Chest_3; break;
            case ((short)Shop_Code.Chest_4): texture = Chest_4; break;
            case ((short)Shop_Code.Chest_5): texture = Chest_5; break;
            case ((short)Shop_Code.Chest_6): texture = Chest_6; break;
            case ((short)Shop_Code.Chest_7): texture = Chest_7; break;
            case ((short)Shop_Code.Chest_8): texture = Chest_8; break;
            case ((short)Shop_Code.Chest_9): texture = Chest_9; break;
            case ((short)Shop_Code.Chest_10): texture = Chest_10; break;

            case ((short)Shop_Code.Tower_101): texture = T_Icon101; break;
            case ((short)Shop_Code.Tower_102): texture = T_Icon102; break;
            case ((short)Shop_Code.Tower_103): texture = T_Icon103; break;
            case ((short)Shop_Code.Tower_104): texture = T_Icon104; break;
            case ((short)Shop_Code.Tower_105): texture = T_Icon105; break;
            case ((short)Shop_Code.Tower_106): texture = T_Icon106; break;
            case ((short)Shop_Code.Tower_107): texture = T_Icon107; break;
            case ((short)Shop_Code.Tower_108): texture = T_Icon108; break;
            case ((short)Shop_Code.Tower_109): texture = T_Icon109; break;
            case ((short)Shop_Code.Tower_110): texture = T_Icon110; break;
            case ((short)Shop_Code.Tower_111): texture = T_Icon111; break;
            case ((short)Shop_Code.Tower_112): texture = T_Icon112; break;
            case ((short)Shop_Code.Tower_113): texture = T_Icon113; break;
            case ((short)Shop_Code.Tower_114): texture = T_Icon114; break;
            case ((short)Shop_Code.Tower_115): texture = T_Icon115; break;
            case ((short)Shop_Code.Tower_116): texture = T_Icon116; break;
            case ((short)Shop_Code.Tower_117): texture = T_Icon117; break;
            case ((short)Shop_Code.Tower_118): texture = T_Icon118; break;
            case ((short)Shop_Code.Tower_119): texture = T_Icon119; break;
            case ((short)Shop_Code.Tower_120): texture = T_Icon120; break;

            case ((short)Shop_Code.Tower_201): texture = T_Icon201; break;
            case ((short)Shop_Code.Tower_202): texture = T_Icon202; break;
            case ((short)Shop_Code.Tower_203): texture = T_Icon203; break;
            case ((short)Shop_Code.Tower_204): texture = T_Icon204; break;
            case ((short)Shop_Code.Tower_205): texture = T_Icon205; break;
            case ((short)Shop_Code.Tower_206): texture = T_Icon206; break;
            case ((short)Shop_Code.Tower_207): texture = T_Icon207; break;
            case ((short)Shop_Code.Tower_208): texture = T_Icon208; break;
            case ((short)Shop_Code.Tower_209): texture = T_Icon209; break;
            case ((short)Shop_Code.Tower_210): texture = T_Icon210; break;
            case ((short)Shop_Code.Tower_211): texture = T_Icon211; break;
            case ((short)Shop_Code.Tower_212): texture = T_Icon212; break;
            case ((short)Shop_Code.Tower_213): texture = T_Icon213; break;
            case ((short)Shop_Code.Tower_214): texture = T_Icon214; break;
            case ((short)Shop_Code.Tower_215): texture = T_Icon215; break;
            case ((short)Shop_Code.Tower_216): texture = T_Icon216; break;
            case ((short)Shop_Code.Tower_217): texture = T_Icon217; break;
            case ((short)Shop_Code.Tower_218): texture = T_Icon218; break;
            case ((short)Shop_Code.Tower_219): texture = T_Icon219; break;
            case ((short)Shop_Code.Tower_220): texture = T_Icon220; break;

            case ((short)Shop_Code.Tower_301): texture = T_Icon301; break;
            case ((short)Shop_Code.Tower_302): texture = T_Icon302; break;
            case ((short)Shop_Code.Tower_303): texture = T_Icon303; break;
            case ((short)Shop_Code.Tower_304): texture = T_Icon304; break;
            case ((short)Shop_Code.Tower_305): texture = T_Icon305; break;
            case ((short)Shop_Code.Tower_306): texture = T_Icon306; break;
            case ((short)Shop_Code.Tower_307): texture = T_Icon307; break;
            case ((short)Shop_Code.Tower_308): texture = T_Icon308; break;
            case ((short)Shop_Code.Tower_309): texture = T_Icon309; break;
            case ((short)Shop_Code.Tower_310): texture = T_Icon310; break;
            case ((short)Shop_Code.Tower_311): texture = T_Icon311; break;
            case ((short)Shop_Code.Tower_312): texture = T_Icon312; break;
            case ((short)Shop_Code.Tower_313): texture = T_Icon313; break;
            case ((short)Shop_Code.Tower_314): texture = T_Icon314; break;
            case ((short)Shop_Code.Tower_315): texture = T_Icon315; break;
            case ((short)Shop_Code.Tower_316): texture = T_Icon316; break;
            case ((short)Shop_Code.Tower_317): texture = T_Icon317; break;
            case ((short)Shop_Code.Tower_318): texture = T_Icon318; break;
            case ((short)Shop_Code.Tower_319): texture = T_Icon319; break;
            case ((short)Shop_Code.Tower_320): texture = T_Icon320; break;

            case ((short)Shop_Code.Tower_401): texture = T_Icon401; break;
            case ((short)Shop_Code.Tower_402): texture = T_Icon402; break;
            case ((short)Shop_Code.Tower_403): texture = T_Icon403; break;
            case ((short)Shop_Code.Tower_404): texture = T_Icon404; break;
            case ((short)Shop_Code.Tower_405): texture = T_Icon405; break;
            case ((short)Shop_Code.Tower_406): texture = T_Icon406; break;
            case ((short)Shop_Code.Tower_407): texture = T_Icon407; break;
            case ((short)Shop_Code.Tower_408): texture = T_Icon408; break;
            case ((short)Shop_Code.Tower_409): texture = T_Icon409; break;
            case ((short)Shop_Code.Tower_410): texture = T_Icon410; break;
            case ((short)Shop_Code.Tower_411): texture = T_Icon411; break;
            case ((short)Shop_Code.Tower_412): texture = T_Icon412; break;
            case ((short)Shop_Code.Tower_413): texture = T_Icon413; break;
            case ((short)Shop_Code.Tower_414): texture = T_Icon414; break;
            case ((short)Shop_Code.Tower_415): texture = T_Icon415; break;
            case ((short)Shop_Code.Tower_416): texture = T_Icon416; break;
            case ((short)Shop_Code.Tower_417): texture = T_Icon417; break;
            case ((short)Shop_Code.Tower_418): texture = T_Icon418; break;
            case ((short)Shop_Code.Tower_419): texture = T_Icon419; break;
            case ((short)Shop_Code.Tower_420): texture = T_Icon420; break;

            case ((short)Shop_Code.Tower_501): texture = T_Icon501; break;
            case ((short)Shop_Code.Tower_502): texture = T_Icon502; break;
            case ((short)Shop_Code.Tower_503): texture = T_Icon503; break;
            case ((short)Shop_Code.Tower_504): texture = T_Icon504; break;
            case ((short)Shop_Code.Tower_505): texture = T_Icon505; break;
            case ((short)Shop_Code.Tower_506): texture = T_Icon506; break;
            case ((short)Shop_Code.Tower_507): texture = T_Icon507; break;
            case ((short)Shop_Code.Tower_508): texture = T_Icon508; break;
            case ((short)Shop_Code.Tower_509): texture = T_Icon509; break;
            case ((short)Shop_Code.Tower_510): texture = T_Icon510; break;
            case ((short)Shop_Code.Tower_511): texture = T_Icon511; break;
            case ((short)Shop_Code.Tower_512): texture = T_Icon512; break;
            case ((short)Shop_Code.Tower_513): texture = T_Icon513; break;
            case ((short)Shop_Code.Tower_514): texture = T_Icon514; break;
            case ((short)Shop_Code.Tower_515): texture = T_Icon515; break;
            case ((short)Shop_Code.Tower_516): texture = T_Icon516; break;
            case ((short)Shop_Code.Tower_517): texture = T_Icon517; break;
            case ((short)Shop_Code.Tower_518): texture = T_Icon518; break;
            case ((short)Shop_Code.Tower_519): texture = T_Icon519; break;
            case ((short)Shop_Code.Tower_520): texture = T_Icon520; break;

            case ((short)Shop_Code.Tower_601): texture = T_Icon601; break;
            case ((short)Shop_Code.Tower_602): texture = T_Icon602; break;
            case ((short)Shop_Code.Tower_603): texture = T_Icon603; break;
            case ((short)Shop_Code.Tower_604): texture = T_Icon604; break;
            case ((short)Shop_Code.Tower_605): texture = T_Icon605; break;
            case ((short)Shop_Code.Tower_606): texture = T_Icon606; break;
            case ((short)Shop_Code.Tower_607): texture = T_Icon607; break;
            case ((short)Shop_Code.Tower_608): texture = T_Icon608; break;
            case ((short)Shop_Code.Tower_609): texture = T_Icon609; break;
            case ((short)Shop_Code.Tower_610): texture = T_Icon610; break;
            case ((short)Shop_Code.Tower_611): texture = T_Icon611; break;
            case ((short)Shop_Code.Tower_612): texture = T_Icon612; break;
            case ((short)Shop_Code.Tower_613): texture = T_Icon613; break;
            case ((short)Shop_Code.Tower_614): texture = T_Icon614; break;
            case ((short)Shop_Code.Tower_615): texture = T_Icon615; break;
            case ((short)Shop_Code.Tower_616): texture = T_Icon616; break;
            case ((short)Shop_Code.Tower_617): texture = T_Icon617; break;
            case ((short)Shop_Code.Tower_618): texture = T_Icon618; break;
            case ((short)Shop_Code.Tower_619): texture = T_Icon619; break;
            case ((short)Shop_Code.Tower_620): texture = T_Icon620; break;
            case ((short)Shop_Code.Tower_621): texture = T_Icon621; break;
            case ((short)Shop_Code.Tower_622): texture = T_Icon622; break;
            case ((short)Shop_Code.Tower_623): texture = T_Icon623; break;
            case ((short)Shop_Code.Tower_624): texture = T_Icon624; break;
            case ((short)Shop_Code.Tower_625): texture = T_Icon625; break;
            case ((short)Shop_Code.Tower_626): texture = T_Icon626; break;
            case ((short)Shop_Code.Tower_627): texture = T_Icon627; break;
            case ((short)Shop_Code.Tower_628): texture = T_Icon628; break;
            case ((short)Shop_Code.Tower_629): texture = T_Icon629; break;
            case ((short)Shop_Code.Tower_630): texture = T_Icon630; break;

            case ((short)Shop_Code.Tower_701): texture = T_Icon701; break;
            case ((short)Shop_Code.Tower_702): texture = T_Icon702; break;
            case ((short)Shop_Code.Tower_703): texture = T_Icon703; break;
            case ((short)Shop_Code.Tower_704): texture = T_Icon704; break;
            case ((short)Shop_Code.Tower_705): texture = T_Icon705; break;
            case ((short)Shop_Code.Tower_706): texture = T_Icon706; break;
            case ((short)Shop_Code.Tower_707): texture = T_Icon707; break;
            case ((short)Shop_Code.Tower_708): texture = T_Icon708; break;
            case ((short)Shop_Code.Tower_709): texture = T_Icon709; break;
            case ((short)Shop_Code.Tower_710): texture = T_Icon710; break;
            case ((short)Shop_Code.Tower_711): texture = T_Icon711; break;
            case ((short)Shop_Code.Tower_712): texture = T_Icon712; break;
            case ((short)Shop_Code.Tower_713): texture = T_Icon713; break;
            case ((short)Shop_Code.Tower_714): texture = T_Icon714; break;
            case ((short)Shop_Code.Tower_715): texture = T_Icon715; break;
            case ((short)Shop_Code.Tower_716): texture = T_Icon716; break;
            case ((short)Shop_Code.Tower_717): texture = T_Icon717; break;
            case ((short)Shop_Code.Tower_718): texture = T_Icon718; break;
            case ((short)Shop_Code.Tower_719): texture = T_Icon719; break;
            case ((short)Shop_Code.Tower_720): texture = T_Icon720; break;
            case ((short)Shop_Code.Tower_721): texture = T_Icon721; break;
            case ((short)Shop_Code.Tower_722): texture = T_Icon722; break;
            case ((short)Shop_Code.Tower_723): texture = T_Icon723; break;
            case ((short)Shop_Code.Tower_724): texture = T_Icon724; break;
            case ((short)Shop_Code.Tower_725): texture = T_Icon725; break;
            case ((short)Shop_Code.Tower_726): texture = T_Icon726; break;
            case ((short)Shop_Code.Tower_727): texture = T_Icon727; break;
            case ((short)Shop_Code.Tower_728): texture = T_Icon728; break;
            case ((short)Shop_Code.Tower_729): texture = T_Icon729; break;
            case ((short)Shop_Code.Tower_730): texture = T_Icon730; break;
            case ((short)Shop_Code.Tower_731): texture = T_Icon731; break;
            case ((short)Shop_Code.Tower_732): texture = T_Icon732; break;
            case ((short)Shop_Code.Tower_733): texture = T_Icon733; break;
            case ((short)Shop_Code.Tower_734): texture = T_Icon734; break;
            case ((short)Shop_Code.Tower_735): texture = T_Icon735; break;
            case ((short)Shop_Code.Tower_736): texture = T_Icon736; break;
            case ((short)Shop_Code.Tower_737): texture = T_Icon737; break;
            case ((short)Shop_Code.Tower_738): texture = T_Icon738; break;
            case ((short)Shop_Code.Tower_739): texture = T_Icon739; break;
            case ((short)Shop_Code.Tower_740): texture = T_Icon740; break;
            case ((short)Shop_Code.Tower_741): texture = T_Icon741; break;
            case ((short)Shop_Code.Tower_742): texture = T_Icon742; break;
            case ((short)Shop_Code.Tower_743): texture = T_Icon743; break;
            case ((short)Shop_Code.Tower_744): texture = T_Icon744; break;
            case ((short)Shop_Code.Tower_745): texture = T_Icon745; break;
            case ((short)Shop_Code.Tower_746): texture = T_Icon746; break;
            case ((short)Shop_Code.Tower_747): texture = T_Icon747; break;
            case ((short)Shop_Code.Tower_748): texture = T_Icon748; break;
            case ((short)Shop_Code.Tower_749): texture = T_Icon749; break;
            case ((short)Shop_Code.Tower_750): texture = T_Icon750; break;
        }
        return texture;
    }

    public Texture Get_Dim_Texture_By_Code(short Code_Number)
    {
        Texture texture = null;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold): texture = Gold_Dim; break;
            case ((short)Shop_Code.Diamond): texture = Diamond_Dim; break;
            case ((short)Shop_Code.Token_01): texture = Token1_Dim; break;
            case ((short)Shop_Code.Token_02): texture = Token2_Dim; break;
            case ((short)Shop_Code.Token_03): texture = Token3_Dim; break;
            case ((short)Shop_Code.Token_04): texture = Token4_Dim; break;
            case ((short)Shop_Code.Token_05): texture = Token5_Dim; break;
            case ((short)Shop_Code.Token_06): texture = Token6_Dim; break;
            case ((short)Shop_Code.Token_07): texture = Token7_Dim; break;
            case ((short)Shop_Code.Token_08): texture = Token8_Dim; break;
            case ((short)Shop_Code.Token_09): texture = Token9_Dim; break;
            case ((short)Shop_Code.Token_10): texture = Token10_Dim; break;
            case ((short)Shop_Code.Chest_1): texture = Chest_1_Dim; break;
            case ((short)Shop_Code.Chest_2): texture = Chest_2_Dim; break;
            case ((short)Shop_Code.Chest_3): texture = Chest_3_Dim; break;
            case ((short)Shop_Code.Chest_4): texture = Chest_4_Dim; break;
            case ((short)Shop_Code.Chest_5): texture = Chest_5_Dim; break;
            case ((short)Shop_Code.Chest_6): texture = Chest_6_Dim; break;
            case ((short)Shop_Code.Chest_7): texture = Chest_7_Dim; break;
            case ((short)Shop_Code.Chest_8): texture = Chest_8_Dim; break;
            case ((short)Shop_Code.Chest_9): texture = Chest_9_Dim; break;
            case ((short)Shop_Code.Chest_10): texture = Chest_10_Dim; break;

            case ((short)Shop_Code.Tower_101): texture = T_Dim101; break;
            case ((short)Shop_Code.Tower_102): texture = T_Dim102; break;
            case ((short)Shop_Code.Tower_103): texture = T_Dim103; break;
            case ((short)Shop_Code.Tower_104): texture = T_Dim104; break;
            case ((short)Shop_Code.Tower_105): texture = T_Dim105; break;
            case ((short)Shop_Code.Tower_106): texture = T_Dim106; break;
            case ((short)Shop_Code.Tower_107): texture = T_Dim107; break;
            case ((short)Shop_Code.Tower_108): texture = T_Dim108; break;
            case ((short)Shop_Code.Tower_109): texture = T_Dim109; break;
            case ((short)Shop_Code.Tower_110): texture = T_Dim110; break;
            case ((short)Shop_Code.Tower_111): texture = T_Dim111; break;
            case ((short)Shop_Code.Tower_112): texture = T_Dim112; break;
            case ((short)Shop_Code.Tower_113): texture = T_Dim113; break;
            case ((short)Shop_Code.Tower_114): texture = T_Dim114; break;
            case ((short)Shop_Code.Tower_115): texture = T_Dim115; break;
            case ((short)Shop_Code.Tower_116): texture = T_Dim116; break;
            case ((short)Shop_Code.Tower_117): texture = T_Dim117; break;
            case ((short)Shop_Code.Tower_118): texture = T_Dim118; break;
            case ((short)Shop_Code.Tower_119): texture = T_Dim119; break;
            case ((short)Shop_Code.Tower_120): texture = T_Dim120; break;

            case ((short)Shop_Code.Tower_201): texture = T_Dim201; break;
            case ((short)Shop_Code.Tower_202): texture = T_Dim202; break;
            case ((short)Shop_Code.Tower_203): texture = T_Dim203; break;
            case ((short)Shop_Code.Tower_204): texture = T_Dim204; break;
            case ((short)Shop_Code.Tower_205): texture = T_Dim205; break;
            case ((short)Shop_Code.Tower_206): texture = T_Dim206; break;
            case ((short)Shop_Code.Tower_207): texture = T_Dim207; break;
            case ((short)Shop_Code.Tower_208): texture = T_Dim208; break;
            case ((short)Shop_Code.Tower_209): texture = T_Dim209; break;
            case ((short)Shop_Code.Tower_210): texture = T_Dim210; break;
            case ((short)Shop_Code.Tower_211): texture = T_Dim211; break;
            case ((short)Shop_Code.Tower_212): texture = T_Dim212; break;
            case ((short)Shop_Code.Tower_213): texture = T_Dim213; break;
            case ((short)Shop_Code.Tower_214): texture = T_Dim214; break;
            case ((short)Shop_Code.Tower_215): texture = T_Dim215; break;
            case ((short)Shop_Code.Tower_216): texture = T_Dim216; break;
            case ((short)Shop_Code.Tower_217): texture = T_Dim217; break;
            case ((short)Shop_Code.Tower_218): texture = T_Dim218; break;
            case ((short)Shop_Code.Tower_219): texture = T_Dim219; break;
            case ((short)Shop_Code.Tower_220): texture = T_Dim220; break;

            case ((short)Shop_Code.Tower_301): texture = T_Dim301; break;
            case ((short)Shop_Code.Tower_302): texture = T_Dim302; break;
            case ((short)Shop_Code.Tower_303): texture = T_Dim303; break;
            case ((short)Shop_Code.Tower_304): texture = T_Dim304; break;
            case ((short)Shop_Code.Tower_305): texture = T_Dim305; break;
            case ((short)Shop_Code.Tower_306): texture = T_Dim306; break;
            case ((short)Shop_Code.Tower_307): texture = T_Dim307; break;
            case ((short)Shop_Code.Tower_308): texture = T_Dim308; break;
            case ((short)Shop_Code.Tower_309): texture = T_Dim309; break;
            case ((short)Shop_Code.Tower_310): texture = T_Dim310; break;
            case ((short)Shop_Code.Tower_311): texture = T_Dim311; break;
            case ((short)Shop_Code.Tower_312): texture = T_Dim312; break;
            case ((short)Shop_Code.Tower_313): texture = T_Dim313; break;
            case ((short)Shop_Code.Tower_314): texture = T_Dim314; break;
            case ((short)Shop_Code.Tower_315): texture = T_Dim315; break;
            case ((short)Shop_Code.Tower_316): texture = T_Dim316; break;
            case ((short)Shop_Code.Tower_317): texture = T_Dim317; break;
            case ((short)Shop_Code.Tower_318): texture = T_Dim318; break;
            case ((short)Shop_Code.Tower_319): texture = T_Dim319; break;
            case ((short)Shop_Code.Tower_320): texture = T_Dim320; break;

            case ((short)Shop_Code.Tower_401): texture = T_Dim401; break;
            case ((short)Shop_Code.Tower_402): texture = T_Dim402; break;
            case ((short)Shop_Code.Tower_403): texture = T_Dim403; break;
            case ((short)Shop_Code.Tower_404): texture = T_Dim404; break;
            case ((short)Shop_Code.Tower_405): texture = T_Dim405; break;
            case ((short)Shop_Code.Tower_406): texture = T_Dim406; break;
            case ((short)Shop_Code.Tower_407): texture = T_Dim407; break;
            case ((short)Shop_Code.Tower_408): texture = T_Dim408; break;
            case ((short)Shop_Code.Tower_409): texture = T_Dim409; break;
            case ((short)Shop_Code.Tower_410): texture = T_Dim410; break;
            case ((short)Shop_Code.Tower_411): texture = T_Dim411; break;
            case ((short)Shop_Code.Tower_412): texture = T_Dim412; break;
            case ((short)Shop_Code.Tower_413): texture = T_Dim413; break;
            case ((short)Shop_Code.Tower_414): texture = T_Dim414; break;
            case ((short)Shop_Code.Tower_415): texture = T_Dim415; break;
            case ((short)Shop_Code.Tower_416): texture = T_Dim416; break;
            case ((short)Shop_Code.Tower_417): texture = T_Dim417; break;
            case ((short)Shop_Code.Tower_418): texture = T_Dim418; break;
            case ((short)Shop_Code.Tower_419): texture = T_Dim419; break;
            case ((short)Shop_Code.Tower_420): texture = T_Dim420; break;

            case ((short)Shop_Code.Tower_501): texture = T_Dim501; break;
            case ((short)Shop_Code.Tower_502): texture = T_Dim502; break;
            case ((short)Shop_Code.Tower_503): texture = T_Dim503; break;
            case ((short)Shop_Code.Tower_504): texture = T_Dim504; break;
            case ((short)Shop_Code.Tower_505): texture = T_Dim505; break;
            case ((short)Shop_Code.Tower_506): texture = T_Dim506; break;
            case ((short)Shop_Code.Tower_507): texture = T_Dim507; break;
            case ((short)Shop_Code.Tower_508): texture = T_Dim508; break;
            case ((short)Shop_Code.Tower_509): texture = T_Dim509; break;
            case ((short)Shop_Code.Tower_510): texture = T_Dim510; break;
            case ((short)Shop_Code.Tower_511): texture = T_Dim511; break;
            case ((short)Shop_Code.Tower_512): texture = T_Dim512; break;
            case ((short)Shop_Code.Tower_513): texture = T_Dim513; break;
            case ((short)Shop_Code.Tower_514): texture = T_Dim514; break;
            case ((short)Shop_Code.Tower_515): texture = T_Dim515; break;
            case ((short)Shop_Code.Tower_516): texture = T_Dim516; break;
            case ((short)Shop_Code.Tower_517): texture = T_Dim517; break;
            case ((short)Shop_Code.Tower_518): texture = T_Dim518; break;
            case ((short)Shop_Code.Tower_519): texture = T_Dim519; break;
            case ((short)Shop_Code.Tower_520): texture = T_Dim520; break;

            case ((short)Shop_Code.Tower_601): texture = T_Dim601; break;
            case ((short)Shop_Code.Tower_602): texture = T_Dim602; break;
            case ((short)Shop_Code.Tower_603): texture = T_Dim603; break;
            case ((short)Shop_Code.Tower_604): texture = T_Dim604; break;
            case ((short)Shop_Code.Tower_605): texture = T_Dim605; break;
            case ((short)Shop_Code.Tower_606): texture = T_Dim606; break;
            case ((short)Shop_Code.Tower_607): texture = T_Dim607; break;
            case ((short)Shop_Code.Tower_608): texture = T_Dim608; break;
            case ((short)Shop_Code.Tower_609): texture = T_Dim609; break;
            case ((short)Shop_Code.Tower_610): texture = T_Dim610; break;
            case ((short)Shop_Code.Tower_611): texture = T_Dim611; break;
            case ((short)Shop_Code.Tower_612): texture = T_Dim612; break;
            case ((short)Shop_Code.Tower_613): texture = T_Dim613; break;
            case ((short)Shop_Code.Tower_614): texture = T_Dim614; break;
            case ((short)Shop_Code.Tower_615): texture = T_Dim615; break;
            case ((short)Shop_Code.Tower_616): texture = T_Dim616; break;
            case ((short)Shop_Code.Tower_617): texture = T_Dim617; break;
            case ((short)Shop_Code.Tower_618): texture = T_Dim618; break;
            case ((short)Shop_Code.Tower_619): texture = T_Dim619; break;
            case ((short)Shop_Code.Tower_620): texture = T_Dim620; break;
            case ((short)Shop_Code.Tower_621): texture = T_Dim621; break;
            case ((short)Shop_Code.Tower_622): texture = T_Dim622; break;
            case ((short)Shop_Code.Tower_623): texture = T_Dim623; break;
            case ((short)Shop_Code.Tower_624): texture = T_Dim624; break;
            case ((short)Shop_Code.Tower_625): texture = T_Dim625; break;
            case ((short)Shop_Code.Tower_626): texture = T_Dim626; break;
            case ((short)Shop_Code.Tower_627): texture = T_Dim627; break;
            case ((short)Shop_Code.Tower_628): texture = T_Dim628; break;
            case ((short)Shop_Code.Tower_629): texture = T_Dim629; break;
            case ((short)Shop_Code.Tower_630): texture = T_Dim630; break;

            case ((short)Shop_Code.Tower_701): texture = T_Dim701; break;
            case ((short)Shop_Code.Tower_702): texture = T_Dim702; break;
            case ((short)Shop_Code.Tower_703): texture = T_Dim703; break;
            case ((short)Shop_Code.Tower_704): texture = T_Dim704; break;
            case ((short)Shop_Code.Tower_705): texture = T_Dim705; break;
            case ((short)Shop_Code.Tower_706): texture = T_Dim706; break;
            case ((short)Shop_Code.Tower_707): texture = T_Dim707; break;
            case ((short)Shop_Code.Tower_708): texture = T_Dim708; break;
            case ((short)Shop_Code.Tower_709): texture = T_Dim709; break;
            case ((short)Shop_Code.Tower_710): texture = T_Dim710; break;
            case ((short)Shop_Code.Tower_711): texture = T_Dim711; break;
            case ((short)Shop_Code.Tower_712): texture = T_Dim712; break;
            case ((short)Shop_Code.Tower_713): texture = T_Dim713; break;
            case ((short)Shop_Code.Tower_714): texture = T_Dim714; break;
            case ((short)Shop_Code.Tower_715): texture = T_Dim715; break;
            case ((short)Shop_Code.Tower_716): texture = T_Dim716; break;
            case ((short)Shop_Code.Tower_717): texture = T_Dim717; break;
            case ((short)Shop_Code.Tower_718): texture = T_Dim718; break;
            case ((short)Shop_Code.Tower_719): texture = T_Dim719; break;
            case ((short)Shop_Code.Tower_720): texture = T_Dim720; break;
            case ((short)Shop_Code.Tower_721): texture = T_Dim721; break;
            case ((short)Shop_Code.Tower_722): texture = T_Dim722; break;
            case ((short)Shop_Code.Tower_723): texture = T_Dim723; break;
            case ((short)Shop_Code.Tower_724): texture = T_Dim724; break;
            case ((short)Shop_Code.Tower_725): texture = T_Dim725; break;
            case ((short)Shop_Code.Tower_726): texture = T_Dim726; break;
            case ((short)Shop_Code.Tower_727): texture = T_Dim727; break;
            case ((short)Shop_Code.Tower_728): texture = T_Dim728; break;
            case ((short)Shop_Code.Tower_729): texture = T_Dim729; break;
            case ((short)Shop_Code.Tower_730): texture = T_Dim730; break;
            case ((short)Shop_Code.Tower_731): texture = T_Dim731; break;
            case ((short)Shop_Code.Tower_732): texture = T_Dim732; break;
            case ((short)Shop_Code.Tower_733): texture = T_Dim733; break;
            case ((short)Shop_Code.Tower_734): texture = T_Dim734; break;
            case ((short)Shop_Code.Tower_735): texture = T_Dim735; break;
            case ((short)Shop_Code.Tower_736): texture = T_Dim736; break;
            case ((short)Shop_Code.Tower_737): texture = T_Dim737; break;
            case ((short)Shop_Code.Tower_738): texture = T_Dim738; break;
            case ((short)Shop_Code.Tower_739): texture = T_Dim739; break;
            case ((short)Shop_Code.Tower_740): texture = T_Dim740; break;
            case ((short)Shop_Code.Tower_741): texture = T_Dim741; break;
            case ((short)Shop_Code.Tower_742): texture = T_Dim742; break;
            case ((short)Shop_Code.Tower_743): texture = T_Dim743; break;
            case ((short)Shop_Code.Tower_744): texture = T_Dim744; break;
            case ((short)Shop_Code.Tower_745): texture = T_Dim745; break;
            case ((short)Shop_Code.Tower_746): texture = T_Dim746; break;
            case ((short)Shop_Code.Tower_747): texture = T_Dim747; break;
            case ((short)Shop_Code.Tower_748): texture = T_Dim748; break;
            case ((short)Shop_Code.Tower_749): texture = T_Dim749; break;
            case ((short)Shop_Code.Tower_750): texture = T_Dim750; break;
        }
        return texture;
    }

    public Texture Get_Small_Currency_Texture(short Code_Number)
    {
        //Debug.Log("Code_Number || " + Code_Number + " || " + Gold_Small);
        Texture texture = null;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold): texture = Gold_Small; break;
            case ((short)Shop_Code.Diamond): texture = Diamond_Small; break;
            case ((short)Shop_Code.Token_01): texture = Token1_Small; break;
            case ((short)Shop_Code.Token_02): texture = Token2_Small; break;
            case ((short)Shop_Code.Token_03): texture = Token3_Small; break;
            case ((short)Shop_Code.Token_04): texture = Token4_Small; break;
            case ((short)Shop_Code.Token_05): texture = Token5_Small; break;
            case ((short)Shop_Code.Token_06): texture = Token6_Small; break;
            case ((short)Shop_Code.Token_07): texture = Token7_Small; break;
            case ((short)Shop_Code.Token_08): texture = Token8_Small; break;
            case ((short)Shop_Code.Token_09): texture = Token9_Small; break;
            case ((short)Shop_Code.Token_10): texture = Token10_Small; break;
            case ((short)Shop_Code.Chest_1): texture = Chest_1_Small; break;
            case ((short)Shop_Code.Chest_2): texture = Chest_2_Small; break;
            case ((short)Shop_Code.Chest_3): texture = Chest_3_Small; break;
            case ((short)Shop_Code.Chest_4): texture = Chest_4_Small; break;
            case ((short)Shop_Code.Chest_5): texture = Chest_5_Small; break;
            case ((short)Shop_Code.Chest_6): texture = Chest_6_Small; break;
            case ((short)Shop_Code.Chest_7): texture = Chest_7_Small; break;
            case ((short)Shop_Code.Chest_8): texture = Chest_8_Small; break;
            case ((short)Shop_Code.Chest_9): texture = Chest_9_Small; break;
            case ((short)Shop_Code.Chest_10): texture = Chest_10_Small; break;
        }
        return texture;
    }

    public Texture Get_Dim_Small_Currency_Texture(short Code_Number)
    {
        //Debug.Log("Code_Number || " + Code_Number + " || " + Gold_Small);
        Texture texture = null;
        switch (Code_Number)
        {
            case ((short)Shop_Code.Gold): texture = Gold_Small_Dim; break;
            case ((short)Shop_Code.Diamond): texture = Diamond_Small_Dim; break;
            case ((short)Shop_Code.Token_01): texture = Token1_Small_Dim; break;
            case ((short)Shop_Code.Token_02): texture = Token2_Small_Dim; break;
            case ((short)Shop_Code.Token_03): texture = Token3_Small_Dim; break;
            case ((short)Shop_Code.Token_04): texture = Token4_Small_Dim; break;
            case ((short)Shop_Code.Token_05): texture = Token5_Small_Dim; break;
            case ((short)Shop_Code.Token_06): texture = Token6_Small_Dim; break;
            case ((short)Shop_Code.Token_07): texture = Token7_Small_Dim; break;
            case ((short)Shop_Code.Token_08): texture = Token8_Small_Dim; break;
            case ((short)Shop_Code.Token_09): texture = Token9_Small_Dim; break;
            case ((short)Shop_Code.Token_10): texture = Token10_Small_Dim; break;
            case ((short)Shop_Code.Chest_1): texture = Chest_1_Small_Dim; break;
            case ((short)Shop_Code.Chest_2): texture = Chest_2_Small_Dim; break;
            case ((short)Shop_Code.Chest_3): texture = Chest_3_Small_Dim; break;
            case ((short)Shop_Code.Chest_4): texture = Chest_4_Small_Dim; break;
            case ((short)Shop_Code.Chest_5): texture = Chest_5_Small_Dim; break;
            case ((short)Shop_Code.Chest_6): texture = Chest_6_Small_Dim; break;
            case ((short)Shop_Code.Chest_7): texture = Chest_7_Small_Dim; break;
            case ((short)Shop_Code.Chest_8): texture = Chest_8_Small_Dim; break;
            case ((short)Shop_Code.Chest_9): texture = Chest_9_Small_Dim; break;
            case ((short)Shop_Code.Chest_10): texture = Chest_10_Small; break;
        }
        return texture;
    }

    public Texture Get_InGame_Sell_Border_Texture(short Level_Number)
    {
        Texture texture = null;
        switch (Level_Number)
        {
            case (0): texture = InGame_Border_Dim; break;
            case (1): texture = InGame_Border_Lv1; break;
            case (2): texture = InGame_Border_Lv2; break;
            case (3): texture = InGame_Border_Lv3; break;
            case (4): texture = InGame_Border_Lv4; break;
            case (5): texture = InGame_Border_Lv5; break;
            case (6): texture = InGame_Border_Lv6; break;
            case (7): texture = InGame_Border_Lv7; break;
            case (8): texture = InGame_Border_Lv8; break;

        }
        return texture;
    }

    public Texture Get_Rock_Base_Texture(short Level_Number)
    {
        Texture texture = null;
        switch (Level_Number)
        {
            case (1): texture = Base_Rock_01; break;
            case (2): texture = Base_Rock_02; break;
            case (3): texture = Base_Rock_03; break;
            case (4): texture = Base_Rock_04; break;
            case (5): texture = Base_Rock_05; break;
            case (6): texture = Base_Rock_06; break;
            case (7): texture = Base_Rock_07; break;
        }
        return texture;
    }

    public Texture Get_Rock_Dim_Texture(short Level_Number)
    {
        Texture texture = null;
        switch (Level_Number)
        {
            case (1): texture = Base_Dim_01; break;
            case (2): texture = Base_Dim_02; break;
            case (3): texture = Base_Dim_03; break;
            case (4): texture = Base_Dim_04; break;
            case (5): texture = Base_Dim_05; break;
            case (6): texture = Base_Dim_06; break;
            case (7): texture = Base_Dim_07; break;
        }
        return texture;
    }

    public Texture Get_Exchange_Sell_Border_Texture(short Level_Number)
    {
        Texture texture = null;
        switch (Level_Number)
        {
            case (0): texture = InGame_Border_Dim; break;
            case (1): texture = InGame_Border_Lv1; break;
            case (2): texture = InGame_Border_Lv2; break;
            case (3): texture = InGame_Border_Lv3; break;
            case (4): texture = InGame_Border_Lv4; break;
            case (5): texture = InGame_Border_Lv5; break;
            case (6): texture = InGame_Border_Lv6; break;
            case (7): texture = InGame_Border_Lv7; break;
            case (8): texture = InGame_Border_Lv8; break;
        }
        return texture;
    }

    public Texture Get_Treasure_Box_Texture(short Level_Number)
    {
        Texture texture = null;
        switch (Level_Number)
        {
            case (0): texture = Treasure_Box_00; break;
            case (1): texture = Treasure_Box_01; break;
            case (2): texture = Treasure_Box_02; break;
            case (3): texture = Treasure_Box_03; break;
            case (4): texture = Treasure_Box_04; break;
            case (5): texture = Treasure_Box_05; break;
            case (6): texture = Treasure_Box_06; break;
            case (7): texture = Treasure_Box_07; break;
            case (8): texture = Treasure_Box_08; break;
            case (9): texture = Treasure_Box_09; break;
        }
        return texture;
    }
}

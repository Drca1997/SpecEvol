using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDesignConstants
{
    public const float BODY_SHAPE_BUFF_AMOUNT = 0.1f;

    public const int   SIMPLE_ATTACK_DAMAGE = 6;
    public const float SIMPLE_ATTACK_ACCURACY = 0.75f;
    public const int   TENTACLE_GRAB_POWER = 12;
    public const float TENTACLE_GRAB_ACCURACY = 0.5f;
    public const int   TENTACLE_GRAB_DAMAGE = 2;
    public const float BUFFED_BICEP_ACCURACY = 0.75f;
    public const float BUFFED_BICEP_POWER = 0.3f;
    public const float POISONOUS_SPIKE_ACCURACY = 0.75f;
    public const int   POISONOUS_SPIKE_DAMAGE = 2;
    public const float POISONOUS_SPIKE_POISON_ACCURACY = 0.5f;
    public const int   POISONOUS_SPIKE_POISON_DAMAGE = 4;
    public const float FIRE_NOSE_ACCURACY = 0.5f;
    public const int FIRE_NOSE_ON_FIRE_DAMAGE = 10;
    public const float LUCKY_DICE_ACCURACY = 0.5f;
    public const int   LUCKY_DICE_POWER = 25;
    public const float MONKEY_PAW_ACCURACY = 0.5f;
    public const int   MONKEY_PAW_POWER = 25;
    public const float BIG_BONKER_ACCURACY = 0.25f;
    public const int   BIG_BONKER_BASE_DAMAGE = 4;
    public const float SWORD_ACCURACY = 0.25f;
    public const float ZAP_ACCURACY = 0.25f;
    public const int   ZAP_DAMAGE = 2;


    public static string [] ALL_SHAPES_LIST = new string[] {"SquareBodyShape", "CircleBodyShape", "TriangleBodyShape"};
    public static string [] ALL_BODY_PARTS_LIST = new string[] {"BigBonker", "BuffedBicep", "LuckyDice", "MonkeyPaw", "PoisonousSpike", "SimpleAttack", "TentacleGrab"};
    
}

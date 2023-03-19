using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDesignConstants
{
    public const float BODY_SHAPE_BUFF_AMOUNT = 0.1f;

    public const int   SIMPLE_ATTACK_DAMAGE = 6;
    public const float SIMPLE_ATTACK_ACCURACY = 0.75f;
    public const string SIMPLE_ATTACK_FLAVOR_TEXT = "Stick them with the pointy end.";
    public const int   TENTACLE_GRAB_POWER = 12;
    public const float TENTACLE_GRAB_ACCURACY = 0.5f;
    public const int   TENTACLE_GRAB_DAMAGE = 2;
    public const string TENTACLE_GRAB_DESCRIPTION = "Slows down an enemy for 3 turns.";
    public const string TENTACLE_GRAB_FLAVOR_TEXT = "Careful where you stick that.";
    public const float BUFFED_BICEP_ACCURACY = 0.75f;
    public const float BUFFED_BICEP_POWER = 0.3f;
    public const string BUFFED_BICEP_DESCRIPTION = "Intimidates enemies, making their attacks weaker for 3 turns.";
    public const string BUFFED_BICEP_FLAVOR_TEXT = "The only guns you need are *THESE GUNS*";
    public const float POISONOUS_SPIKE_ACCURACY = 0.75f;
    public const int   POISONOUS_SPIKE_DAMAGE = 2;
    public const float POISONOUS_SPIKE_POISON_ACCURACY = 0.5f;
    public const int   POISONOUS_SPIKE_POISON_DAMAGE = 4;
    public const string POISONOUS_SPIKE_DESCRIPTION = "Has a 50% of poisoning the enemy for 3 turns. For each turn that a creature is poisoned, it takes 4 base damage.";
    public const string POISONOUS_SPIKE_FLAVOR_TEXT = "Hope you are not alergic";
    public const float FIRE_NOSE_ACCURACY = 0.5f;
    public const int   FIRE_NOSE_ON_FIRE_DAMAGE = 10;
    public const string FIRE_NOSE_DESCRIPTION = "Puts an enemy's arm on fire. If the enemy uses that arm, takes 10 base damage.";
    public const string FIRE_NOSE_FLAVOR_TEXT = "BURN THEM ALL!!!";
    public const float LUCKY_DICE_ACCURACY = 0.5f;
    public const int   LUCKY_DICE_POWER = 25;
    public const string LUCKY_DICE_DESCRIPTION = "Gives a 25% accuracy boost during the next 3 turns.";
    public const string LUCKY_DICE_FLAVOR_TEXT = "Time to get lucky.";
    public const float MONKEY_PAW_ACCURACY = 0.5f;
    public const int   MONKEY_PAW_POWER = 25;
    public const string MONKEY_PAW_DESCRIPTION = "Gives a 25% accuracy penalty during the next 3 turns.";
    public const string MONKEY_PAW_FLAVOR_TEXT = "No monkey business.";
    public const float BIG_BONKER_ACCURACY = 0.25f;
    public const int   BIG_BONKER_BASE_DAMAGE = 4;
    public const string BIG_BONKER_DESCRIPTION = "For each successful consecutive attack, the damage dealt doubles.";
    public const string BIG_BONKER_FLAVOR_TEXT = "A real big bonk.";
    public const float SWORD_ACCURACY = 0.25f;
    public const string SWORD_DESCRIPTION = "Cuts off an enemy's arm. Can only be used one time per battle.";
    public const string SWORD_FLAVOR_TEXT = "Just a bit at the top";
    public const float ZAP_ACCURACY = 0.25f;
    public const int   ZAP_DAMAGE = 2;
    public const string ZAP_DESCRIPTION = "Stuns an enemy, forcing it to skip a turn.";
    public const string ZAP_FLAVOR_TEXT = "Stolen from Zeus.";


    public static string [] ALL_SHAPES_LIST = new string[] {"SquareBodyShape", "CircleBodyShape", "TriangleBodyShape"};
    public static string [] ALL_BODY_PARTS_LIST = new string[] {"BigBonker", "BuffedBicep", "FireNose", "LuckyDice", "MonkeyPaw", "PoisonousSpike", "SimpleAttack", "Sword", "TentacleGrab", "Zap"};
    

}

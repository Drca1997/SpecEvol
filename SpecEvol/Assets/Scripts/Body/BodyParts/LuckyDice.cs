using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyDice : BodyPart
{
    public LuckyDice()
    {
        name = "LuckyDice";
        actionName = "Get Lucky";
        affectedStat = Enums.AffectedStat.LUCK;
        accuracy = GameDesignConstants.LUCKY_DICE_ACCURACY;
        damage = GameDesignConstants.LUCKY_DICE_POWER;
        damageType = Enums.DamageType.NONE;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        owner.GetComponent<CreatureData>().CurrentLuck += damage;
    }
}

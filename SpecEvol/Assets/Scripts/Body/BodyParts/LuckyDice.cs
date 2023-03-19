using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyDice : BodyPart
{
    public LuckyDice()
    {
        name = "LuckyDice";
        actionName = "Lucky Dice";
        accuracy = GameDesignConstants.LUCKY_DICE_ACCURACY;
        damage = GameDesignConstants.LUCKY_DICE_POWER;
        description = GameDesignConstants.LUCKY_DICE_DESCRIPTION;
        flavorText = GameDesignConstants.LUCKY_DICE_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            owner.GetComponent<CreatureData>().Luckied = 3;
        }
        else
        {
            Debug.Log("LUCKY DICE MISS");
        }
    }
}

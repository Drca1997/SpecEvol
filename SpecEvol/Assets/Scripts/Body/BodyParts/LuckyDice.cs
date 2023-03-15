using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyDice : BodyPart
{
    public LuckyDice()
    {
        name = "LuckyDice";
        actionName = "Get Lucky";
        accuracy = GameDesignConstants.LUCKY_DICE_ACCURACY;
        damage = GameDesignConstants.LUCKY_DICE_POWER;
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

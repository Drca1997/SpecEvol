using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedBicep : BodyPart
{
    private float attackReduceAmount;
    public BuffedBicep()
    {
        name = "BuffedBicep";
        actionName = "Flex";
        affectedStat = Enums.AffectedStat.ATTACK;
        accuracy = GameDesignConstants.BUFFED_BICEP_ACCURACY;
        damage = 0;
        damageType = Enums.DamageType.NONE;
        attackReduceAmount = GameDesignConstants.BUFFED_BICEP_POWER;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (ShouldExecute())
        {
            enemy.GetComponent<CreatureData>().Intimidated = 3;
        }
        else
        {
            Debug.Log("FLEX MISS");
        }

    }
}

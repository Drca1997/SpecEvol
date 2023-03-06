using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedBicep : BodyPart
{
    private float attackReduceAmount;
    public BuffedBicep()
    {
        name = "BuffedBicep";
        affectedStat = Enums.AffectedStat.ATTACK;
        accuracy = GameDesignConstants.BUFFED_BICEP_ACCURACY;
        damage = 0;
        damageType = Enums.DamageType.NONE;
        attackReduceAmount = GameDesignConstants.BUFFED_BICEP_POWER;
    }

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        // Add Intimidated Status Effect
    }
}

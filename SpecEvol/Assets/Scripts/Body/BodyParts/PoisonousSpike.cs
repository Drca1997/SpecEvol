using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousSpike : BodyPart
{
    public PoisonousSpike()
    {
        affectedStat = Enums.AffectedStat.HEALTH;
        accuracy = GameDesignConstants.POISONOUS_SPIKE_ACCURACY;
        damage = GameDesignConstants.POISONOUS_SPIKE_DAMAGE;
        damageType = Enums.DamageType.CHEMICAL;
    }

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        //Add Status Effect of Poison
    }
}

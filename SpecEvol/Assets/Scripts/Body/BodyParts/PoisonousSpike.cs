using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousSpike : BodyPart
{
    public PoisonousSpike()
    {
        name = "PoisonousSpike";
        actionName = "Poison";
        accuracy = GameDesignConstants.POISONOUS_SPIKE_ACCURACY;
        damage = GameDesignConstants.POISONOUS_SPIKE_DAMAGE;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            //TO DO Check if Poison
        }
        else
        {
            Debug.Log("POISONOUS SPIKE FAIL");
        }
    }
}

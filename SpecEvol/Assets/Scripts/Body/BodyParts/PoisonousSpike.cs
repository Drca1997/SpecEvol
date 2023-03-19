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
        description = GameDesignConstants.POISONOUS_SPIKE_DESCRIPTION;
        flavorText = GameDesignConstants.POISONOUS_SPIKE_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            //Check if Poison
            if (DidItPoison())
            {
                enemy.GetComponent<CreatureData>().Poisoned = 3;
            }
        }
        else
        {
            Debug.Log("POISONOUS SPIKE FAIL");
        }
    }

    private bool DidItPoison()
    {
        int num = Random.Range(0, 100);
        num += Mathf.Clamp(num, 0, 99);
        float res = (float)num / 100;
        if (res <= GameDesignConstants.POISONOUS_SPIKE_POISON_ACCURACY)
        {
            return true;
        }
        return false;
    }
}

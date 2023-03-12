using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : BodyPart
{

    public SimpleAttack()
    {
        name = "SimpleAttack";
        actionName = "Attack";
        accuracy = GameDesignConstants.SIMPLE_ATTACK_ACCURACY;
        damage = GameDesignConstants.SIMPLE_ATTACK_DAMAGE;
        damageType = Enums.DamageType.SLASHING;
        affectedStat = Enums.AffectedStat.HEALTH;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (ShouldExecute())
        {
            if (owner.GetComponent<CreatureData>().Intimidated > 0)
            {
                enemy.GetComponent<HealthSystem>().ChangeHealth((int)Mathf.Floor(-damage * GameDesignConstants.BUFFED_BICEP_POWER));

            }
            else
            {
                enemy.GetComponent<HealthSystem>().ChangeHealth(-damage);
            }
            Debug.Log("SIMPLE ATTACK");
        }
        else
        {
            Debug.Log("SIMPLE ATTACK MISS");
        }
    }
}

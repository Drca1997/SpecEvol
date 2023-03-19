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
        description = "";
        flavorText = GameDesignConstants.SIMPLE_ATTACK_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            Debug.Log("SIMPLE ATTACK");
        }
        else
        {
            Debug.Log("SIMPLE ATTACK MISS");
        }
    }
}

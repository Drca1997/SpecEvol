using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBonker : BodyPart
{
    private int damageStack;

    public BigBonker()
    {
        name = "BigBonker";
        actionName = "Big Bonk";
        accuracy = GameDesignConstants.BIG_BONKER_ACCURACY;
        damage = GameDesignConstants.BIG_BONKER_BASE_DAMAGE;
        damageStack = 0;
        description = GameDesignConstants.BIG_BONKER_DESCRIPTION;
        flavorText = GameDesignConstants.BIG_BONKER_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(- totalAttackDamage - damageStack);
            damageStack += totalAttackDamage;
            Debug.Log("BIG BONKER SUCCESS");
        }
        else
        {
            damageStack = 0;
            Debug.Log("BIG BONKER MISS");
        }
    }
}

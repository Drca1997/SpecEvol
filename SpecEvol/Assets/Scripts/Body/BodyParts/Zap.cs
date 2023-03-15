using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : BodyPart
{
    public Zap()
    {
        name = "Zap";
        actionName = "Zap";
        accuracy = GameDesignConstants.ZAP_ACCURACY;
        damage = GameDesignConstants.ZAP_DAMAGE;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            //TO DO: make enemy skip a turn
        }
        else
        {
            Debug.Log("ZAP MISS");
        }
    }
}

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
        description = GameDesignConstants.ZAP_DESCRIPTION;
        flavorText = GameDesignConstants.ZAP_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            //make enemy skip a turn
            GameManager.Instance.SkipTurn(enemy);
        }
        else
        {
            Debug.Log("ZAP MISS");
        }
    }
}

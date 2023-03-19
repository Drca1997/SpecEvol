using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : BodyPart
{
    private int slowDownPower;
    public TentacleGrab()
    {
        name = "TentacleGrab";
        actionName = "Grab";
        accuracy = GameDesignConstants.TENTACLE_GRAB_ACCURACY;
        damage = GameDesignConstants.TENTACLE_GRAB_DAMAGE;
        slowDownPower = GameDesignConstants.TENTACLE_GRAB_POWER;
        description = GameDesignConstants.TENTACLE_GRAB_DESCRIPTION;
        flavorText = GameDesignConstants.TENTACLE_GRAB_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<HealthSystem>().ChangeHealth(-totalAttackDamage);
            enemy.GetComponent<CreatureData>().CurrentSpeed -= slowDownPower;
            //Add Slowed Down Status
            enemy.GetComponent<CreatureData>().SlowedDown = 3;
            //RecalculateTurnOrder 
            GameManager.Instance.RecalculateTurnOrder();
        }
        else
        {
            Debug.Log("TENTACLE GRAB MISS");
        }
    }
}

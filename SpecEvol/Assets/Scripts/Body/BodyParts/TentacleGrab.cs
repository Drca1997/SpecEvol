using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : BodyPart
{
    public TentacleGrab()
    {
        name = "TentacleGrab";
        actionName = "Grab";
        affectedStat = Enums.AffectedStat.SPEED;
        accuracy = GameDesignConstants.TENTACLE_GRAB_ACCURACY;
        damage = GameDesignConstants.TENTACLE_GRAB_POWER;
        damageType = Enums.DamageType.NONE;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (ShouldExecute())
        {
            enemy.GetComponent<CreatureData>().CurrentSpeed -= damage;
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

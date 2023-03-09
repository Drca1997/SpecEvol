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

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        //enemy.GetComponent<CreatureData>().CurrentLuck = damage;
        //Add Slowed Down Status
        //enemy.GetComponent<CreatureData>().StatusEffects.Add(Enums.S);
        //RecalculateTurnOrder 
    }
    
}

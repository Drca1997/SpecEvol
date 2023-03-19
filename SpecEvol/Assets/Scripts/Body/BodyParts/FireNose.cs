using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNose : BodyPart
{
    public FireNose()
    {
        name = "FireNose";
        actionName = "Fire";
        accuracy = GameDesignConstants.FIRE_NOSE_ACCURACY;
        damage = GameDesignConstants.FIRE_NOSE_ON_FIRE_DAMAGE;
        description = GameDesignConstants.FIRE_NOSE_DESCRIPTION;
        flavorText = GameDesignConstants.FIRE_NOSE_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<CreatureData>().GetRandomBodyPart(out int bodyPartIndex).OnFire = 3;
        }
        else
        {
            Debug.Log("FIRE MISS");
        }
    }
}

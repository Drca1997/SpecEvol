using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedBicep : BodyPart
{
    public BuffedBicep()
    {
        name = "BuffedBicep";
        actionName = "Flex Bicep";
        accuracy = GameDesignConstants.BUFFED_BICEP_ACCURACY;
        damage = 0;
        description = GameDesignConstants.BUFFED_BICEP_DESCRIPTION;
        flavorText = GameDesignConstants.BUFFED_BICEP_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<CreatureData>().Intimidated = 3;
        }
        else
        {
            Debug.Log("FLEX MISS");
        }

    }
}

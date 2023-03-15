using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedBicep : BodyPart
{
    public BuffedBicep()
    {
        name = "BuffedBicep";
        actionName = "Flex";
        accuracy = GameDesignConstants.BUFFED_BICEP_ACCURACY;
        damage = 0;
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

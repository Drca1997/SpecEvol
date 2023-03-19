using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPaw : BodyPart
{
    public MonkeyPaw()
    {
        name = "MonkeyPaw";
        actionName = "Jynx";
        accuracy = GameDesignConstants.MONKEY_PAW_ACCURACY;
        damage = GameDesignConstants.MONKEY_PAW_POWER;
        description = GameDesignConstants.MONKEY_PAW_DESCRIPTION;
        flavorText = GameDesignConstants.MONKEY_PAW_FLAVOR_TEXT;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        GetAttackModifiers(owner);
        if (ShouldExecute(attackLuckModifier))
        {
            enemy.GetComponent<CreatureData>().Jynxed = 3;
        }
        else
        {
            Debug.Log("JYNX MISS");
        }
    }
}

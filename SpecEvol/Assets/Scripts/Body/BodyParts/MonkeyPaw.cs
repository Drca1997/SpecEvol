using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPaw : BodyPart
{
    public MonkeyPaw()
    {
        name = "MonkeyPaw";
        actionName = "Jynx";
        affectedStat = Enums.AffectedStat.LUCK;
        accuracy = GameDesignConstants.MONKEY_PAW_ACCURACY;
        damage = GameDesignConstants.MONKEY_PAW_POWER;
        damageType = Enums.DamageType.NONE;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (ShouldExecute())
        {
            enemy.GetComponent<CreatureData>().CurrentLuck -= damage;
        }
        else
        {
            Debug.Log("JYNX MISS");
        }
    }
}

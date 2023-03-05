using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPaw : BodyPart
{
    public MonkeyPaw()
    {
        affectedStat = Enums.AffectedStat.LUCK;
        accuracy = GameDesignConstants.MONKEY_PAW_ACCURACY;
        damage = GameDesignConstants.MONKEY_PAW_POWER;
        damageType = Enums.DamageType.NONE;
    }

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        enemy.GetComponent<CreatureData>().CurrentLuck -= damage;
    }
}

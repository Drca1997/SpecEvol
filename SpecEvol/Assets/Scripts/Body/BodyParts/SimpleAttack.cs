using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : BodyPart
{

    public SimpleAttack()
    {
        accuracy = GameDesignConstants.SIMPLE_ATTACK_ACCURACY;
        damage = GameDesignConstants.SIMPLE_ATTACK_DAMAGE;
        damageType = Enums.DamageType.SLASHING;
        affectedStat = Enums.AffectedStat.HEALTH;
    }

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        enemy.GetComponent<HealthSystem>().ChangeHealth(-damage); 
    }
}

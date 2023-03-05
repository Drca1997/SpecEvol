using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBonker : BodyPart
{
    private int damageStack;

    public BigBonker()
    {
        accuracy = 0.25f;
        affectedStat = Enums.AffectedStat.HEALTH;
        damage = GameDesignConstants.BIG_BONKER_BASE_DAMAGE;
        damageType = Enums.DamageType.BONK;
        damageStack = 0;
    }

    protected override void Execute(GameObject owner, GameObject enemy)
    {
        enemy.GetComponent<HealthSystem>().ChangeHealth(- (damage + damageStack));
        damageStack += damage;
        //to do: reset damage stack when failed. it is not here tho
    }
}

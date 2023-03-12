using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBonker : BodyPart
{
    private int damageStack;

    public BigBonker()
    {
        name = "BigBonker";
        actionName = "Big Bonk";
        accuracy = 0.25f;
        affectedStat = Enums.AffectedStat.HEALTH;
        damage = GameDesignConstants.BIG_BONKER_BASE_DAMAGE;
        damageType = Enums.DamageType.BONK;
        damageStack = 0;
    }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (ShouldExecute())
        {
            if (owner.GetComponent<CreatureData>().Intimidated > 0)
            {
                enemy.GetComponent<HealthSystem>().ChangeHealth((int)Mathf.Floor(- (damage + damageStack) * GameDesignConstants.BUFFED_BICEP_POWER)); 
            }
            else
            {
                enemy.GetComponent<HealthSystem>().ChangeHealth(- (damage + damageStack));
            }
            damageStack += damage;
            Debug.Log("BIG BONKER SUCCESS");
        }
        else
        {
            damageStack = 0;
            Debug.Log("BIG BONKER MISS");
        }
    }
}

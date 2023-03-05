using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public abstract class BodyPart
{
    protected float accuracy;
    protected int damage;

    protected AffectedStat affectedStat;
    protected DamageType damageType;

    protected abstract void Execute(GameObject owner, GameObject enemy);
    
}

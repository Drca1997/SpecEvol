using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[Serializable]
public abstract class BodyPart
{
    protected string name;
    protected float accuracy;
    protected int damage;

    protected AffectedStat affectedStat;
    protected DamageType damageType;

    protected abstract void Execute(GameObject owner, GameObject enemy);
    
}

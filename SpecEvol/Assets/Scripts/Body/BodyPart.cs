using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[Serializable]
public abstract class BodyPart
{
    protected string name;
    protected string actionName;
    protected float accuracy;
    protected int damage;

    protected AffectedStat affectedStat;
    protected DamageType damageType;

    public string Name { get => name; }

    protected abstract void Execute(GameObject owner, GameObject enemy);
    
}

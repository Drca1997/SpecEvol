using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

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
    public string ActionName { get => actionName; }

    public abstract void Execute(GameObject owner, GameObject enemy);

    protected bool ShouldExecute()
    {
        int num = Random.Range(0, 100);
        float res = (float)num / 100;
        if (res <= accuracy)
        {
            return true;
        }
        return false;
    }
    
}

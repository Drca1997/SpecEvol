using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public interface IBodyPart 
{
    float Accuracy { get; set; }
    int Damage { get; set; }

    AffectedStat AffectedStat
    {
        get;
        set;
    }

    DamageType DamageType
    {
        get;
        set;
    }

    void Execute();
}

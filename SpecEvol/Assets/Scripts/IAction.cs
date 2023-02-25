using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public interface IAction 
{
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

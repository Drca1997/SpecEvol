using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public abstract class BodyShape
{
    protected AffectedStat affectedStat;
    private float buffAmount = GameDesignConstants.BODY_SHAPE_BUFF_AMOUNT;

    public AffectedStat AffectedStat { get => affectedStat;}
    public float BuffAmount { get => buffAmount;  }
}

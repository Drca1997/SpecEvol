using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[Serializable]
public abstract class BodyShape
{
    protected Vector3[] bodyPartSlots;  
    protected AffectedStat affectedStat;
    private float buffAmount = GameDesignConstants.BODY_SHAPE_BUFF_AMOUNT;

    protected List<BodyPart> attachedBodyParts;
    public AffectedStat AffectedStat { get => affectedStat;}
    public float BuffAmount { get => buffAmount;  }
    public List<BodyPart> AttachedBodyParts { get => attachedBodyParts; set => attachedBodyParts = value; }
}

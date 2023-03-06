using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBodyShape : BodyShape
{
    public SquareBodyShape()
    {
        bodyPartSlots = new Vector3[2];
        affectedStat = Enums.AffectedStat.ATTACK;
        attachedBodyParts = new List<BodyPart>();
    }
}

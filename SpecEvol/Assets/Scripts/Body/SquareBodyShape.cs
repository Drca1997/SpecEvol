using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBodyShape : BodyShape
{
    public SquareBodyShape()
    {
        affectedStat = Enums.AffectedStat.ATTACK;
        attachedBodyParts = new List<BodyPart>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBodyShape : BodyShape
{
    public TriangleBodyShape()
    {
        affectedStat = Enums.AffectedStat.SPEED;
        attachedBodyParts = new List<BodyPart>();
    }
}

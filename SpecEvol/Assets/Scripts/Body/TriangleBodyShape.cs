using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBodyShape : BodyShape
{
    public TriangleBodyShape()
    {
        bodyPartSlots = new Vector3[2];
        affectedStat = Enums.AffectedStat.SPEED;
        attachedBodyParts = new List<BodyPart>();
    }
}

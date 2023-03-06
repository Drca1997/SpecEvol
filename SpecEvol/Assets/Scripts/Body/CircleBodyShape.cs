using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBodyShape : BodyShape
{
    public CircleBodyShape()
    {
        bodyPartSlots = new Vector3[2];
        affectedStat = Enums.AffectedStat.HEALTH;
        attachedBodyParts = new List<BodyPart>();
    }
}

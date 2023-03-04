using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBodyShape : BodyShape
{
    public CircleBodyShape()
    {
        affectedStat = Enums.AffectedStat.HEALTH;
    }
}

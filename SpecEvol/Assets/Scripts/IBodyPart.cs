using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBodyPart 
{
    List<IAction> bodyPartActions { get; set; }
}

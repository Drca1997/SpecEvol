using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Creature SO")]
public class PlayerCreatureSO : ScriptableObject
{
    public int maximumHealth;
    public int maximumSpeed;
    public int maximumLuck;
    public List<BodyShape> bodyShapes;
}

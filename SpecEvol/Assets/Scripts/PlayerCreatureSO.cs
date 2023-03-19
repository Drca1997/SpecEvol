using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Creature SO")]
public class PlayerCreatureSO : ScriptableObject
{
    public int maximumHealth;
    public int maximumSpeed;
    public List<string> bodyMorphology;
    public List<string> encodedBodyShapes;

    
    public bool IsInitialized()
    {
        return encodedBodyShapes != null && encodedBodyShapes.Count > 0;
    }

    public void Reset()
    {
        maximumHealth = 0;
        maximumSpeed = 0;
        bodyMorphology = null;
        encodedBodyShapes = null;
    }
}

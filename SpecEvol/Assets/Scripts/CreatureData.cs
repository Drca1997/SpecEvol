using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CreatureData : MonoBehaviour
{
    private string creatureName;
    private int maximumHealth;
    private int maximumSpeed;
    private int maximumLuck;
    private int currentLuck;
    private int currentSpeed;

    private List<BodyShape> bodyShapes;

    public string CreatureName { get => creatureName; set => creatureName = value; }
    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    public int MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }
    public int MaximumLuck { get => maximumLuck; set => maximumLuck = value; }
    public int CurrentLuck { get => currentLuck; set => currentLuck = value; }
    public int CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public List<BodyShape> BodyShapes { get => bodyShapes; set => bodyShapes = value; }

    private void Start()
    {
        currentLuck = maximumHealth;
        currentSpeed = maximumSpeed;
    }

    public void GetBodyShapeRefs()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<BodyShapeData>() != null)
            {
                bodyShapes.Add(transform.GetChild(i).GetComponent<BodyShapeData>().BodyShape);
            }
        }
    }

}

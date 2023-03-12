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

    private int slowedDown = 0;
    private int intimidated = 0;
    private int jynxed = 0;
    private int luckied = 0;

    public string CreatureName { get => creatureName; set => creatureName = value; }
    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    public int MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }
    public int MaximumLuck { get => maximumLuck; set => maximumLuck = value; }
    public int CurrentLuck { get => currentLuck; set => currentLuck = value; }
    public int CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public List<BodyShape> BodyShapes { get => bodyShapes; set => bodyShapes = value; }
    public int SlowedDown { get => slowedDown; set => slowedDown = value; }
    public int Intimidated { get => intimidated; set => intimidated = value; }
    public int Jynxed { get => jynxed; set => jynxed = value; }
    public int Luckied { get => luckied; set => luckied = value; }

    private void Start()
    {
        currentLuck = maximumHealth;
        currentSpeed = maximumSpeed;
    }

    public void GetBodyShapeRefs()
    {
        bodyShapes.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<BodyShapeData>() != null)
            {
                bodyShapes.Add(transform.GetChild(i).GetComponent<BodyShapeData>().BodyShape);
            }
        }
    }

    public List<BodyPart> GetBodyParts()
    {
        List<BodyPart> bodyParts = new List<BodyPart>();
        foreach(BodyShape shape in bodyShapes)
        {
            bodyParts.AddRange(shape.AttachedBodyParts);
        }
        return bodyParts;
    }

    public void UpdateStatus()
    {
        if (slowedDown > 0)
        {
            slowedDown--;
            if (slowedDown == 0)
            {
                currentSpeed = maximumSpeed; //full recovery ou só amount que perdeu?
            }
        }
        if (intimidated > 0)
        {
            intimidated--;
        }
        if (luckied > 0)
        {
            luckied--;
            if (luckied == 0)
            {
                currentLuck = maximumLuck;
            }
        }
        if (jynxed > 0)
        {
            jynxed--;
            if (jynxed == 0)
            {
                currentLuck = maximumLuck;
            }
        }
    }
}

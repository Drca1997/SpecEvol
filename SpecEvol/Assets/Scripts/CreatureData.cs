using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CreatureData : MonoBehaviour
{
    private string creatureName;
    private int maximumHealth;
    private int maximumSpeed;
    private int currentSpeed;

    private List<BodyShape> bodyShapes;

    private int slowedDown = 0;
    private int intimidated = 0;
    private int jynxed = 0;
    private int luckied = 0;
    private int poisoned = 0;

    public string CreatureName { get => creatureName; set => creatureName = value; }
    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    public int MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }
    public int CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public List<BodyShape> BodyShapes { get => bodyShapes; set => bodyShapes = value; }
    public int SlowedDown { get => slowedDown; set => slowedDown = value; }
    public int Intimidated { get => intimidated; set => intimidated = value; }
    public int Jynxed { get => jynxed; set => jynxed = value; }
    public int Luckied { get => luckied; set => luckied = value; }
    public int Poisoned { get => poisoned; set => poisoned = value; }

    private void Start()
    {
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
        UpdateBodyPartsActiveStatus();
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
        }
        if (jynxed > 0)
        {
            jynxed--;
        }
        if (poisoned > 0)
        {
            poisoned--;
            GetComponent<HealthSystem>().ChangeHealth(-GameDesignConstants.POISONOUS_SPIKE_POISON_DAMAGE);
        }
    }

    public void UpdateBodyPartsActiveStatus()
    {
        foreach (BodyShape shape in bodyShapes)
        {
            foreach (BodyPart part in shape.AttachedBodyParts)
            {
                if (part.OnFire > 0)
                {
                    part.OnFire--;
                }
            }
        }
    }

    public void ResetBodyPartsStatus()
    {
        foreach (BodyShape shape in bodyShapes)
        {
            foreach (BodyPart part in shape.AttachedBodyParts)
            {
                if (part.CutOff)
                {
                    part.CutOff = false;
                }
                if (part.OnFire > 0)
                {
                    part.OnFire = 0;
                }
            }
        }
    }

    public void CalculateStats()
    {
        maximumHealth = 0;
        maximumSpeed = 0;
        foreach(BodyShape shape in bodyShapes)
        {
            if (shape is CircleBodyShape)
            {
                MaximumHealth += 33;
                MaximumSpeed += 25;
            }   
            else if(shape is TriangleBodyShape)
            {
                MaximumHealth += 25;
                MaximumSpeed += 33;
            }
            else
            {
                MaximumHealth += 25;
                MaximumSpeed += 25;
            }
        }
        currentSpeed = MaximumSpeed;
        GetComponent<HealthSystem>().UpdateMaximumHealth(maximumHealth);
    }

    public int GetSquareDamageBonus()
    {
        int damageBonus = 0;
        foreach(BodyShape shape in bodyShapes)
        {
            if (shape is SquareBodyShape)
            {
                damageBonus += 2;
            }
        }
        return damageBonus;
    }

    public int GetLuckModifier()
    {
        int modifier = 0;
        if (luckied > 0)
        {
            modifier += GameDesignConstants.LUCKY_DICE_POWER;
        }
        else if(jynxed > 0)
        {
            modifier -= GameDesignConstants.MONKEY_PAW_POWER;
        }
        return modifier;
    }

    public BodyPart GetRandomBodyPart(out int bodyPartIndex)
    {
        List<BodyPart> allParts = new List<BodyPart>();
        foreach (BodyShape shape in bodyShapes)
        {
            foreach(BodyPart part in shape.AttachedBodyParts)
            {
                allParts.Add(part);
            }
        }
        bodyPartIndex = Random.Range(0, allParts.Count);
        return allParts[bodyPartIndex];
    }
}

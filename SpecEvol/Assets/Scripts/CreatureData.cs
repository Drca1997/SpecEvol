using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CreatureData : MonoBehaviour
{
    [SerializeField]
    private int maximumHealth;
    [SerializeField]
    private int maximumSpeed;

    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    public int MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }

    List<IAction> possibleActions;
    //List<Status> activeStatus

    // Start is called before the first frame update
    void Start()
    {
        InitializeCreature();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeCreature()
    {
        maximumHealth += CalculateStartingHealth();
        maximumSpeed += CalculateStartingSpeed();
        possibleActions = GetPossibleActions();
        Assert.IsTrue(possibleActions != null);
    }

    private int CalculateStartingHealth()
    {
        return 0;
    }
    
    private int CalculateStartingSpeed()
    {
        return 0;
    }

    private List<IAction> GetPossibleActions()
    {
        List<IAction> actions = new List<IAction>();
        IBodyPart [] mutations = GetComponentsInChildren<IBodyPart>();
        foreach(IBodyPart mutation in mutations)
        {
            actions.Add(mutation.bodyPartAction);
        }
        return actions;
    }
}

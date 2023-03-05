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

    public string CreatureName { get => creatureName; set => creatureName = value; }
    public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    public int MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }
    public int MaximumLuck { get => maximumLuck; set => maximumLuck = value; }
    public int CurrentLuck { get => currentLuck; set => currentLuck = value; }

    private void Start()
    {
        currentLuck = maximumHealth;
    }

}

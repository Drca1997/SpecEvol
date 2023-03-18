using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    private int currentHealth;

    public static event EventHandler<OnHealthChangedArgs> OnHealthChanged;

    public class OnHealthChangedArgs: EventArgs
    {
        public GameObject gameObject;
    }

    public int CurrentHealth { get => currentHealth; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<CreatureData>().MaximumHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int changeAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + changeAmount, 0, int.MaxValue);
        OnHealthChanged?.Invoke(this, new OnHealthChangedArgs {gameObject = gameObject });
        if (currentHealth <= 0)
        {
            //Calls Death Method/Script
            if (gameObject == PlayerManager.Instance.PlayerGameObject)
            {
                //GameOver
                GameManager.Instance.GameOver();
            }
            else
            {
                //Battle Victory
                GameManager.Instance.BattleVictory();
            }
        }
    }

    public void UpdateMaximumHealth(int health)
    {
        currentHealth = health;
    }
}

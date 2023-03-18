using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

[Serializable]
public abstract class BodyPart
{
    protected string name;
    protected string actionName;
    protected float accuracy;
    protected int damage;

    protected bool cutOff = false;
    protected int onFire = 0;

    protected int totalAttackDamage;
    protected int attackLuckModifier;

    

    public string Name { get => name; }
    public string ActionName { get => actionName; }
    public bool CutOff { get => cutOff; set => cutOff = value; }
    public int OnFire { get => onFire; set => onFire = value; }

    public abstract void Execute(GameObject owner, GameObject enemy);

    protected bool ShouldExecute(int luckModifier)
    {
        int num = Random.Range(0, 100);
        num += Mathf.Clamp(num + luckModifier, 0, 99);
        float res = (float)num / 100;
        if (res <= accuracy)
        {
            int attackSound = Random.Range(1, 3);
            AudioManager.instance.Play("Attack" + attackSound.ToString());
            return true;
        }
        int failedAttackSound = Random.Range(1, 5);
        AudioManager.instance.Play("FailedAttack" + failedAttackSound.ToString());
        return false;
    }

    protected void GetAttackModifiers(GameObject owner)
    {
        CreatureData ownerCreatureData = owner.GetComponent<CreatureData>();
        attackLuckModifier = ownerCreatureData.GetLuckModifier();
        totalAttackDamage = GetTotalAttackDamage(ownerCreatureData);
    }

    protected int GetTotalAttackDamage(CreatureData creatureData)
    {
        int totalAttackDamage = damage + creatureData.GetSquareDamageBonus();
        if (creatureData.Intimidated > 0)
        {
            totalAttackDamage = (int)Mathf.Floor(totalAttackDamage * GameDesignConstants.BUFFED_BICEP_POWER);
        }
        return totalAttackDamage;
    }
    
}

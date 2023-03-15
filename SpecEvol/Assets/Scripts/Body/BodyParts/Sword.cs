using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BodyPart
{
    private bool used;

    public Sword()
    {
        name = "Sword";
        actionName = "Sword";
        accuracy = GameDesignConstants.SWORD_ACCURACY;
        damage = 0;
        used = false;
    }

    public bool Used { get => used; set => used = value; }

    public override void Execute(GameObject owner, GameObject enemy)
    {
        if (!used)
        {
            GetAttackModifiers(owner);
            if (ShouldExecute(attackLuckModifier))
            {
                enemy.GetComponent<CreatureData>().GetRandomBodyPart().CutOff = true; 
                //TO DO: if enemy is player, deactivate cutted off body part action button in UI
                used = true;
            }
            else
            {
                Debug.Log("SWORD MISS");
            }
        }
    }
}

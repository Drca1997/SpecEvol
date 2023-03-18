using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BodyPart
{
    private bool used;

    public static event EventHandler<OnCutArgs> OnCut;
    public class OnCutArgs: EventArgs
    {
        public int cutBodyPartIndex;
    }

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
                BodyPart cutPart = enemy.GetComponent<CreatureData>().GetRandomBodyPart(out int bodyPartIndex);
                cutPart.CutOff = true;

                //if enemy is player, deactivate cutted off body part action button in UI
                if (enemy == PlayerManager.Instance.PlayerGameObject)
                {
                    OnCut?.Invoke(this, new OnCutArgs {cutBodyPartIndex = bodyPartIndex});
                }
                used = true;
            }
            else
            {
                Debug.Log("SWORD MISS");
            }
        }
    }
}

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
        name = "Claw";
        actionName = "Claw";
        accuracy = GameDesignConstants.SWORD_ACCURACY;
        damage = 0;
        used = false;
        description = GameDesignConstants.SWORD_DESCRIPTION;
        flavorText = GameDesignConstants.SWORD_FLAVOR_TEXT;
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
                HideCutPartSprite(enemy.GetComponent<CreatureData>(), bodyPartIndex);
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

    private void HideCutPartSprite(CreatureData creature, int bodyPartIndex)
    {
        int shapeIndex = (int)Mathf.Floor(bodyPartIndex / 2);
        int j = -1;
        for (int i = 0; i < creature.gameObject.transform.childCount; i++)
        {
            if (creature.gameObject.transform.GetChild(i).GetComponent<BodyShapeData>() != null)
            {
                j++;
                if (shapeIndex == j)
                {
                    if (bodyPartIndex % 2 == 0)
                    {
                        creature.gameObject.transform.GetChild(i).GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = null;
                    }
                    else
                    {
                        creature.gameObject.transform.GetChild(i).GetChild(1).GetComponentInChildren<SpriteRenderer>().sprite = null;
                    }
                }
            }
        }
    }
}

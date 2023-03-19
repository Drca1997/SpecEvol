using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro partName;
    [SerializeField]
    private TextMeshPro damageValue;
    [SerializeField]
    private TextMeshPro accuracyValue;
    [SerializeField]
    private TextMeshPro description;
    [SerializeField]
    private TextMeshPro flavorText;

    public void SetInfo(string name, int damage, float accuracy, string description, string flavorText)
    {
        partName.text         = name;
        damageValue.text      = "Damage:      " + damage.ToString();
        accuracyValue.text    = "Accuracy:   " + (accuracy * 100).ToString() + "%";
        this.description.text = description;
        this.flavorText.text = flavorText;
    }


    
}

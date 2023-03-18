using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem creatureHealth;
    private RectTransform bar;
    private int initialHealth;
    private bool flashing = false;
    [SerializeField]
    private TextMeshProUGUI healthLabel;
    [SerializeField]
    private float flashTime;
    [SerializeField]
    private Color32 normalHealthColor;
    [SerializeField]
    private Color32 lowHealthColor;
    private float currentFlashTime;
    private Image barImage;



    private void Start()
    {
        HealthSystem.OnHealthChanged += OnHealthChanged;
    }

    public void Init(GameObject target)
    {
        creatureHealth = target.GetComponent<HealthSystem>();
        initialHealth = target.GetComponent<CreatureData>().MaximumHealth;
        bar = gameObject.transform.GetChild(1).GetComponent<RectTransform>();
        barImage = bar.GetComponent<Image>();
        Assert.IsNotNull(barImage);
        barImage.color = normalHealthColor;
        SetBar(creatureHealth.CurrentHealth);
    }

    private void Update()
    {
        if (flashing)
        {
            FlashingHealthBar();
        }
       //SetBar(creatureHealth.CurrentHealth);
    }


    private void OnHealthChanged(object sender, HealthSystem.OnHealthChangedArgs args)
    {
        if (args.gameObject == creatureHealth.gameObject)
        {
            SetBar(creatureHealth.CurrentHealth);
        }
    }
    public void SetBar(int health)
    {
        bar.localScale = new Vector3(Conversion(health), bar.localScale.y);
        if (bar.localScale.x < 0.334f)
        {
            flashing = true;
        }
        else
        {
            flashing = false;
        }
        healthLabel.SetText(creatureHealth.CurrentHealth + "/" + creatureHealth.gameObject.GetComponent<CreatureData>().MaximumHealth);
    }


    private float Conversion(int value)
    {
        return (float)value / initialHealth;
    }

    private void FlashingHealthBar()
    {
        currentFlashTime -= Time.deltaTime;
        if (currentFlashTime <= 0)
        {
            SwapBarColors();
            currentFlashTime = flashTime;
        }
    }

    private void SwapBarColors()
    {
        if (barImage.color == normalHealthColor)
        {
            barImage.color = lowHealthColor;
        }
        else
        {
            barImage.color = normalHealthColor;
        }
    }
}

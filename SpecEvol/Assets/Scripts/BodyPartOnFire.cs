using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartOnFire : MonoBehaviour
{
    [SerializeField]
    private float flashTime;
    private Image buttonImage;
    private float currentFlashTime;
    [SerializeField]
    private Color32 normalColor;
    [SerializeField]
    private Color32 onFireColor;
    private bool flashing = false;

    // Start is called before the first frame update
    void Start()
    {
        flashTime = 0.1f;
        normalColor = Color.white;
        onFireColor = Color.red;
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashing)
        {
            FlashingButton();
        }
    }

    private void FlashingButton()
    {
        
        currentFlashTime -= Time.deltaTime;
        if (currentFlashTime <= 0)
        {
            SwapButtonColors();
            currentFlashTime = flashTime;
        }
    }

    private void SwapButtonColors()
    {
        
        if (buttonImage.color == normalColor)
        {
            buttonImage.color= onFireColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
       
    }

    public void SetOnFireStatus(bool status)
    {
        flashing = status;
    }
}

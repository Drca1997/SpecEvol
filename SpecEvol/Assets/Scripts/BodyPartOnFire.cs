using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartOnFire : MonoBehaviour
{
    [SerializeField]
    private float flashTime;
    private Button button;
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
        button = GetComponent<Button>();
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
        var colors = button.colors;
        if (button.colors.normalColor == normalColor)
        {
            colors.normalColor = onFireColor;
        }
        else
        {
            colors.normalColor = normalColor;
        }
        button.colors = colors;
    }

    public void SetOnFireStatus(bool status)
    {
        flashing = status;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.DamagePopupPrefab, position, Quaternion.identity).transform;
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private const float INCREASE_SCALE_AMOUNT = 1f;
    private const float DECREASE_SCALE_AMOUNT = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
    }

    private void Update()
    {
        float moveYSpeed = 3f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            transform.localScale += Vector3.one * INCREASE_SCALE_AMOUNT * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * DECREASE_SCALE_AMOUNT * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] allCutscenesImages;
    [SerializeField]
    private TextMeshProUGUI[] scriptParts;
    [SerializeField]
    private Image cutsceneDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCutscene());

    }

    private IEnumerator PlayCutscene()
    {
        cutsceneDisplay.sprite = allCutscenesImages[0];
        scriptParts[0].gameObject.SetActive(true);
        for (int i = 1; i < allCutscenesImages.Length; i++)
        {
            yield return new WaitForSeconds(5f);
            cutsceneDisplay.sprite = allCutscenesImages[i];
            scriptParts[i - 1].gameObject.SetActive(false);
            scriptParts[i].gameObject.SetActive(true);
        }

    }
}

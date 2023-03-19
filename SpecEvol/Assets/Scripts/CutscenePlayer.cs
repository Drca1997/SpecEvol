using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] allCutscenesImages;
    [SerializeField]
    private TextMeshProUGUI[] scriptParts;
    [SerializeField]
    private Image cutsceneDisplay;
    [SerializeField]
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCutscene());

    }

    private IEnumerator PlayCutscene()
    {
        cutsceneDisplay.sprite = allCutscenesImages[0];
        source.Play();
        //scriptParts[0].gameObject.SetActive(true);
        
        for (int i = 1; i < allCutscenesImages.Length; i++)
        {
            if (i == allCutscenesImages.Length - 1)
            {
                yield return new WaitForSeconds(0.66f);
            }
            else
            {
                yield return new WaitForSeconds(0.33f);
            }
            cutsceneDisplay.sprite = allCutscenesImages[i];
            //scriptParts[i - 1].gameObject.SetActive(false);
            //scriptParts[i].gameObject.SetActive(true);
        }
        SceneManager.LoadScene("MutationScene");

    }
}

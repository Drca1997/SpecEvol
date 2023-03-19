using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBackground : MonoBehaviour
{
    [SerializeField]
    private Image imageField;
    [SerializeField]
    private Sprite[] backgroundFrames;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateBackgroundCoRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AnimateBackgroundCoRoutine()
    {
        int i = 0;
        imageField.sprite = backgroundFrames[i];
        while (true)
        {
            yield return new WaitForSeconds(0.33f);
            i++;
            if (i >= backgroundFrames.Length)
            {
                i = 0;
            }
            imageField.sprite = backgroundFrames[i];
        }
    }

}

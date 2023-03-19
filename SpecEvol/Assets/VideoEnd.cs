using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            EndReached(GetComponent<VideoPlayer>());
        }
    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadSceneAsync(nextSceneName);
    }
}

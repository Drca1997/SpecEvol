using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    [SerializeField]
    private LevelSO levelSO;

    [SerializeField]
    private DefeatedArmsSO defeatedArmsSO;

    [SerializeField]
    private PlayerCreatureSO playerCreatureSO;


    // Start is called before the first frame update
    void Start()
    {
        levelSO.level = 0;
        defeatedArmsSO.option1 = null;
        defeatedArmsSO.option2 = null;
        playerCreatureSO.Reset();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnNewGame()
    {
        SceneManager.LoadSceneAsync("InitialCutscene");
    }
}

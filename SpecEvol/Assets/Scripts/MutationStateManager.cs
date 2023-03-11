using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MutationStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBasePrefab;
    [SerializeField]
    private GameObject gameAssets;
    [SerializeField]
    private Transform placeHolder;
    

    private Enums.BodyShape initialBodyShape;

    public Enums.BodyShape InitialBodyShape { get => initialBodyShape; set => initialBodyShape = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameAssets);
        if (PlayerManager.Instance.PlayerCreature.IsInitialized())
        {
            PlayerManager.Instance.CreatePlayer(placeHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBodyShapeSelected()
    {
        GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab, InitialBodyShape, placeHolder.transform.position);
        PlayerManager.Instance.SavePlayerCreature(newPlayer);
    }

    public void OnNextBattleButton()
    {
        PlayerManager.Instance.SavePlayerCreature(PlayerManager.Instance.PlayerGameObject);
        SceneManager.LoadScene("BattleScene");
    }

}

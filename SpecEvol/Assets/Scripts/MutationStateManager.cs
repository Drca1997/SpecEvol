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
    [SerializeField]
    private GameObject playerManagerObject;
    

    private Enums.BodyShape initialBodyShape;

    public Enums.BodyShape InitialBodyShape { get => initialBodyShape; set => initialBodyShape = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameAssets);
        DontDestroyOnLoad(playerManagerObject);
        if (PlayerManager.Instance.PlayerCreature.IsInitialized())
        {
            PlayerManager.Instance.CreatePlayer(placeHolder.transform);
            if (IsBodyShapeLevelUpTime())
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBodyShapeSelected()
    {
        GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab, InitialBodyShape, placeHolder.transform.position);
        newPlayer.transform.parent = placeHolder.transform;
        newPlayer.transform.localPosition = CreatureGenerator.Instance.GetCreatureSpawnPosition(newPlayer);
        PlayerManager.Instance.SavePlayerCreature(newPlayer);
    }

    public void OnNextBattleButton()
    {
        PlayerManager.Instance.SavePlayerCreature(PlayerManager.Instance.PlayerGameObject);
        SceneManager.LoadScene("BattleScene");
    }

    private bool IsBodyShapeLevelUpTime()
    {
        return false;
    }

}

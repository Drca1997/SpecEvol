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
    [SerializeField]
    private LevelSO levelSO;
    

    private Enums.BodyShape choosenBodyShape;

    public Enums.BodyShape ChoosenBodyShape { get => choosenBodyShape; set => choosenBodyShape = value; }

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
        if (levelSO.level <= 0) 
        {
            GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab, ChoosenBodyShape, placeHolder.transform.position);
            newPlayer.transform.parent = placeHolder.transform;
            newPlayer.transform.localPosition = CreatureGenerator.Instance.GetCreatureSpawnPosition(newPlayer);
            PlayerManager.Instance.SavePlayerCreature(newPlayer);
        }
        else
        {
            switch (choosenBodyShape)
            {
                case Enums.BodyShape.SQUARE:
                    PlayerManager.Instance.UpdatePlayerShape("SquareBodyShape");
                    break;
                case Enums.BodyShape.CIRCLE:
                    PlayerManager.Instance.UpdatePlayerShape("CircleBodyShape");
                    break;
                case Enums.BodyShape.TRIANGLE:
                    PlayerManager.Instance.UpdatePlayerShape("TriangleBodyShape");
                    break;
                default:
                    break;
            }
            choosenBodyShape = Enums.BodyShape.UNDEFINED;
        }
    }

    public void OnNextBattleButton()
    {
        PlayerManager.Instance.SavePlayerCreature(PlayerManager.Instance.PlayerGameObject);
        SceneManager.LoadScene("BattleScene");
    }

    private bool IsBodyShapeLevelUpTime()
    {
        if (levelSO.level % 3 == 0)
        {
            return true;
        }
        return false;
    }

}

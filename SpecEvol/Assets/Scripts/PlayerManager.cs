using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerManager : MonoBehaviour
{
    private PlayerCreatureSO playerCreature;
    [SerializeField]
    private GameObject playerBasePrefab;
    private GameObject playerGameObject;

    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

    public PlayerCreatureSO PlayerCreature { get => playerCreature; set => playerCreature = value; }
    public GameObject PlayerGameObject { get => playerGameObject; set => playerGameObject = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            playerCreature = (PlayerCreatureSO)Resources.Load("New Player Creature SO");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreatePlayer(Transform playerSpawnPosition)
    {
        Assert.IsTrue(playerCreature != null);
        //playerGameObject = Instantiate(playerBasePrefab, Vector3.zero, Quaternion.Euler(0, 0, 0), playerSpawnPosition);
        playerGameObject = Instantiate(playerBasePrefab, playerSpawnPosition);
        CreatureData playerCreatureData = playerGameObject.AddComponent<CreatureData>();
        playerCreatureData.MaximumHealth = playerCreature.maximumHealth;
        playerCreatureData.MaximumSpeed = playerCreature.maximumSpeed;
        playerCreatureData.BodyShapes = CreatureGenerator.Instance.BodyMorphologyDecoding(playerCreature.bodyMorphology, playerCreature.encodedBodyShapes);
        
        CreatureGenerator.Instance.InstantiateCreatureBody(PlayerGameObject, playerCreatureData);
        
        playerCreatureData.GetBodyShapeRefs();
        playerGameObject.transform.localPosition = CreatureGenerator.Instance.GetCreatureSpawnPosition(playerGameObject);
        playerCreatureData.CalculateStats();
    }

    public void SavePlayerCreature(GameObject player)
    {
        CreatureData creatureData = player.GetComponent<CreatureData>();
        playerCreature.maximumHealth = creatureData.MaximumHealth;
        playerCreature.maximumSpeed = creatureData.MaximumSpeed;
        playerCreature.bodyMorphology = CreatureGenerator.Instance.BodyMorphologyEncoding(creatureData.BodyShapes, out List<string> encodedShapes);
        PlayerCreature.encodedBodyShapes = encodedShapes;
    }

    public void UpdatePlayerMorphology(string newPartName)
    {
        playerCreature.bodyMorphology.Add(newPartName);
        Transform battleStation = playerGameObject.transform.parent;
        Destroy(playerGameObject);
        CreatePlayer(battleStation);
    }

    public void UpdatePlayerShape(string newShapeName)
    {
        playerCreature.encodedBodyShapes.Add(newShapeName);
        Transform battleStation = playerGameObject.transform.parent;
        Destroy(playerGameObject);
        CreatePlayer(battleStation);
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {

    }

    

}

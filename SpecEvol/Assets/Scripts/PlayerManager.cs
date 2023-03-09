using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
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
        playerCreatureData.MaximumLuck = playerCreature.maximumLuck;
        playerCreatureData.BodyShapes = BodyMorphologyDecoding(playerCreature.bodyMorphology, playerCreature.encodedBodyShapes);
        
        CreatureGenerator.Instance.InstantiateCreatureBody(PlayerGameObject, playerCreatureData);
        
        playerCreatureData.GetBodyShapeRefs();
        SavePlayerCreature(playerGameObject);
    }

    public void SavePlayerCreature(GameObject player)
    {
        CreatureData creatureData = player.GetComponent<CreatureData>();
        playerCreature.maximumHealth = creatureData.MaximumHealth;
        playerCreature.maximumSpeed = creatureData.MaximumSpeed;
        playerCreature.maximumLuck = creatureData.MaximumLuck;
        playerCreature.bodyMorphology = BodyMorphologyEncoding(creatureData.BodyShapes, out List<string> encodedShapes);
        PlayerCreature.encodedBodyShapes = encodedShapes;
    }

    private List<string> BodyMorphologyEncoding(List<BodyShape> bodyShapes, out List<string> encodedBodyShapes)
    {
        List<string> encodedBodyMorphology = new List<string>();
        encodedBodyShapes = new List<string>();
        foreach(BodyShape shape in bodyShapes)
        {
            if (shape is SquareBodyShape) 
            {
                encodedBodyShapes.Add("Square");
            }
            else if(shape is CircleBodyShape)
            {
                encodedBodyShapes.Add("Circle");
            }
            else if (shape is TriangleBodyShape)
            {
                encodedBodyShapes.Add("Triangle");
            }
            foreach(BodyPart part in shape.AttachedBodyParts)
            {
                encodedBodyMorphology.Add(part.Name);
            }
        }
        return encodedBodyMorphology;
    }

    private List<BodyShape> BodyMorphologyDecoding(List<string> encodedMorphology, List<string> encodedShapes)
    {
        List<BodyShape> decodedBodyMorphology = new List<BodyShape>();
        List<List<BodyPart>> allBodyParts = new List<List<BodyPart>>();
        for (int i = 0; i < encodedMorphology.Count; i++)
        {
            if (i % 2 == 0)
            {
                List<BodyPart> shapeBodyParts = new List<BodyPart>();
                allBodyParts.Add(shapeBodyParts);
            }
            switch (encodedMorphology[i])
            {
                case "BigBonker":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new BigBonker());
                    break;
                case "BuffedBicep":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new BuffedBicep());
                    break;
                case "LuckyDice":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new LuckyDice());
                    break;
                case "MonkeyPaw":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new MonkeyPaw());
                    break;
                case "PoisonousSpike":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new PoisonousSpike());
                    break;
                case "SimpleAttack":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new SimpleAttack());
                    break;
                case "TentacleGrab":
                    allBodyParts[(int)Mathf.Floor(i / 2)].Add(new TentacleGrab());
                    break;
            }
        }
        for (int i = 0; i < encodedShapes.Count; i++)
        {
            BodyShape decodedBodyShape;
            switch (encodedShapes[i])
            {
                case "Square":
                    decodedBodyShape = new SquareBodyShape();
                    break;
                case "Circle":
                    decodedBodyShape = new CircleBodyShape();
                    break;
                case "Triangle":
                    decodedBodyShape = new TriangleBodyShape();
                    break;
                default:
                    decodedBodyShape = new SquareBodyShape();
                    break;
            }
            decodedBodyShape.AttachedBodyParts = allBodyParts[i];
            decodedBodyMorphology.Add(decodedBodyShape);
        }
        return decodedBodyMorphology;
    }

}

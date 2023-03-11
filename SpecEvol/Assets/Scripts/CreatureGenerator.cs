using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CreatureGenerator : MonoBehaviour
{

    private static CreatureGenerator _instance;
    public static CreatureGenerator Instance { get { return _instance; } }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> InstantiateEnemies(int numEnemies)
    {
        List<GameObject> enemies = new List<GameObject>();
        for(int i = 0; i < numEnemies; i++)
        {
            enemies.Add(CreateRandomCreature());
        }
        
        return enemies;
    }

    public GameObject CreateRandomCreature()
    {
        GameObject enemy = new GameObject("Enemy");
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
        creatureData.MaximumHealth = Random.Range(50, 150);
        creatureData.MaximumSpeed = Random.Range(10, 90);
        creatureData.MaximumLuck = Random.Range(10, 90);

        //TO DO: Same Method but Generating BodyParts and Shapes according to difficulty
        int num_shapes = Random.Range(1, 4);
        List<string> encodedShapes = new List<string>();
        List<string> encodedMorphology = new List<string>();
        for (int i = 0; i < num_shapes; i++)
        {
            encodedShapes.Add(GameDesignConstants.ALL_SHAPES_LIST[Random.Range(0, 3)]);
        }
        for(int i = 0; i < encodedShapes.Count; i++)
        {
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[Random.Range(0, GameDesignConstants.ALL_BODY_PARTS_LIST.Length)]);
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[Random.Range(0, GameDesignConstants.ALL_BODY_PARTS_LIST.Length)]);
        }

        creatureData.BodyShapes = BodyMorphologyDecoding(encodedMorphology, encodedShapes);
        InstantiateCreatureBody(enemy, creatureData);
        creatureData.GetBodyShapeRefs();
        enemy.AddComponent<HealthSystem>();
        return enemy;
    }

    public GameObject CreateNewPlayerCreature(GameObject playerBasePrefab, Enums.BodyShape initialBodyShape, Vector3 spawnPosition)
    {
        GameObject newPlayerCreature = Instantiate(playerBasePrefab, spawnPosition, Quaternion.Euler(0, 0, 0), null);
        CreatureData playerCreatureData = newPlayerCreature.AddComponent<CreatureData>();
        playerCreatureData.MaximumHealth = GameDesignConstants.STARTING_PLAYER_HEALTH;
        playerCreatureData.MaximumSpeed = GameDesignConstants.STARTING_PLAYER_SPEED;
        playerCreatureData.MaximumLuck = GameDesignConstants.STARTING_PLAYER_LUCK;
        playerCreatureData.BodyShapes = new List<BodyShape>();
        switch (initialBodyShape)
        {
            case Enums.BodyShape.SQUARE:
                playerCreatureData.BodyShapes.Add(new SquareBodyShape());
                break;
            case Enums.BodyShape.CIRCLE:
                playerCreatureData.BodyShapes.Add(new CircleBodyShape());
                break;
            case Enums.BodyShape.TRIANGLE:
                playerCreatureData.BodyShapes.Add(new TriangleBodyShape());
                break;
        }

        newPlayerCreature.AddComponent<HealthSystem>();
        playerCreatureData.BodyShapes[0].AttachedBodyParts = new List<BodyPart>();
        playerCreatureData.BodyShapes[0].AttachedBodyParts.Add(new SimpleAttack());
        InstantiateCreatureBody(newPlayerCreature, playerCreatureData);
        PlayerManager.Instance.PlayerGameObject = newPlayerCreature;
        return newPlayerCreature;
    }


    public void InstantiateCreatureBody(GameObject creatureObject, CreatureData creatureData)
    {
        foreach (BodyShape shape in creatureData.BodyShapes)
        {
            string name = "";
            if (shape is SquareBodyShape)
            {
                name = "SquareBodyShape";
            }
            else if (shape is CircleBodyShape)
            {
                name = "CircleBodyShape";
            }
            else if (shape is TriangleBodyShape)
            {
                name = "TriangleBodyShape";
            }
            GameObject shapeobj = InstantiateBodyShape(name, creatureObject, shape);
            foreach (BodyPart bodyPart in shape.AttachedBodyParts)
            {
                //Instantiate body part
                GameObject bodyPartObj = new GameObject(bodyPart.ToString());
                bodyPartObj.transform.parent = GetNextFreeBodyPartHolder(shapeobj.transform);
                bodyPartObj.transform.localPosition = Vector3.zero;
                SpriteRenderer bodyPartSpriteRenderer = bodyPartObj.AddComponent<SpriteRenderer>();
                bodyPartSpriteRenderer.sprite = GameAssets.Instance.GetBodyPartByName(bodyPart.Name);
                bodyPartSpriteRenderer.sortingOrder = 1;
                BodyPartData bodyPartData = bodyPartObj.AddComponent<BodyPartData>();
                bodyPartData.BodyPart = bodyPart;
            }
        }
    }


    private GameObject InstantiateBodyShape(string name, GameObject creatureObject, BodyShape shape)
    {
        GameObject shapeobj = new GameObject(name);
        shapeobj.transform.parent = creatureObject.transform;
        SpriteRenderer spriteRenderer = shapeobj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameAssets.Instance.GetBodyShapeSpriteByName(name);
        spriteRenderer.sortingOrder = 0;
        if (creatureObject.transform.childCount <= 1)
        {
            shapeobj.transform.localPosition = Vector3.zero;
        }
        else
        {
            Transform lastChild = creatureObject.transform.GetChild(creatureObject.transform.childCount - 1);
            int offset = creatureObject.transform.childCount - 1;
            shapeobj.transform.localPosition = new Vector3(0f, -lastChild.GetComponent<Renderer>().bounds.extents.y * 2 * offset, 0f);
        }
        BodyShapeData bodyShapeData = shapeobj.AddComponent<BodyShapeData>();
        bodyShapeData.BodyShape = shape;
        GameObject bodyPartLeftHolder = new GameObject("BodyPartLeftHolder");
        bodyPartLeftHolder.transform.parent = shapeobj.transform;
        bodyPartLeftHolder.transform.localPosition = new Vector3(-shapeobj.GetComponent<Renderer>().bounds.extents.x, 0f, 0f); 
        GameObject bodyPartRightHolder = new GameObject("BodyPartRightHolder");
        bodyPartRightHolder.transform.parent = shapeobj.transform;
        bodyPartRightHolder.transform.localPosition = new Vector3(shapeobj.GetComponent<Renderer>().bounds.extents.x, 0f, 0f);
        return shapeobj;
    }

    private Transform GetNextFreeBodyPartHolder(Transform parent)
    {
        if (parent.GetChild(0).childCount == 0)
        {
            return parent.GetChild(0);
        }
        else if (parent.GetChild(1).childCount == 0)
        {
            return parent.GetChild(1);
        }
        return null;
    }

    public List<string> BodyMorphologyEncoding(List<BodyShape> bodyShapes, out List<string> encodedBodyShapes)
    {
        List<string> encodedBodyMorphology = new List<string>();
        encodedBodyShapes = new List<string>();
        foreach (BodyShape shape in bodyShapes)
        {
            if (shape is SquareBodyShape)
            {
                encodedBodyShapes.Add("SquareBodyShape");
            }
            else if (shape is CircleBodyShape)
            {
                encodedBodyShapes.Add("CircleBodyShape");
            }
            else if (shape is TriangleBodyShape)
            {
                encodedBodyShapes.Add("TriangleBodyShape");
            }
            foreach (BodyPart part in shape.AttachedBodyParts)
            {
                encodedBodyMorphology.Add(part.Name);
            }
        }
        return encodedBodyMorphology;
    }

    public List<BodyShape> BodyMorphologyDecoding(List<string> encodedMorphology, List<string> encodedShapes)
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
                case "SquareBodyShape":
                    decodedBodyShape = new SquareBodyShape();
                    break;
                case "CircleBodyShape":
                    decodedBodyShape = new CircleBodyShape();
                    break;
                case "TriangleBodyShape":
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

    public Vector3 GetCreatureSpawnPosition(GameObject creature)
    {
        int numChild = creature.transform.childCount;
        Transform lastChild = creature.transform.GetChild(numChild - 1);
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if (numChild == 1)
        {
            spawnPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y, 0f);
        }
        else
        {
            spawnPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y * (numChild * 2), 0f);

        }
        return spawnPosition;
    }

}

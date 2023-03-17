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

    public List<GameObject> InstantiateEnemies(int numEnemies, int currentLevel)
    {
        List<GameObject> enemies = new List<GameObject>();
        for(int i = 0; i < numEnemies; i++)
        {
            enemies.Add(CreateCreatureAccordingtToDifficulty(currentLevel));
        }
        
        return enemies;
    }

    private GameObject CreateCreatureAccordingtToDifficulty(int currentLevel)
    {
        GameObject enemy = new GameObject("Enemy");
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
        //creatureData.MaximumHealth = Random.Range(50, 150);
        //creatureData.MaximumSpeed = Random.Range(10, 90);
        //creatureData.MaximumLuck = Random.Range(10, 90);
        List<string> encodedShapes = new List<string>();
        List<string> encodedMorphology = new List<string>();
        int numShapes;
        if (currentLevel < 3)
        {
            numShapes = 1;
        }
        else if(currentLevel < 6)
        {
            numShapes = 2;
        }
        else if(currentLevel < 9)
        {
            numShapes = 3;
        }
        else
        {
            numShapes = 4;
        }
        for (int i = 0; i < numShapes; i++)
        {
            encodedShapes.Add(GameDesignConstants.ALL_SHAPES_LIST[Random.Range(0, 3)]);
        }
        for (int i = 0; i < encodedShapes.Count; i++)
        {
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[Random.Range(0, GameDesignConstants.ALL_BODY_PARTS_LIST.Length)]);
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[Random.Range(0, GameDesignConstants.ALL_BODY_PARTS_LIST.Length)]);
        }
        creatureData.BodyShapes = BodyMorphologyDecoding(encodedMorphology, encodedShapes);
        InstantiateCreatureBody(enemy, creatureData);
        creatureData.GetBodyShapeRefs();
        enemy.AddComponent<HealthSystem>();
        creatureData.CalculateStats();
        return enemy;
    }

    public GameObject CreateRandomCreature()
    {
        GameObject enemy = new GameObject("Enemy");
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
        creatureData.MaximumHealth = Random.Range(50, 150);
        creatureData.MaximumSpeed = Random.Range(10, 90);

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
        playerCreatureData.CalculateStats();

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
                bodyPartObj.transform.parent = GetNextFreeBodyPartHolder(shapeobj.transform, out bool isLeft);
                bodyPartObj.transform.localPosition = Vector3.zero;
                SpriteRenderer bodyPartSpriteRenderer = bodyPartObj.AddComponent<SpriteRenderer>();
                bodyPartSpriteRenderer.sprite = GameAssets.Instance.GetBodyPartByName(bodyPart.Name);
                bodyPartSpriteRenderer.sortingOrder = 1;
                if (isLeft)
                {
                    bodyPartObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
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
        spriteRenderer.sprite = GameAssets.Instance.GetBodyShapeSpriteByName(name, creatureObject.transform.childCount - 1);
        if (creatureObject == PlayerManager.Instance.PlayerGameObject)
        {
            shapeobj.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        spriteRenderer.sortingOrder = 0;
        if (creatureObject.transform.childCount <= 1)
        {
            shapeobj.transform.localPosition = Vector3.zero;
        }
        else
        {
            //Transform lastChild = creatureObject.transform.GetChild(creatureObject.transform.childCount - 1);
            //int offset = creatureObject.transform.childCount - 1;
            shapeobj.transform.localPosition = CalculateShapeLocalPosition(creatureObject.transform);//new Vector3(0f, -lastChild.GetComponent<Renderer>().bounds.extents.y * 2 * offset, 0f);
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

    public Transform GetNextFreeBodyPartHolder(Transform parent, out bool isLeft)
    {
        if (parent.GetChild(0).childCount == 0)
        {
            isLeft = true;
            return parent.GetChild(0);
        }
        else if (parent.GetChild(1).childCount == 0)
        {
            isLeft = false;
            return parent.GetChild(1);
        }
        isLeft = false;
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
        //List<List<BodyPart>> allBodyParts = new List<List<BodyPart>>();
        List<BodyPart> allBodyParts = new List<BodyPart>();
        for (int i = 0; i < encodedMorphology.Count; i++)
        {
            switch (encodedMorphology[i])
            {
                case "BigBonker":
                    allBodyParts.Add(new BigBonker());
                    break;
                case "BuffedBicep":
                    allBodyParts.Add(new BuffedBicep());
                    break;
                case "LuckyDice":
                    allBodyParts.Add(new LuckyDice());
                    break;
                case "MonkeyPaw":
                    allBodyParts.Add(new MonkeyPaw());
                    break;
                case "PoisonousSpike":
                    allBodyParts.Add(new PoisonousSpike());
                    break;
                case "SimpleAttack":
                    allBodyParts.Add(new SimpleAttack());
                    break;
                case "TentacleGrab":
                    allBodyParts.Add(new TentacleGrab());
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
            List<BodyPart> attachedBodyParts = new List<BodyPart>();
            if (i * 2 < allBodyParts.Count)
            {
                attachedBodyParts.Add(allBodyParts[i * 2]);
                if(i * 2 + 1 < allBodyParts.Count)
                {
                    attachedBodyParts.Add(allBodyParts[i * 2 + 1]);
                }    
            }
            decodedBodyShape.AttachedBodyParts = attachedBodyParts;
            decodedBodyMorphology.Add(decodedBodyShape);
        }
        return decodedBodyMorphology;
    }

    public Vector3 GetCreatureSpawnPosition(GameObject creature)
    {
        int numChild = creature.transform.childCount;
        /*Transform lastChild = creature.transform.GetChild(numChild - 1);
        Vector3 spawnPosition;
        if (numChild == 1)
        {
            spawnPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y, 0f);
        }
        else
        {
            spawnPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y * (numChild * 2), 0f);

        }*/
        float totalHeight = 0;
        for(int i = 0; i < numChild; i++)
        {
            totalHeight += creature.transform.GetChild(i).GetComponent<Renderer>().bounds.size.y * creature.transform.GetChild(i).localScale.y;
        }

        return new Vector3(0f, totalHeight - creature.transform.GetChild(0).localScale.y, 0f);
    }

    private Vector3 CalculateShapeLocalPosition(Transform parent)
    {
        float totalHeight = 0;
        if (parent.childCount == 2)
        {

            totalHeight = parent.GetChild(1).GetComponent<Renderer>().bounds.size.y + parent.GetChild(1).localScale.y;
            totalHeight -= 0.2f;
        }
        else
        {
            for (int i = 1; i < parent.childCount; i++)
            {
                totalHeight += parent.GetChild(i).GetComponent<Renderer>().bounds.size.y + parent.GetChild(i).localScale.y;
            }
        }
        return new Vector3(0f, -totalHeight, 0f);
    }

}

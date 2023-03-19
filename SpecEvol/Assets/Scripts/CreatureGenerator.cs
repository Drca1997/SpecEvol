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

    public List<GameObject> InstantiateEnemies(int numEnemies, int currentLevel, Transform enemySpawnPosition)
    {
        List<GameObject> enemies = new List<GameObject>();
        for(int i = 0; i < numEnemies; i++)
        {
            enemies.Add(CreateCreatureAccordingtToDifficulty(currentLevel, enemySpawnPosition));
        }
        
        return enemies;
    }

    private GameObject CreateCreatureAccordingtToDifficulty(int currentLevel, Transform enemySpawnPosition)
    {
        GameObject enemy = Instantiate(PlayerManager.Instance.PlayerBasePrefab, enemySpawnPosition);
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
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
        else
        {
            numShapes = 3;
        }
        for (int i = 0; i < numShapes; i++)
        {
            encodedShapes.Add(GameDesignConstants.ALL_SHAPES_LIST[Random.Range(0, 3)]);
        }
        for (int i = 0; i < encodedShapes.Count; i++)
        {
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[GetRandomUniqueBodyPartIndex(encodedMorphology)]);
            encodedMorphology.Add(GameDesignConstants.ALL_BODY_PARTS_LIST[GetRandomUniqueBodyPartIndex(encodedMorphology)]);
        }
#if UNITY_EDITOR
        encodedMorphology[encodedMorphology.Count - 1] = "Claw";
#endif
        CheckIfAtLeastOneDamageDealerArm(encodedMorphology);
        creatureData.BodyShapes = BodyMorphologyDecoding(encodedMorphology, encodedShapes);
        InstantiateCreatureBody(enemy, creatureData);
        if (currentLevel >= 9)
        {
            enemy.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            InstantiateBossHead(enemy);
        }
        creatureData.GetBodyShapeRefs();
        enemy.AddComponent<HealthSystem>();
        creatureData.CalculateStats();
        enemy.AddComponent<DecisionMaking>();
        return enemy;
    }


    private void CheckIfAtLeastOneDamageDealerArm(List<string> encodedMorphology)
    {
        bool val = false;
        foreach (string part in encodedMorphology)
        {
            if (!(part.Equals("BuffedBicep") || part.Equals("LuckyDice") || part.Equals("MonkeyPaw") || part.Equals("Claw")))
            {
                val = true;
                break;
            } 
                    
        }
        if (!val)
        {
            encodedMorphology[encodedMorphology.Count - 1] = "SimpleAttack";
        }
    }

    private int GetRandomUniqueBodyPartIndex(List<string> bodyParts)
    {
        bool valid = false;
        int index = 0;
        while (!valid)
        {
            index = Random.Range(0, GameDesignConstants.ALL_BODY_PARTS_LIST.Length);
            if (!bodyParts.Contains(GameDesignConstants.ALL_BODY_PARTS_LIST[index]))
            {
                valid = true;
            }
        }
        return index;
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
        InstantiateCreatureBody(newPlayerCreature, playerCreatureData, true);
        PlayerManager.Instance.PlayerGameObject = newPlayerCreature;
        playerCreatureData.CalculateStats();

        return newPlayerCreature;
    }


    public void InstantiateCreatureBody(GameObject creatureObject, CreatureData creatureData, bool isPlayer = false)
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
            GameObject shapeobj = InstantiateBodyShape(name, creatureObject, shape, isPlayer);
            foreach (BodyPart bodyPart in shape.AttachedBodyParts)
            {
                //Instantiate body part
                GameObject bodyPartObj = new GameObject(bodyPart.ToString());
                bodyPartObj.transform.parent = GetNextFreeBodyPartHolder(shapeobj.transform, out bool isLeft);
                bodyPartObj.transform.localPosition = Vector3.zero;
                bodyPartObj.AddComponent<BoxCollider2D>();
                SpriteRenderer bodyPartSpriteRenderer = bodyPartObj.AddComponent<SpriteRenderer>();
                bodyPartSpriteRenderer.sprite = GameAssets.Instance.GetBodyPartByName(bodyPart.Name);
                bodyPartSpriteRenderer.sortingOrder = 3;
                if (isLeft)
                {
                    bodyPartObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                BodyPartData bodyPartData = bodyPartObj.AddComponent<BodyPartData>();
                bodyPartData.BodyPart = bodyPart;
            }
        }
        InstantiateEyes(creatureObject, isPlayer);
        InstantiateLegs(creatureObject, isPlayer);
    }


    private GameObject InstantiateBodyShape(string name, GameObject creatureObject, BodyShape shape, bool isPlayer= false)
    {
        GameObject shapeobj = new GameObject(name);
        shapeobj.transform.parent = creatureObject.transform;
        SpriteRenderer spriteRenderer = shapeobj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameAssets.Instance.GetBodyShapeSpriteByName(name, creatureObject.transform.childCount - 1);
        if (isPlayer)
        {
            spriteRenderer.flipX = true;
        }
        spriteRenderer.sortingOrder = 1;
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

    private void InstantiateEyes(GameObject creature, bool isPlayer= false)
    {
        GameObject eyes = new GameObject("Eyes");
        eyes.transform.SetParent(creature.transform.GetChild(0).transform);
        eyes.transform.localPosition = Vector3.zero;
        SpriteRenderer spriteRenderer = eyes.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 2;
        if (isPlayer)
        {
            spriteRenderer.sprite = GameAssets.Instance.GetPlayerEyesSpriteByName("Normal");
        }
        else
        {
            spriteRenderer.sprite = GameAssets.Instance.GetEnemyEyesSpriteByName("Normal");
        }
        
    }

    private void InstantiateLegs(GameObject creatureObject, bool isPlayer = false)
    {
        GameObject legs = new GameObject("Legs");
        SpriteRenderer spriteRenderer = legs.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameAssets.Instance.LegsSprite;
        spriteRenderer.sortingOrder = 0;
        if (isPlayer)
        {
            spriteRenderer.flipX = true;
        }
        legs.transform.SetParent(creatureObject.transform);
        legs.transform.localPosition = CalculateShapeLocalPosition(creatureObject.transform);
    }

    private void InstantiateBossHead(GameObject boss)
    {
        GameObject head = new GameObject("BossHead");
        SpriteRenderer spriteRenderer = head.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameAssets.Instance.BossHead;
        head.transform.SetParent(boss.transform);
        head.transform.localPosition = new Vector3(0f, boss.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y * 2, 0f);
        head.transform.SetAsFirstSibling();
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
                case "FireNose":
                    allBodyParts.Add(new FireNose());
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
                case "Claw":
                    allBodyParts.Add(new Sword());
                    break;
                case "TentacleGrab":
                    allBodyParts.Add(new TentacleGrab());
                    break;
                case "Zap":
                    allBodyParts.Add(new Zap());
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
        if (numChild >= 5)
        {
            totalHeight -= creature.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y * creature.transform.GetChild(0).localScale.y;
        }

        return new Vector3(0f, totalHeight - creature.transform.GetChild(0).localScale.y - 0.7f, 0f);
    }

    private Vector3 CalculateShapeLocalPosition(Transform parent)
    {
        float totalHeight = 0;
        if (parent.childCount == 2)
        {
            totalHeight = parent.GetChild(1).GetComponent<Renderer>().bounds.size.y * parent.GetChild(1).localScale.y;
        }
        else
        {
            for (int i = 1; i < parent.childCount; i++)
            {
                totalHeight += parent.GetChild(i).GetComponent<Renderer>().bounds.size.y * parent.GetChild(i).localScale.y;
                //totalHeight -= 0.2f;
            }
        }
        return new Vector3(0f, -totalHeight, 0f);
    }

    public BodyPart GetNewBodyPartFromName(string name)
    {
        switch (name)
        {
            case "BigBonker":
                return new BigBonker();
            case "BuffedBicep":
                return new BuffedBicep();
            case "FireNose":
                return new FireNose();
            case "LuckyDice":
                return new LuckyDice();
            case "MonkeyPaw":
                return new MonkeyPaw();
            case "PoisonousSpike":
                return new PoisonousSpike();
            case "SimpleAttack":
                return new SimpleAttack();
            case "Claw":
                return new Sword();
            case "TentacleGrab":
                return new TentacleGrab();
            case "Zap":
                return new Zap();
            default:
                return null;
        }
    }
}

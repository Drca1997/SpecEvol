using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureGenerator : MonoBehaviour
{

    private static CreatureGenerator _instance;
    public static CreatureGenerator Instance { get { return _instance; } }

    public const int STARTING_HEALTH = 50;
    public const int STARTING_SPEED = 50;
    public const int STARTING_LUCK = 50;

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

    private GameObject CreateRandomCreature()
    {
        GameObject enemy = new GameObject("Enemy");
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
        creatureData.MaximumHealth = Random.Range(50, 150);
        creatureData.MaximumSpeed = Random.Range(10, 90);
        creatureData.MaximumLuck = Random.Range(10, 90);
        return enemy;
    }

    public GameObject CreateNewPlayerCreature(GameObject playerBasePrefab)
    {
        GameObject newPlayerCreature = Instantiate(playerBasePrefab);
        CreatureData playerCreatureData = newPlayerCreature.AddComponent<CreatureData>();
        playerCreatureData.MaximumHealth = STARTING_HEALTH;
        playerCreatureData.MaximumSpeed = STARTING_SPEED;
        playerCreatureData.MaximumLuck = STARTING_LUCK;

        BodyData playerBodyData = newPlayerCreature.AddComponent<BodyData>();
        playerBodyData.BodyParts = new List<IBodyPart>();
        playerBodyData.BodyShapes = new List<BodyShape>();
        //TO DO: instantiate shape that player picked at the start
        return newPlayerCreature;
    }

}

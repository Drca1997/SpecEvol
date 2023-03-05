using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private GameObject CreateRandomCreature()
    {
        GameObject enemy = new GameObject("Enemy");
        CreatureData creatureData = enemy.AddComponent<CreatureData>();
        creatureData.MaximumHealth = Random.Range(50, 150);
        creatureData.MaximumSpeed = Random.Range(10, 90);
        creatureData.MaximumLuck = Random.Range(10, 90);
        return enemy;
    }

    public GameObject CreateNewPlayerCreature(GameObject playerBasePrefab, Enums.BodyShape initialBodyShape)
    {
        GameObject newPlayerCreature = Instantiate(playerBasePrefab);
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
        //TO DO: instantiate default starting arm
        return newPlayerCreature;
    }

}

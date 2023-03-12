using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public BattleState battleState;
    [SerializeField]
    private Transform playerBattleStationPosition;
    [SerializeField]
    private Transform enemyBattleStationPosition;
    [SerializeField]
    private DefeatedArmsSO defeatedArms; 
    [SerializeField]
    private LevelSO levelSO;
    private List<GameObject> battleParticipants; //ex: 0 -> Player, 1 - Enemy
    private List<int> turnOrder; // elementos indicam qual participante �: battleParticipants[i]
    private List<int> battleParticipantsSpeed;
    private List<int> initiative;
    public static event EventHandler OnPlayerTurn;
    public static event EventHandler OnEnemyTurn;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public List<GameObject> BattleParticipants { get => battleParticipants; set => battleParticipants = value; }

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
        PlayerManager.Instance.CreatePlayer(playerBattleStationPosition);
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupBattle()
    {
        battleState = BattleState.START;
        battleParticipants = new List<GameObject>();    
        battleParticipants.Add(PlayerManager.Instance.PlayerGameObject);
        battleParticipants.AddRange(InstantiateBattleParticipants());
        battleParticipantsSpeed = battleParticipants.Select(p => p.GetComponent<CreatureData>().MaximumSpeed).ToList();
        initiative = Enumerable.Repeat(0, battleParticipantsSpeed.Count).ToList();
        turnOrder = TurnManager.StartBattleTurns(battleParticipantsSpeed, initiative);

        NextTurn(true);
    }

    private List<GameObject> InstantiateBattleParticipants()
    {
        List<GameObject> spawnedEnemies = CreatureGenerator.Instance.InstantiateEnemies(1);
        foreach(GameObject enemy in spawnedEnemies)
        {
            enemy.transform.parent = enemyBattleStationPosition;
            enemy.transform.localPosition = CreatureGenerator.Instance.GetCreatureSpawnPosition(enemy);
        }
        return spawnedEnemies;
    }

    private BattleState CalculateBattleState()
    {
         return turnOrder[0] == 0 ? BattleState.PLAYER_TURN : BattleState.ENEMY_TURN;
    }

    private void NextTurn(bool firstTurn= false)
    {
        if (!firstTurn)
        {
            turnOrder.RemoveAt(0);
        }
        if (turnOrder.Count == 0)
        {
            turnOrder = TurnManager.CalculateMoreTurns(battleParticipantsSpeed, initiative);
        }
        battleState = CalculateBattleState();
        if (battleState == BattleState.PLAYER_TURN)
        {
            StartCoroutine(PlayerTurn());
        }
        else if (battleState == BattleState.ENEMY_TURN)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("PLAYER TURN...");
        OnPlayerTurn?.Invoke(this, new EventArgs());
        yield return new WaitForSeconds(1);
        battleParticipants[1].GetComponent<HealthSystem>().ChangeHealth(-20);
        NextTurn();
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("ENEMY TURN..");
        OnEnemyTurn?.Invoke(this, new EventArgs());
        yield return new WaitForSeconds(1);
        NextTurn();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    public void BattleVictory()
    {
        Debug.Log("VICTORY");
        GetRandomBodyPartChoicesFromFallenEnemy();
        levelSO.level += 1;
        SceneManager.LoadScene("MutationScene");
    }

    private void GetRandomBodyPartChoicesFromFallenEnemy()
    {
        List<BodyPart> allBodyParts = battleParticipants[1].GetComponent<CreatureData>().GetBodyParts();
        List<string> differentBodyPartsNames = new List<string>();
        foreach(BodyPart bodyPart in allBodyParts)
        {
            if (!differentBodyPartsNames.Contains(bodyPart.Name))
            {
                differentBodyPartsNames.Add(bodyPart.Name);
            }
        }
        int option1Index = Random.Range(0, differentBodyPartsNames.Count);
        defeatedArms.option1 = differentBodyPartsNames[option1Index];
        if (differentBodyPartsNames.Count > 1)
        {
            bool valid = false;
            int option2Index = -1;
            while (!valid)
            {
                option2Index = Random.Range(0, differentBodyPartsNames.Count);
                if (option2Index == option1Index)
                {
                    continue;
                }
                else
                {
                    valid = true;
                }
            }
            defeatedArms.option2 = differentBodyPartsNames[option2Index];
        }
    }
}

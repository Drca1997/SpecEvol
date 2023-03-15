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
    private List<int> turnOrder; // elementos indicam qual participante é: battleParticipants[i]
    private List<int> battleParticipantsSpeed;
    private List<int> initiative;
    public static event EventHandler OnPlayerTurn;
    public static event EventHandler OnEnemyTurn;
    public static event EventHandler OnUpdateTurnOrder;
    private bool actionChosen = false;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public List<GameObject> BattleParticipants { get => battleParticipants; set => battleParticipants = value; }
    public List<int> TurnOrder { get => turnOrder; set => turnOrder = value; }

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
        BattleUIManager.OnActionChosen += OnActionChosen;
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupBattle()
    {
        initiative = new List<int>();
        battleState = BattleState.START;
        battleParticipants = new List<GameObject>();    
        battleParticipants.Add(PlayerManager.Instance.PlayerGameObject);
        battleParticipants.AddRange(InstantiateBattleParticipants());
        battleParticipantsSpeed = battleParticipants.Select(p => p.GetComponent<CreatureData>().MaximumSpeed).ToList();
        initiative = Enumerable.Repeat(0, battleParticipantsSpeed.Count).ToList();
        turnOrder = TurnManager.StartBattleTurns(battleParticipantsSpeed, initiative);
        OnUpdateTurnOrder?.Invoke(this, EventArgs.Empty);
        NextTurn(true);
    }

    private List<GameObject> InstantiateBattleParticipants()
    {
        List<GameObject> spawnedEnemies = CreatureGenerator.Instance.InstantiateEnemies(1, levelSO.level);
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
            OnUpdateTurnOrder?.Invoke(this, EventArgs.Empty);
        }
        if (turnOrder.Count <= 10)
        {
            turnOrder = TurnManager.CalculateMoreTurns(battleParticipantsSpeed, initiative);
            OnUpdateTurnOrder?.Invoke(this, EventArgs.Empty);
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
        actionChosen = false;
        OnPlayerTurn?.Invoke(this, EventArgs.Empty);
        while (!actionChosen)
        {
            yield return null;

        }
        PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().UpdateStatus();
        NextTurn();
    }

    private void OnActionChosen(object sender, EventArgs e)
    {
        actionChosen = true;
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("ENEMY TURN..");
        OnEnemyTurn?.Invoke(this, EventArgs.Empty);
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
        PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().ResetBodyPartsStatus();
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

    public void RecalculateTurnOrder()
    {
        battleParticipantsSpeed.Clear();
        initiative.Clear();
        turnOrder.Clear();
        battleParticipantsSpeed = battleParticipants.Select(p => p.GetComponent<CreatureData>().CurrentSpeed).ToList();
        initiative = Enumerable.Repeat(0, battleParticipantsSpeed.Count).ToList();
        turnOrder = TurnManager.StartBattleTurns(battleParticipantsSpeed, initiative);
        OnUpdateTurnOrder?.Invoke(this, EventArgs.Empty);
        return;
    }
}

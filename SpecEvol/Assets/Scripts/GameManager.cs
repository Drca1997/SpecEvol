using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class GameManager : MonoBehaviour
{
    public BattleState battleState;
    [SerializeField]
    private Transform playerBattleStationPosition;
    [SerializeField]
    private Transform enemyBattleStationPosition;
    private List<GameObject> battleParticipants; //ex: 0 -> Player, 1 - Enemy
    private List<int> turnOrder; // elementos indicam qual participante é: battleParticipants[i]
    private List<int> battleParticipantsSpeed;
    private List<int> initiative;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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
            Transform lastShape = enemy.transform.GetChild(enemy.transform.childCount - 1);
            enemy.transform.localPosition = new Vector3(0f, -lastShape.GetComponent<Renderer>().bounds.min.y, 0f);
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
        yield return new WaitForSeconds(1);

        NextTurn();
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("ENEMY TURN..");
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
        SceneManager.LoadScene("MutationScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MutationStateManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCreatureSO m_Creature;
    [SerializeField]
    private GameObject playerBasePrefab;
    [SerializeField]
    private GameObject gameAssets;

    private Enums.BodyShape initialBodyShape;

    public Enums.BodyShape InitialBodyShape { get => initialBodyShape; set => initialBodyShape = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameAssets);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBodyShapeSelected()
    {
        GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab, InitialBodyShape);
        SavePlayerCreature(newPlayer);
        SceneManager.LoadScene("BattleScene");
    }

    private void SavePlayerCreature(GameObject player)
    {
        CreatureData creatureData = player.GetComponent<CreatureData>();
        m_Creature.maximumHealth = creatureData.MaximumHealth;
        m_Creature.maximumSpeed = creatureData.MaximumSpeed;
        m_Creature.maximumLuck = creatureData.MaximumLuck;
        m_Creature.bodyShapes = creatureData.BodyShapes;
    }
}

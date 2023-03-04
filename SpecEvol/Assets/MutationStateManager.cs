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
    // Start is called before the first frame update
    void Start()
    {
        GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab);
        SavePlayerCreature(newPlayer);
        SceneManager.LoadScene("BattleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SavePlayerCreature(GameObject player)
    {
        CreatureData creatureData = player.GetComponent<CreatureData>();
        m_Creature.maximumHealth = creatureData.MaximumHealth;
        m_Creature.maximumSpeed = creatureData.MaximumSpeed;
        m_Creature.maximumLuck = creatureData.MaximumLuck;

        BodyData bodyData = player.GetComponent<BodyData>();
        m_Creature.bodyParts = bodyData.BodyParts;
        m_Creature.bodyShapes = bodyData.BodyShapes;
    }
}

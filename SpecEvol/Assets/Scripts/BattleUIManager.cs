using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject actionButtonPrefab;
    [SerializeField]
    private Transform scrollViewContent;



    // Start is called before the first frame update
    void Start()
    {
        InstantiateActionButtons();
        GameManager.OnPlayerTurn += OnPlayerTurn;
        GameManager.OnEnemyTurn += OnEnemyTurn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateActionButtons()
    {
        List<BodyPart> bodyParts = PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().GetBodyParts();
        foreach(BodyPart bodyPart in bodyParts)
        {
            GameObject newButton = Instantiate(actionButtonPrefab);
            newButton.transform.parent = scrollViewContent;
            newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(bodyPart.ActionName);
            newButton.GetComponent<Button>().onClick.AddListener(() => bodyPart.Execute(PlayerManager.Instance.PlayerGameObject, GameManager.Instance.BattleParticipants[1]));
        }
    }

    private void OnPlayerTurn(object sender, EventArgs e)
    {
        ToggleScrollView(true);
    } 
    
    private void OnEnemyTurn(object sender, EventArgs e)
    {
        ToggleScrollView(false);
    }

    private void ToggleScrollView(bool toggleValue)
    {
        scrollViewContent.gameObject.SetActive(toggleValue);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject actionButtonPrefab;
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private Transform turnOrder;

    public static event EventHandler OnActionChosen;

    private void Awake()
    {
        GameManager.OnUpdateTurnOrder += SetTurnOrder;
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateActionButtons();
        GameManager.OnPlayerTurn += OnPlayerTurn;
        GameManager.OnEnemyTurn += OnEnemyTurn;
        Sword.OnCut += OnSwordCut;
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
            newButton.GetComponent<Button>().onClick.AddListener(OnActionChosenClick);
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
        if (scrollViewContent != null)
        {
            scrollViewContent.gameObject.SetActive(toggleValue);
        }
    }

    private void OnActionChosenClick()
    {
        OnActionChosen?.Invoke(this, EventArgs.Empty);
    }

    private void SetTurnOrder(object sender, EventArgs e)
    {
        if(turnOrder != null)
        {

            for(int i = 0; i < turnOrder.childCount; i++)
            {
                if (GameManager.Instance.TurnOrder[i] == 0)
                {
                    turnOrder.GetChild(i).GetComponent<Image>().sprite = GameAssets.Instance.CreaturesIcons[0];
                }
                else if (GameManager.Instance.TurnOrder[i] == 1)
                {
                    turnOrder.GetChild(i).GetComponent<Image>().sprite = GameAssets.Instance.CreaturesIcons[1];
                }
            }
        }
    }

    private void OnSwordCut(object sender, Sword.OnCutArgs args)
    {
        //da bug aqui
        scrollViewContent.transform.GetChild(args.cutBodyPartIndex).GetComponentInChildren<TextMeshProUGUI>().text = "Disabled (Cut By Claw)";
        scrollViewContent.transform.GetChild(args.cutBodyPartIndex).GetComponent<Button>().enabled = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject actionButtonPrefab;
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private Transform turnOrder;
    [SerializeField]
    private GameObject infoPanelPrefab;

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
        FireNose.OnFirePart += OnFire;
        CreatureData.OnFireEnd += OnFireReset;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateActionButtons()
    {
        List<BodyPart> bodyParts = PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().GetBodyParts();
        foreach (BodyPart bodyPart in bodyParts)
        {
            GameObject newButton = Instantiate(actionButtonPrefab);
            newButton.transform.parent = scrollViewContent;
            newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(bodyPart.ActionName);
            newButton.GetComponent<Button>().onClick.AddListener(() => bodyPart.Execute(PlayerManager.Instance.PlayerGameObject, GameManager.Instance.BattleParticipants[1]));
            newButton.GetComponent<Button>().onClick.AddListener(OnActionChosenClick);
            BodyPartData bodyPartData = newButton.AddComponent<BodyPartData>();
            bodyPartData.BodyPart = bodyPart;
            UIButtonOnhover script = newButton.AddComponent<UIButtonOnhover>();
            script.InfoPanelPrefab = infoPanelPrefab;
            script.garbage = GameManager.Instance.Garbage;
            newButton.AddComponent<OnHoverTrigger>();
            newButton.AddComponent<BodyPartOnFire>();
        }
        GameObject doNothingButton = Instantiate(actionButtonPrefab);
        doNothingButton.transform.parent = scrollViewContent;
        doNothingButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Do Nothing");
        doNothingButton.GetComponent<Button>().onClick.AddListener(OnActionChosenClick);
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
        if (turnOrder != null)
        {

            for (int i = 0; i < turnOrder.childCount; i++)
            {
                if (i < GameManager.Instance.TurnOrder.Count)
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
                else
                {
                    turnOrder.GetChild(i).GetComponent<Image>().sprite = null;
                }
            }
        }
    }

    private void OnSwordCut(object sender, Sword.OnCutArgs args)
    {
        scrollViewContent.transform.GetChild(args.cutBodyPartIndex).GetComponentInChildren<TextMeshProUGUI>().text = "Disabled (Cut By Claw)";
        scrollViewContent.transform.GetChild(args.cutBodyPartIndex).GetComponent<Button>().enabled = false;
    }

    private void OnFire(object sender, FireNose.OnFireArgs args)
    {
        scrollViewContent.transform.GetChild(args.bodyPartIndex).GetComponent<BodyPartOnFire>().SetOnFireStatus(true);
    }

    private void OnFireReset(object sender, CreatureData.OnFireEndArgs args)
    {
        scrollViewContent.transform.GetChild(args.bodyPartIndex).GetComponent<BodyPartOnFire>().SetOnFireStatus(false);
    }

    
}

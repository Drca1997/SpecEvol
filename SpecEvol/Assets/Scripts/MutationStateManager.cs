using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class MutationStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBasePrefab;
    [SerializeField]
    private GameObject gameAssets;
    [SerializeField]
    private Transform placeHolder;
    [SerializeField]
    private GameObject playerManagerObject;
    [SerializeField]
    private LevelSO levelSO;
    [SerializeField]
    private DefeatedArmsSO defeatedArms;
    [SerializeField]
    private GameObject playerPlaceHolder;
    [Header("UI References")]
    [SerializeField]
    private GameObject initialBodyShapeChoice;
    [SerializeField]
    private GameObject mutationMenu;
    [SerializeField]
    private GameObject keepCurrentMenu;
    [SerializeField]
    private TextMeshProUGUI replaceLabel;
    [SerializeField]
    private GameObject battleButton;
    [SerializeField]
    private GameObject statsMenu;
    [SerializeField]
    private TextMeshProUGUI healthValue;
    [SerializeField]
    private TextMeshProUGUI speedValue;
    [SerializeField]
    private TextMeshProUGUI damageBonusValue;


    private Enums.BodyShape choosenBodyShape;

    public Enums.BodyShape ChoosenBodyShape { get => choosenBodyShape; set => choosenBodyShape = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameAssets);
        DontDestroyOnLoad(playerManagerObject);
        if (PlayerManager.Instance.PlayerCreature.IsInitialized())
        {
            PlayerManager.Instance.CreatePlayer(placeHolder.transform);
            SetStatsMenuActive();
            SetMutationMenuActive();
            if (IsBodyShapeLevelUpTime())
            {
                OnBodyShapeLevelUp();
                
            }
        }
        else
        {
            initialBodyShapeChoice.SetActive(true);
        }
    }

    

    private void OnBodyShapeLevelUp()
    {
        initialBodyShapeChoice.SetActive(true);
        mutationMenu.SetActive(false);
        playerPlaceHolder.SetActive(false);
    }

    public void OnBodyShapeSelected()
    {
        if (levelSO.level <= 0) 
        {
            GameObject newPlayer = CreatureGenerator.Instance.CreateNewPlayerCreature(playerBasePrefab, ChoosenBodyShape, placeHolder.transform.position);
            newPlayer.transform.parent = placeHolder.transform;
            Vector3 oldScale = newPlayer.transform.localScale;
            newPlayer.transform.localScale = Vector3.one;
            for (int i = 0; i < newPlayer.transform.childCount; i++)
            {
                newPlayer.transform.GetChild(i).transform.localScale = oldScale;
            }
            newPlayer.transform.localPosition = CreatureGenerator.Instance.GetCreatureSpawnPosition(newPlayer);
            newPlayer.transform.GetChild(newPlayer.transform.childCount - 1).localPosition -= new Vector3(0f, 1.2f, 0f);
            PlayerManager.Instance.SavePlayerCreature(newPlayer);
            battleButton.SetActive(true);
        }
        else
        {
            switch (choosenBodyShape)
            {
                case Enums.BodyShape.SQUARE:
                    PlayerManager.Instance.UpdatePlayerShape("SquareBodyShape");
                    break;
                case Enums.BodyShape.CIRCLE:
                    PlayerManager.Instance.UpdatePlayerShape("CircleBodyShape");
                    break;
                case Enums.BodyShape.TRIANGLE:
                    PlayerManager.Instance.UpdatePlayerShape("TriangleBodyShape");
                    break;
                default:
                    break;
            }
            choosenBodyShape = Enums.BodyShape.UNDEFINED;
            SetMutationMenuActive();
        }
        SetStatsMenuActive();
    }

    public void OnNextBattleButton()
    {
        PlayerManager.Instance.SavePlayerCreature(PlayerManager.Instance.PlayerGameObject);
        SceneManager.LoadScene("BattleScene");
    }

    private bool IsBodyShapeLevelUpTime()
    {
        if (levelSO.level % 3 == 0 && levelSO.level < 9)
        {
            int levelUpSound = Random.Range(1, 3);
            AudioManager.instance.Play("LevelUp" + levelUpSound.ToString());
            return true;
        }
        return false;
    }

    public void OnSquareSelect()
    {
        Debug.Log("SQUARE");
        initialBodyShapeChoice.SetActive(false);
        playerPlaceHolder.SetActive(true);
        ChoosenBodyShape = Enums.BodyShape.SQUARE;
        OnBodyShapeSelected();
    }
    public void OnCircleSelect()
    {
        Debug.Log("CIRCLE");
        initialBodyShapeChoice.SetActive(false);
        playerPlaceHolder.SetActive(true);
        
        ChoosenBodyShape = Enums.BodyShape.CIRCLE;
        OnBodyShapeSelected();
    }

    public void OnTriangleSelect()
    {
        Debug.Log("TRIANGLE");
        initialBodyShapeChoice.SetActive(false);
        playerPlaceHolder.SetActive(true);
        ChoosenBodyShape = Enums.BodyShape.TRIANGLE;
        OnBodyShapeSelected();
    }


    public void OnBodyPart1()
    {
        AudioManager.instance.Play("MenuItemSelect");
        battleButton.SetActive(true);
        mutationMenu.SetActive(false);
        PlayerManager.Instance.UpdatePlayerMorphology(mutationMenu.transform.GetChild(0).GetComponent<Image>().sprite.name);
    }

    public void OnBodyPart2()
    {
        AudioManager.instance.Play("MenuItemSelect");
        battleButton.SetActive(true);
        mutationMenu.SetActive(false);
        PlayerManager.Instance.UpdatePlayerMorphology(mutationMenu.transform.GetChild(1).GetComponent<Image>().sprite.name);
    }

    public void OnKeepCurrent()
    {
        AudioManager.instance.Play("MenuItemSelect");
        battleButton.SetActive(true);
        mutationMenu.SetActive(false);
    }

    public void SetMutationMenuActive()
    {
        playerPlaceHolder.SetActive(true);
        mutationMenu.SetActive(true);
        
        if (PlayerManager.Instance.HasFreeSlot())
        {
            keepCurrentMenu.SetActive(false);
            replaceLabel.SetText("Choose New Body Part:");
        }
        Assert.IsTrue(defeatedArms.option1 != null);
        Assert.IsTrue(defeatedArms.option2 != null);
        mutationMenu.transform.GetChild(0).GetComponent<Image>().sprite = GameAssets.Instance.GetBodyPartByName(defeatedArms.option1);
        mutationMenu.transform.GetChild(1).GetComponent<Image>().sprite = GameAssets.Instance.GetBodyPartByName(defeatedArms.option2);
    }

    private void SetStatsMenuActive()
    {
        statsMenu.SetActive(true);
        healthValue.SetText(PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().MaximumHealth.ToString());
        speedValue.SetText(PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().MaximumSpeed.ToString());
        damageBonusValue.SetText("+" + PlayerManager.Instance.PlayerGameObject.GetComponent<CreatureData>().GetSquareDamageBonus().ToString());
    
    }

    

}

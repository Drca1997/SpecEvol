using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject initialBodyShapeChoice;
    [SerializeField]
    private GameObject mutationMenu;
    [SerializeField]
    private GameObject battleButton;
    [SerializeField]
    private DefeatedArmsSO defeatedArms;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerManager.Instance.PlayerCreature.IsInitialized()) 
        {
            initialBodyShapeChoice.SetActive(true);
        }
        else
        {
            SetMutationMenuActive();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSquareSelect()
    {
        Debug.Log("SQUARE");
        initialBodyShapeChoice.SetActive(false);
        battleButton.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.SQUARE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }
    public void OnCircleSelect()
    {
        Debug.Log("CIRCLE");
        initialBodyShapeChoice.SetActive(false);
        battleButton.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.CIRCLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

    public void OnTriangleSelect()
    {
        Debug.Log("TRIANGLE");
        initialBodyShapeChoice.SetActive(false);
        battleButton.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.TRIANGLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

    public void OnBodyPart1()
    {
        battleButton.SetActive(true);
        mutationMenu.SetActive(false);
        PlayerManager.Instance.UpdatePlayerMorphology(mutationMenu.transform.GetChild(0).GetComponent<Image>().sprite.name);
    }

    public void OnBodyPart2()
    {
        battleButton.SetActive(true);
        mutationMenu.SetActive(false);
        PlayerManager.Instance.UpdatePlayerMorphology(mutationMenu.transform.GetChild(mutationMenu.transform.childCount - 1).GetComponent<Image>().sprite.name);
    }

    public void SetMutationMenuActive()
    {
        mutationMenu.SetActive(true);
        Assert.IsTrue(defeatedArms.option1 != null);
        Assert.IsTrue(defeatedArms.option2 != null);
        mutationMenu.transform.GetChild(0).GetComponent<Image>().sprite = GameAssets.Instance.GetBodyPartByName(defeatedArms.option1);
        mutationMenu.transform.GetChild(mutationMenu.transform.childCount - 1).GetComponent<Image>().sprite = GameAssets.Instance.GetBodyPartByName(defeatedArms.option2);
    }

}

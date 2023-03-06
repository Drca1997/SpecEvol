using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject initialBodyShapeChoice;
    [SerializeField]
    private GameObject mutationMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerManager.Instance.PlayerCreature.IsInitialized()) 
        {
            initialBodyShapeChoice.SetActive(true);
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
        mutationMenu.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.SQUARE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }
    public void OnCircleSelect()
    {
        Debug.Log("CIRCLE");
        initialBodyShapeChoice.SetActive(false);
        mutationMenu.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.CIRCLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

    public void OnTriangleSelect()
    {
        Debug.Log("TRIANGLE");
        initialBodyShapeChoice.SetActive(false);
        mutationMenu.SetActive(true);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.TRIANGLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

}

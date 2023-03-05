using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject initialBodyShapeChoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSquareSelect()
    {
        Debug.Log("SQUARE");
        initialBodyShapeChoice.SetActive(false);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.SQUARE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }
    public void OnCircleSelect()
    {
        Debug.Log("CIRCLE");
        initialBodyShapeChoice.SetActive(false);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.CIRCLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

    public void OnTriangleSelect()
    {
        Debug.Log("TRIANGLE");
        initialBodyShapeChoice.SetActive(false);
        GetComponent<MutationStateManager>().InitialBodyShape = Enums.BodyShape.TRIANGLE;
        GetComponent<MutationStateManager>().OnBodyShapeSelected();
    }

}

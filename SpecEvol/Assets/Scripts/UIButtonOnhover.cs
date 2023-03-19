using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonOnhover : MonoBehaviour
{
    [SerializeField]
    private GameObject infoPanelPrefab;

    private GameObject uiInfoPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseEnter(BodyPartData bodyPartData)
    {
        Debug.Log("ENTER");
        BodyPart bodyPart = bodyPartData.BodyPart;
        uiInfoPanel = Instantiate(infoPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        uiInfoPanel.GetComponent<InfoPanel>().SetInfo(bodyPart.Name, bodyPart.Damage, bodyPart.Accuracy, bodyPart.Description, bodyPart.FlavorText);
    }

    public void MouseExit()
    {
        Debug.Log("Exit");
        Destroy(uiInfoPanel);
    }
}

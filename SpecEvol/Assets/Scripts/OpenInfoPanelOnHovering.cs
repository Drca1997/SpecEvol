using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OpenInfoPanelOnHovering : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject infoPanelPrefab;

    private GameObject currentInfoPanel;
    private float windowOriginX;
    private float windowOriginY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            BodyPartData bodyPartData = hit.collider.GetComponent<BodyPartData>();
            if (bodyPartData != null && currentInfoPanel == null)
            {
                Renderer renderer = bodyPartData.GetComponent<Renderer>();
                
                windowOriginX = renderer.bounds.size.x * bodyPartData.transform.localScale.x * 2;
                windowOriginY = renderer.bounds.size.y * bodyPartData.transform.localScale.y;
                BodyPart bodyPart = bodyPartData.BodyPart;
                Vector3 panelOrigin;
                if (hit.collider.transform.parent.parent.parent.gameObject == PlayerManager.Instance.PlayerGameObject)
                {
                    panelOrigin = new Vector3(hit.collider.transform.position.x + windowOriginX, hit.collider.transform.position.y + windowOriginY, hit.collider.transform.position.z);
                }
                else
                {
                    panelOrigin = new Vector3(hit.collider.transform.position.x - windowOriginX, hit.collider.transform.position.y + windowOriginY, hit.collider.transform.position.z);
                }
                currentInfoPanel = Instantiate(infoPanelPrefab, panelOrigin, Quaternion.identity, hit.collider.transform);
                currentInfoPanel.GetComponent<InfoPanel>().SetInfo(bodyPart.Name, bodyPart.Damage, bodyPart.Accuracy, bodyPart.Description, bodyPart.FlavorText);
            }
        }
        else
        {
            if (currentInfoPanel != null)
            {
                Destroy(currentInfoPanel);
            }
        }
    }
}

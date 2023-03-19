using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<UIButtonOnhover>().MouseEnter(GetComponent<BodyPartData>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<UIButtonOnhover>().MouseExit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaking : MonoBehaviour
{
    private List<BodyPart> allBodyParts;

    // Start is called before the first frame update
    void Start()
    {
        allBodyParts = GetComponent<CreatureData>().GetBodyParts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAction()
    {
        int action = Random.Range(0, allBodyParts.Count);
        allBodyParts[action].Execute(gameObject, PlayerManager.Instance.PlayerGameObject);
    }
}

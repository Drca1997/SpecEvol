using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_Transform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject creature = CreatureGenerator.Instance.CreateRandomCreature();
        creature.transform.parent = m_Transform;
        int numChild = creature.transform.childCount;
        Transform lastChild = creature.transform.GetChild(numChild - 1);
        Debug.Log(creature.transform.GetChild(0).GetComponent<Renderer>().bounds.size);
        Debug.Log(lastChild.GetComponent<Renderer>().bounds.size);
        if (numChild == 1)
        {
            creature.transform.localPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y, 0f);
        }
        else
        {
            creature.transform.localPosition = new Vector3(0f, lastChild.GetComponent<Renderer>().bounds.size.y * (numChild * 2), 0f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

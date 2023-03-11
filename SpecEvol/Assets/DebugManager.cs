using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatureGenerator.Instance.CreateRandomCreature();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

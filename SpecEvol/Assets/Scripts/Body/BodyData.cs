using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyData : MonoBehaviour
{
    List<IBodyPart> bodyParts;
    List<BodyShape> bodyShapes;
    public List<IBodyPart> BodyParts { get => bodyParts; set => bodyParts = value; }
    public List<BodyShape> BodyShapes { get => bodyShapes; set => bodyShapes = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

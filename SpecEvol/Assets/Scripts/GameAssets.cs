using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance { get { return _instance; } }

    [SerializeField]
    private Sprite[] bodyShapeSprites; 

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Sprite GetBodyShapeSpriteByName(string bodyShapeName)
    {
        Sprite sprite = null;
        if (bodyShapeName.Equals("SquareBodyShape"))
        {
            sprite = bodyShapeSprites[0];
        }
        else if (bodyShapeName.Equals("CircleBodyShape"))
        {
            sprite = bodyShapeSprites[1];
        }
        else if (bodyShapeName.Equals("TriangleBodyShape"))
        {
            sprite = bodyShapeSprites[2];
        }
        return sprite;
    }
    
}

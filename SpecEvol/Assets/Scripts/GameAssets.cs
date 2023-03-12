using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance { get { return _instance; } }


    [SerializeField]
    private Sprite[] bodyShapeSprites;

    [SerializeField]
    private Sprite[] bodyPartSprites;

    [SerializeField]
    private Sprite [] creaturesIcons;
    public Sprite[] CreaturesIcons { get => creaturesIcons;  }

    private Dictionary<string, Sprite> bodyPartSpritesDict;


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
        bodyPartSpritesDict = new Dictionary<string, Sprite>();
        foreach(Sprite sprite in bodyPartSprites)
        {
            bodyPartSpritesDict[sprite.name] = sprite;
        }
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

    public Sprite GetBodyPartByName(string name)
    {
        return bodyPartSpritesDict[name];
    }
    
}

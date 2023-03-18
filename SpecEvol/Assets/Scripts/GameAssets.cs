using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance { get { return _instance; } }


    [SerializeField]
    private Sprite[] squareBodyShapeSprites;
    [SerializeField]
    private Sprite[] circleBodyShapeSprites;
    [SerializeField]
    private Sprite[] triangleBodyShapeSprites;

    [SerializeField]
    private Sprite legsSprite;

    [SerializeField]
    private Sprite[] bodyPartSprites;

    [SerializeField]
    private Sprite [] creaturesIcons;

    [SerializeField]
    private GameObject damagePopupPrefab;

    [SerializeField]
    private Sprite[] playerEyesSprites;

    [SerializeField]
    private Sprite[] enemyEyesSprites;

    [SerializeField]
    private Sprite bossHead;
    public Sprite[] CreaturesIcons { get => creaturesIcons;  }
    public GameObject DamagePopupPrefab { get => damagePopupPrefab; set => damagePopupPrefab = value; }
    public Sprite LegsSprite { get => legsSprite; }
    public Sprite BossHead { get => bossHead; }

    private Dictionary<string, Sprite> bodyPartSpritesDict;
    private Dictionary<string, Sprite> playerEyesDict;
    private Dictionary<string, Sprite> enemyEyesDict;


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
        BuildAssetsDictionary(ref bodyPartSpritesDict, bodyPartSprites);
        BuildAssetsDictionary(ref playerEyesDict, playerEyesSprites);
        BuildAssetsDictionary(ref enemyEyesDict, enemyEyesSprites);
    }

    private void BuildAssetsDictionary(ref Dictionary<string, Sprite> dictionary, Sprite[] sprites)
    {
        dictionary = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in sprites)
        {
            dictionary[sprite.name] = sprite;
        }
    }

    public Sprite GetBodyShapeSpriteByName(string bodyShapeName, int index)
    {
        Sprite sprite = null;
        if (bodyShapeName.Equals("SquareBodyShape"))
        {
            sprite = squareBodyShapeSprites[index];
        }
        else if (bodyShapeName.Equals("CircleBodyShape"))
        {
            sprite = circleBodyShapeSprites[index];
        }
        else if (bodyShapeName.Equals("TriangleBodyShape"))
        {
            sprite = triangleBodyShapeSprites[index];
        }
        return sprite;
    }

    public Sprite GetBodyPartByName(string name)
    {
        return bodyPartSpritesDict[name];
    }
    
    public Sprite GetPlayerEyesSpriteByName(string name)
    {
        return playerEyesDict[name];
    }

    public Sprite GetEnemyEyesSpriteByName(string name)
    {
        return enemyEyesDict[name];
    }
}

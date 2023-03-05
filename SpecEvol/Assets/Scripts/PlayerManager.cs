using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCreatureSO playerCreature;
    [SerializeField]
    private GameObject playerBasePrefab;
    private GameObject playerGameObject;

    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

    public PlayerCreatureSO PlayerCreature { get => playerCreature; set => playerCreature = value; }
    public GameObject PlayerGameObject { get => playerGameObject; set => playerGameObject = value; }

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

    public void CreatePlayer(Transform playerSpawnPosition)
    {
        Assert.IsTrue(playerCreature != null);
        //playerGameObject = Instantiate(playerBasePrefab, Vector3.zero, Quaternion.Euler(0, 0, 0), playerSpawnPosition);
        playerGameObject = Instantiate(playerBasePrefab, playerSpawnPosition);
        CreatureData playerCreatureData = playerGameObject.AddComponent<CreatureData>();
        playerCreatureData.MaximumHealth = playerCreature.maximumHealth;
        playerCreatureData.MaximumSpeed = playerCreature.maximumSpeed;
        playerCreatureData.MaximumLuck = playerCreature.maximumLuck;
        playerCreatureData.BodyShapes = playerCreature.bodyShapes;

        foreach (BodyShape shape in playerCreatureData.BodyShapes)
        {
            string name = "";
            if(shape is SquareBodyShape)
            {
                name = "SquareBodyShape";
            }
            else if(shape is CircleBodyShape)
            {
                name = "CircleBodyShape";
            }
            else if (shape is TriangleBodyShape)
            {
                name = "TriangleBodyShape";
            }
            GameObject shapeobj = new GameObject(name);
            shapeobj.transform.parent = playerGameObject.transform;
            shapeobj.transform.localPosition = Vector3.zero;
            SpriteRenderer spriteRenderer = shapeobj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = GameAssets.Instance.GetBodyShapeSpriteByName(name);
            foreach (BodyPart bodyPart in shape.AttachedBodyParts)
            {
                //Instantiate body part
            }
        }
    }

}

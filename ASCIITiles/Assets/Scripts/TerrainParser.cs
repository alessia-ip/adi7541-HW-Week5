using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainParser : MonoBehaviour
{

    public float xOffset;
    public float yOffset;
    public static float tileSize = 0.15f;
    
    private const string DIR = "/ASCIIFiles/";
    [FormerlySerializedAs("FILE_NAME")] public string FILE_NAME_TERRAIN;
    private string PATH_TO_TERRAIN;
    public string FILE_NAME_OBJECTS;
    private string PATH_TO_OBJECTS;
    
    public GameObject instance;
    
    [Header("Terrain Prefabs")] 
    public GameObject grassT;
    public GameObject dirtT;
    public GameObject cobblestoneT;
    public GameObject sandT;
    public GameObject waterT;
    
    [Header("Object Prefabs")] 
    public GameObject playerT;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
        PATH_TO_TERRAIN = Application.dataPath + DIR + FILE_NAME_TERRAIN;
        PATH_TO_OBJECTS = Application.dataPath + DIR + FILE_NAME_OBJECTS;
        //Debug.Log(PATH_TO_TERRAIN);
        GetTerrain();
        GetObjects();
    }

    void GetTerrain()
    {
        //In this line we want to get all of the lines of the file, so we can iterate over them
        //This would not work if we saved it as a single line of text
        string[] terrain = File.ReadAllLines(PATH_TO_TERRAIN);

        for (int y = 0; y < terrain.Length; y++)
        {
            string currentLine = terrain[y]; //this is the current line of the ASCII file
            char[] characters = currentLine.ToCharArray(); //we want to get every character of that file

            for (int x = 0; x < characters.Length; x++)
            {
                GameObject newObj;
                char c = characters[x];

                switch (c)
                {
                    case 'g': //grass 
                        newObj = Instantiate<GameObject>(grassT);
                        break;
                    case 'c': //cobblestone
                        newObj = Instantiate<GameObject>(cobblestoneT);
                        break;
                    case 'd': //dirt
                        newObj = Instantiate<GameObject>(dirtT);
                        break;
                    case 's': //sand
                        newObj = Instantiate<GameObject>(sandT);
                        break;
                    case 'w': //water
                        newObj = Instantiate<GameObject>(waterT);
                        break;
                    default:
                        newObj = null;
                        break;
                }

                if (newObj != null)
                {
                    newObj.transform.position = new Vector2(x * tileSize + xOffset, -y * tileSize + yOffset);
                    newObj.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                
            }
        }
        
    }

    void GetObjects()
    {
        string[] objects = File.ReadAllLines(PATH_TO_OBJECTS);

        for (int y = 0; y < objects.Length; y++)
        {
            string currentLine = objects[y]; //this is the current line of the ASCII file
            char[] characters = currentLine.ToCharArray(); //we want to get every character of that file

            for (int x = 0; x < characters.Length; x++)
            {
                GameObject newObj;
                char c = characters[x];

                switch (c)
                {
                    case 'p': //player 
                        newObj = Instantiate<GameObject>(playerT);
                        break;
                    default:
                        newObj = null;
                        break;
                }

                if (newObj != null)
                {
                    newObj.transform.position = new Vector2(x * tileSize + xOffset, -y * tileSize + yOffset);
                    newObj.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                
            }
        }
    }

}

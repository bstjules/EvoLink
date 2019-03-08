using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelGenerator : MonoBehaviour
{

    public GameObject NodePrefab;
    public GameObject StrongNodePrefab;
    public GameObject TilePrefab;

    public GameObject NodeBucket;
    public GameObject TileBucket;

    public string LevelName;

   

    [System.Serializable]
    public class LevelInfo
    {
        public string name;
        public string difficulty;
        public int timer;
        public int gold;
        public int silver;
        public int bronze;
        
        public List<Node> nodes;
        public List<Tile> tiles;

        [System.Serializable]
        public class Node
        {
            public int id;
            public float xPos;
            public float yPos;
            public int nodeStrength;
        }

        [System.Serializable]
        public class Tile
        {
            public int id;
            public string title;
            public string extrainfo;
        }
        public static LevelInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<LevelInfo>(jsonString);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string jsonfilename = "Assets/" + LevelName + ".json";
        string dataAsJson = File.ReadAllText(jsonfilename);
        var levelobject = LevelInfo.CreateFromJSON(dataAsJson);


        for (int i = NodeBucket.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(NodeBucket.transform.GetChild(i).gameObject);
        }
        NodeBucket.transform.DetachChildren();

        for (int i = TileBucket.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(TileBucket.transform.GetChild(i).gameObject);
        }
        TileBucket.transform.DetachChildren();

        for (int i = 0; i < levelobject.nodes.Count; i++)
        {
            var placeposition = new Vector3(levelobject.nodes[i].xPos, levelobject.nodes[i].yPos, 1);
            GameObject newNode;
            if (levelobject.nodes[i].nodeStrength == 1)
            {
                newNode = Instantiate(NodePrefab, placeposition, Quaternion.identity, NodeBucket.transform);
            }
            else
            {
                newNode = Instantiate(StrongNodePrefab, placeposition, Quaternion.identity, NodeBucket.transform);
            }
           
        }
        for (int i = 0; i < levelobject.tiles.Count; i++)
        {
            var placeposition = new Vector3(i * 200 -390, -715, 1);
            GameObject newNode = Instantiate(TilePrefab, placeposition, Quaternion.identity, TileBucket.transform);
            newNode.GetComponentInChildren<TextMesh>().text = levelobject.tiles[i].title;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

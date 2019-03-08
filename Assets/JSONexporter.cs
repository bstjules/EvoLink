using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONexporter : MonoBehaviour
{
    public string Name;
    public string Difficulty;
    public int Timer;
    public int Gold;
    public int Silver;
    public int Bronze;

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

    public void ExportNodes()
    {
        var info = new LevelInfo();


        info.name = Name;
        info.difficulty = Difficulty;
        info.timer = Timer;
        info.gold = Gold;
        info.silver = Silver;
        info.bronze = Bronze;
        var nodes = GameObject.FindGameObjectsWithTag("Node");
        info.nodes = new List<LevelInfo.Node>();
        info.tiles = new List<LevelInfo.Tile>();

        var strongnodes = GameObject.FindGameObjectsWithTag("NodeStrong");
        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        int counter = 1;
        foreach (GameObject go in nodes)
        {
            var thisnode = new LevelInfo.Node();
            thisnode.xPos = go.transform.position.x;
            thisnode.yPos = go.transform.position.y;
            thisnode.id = counter;
            thisnode.nodeStrength = 1;
            info.nodes.Add(thisnode);
            counter++;
        }
        foreach (GameObject go in strongnodes)
        {
            var thisnode = new LevelInfo.Node();
            thisnode.xPos = go.transform.position.x;
            thisnode.yPos = go.transform.position.y;
            thisnode.id = counter;
            thisnode.nodeStrength = 2;
            info.nodes.Add(thisnode);
            counter++;
        }
        counter = 1;
        foreach (GameObject go in tiles)
        {

            var thistile = new LevelInfo.Tile();
            thistile.id = counter;
            thistile.title = go.GetComponentInChildren<TextMesh>().text;
            thistile.extrainfo = "I don't have extra info cause i suck.";
            info.tiles.Add(thistile);
            counter++;
        }
        var filePath = "Assets/" + Name + ".json";
        var JSONString = JsonUtility.ToJson(info);
        File.WriteAllText(filePath, JSONString);



        //go through the  nodes and tiles and make it into one of those json data structures.
    }
}

/*
 * Just a simple class to test Unity's JsonUtility, using a made-up PlayerData C# class.
 * */

using UnityEngine;
using System.Reflection; // so it can find the location of its executing assembly. This allows us to use relative paths.
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class JsonDeserializationTest: MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {

        // First, to get our objects, we need to read in the entire json as text, then deserialize it.
        // Note that location of executing assemebly is: *\netCDF-Unity\Interactive Visualization\Library\ScriptAssemblies\Assembly-CSharp.dll
        // the .dll itself is a folder.
        string playerStatsLoc = Assembly.GetExecutingAssembly().Location + "\\..\\..\\..\\Assets\\Test Scripts\\JSON\\data.json";
        List<PlayerData> stats =  JsonConvert.DeserializeObject<List<PlayerData>>(File.ReadAllText(playerStatsLoc));

        // printing player attributes
        scoreText.text = "";
        foreach (PlayerData d in stats)
        {
            scoreText.text+= "Name: "+d.name+"\t Scores: " + d.scores[0] + ", " + d.scores[d.scores.Length-1] + '\n';
        } // end foreach

	} // end method Start()
	
	// Update is called once per frame
	void Update () {
	
	}
}

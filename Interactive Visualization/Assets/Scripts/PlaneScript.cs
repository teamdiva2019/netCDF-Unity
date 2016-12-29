using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;

public class PlaneScript : MonoBehaviour {

	private Texture2D myTexture;
	private Material myMaterial;
    public Text assemblyLocation; //Assembly Location: "*\netCDF-Unity\Interactive Visualization\Library\ScriptAssemblies\Assembly-CSharp.dll". 3 ..'s get you to Interactive Visualization.
                                  // we need to know the location of the assembly to use relative paths.

    // Use this for initialization
    void Start () {

		GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);

        // this is so we can use relative paths.
        assemblyLocation.text = "Assembly Location: "+Assembly.GetExecutingAssembly().Location; // lets you see it.
        string loc = Assembly.GetExecutingAssembly().Location;


        /*taken from http://stackoverflow.com/questions/20764049/how-to-execute-shell-script-in-c-sharp*/
        Process proc = new Process {
			StartInfo = new ProcessStartInfo {
				FileName = "python.exe",
				Arguments = loc+"\\..\\..\\..\\Assets\\ReadnCDF\\PlotData.py", // Change this to the full path
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}
		};
		// Delete the time.txt if it's there
		string imageString = loc + "\\..\\..\\..\\Assets\\Resources\\Plot.png";
		File.Delete(loc + "\\..\\..\\..\\Assets\\ReadnCDF\\time.txt");
		proc.Start ();
		proc.Close ();

		var bytes = File.ReadAllBytes (imageString);
		var tex = new Texture2D (1, 1);
		tex.LoadImage (bytes);
		myMaterial.mainTexture = tex;
		MeshRenderer mr = plane.GetComponent<MeshRenderer> ();
		mr.material = myMaterial;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

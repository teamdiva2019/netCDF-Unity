using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
public class PlaneScript : MonoBehaviour {

	private Texture2D myTexture;
	private Material myMaterial;

	// Use this for initialization
	void Start () {

		GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);

		/*taken from http://stackoverflow.com/questions/20764049/how-to-execute-shell-script-in-c-sharp*/
		Process proc = new Process {
			StartInfo = new ProcessStartInfo {
				FileName = "python.exe",
				Arguments = "\"C:\\Users\\mughi\\Documents\\Unity Projects\\Interactive Visualization\\Assets\\ReadnCDF\\PlotData.py\"", // Change this to the full path
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}
		};
		// Delete the time.txt if it's there
		string imageString = "\"C:\\Users\\mughi\\Documents\\Unity Projects\\Interactive Visualization\\Assets\\Resources\\Plot.png\"";
		File.Delete("\"C:\\Users\\mughi\\Documents\\Unity Projects\\Interactive Visualization\\Assets\\ReadnCDF\\time.txt\"");
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

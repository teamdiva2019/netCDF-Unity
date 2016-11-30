using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	private int count;
	public float speed;
	public Text countText;
	public Text winText;
	private string output;
	void Start() {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		output = "";
		/*taken from http://stackoverflow.com/questions/20764049/how-to-execute-shell-script-in-c-sharp*/
		Process proc = new Process {
			StartInfo = new ProcessStartInfo {
				FileName = "python.exe",
				Arguments = "\"C:\\Users\\mughi\\Documents\\Python Programs\\readCDF.py\"",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}
		};
		proc.Start ();
		while (!proc.StandardOutput.EndOfStream) {
			string line = proc.StandardOutput.ReadLine ();
			output += "\n" + line;
		}
		proc.Close ();
	}
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "YOU WIN!!!";
		}
	}
	void OnGUI(){
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),output);
	}
}

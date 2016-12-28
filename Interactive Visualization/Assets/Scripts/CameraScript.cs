using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// go to initial position
	void Start () {
        transform.position = new Vector3(0, 0, -30);
	}
	
	// camera
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime); // RotateAround(point, axis, angle in deg)
        transform.LookAt(new Vector3(0, 0, 0));
	}
}

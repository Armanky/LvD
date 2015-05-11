using UnityEngine;
using System.Collections;

public class transformRotator : MonoBehaviour {

	public float xRPS;
	public float yRPS;
	public float zRPS;

	private Vector3 rotationDuration;

	private Vector3 newRotation;
	
	// Use this for initialization
	void Start () {
		rotationDuration = new Vector3 (
			(xRPS != 0) ? 1 / xRPS : 0,
			(yRPS != 0) ? 1 / yRPS : 0,
			(zRPS != 0) ? 1 / zRPS : 0
		);
	}
	
	// Update is called once per frame
	void Update () {

		newRotation = new Vector3 ();

		//Perform the rotation for all three axes
		newRotation.x = (rotationDuration.x != 0) ? ((Time.time % rotationDuration.x) / rotationDuration.x) * 360 : 0.0f;
		newRotation.y = (rotationDuration.y != 0) ? ((Time.time % rotationDuration.y) / rotationDuration.y) * 360 : 0.0f;
		newRotation.z = (rotationDuration.z != 0) ? ((Time.time % rotationDuration.z) / rotationDuration.z) * 360 : 0.0f;

		transform.eulerAngles = newRotation;
	
	}
}

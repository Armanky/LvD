using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {

	public float period;
	public float amplitude;

	private float initTime;
	private float timeDelta;
	private float initPos;

	private Vector3 newPos;

	void Start(){
		initTime = Time.time;
		initPos = transform.position.y;

		newPos = new Vector3 (0, 0, 0);

	}

	// Update is called once per frame
	void Update () {
	
		timeDelta = Time.time - initTime;

		newPos.y = initPos + (amplitude * Mathf.Sin (((2 * Mathf.PI) / period) * timeDelta));

		transform.position = newPos;
	}
}

using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float flickerChance;
	public float dimRatio;

	private int rand;
	private Light L;

	private float normalIntensity;

	// Use this for initialization
	void Start () {
		L = this.GetComponent<Light> ();
		normalIntensity = L.intensity;
	}
	
	// Update is called once per frame
	void Update () {
	
		rand = Random.Range (0, 10000);

		if ((float)rand / 10000 < flickerChance)
			L.intensity *= dimRatio;
		else
			L.intensity = normalIntensity;

	}
}

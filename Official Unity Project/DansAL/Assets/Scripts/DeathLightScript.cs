using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathLightScript : MonoBehaviour {

	public GHOSTController ghostController;

	private Image img;
	private Color c;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		c = img.color;
		c.a = 0;
		img.color = c;
	}
	
	// Update is called once per frame
	void Update () {
		c.a = ghostController.getKillPercentage ();
		img.color = c;

	}
}

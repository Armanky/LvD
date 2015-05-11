using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScreenButton : MonoBehaviour {

	public int buttonType;	// 0 = tutorial, 1 = first mission, -1 = exit

	private Text textComponent;
	private Color c;
	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		switch (buttonType) {
		case -1:
			//Exit game
			Application.Quit ();
			break;
		case 0:
			//Load snacks
			Application.LoadLevel ("loadSnacks");
			break;

		case 1:
			//Load tools
			Application.LoadLevel ("loadTools");
			break;
		}
	}
	
	void OnMouseEnter(){
		c = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		textComponent.color = c;
	}
	
	void OnMouseExit(){
		c = new Color (0.5f, 0.5f, 0.5f, 1.0f);
		textComponent.color = c;
	}
}

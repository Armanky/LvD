using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerConsoleButton : MonoBehaviour {
	
	public Sprite imgHighlight;
	public Sprite imgSelect;
	public int room;

	private GameObject G;
	private PowerConsole PC;
	private SpriteRenderer img;
	private GUITexture tex;



	private bool active;



	// Use this for initialization
	void Start () {
		G = GameObject.FindGameObjectWithTag ("global");
		img = GetComponent<SpriteRenderer> ();
		img.enabled = false;

		tex = GetComponent<GUITexture> ();

		active = false;
	}

	public void setRoom(int r){
		room = r;
	}

	void OnMouseDown(){
		//Activate the room associated with this image
		G.BroadcastMessage ("onPowerConsoleButtonPress", room, SendMessageOptions.RequireReceiver);
		img.sprite = (active) ? imgHighlight : imgSelect;
		active = !active;
	}

	void OnMouseEnter(){
		//Highlight this
		G.BroadcastMessage ("onPowerConsoleButtonHighlight", room, SendMessageOptions.RequireReceiver);
		if (!active) {
			img.sprite = imgHighlight;
			img.enabled = true;
		}
	}

	void OnMouseExit(){
		//If not active, remove the image
		if (!active)
			img.enabled = false;
	}

	void onSendPower(int r){
		active = false;
		img.enabled = false;
	}
}

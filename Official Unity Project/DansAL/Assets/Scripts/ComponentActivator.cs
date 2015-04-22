using UnityEngine;
using System.Collections;

public class ComponentActivator : MonoBehaviour {

	private GHOSTController ghost;
	private RadioController radio;
	private RoomController room;
	private CharacterController player;



	// Use this for initialization
	void Start () {
	
		ghost = GetComponent<GHOSTController> ();
		radio = GetComponent<RadioController> ();
		room = GetComponent<RoomController> ();

		player = transform.parent.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onPowerConsole(){

		//Deactivate GHOST and character controllers
		room.enabled = false;
		//player.enabled = false;

	}

	void onExitPowerConsole(){

		//Reactive paused components
		player.enabled = true;
		room.enabled = true;
	}
}

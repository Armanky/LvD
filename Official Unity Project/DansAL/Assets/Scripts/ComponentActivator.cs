using UnityEngine;
using System.Collections;

public class ComponentActivator : MonoBehaviour {

	private GHOSTController ghost;
	private RadioController radio;
	private RoomController room;
	private CharacterController player;
	private GameObject powerConsole;



	// Use this for initialization
	void Start () {
	
		ghost = GetComponent<GHOSTController> ();
		radio = GetComponent<RadioController> ();
		room = GetComponent<RoomController> ();

		powerConsole = transform.parent.parent.FindChild ("UI").FindChild ("Power Console").gameObject;
		powerConsole.SetActive (false);

		player = transform.parent.GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onPowerConsole(){
	
		//Deactivate GHOST and character controllers
		room.enabled = false;
		powerConsole.SetActive (true);
		//player.enabled = false;

	}

	void onExitPowerConsole(){

		//Reactive paused components
		player.enabled = true;
		room.enabled = true;
		powerConsole.SetActive (false);
	}

	void onDoorClick(Door d){

		//TODO: Deactivate necessary components
		ghost.enabled = false;
		player.enabled = false;

	}

	void onFadedIn(){

		ghost.enabled = true;
		player.enabled = true;
	}
}

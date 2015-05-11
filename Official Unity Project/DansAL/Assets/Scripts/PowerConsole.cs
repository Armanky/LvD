using UnityEngine;
using System.Collections;

public class PowerConsole : MonoBehaviour {

	public AudioSource aud;

	private bool [] roomStates;
	private GameObject G;

	// Use this for initialization
	void Start () {
	
		roomStates = new bool[13];

		clearRoomStates ();

		G = GameObject.FindGameObjectWithTag ("global");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void clearRoomStates(){
		//Set all room toggle flags to false
		for (int i = 0; i < roomStates.Length; ++i)
			roomStates[i] = false;
		
	}

	public void toggleRoomState(int r){
		roomStates [r] = !roomStates [r];
	}

	void onPowerConsoleButtonPress(int r){
		toggleRoomState (r);
	}

	void OnMouseEnter(){
		G.BroadcastMessage ("onPowerConsoleButtonHighlight", -1, SendMessageOptions.RequireReceiver);
	}



	void OnMouseDown(){
		//Broadcast a message for each room
		for (int i = 0; i < roomStates.Length; ++i) {
			if (roomStates[i])
				G.BroadcastMessage ("onSendPower", i, SendMessageOptions.RequireReceiver);
		}

		clearRoomStates ();
	}
	
}

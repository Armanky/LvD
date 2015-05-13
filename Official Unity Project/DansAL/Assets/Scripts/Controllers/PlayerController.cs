using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : EventReceiver {

	public int currentRoom;

	private int prevRoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onDoorClick(Door d){
		prevRoom = d.from;
		currentRoom = d.to;
	}

	void onChangeRoom(Room r){

		//Choose the appropriate positioner object and take its transform
		GameObject[] p = GameObject.FindGameObjectsWithTag ("chiefPosition");
		for (int i = 0; i < p.Length; ++i) {
			if (p[i].transform.parent.GetComponent<Door>().to == prevRoom){
				transform.position = p[i].transform.position;
			}
		}

	}
	
}

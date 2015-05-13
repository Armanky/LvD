using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class miniMap : MonoBehaviour {

	public Image[] rooms;

	private Image currentRoom;

	// Use this for initialization
	void Start () {
		for (int i = 1; i < rooms.Length; ++i)
			rooms [i].enabled = false;

		currentRoom = rooms [0];
	}

	void onChangeRoom(Room r){
		currentRoom.enabled = false;
		rooms [r.id].enabled = true;
		currentRoom = rooms [r.id];
	}
}

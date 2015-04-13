using UnityEngine;
using System.Collections;

[System.Serializable]
public class Room {

	public string name;
	public string scene;

	//TODO: Make this private
	public bool dark;

	public int[] connectedRooms;

	public int id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleLight(){
		dark = !dark;
	}

	public void turnOffLight(){
		dark = true;
	}

	public string getName(){
		return name;
	}


}

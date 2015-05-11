using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class RoomController : EventReceiver  {

	public Room[] rooms;

	public int blackoutDelay; //seconds
	public float blackoutChance;

	public int playerRoom;
	public int ghostRoom;

	private int blackoutCounter;
	private int rand;

	private int[,] ghostPathMatrix;

	private GameObject G;
	// Use this for initialization
	void Start () {
		blackoutCounter = blackoutDelay * 60;
		G = GameObject.FindGameObjectWithTag ("global");

		ghostRoom = 8;
		playerRoom = 0;

		//Set IDs for all the rooms
		for (int i = 0; i < rooms.Length; ++i)
			rooms[i].id = i;

			
		ghostPathMatrix = new int[,] { 
			//0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16
			{-1,  1,  1,  1,  1,  1,  1,  11, 11, 11, 11, 11, 11, 1,  1,  11, 11}, //0
			{ 0, -1,  2,  3,  2,  3,  2,  2,  2,  0,  0,  0,  0,  14, 14, 0,  3},//1
			{ 1,  1, -1,  4,  4,  5,  4,  4,  4,  4,  1,  1,  1,  1,  1,  1,  4}, //2
			{ 1,  1,  1, -1,  4,  4,  4,  4,  4,  4,  1,  1,  1,  1,  1,  1,  4},//3
			{ 2,  2,  2,  3, -1,  6,  6,  6,  6,  6,  6,  2,  2,  6,  3,  3,  6},//4
			{ 3,  3,  2,  2,  4, -1,  6,  6,  6,  6,  6,  3,  6,  6,  2,  2,  6},//5
			{ 4,  4,  4,  4,  2,  5, -1,  7,  8,  7,  7,  8,  8,  7,  4,  7,  7},//6
			{ 9,  6,  6,  6,  6,  6,  6, -1,  6,  9,  9,  9,  9,  16, 6,  9,  16},//7
			{ 9,  6,  6,  6,  6,  6,  6,  6, -1,  9,  9,  9,  9,  6,  6,  9,  9},//8
			{10, 10,  7,  7,  7,  8,  8,  7,  8, -1, 10, 10, 10,  7,  10, 10, 7},//9
			{11, 11, 11, 11,  9,  9,  9,  9,  9,  9, -1, 11, 12,  11, 11, 11, 9},//10
			{ 0,  0,  0,  0,  0,  0, 10, 10, 10, 10, 10, -1, 12,  15, 0,  15, 10},//11
			{11, 11, 11, 11, 11, 11, 10, 10, 10, 10, 10, 11, -1,  11, 1,  11, 10},//12
			{14, 14, 14, 14, 14, 14, 16, 16, 16, 16, 15, 15, 15,  -1, 14, 15, 16},//13
			{ 1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  13, -1, 13, 13},//14
			{11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11,  13, 13, -1, 13},//15
			{14, 14, 14, 14, 14, 14, 16, 16, 16, 16, 15, 15, 15,  13, 13, 13, -1},};//16
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.K)) {
			//ExecuteEvents.Execute<IMessageTarget> (this.gameObject, null, (x, y) => x.onEnterDarkRoom ());
			G.BroadcastMessage ("onEnterDarkRoom");
		}

		if (Input.GetKeyDown (KeyCode.L))
			ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y)=>x.onEnterLightRoom());



		//Update chance to blackout a room
		if (blackoutCounter > 0)
			blackoutCounter--;
		else {
			rand = Random.Range (1, 10000);
			if ((float)rand/10000 <= blackoutChance){
				//Black out a random room
				blackoutCounter = blackoutDelay * 60;

				rand = Random.Range (0, rooms.Length - 4);
				while (!allRoomsDark()){

					//Try to darken room, as long as room isn't dark
					if (rooms[rand].dark){
						//Room is already dark, choose another one
						rand = Random.Range(0, rooms.Length - 4);
					}
					else{
						darkenRoom (rand);
						break;
					}

				}

			}


		}
	}

	void toggleRoomState(int r){

		rooms [r].toggleLight ();

	}

	void darkenRoom(int r){

		rooms [r].turnOffLight ();
		//ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y)=>x.onRoomWentDark(r));
		G.BroadcastMessage ("onRoomWentDark", rooms [r], SendMessageOptions.DontRequireReceiver);

	}

	bool isRoomDark(int r){

		return rooms[r].dark;

	}

	public bool allRoomsDark(){
		bool result = true;

		for (int i = 0; i < rooms.Length - 4; ++i)
			if (!rooms[i].dark)
				result = false;

		return result;
		
	}

	public int numDarkRooms(){
		int result = 0;

		for (int i = 0; i < rooms.Length - 4; ++i)
			if (rooms [i].dark)
				result++;

		return result;
	}

	private bool isPlayerAdjacentToGhost(){

		bool result = false;

		//Look for the GHOST room in the rooms connected to the player room
		for (int i = 0; i < rooms[playerRoom].connectedRooms.Length; ++i)
			if (rooms [playerRoom].connectedRooms [i] == ghostRoom)
				result = true;

		return result;
	}

	private bool isPlayerInGhostRoom(){

		return (playerRoom == ghostRoom);

	}

	void setRoomLighting(){

		//Check the state of the current room and switch the lighting if necessary
		GameObject flashlight = GameObject.FindGameObjectWithTag ("MainCamera").transform.FindChild ("Flashlight").gameObject;

		if (!isPlayerInGhostRoom ()) {
			if (rooms [playerRoom].dark) {
				//Make the room dark!
				GameObject[] lights = GameObject.FindGameObjectsWithTag ("roomLight");
				Light l;
				for (int i = 0; i < lights.Length; ++i)
					lights [i].SetActive (false);

				flashlight.SetActive (true);

				//ExecuteEvents.Execute<IMessageTarget>(G, null, (x, y)=>x.onEnterDarkRoom());
				G.BroadcastMessage ("onEnterDarkRoom");
			} else {
				flashlight.SetActive (false);
				//ExecuteEvents.Execute<IMessageTarget>(G, null, (x, y)=>x.onEnterLightRoom());
				G.BroadcastMessage ("onEnterLightRoom");
			}
		} else {
			//Player is in GHOST room
			GameObject[] lights = GameObject.FindGameObjectsWithTag ("roomLight");
			Light l;
			for (int i = 0; i < lights.Length; ++i)
				lights [i].SetActive (false);

			flashlight.SetActive (false);
		}
	}


	//Event handlers


	void onRoomWentDark(Room r){
		Debug.Log (r.name + " went dark!");
		setRoomLighting ();
	}
	
	void onFadedOut(int newRoom){

		playerRoom = newRoom;
		Application.LoadLevel (rooms[newRoom].scene);

	}

	void OnLevelWasLoaded(){

		if (Application.loadedLevelName.Substring (0, 4) != "load") {

			G.BroadcastMessage ("onChangeRoom", rooms [playerRoom], SendMessageOptions.DontRequireReceiver);

			if (isPlayerAdjacentToGhost ()) 
				G.BroadcastMessage ("onAdjacentToGhost");
			else
				G.BroadcastMessage ("onNotAdjacentToGhost");

			if (isPlayerInGhostRoom ()) {
				G.BroadcastMessage ("onGHOSTMet");
			}

			setRoomLighting ();

		}
	}

	void onGHOSTIsMoving(){
		if (!isPlayerInGhostRoom ()) {
			//Pick a room for the GHOST
			if (rooms [playerRoom].dark) {
				//Use the matrix to pick a room
				ghostRoom = ghostPathMatrix [ghostRoom, playerRoom];

			} else if (!isPlayerInGhostRoom ()) {
				//Pick a room at random
				int randomRoom; 
				int roomSelected = 99;
				Room curr = rooms [ghostRoom];

				while (roomSelected >= 13) {
					randomRoom = Random.Range (0, curr.connectedRooms.Length);
					roomSelected = curr.connectedRooms [randomRoom];

					//If the GHOST is already in the Power Station, he has to move through the tunnels
					if (ghostRoom == 13)
						break;
				}

				ghostRoom = roomSelected;

			}

			//Tell everything else that the GHOST has moved
			G.BroadcastMessage ("onGHOSTMoved", rooms[ghostRoom], SendMessageOptions.DontRequireReceiver);

			Debug.Log ("GHOST moved to " + rooms [ghostRoom].name);

			//Determine the player's location in relation to the GHOST's
			if (isPlayerInGhostRoom ()){
				G.BroadcastMessage ("onGHOSTMet");
			}

			setRoomLighting ();

		}
	
		if (isPlayerAdjacentToGhost ()) 
			G.BroadcastMessage ("onAdjacentToGhost");
		else
			G.BroadcastMessage ("onNotAdjacentToGhost");
	}

	void playerDied(){
		Debug.Log ("YOU ARE DEAD");
	}

	void onSendPower(int r){
		toggleRoomState (r);
	}



}

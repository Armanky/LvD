using UnityEngine;
using System.Collections;

public class GHOSTController : MonoBehaviour {

	public float normalWaitTime;	//Player's in a light room or Power Station areas
	public float fastWaitTime;		//Player's in a dark room
	public float frenzyWaitTime;	//Player has pissed off the GHOST

	public float normalChance;		//Chance the GHOST will move this frame
	public float fastChance;
	public float frenzyChance;

	public float frenzyDuration;

	public float movementChance;

	private int movementSpeed;		//0 == normal, 1 == fast, 2 == frenzy
	private int[,] pathMatrix = new int[17,17];
	private Room playerRoom;

	private float timeOfLastMovement;
	private float timeOfBeginFrenzy;
	private float timeDelta;

	private float currentWaitTime;
	private float currentChance;

	private bool isFrenzy;

	private int rand;

	private GameObject G;

	// Use this for initialization
	void Start () {
	
		//Rows represent GHOST location. Columns represent player location.

		//Start with normal movement speed
		movementSpeed = 0;
        

		timeOfLastMovement = Time.time;
		timeDelta = 0.0f;

		G = GameObject.FindGameObjectWithTag ("global");

		isFrenzy = false;

		playerRoom = new Room ();
		playerRoom.dark = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		timeDelta = Time.time - timeOfLastMovement;

		//Check if we're in frenzy
		if (isFrenzy && Time.time - timeOfBeginFrenzy < frenzyDuration) {
			//Keep the frenzy going until the time is over
			movementSpeed = 2;
		} else {
			isFrenzy = false;
			setSpeed ();
		}

		switch (movementSpeed) {
		case 0:
			currentWaitTime = normalWaitTime;
			currentChance = normalChance;
			break;
		case 1:
			currentWaitTime = fastWaitTime;
			currentChance = fastChance;
			break;
		case 2:
			currentWaitTime = frenzyWaitTime;
			currentChance = frenzyChance;
			break;
		}

		//Update timer events
		if (timeDelta > currentWaitTime) {
			//GHOST is able to move
			rand = Random.Range (1, 10000);

			if ((float)rand/10000 <= currentChance){
				//Move the GHOST
				G.BroadcastMessage ("onGHOSTIsMoving");

				//Reset move timer
				timeOfLastMovement = Time.time;
			}
		}




	}

	void setSpeed(){
		if (playerRoom.dark) {
			if (playerRoom.id < 13)
				movementSpeed = 1;	//fast
			else
				movementSpeed = 0;	
		} else {
			movementSpeed = 0;
		}
	}

	void onGHOSTMet(){
		movementSpeed = 2;
		isFrenzy = true;
		timeOfBeginFrenzy = Time.time;
	}

	void onChangeRoom(Room r){
		playerRoom = r;
		setSpeed ();
	}

	void onRoomWentDark(Room r){
		//Give the player a reprieve if their room goes dark
		timeOfLastMovement = Time.time;
	}


}

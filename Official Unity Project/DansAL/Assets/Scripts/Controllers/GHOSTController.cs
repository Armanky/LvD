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

	public float killTime;			//Number of seconds a player can share a room until death
	private float currentKillTime;

	private int movementSpeed;		//0 == normal, 1 == fast, 2 == frenzy
	private int[,] pathMatrix = new int[17,17];
	private Room playerRoom;

	private float timeOfLastMovement;
	private float timeOfBeginFrenzy;
	private float timeDelta;

	private float currentWaitTime;
	private float currentChance;



	private bool isFrenzy;
	private bool isKilling;

	private int rand;

	private GameObject G;
	private GameObject roomGhost;

	// Use this for initialization
	void Start () {
	
		//Rows represent GHOST location. Columns represent player location.

		//Start with normal movement speed
		movementSpeed = 0;
        

		timeOfLastMovement = Time.time;
		timeDelta = 0.0f;

		G = GameObject.FindGameObjectWithTag ("global");
		roomGhost = GameObject.FindGameObjectWithTag ("GHOST");

		isFrenzy = false;

		playerRoom = new Room ();
		playerRoom.dark = false;

		currentKillTime = 0;

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
				move ();
			}
		}

		//Update kill state
		if (isKilling) {
			currentKillTime = Mathf.Min (killTime, currentKillTime + Time.deltaTime);
			if (currentKillTime >= killTime){
				G.BroadcastMessage ("playerDied");
			}
		} else {
			currentKillTime = Mathf.Max (0, currentKillTime - (Time.deltaTime/2));
		}

		//DEBUG: Move GHOST manually
		if (Input.GetKeyDown (KeyCode.M)){
			move ();
		}


	}

	
	void move(){
		//Move the GHOST
		G.BroadcastMessage ("onGHOSTIsMoving");
			
		//Reset move timer
		timeOfLastMovement = Time.time;
	}

	public float getKillPercentage(){
		return Mathf.Pow(currentKillTime / killTime, 3.5f);
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

		roomGhost.SetActive (true);
		roomGhost.GetComponentInChildren<AudioSource> ().Play ();

		isKilling = true;

		//Turn off the radio
		setRadioSources (false);
	}

	void onChangeRoom(Room r){
		playerRoom = r;
		setSpeed ();

		roomGhost = GameObject.FindGameObjectWithTag ("GHOST");
		roomGhost.SetActive (false);

		isKilling = false;

		setRadioSources (true);

	}

	void onRoomWentDark(Room r){
		//Give the player a reprieve if their room goes dark
		timeOfLastMovement = Time.time;
	}

	void setRadioSources(bool state){
		AudioSource[] aud = this.gameObject.GetComponentsInChildren<AudioSource> ();
		for (int i = 0; i < aud.Length; ++i)
			aud [i].enabled = state;
	}
	


}

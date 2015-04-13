using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;



public class RadioController : EventReceiver {

	/*
	 * 
	 * 	PERSISTENT SOUND CHANNELS
	 * 	-Static
	 * 	-GHOST Music
	 * 
	 * 	ONE SHOT SOUND CHANNELS
	 * 	-Ambient FX
	 * 	-GHOST Movement
	 * 	-Blackout
	 * 	-Ed
	 * 	-Radar
	 * 
	 * 
	 */

	public RadioChannel staticChannel;
	public RadioChannel ghostMusicChannel;
	public RadioChannel ambienceChannel;
	public RadioChannel ghostMovementChannel;
	public RadioChannel blackoutChannel;
	public RadioChannel edChannel;
	public RadioChannel radarChannel;
	public RadioChannel UIChannel;

	public DialogueLoader dialogue;

	public AudioClip lightStatic;
	public AudioClip darkStatic;

	public int rambleInterval;	//Number of frames that must pass before Ed can ramble again
	public int rambleChance;	//Probability that Ed will start rambling when he is able to do so (1 in rambleChance odds)

	private int introIndex;

	private bool isGivingIntroduction;
	private int rambleCountdown;

	private int radarRate;
	private int radarCounter;

	private int rand;	//Used to hold random numbers

	private GameObject G;
	
	// Use this for initialization
	void Start () {
		//Start playing Ed's introduction
		introIndex = 0;
		edChannel.src.clip = dialogue.IntroClips [introIndex];
		edChannel.src.Play ();
		isGivingIntroduction = true;
		rambleCountdown = -1;

		staticChannel.targetVolume = 0.2f;

		radarRate = 40;

		G = GameObject.FindGameObjectWithTag ("global");

	}
	
	// Update is called once per frame
	void Update () {

		updateRadar ();
		updateGHOST ();
		updateDialogue ();

		staticChannel.update ();

		blackoutChannel.update ();

	}











	void updateGHOST(){

		//Update channels
		ghostMovementChannel.update ();
		ghostMusicChannel.update ();
		
	}

	void updateRadar(){
		//Check for item proximity
		float dist = findDistanceToNearestItem ();

		if (dist <= 4.0f) {
			//It's within grabbing distance!
			radarRate = 30;
			radarChannel.setTargetVolume (1.0f, 0.5f);
			radarChannel.setTargetPitch (1.0f, 0.005f);

		} else if (dist > 4.0f && dist <= 12.0f) {
			//Item is close!
			radarRate = 80;
			radarChannel.setTargetVolume (0.8f, 0.5f);
			radarChannel.setTargetPitch (0.9f, 0.005f);

		} else if (dist > 12.0f && dist <= 18.0f) {
			//Item is in the vicinity!
			radarRate = 120;
			radarChannel.setTargetVolume (0.75f, 0.5f);
			radarChannel.setTargetPitch (0.85f, 0.005f);

		} else if (dist < float.MaxValue) {
			//There is an item in the room, but it's far away
			radarRate = 180;
			radarChannel.setTargetVolume (0.7f, 0.5f);
			radarChannel.setTargetPitch (0.8f, 0.005f);
		} else {
			//No items in the room
			radarRate = -1;
			radarChannel.setTargetVolume (0.0f, 0.5f);
			radarChannel.setTargetPitch (0.0f, 0.005f);

		}

		if (radarRate >= 0) {
			//Only update if the radar is active
			if (radarCounter >= 0){
				//If we're still above the new rate, drop it down
				if (radarCounter > radarRate)
					radarCounter = radarRate;

				radarCounter--;
			}
			else {
				radarChannel.src.time = 0;
				radarChannel.Play ();
				radarCounter = radarRate;
			}

		}

		radarChannel.update ();
	}
	
	void updateDialogue(){


		//Check if Ed's giving his introduction
		if (!edChannel.src.isPlaying && isGivingIntroduction) {
			//ED IS GIVING HIS INTRODUCTION

			if (++introIndex < dialogue.IntroClips.Length) {
				//Advance introduction clip
				edChannel.src.clip = dialogue.IntroClips [introIndex];
				edChannel.Play ();
			} else
				isGivingIntroduction = false;

		} 
		else {
			//NORMAL BEHAVIOR

			//Check for victory conditions, they override other dialogue
			//ITEM QUOTA MET

			//ALL ITEMS COLLECTED

			//ESCAPE

			if (!edChannel.src.isPlaying){
				//ED IS NOT TALKING

				//RAMBLE DIALOGUE
				if (rambleCountdown >= 0)
					rambleCountdown--;
				else{
					//Enough time has passed that we can play a ramble clip
					rand = Random.Range(0, rambleChance);

					if (rand == 0){
						//Start rambling
						rand = Random.Range (0, dialogue.RambleClips.Length);
						edChannel.src.clip = dialogue.RambleClips[rand];
						edChannel.Play ();
					}
				}

			}
			else {
				//ED IS TALKING

				//Keep the ramble countdown maxed out
				rambleCountdown = rambleInterval;
			}
				


		}
		

		edChannel.update ();
	}

	private void playDialogue(AudioClip clip){
		//Play the specified dialogue clip
		//TODO: Know information corresponding to subtitle data

		ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y)=>x.onPlayDialogue(0));

		edChannel.src.clip = clip;
		edChannel.Play ();
	}

	private float findDistanceToNearestItem(){
		//For item radar, find the closest collectible

		GameObject[] items = GameObject.FindGameObjectsWithTag ("Collectible");
		float distance = float.MaxValue;
		float currentDistance = float.MaxValue;

		for (int i = 0; i < items.Length; ++i) {
			currentDistance = Vector3.Distance (items[i].transform.position, this.transform.parent.position);
			if (currentDistance < distance)
				distance = currentDistance;
		}

		return distance;

	}

	//Event handlers

	void onRoomWentDark(Room r){

		blackoutChannel.src.time = 0;
		blackoutChannel.Play ();

		//Ed has a 20% chance of identifying the room
		rand = Random.Range (0, 2);
		if (rand == 0 && !edChannel.src.isPlaying) {
			//Play the clip associated with the room
			edChannel.src.clip = dialogue.BlackoutClips[r.id];
			edChannel.Play ();
		}

	}

	public override void onQuotaMet(){

		//This sound clip overrides whatever Ed is saying
		edChannel.src.clip = dialogue.QuotaMetClip;
		edChannel.Play ();

	}

	void onEnterDarkRoom(){

		//Play transition clip
		ambienceChannel.src.time = 0;
		ambienceChannel.Play ();

		//Turn off other channels
		edChannel.setTargetVolume (0, 0.01f);
		edChannel.setTargetPitch (0.2f, 0.008f);

		staticChannel.src.clip = darkStatic;
		staticChannel.setTargetVolume (1, 0.05f);
		staticChannel.src.loop = true;
		staticChannel.Play ();

		radarChannel.setActive (false);

		blackoutChannel.setTargetVolume (0, 0.5f);
		blackoutChannel.setActive (false);

		ghostMovementChannel.setTargetVolume (0, 0.5f);
		ghostMovementChannel.setActive (false);

	}

	void onEnterLightRoom(){

		//ambienceChannel.src.time = 0;
		//ambienceChannel.Play ();

		edChannel.setTargetVolume (1, 0.005f);
		edChannel.setTargetPitch (1, 0.008f);

		staticChannel.src.clip = lightStatic;
		staticChannel.setTargetVolume (0.2f, 0.05f);
		staticChannel.src.loop = true;
		staticChannel.Play ();

		radarChannel.setActive (true);

		blackoutChannel.setTargetVolume (0.55f, 0.5f);
		blackoutChannel.setActive (true);

		ghostMovementChannel.setTargetVolume (0.55f, 0.5f);
		ghostMovementChannel.setActive (true);
	}

	void onAdjacentToGhost(){
		//Turn on GHOST music
		ghostMusicChannel.setTargetVolume (0.5f, 0.005f);
		//ghostMusicChannel.Play ();
	}

	void onNotAdjacentToGhost(){
		ghostMusicChannel.setTargetVolume (0.0f, 0.05f);
	}

	void onGHOSTMet(){
		Debug.Log ("THE GHOST IS HERE!!!!");
		staticChannel.setTargetVolume (0.0f, 0.05f);

	}

	void onItemClick(Collectible c){
		UIChannel.Play ();
	}

	void onChangeRoom(Room r){

	}

	void onGHOSTMoved(Room r){
		ghostMovementChannel.src.time = 0;
		ghostMovementChannel.Play ();
	}
	
}

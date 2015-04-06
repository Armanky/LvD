using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CollectibleController : EventReceiver {

	public int mission;

	private bool[] db;

	//List of indices associated with each mission
	private int[][] missionIndices;

	private int quotaTotal;
	private int rand;

	// Use this for initialization
	void Start () {
	
		missionIndices = new int[5][];

		//Generate an item database based on our list
		//https://docs.google.com/spreadsheets/d/1r2QRdUrdkswYt4-aMFmbOhygtG6-MSGl0pyw4tLXRGM/edit#gid=0

		//SNACKS
		missionIndices[0] = new int[]{
			1,
			7
		};

		//TOOLS
		missionIndices[1] = new int[]{
			0,
			2,
			3,
			4,
			5,
			6,
			1,
			7
		};

		//BOOKS
		missionIndices[2] = new int[]{
			-1
		};

		//ROCKS
		missionIndices[3] = new int[]{
			-1
		};

		//RELICS
		missionIndices[4] = new int[]{
			-1
		};


		db = new bool[50];
		for (int i = 0; i < db.Length; ++i)
			db[i] = false;

		initializeDatabase ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void checkCollectiblesInScene(){
		//Check all the collectibles in the scene against our list
		GameObject[] items = GameObject.FindGameObjectsWithTag ("Collectible");
		Debug.Log ("Checking " + items.Length + " items");
		Collectible c;

		for (int i = 0; i < items.Length; ++i){

			c = items[i].GetComponent<Collectible>();
			c.gameObject.SetActive ( db[c.gid] );

		}
	}

	private void initializeDatabase(){
		//Based on the mission we're in, choose items from the database to spawn
		//TODO: Generate at least twenty items
		int curr;
		for (int i = 0; i < 5; ++i){
			rand = Random.Range(0, missionIndices[mission].Length);
			curr = missionIndices[mission][rand];

			if (!db[curr])
				db[curr] = true;

			//TODO: Remove this.  This will prevent an infinite loop while our item lists are small
			bool esc = true;
			for (int j = 0; j < missionIndices[mission].Length; ++j){
				if (!db[missionIndices[mission][j]])
					esc = false;
			}

			if (esc)
				break;
		}

	}

	void OnLevelWasLoaded(int i){
		checkCollectiblesInScene ();
	}

	public override void onItemClick(int id, int value){
		db [id] = false;
	}
}

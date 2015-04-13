using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomLabelScript : EventReceiver {

	public float displayDuration;

	private Text label;

	private float activeStartTime;
	private float transitionDuration;

	private float timeDelta;

	private int quota;

	// Use this for initialization
	void Start () {
		label = this.gameObject.GetComponentInChildren<Text> ();

		transitionDuration = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Color c = label.color;


		timeDelta = Time.time - activeStartTime;
		//Check timer events
		if (timeDelta < transitionDuration) {
			//Currently fading text in
			c.a = timeDelta / transitionDuration;
		} else if (timeDelta >= transitionDuration && timeDelta < displayDuration + transitionDuration) {
			//Displaying text
			c.a = 1;
		} else if (timeDelta >= displayDuration + transitionDuration && timeDelta < displayDuration + (2 * transitionDuration)) {
			//Fading out
			c.a = 1 - ((timeDelta - (displayDuration + transitionDuration)) / transitionDuration);
		} else {
			c.a = 0;
		}

		label.color = c;

	}


	void activate(){
		activeStartTime = Time.time;
	}

	void onChangeRoom(Room r){
		//Change text to the name of the room
		label.text = r.name;
		activate ();

	}

	void onItemClick(Collectible c){
		quota += c.value;
		this.gameObject.GetComponentsInChildren<Text> () [1].text = c.name + '\n' + '+' + quota.ToString () + '%';
	}

}

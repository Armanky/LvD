using UnityEngine;
using System.Collections;

public class TitleScreenEffects : MonoBehaviour {

	public Light backgroundLight;
	public Light foregroundLight;
	public GameObject farGhost;
	public GameObject midGhost;
	public GameObject nearGhost;

	private int ghostPosition; //-1 = none, 1 = far, 2 = mid, 3 = near
	private int rand;
	private GameObject selectedGhost;

	private float ghostActivateTime;
	private float ghostActivateDuration;
	private float timeDelta;

	// Use this for initialization
	void Start () {
		farGhost.SetActive (false);
		midGhost.SetActive (false);
		nearGhost.SetActive (false);

		setGhostLights (farGhost, false);
		setGhostLights (midGhost, false);
		setGhostLights (nearGhost, false);

		ghostActivateTime = 0.0f;
		ghostActivateDuration = 8.0f;

		changeGhost ();
	}
	
	// Update is called once per frame
	void Update () {

		//Update GHOST activation
		timeDelta = Time.time - ghostActivateTime;
		if (timeDelta < ghostActivateDuration) {
			if (timeDelta > 1.0f && timeDelta < 2.5f) {
				//Flicker eyes
				if (Random.Range (1, 11) % 10 == 0)
					setGhostLights (selectedGhost, true);
				else
					setGhostLights (selectedGhost, false);
			} else if (timeDelta >= 2.5f && timeDelta <= 6.0f) {
				setGhostLights (selectedGhost, true);
			} else {
				setGhostLights (selectedGhost, false);
			}
		} else {

			//GHOST isn't currently activating
			changeGhost ();
			if (ghostPosition > 0)
				activateGhost ();
		}
	}

	void changeGhost(){
		//Activate a random GHOST
		rand = ghostPosition;

		while (rand == ghostPosition)
			rand = Random.Range (-1, 4);

		ghostPosition = rand;

		farGhost.SetActive (false);
		midGhost.SetActive (false);
		nearGhost.SetActive (false);

		selectedGhost = (rand >= 1) ? ( (rand >= 2) ? ( (rand >= 3) ? nearGhost : midGhost) : farGhost ) : null;

		if (selectedGhost != null)
			selectedGhost.SetActive (true);
	}

	void setGhostLights(GameObject ghost, bool state){

		ghost.transform.FindChild ("EyeSourceLeft").gameObject.SetActive (state);
		ghost.transform.FindChild ("EyeSourceRight").gameObject.SetActive (state);
		ghost.transform.FindChild ("MouthSource").gameObject.SetActive (state);

	}

	void activateGhost(){
		backgroundLight.gameObject.SetActive (false);
		foregroundLight.gameObject.SetActive (false);

		ghostActivateTime = Time.time;

	}
}

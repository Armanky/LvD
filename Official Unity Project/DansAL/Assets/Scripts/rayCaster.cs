using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class rayCaster : EventReceiver {

	public Texture doorTexture;
	public Texture defaultTexture;
	public Texture itemTexture; 
	public Texture currentTexture; 

	public float trackerLength = 5f; 

	private Vector3 fwd;
	private RaycastHit hit;
	private Ray cast;

	private string objTag;
	private GameObject G;

	void OnGUI(){
		GUI.Label(new Rect (Screen.width/2f, Screen.height/2f,32,32), currentTexture); 
	}

	// Use this for initialization
	void Start () {

		fwd = transform.TransformDirection (Vector3.forward); 
		cast = new Ray(transform.position, fwd);
		currentTexture = defaultTexture;
		Cursor.visible = false;

		G = GameObject.FindGameObjectWithTag ("global");

	}
	
	// Update is called once per frame
	void Update () {
	
		//always updates ray to looking foward. 
		fwd = transform.TransformDirection (Vector3.forward); 

		//Ray will always look forward
		cast.origin = transform.position;
		cast.direction = fwd;

		//draws Ray
		Debug.DrawRay (transform.position, fwd * 5, Color.red); 

		//Check for ray collision
		if (Physics.Raycast (cast, out hit, trackerLength)) {

			//Find out what type of object we've hit
			objTag = hit.collider.gameObject.tag;

			switch (objTag){
			case "Door":

				currentTexture = doorTexture;

				if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.F)){
					//We've clicked a door. Change rooms!
					GameObject d = hit.collider.gameObject;
					G.BroadcastMessage ("onDoorClick", d.GetComponent<Door>(), SendMessageOptions.DontRequireReceiver);

				}

				break;

			case "Collectible":

				currentTexture = itemTexture;

				if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.F)){
					//We've clicked an item. Collect it!
					GameObject d = hit.collider.gameObject;
					GameObject[] roots = GameObject.FindGameObjectsWithTag ("Collectible");

					//Call for all collectible objects
					d.GetComponent<Collectible>().collect ();
					//for (int i = 0; i < roots.Length; ++i){
					//	roots[i].BroadcastMessage ("onItemClick", d.GetComponent<Collectible>(), SendMessageOptions.DontRequireReceiver);

		



				}

				break;
				
			default:
				
				currentTexture = defaultTexture;
				break;

			}
		}
		else
			currentTexture = defaultTexture;

	}

	//P: -7.58, 0, 11.71
	//R: 0, -135, 0
}

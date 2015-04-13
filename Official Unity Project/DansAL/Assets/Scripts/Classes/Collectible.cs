using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[System.Serializable]
public class Collectible : EventReceiver{

	//Store the associated object and the mission that contains it
	public int gid;
	public int value;
	public string name;

	private GameObject G;

	void Start(){
		G = GameObject.FindGameObjectWithTag ("global");
	}


	public void collect(){
		G.BroadcastMessage ("onItemClick", this, SendMessageOptions.DontRequireReceiver);
		this.gameObject.SetActive (false);
	}
}

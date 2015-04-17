using UnityEngine;
using System.Collections;

public class GHOSTRotate : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("global").transform.FindChild ("Chief");
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.LookRotation (player.position -transform.position);
	}

	void onGHOSTMet(){

		transform.rotation = Quaternion.LookRotation (player.position -transform.position);

	}
}

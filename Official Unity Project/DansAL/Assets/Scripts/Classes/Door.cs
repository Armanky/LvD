using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour {

	public int from;
	public int to;

	private Animator anim;
	private int open;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		open = Animator.StringToHash ("open");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void openDoor(){
		anim.Play (open);
	}
}

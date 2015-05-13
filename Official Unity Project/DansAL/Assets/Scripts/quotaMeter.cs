using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class quotaScript : EventReceiver {
	//This is just so we can adjust it according to the screen.
	public float YAX, XAX; 

	public Texture[] Quota; 
	public int quotaIndex =0;
	// Use this for initialization

	void OnGUI(){
		GUI.Label(new Rect (Screen.width-XAX, Screen.height-(Screen.height-YAX),400,50), Quota[quotaIndex]); 
	}

	void updateQuota(Collectible c)
	{
		quotaIndex += c.value/10;

		if(quotaIndex > 10)
			quotaIndex = 10; 
		OnGUI(); 
	}
}

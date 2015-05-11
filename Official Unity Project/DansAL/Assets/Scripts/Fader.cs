using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	public float fadeSpeed = 1.0f;	//Duration in seconds
	public Image fadeImage;

	private bool transitionFadeIn;
	private bool transitionFadeOut;
	private Color c;

	private int newRoom;

	private float startTime;
	private float timeDelta;

	private GameObject G;

	// Use this for initialization
	void Start () {
		
		c = fadeImage.color;
		//c.a = 0;
		transitionFadeIn = false;
		transitionFadeOut = false;

		G = transform.parent.parent.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//testing code
		/*c.a += fadeSpeed * Time.deltaTime;
        c.a = Mathf.Clamp01(c.a);
        fadeImage.color = c;*/
		
		if (transitionFadeOut == true)
		{
			timeDelta = Time.time - startTime;
			c.a = Mathf.Min (1, timeDelta / fadeSpeed);

			//c.a += fadeSpeed * Time.deltaTime;
			//c.a = Mathf.Clamp01(c.a);
			
			fadeImage.color = c;
			
			if (c.a == 1)
			{
				//Debug.Log("Hit transition false");
				transitionFadeOut = false;
				G.BroadcastMessage ("onFadedOut", newRoom, SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if (transitionFadeIn == true)
		{
			timeDelta = Time.time - startTime;
			c.a = Mathf.Max (0, (1 - timeDelta / fadeSpeed));

			//c.a -= fadeSpeed * Time.deltaTime;
			//c.a = Mathf.Clamp01(c.a);
			
			fadeImage.color = c;
			
			if (c.a == 0){
				transitionFadeIn = false;
				G.BroadcastMessage ("onFadedIn", newRoom, SendMessageOptions.DontRequireReceiver);

			}
		}
		
	}
	
	//broadcast method
	public void transitionIn()
	{
		transitionFadeIn = true;
	}
	
	//broadcast method
	public void transitionOut()
	{
		transitionFadeOut = true;
	}
	
	public void onDoorClick(Door d)
    {
        
        //insert your script disabales here 
          
        //call transitionIn here
        //yield return new WaitForSeconds(fadeSpeed);
		transitionFadeOut = true;
		newRoom = d.to;
		startTime = Time.time;

        //call loadlevel
        //call transitionOut here
        //yield return new WaitForSeconds(1.0f);
        
        //re-activate scripts here
         
    }

	void OnLevelWasLoaded(){
		startTime = Time.time;
		transitionFadeIn = true;
	}
}
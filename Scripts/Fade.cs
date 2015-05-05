﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	bool transitionFadeIn;
    bool transitionFadeOut;
    float fadeSpeed;
	Color c;
    public Image fadeImage;

	// Use this for initialization
	void Start () {
        //how many seconds
        fadeSpeed = 1.0f;

		c = fadeImage.color;
		//c.a = 0;
		transitionFadeIn = false;
        transitionFadeOut = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        //testing code
        /*c.a += fadeSpeed * Time.deltaTime;
        c.a = Mathf.Clamp01(c.a);
        fadeImage.color = c;*/

        if (transitionFadeIn == true)
        {
            c.a += fadeSpeed * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);

            fadeImage.color = c;

            if (c.a == 1)
            {
                //Debug.Log("Hit transition false");
                transitionFadeIn = false;
            }
        }

        if (transitionFadeOut == true)
        {
            c.a -= fadeSpeed * Time.deltaTime;
            c.a = Mathf.Clamp01(c.a);

            fadeImage.color = c;

            if (c.a == 0)
                transitionFadeOut = false;
        }
	
	}

    //broadcast method
	public void transitionIn(bool trans)
	{
		transitionFadeIn = trans;
	}

    //broadcast method
    public void transitionOut(bool trans2)
    {
        transitionFadeOut = trans2;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemBar : MonoBehaviour {

    public Image filling;

    float amount;
    float limit;

    //bool fillingCurrent;

	// Use this for initialization
	void Start () {

        //amount = .52f;
        //limit = 0.0f;

        //fillingCurrent = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        /*if (fillingCurrent == true)
        {
            Debug.Log("hit filling");

            filling.fillAmount += 0.01f;
            limit += 0.01f;

            if (limit >= amount)
                fillingCurrent = false;
        }*/
	
	}

    //should be percentage
    void fillAmount(float itemAmount)
    {
        filling.fillAmount += itemAmount;
        //fillingCurrent = true;
    }
}

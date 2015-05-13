using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemBar : MonoBehaviour {

    public Image filling;

    float amount;
    float limit;
    float totalAmount;

    bool fillingCurrent;

	// Use this for initialization
	void Start () {

        amount = 0.0f;
        limit = 0.0f;
        totalAmount = 0.0f;

        fillingCurrent = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (fillingCurrent == true)
        {

            filling.fillAmount += 0.01f;
            limit += 0.01f;

            if (limit >= amount)
            {
                fillingCurrent = false;
                filling.fillAmount = totalAmount;
                limit = 0.0f;
            }
        }
	
	}

    //should be percentage
    void fillAmount(float itemAmount)
    {
        amount = itemAmount;
        totalAmount += amount;
        fillingCurrent = true;
    }
}

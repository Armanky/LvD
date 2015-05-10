using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class minimap : MonoBehaviour {

    public Image baseImage;
    public Image[] location;

    float timer;

    private int roomLocation;

	// Use this for initialization
	void Start () {
        timer = 0.0f;

        for(int i = 1; i < 17; i++)
        {
            location[i].enabled = false;
        }

        roomLocation = 0;
        location[roomLocation].enabled = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            location[roomLocation].enabled = false;
            roomLocation++;
            if (roomLocation > 16)
            {
                roomLocation = 0;
            }
            location[roomLocation].enabled = true;
            timer = 0.0f;
        }
	
	}

    //broadcast method
    void locationSet(int currentRoom)
    {
        location[roomLocation].enabled = false;
        roomLocation = currentRoom;
        location[roomLocation].enabled = true;
    }
}

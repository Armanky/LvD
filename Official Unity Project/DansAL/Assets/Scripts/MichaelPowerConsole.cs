using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class breakerBoxScript : EventReceiver {
	
	bool [] powerButtons;
	public Image[] selectedImages;
	public Image[] highlightImages;
	public Button[] buttonObjects;
	
	public Canvas breakerBoxUI;
	GameObject powerStation;
	GameObject player;
	Image Base;
	
	/*public Button entraceBay;
    public Button medLab;
    public Button commCenter;
    public Button restRoom;
    public Button breakRoom;
    public Button crewQuarters;
    public Button northHallway;
    public Button materialsLab;
    public Button bioLab;
    public Button ghostLab;
    public Button eastShaft;
    public Button messHall;
    public Button kitchen;
    public Button sendPower;*/
	
	//Image entraceBayImage;
	//GameObject entraceBayObject;
	
	RoomController roomControllerOther;
	
	// Use this for initialization
	void Start () {
		
		breakerBoxUI = breakerBoxUI.GetComponent<Canvas>();
		breakerBoxUI.enabled = true;
		
		player = GameObject.Find ("Chief 1");
		
		Cursor.visible = true;
		
		powerStation = GameObject.Find("Base");
		Base = powerStation.GetComponent<Image>();
		//Base = powerStation.GetComponent<Image>();
		
		for (int i = 0; i < 13; i++)
		{
			selectedImages[i].enabled = false; 
		}
		
		for (int i = 0; i < 14; i++)
		{
			highlightImages[i].enabled = false;
		}
		
		powerButtons = new bool[13];
		
		for(int i = 0; i < 13; i++)
		{
			powerButtons[i] = false;
		}
		
		/*entraceBay = entraceBay.GetComponent<Button>();
        medLab = medLab.GetComponent<Button>();
        commCenter = commCenter.GetComponent<Button>();
        restRoom = restRoom.GetComponent<Button>();
        breakRoom = breakRoom.GetComponent<Button>();
        crewQuarters = crewQuarters.GetComponent<Button>();
        northHallway = northHallway.GetComponent<Button>();
        materialsLab = materialsLab.GetComponent<Button>();
        bioLab = bioLab.GetComponent<Button>();
        ghostLab = ghostLab.GetComponent<Button>();
        eastShaft = eastShaft.GetComponent<Button>();
        messHall = messHall.GetComponent<Button>();
        kitchen = kitchen.GetComponent<Button>();
        sendPower = sendPower.GetComponent<Button>();*/
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			breakerBoxUI.enabled = false;
			Cursor.visible = false;
		}
		
	}
	
	public void roomMethods(int r)
	{
		powerButtons[r] = !powerButtons[r];
		
		selectedImages[r].enabled = !selectedImages[r].enabled;
	}
	
	public void imageHilight(int r)
	{
		highlightImages[r].enabled = true;
	}
	
	public void imageExit(int r)
	{
		highlightImages[r].enabled = false;
	}
	
	public void sendPowerMethod()
	{
		roomControllerOther = GetComponent<RoomController>();
		
		for (int i = 0; i < 13; i++)
		{
			if (powerButtons[i] == true)
			{
				roomControllerOther.rooms[i].toggleLight();
			}
		}
	}
	
	public void breakerBoxExit()
	{
		for (int i = 0; i < 13; i++) 
		{
			powerButtons[i] = false;
			
			selectedImages[i].enabled = false;
		}
		
		breakerBoxUI.enabled = false;
		(player.GetComponent ("FirstPersonController") as MonoBehaviour).enabled = true;
		Cursor.visible = false;
	}
	
	public void breakerBoxClick()
	{
		breakerBoxUI.enabled = true;
		(player.GetComponent ("FirstPersonController") as MonoBehaviour).enabled = false;
		Cursor.visible = true;
	}
}
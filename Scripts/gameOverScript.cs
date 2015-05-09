using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameOverScript : EventReceiver {

    //public ghostAI ghostAIOther;

    public Canvas gameOver;
    public Button Yes;
    public Button No;

    //public Camera playerCam;
    //public Camera mainMenuCam;

    //GameObject dead;
	public GameObject global;

    string levelName;

	//string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        //dead = GameObject.Find("GameController");

        //ghostAIOther = dead.GetComponent<ghostAI>();

        /*if (ghostAIOther.gotDead() == true)
        {
            gameOver.enabled = true;
			Time.timeScale = 0;
        }

		gameOver.enabled = true;*/
		//Time.timeScale = 0;
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        //so unity editor doesn't complain
        //mainMenuCam = null;
        //playerCam = null;

		//dead = GameObject.Find("GameController");
		//player = GameObject.Find ("Chief 1");
		
		//ghostAIOther = dead.GetComponent<ghostAI>();

        //gameOver = gameOver.GetComponent<Canvas>();

        //default name
        levelName = "loadTools";

		gameOver.enabled = false;
		Cursor.visible = false;
    }

    public void Retry()
    {
        Destroy(global); 
		Cursor.visible = false;
        Application.LoadLevel(levelName);      
        gameOver.enabled = false;   
    }

    public void mainMenu()
    {      
        Destroy(global);

        //mainMenuCam.enabled = true;
        //playerCam.enabled = false; 

		Destroy (gameObject);

        Application.LoadLevel(0);
    }

    //broadcast method
    void Died()
    {
        gameOver.enabled = true;
        Cursor.visible = true;

		(global.GetComponent ("FirstPersonController") as MonoBehaviour).enabled = false;
    }

    //broadcast method
    void levelNameing(string s)
    {
        levelName = s;
    }
}

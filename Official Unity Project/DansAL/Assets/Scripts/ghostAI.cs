using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ghostAI : EventReceiver {

	//https://docs.google.com/spreadsheets/d/1r2QRdUrdkswYt4-aMFmbOhygtG6-MSGl0pyw4tLXRGM/edit?pli=1#gid=0

    /*Base - Entrace Bay 0
      Base - Med Lab 1
      Base - Comm Center 2
      Base - Restroom 3
      Base - Break Room 4
      Base - Crew Quarters 5
      Base - North Hallway 6
      Base - Materials Lab 7
      Base - GHOST Lab 8
      Base - Bio Lab 9
      Base - East Shaft 10
      Base - Mess Hall 11
      Base - Kitchen 12
      Base - Power Station 13
      Base - Tunnel A 14
      Base - Tunnel B 15
      Base - Tunnel C 16*/

    //roomSelected roomSelectedOther;

    RoomController roomsControllerOther;
	int[,] roomMatrix = new int[17,17];

    /*GameObject cube1;
    GameObject cube2;
    GameObject cube3;
    GameObject cube4;
	GameObject player;*/

    //int[] rooms = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
    //int[] adRooms = {3, 4, 9};

    float notChasingTimer;
    float powerStationTimer;
	float killTimer;
	float goofingOffTimer;
    float chasingTimer;

	bool escaped;
	bool isChasing;
    bool tryingToKill;
    bool playerLight;
    bool goofingOff;

    bool gameOver;

    int randomValue;
	int ghostRoomLocation;
    int playerRoomLocation;
    int adjacentRooms;
    int newRoom;


	// Use this for initialization
	void Start () {

		//roomSelectedOther = GetComponent<roomSelected>();
        roomsControllerOther = GetComponent<RoomController>();

        /*cube1 = GameObject.Find("Cube1");
        cube2 = GameObject.Find("Cube2");
        cube3 = GameObject.Find("Cube3");
        cube4 = GameObject.Find("Cube4");
		player = GameObject.Find("player");*/

        //rows will be ghost loaction, columns player location

                                  //0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16
        roomMatrix = new int[,] { {-1,  1,  1,  1,  1,  1,  1,  1, 11, 11, 11, 11, 11, 70, 70, 70, 70},//0
                                  { 0, -1,  2,  3,  2,  3,  2,  2,  2,  0,  0,  0,  0, 70, 70, 70, 70},//1
                                  { 1,  1, -1,  4,  4,  4,  4,  4,  4,  4,  1,  1,  1, 70, 70, 70, 70},//2
                                  { 1,  1,  1, -1,  4,  5,  4,  4,  4,  4,  1,  1,  1, 70, 70, 70, 70},//3
                                  { 2,  2,  2,  3, -1,  5,  6,  6,  6,  6,  6,  2,  2, 70, 70, 70, 70},//4
                                  { 3,  3,  4,  3,  4, -1,  6,  6,  6,  6,  6,  3,  6, 70, 70, 70, 70},//5
                                  { 4,  4,  4,  4,  4,  5, -1,  7,  8,  7,  7,  8,  8, 70, 70, 70, 70},//6
                                  { 9,  6,  6,  6,  6,  6,  6, -1,  8,  9,  9,  9,  9, 70, 70, 70, 16},//7
                                  { 9,  6,  6,  6,  6,  6,  6,  7, -1,  9,  9,  9,  9, 70, 70, 70, 70},//8
                                  {10, 10,  7,  7,  7,  8,  8,  7,  8, -1, 10, 10, 10, 70, 70, 70, 70},//9
                                  {11, 11, 11, 11,  9,  9,  9,  9,  9,  9, -1, 11, 12, 70, 70, 70, 70},//10
                                  { 0,  0,  0,  0,  0,  0, 10, 10, 10, 10, 10, -1, 12, 70, 70, 70, 70},//11
                                  {11, 11, 11, 11, 11, 11, 10, 10, 10, 10, 10, 11, -1, 70, 70, 70, 70},//12
                                  {14, 14, 14, 14, 14, 14, 16, 16, 16, 16, 15, 15, 15, -1, 14, 15, 16},//13
                                  { 1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1, 70, -1, 70, 70},//14
                                  {11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 70, 70, -1, 70},//15
                                  { 7,  7,  7,  7,  7,  7,  7,  7,  7,  7,  7,  7,  7, 70, 70, 70, -1},};//16


        notChasingTimer = 0.0f;
        powerStationTimer = 0.0f;
		killTimer = 0.0f;
		goofingOffTimer = 0.0f;
        chasingTimer = 0.0f;

		escaped = false;
		isChasing = false;
        tryingToKill = false;
        playerLight = true;
        goofingOff = false;

        gameOver = false;

        ghostRoomLocation = 8;
        playerRoomLocation = 0;


		//old testing values
        /*if (randomValue == 0)
        {
            room1Ghost = true;
            //cube1.renderer.material.color = Color.green;
        }
        else if (randomValue == 1)
        {
            room2Ghost = true;
            //cube2.renderer.material.color = Color.green;
        }
        else if (randomValue == 2)
        {
            room3Ghost = true;
            //cube3.renderer.material.color = Color.green;
        }
        else
        {
            room4Ghost = true;
            //cube4.renderer.material.color = Color.green;
        }*/
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region older ai patterns
        //together in room condition
        /*if (Application.loadedLevel == 1 && room1Ghost == true)
        {
            killTimer += Time.deltaTime;
            tryingToKill = true;
            if (killTimer > 5.0)
            {
                gameOver = true;
            }
        }
        else if (Application.loadedLevel == 2 && room2Ghost == true)
        {
            killTimer += Time.deltaTime;
            tryingToKill = true;
            if (killTimer > 5.0)
            {
                gameOver = true;
            }
        }
        else if (Application.loadedLevel == 3 && room3Ghost == true)
        {
            killTimer += Time.deltaTime;
            tryingToKill = true;
            if (killTimer > 5.0)
            {
                gameOver = true;
            }
        }
        else
            tryingToKill = false;*/

		//chasing conditions
        /*if (Application.loadedLevel == 1 && roomSelectedOther.lights == 1)
        {
            isChasing = true;
        }
        else if (Application.loadedLevel == 2 && roomSelectedOther.lights == 2)
        {
            isChasing = true;
        }
        else
            isChasing = false;*/

        //change ai pattern
        #endregion        

        //not chasing conditions
	      //maybe a minute or two move condition

        //using these for testing
        if (Input.GetKeyDown (KeyCode.Space))
        {
            playerLight = !playerLight;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            playerRoomLocation = 0;
        }

        //Debug.Log(tryingToKill);
        if (tryingToKill == false && goofingOff == false)
        {
                if (playerLight == true)
                {
                    notChasingTimer += Time.deltaTime;

                    //Debug.Log(notChasingTimer);

                    if (notChasingTimer > 5.0)
                    {

                        if (ghostRoomLocation != 13 && (ghostRoomLocation == 14 || ghostRoomLocation == 15 || ghostRoomLocation == 16))
                        {
                            do
                            {                               
                                randomValue = Random.Range(0, roomsControllerOther.rooms[ghostRoomLocation].connectedRooms.Length);

                                ghostRoomLocation = roomsControllerOther.rooms[ghostRoomLocation].connectedRooms[randomValue];    
                     
                            } while (ghostRoomLocation >= 13);

                            ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
                            notChasingTimer = 0.0f;
							chasingTimer = 0.0f;
                        }
                        else if (ghostRoomLocation == 14 || ghostRoomLocation == 15 || ghostRoomLocation == 16)
                        {
                            do
                            {
                                randomValue = Random.Range(0, roomsControllerOther.rooms[ghostRoomLocation].connectedRooms.Length);

                                ghostRoomLocation = roomsControllerOther.rooms[ghostRoomLocation].connectedRooms[randomValue];

                            } while (ghostRoomLocation == 13);
                            ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
                            chasingTimer = 0.0f;
                        }
                        else
                        {
                            randomValue = Random.Range(0, 3);

                            ghostRoomLocation = roomsControllerOther.rooms[ghostRoomLocation].connectedRooms[randomValue];
                            notChasingTimer = 0.0F;
                            chasingTimer = 0.0f;

                            ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
                        }

                        //Debug.Log(randomValue);
                    }
                }
                else
                {
                    //ghost chasing code going here.

                    chasingTimer += Time.deltaTime;

                    if (chasingTimer > 5.0)
                    {
                        ghostRoomLocation = roomMatrix[ghostRoomLocation, playerRoomLocation];
                        ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
                        chasingTimer = 0.0f;
                        notChasingTimer = 0.0f;
						ghostKill();
                    }
                }
                //cube1.renderer.material.color = Color.green;
                //timer = 0;
        }
        else if (goofingOff == true && tryingToKill == false)
        {
            goofingOffTimer += Time.deltaTime;

            if (goofingOffTimer > 5.0)
            {
                ghostRoomLocation = 13;
                ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
                goofingOff = false;   
            }
        }
        else
        {
            killTimer += Time.deltaTime;

            if (killTimer > 5.0)
            {
                Debug.Log("Dead");
                gameOver = true;
            }

            notChasingTimer = 0.0f;
        }

        if ((playerRoomLocation == 13 || playerRoomLocation == 14 || playerRoomLocation == 15 || playerRoomLocation == 16) && tryingToKill == false && goofingOff == false)
        {
            goofingOffTimer += Time.deltaTime;

            if (goofingOffTimer > 5.0)
            {
                randomValue = Random.Range(0, 3);

                if (randomValue == 0)
                {
                    ghostRoomLocation = 14;
                    goofingOff = true;
                    goofingOffTimer = 0.0f;
                    notChasingTimer = 0.0f;
                    chasingTimer = 0.0f;
                    ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
					ghostKill();
                }
                else if (randomValue == 2)
                {
                    ghostRoomLocation = 15;
                    goofingOff = true;
                    goofingOffTimer = 0.0f;
                    notChasingTimer = 0.0f;
                    chasingTimer = 0.0f;
                    ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
					ghostKill();
                }
                else
                {
                    ghostRoomLocation = 16;
                    goofingOff = true;
                    goofingOffTimer = 0.0f;
                    notChasingTimer = 0.0f;
                    chasingTimer = 0.0f;
                    ExecuteEvents.Execute<IMessageTarget>(this.gameObject, null, (x, y) => x.onGHOSTMove(ghostRoomLocation));
					ghostKill();
                }

            }
        }
        else
        {
            //Debug.Log("goofing off");
            goofingOff = false;
            goofingOffTimer = 0.0f;
        }
    }

    public void ghostKill()
    {
        if (playerRoomLocation == ghostRoomLocation)
        {
            tryingToKill = true;
            killTimer = 0.0f;
        }
        else
            tryingToKill = false;
    }

    public override void onGHOSTMove(int r)
    {
        Debug.Log("ghost Moved to " + r);
    }

	public override void onEnterDarkRoom()
	{
		playerLight = false;
		ghostKill();
	}

	public override void onEnterLightRoom()
	{
		playerLight = true;
		ghostKill ();
	}

    public bool gotDead()
    {
        return gameOver;
    }

}

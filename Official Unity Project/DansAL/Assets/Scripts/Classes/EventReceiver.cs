using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IMessageTarget : IEventSystemHandler
{
	// functions that can be called via the messaging system
	void onRoomWentDark(int r);
	void onGHOSTMove(int r);

	void onChangeRoom(int r);
	void onEnterDarkRoom();
	void onEnterLightRoom();
	void onAdjacentToGhost();
	void onAwayFromGhost();

	void onItemCollect(int value);
	void onQuotaMet();
	void onAllItems();

	void onPlayDialogue (int id);
	void onDialogueEnd();

	void onDoorClick(int r, Vector3 pos, Vector3 rot);
	void onItemClick (int id, int value);

	void sendPower (int r);



}

public class EventReceiver : MonoBehaviour, IMessageTarget {

	//Declare empty virtual functions

	//Changes in environment
	public virtual void onRoomWentDark(int r){}
	public virtual void onGHOSTMove(int r){}

	//Player movement
	public virtual void onChangeRoom(int r){}
	public virtual void onEnterDarkRoom(){}
	public virtual void onEnterLightRoom(){}
	public virtual void onAdjacentToGhost(){}	//Player room is adjacent to GHOST room
	public virtual void onAwayFromGhost(){}		//Player room is NOT adjacent to GHOST room

	//Item events
	public virtual void onItemCollect(int value){}
	public virtual void onQuotaMet(){}
	public virtual void onAllItems(){}

	//Radio events
	public virtual void onPlayDialogue(int id){}
	public virtual void onDialogueEnd(){}

	//Misc
	public virtual void onDoorClick(int r, Vector3 pos, Vector3 rot){}	//Player has clicked on a door
	public virtual void onItemClick(int id, int value){}					//Player has clicked on an item

	public virtual void sendPower(int r);

}
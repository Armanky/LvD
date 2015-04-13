using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IMessageTarget : IEventSystemHandler
{
	// functions that can be called via the messaging system

	void onGHOSTMove(int r);

	void onChangeRoom(Room r);
	void onEnterDarkRoom();
	void onEnterLightRoom();
	void onAdjacentToGhost();
	void onAwayFromGhost();

	void onItemCollect(int value);
	void onQuotaMet();
	void onAllItems();

	void onPlayDialogue (int id);
	void onDialogueEnd();




}

public class EventReceiver : MonoBehaviour, IMessageTarget {

	//Declare empty virtual functions

	//Changes in environment

	public virtual void onGHOSTMove(int r){}

	//Player movement
	public virtual void onChangeRoom(Room r){}
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


	

}
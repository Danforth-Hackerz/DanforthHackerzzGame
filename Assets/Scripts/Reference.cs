using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to store static refrences to obejcts in the scene
public class Reference : Singleton<Reference>
{
    //In scene refrences
    public PlayerInventoryManager PIM;
    public GameObject player;
    public Room currentRoom;

    //Static refrences
    //public static PlayerInventoryManager PIM;
    //public static GameObject player;
    //public static Room currentRoom;

    ////Method called on scene load
    //new private void Awake()
    //{
    //    //Assigns static refrences to the in scene refrences
    //    PIM = _PIM;
    //    player = _player;
    //    currentRoom = startRoom;
    //}
}

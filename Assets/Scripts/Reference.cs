using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to store static refrences to obejcts in the scene
public class Reference : MonoBehaviour
{
    //In scene refrences
    [SerializeField] private PlayerInventoryManager _PIM;
    [SerializeField] private GameObject _player;
    [SerializeField] private Room startRoom;

    //Static refrences
    public static PlayerInventoryManager PIM;
    public static GameObject player;
    public static Room currentRoom;

    //Method called on scene load
    private void Start()
    {
        //Assigns static refrences to the in scene refrences
        PIM = _PIM;
        player = _player;
        currentRoom = startRoom;
    }
}

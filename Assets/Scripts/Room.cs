using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to control each room
public class Room : MonoBehaviour
{
    //GameObject containing all the gameobjects in the room
    [SerializeField] private GameObject container;

    //Method to show the room
    public virtual void Show()
    {
        container.SetActive(true);
    }

    //Method to hide the room
    public virtual void Hide()
    {
        container.SetActive(false);
    }
}

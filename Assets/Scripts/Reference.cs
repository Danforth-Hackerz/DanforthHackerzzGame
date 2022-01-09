using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to store static refrences to obejcts in the scene
public class Reference : MonoBehaviour
{
    //In scene refrences
    [SerializeField] private PlayerInventoryManager _PIM;

    //Static refrences
    public static PlayerInventoryManager PIM;

    //Method called on scene load
    private void Start()
    {
        //Assigns static refrences to the in scene refrences
        PIM = _PIM;
    }
}

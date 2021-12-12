using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHolder : MonoBehaviour
{
    //Class to store instance data for Player inventory manager
    public class PlayerInventoryData
    {
        public float bloomIntensity { get; private set; } //how much the closest object should glow
        public float bloomTransitionSpeed { get; private set; } //how long the object glow animation should take (Intensity change per second)
        public float interactDistance { get; private set; } //how close the player needs to be to objects to pick them up
        public int maxItems { get; private set; } //Stores the max amount of items the user can have in their inventory
        public List<CollectableItem> items { get; private set; } //Stores all the items in the players inventory

        //Constructor which transfers data from PIM to instance variables
        public PlayerInventoryData(PlayerInventoryManager PIM)
        {
            bloomIntensity = PIM.GetBloomIntenisty();
            bloomTransitionSpeed = PIM.bloomTransitionSpeed;
            interactDistance = PIM.interactDistance;
            maxItems = PIM.GetMaxItems();
            items = PIM.GetItems();
        }
    }

    //Static refrence to PID
    public static PlayerInventoryData PID;
}

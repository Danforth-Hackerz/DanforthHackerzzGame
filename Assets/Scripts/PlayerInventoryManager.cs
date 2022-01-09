using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] PlayerInventoryUI playerInventoryUI; //reference to the inventory UI, so the player can send game objects that they collect to the inventory display
    [SerializeField] float bloomIntensity; //how much the closest object should glow
    [SerializeField] public float bloomTransitionSpeed; //how long the object glow animation should take (Intensity change per second)
    [SerializeField] public float interactDistance = 5; //how close the player needs to be to objects to pick them up
    public static float closestObjectDistance;
    public static GameObject closestObject = null;
    GameObject prevObj = null;

    [SerializeField] private int maxItems; //Stores the max amount of items the user can have in their inventory
    private List<CollectableItem> items; //Stores all the items in the players inventory

    // Start is called before the first frame update
    void Start()
    {
        //Initializes items
        items = new List<CollectableItem>();

        //Loads from data if data is set
        if (VariableHolder.PID != null)
        {
            LoadFromData(VariableHolder.PID);
        }
        //Otherwise set data to base values
        else
        {
            VariableHolder.PID = new VariableHolder.PlayerInventoryData(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Picks up the closest item if the user presses E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        //Drops the item if you hit the associated button
        for (int i = 0; i < maxItems; i++)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i)))
            {
                RemoveItem(i);
            }
        }
    }

    void FixedUpdate()
    {
        if (prevObj != closestObject && prevObj != null)
        {
            //Animates the bloom if the previous closest object if it changed
            //StartCoroutine(Animations.TransitionBloom(prevObj.GetComponent<CollectableItem>(), bloomIntensity, 1, bloomTransitionTime));
            prevObj.GetComponent<Interactable>().SetTargetIntensity(1);
            //Debug.Log("Reset Glow");

        }

        if (closestObject != prevObj && closestObject != null)
        {
            //Animates the bloom on the closest object if it changed
            //StartCoroutine(Animations.TransitionBloom(closestObject.GetComponent<CollectableItem>(), 1, bloomIntensity, bloomTransitionTime));
            closestObject.GetComponent<Interactable>().SetTargetIntensity(bloomIntensity);
            //Debug.Log("Glow");
        }

        //Resets variables at the end of fixed update before onTrigger functions are called
        prevObj = closestObject;
        closestObjectDistance = interactDistance;
        closestObject = null;
    }

    //Method called to pick up an item
    private void Interact()
    {
        //Returns if there is no object to pick up or the users inventory is already full
        if (closestObject == null)
        {
            Debug.Log("Here");
            return;
        }

        closestObject.GetComponent<Interactable>().Interact();
        VariableHolder.PID = new VariableHolder.PlayerInventoryData(this);
    }

    public void PickUp(CollectableItem item)
    {
        if (items.Count == maxItems)
        {
            Debug.Log("Your inventory is full");
            return;
        }

        //Adds the item to the inventory list (of the actual gameObjects which become disabled)
        items.Add(item);

        //Adds the item to the UI
        playerInventoryUI.AddItem(item.gameObject);

        //Disables the gameobject
        closestObject.SetActive(false);
    }

    private void RemoveItem(int index)
    {
        //Returns if the player tries to drop an item they don't have
        if (index >= items.Count)
        {
            return;
        }

        items[index].gameObject.transform.position = transform.position - new Vector3(0, 1);
        items[index].gameObject.SetActive(true);

        playerInventoryUI.RemoveItem(index);

        items.RemoveAt(index);
    }

    //Getters for private instance variables
    public List<CollectableItem> GetItems()
    {
        return items;
    }

    public int GetMaxItems()
    {
        return maxItems;
    }

    public float GetBloomIntenisty()
    {
        return bloomIntensity;
    }

    //Method to load player inventory manager from static data
    public void LoadFromData(VariableHolder.PlayerInventoryData PID)
    {
        bloomIntensity = PID.bloomIntensity;
        bloomTransitionSpeed = PID.bloomTransitionSpeed;
        maxItems = PID.maxItems;
        items = PID.items;
        interactDistance = PID.interactDistance;
    }
}

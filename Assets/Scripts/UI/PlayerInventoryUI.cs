using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : UI
{
    [SerializeField] GameObject itemBox;

    //Set to serialize field temporarily so that you can mess around with different sizes (remove once sizes are decided)
    [SerializeField] Vector2 boxScale = new Vector2(1, 1);
    Vector2 boxSize;
    [SerializeField] Vector2 marginSize = new Vector2(20, 20);

    //Stores all the items in the players inventory
    List<GameObject> items = new List<GameObject>();

    //temp object for testing
    [SerializeField] GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        boxSize = new Vector2(100, 100) * boxScale;
    }

    // Update is called once per frame
    void Update()
    {
        //temp testing so that you can modify ui while running the game 
        //Expensive and unneccesary so remove once no longer needed
        boxSize = new Vector2(100, 100) * boxScale;
        UpdateInventoryUI();
        foreach (GameObject item in items)
        {
            item.transform.localScale = boxScale; //sets the size of the box to match box size variable
        }

        //Test to make sure system works
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(test);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem("Test");
        }
    }

    //Updates the positions of the inventory items based on the items list
    //Not implementing vertical shifting yet (or maybe lower the size of the elements if there are too many)
    public void UpdateInventoryUI()
    {
        if (items.Count == 0)
        {
            return;
        }

        float currentItemX;
        if (items.Count % 2 == 0)
        {
            //If even number of objects
            //Calculate the x of the first item
            currentItemX = -(((items.Count / 2) - 0.5f) * (boxSize.x + marginSize.x));
        }
        else
        {
            //If odd number of objects
            //Calculate the x of the first item
            currentItemX = -(items.Count / 2 * (marginSize.x + boxSize.x));
        }
        //Debug.Log(currentItemX);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition = new Vector3(currentItemX, 0, 0);
            currentItemX += boxSize.x + marginSize.x;
        }
    }

    public void AddItem(GameObject item)
    {
        GameObject newItem = Instantiate(itemBox); //Creates a new item based on the item box prefab
        newItem.transform.SetParent(container.transform, false); //Sets the parent to the container
        newItem.GetComponent<InventoryItem>().itemName = item.GetComponent<CollectableItem>().itemName;
        newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite; //sets the sprite of the emtpy image to the sprite of the item we are adding
        newItem.transform.localScale = boxScale; //sets the size of the box to match box size
        items.Add(newItem); //Add item to list
        UpdateInventoryUI(); //Update ui
    }

    public void RemoveItem(string name)
    {
        int index = IndexOfItem(name);
        Destroy(items[index]); //Destroys the gameobject

        //Debug.Log("Item removed: " + items[index]);

        items.RemoveAt(index); //Removes reference to the object (It's still in the list)
        UpdateInventoryUI(); //update Ui
    }

    public bool ContainsItem(string name)
    {
        foreach (GameObject item in items)
        {
            if (item.GetComponent<InventoryItem>().itemName == name)
            {
                return true;
            }
        }
        return false;
    }

    public int IndexOfItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetComponent<InventoryItem>().itemName == name)
            {
                return i;
            }
        }
        Debug.LogError("No item with name: " + name);
        return -1;
    }

    /*
    //Works but is not needed
    public GameObject FirstItemWithTag(string tag)
    {
        foreach (GameObject item in items)
        {
            if (item.CompareTag(tag))
            {
                return item;
            }
        }

        Debug.LogError("No item with tag " + tag);
        return null;
    }
    */
}

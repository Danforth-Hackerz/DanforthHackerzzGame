using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] GameObject playerInventoryUI; //reference to the inventory UI, so the player can send game objects that they collect to the inventory display
    [SerializeField] float bloomIntensity; //how much the closest object should glow
    [SerializeField] public float pickUpDistance = 5; //how close the player needs to be to objects to pick them up
    public static float closestObjectDistance;
    public static GameObject closestObject = null;
    GameObject prevObj = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if(prevObj != null)
        {
            prevObj.GetComponent<CollectableItem>().SetIntesity(1);
        }

        //Do something with the closest object
        if (closestObject != null)
        {
            closestObject.GetComponent<CollectableItem>().SetIntesity(bloomIntensity);
        }

        //Resets variables at the end of fixed update before onTrigger functions are called
        prevObj = closestObject;

        closestObjectDistance = pickUpDistance;
        closestObject = null;
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {

        //Checks if the object is collectable and closer than the current closest object
        if (other.CompareTag("Collectable") && Vector3.Distance(transform.position, other.transform.position) < closestObjectDistance)
        {
            //Performs a raycast to make sure that you have direct line of sight with the object
            //Stops you from collecting items through walls
            Vector2 direction = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pickUpDistance);
 
            if (hit.collider.gameObject == other.gameObject)
            {
               closestObject = other.gameObject;
               closestObjectDistance = ((Vector2)(other.transform.position - transform.position)).magnitude;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Checks if the object is collectable and closer than the current closest object
        if (other.CompareTag("Collectable") && Vector3.Distance(transform.position, other.transform.position) < closestObjectDistance)
        {
            //Performs a raycast to make sure that you have direct line of sight with the object
            //Stops you from collecting items through walls
            Vector2 direction = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pickUpDistance, ~player);

            Debug.DrawRay(transform.position, direction * pickUpDistance);

            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.gameObject == other.gameObject)
            {
                closestObject = other.gameObject;
                closestObjectDistance = ((Vector2)(other.transform.position - transform.position)).magnitude;
            }
        }
    }
    */
}

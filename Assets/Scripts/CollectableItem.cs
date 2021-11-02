using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    LayerMask ignoreCollectable; //layerMask is needed so raycast does not immediately collide with the item it's originating from
    SpriteRenderer _renderer;
    CircleCollider2D trigger;
    float pickUpDistance;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<CircleCollider2D>();

        //Warning: This assumes that the collection distance of the player never changes, so if that is a feature added into the game make sure to change this
        //finds the player and gets the pickUpDistance
        pickUpDistance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryManager>().pickUpDistance * (1 / Mathf.Max(transform.lossyScale.x, transform.lossyScale.y)); 
        trigger.radius = pickUpDistance;

        _renderer = GetComponent<SpriteRenderer>();
        ignoreCollectable = ~LayerMask.GetMask("Collectable"); //the tilde inverts the layermask from only the collectable layer to all other layers
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIntesity(float intensity)
    {
        _renderer.material.SetColor("_Color", Color.white * intensity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if the player entered the trigger and if the distance is less than the current closest object
        if (other.CompareTag("Player") && Vector2.Distance(transform.position, other.transform.position) < PlayerInventoryManager.closestObjectDistance)
        {
            //raycast to make sure there is line of sight to the player (avoid detection through walls)
            Vector2 direction = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pickUpDistance, ignoreCollectable);

            Debug.DrawRay(transform.position, direction * pickUpDistance);

            //Debug.Log(hit.collider.gameObject.name);

            //If you hit the player
            if (hit.transform.CompareTag("Player"))
            {
                PlayerInventoryManager.closestObject = gameObject;
                PlayerInventoryManager.closestObjectDistance = Vector2.Distance(transform.position, other.transform.position);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //checks if the player entered the trigger and if the distance is less than the current closest object
        if (other.CompareTag("Player") && Vector2.Distance(transform.position, other.transform.position) < PlayerInventoryManager.closestObjectDistance)
        {
            //raycast to make sure there is line of sight to the player (avoid detection through walls)
            Vector2 direction = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pickUpDistance, ignoreCollectable);

            Debug.DrawRay(transform.position, direction * pickUpDistance);

            //Debug.Log(hit.collider.gameObject.name);

            //If you hit the player
            if (hit.transform.CompareTag("Player"))
            {
                PlayerInventoryManager.closestObject = gameObject;
                PlayerInventoryManager.closestObjectDistance = Vector2.Distance(transform.position, other.transform.position);
            }
        }
    }
}

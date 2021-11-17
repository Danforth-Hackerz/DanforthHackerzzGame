using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    bool destroyOnTargetScale = false;
    Vector3 targetPosition;
    Vector3 originalScale = Vector3.one; //Original scale should only be changed to change ratios between width and height (I think)
    float targetScale;
    float translateSpeed;
    float scaleSpeed;

    //For Debug Testing
    //double startTime;

    // Start is called before the first frame update
    void Start()
    {
        //Gets animation speeds from player inventory manager script
        PlayerInventoryUI inventoryScript = GameObject.Find("Player Inventory").GetComponent<PlayerInventoryUI>();
        translateSpeed = inventoryScript.translateSpeed;
        scaleSpeed = inventoryScript.scaleSpeed;

        //Void Start is consistently called after target position is set in the updateInventory method
        //You can just set the position to the target position one time when the object is instantiated
        //This is probably bad practice and should be changed to a better system
        //Fixes an issue where the object always starts in the center and moves to it's target position
        transform.localPosition = targetPosition;
        
        
        //originalScale = transform.localScale;
        //ResetTargets();


    }

    // Update is called once per frame
    void Update()
    {
        //Adjust position if neccessary
        if (transform.localPosition != targetPosition)
        {
            if (Vector2.Distance(transform.localPosition, targetPosition) <= translateSpeed * Time.deltaTime)
            {
                transform.localPosition = targetPosition;
            }
            else
            {
                Vector3 direction = ((Vector2)(targetPosition - transform.localPosition)).normalized;
                transform.localPosition += translateSpeed * Time.deltaTime * direction;
            }
        }

        //Adjusts scale if neccessary
        //I think that a coroutine might be better (though this is better if you pick up an item then immediately drop it)
        if (transform.localScale.x != targetScale * originalScale.x)
        {
            //Debug.Log("Time: " + (Time.realtimeSinceStartup - startTime));
            if (Mathf.Abs((targetScale * originalScale.x) - transform.localScale.x) <= scaleSpeed * originalScale.x * Time.deltaTime)
            {
                if (destroyOnTargetScale)
                {
                    Destroy(gameObject);
                }

                //Weird math is to keep the z scale 1 always (Casting to a vector2, then vector3, removes the last digit, then add 0,0,1 to set last digit to 1)
                transform.localScale = (Vector3)(targetScale * (Vector2)originalScale) + Vector3.forward;
            }
            else
            {
                //converts to vector2 then to vector3 do discard the z amount
                transform.localScale += (Vector3)(Mathf.Sign(targetScale - transform.localScale.x / originalScale.x) * scaleSpeed * Time.deltaTime * (Vector2)originalScale);
            }
        }

        //Testing
        if (transform.localScale.x != targetScale * originalScale.x && transform.localPosition != targetPosition)
        {

        }
    }

    public void SetTargetScale(float scaleMultiplier, bool destroy)
    {
        targetScale = scaleMultiplier;
        destroyOnTargetScale = destroy;

        //Temporary testing
        //startTime = Time.realtimeSinceStartup;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        Debug.Log("Set Position " + position);
    }

    public void ResetTargets()
    {
        targetPosition = transform.localPosition;
        targetScale = transform.localScale.x / originalScale.x;
    }
}

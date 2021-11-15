using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    bool destroyOnTargetScale = false;
    Vector3 targetPosition;
    Vector3 originalScale;
    float targetScale;
    float translateSpeed;
    float scaleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //gets animation speeds from player inventory manager script
        PlayerInventoryUI inventoryScript = GameObject.Find("Player Inventory").GetComponent<PlayerInventoryUI>();
        translateSpeed = inventoryScript.translateSpeed;
        scaleSpeed = inventoryScript.scaleSpeed;

        originalScale = transform.localScale;
        ResetTargets();
    }

    // Update is called once per frame
    void Update()
    {
        //adjust position if neccessary
        if (transform.position != targetPosition)
        {
            if (Vector3.Distance(transform.position, targetPosition) <= translateSpeed * Time.deltaTime)
            {
                transform.position = targetPosition;
            }
            else
            {
                Vector3 direction = ((Vector2)(targetPosition - transform.position)).normalized;
                transform.position += direction * translateSpeed * Time.deltaTime;
            }
        }

        //adjusts scale if neccessary
        //kind of scuffed (plus the destroyOnTargetScale makes this break when changing scale while changing down to 0(though this shouldn't be done based on current uses))
        //I think that a coroutine might be better (though this is better if you pick up an item then immediately drop it)
        if (transform.localScale.x != targetScale * originalScale.x)
        {
            if (Mathf.Abs((targetScale * originalScale.x) - transform.localScale.x) <= scaleSpeed * originalScale.x * Time.deltaTime)
            {
                if (destroyOnTargetScale)
                {
                    Destroy(gameObject);
                }

                //weird math is to keep the z scale 1 always
                transform.localScale = (Vector3)(targetScale * (Vector2)originalScale) + Vector3.forward;
            }
            else
            {
                //converts to vector2 then to vector3 do discard the z amount
                transform.localScale += (Vector3)(Mathf.Sign(targetScale - transform.localScale.x / originalScale.x)* scaleSpeed * Time.deltaTime * (Vector2)originalScale);
            }
        }
    }

    public void SetTargetScale(float scaleMultiplier, bool destroy)
    {
        targetScale = scaleMultiplier;
        destroyOnTargetScale = destroy;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void ResetTargets()
    {
        targetPosition = transform.position;
        targetScale = transform.localScale.x / originalScale.x;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Interactable : MonoBehaviour
{
    private float bloomTransitionSpeed; //(intensity change per second)
    private float targetIntensity = 1;

    protected CircleCollider2D trigger;
    protected SpriteRenderer _renderer;
    protected LayerMask ignoreCollectable; //layerMask is needed so raycast does not immediately collide with the item it's originating from
    protected float interactDistance;

    [SerializeField] protected float intensityMultiplier;

    public bool isInteractable { get; private set; }

    private void Start()
    {
        trigger = GetComponent<CircleCollider2D>();

        //Warning: This assumes that the collection distance of the player never changes, so if that is a feature added into the game make sure to change this
        //finds the player and gets the interactDistance
        interactDistance = Reference.PIM.interactDistance * (1 / Mathf.Max(transform.lossyScale.x, transform.lossyScale.y));
        bloomTransitionSpeed = Reference.PIM.bloomTransitionSpeed;

        trigger.radius = interactDistance;

        _renderer = GetComponent<SpriteRenderer>();
        ignoreCollectable = ~LayerMask.GetMask("Collectable"); //the tilde inverts the layermask from only the collectable layer to all other layers

        //Calls start for inherited objects
        IStart();
    }

    protected virtual void IStart() { }

    // Update is called once per frame
    private void Update()
    {
        float intensity = _renderer.material.color.r;
        if (intensity != targetIntensity)
        {
            float sign = Mathf.Sign(targetIntensity - intensity);
            _renderer.material.color += Color.white * sign * bloomTransitionSpeed * intensityMultiplier * Time.deltaTime;
            intensity = _renderer.material.color.r;

            if ((sign > 0 && intensity > targetIntensity) || (sign < 0 && intensity < targetIntensity))
            {
                _renderer.material.color = Color.white * targetIntensity;
            }
        }

        //Calls update for inherited objects
        IUpdate();
    }

    protected virtual void IUpdate() { }

    // Sets the target intensity
    public void SetTargetIntensity(float intensity)
    {
        if (intensity != 1)
        {
            targetIntensity = intensity * intensityMultiplier;
        }
        else
        {
            targetIntensity = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if the player entered the trigger and if the distance is less than the current closest object
        if (other.CompareTag("Player") && Vector2.Distance(transform.position, other.transform.position) < PlayerInventoryManager.closestObjectDistance)
        {
            //raycast to make sure there is line of sight to the player (avoid detection through walls)
            //Head
            Vector3 headPosition = other.transform.position + Vector3.up * 0.85f;
            Vector2 directionHead = ((Vector2)(headPosition - transform.position)).normalized;
            RaycastHit2D hitHead = Physics2D.Raycast(transform.position, directionHead, Reference.PIM.interactDistance, ignoreCollectable);

            //Debug.Log("Head: " + headPosition + " " + directionHead);
            Debug.DrawRay(transform.position, directionHead * Reference.PIM.interactDistance);

            //Feet
            Vector3 feetPosition = other.transform.position + Vector3.down * 0.95f;
            Vector2 directionFeet = ((Vector2)(feetPosition - transform.position)).normalized;
            RaycastHit2D hitFeet = Physics2D.Raycast(transform.position, directionFeet, Reference.PIM.interactDistance, ignoreCollectable);

            Debug.DrawRay(transform.position, directionFeet * Reference.PIM.interactDistance);

            //Middle/Origin of player
            Vector2 directionMiddle = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, directionMiddle, Reference.PIM.interactDistance, ignoreCollectable);

            //Debug.Log("Middle: " + other.transform.position + " " + directionMiddle);
            Debug.DrawRay(transform.position, directionMiddle * Reference.PIM.interactDistance);

            //Debug.Log(hit.collider.gameObject.name);

            //If you hit the player
            if (hitHead.transform.CompareTag("Player") || hitFeet.transform.CompareTag("Player") || hitMiddle.transform.CompareTag("Player"))
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
            Vector3 headPosition = other.transform.position + Vector3.up * 0.85f;
            Vector2 directionHead = ((Vector2)(headPosition - transform.position)).normalized;
            RaycastHit2D hitHead = Physics2D.Raycast(transform.position, directionHead, Reference.PIM.interactDistance, ignoreCollectable);

            //Debug.Log("Head: " + headPosition + " " + directionHead);
            Debug.DrawRay(transform.position, directionHead * Reference.PIM.interactDistance);

            Vector3 feetPosition = other.transform.position + Vector3.down * 0.95f;
            Vector2 directionFeet = ((Vector2)(feetPosition - transform.position)).normalized;
            RaycastHit2D hitFeet = Physics2D.Raycast(transform.position, directionFeet, Reference.PIM.interactDistance, ignoreCollectable);

            Debug.DrawRay(transform.position, directionFeet * Reference.PIM.interactDistance);

            Vector2 directionMiddle = ((Vector2)(other.transform.position - transform.position)).normalized;
            RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, directionMiddle, Reference.PIM.interactDistance, ignoreCollectable);

            //Debug.Log("Middle: " + other.transform.position + " " + directionMiddle);
            Debug.DrawRay(transform.position, directionMiddle * Reference.PIM.interactDistance);

            //Debug.Log(hit.collider.gameObject.name);

            //If you hit the player
            if (hitHead.transform.CompareTag("Player") || hitFeet.transform.CompareTag("Player") || hitMiddle.transform.CompareTag("Player"))
            {
                PlayerInventoryManager.closestObject = gameObject;
                PlayerInventoryManager.closestObjectDistance = Vector2.Distance(transform.position, other.transform.position);
            }
        }
    }

    //Called when the uesr tries to interact with the object
    public virtual void Interact()
    {
        Debug.LogError("The player tried to interact with this item, but the method was not overriden");
    }
}

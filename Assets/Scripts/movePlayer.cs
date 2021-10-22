using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {

    }

    //FixedUpdate is called every physics update, anything involving physics should go in here
    void FixedUpdate()
    {
        //Gets the inputs from the player and stores them in floats (they should be -1, 0, or 1)
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        //only move player if they are actually pressing inputs
        if (vertical != 0 || horizontal != 0)
        {
            //normalize the vector so that they player doesn't move faster diagonally
            Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

            //Applies movement to rigid body (multiply by fixed delta time so that movement speed isn't affected if we change the physics frame rate)
            playerRB.MovePosition(playerRB.position + (moveDirection * speed * Time.fixedDeltaTime));

        }
    }


}

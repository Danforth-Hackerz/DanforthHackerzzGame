using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private CameraController cameraController;
    Rigidbody2D playerRB;
    Animator playerAnim;

    //store movement direction
    float vertical = 0;
    float horizontal = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Gets components from player gameobject and stores them in variables
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    //Called once per frame
    private void Update()
    {
        //Gets the inputs from the player and stores them in floats (they should be -1, 0, or 1)
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        //Walking Animation Logic 
        int walkDirection; //0 = idle, 1 = up, 2 = right, 3 = down, 4 = left

        if (horizontal < 0)//left
        {
            walkDirection = 4;
        }
        else if (horizontal > 0)//right
        {
            walkDirection = 2;
        }
        else if (vertical > 0)//up
        {
            walkDirection = 1;
        }
        else if (vertical < 0)//down
        {
            walkDirection = 3;
        }
        else //idle
        {
            walkDirection = 0;
        }

        //Sends the walk direction to the animator (the animator logic will determine what animation is played)
        playerAnim.SetInteger("Walk Direction", walkDirection);

    }

    //FixedUpdate is called every physics update, anything involving physics should go in here
    void FixedUpdate()
    {
        //Gets the inputs from the player and stores them in floats (they should be -1, 0, or 1)
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        //only move player if they are actually pressing inputs
        if (vertical != 0 || horizontal != 0)
        {
            //normalize the vector so that they player doesn't move faster diagonally
            Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

            //Applies movement to rigid body (multiply by fixed delta time so that movement speed isn't affected if we change the physics frame rate)
            playerRB.MovePosition(playerRB.position + (moveDirection * speed * Time.fixedDeltaTime));
            
            //Update camera position
            cameraController.OnPlayerPositionChanged(transform.position);
        }
    }


}

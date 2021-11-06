using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UI 
{
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pauses the game if escape is pressed
        if (Input.GetKey(KeyCode.Escape)){
            isPaused = !isPaused;
            if (isPaused)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}

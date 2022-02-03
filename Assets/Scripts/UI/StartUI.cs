using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    
//Class used to control the UI at the start of the game
public class StartUI : UI
{
    
    //Method called when the user clicks to start
    public void OnClickStart()
    {
        CursorController.cursor.LockCursor();
        //Loads the main scene
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    //Method called when the user clicks to quit
    public void OnClickQuit()
    {
        //Quits the application, doesn't work in the editor so a debug is added to validate that it is working
        Application.Quit();
        Debug.LogWarning("Application could not quit because it is being run in the editor");
    }
}

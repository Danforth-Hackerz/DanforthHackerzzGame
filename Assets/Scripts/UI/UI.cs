using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Inherited base class, used for all UI in the game
public abstract class UI : MonoBehaviour
{
    //Gameobject which contains all the UI elements, allows for the UI to be disabled while this script is still active
    [SerializeField] protected GameObject container;

    //Overridable method called to show the UI
    public virtual void Show()
    {
        //Activates the container
        container.SetActive(true);
    }

    //Overridable method called to hide the UI
    public virtual void Hide()
    {
        //Deactivates the container
        container.SetActive(false);
    }
}

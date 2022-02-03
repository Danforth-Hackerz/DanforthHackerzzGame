using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class used for objects which transition between an area or scenes
[RequireComponent(typeof(Animator))]
public abstract class TransitionalObject : Interactable
{
    //Animator for playing exit animation from scene
    protected Animator animator;
    //Gameobject for fading between areas
    protected GameObject fadePanel;

    //Overidable methods for each transition
    protected virtual void PlayExitAnimation() { }
    protected virtual void Transition() { }

    //Called on scene load
    protected override void IStart()
    {
        //Assigns animator
        animator = GetComponent<Animator>();
    }

    //Called when the user interacts with the object
    public override void Interact()
    {
        Debug.Log("Interacted");
        //Plays the exit animation and transitions
        PlayExitAnimation();
        Transition();
    }

    //Static method used for calling an action with a fade
    protected static IEnumerator FadeWithAction(GameObject fadePanel, System.Action action)
    {
        Debug.Log("In the coroutine");

        //Fade out

        //Action called
        action.Invoke();

        //Fade in

        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used for objects which transition between areas but not scenes
public class AreaTranisitonObject : TransitionalObject
{
    //GameObject used to store the next and current areas
    [SerializeField] private Room currrentArea;
    [SerializeField] private Room nextArea;

    //Called when the object is to transition between scenes.
    protected override void Transition()
    {
        //Fades to the next area
        StartCoroutine(FadeWithAction(fadePanel, new System.Action(() =>
        {
            Debug.Log("In the invoke");

            nextArea.Show();

            /* Move player somehow???*/

            Reference.currentRoom = nextArea;

            currrentArea.Hide();
        })));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used for objects which transition between areas but not scenes
public class AreaTranisitonObject : TransitionalObject
{
    //GameObject used to store the next and current areas
    [SerializeField] private GameObject currrentArea;
    [SerializeField] private GameObject nextArea;

    //Called when the object is to transition between scenes.
    protected override void Transition()
    {
        //Fades to the next area
        StartCoroutine(FadeWithAction(fadePanel, new System.Action(() =>
        {
            nextArea.SetActive(true);

            /* Move player somehow???*/

            currrentArea.SetActive(false);
        })));
    }
}

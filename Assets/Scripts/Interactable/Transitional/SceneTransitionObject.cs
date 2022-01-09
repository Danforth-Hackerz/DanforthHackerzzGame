using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class used for objects which transition between scenes
public class SceneTransitionObject : TransitionalObject
{
    //Unisigned short to store the next scene index
    [SerializeField] private ushort nextSceneIndex;

    //Called when the object is to transition
    protected override void Transition()
    {
        //Returns if the scene does not exist
        if (SceneManager.sceneCount <= nextSceneIndex)
        {
            Debug.LogWarning("There is no scene at index " + nextSceneIndex);
            return;
        }

        Debug.Log("Loading Scene " + SceneManager.GetSceneAt(nextSceneIndex).name + "...");

        //Fade with scene load
        StartCoroutine(FadeWithAction(fadePanel, new System.Action(() =>
        {
            SceneManager.LoadScene(nextSceneIndex);
        })));
    }
}

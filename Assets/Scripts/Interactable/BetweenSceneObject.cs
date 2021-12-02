using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenSceneObject : Interactable
{
    [SerializeField] private ushort nextSceneIndex;

    public override void Interact()
    {
        if(SceneManager.sceneCount <= nextSceneIndex)
        {
            Debug.Log("There is no scene at index " + nextSceneIndex);
            return;
        }

        Debug.Log("Loading Scene " + SceneManager.GetSceneAt(nextSceneIndex).name + "...");
        SceneManager.LoadScene(nextSceneIndex);
    }
}

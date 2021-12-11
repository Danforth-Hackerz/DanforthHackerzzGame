using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires camera component
[RequireComponent(typeof(Camera))]
//Class used to control the camera
public class CameraController : MonoBehaviour
{
    //Bound class used for defining the bounds of the level
    [System.Serializable]
    private class Bound
    {
        public float right;
        public float left;
        public float top;
        public float bottom;
    }

    //Instance of the bounds and camera component
    [SerializeField] private Bound bounds;
    private Camera cam;

    //Called on scene load
    public void Start()
    {
        //Assigns the camera component refrence
        cam = GetComponent<Camera>();
    }

    //Called when the player's position changes
    public void OnPlayerPositionChanged(Vector3 newPosition)
    {
        //Fixes the x position to the bounds plus or minus half the size of the view of the camera
        if (newPosition.x > bounds.right - cam.orthographicSize / 2)
        {
            newPosition.x = bounds.right - cam.orthographicSize / 2;
        }
        else if (newPosition.x < bounds.left + cam.orthographicSize / 2)
        {
            newPosition.x = bounds.left + cam.orthographicSize / 2;
        }

        //Fixes the y position to the bounds plus or minus half the size of the view of the camera
        if (newPosition.y > bounds.top - cam.orthographicSize / 2)
        {
            newPosition.y = bounds.top - cam.orthographicSize / 2;
        }
        else if (newPosition.y < bounds.bottom + cam.orthographicSize / 2)
        {
            newPosition.y = bounds.bottom + cam.orthographicSize / 2;
        }

        //Assigns the new position
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}

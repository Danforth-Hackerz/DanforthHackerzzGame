using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to control the background tile maps
public class BackgroundController : MonoBehaviour
{
    //Float to store the x angle of the grid
    [SerializeField] private float xAngle;

    //Gets refrence to the players transform
    [SerializeField] private Transform playerTransform;

    //Called on scene load
    private void Start()
    {
        //Sets the gmaeobject x angle to the x angle
        transform.rotation = Quaternion.Euler(new Vector3(xAngle, 0, 0));
    }

    //Called every frame
    private void Update()
    {
        //Calculates where the tilempa should be placed to create the perspective effect
        float newZVal = playerTransform.position.y * Mathf.Tan(xAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZVal);
    }
}

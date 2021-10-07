using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.MovePosition(transform.position + new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime) + new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] //Always privatize variables and use SerializeField to have it still function as public for Unity
    private Rigidbody2D myRigidbody;

	void Start ()
    {
        //How to print to console
        //Debug.Log("This is Start!");
	}
	

	void Update ()
    {
        if (Input.GetKey("left"))
        {
            myRigidbody.velocity = new Vector2(-5, myRigidbody.velocity.y);
        }
        else if (Input.GetKey("right"))
        {
            myRigidbody.velocity = new Vector2(5, myRigidbody.velocity.y);
        }
	}
}

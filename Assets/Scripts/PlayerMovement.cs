using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] //Always privatize variables and use SerializeField to have it still function as public for Unity
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float jumpForce = 10;

    private float horizontalMovement;

	void Update ()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        Jump();
    }
    private void FixedUpdate()
    {
        Move();        
    }
    private void Move()
    {
        myRigidbody.AddForce(Vector2.right * horizontalMovement * accelerationForce);
        Vector2 clampedVelocity = myRigidbody.velocity;
        clampedVelocity.x = Mathf.Clamp(myRigidbody.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody.velocity = clampedVelocity;
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

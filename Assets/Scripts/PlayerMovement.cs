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

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    private bool isOnGround;
    private float horizontalMovement;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

	void Update ()
    {
        UpdateIsOnGround();
        HandleHorizontalInput();
        HandleJumpInput();
    }
    private void FixedUpdate()
    {
        Move();        
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;

    }
    private void HandleHorizontalInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        myRigidbody.AddForce(Vector2.right * horizontalMovement * accelerationForce);
        Vector2 clampedVelocity = myRigidbody.velocity;
        clampedVelocity.x = Mathf.Clamp(myRigidbody.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody.velocity = clampedVelocity;
    }
    private void HandleJumpInput()
    {
        if(Input.GetButtonDown("Jump") && isOnGround)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

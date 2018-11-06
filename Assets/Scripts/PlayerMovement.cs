using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] //Always privatize variables and use SerializeField to have it still function as public for Unity
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

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

    [SerializeField]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Animator anim;

    private bool isOnGround;
    private float horizontalMovement;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private bool isFacingRight = true;

    void Update()
    {
        UpdateIsOnGround();
        HandleHorizontalInput();
        HandleJumpInput();
    }
    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }
    private void UpdatePhysicsMaterial()
    {
        if (horizontalMovement == 0)
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        anim.SetBool("Ground", isOnGround);
        anim.SetFloat("vSpeed", myRigidbody.velocity.y);

    }
    private void HandleHorizontalInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }
    private void Move()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        myRigidbody.AddForce(Vector2.right * horizontalMovement * accelerationForce);
        Vector2 clampedVelocity = myRigidbody.velocity;
        clampedVelocity.x = Mathf.Clamp(myRigidbody.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody.velocity = clampedVelocity;
        if (horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }
    }
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //anim.SetBool("Ground", false);
        }
    }
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
    public void Respawn()
    {
        anim.SetBool("Dead", true);
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            anim.SetBool("Dead", false);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

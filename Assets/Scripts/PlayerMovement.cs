using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private Animator anim;

    [SerializeField]
    private Text countText;

    private bool isOnGround;
    private float horizontalMovement;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private bool isFacingRight = true;
    private bool isDead = false;
    private static int coinCount;

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
        if (!isDead)
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
        isDead = true;
        anim.SetBool("Dead", true);
        Invoke("RespawnDelay", 1.00f);

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void RespawnDelay()
    {
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
            isDead = false;
        }
        anim.SetBool("Dead", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            coinCount++;
            SetCountText();
        }
    }
    private void SetCountText()
    {
        countText.text = "Stars: " + coinCount.ToString();
    }
}

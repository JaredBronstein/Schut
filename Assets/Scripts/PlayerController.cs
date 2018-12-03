using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
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
    private Text countText; //This is for the Scoring system, allows access to the canvas system

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 40;

    [SerializeField]
    private Transform firePoint; //Location in which the bullet spawns from
    #endregion
    #region private fields
    private bool isOnGround;
    private float horizontalMovement;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private bool isFacingRight = true;
    private bool isDead = false;
    #endregion
    public static int coinCount;
    #region public functions
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
    public void Die()
    {
        isDead = true;
        anim.SetBool("Dead", true);
        Invoke("Respawn", 1.00f);
        coinCount = 0;
        SetCountText();
    }
    public void SetCountText()
    {
            countText.text = "Stars: " + coinCount.ToString();
    }
    #endregion
    void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if(Bullet.AllBullets.Count > 0)
        {
            Debug.Log("There are " + Bullet.AllBullets.Count.ToString() + " bullets on screen");
        }
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
    }
    private void UpdateHorizontalInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }
    private void Move()
    {
        if (!isDead)
        {
            anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));
            myRigidbody.AddForce(Vector2.right * horizontalMovement * accelerationForce);
            UpdateCharacterDirection();
        }
    }
    private void UpdateCharacterDirection()
    {
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
            anim.SetFloat("vSpeed", myRigidbody.velocity.y);
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bulletSpeed = -bulletSpeed;
        firePoint.position = new Vector3((firePoint.position.x-this.transform.position.x) + this.transform.position.x, firePoint.position.y, firePoint.position.z);
    }
    private void Respawn()
    {
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            DeleteBullets();
            transform.position = currentCheckpoint.transform.position;
            isDead = false;
        }
        anim.SetBool("Dead", false);
    }
    private void DeleteBullets()
    {
        for (int i = 0; i < Bullet.AllBullets.Count; i++)
        {
            Destroy(Bullet.AllBullets[i].gameObject);
        }
        Bullet.AllBullets.Clear();
    }
    private void Shoot()
    {
        GameObject bulletClone = Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        Rigidbody2D bulletRigidbody = bulletClone.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(Vector2.right * bulletSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed = 10;

    private float shotDirection;

    PlayerMovement player = collision.GetComponent<PlayerMovement>();
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            player.Respawn();
        }
        else
        {

        }
    }
}

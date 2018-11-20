﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidBody;

    private float shotDirection;

    public static List<Bullet> allBullets;

    private void Awake()
    {
        allBullets = new List<Bullet>();
        allBullets.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.Respawn();
        }
    }
}

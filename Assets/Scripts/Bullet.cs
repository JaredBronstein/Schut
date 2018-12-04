using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //Adds created bullet to an array so when the player respawns via checkpoint it deletes all the bullets in the scene as well as killing player on touch
{
    private float shotDirection;
    private static List<Bullet> allBullets = new List<Bullet>();

    public static List<Bullet> AllBullets
    {
        get
        {
            return allBullets;
        }
    }
    /// <summary>
    /// When created adds self to an array that way when the player respawns from a checkpoint all bullets in game can be deleted
    /// </summary>
    private void Awake()
    {
        allBullets.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.Die();
        }
    }
}

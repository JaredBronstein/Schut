using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float shotDirection;

    public static List<Bullet> allBullets = new List<Bullet>();

    private void Awake()
    {
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

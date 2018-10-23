using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 10;

    [SerializeField]
    private Transform firePoint;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject bulletClone = Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
            Rigidbody2D bulletRigidbody = bulletClone.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(Vector2.right * bulletSpeed);
        }
    }
}

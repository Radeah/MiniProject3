using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    void Update()
    {
        // Check for user input to shoot
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a new bullet at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the bullet's Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet to shoot it
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}

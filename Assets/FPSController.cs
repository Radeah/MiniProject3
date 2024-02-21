using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    private Rigidbody rb;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        // Check if firePoint is properly assigned
        if (firePoint == null)
        {
            Debug.LogError("Fire point is not assigned in FPSController.");
        }
    }

    void Update()
    {
        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.y = rb.velocity.y; // Maintain vertical velocity

        rb.velocity = move * moveSpeed;

        // Check if firePoint is properly assigned
        if (firePoint == null)
        {
            Debug.LogError("Fire point is not assigned in FPSController.");
            return; // Exit early to prevent further errors
        }

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


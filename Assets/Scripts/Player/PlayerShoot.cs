using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime = 0f;

    // The 8 fixed directions
    private Vector2[] eightDirections = new Vector2[]
    {
        Vector2.right,                 // Right (0°)
        new Vector2(1, 1).normalized,  // Up-Right (45°)
        Vector2.up,                    // Up (90°)
        new Vector2(-1, 1).normalized, // Up-Left (135°)
        Vector2.left,                  // Left (180°)
        new Vector2(-1, -1).normalized,// Down-Left (225°)
        Vector2.down,                  // Down (270°)
        new Vector2(1, -1).normalized  // Down-Right (315°)
    };

    private void Update()
    {
        // Get the shooting direction from UserInput
        Vector2 shootDirection = UserInput.Shoot;

        // Check if player is pressing any arrow key and if cooldown has passed
        if (shootDirection.sqrMagnitude > 0.1f && Time.time >= nextFireTime)
        {
            // Convert to closest 8-direction
            Vector2 snappedDirection = SnapTo8Directions(shootDirection);

            Fire(snappedDirection);
            nextFireTime = Time.time + fireRate;
        }
    }

    private Vector2 SnapTo8Directions(Vector2 input)
    {
        // Calculate angle in degrees (0-360)
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // Convert to closest 45-degree increment
        int index = Mathf.RoundToInt(angle / 45f) % 8;

        return eightDirections[index];
    }

    private void Fire(Vector2 shootDirection)
    {
        // Create a bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Get the bullet's rigidbody component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity based on the shoot direction
        rb.velocity = shootDirection * bulletSpeed;

        // Rotate bullet to face the direction it's moving
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Optional: Destroy bullet after some time to avoid cluttering the scene
        Destroy(bullet, 5f);
    }
}

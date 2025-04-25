using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMouse : BaseEnemy
{
    [Header("Fare Özellikleri")]
    public float chargeSpeed = 8f;
    public float normalSpeed = 3f;
    public float changeDirectionTime = 0.5f;

    private float directionTimer;
    private Vector2 randomDirection;

    protected override void Start()
    {
        base.Start();
        health = 30f;
        movementSpeed = normalSpeed;
        damage = 10f;
    }

    private void Update()
    {
        if (IsPlayerInRange())
        {
            ChasePlayer();
        }
        else
        {
            MoveRandomly();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chargeSpeed;

        // Sprite yönünü ayarla
        spriteRenderer.flipX = direction.x < 0;
    }

    private void MoveRandomly()
    {
        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0)
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            directionTimer = changeDirectionTime;
        }

        rb.velocity = randomDirection * normalSpeed;
        spriteRenderer.flipX = randomDirection.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncuya hasar ver
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Temas sonrasý geri sekme
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse);
        }
    }
}

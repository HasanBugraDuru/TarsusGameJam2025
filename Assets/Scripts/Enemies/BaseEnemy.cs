using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Temel �zellikler")]
    public float health;
    public float movementSpeed;
    public float damage;
    public float detectionRange;

    protected Transform player;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // Hasar alma animasyonu
            StartCoroutine(FlashRed());
        }
    }

    protected virtual IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    protected virtual void Die()
    {
        // �l�m efekti, puan art��� vb.
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        // Alt s�n�flar taraf�ndan uygulanacak hareket mant���
    }

    protected virtual void Attack()
    {
        // Alt s�n�flar taraf�ndan uygulanacak sald�r� mant���
    }

    protected virtual bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRange;
    }
}
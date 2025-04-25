using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 8f;
    [SerializeField] float deceleration = 10f;
    private Vector2 currentVelocity;
    public bool _isDead = false;



    private Rigidbody2D rb;
    private Vector2 moveInput;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
       Move();
    }

    #region Movement Functions
    private void Move()
    {
        // Hareket giri�i
        moveInput = UserInput.Movement;
        Vector2 targetVelocity = moveInput.normalized * moveSpeed;
        // H�zlanma veya yava�lama uygulama
        if (moveInput.magnitude > 0)
        {
            // H�zlanma
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            // Yava�lama
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        rb.velocity = currentVelocity;
        animator.SetFloat("Velo", rb.velocity.magnitude);
    }

    #endregion
}

using Cinemachine;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController PlayerMovement;
    Animator animator;
    CinemachineImpulseSource _ýmpulseSource;
    [Header("UserInput")]
    [SerializeField] private UserInput userInput;


    [Header("Health")]
    [SerializeField] private float impulseForce = 5f;
    [SerializeField] private float maxHealth = 1;
    private float currentHealth;
    public int currentCoin=0;


    private void Awake()
    {
        PlayerMovement = GetComponent<PlayerController>();
        _ýmpulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth; // Oyuncu tam canla baþlar
    }

    #region Health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Active", true);
        animator.SetTrigger("Dead");
        UserInput.Movement = Vector2.zero;
        userInput.enabled = false;
        _ýmpulseSource.GenerateImpulse(impulseForce);
        PlayerMovement._isDead = true;
        Invoke("Restrart", 1);
    }

    private void Restrart()
    {
        // Önce oyun zamanýný normal hýza getir
        Time.timeScale = 1f;
       
        // Fade efekti ile sahneyi yeniden yükle
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SceneTransitionManager.ChangeScene(currentSceneName);
    }
    #endregion
}

using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")] 
    
    private GameObject _player; 
    [SerializeField] private int _maxHealth = 100;
    public int _currentHealth; 
    [SerializeField] public Canvas gameEndCanvas;

    [Header("UI Feedback")] 
    [SerializeField] public TextMeshProUGUI playerDamageText;

    void Start()
    {
        _player = this.gameObject; 
        _currentHealth = _maxHealth;
        
        if (gameEndCanvas != null) 
            gameEndCanvas.enabled = false;
            
        HealthUpdate();
    }
    

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
         
        if (_currentHealth < 0) _currentHealth = 0;
          
        HealthUpdate();

        
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
       
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // 2. Show Game Over Screen
        if (gameEndCanvas != null)
        {
            gameEndCanvas.enabled = true;
        }

        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame("Died from Damage");
        }
        else
        {
            Debug.LogError("Game Manager not found! Data won't save.");
        }

        // Optional: Stop time
        // Time.timeScale = 0; 
    }
     
    private void HealthUpdate()
    {
        if (playerDamageText != null)
        {
            playerDamageText.text = $"HEALTH : {_currentHealth}";
        }
    }
}

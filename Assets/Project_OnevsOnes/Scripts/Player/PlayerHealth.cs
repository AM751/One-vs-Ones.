using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")] 
    [SerializeField] private GameObject _player;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _damage;
    [SerializeField] public Canvas gameEndCanvas;

    [Header("Player Damage")] 
    [SerializeField] public TextMeshProUGUI playerDamageText;
    void Start()
    {
        _currentHealth = _maxHealth;
        gameEndCanvas.enabled = false;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == _player)
        {
            TakeDamage();
            HealthUpdate();
        }
        
    }

    private void TakeDamage()
    {
        if (_currentHealth <= 0) return;
     
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
    }

    public void TakeDamage(int damage)
     {
         _currentHealth -= damage;
         
          if (_currentHealth < 0)
          {
              _currentHealth = 0;
          }
          
          HealthUpdate();

          
          if (_currentHealth == 0)
          {
              gameEndCanvas.enabled = true;
              Time.timeScale = 0;
          }
     }
     
      private void HealthUpdate()
     {
         if (playerDamageText != null)
         {
             playerDamageText.text = $"HEALTH : {_currentHealth}";
             
             PlayerController controller = _player.GetComponent<PlayerController>();

             if (controller != null)
             {
                 controller.enabled = false;
             }
         }
     }
}

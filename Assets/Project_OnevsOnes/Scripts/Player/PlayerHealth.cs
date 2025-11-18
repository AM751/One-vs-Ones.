using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")] 
    [SerializeField] public GameObject player;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _damage;
    
    [Header("Player Obstacle")]
    [SerializeField] private GameObject _playerObstacle;

    [Header("Player Damage")] 
    [SerializeField] public TextMeshProUGUI playerDamageText;
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            TakeDamage();
            HealthUpdate();
            //playerDamageText.text = $"HEALTH: {_currentHealth}";
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
              Debug.Log("Dei, Avan sethutan da!!!");
          }
     }
     
      private void HealthUpdate()
     {
         if (playerDamageText != null)
         {
             playerDamageText.text = $"HEALTH : {_currentHealth}";
         }
     }
}

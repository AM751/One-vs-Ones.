using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;

public class Obstacle : MonoBehaviour
{
    [Header("Player Damage")]
    [SerializeField] private GameObject _player;
    [SerializeField] private int _obstacleDamageTaken;
    
    [Header("Player Particle Effect.")]
    [SerializeField] private ParticleSystem _playerCollidedParticles; 
    
    [Header("Player Knockback Instruction.")]
    [SerializeField] private Canvas _playerKnockbackInstructionCanvas;
    private bool _isPlayerKnockedBackInstructionActive = false;
    
    

    private void Start()
    {
        if (_playerKnockbackInstructionCanvas != null)
        {
            _playerKnockbackInstructionCanvas.enabled = false;
        }
    }
    

    private void Update()
    {
        if (_isPlayerKnockedBackInstructionActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                KnockBackInstructionDisable();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == _player)
        {
            Vector2 collidingObstacle = other.GetContact(0).point;
            Instantiate(_playerCollidedParticles, collidingObstacle, Quaternion.identity);
            PlayerAudio.Instance.playSoundOnObjectCollide();
            KnockBackInstructionEnable();
        }

        //For Health UI update:
        if (other.gameObject == _player)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            //For taking the damage:
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_obstacleDamageTaken);
            }
        }
    }

    private void KnockBackInstructionEnable()
    {
        if (_playerKnockbackInstructionCanvas != null)
        {
            _playerKnockbackInstructionCanvas.enabled = true;
            _isPlayerKnockedBackInstructionActive = true;
        }
    }

    private void KnockBackInstructionDisable()
    {
        if (_playerKnockbackInstructionCanvas != null)
        {
            _playerKnockbackInstructionCanvas.enabled = false;
            _isPlayerKnockedBackInstructionActive = false;
        }
    }
    
}

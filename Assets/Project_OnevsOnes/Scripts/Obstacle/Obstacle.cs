using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Player Object.")] [SerializeField]
    private GameObject _player;
    
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

using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem _playerCollidedParticles;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerCollidedParticles != null && other.gameObject.CompareTag("Obstacles"))
        {
            Vector2 collidingPoint = other.GetContact(0).point;
            
            Instantiate(_playerCollidedParticles, collidingPoint, Quaternion.identity);
        }
    }
}

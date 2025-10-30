using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Player Particle Effect.")] [SerializeField]
    private GameObject _player;
    
    [Header("Player Particle Effect.")]
    [SerializeField] private ParticleSystem _playerCollidedParticles; 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == _player)
        {
            Vector2 collidingObstacle = other.GetContact(0).point;
            Instantiate(_playerCollidedParticles, collidingObstacle, Quaternion.identity);
            PlayerAudio.Instance.playSoundOnObjectCollide();
        }
    }
}

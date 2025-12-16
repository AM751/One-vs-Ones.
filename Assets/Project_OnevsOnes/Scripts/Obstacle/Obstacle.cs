using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("System Damage Settings")]
    public int healthDamage = 20;   
    public float staminaDamage = 30f; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other.gameObject);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.gameObject);
    }

    void HandleHit(GameObject player)
    {
        // 1. Attack Health System
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(healthDamage);
         
        }

        // 2. Attack Stamina System (The Interconnection)
       
        Stamina stamina = player.GetComponent<Stamina>();
        if (stamina != null)
        {
            stamina.InstantDrain(staminaDamage);
        }

        // 3. Destroy Obstacle (Optional)
        // If I want the obstacle to disappear after hitting it:
        // Destroy(gameObject);
    }
}
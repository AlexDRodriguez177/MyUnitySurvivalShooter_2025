using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    public int damageAmount = 20;
    public float despawnTime = 20f;
    public AudioClip trapSound;
    public ParticleSystem trapEffect;
    
    void Start()
    {
        // Destroy the trap automatically after 20 seconds
        Destroy(gameObject, despawnTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Only respond to objects tagged as Player
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Try to get the PlayerHealth component
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        
        
            
            // Deal damage
            playerHealth.TakeDamage(damageAmount);

        // Gets audio socurce from player
        AudioSource playerAudio = playerHealth.GetComponentInChildren<AudioSource>();
        
        
            //plays trap audio
            playerAudio.PlayOneShot(trapSound);


            // Play trap particle effect
            ParticleSystem effect = Instantiate(trapEffect, other.transform.position, Quaternion.identity, other.transform);
             Destroy(effect.gameObject, 1f);
        
             
        
                
          // Destroy this trap after triggering
             Destroy(gameObject);  
       
    }
}

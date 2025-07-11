using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healingAmount = 10;
    public float despawnTime = 15f;
    public AudioClip healingAudio;
    public ParticleSystem healthPickupEffect;
    void Start()
    {
        // destroy object after 15 sec
        Destroy(gameObject, despawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only respond to objects tagged as Player
        if (!other.CompareTag("Player"))
        {
            return;
        }
        // Try to get the PlayerHealth component in parent hierarchy
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();


        // Heal the player
        playerHealth.Heal(healingAmount);


        // Try to find an AudioSource on the player
        AudioSource playerAudio = playerHealth.GetComponentInChildren<AudioSource>();


        // Play healing sound
        playerAudio.PlayOneShot(healingAudio);


        //Plays particle system and destorys after 1 sec
        ParticleSystem effect = Instantiate(healthPickupEffect, other.transform.position, Quaternion.identity, other.transform);
            Destroy(effect.gameObject, 1f);

            
        
            // destroys game object after interaction
            Destroy(gameObject); 
    }
}

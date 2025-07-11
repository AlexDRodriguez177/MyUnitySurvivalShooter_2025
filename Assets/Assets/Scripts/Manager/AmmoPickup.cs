using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // amount of ammo given from each pickup 
    public int ammoAmount = 10;
    // despawn timer
    public float despawnTime = 10f;
    //Audio for picking up ammo
    public AudioClip ammoAudio;
    public ParticleSystem ammoPickupEffect;
    private void Start()
    {
        // destroy object after 10 sec
        Destroy(gameObject, despawnTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Gun"))
        {
            // Ignore anything that isn't tagged as Gun
            return;
        }
        // calls player shooting script 
        PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
            
            // calls AddAmmo function in playerShooting 
            playerShooting.AddAmmo(ammoAmount);
           
            // gets player audio source
            AudioSource playerAudio = other.GetComponent<AudioSource>();
            //plays ammo audio
            playerAudio.PlayOneShot(ammoAudio);
            
            //plays ammo effect and destroys after 1 sec
            ParticleSystem effect = Instantiate(ammoPickupEffect, other.transform.position, Quaternion.identity, other.transform);
            Destroy(effect.gameObject, 1f);
        
            // Destroy object after player "Picks up" 
            Destroy(gameObject);
    }
        
        
}


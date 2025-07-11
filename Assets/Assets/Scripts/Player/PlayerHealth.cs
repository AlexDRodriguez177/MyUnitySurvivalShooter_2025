using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, .1f);

    Animator anm;
    AudioSource playerAudio;
    PlayerMovment playerMovment;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    private void Awake()
    {
        anm=GetComponent<Animator>();
        playerAudio=GetComponent<AudioSource>();
        playerMovment=GetComponent<PlayerMovment>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;

    }
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        }
        damaged = false;


    }

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if ( currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        
        playerShooting.DisabledEffects();
        anm.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMovment.enabled = false;
        playerShooting.enabled = false;

    }

    public void Heal(int amount)
    {
        // Increase current health by the amount
        currentHealth += amount;

        // make sure health does not go bellow or above 
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        // Update the health bar
        healthSlider.value = currentHealth;

       
    }


}

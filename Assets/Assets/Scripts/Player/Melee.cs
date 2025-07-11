using UnityEngine;
using System.Collections;
public class Melee : MonoBehaviour
{
public int damage = 10;

public float range = 20f;
public float attackRadius = 5f;
public Transform attackPoint;
public LayerMask enemyMask;
public float knockBack = 10f;
public float cooldown = .5f;
private float timer = 0f;

public AudioSource meleeAudio;
public PlayerShooting playerShooting;
public GameObject slashEffect;

    // Update is called once per frame
    void Update()
    {
        // update the cooldown timer every frame 
        timer += Time.deltaTime;

        


        // check if player hit the melee button and the cooldown is finished
        if (Input.GetButtonDown ("Fire2") && timer>=cooldown)
        {
            //disables shooting
            playerShooting.DisabledEffects();
           //plays slashing noise
            meleeAudio.Play();
            //executes melee attack code
            MeleeAttack();

            
            

         }

    }




    void MeleeAttack()
    {
        //Reset timer to 0 
        timer = 0f;

        // Spawn slashing effect
        if (slashEffect != null)
        {
            GameObject slash = Instantiate(slashEffect, attackPoint.position, attackPoint.rotation);
            Destroy(slash, 2f);
        }

        // Find all enemy colliders in the attack radius like a swing 
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyMask);

        //apply damage and knock back for all colliders in radius 
        foreach (Collider enemy in hitEnemies)
        {
            //Damage
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, enemyHealth.transform.position);
            }
            //Knockback
            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 knockbackDirection = enemy.transform.position - attackPoint.position;
                knockbackDirection.y = 0f;

                if (knockbackDirection != Vector3.zero)
                {
                    knockbackDirection = knockbackDirection.normalized;
                    enemyRb.AddForce(knockbackDirection * knockBack, ForceMode.Impulse);
                }
            }
        }

    }

    
}

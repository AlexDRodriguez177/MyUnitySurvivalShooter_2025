using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = .5f;
    public int attackDamage = 10;

    private Animator anim;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;


    private void Start()
    {
        
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>(); 
    }

    /// <summary>
    /// The EnemyManagerReference method is used to set the player reference
    /// It initializes the playerHealth component from the player GameObject
    /// Bypassing the start method which would call for the Player reference too early
    /// </summary>
    public void EnemyManagerReference(GameObject playerReference)
    {
        player= playerReference;
        playerHealth = player.GetComponent<PlayerHealth>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {

            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {  
            playerInRange = false;
        }

    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();

        }

        if (playerHealth.currentHealth<=0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}

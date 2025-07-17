using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;
    

    private void Start()
    {
        
        
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// The enemyManagerReference method is used to set the player reference
    /// Bypassing the start method which would call for the Player reference too early
    /// reference to the PlayerHealth component from the player Transform
    /// </summary>

    public void EnemyManagerReference(Transform targetPlayer)
    {
        player = targetPlayer;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if ( enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled=false;
        }
        
    }
}

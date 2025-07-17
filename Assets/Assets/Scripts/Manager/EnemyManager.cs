using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public GameObject player;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth<=0)
        {
            return;
        }
        // Randomly select a spawn point from the array of spawn points
        int spawnPointIndex =Random.Range(0, spawnPoints.Length);
        //Instantiate enemy and is stored in a variable
        GameObject spawnedEnemy =Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        // Gets the EnemyAttack and assigns the player reference to it
        EnemyAttack attack = spawnedEnemy.GetComponent<EnemyAttack>();
        attack.EnemyManagerReference(player);
        // Gets the EnemyMovement and assigns the player reference to it
        EnemyMovment movement = spawnedEnemy.GetComponent<EnemyMovment>();
        movement.EnemyManagerReference(player.transform);
    }
}

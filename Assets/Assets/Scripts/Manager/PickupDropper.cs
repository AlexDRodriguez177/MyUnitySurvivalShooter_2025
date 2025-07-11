using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    
    public GameObject[] pickups;
    public float dropChance = 1f;
    public Transform pickupSpawnPoint;
    public void SpawnAtPosition(Vector3 position)
    {
        // Roll for drop chance
        if (Random.value > dropChance)
        {
            // No drop this time
            return;
        }

        // Choose random pickup
        int randomIndex = Random.Range(0, pickups.Length);
        GameObject selectedPickup = pickups[randomIndex];

        Instantiate(selectedPickup, position, Quaternion.identity);
    }
}


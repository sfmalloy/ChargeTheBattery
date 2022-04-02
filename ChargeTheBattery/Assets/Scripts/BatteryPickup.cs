using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.batteryLevel += Random.Range(1, 5);
            Destroy(gameObject);
        }
    }
}
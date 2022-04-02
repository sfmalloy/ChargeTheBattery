using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.UpdateBattery(Random.Range(3, 5));
            Destroy(gameObject);
        }
    }
}

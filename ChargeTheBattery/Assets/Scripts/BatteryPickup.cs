using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour
{
    public AudioSource audioSource;
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
            if (audioSource.isPlaying)
                audioSource.Stop();
            AudioSource.PlayClipAtPoint(audioSource.clip, new Vector3(0, 0, -7));
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public GameObject energy;
    private bool isDelayed;
    private GameManager gameManager;

    private Vector2 min;
    private Vector2 max;
    private Vector2 waitRange;

    void Start()
    {
        min = new Vector2(
            transform.position.x - transform.lossyScale.x / 2 + 0.5f,
            transform.position.y - transform.lossyScale.y / 2 + 0.5f
        );

        max = new Vector2(
            transform.position.x + transform.lossyScale.x / 2 - 0.5f,
            transform.position.y + transform.lossyScale.y / 2 - 0.5f
        );
        isDelayed = false;
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(DelaySpawn());
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!isDelayed)
        {
            Instantiate(energy, new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)), Quaternion.identity);
            StartCoroutine(DelaySpawn());
        }

        if (gameManager.decreaseDelay <= 0.2f)
            waitRange = new Vector2(5, 10);
        else if (gameManager.decreaseDelay <= 0.3f)
            waitRange = new Vector2(15, 20);
        else
            waitRange = new Vector2(20, 30);
    }

    IEnumerator DelaySpawn() 
    {
        isDelayed = true;
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        isDelayed = false;
    }
}

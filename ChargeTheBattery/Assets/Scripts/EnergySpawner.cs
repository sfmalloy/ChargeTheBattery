using System.Collections;
using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public GameObject energy;
    private bool isDelayed;

    private Vector2 min;
    private Vector2 max;
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
    }

    void Update()
    {
        if (!isDelayed)
        {
            Instantiate(energy, new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)), Quaternion.identity);
            StartCoroutine(DelaySpawn());
        }
    }

    IEnumerator DelaySpawn() 
    {
        isDelayed = true;
        yield return new WaitForSeconds(Random.Range(15, 30));
        isDelayed = false;
    }
}

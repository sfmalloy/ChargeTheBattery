using System.Collections;
using UnityEngine;

public class OutletCharger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    private bool canCharge;
    private bool isDelayed;
    private bool isFuseBlown;
    private bool timingFuse;
    private Color defaultColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        defaultColor = Color.white;
    }

    void Update()
    {
        if (!isFuseBlown && canCharge && !isDelayed)
        {
            gameManager.UpdateBattery(Random.Range(5, 10));
            StartCoroutine(DelayCharge());
            if (!timingFuse)
                StartCoroutine(TimeFuse());
        }

        spriteRenderer.color = isFuseBlown ? Color.red : defaultColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canCharge = true;
            defaultColor = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canCharge = false;
            defaultColor = Color.white;
        }
    }

    IEnumerator DelayCharge()
    {
        isDelayed = true;
        yield return new WaitForSeconds(1.0f);
        isDelayed = false;
    }

    IEnumerator TimeFuse() 
    {
        timingFuse = true;

        isFuseBlown = false;
        yield return new WaitForSeconds(Random.Range(2, 4));
        isFuseBlown = true;
        yield return new WaitForSeconds(Random.Range(10, 14));
        isFuseBlown = false;

        timingFuse = false;
    }
}

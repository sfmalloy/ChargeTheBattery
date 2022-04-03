using System.Collections;
using UnityEngine;

public class OutletCharger : MonoBehaviour
{
    public AudioSource deadFuseSound;
    public AudioSource collideSound;

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    private bool canCharge;
    private bool isDelayed;
    private bool isFuseBlown;
    private bool timingFuse;
    private Color defaultColor;
    private float chargeDelay;

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
            collideSound.pitch = 1.35f;
            collideSound.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canCharge = false;
            defaultColor = Color.white;
            collideSound.pitch = 1.0f;
            collideSound.Play();
        }
    }

    IEnumerator DelayCharge()
    {
        isDelayed = true;
        if (gameManager.decreaseDelay <= 0.1f)
            yield return new WaitForSeconds(0.4f);
        else if (gameManager.decreaseDelay <= 0.2f)
            yield return new WaitForSeconds(0.5f);
        else if (gameManager.decreaseDelay <= 0.25f)
            yield return new WaitForSeconds(0.7f);
        else if (gameManager.decreaseDelay <= 0.3f)
            yield return new WaitForSeconds(0.9f);
        else
            yield return new WaitForSeconds(1.0f);
        isDelayed = false;
    }

    IEnumerator TimeFuse() 
    {
        timingFuse = true;

        isFuseBlown = false;
        yield return new WaitForSeconds(Random.Range(2, 4));
        isFuseBlown = true;
        deadFuseSound.Play();
        yield return new WaitForSeconds(Random.Range(10, 14));
        isFuseBlown = false;

        timingFuse = false;
    }
}

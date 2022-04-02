using System.Collections;
using UnityEngine;

public class OutletCharger : MonoBehaviour
{
    public GameManager gameManager;
    
    private SpriteRenderer spriteRenderer;
    private bool canCharge;
    private bool isDelayed;
    private bool isFuseBlown;
    private bool timingFuse;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isFuseBlown && canCharge && !isDelayed)
        {
            gameManager.UpdateBattery(1);
            StartCoroutine(DelayCharge());
            if (!timingFuse)
                StartCoroutine(TimeFuse());
        }

        spriteRenderer.color = isFuseBlown ? Color.red : Color.white;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        canCharge = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        canCharge = false;
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
        yield return new WaitForSeconds(Random.Range(5, 7));
        isFuseBlown = true;
        yield return new WaitForSeconds(Random.Range(10, 14));
        isFuseBlown = false;

        timingFuse = false;
    }
}

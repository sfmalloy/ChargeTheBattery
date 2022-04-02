using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;

    private float hVel;
    private float vVel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        hVel = Input.GetAxisRaw("Horizontal");
        vVel = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Adjust so that pushing both vertical and horizontal keys won't make make you go faster than indended.
        if (hVel != 0 && vVel != 0) 
            rb.velocity = new Vector3(Mathf.Sqrt(0.5f) * hVel, Mathf.Sqrt(0.5f) * vVel) * playerSpeed;
        else
            rb.velocity = new Vector3(hVel, vVel) * playerSpeed;
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;

    [Header("Animator Controllers")]
    public RuntimeAnimatorController up;
    public RuntimeAnimatorController down;
    public RuntimeAnimatorController left;
    public RuntimeAnimatorController right;

    [Header("Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float hVel;
    private float vVel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.runtimeAnimatorController = down;
    }

    void Update()
    {
        hVel = Input.GetAxisRaw("Horizontal");
        vVel = Input.GetAxisRaw("Vertical");
        animator.enabled = hVel != 0 || vVel != 0;

        if (animator.enabled)
        {
            if (vVel > 0)
            {
                animator.runtimeAnimatorController = up;
            }
            else if (vVel < 0)
            {
                animator.runtimeAnimatorController = down;
            }
            else if (hVel > 0)
            {
                animator.runtimeAnimatorController = right;
            }
            else if (hVel < 0)
            {
                animator.runtimeAnimatorController = left;
            }
        }
        else
        {
            if (animator.runtimeAnimatorController.Equals(up))
            {
                spriteRenderer.sprite = upSprite;
            }
            else if (animator.runtimeAnimatorController.Equals(down))
            {
                spriteRenderer.sprite = downSprite;
            }
            else if (animator.runtimeAnimatorController.Equals(right))
            {
                spriteRenderer.sprite = rightSprite;
            }
            else if (animator.runtimeAnimatorController.Equals(left))
            {
                spriteRenderer.sprite = leftSprite;
            }
        }
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

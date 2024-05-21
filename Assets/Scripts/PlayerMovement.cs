using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float horizontalDir = 0f;
    public float movement = 0f;
    public float groundCheckRadius = 0.3f;
    private float runSpeed = 9f;
    private float walkSpeed = 5f;
    public float jumpForce = 10f;

    public bool isWatchingRight = true;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isGrounded = true;

    public GameObject knife;
    public GameObject gun;
    public GameObject groundCheck;

    public LayerMask groundLayer;

    public PlayerAttack playerAttack;

    public static PlayerMovement instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Il y a + d'une instance de PlayerMovement dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("is Jumping !!!");
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalDir = 1f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            horizontalDir = -1f;
        }
        else
        {
            horizontalDir = 0f;
        }

        if (horizontalDir != 0f && Input.GetKey(KeyCode.C))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        movement = horizontalDir * moveSpeed;

        float characterVelocity = Mathf.Abs(movement);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("IsRunning", isRunning);
    }


    void FixedUpdate()
    {
        Flip(movement);
        rb.velocity = new Vector2(movement, 0);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f,jumpForce));
            isJumping = false;
        }
    }



    void Flip(float _movement)
    {
        if(_movement > 0.1f)
        {
            transform.localScale = new Vector3(1,1,1);
            isWatchingRight = true;
        }
        else if(_movement < -0.1f)
        {
            transform.localScale = new Vector3(-1,1,1);
            isWatchingRight = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }

}

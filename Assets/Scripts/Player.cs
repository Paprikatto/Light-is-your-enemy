using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        LevelManager.instance.player = this;
    }

    private void Update()
    {
        //turn off inputs if level state is not playing
        if (LevelManager.instance.levelState != LevelState.Playing)
        {
            //rb.bodyType = RigidbodyType2D.Static;
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y - 0.5f), 0.2f, groundMask);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }


        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        #region Animations

        if (moveInput != 0)
        {
            animator.SetBool("Walking",  true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        #endregion

        #region MovingPlatforms
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundMask);
        if(hit.collider != null)
        {
            if (hit.collider.tag == "MovingPlatform")
            {
                transform.SetParent(hit.transform.parent);
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.SetParent(null);
            }
        }
        else if(transform.parent != null)
        {
            transform.SetParent(null);
        }

        #endregion
    }
    void FixedUpdate()
    {
        if (LevelManager.instance.levelState != LevelState.Playing) return;

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
    }

    #region Win and Lose

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WinCollider")
        {
            LevelManager.instance.levelState = LevelState.Won;
        }else if (collision.tag == "GameOverCollider")
        {
            LevelManager.instance.levelState = LevelState.Lost;
        }
    }
    #endregion
}

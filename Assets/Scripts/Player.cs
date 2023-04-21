using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;

    private float moveInput;

    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //turn off inputs if level state is not playing
        if (LevelManager.instance.levelState != LevelState.Playing)
        {
            rb.bodyType = RigidbodyType2D.Static;
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
    }
    void FixedUpdate()
    {
        if (LevelManager.instance.levelState != LevelState.Playing) return;

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
    }

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
}

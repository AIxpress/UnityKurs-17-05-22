using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float speed = 2f;
    public float jumpHeight = 5f;

    private int jumpsLeft = 2;
    private bool isGrounded = false;

    public GameObject deathPartcilePrefab;

    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");    
        
        if (horizontalInput < -0.001f) {
            spriteRenderer.flipX = true;
        }

        if (horizontalInput > 0.001f) {
            spriteRenderer.flipX = false;
        }

        transform.position += Vector3.right * horizontalInput * 
        Time.deltaTime * speed;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            if (Mathf.Abs(horizontalInput) > 0.001f) {
                animator.Play("Run");
            } else {
                animator.Play("Idle");
            }
        }

        if (Input.GetButtonDown("Jump")) {
            // pls do only jump if cool
            if (jumpsLeft > 0) {
                DoJump();

                if (!isGrounded) {
                    jumpsLeft--;
                }
            }     
        }
    }

    void DoJump() {
        animator.Play("Jump");

        rigidbody2D.velocity = new Vector2(
            //x velocity keeps unchanged
            rigidbody2D.velocity.x,

            // y velocity gets set
            jumpHeight
        );
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
        jumpsLeft = 1;
        isGrounded = true;

        if (other.gameObject.tag == "Spikes") {
            Instantiate(deathPartcilePrefab, transform.position, Quaternion.identity);
        }
        
    }

    void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;
    }
}

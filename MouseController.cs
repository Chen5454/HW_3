using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
   
    private Rigidbody2D rb2d;
    public Animator _animator;
    private Vector2 velocity;
    private Vector3 scale;
    private Vector3 negScale;
    private int jumpCount;
    private int maxHp;
    private float movement;
    public float speed;
    public float jumpForce;
    private float doubleJumpForce;
    private bool isJumping;
    private bool isGrounded;
    private bool isFliped;
    public bool canDoubleJump;
    
   // private bool walljump;
    void Start()
    {
        Init();
    }

    void Init()
    {
        maxHp = 3;
        rb2d = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        negScale = new Vector3(-scale.x, scale.y, scale.y);
        doubleJumpForce = jumpForce * 2;
        canDoubleJump = false;
        GameManager.instance.currentHp = maxHp;
    }



    private void FixedUpdate()
    {

        if (GameManager.instance.isAlive&& !GameManager.instance.isDancing)
        {
         PlayerMovement();
         Jump();
         flip();
        
        }
        else if(!GameManager.instance.isAlive)
        {
            
            _animator.SetTrigger("doDeath");
        
        }
    }

    void PlayerMovement()
    {
       
            //movement 
            movement = Input.GetAxis("Horizontal");
            if (movement > 0)
            {

                velocity.x = speed;

                _animator.SetBool("iswalking", true);
            }
            else if (movement < 0)
            {
                velocity.x = -speed;

                _animator.SetBool("iswalking", true);
            }
            else
            {
                velocity.x = 0f;
                _animator.SetBool("iswalking", false);
            }
            transform.Translate(velocity * Time.fixedDeltaTime);
        
       
    }
    private void flip()
    {
        if (movement > 0)
        {
            isFliped = false;
            transform.localScale = scale;
        }
        if (movement < 0)
        {
            isFliped = true;
            transform.localScale = negScale;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")||collision.CompareTag("Platformer"))
        {
            isGrounded = true;
           velocity = Vector2.zero;
           rb2d.velocity = Vector2.zero;
           // Debug.Log("IsGrounded" + isGrounded);
          _animator.SetBool("isfalling", false);
        }
     
        
        // if (collision.gameObject.tag.Equals("Wall"))
        //  {
        //     walljump = true;
        //  }

        if (collision.gameObject.CompareTag("pickup"))
        {
            canDoubleJump = true;
        }

        if (collision.gameObject.CompareTag("Victory"))
        {
            
            _animator.SetTrigger("doDance");
        }
    }



    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Platformer"))
        {
            isGrounded = false;
          //  Debug.Log("IsGrounded" + isGrounded);
            _animator.SetBool("isfalling", true);
        }
      
        
      //  if (collision.gameObject.tag.Equals("Wall"))
       // {
         //   walljump = false;
            
       // }
    }

   /* void GroundCheck()
    {
            
        if (rb2d.velocity.y==0)
        {
            isGrounded = true;
            Debug.Log("IsGrounded" + isGrounded);
        }
    }
   */

    void Jump()
    {
        //jumping
        isJumping = Input.GetButtonDown("Jump");
        if (isJumping && isGrounded)
        {
            _animator.SetTrigger("isjump");
            _animator.SetBool("is2ndJump", false);
            jumpCount++;
            velocity.y = jumpForce;
            
            //_animator.SetBool("isJump", true);
        }
        //doublejump
        if (isJumping && !isGrounded)
        {
            if (canDoubleJump)
            {
                _animator.SetBool("is2ndJump", true);
                _animator.SetBool("isJump", false);
                jumpCount++;
                velocity.y = doubleJumpForce;
            }

        }
        if (jumpCount >= 2)
        {
            //_animator.SetBool("is2ndJump", true);
            //_animator.SetBool("isJump", false);
            isJumping = false;
        }
        //if ()
        //{
        //    _animator.SetBool("isfalling", true);
        //}
        else
        {
            
            _animator.SetBool("is2ndJump", false);
            _animator.SetBool("isJump", false);
        }
    }


    
}

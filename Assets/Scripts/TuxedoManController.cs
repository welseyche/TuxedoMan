using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TuxedoManController: MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float jumpHeight;
    public float moveSpeed;
    private float moveInput;
    private float direction = -1;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping = false;
    private bool facingRight = true;
    private int extraJumps;
    public int extraJumpsValue;
    public int Respawn;
    public HealthBar healthBar;
    public int maxHealth = 3;
    public int currentHealth;
    public float attackTime = 0.5f;
    private bool attackTimeIsRunning = false;
    public float deadTime = 0.5f;
    private bool deadTimeIsRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(facingRight == false && moveInput > 0 && attackTimeIsRunning == false && deadTimeIsRunning == false)
        {
            Flip();
            direction = -1;
        }
        else if(facingRight == true && moveInput < 0  && attackTimeIsRunning == false && deadTimeIsRunning == false)
        {
            Flip();
            direction  = 1;
        }

        if (deadTimeIsRunning == true)
        {
            if (deadTime > 0)
            {
                deadTime -= Time.deltaTime;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                deadTimeIsRunning = false;
                SceneManager.LoadScene(Respawn);
            }
        }

        if (attackTimeIsRunning == true)
        {
            if (attackTime > 0)
            {
                attackTime -= Time.deltaTime;
                rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            }
            else
            {
                attackTime = 0.5f;
                attackTimeIsRunning = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")){
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetButtonDown("Jump") && extraJumps > 0  && attackTimeIsRunning == false && deadTimeIsRunning == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpHeight;
            extraJumps--;
            anim.SetBool("isJumping", true);
        }
        else if(isGrounded == true && Input.GetButtonDown("Jump") && extraJumps == 0  && attackTimeIsRunning == false && deadTimeIsRunning == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpHeight;
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpHeight;
                jumpTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

    	if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (attackTimeIsRunning == true)
        {
            if (attackTime > 0)
            {
                anim.SetBool("isAttacked", true);
            }
            else
            {
                anim.SetBool("isAttacked", false);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate (0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        attackTimeIsRunning = true;
        if(currentHealth <= 0)
        {
            attackTimeIsRunning = false;
            anim.SetBool("isDead", true);
            deadTimeIsRunning = true;
        }
    }
}
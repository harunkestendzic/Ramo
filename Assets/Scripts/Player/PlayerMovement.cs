using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float vertical;
    private float ladderSpeed = 8f;
    private bool isLadder;
    private bool isClimbing;
    public bool isClimbing_Moving;
    private Rigidbody2D body;
    public bool grounded;
    private Animator anim;
    [SerializeField] private float jump_height;
    [SerializeField] private float fallMultiplier;
    Vector2 vecGravity;
    Vector2 kecGravity;
    [SerializeField] BoxCollider2D standingCollider;
    [SerializeField] BoxCollider2D crouchingCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] public bool isCrouched;
    [SerializeField] LayerMask flying_groundLayer;
    [SerializeField] private bool crouchFlag;

    private AudioSource audioSource;

    [SerializeField] private AudioClip climbSound;
    [SerializeField] private AudioClip jumpSound;

    public float overheadCheckRadius = 0.2f;
    public float movement_speed = 5.5f;
    public float crouch_speed = 2.5f;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        kecGravity = new Vector2(0, Physics2D.gravity.y);
    }

    private void Update()
    {
        #region JUMP

        if (Input.GetKey(KeyCode.Space) && canJump())
        {
            SoundManager.instance.PlaySound(jumpSound);
            Jump();
        }
        if (body.velocity.y < 0)
        {
            body.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
        if (body.velocity.y > 0)
        {
            body.velocity += kecGravity * fallMultiplier * Time.deltaTime;
        }

        #endregion

        #region CLIMB
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            grounded = false;
        }

        if (isClimbing && body.velocity.y != 0f)
        {
            isClimbing_Moving = true;
            SoundManager.instance.PlayLoopingSound(climbSound);
        }
        else
        {
            isClimbing_Moving = false;
            SoundManager.instance.StopLoopingSound();
        }

        #endregion

        #region WALK

        float horizontalInput = Input.GetAxis("Horizontal");

        bool isWalking = horizontalInput != 0;

        // kretanje lijevo desno
        body.velocity = new Vector2(horizontalInput * movement_speed, body.velocity.y);

        // okretanje lijevo desno
        if (horizontalInput > 0.01f)
        { 
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, -0.2f);
        }

        // animator parametri
        anim.SetBool("walking", isWalking);
        anim.SetBool("climbing", isClimbing);
        anim.SetBool("climbing_moving", isClimbing_Moving);
        anim.SetBool("grounded", grounded);

        #endregion

        #region CROUCH


        if (Input.GetButtonDown("Crouch") && grounded)
        {
            isCrouched = true;
            crouchFlag = true;
            crouchingCollider.enabled = true; 
            standingCollider.enabled = false; 
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouched = false;
            crouchFlag = false;
        }

        // Ako iznad glave ima nesta ostaje u crouch
        bool nadGlavom = Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, flying_groundLayer);

        if (!crouchFlag)
        {
            if (nadGlavom)
            {
                crouchFlag = true;
                crouchingCollider.enabled = true; 
                standingCollider.enabled = false; 
            }
            else
            {
                crouchingCollider.enabled = false; 
                standingCollider.enabled = true; 
            }
        }
        
        if (crouchFlag)
        {
            body.velocity = new Vector2(horizontalInput * crouch_speed, body.velocity.y);
        }

        // crouch animacija
        anim.SetBool("crouch", crouchFlag);

        // crouch walk animacija
        if (crouchFlag && horizontalInput != 0 && grounded)
        {
            anim.SetBool("crouch_walking", true);
            anim.SetBool("crouch", false);
            anim.SetBool("walking", false);
        }        
        else
        {
            anim.SetBool("crouch_walking", false);
        }

        #endregion 
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_height);
        grounded = false;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            body.gravityScale = 0f;
            body.velocity = new Vector2(body.velocity.x, vertical * ladderSpeed);
        }
        else
        {
            body.gravityScale = 1.3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Flying Ground")
            grounded = true;
    }

    public bool canAttack()
    {
        return grounded && !isCrouched;
    }
    public bool canJump()
    {
        return grounded && !isCrouched;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float moveForce = 10;
    [SerializeField] float counterForce = 1;
    bool canMove = true;
    [Header("Jump Variables")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpBufferTimer = .1f;
    bool shouldJump = false;
    int jumpsLeft = 2;

    [Header("Ground Checking")]
    [SerializeField] LayerMask ground;
    [SerializeField] Vector2 gcOffset;
    [SerializeField] float gcRad;
    public bool grounded = true;

    [Header("Wall Checking")]
    [SerializeField] Vector2 wcOffset;
    [SerializeField] Vector2 wcSize;
    [SerializeField] float wallJumpPauseTime = .1f;
    [SerializeField] float wallJumpForce;
    int onWall;
    
    [Header("Dashing")]
    [SerializeField] float dashForce = 1;
    [SerializeField] float dashTimer = .1f;
    [SerializeField] float dashResetTime = .3f;
    bool isDashing = false;
    bool canDash = true;
    float dashResetTimer = 0;

    [Header("Damage Knockback")]
    [SerializeField] float dmgKB;
    float gravScale = 1;
    Vector3 gravDir;
    Rigidbody2D rb;
    float inputDir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravDir = Physics2D.gravity;
    }

    private void Update()
    {
       CheckGrounding();
        if(onWall != 0)
        {
            gravScale = .2f;
        }

        if(dashResetTimer > dashResetTime && (grounded || onWall!=0))
        {
            canDash = true;

        }
        GetInputs();
        dashResetTimer += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (!grounded && !isDashing)
        {
            ApplyGravity(gravScale);
        }
        else
        {
            Vector2 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
        }
        if (inputDir != 0)
        {
            Vector2 vel = rb.velocity;
            vel.x  = Mathf.Clamp(vel.x,-maxSpeed,maxSpeed);
            rb.velocity = vel;

            rb.AddForce(Vector2.right * inputDir);
            if(Mathf.Sign(inputDir) != Mathf.Sign(rb.velocity.x))
            {
                ApplyCounterForce();
            }
        }
        else
        {
            if(!isDashing)
           ApplyCounterForce();
        }
        if(onWall != 0&& shouldJump)
        {
            StartCoroutine("LockMovement", wallJumpPauseTime);
            shouldJump = false;
            jumpsLeft = 1;
            StopCoroutine("CancelJump");
            Jump(Vector2.up*jumpForce + (Vector2.right * -onWall)*wallJumpForce);
        }
        if(jumpsLeft > 0 && shouldJump)
        {
            
            shouldJump = false;
            StopCoroutine("CancelJump");
            jumpsLeft--;
            Jump(Vector2.up*jumpForce);
        }
        
        
    }
    public void OnHit(float duration,Vector2 dir)
    {
        StopCoroutine("LockMovement");
        
        if (GetComponent<Health>().health > 0)
        {
            StartCoroutine("LockMovement", duration);

            rb.AddForce(((Vector2)transform.position - dir).normalized * dmgKB);
        }
            
    }
    void CheckGrounding()
    {
        grounded = Physics2D.OverlapCircle(transform.position + (Vector3)gcOffset, gcRad, ground);
        if (canMove) { inputDir = Input.GetAxisRaw("Horizontal") * moveForce; }
        else inputDir = 0;
        Vector2 wcOffsetLeft = wcOffset;
        wcOffsetLeft.x = -wcOffsetLeft.x;

        //Check Grounding
        if (Physics2D.OverlapBox(transform.position + (Vector3)wcOffset, wcSize, 0, ground))
        {
            onWall = 1;
        }
        else if (Physics2D.OverlapBox(transform.position + (Vector3)wcOffsetLeft, wcSize, 0, ground))
        {
            onWall = -1;
        }
        else
        {
            onWall = 0;
        }
    }

    void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { shouldJump = true; StartCoroutine("CancelJump"); }

        if (!grounded && Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            gravScale = .2f;
        }
        else if (onWall == 0)
        {
            gravScale = 1;
        }
        if (grounded)
        {
            jumpsLeft = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            if (Dash())
            {
                canDash = false;
                dashResetTimer = 0;
            }
                
        }
    }



    bool Dash()
    {
        if(inputDir != 0)
        {
            Vector2 vel = rb.velocity;
            vel.y = 0;
            rb.AddForce(Vector2.right * dashForce * inputDir);
            StartCoroutine("DashIE");
            StartCoroutine("LockMovement", dashTimer);
            return true;
        }
        return false;
    }

    void ApplyGravity(float Scale)
    {
        rb.AddForce(gravDir * Scale);
    }
    void ApplyCounterForce()
    {
        Vector2 vel = rb.velocity;
        vel.y = 0;
        vel = -vel;
        rb.AddForce(vel * counterForce);
    }
    void Jump(Vector2 dir)
    {
        Vector2 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
        rb.AddForce(dir);
    }
    IEnumerator CancelJump()
    {
        yield return new WaitForSeconds(jumpBufferTimer);
        shouldJump = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 wcOffsetLeft = wcOffset;
        wcOffsetLeft.x = -wcOffsetLeft.x;
        Gizmos.DrawWireSphere(transform.position + (Vector3)gcOffset, gcRad);
        Gizmos.DrawWireCube(transform.position + (Vector3)wcOffset, wcSize);
        Gizmos.DrawWireCube(transform.position + (Vector3)wcOffsetLeft, wcSize);
    }

    IEnumerator LockMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    IEnumerator DashIE()
    {
        isDashing = true;
        //disabe damage taking
        yield return new WaitForSeconds(dashTimer);
        isDashing=false;
    }
}
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
    int onWall;
    
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
        grounded = Physics2D.OverlapCircle(transform.position + (Vector3)gcOffset, gcRad, ground);
        if (canMove) { inputDir = Input.GetAxisRaw("Horizontal") * moveForce; }
        else inputDir = 0;
        Vector2 wcOffsetLeft = wcOffset;
        wcOffsetLeft.x = -wcOffsetLeft.x;
        if(Physics2D.OverlapBox(transform.position + (Vector3)wcOffset, wcSize, 0, ground))
        {
            onWall = 1;
        }
        else if(Physics2D.OverlapBox(transform.position + (Vector3)wcOffsetLeft, wcSize, 0, ground))
        {
            onWall = -1;
        }
        else
        {
            onWall = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space)) { shouldJump = true; StartCoroutine("CancelJump"); }
       
        if (!grounded && Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            gravScale = .2f;
        }
        else
        {
            gravScale = 1;
        }
        if (grounded)
        {
            jumpsLeft = 2;
        }
    }
    private void FixedUpdate()
    {
        if (!grounded)
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
           ApplyCounterForce();
        }
        if(onWall != 0&& shouldJump)
        {
            StartCoroutine("LockMovement", wallJumpPauseTime);
            shouldJump = false;
            jumpsLeft = 1;
            StopCoroutine("CancelJump");
            Jump(Vector2.up + (Vector2.right * -onWall));
        }
        if(jumpsLeft > 0 && shouldJump)
        {
            
            shouldJump = false;
            StopCoroutine("CancelJump");
            jumpsLeft--;
            Jump(Vector2.up);
        }
        
        
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
        rb.AddForce(dir * jumpForce);
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
}
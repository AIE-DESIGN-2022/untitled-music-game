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
    [Header("Jump Variables")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpBufferTimer = .1f;
    [Header("GroundChecking")]
    [SerializeField] LayerMask ground;
    [SerializeField] Vector2 gcOffset;
    [SerializeField] Vector2 gcSize;
    public bool grounded = true;
    bool shouldJump = false;
    Rigidbody2D rb;
    float inputDir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputDir = Input.GetAxisRaw("Horizontal") * moveForce;
        if (Input.GetKeyDown(KeyCode.Space)) { shouldJump = true; StartCoroutine("CancelJump"); }
        grounded = Physics2D.OverlapBox(transform.position + (Vector3)gcOffset, gcSize,0,ground);

    }
    private void FixedUpdate()
    {
        if(inputDir != 0)
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
        if(grounded && shouldJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            shouldJump = false;
            StopCoroutine("CancelJump");
        }
    }
    void ApplyCounterForce()
    {
        Vector2 vel = rb.velocity;
        vel.y = 0;
        vel = -vel;
        rb.AddForce(vel * counterForce);
    }

    IEnumerator CancelJump()
    {
        yield return new WaitForSeconds(jumpBufferTimer);
        shouldJump = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)gcOffset, (Vector3)gcSize);
    }

}
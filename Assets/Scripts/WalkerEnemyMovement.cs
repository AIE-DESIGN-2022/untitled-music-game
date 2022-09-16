using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Vector3 checkOffset;
    public float checkRad;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if(Mathf.Sign(speed) <0)
        {
            checkOffset.x = -Mathf.Abs(checkOffset.x);
        }
        else
        {
            checkOffset.x = Mathf.Abs(checkOffset.x);
        }
        if (!Physics2D.OverlapCircle(transform.position + checkOffset, checkRad, ground))
        {
            speed = -speed;
        }
        GetComponent<SpriteRenderer>().flipX = speed > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = -speed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + checkOffset, checkRad);
    }
}

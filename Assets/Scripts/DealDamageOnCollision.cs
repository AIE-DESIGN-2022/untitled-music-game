using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnCollision : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage,transform.position);
        }
    }
}

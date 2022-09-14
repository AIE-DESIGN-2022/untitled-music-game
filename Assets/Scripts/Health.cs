using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public bool canTakeDmg = true;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}

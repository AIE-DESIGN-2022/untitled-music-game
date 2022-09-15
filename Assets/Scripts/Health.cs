using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDeathEvent;
    public float health = 100;
    [SerializeField] float maxHealth = 100;
    public bool canTakeDmg = true;
    [SerializeField] bool destroyOnDeath = true;

    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        OnDeathEvent.Invoke();
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]AudioSource damageSound;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnHitEvent;
    public float health = 100;
    public float maxHealth = 100;
    public bool canTakeDmg = true;
    [SerializeField] bool destroyOnDeath = true;
    public Vector2 lastHitFrom;
    [SerializeField] float iFrameDuration;
    private void Start()
    {
        
        health = maxHealth;
    }
    public void TakeDamage(float damage,Vector2 damageOrigin)
    {
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        lastHitFrom = damageOrigin;
        
        OnHitEvent.Invoke();
        if(damageSound)
        damageSound.Play();
        health -= damage;
        
        if(iFrameDuration > 0)
        {
            GetIFrames();
        }
        
    }

    void GetIFrames()
    {
        PauseDmg(iFrameDuration);
        if (GetComponent<Movement>())
        {
            GetComponent<Movement>().OnHit(iFrameDuration, lastHitFrom);
        }
    }
    IEnumerator PauseDmg(float time)
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(iFrameDuration);
        canTakeDmg = true;
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

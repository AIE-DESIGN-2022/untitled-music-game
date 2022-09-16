using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    public Checkpoint lastCheckpoint;
    public void OnDeath()
    {
        transform.position = lastCheckpoint.transform.position;
        Health health = GetComponent<Health>();
        health.health = health.maxHealth;
        
    }
}

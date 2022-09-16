using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkPointIndex;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnAtCheckpoint player = collision.GetComponent<RespawnAtCheckpoint>();
        if (player)
        {
            if(player.lastCheckpoint.checkPointIndex < checkPointIndex)
            {
                player.lastCheckpoint = this;
            }

        }
    }
}

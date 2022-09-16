using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint : MonoBehaviour
{
    public int checkPointIndex;
    [SerializeField] bool isEnd = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnAtCheckpoint player = collision.GetComponent<RespawnAtCheckpoint>();
        if (player)
        {
            if (isEnd)
            {
                SaveManager.instance.SaveProgress(SceneManager.GetActiveScene().buildIndex);
            }
            if(player.lastCheckpoint.checkPointIndex < checkPointIndex)
            {
                player.lastCheckpoint = this;
            }

        }
    }
}

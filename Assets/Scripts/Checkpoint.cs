using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class Checkpoint : MonoBehaviour
{
    public int checkPointIndex;
    public UnityEvent OnEnd;
    [SerializeField] bool isEnd = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnAtCheckpoint player = collision.GetComponent<RespawnAtCheckpoint>();
        if (player)
        {
            if (isEnd)
            {
                if (!SaveManager.instance) { return; }
                if(SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 1)
                SaveManager.instance.SaveProgress(SceneManager.GetActiveScene().buildIndex+1);
                OnEnd.Invoke();
            }
            if(player.lastCheckpoint.checkPointIndex < checkPointIndex)
            {
                player.lastCheckpoint = this;
            }

        }
    }
}

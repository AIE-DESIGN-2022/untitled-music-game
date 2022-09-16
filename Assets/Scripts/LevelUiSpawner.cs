using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUiSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.sceneCountInBuildSettings <= 0)
        {
            return;
        }
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            GameObject go = Instantiate(prefab,transform);
        }
    }

}

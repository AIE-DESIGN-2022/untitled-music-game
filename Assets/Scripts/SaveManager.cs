using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int HighestLevel =1;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //todo: load from file
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }
    public void SaveProgress(int level)
    {
        if(level <= HighestLevel) { return; }
        HighestLevel = level;
        //save to file
    }
    
}

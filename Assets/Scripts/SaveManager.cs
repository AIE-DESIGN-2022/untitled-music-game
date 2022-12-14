using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int HighestLevel =1;
    SaveToFile file;
    // Start is called before the first frame update
    void Awake()
    {
        file = GetComponent<SaveToFile>();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //todo: load from file
            file.Load();
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
        file.Save();
    }
    
}

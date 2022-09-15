using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    [SerializeField] int beatsToToggle =1;
    [SerializeField] bool toggleOnHalfBeat;
    int counter = 0;
    private void Update()
    {
        if (toggleOnHalfBeat)
        {
            if (BeatManager.halfBeatFrame)
            {
                counter++;
                if(counter >= beatsToToggle)
                {
                    Toggle();
                    counter = 0;
                }
            }
        }
        else
        {
            if (BeatManager.beatFrame) 
            {
                counter++;
                if(counter >= beatsToToggle)
                {
                    Toggle();
                    counter = 0;
                }
                
            }
        }
        
    }
    public void Toggle()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
        }
    }
}

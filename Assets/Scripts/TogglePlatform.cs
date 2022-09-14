using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    public static bool play = true;
    public float bpm;
    public bool halfBeat = false;
    float maxTime = 0;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        maxTime = bpm / 60;
        if (halfBeat)
        {
            maxTime = maxTime / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime)
        {
            Toggle();
            timer = 0;
        }
        timer+=Time.deltaTime;
    }
    void Toggle()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
        }
    }
}

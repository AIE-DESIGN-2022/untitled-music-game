using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BeatManager : MonoBehaviour
{
    public static bool beatFrame = false;
    AudioSource sound;
    public bool play = true;
    public float bpm;
    public static bool halfBeatFrame = false;
    float maxTime = 0;
    bool usedHBeat = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        maxTime = bpm / 60;
        
        maxTime = 1 / maxTime;
        
    }

    public void Pause()
    {
        if (play)
        {
            sound.Pause();
            play = false;
        }
        else
        {
            sound.UnPause();
            play = true;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime / 2 && !usedHBeat)
        {
            halfBeatFrame = true;
            usedHBeat = true;
        }
        if (timer > maxTime)
        {   
            usedHBeat = false;
            halfBeatFrame = true;
            beatFrame = true;
            timer = 0;
        }
        else
        {
            beatFrame = false;
        }
        timer += Time.deltaTime;
    }
}

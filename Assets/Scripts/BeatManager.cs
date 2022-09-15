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
        if (timer > maxTime)
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceLevel : BaseLevel
{
    private float timer;
    private float bestLap;
    private bool playing = true;

    private int checkpointTotal;
    private int checkpointCurrent;

    private RaceUI ui;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        checkpointTotal = GetComponentsInChildren<RaceCheckpoint>().Length;
        ui = gameUI.SetRaceUI();
        bestLap = PlayerPrefs.GetFloat(gameObject.name + ":bestlap", float.MaxValue);
        Debug.Log(bestLap);
        ui.Init(bestLap);
        timer = 0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            ui.SetTimerText(timer);
        }

        if (timer > 100 && playing)
        {
            playing = false; // Prevent multiple calls of ExitLevel in case of multithreading.
            ExitLevel();
        }
    }

    public void Checkpoint(int checkpointNr)
    {
        if (checkpointNr == checkpointCurrent + 1)
        {
            checkpointCurrent++;

            if (checkpointCurrent == checkpointTotal)
            {
                checkpointCurrent = 0;
                
                if (timer < bestLap)
                {
                    bestLap = timer;
                    ui.SetBestLap(timer);
                    PlayerPrefs.SetFloat(gameObject.name + ":bestlap", bestLap);
                }

                timer = 0;
            }
        }
    }
}

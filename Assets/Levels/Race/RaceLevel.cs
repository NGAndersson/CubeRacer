using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceLevel : BaseLevel
{
    public GameObject playerObj;

    private float timer;
    private float bestLap;

    private int checkpointTotal;
    private int checkpointCurrent = 0;

    private RaceUI ui;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.player = Instantiate(playerObj, base.playerStart).GetComponent<BasePlayer>();
        base.Awake();
        Init();
    }

    public override void Init()
    {
        checkpointTotal = GetComponentsInChildren<RaceCheckpoint>().Length;
        ui = gameUI.SetRaceUI(this);
        bestLap = PlayerPrefs.GetFloat(gameObject.name + ":bestlap", float.MaxValue);
        ui.Init(bestLap);
        timer = 0f;
        playing = true;
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
        Debug.Log("Checkpoint: " + checkpointNr);
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

    public override void RestartLevel()
    {
        timer = 0f;
        checkpointCurrent = 0;
        player.gameObject.transform.position = playerStart.transform.position;
        player.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}

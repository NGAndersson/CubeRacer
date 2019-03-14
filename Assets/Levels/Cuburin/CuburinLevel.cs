using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuburinLevel : BaseLevel
{
    public GameObject playerObj;

    private float timer;
    private float record;
    
    private CuburinUI ui;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.player = Instantiate(playerObj, base.playerStart).GetComponent<BasePlayer>();
        base.Awake();
        Init();
    }

    public override void Init()
    {
        ui = gameUI.SetCuburinUI(this);
        record = PlayerPrefs.GetFloat(gameObject.name + ":record", float.MaxValue);
        ui.Init(record);
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

    public void Goal()
    {
        if (timer < record)
        {
            record = timer;
            ui.SetRecord(timer);
            PlayerPrefs.SetFloat(gameObject.name + ":record", record);
        }

        timer = 0;

        ExitLevel();
    }

    public override void RestartLevel()
    {
        Destroy(player.gameObject);
        base.player = Instantiate(playerObj, base.playerStart).GetComponent<BasePlayer>();
        timer = 0f;
    }
}

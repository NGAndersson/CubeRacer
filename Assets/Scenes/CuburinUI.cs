using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuburinUI : BaseGameplayUI
{
    public Text record;

    public Text timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(float recordtime)
    {
        SetTimerText(0);

        if (recordtime == float.MaxValue)
        {
            SetRecord(0);
        }
        else
        {
            SetRecord(recordtime);
        }
    }

    public void SetRecord(float time)
    {
        record.text = formatTime(time);
    }

    public void SetTimerText(float time)
    {
        timer.text = formatTime(time);
    }

    private string formatTime(float time)
    {
        string minutes = Mathf.FloorToInt(time / 60f).ToString("00");
        string seconds = Mathf.FloorToInt(time % 60f).ToString("00");
        string ms = Mathf.FloorToInt((time * 100 - Mathf.Floor(time) * 100)).ToString("00");
        return minutes + ":" + seconds + ":" + ms;
    }
}

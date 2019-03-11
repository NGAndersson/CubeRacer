using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceUI : BaseGameplayUI
{
    public Text bestLap;

    public Text timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(float bestLap)
    {
        SetTimerText(0);

        if (bestLap == float.MaxValue)
        {
            SetBestLap(0);
        }
        else
        {
            SetBestLap(bestLap);
        }
    }

    public void SetBestLap(float lap)
    {
        bestLap.text = formatTime(lap);
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

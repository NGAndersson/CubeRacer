using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructionUI : BaseGameplayUI
{
    public Text pickupCounter;

    public Text timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int pickupTotal, float timeLimit)
    {
        SetTimerText(timeLimit);
        SetPickupCounter(0, pickupTotal);
    }

    public void SetPickupCounter(int current, int total)
    {
        pickupCounter.text = current.ToString() + "/" + total.ToString();
    }

    public void SetTimerText(float time)
    {
        timer.text = time.ToString("#.0");
    }
}

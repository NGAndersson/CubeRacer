using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructionLevel : BaseLevel
{
    public float timeLimit;

    private float timer;
    private bool playing = true;

    private int pickupTotal;
    private int pickupCurrent;

    public GameObject popup;

    private DestructionUI ui;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Init();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (playing)
        {
            timer -= Time.deltaTime;
            ui.SetTimerText(timer);
        }

        if (timer < 0 && playing)
        {
            playing = false; // Prevent multiple calls of ExitLevel in case of multithreading.
            ExitLevel();
        }
    }

    private void Init()
    {
        pickupTotal = GetComponentsInChildren<Fueltank>().Length;
        ui = gameUI.SetDestructionUI(this);
        ui.Init(pickupTotal, timeLimit);
        timer = timeLimit;
    }

    public void Pickup(int time, Vector3 position)
    {
        Instantiate(popup, position, Quaternion.LookRotation(position - Camera.current.transform.position)).GetComponent<TextMesh>().text = "+" + time.ToString();
        timer += time;
        pickupCurrent++;
        ui.SetPickupCounter(pickupCurrent, pickupTotal);

        if (pickupCurrent >= pickupTotal)
        {
            ExitLevel();
        }
    }

    public override void RestartLevel()
    {

    }
}

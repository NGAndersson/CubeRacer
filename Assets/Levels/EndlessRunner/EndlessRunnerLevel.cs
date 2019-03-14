using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessRunnerLevel : BaseLevel
{
    public GameObject playerObj;

    public GameObject chaser;

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
        ui = gameUI.SetRaceUI(this);
        ui.Init(0);
        Camera.main.GetComponent<CameraController>().offset = new Vector3(0, 3, -7);
        playing = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        chaser.transform.position = new Vector3(chaser.transform.position.x, chaser.transform.position.y, chaser.transform.position.z - 4 * Time.deltaTime);
    }

    public override void RestartLevel()
    {
        
        //player.gameObject.transform.position = playerStart.transform.position;
        //player.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public override void ExitLevel()
    {
        Camera.main.GetComponent<CameraController>().offset = new Vector3(0, 15, -3);
        base.ExitLevel();
    }
}

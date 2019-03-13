using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNLevel : BaseLevel
{
    public GameObject playerObj;

    private VNUI ui;

    public TextAsset storyFile;

    private List<Dialogue> story = new List<Dialogue>();

    private int storyTracker = 0;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.player = Instantiate(playerObj, base.playerStart).GetComponent<BasePlayer>();
        string[] chopped = storyFile.text.Split(';', '\n');

        for (int i = 0; i < chopped.Length; i++)
        {
            Dialogue line;
            line.speaker = chopped[i];
            i++;
            line.line = chopped[i];
            story.Add(line);
        }
        base.Awake();
        Init();
    }

    public override void Init()
    {
        ui = gameUI.SetVNUI(this);
        Camera.main.GetComponent<CameraController>().offset = new Vector3(0, 1, -3);
        NextLine();
        playing = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            NextLine();
        }
    }

    public override void RestartLevel()
    {
    }

    public struct Dialogue
    {
        public string line;
        public string speaker;
    }

    private void NextLine()
    {
        if (storyTracker < story.Count)
        {
            Dialogue line = story[storyTracker];
            ui.SetText(line.line, line.speaker);
            storyTracker++;
        }
        else
        {
            ExitLevel();
        }
    }

    public override void ExitLevel()
    {
        // RESET CAMERA OFFSET (FIX LATER WITH DIFFERENT CAMERA CONTROLLERS?)
        Camera.main.GetComponent<CameraController>().offset = new Vector3(0, 15, -3);
        base.ExitLevel();
    }
}

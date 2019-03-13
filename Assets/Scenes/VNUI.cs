using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNUI : BaseGameplayUI
{
    public Text dialogue;

    public Text speaker;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init()
    {
        
    }

    public void SetText(string line, string name)
    {
        speaker.text = name;
        dialogue.text = line;
    }
}

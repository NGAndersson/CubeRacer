using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject destructionUI;

    public GameObject raceUI;

    public GameObject cuburinUI;

    public Button restart;
    public Button exit;

    private BaseLevel currentLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DestructionUI SetDestructionUI(BaseLevel level)
    {
        currentLevel = level;
        DisableAllUI();
        destructionUI.SetActive(true);
        SetGenericUI();
        return destructionUI.GetComponent<DestructionUI>();
    }

    public CuburinUI SetCuburinUI(BaseLevel level)
    {
        currentLevel = level;
        DisableAllUI();
        cuburinUI.SetActive(true);
        SetGenericUI();
        return cuburinUI.GetComponent<CuburinUI>();
    }

    public RaceUI SetRaceUI(BaseLevel level)
    {
        currentLevel = level;
        DisableAllUI();
        raceUI.SetActive(true);
        SetGenericUI();
        return raceUI.GetComponent<RaceUI>();
    }
    
    private void DisableAllUI()
    {
        destructionUI.SetActive(false);
        raceUI.SetActive(false);
    }

    private void SetGenericUI()
    {
        exit.onClick.AddListener(() => { currentLevel.ExitLevel(); });
        restart.onClick.AddListener(() => { currentLevel.RestartLevel(); });
    }
}

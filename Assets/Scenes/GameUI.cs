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

    public GameObject vnUI;

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
    /*
    public T SetDestructionUI<T>(BaseLevel level) where T : BaseGameplayUI
    {
        currentLevel = level;
        DisableAllUI();
        destructionUI.SetActive(true);
        SetGenericUI();
        return destructionUI.GetComponent<T>();
    }*/

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

    public VNUI SetVNUI(BaseLevel level)
    {
        currentLevel = level;
        DisableAllUI();
        vnUI.SetActive(true);
        SetGenericUI();
        return vnUI.GetComponent<VNUI>();
    }
    
    private void DisableAllUI()
    {
        destructionUI.SetActive(false);
        raceUI.SetActive(false);
        cuburinUI.SetActive(false);
        vnUI.SetActive(false);
    }

    private void SetGenericUI()
    {
        exit.onClick.AddListener(() => { currentLevel.ExitLevel(); });
        restart.onClick.AddListener(() => { currentLevel.RestartLevel(); });
    }
}

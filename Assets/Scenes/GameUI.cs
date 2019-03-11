using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject DestructionUI;

    public GameObject RaceUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DestructionUI SetDestructionUI()
    {
        DisableAllUI();
        DestructionUI.SetActive(true);
        return DestructionUI.GetComponent<DestructionUI>();
    }
    
    public RaceUI SetRaceUI()
    {
        DisableAllUI();
        RaceUI.SetActive(true);
        return RaceUI.GetComponent<RaceUI>();
    }
    

    private void DisableAllUI()
    {
        DestructionUI.SetActive(false);
        RaceUI.SetActive(false);
    }
}

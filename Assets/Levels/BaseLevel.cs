using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevel : MonoBehaviour
{
    [HideInInspector]
    public GameUI gameUI;

    [HideInInspector]
    public PlayerController player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void ExitLevel()
    {
        Debug.Log("Exit level");
        FindObjectOfType<UIManager>().ExitLevel();
        Destroy(player.gameObject);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevel : MonoBehaviour
{
    [HideInInspector]
    public GameUI gameUI;

    [HideInInspector]
    public BasePlayer player;

    public Transform playerStart;

    [HideInInspector]
    public bool playing;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void Init();

    public virtual void ExitLevel()
    {
        FindObjectOfType<UIManager>().ExitLevel();
        Destroy(gameObject);
    }

    public abstract void RestartLevel();
}

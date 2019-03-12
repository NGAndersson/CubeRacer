using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> levels;
    public GameObject player;
    public Camera cam;

    private GameObject currentLevel = null;

    public GameObject MainMenu;
    public GameObject GameUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int id)
    {
        if (levels.Count > id)
        {
            MainMenu.SetActive(false);
            GameUI.SetActive(true);
            
            currentLevel = Instantiate(levels[id]);
        }
    }

    public void ExitLevel()
    {
        MainMenu.SetActive(true);
        GameUI.SetActive(false);
    }

    public enum LevelType
    {
        Destruction,
        Race
    }
}

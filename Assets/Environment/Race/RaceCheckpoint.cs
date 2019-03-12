using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{

    public int order = 1;

    private RaceLevel gameplay;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = FindObjectOfType<RaceLevel>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            gameplay.Checkpoint(order);
        }
    }
}

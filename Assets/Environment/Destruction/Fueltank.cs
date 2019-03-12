using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fueltank : MonoBehaviour
{
    public int order = 0;

    private DestructionPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<DestructionPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (order == player.pickupOrder)
        {
            // Blink or some shit.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((order == 0 || player.pickupOrder == order) && collision.gameObject.GetComponent<BaseBullet>() != null)
        {
            player.Pickup(DestructionPlayer.PickupType.fuel, order > 0);

            Destroy(gameObject);
        }
    }
}

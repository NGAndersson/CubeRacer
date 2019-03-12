using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float cooldown = 1000;

    private float timer = 0f;

    [SerializeField]
    public List<GameObject> bullets;

    private GameObject bullet;

    public BulletTypes BulletType
    {
        get
        {
            return BulletType;
        }
        set
        {
            switch (value)
            {
                case BulletTypes.Peashot:
                    bullet = bullets[0];
                    cooldown = 0;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 1000;

        // Prevent overflow
        if (timer > cooldown)
        {
            timer = cooldown + 0.001f;
        }
    }

    public bool Shoot()
    {
        if (timer > cooldown)
        {
            // Spawn and fire bullet
            GameObject instance = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            instance.GetComponent<BaseBullet>().Fire();
            
            timer = timer - cooldown;
            return true;
        }

        return false;
    }

    public enum BulletTypes
    {
        Peashot
    }
}

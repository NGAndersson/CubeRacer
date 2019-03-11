using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    private float ttl = 5;
    private float timer = 0;

    public virtual void Update()
    {
        timer += Time.deltaTime;

        if (timer > ttl)
        {
            Destroy(gameObject);
        }
    }

    public abstract void Fire();

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashot : BaseBullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Fire()
    {
        GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*1000);
    }
}

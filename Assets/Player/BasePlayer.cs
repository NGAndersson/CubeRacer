using UnityEngine;
using System.Collections;

public abstract class BasePlayer : MonoBehaviour
{
    public Camera gameCamera;

    // Use this for initialization
    public virtual void Start()
    {
        gameCamera = Camera.main;
        gameCamera.GetComponent<CameraController>().Init(this.transform);
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public abstract void Init();
}

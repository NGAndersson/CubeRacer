using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    private Vector3 lookAtVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            this.gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, 0.6f);
            this.transform.LookAt(player);
        }
    }

    public void Init(Transform player)
    {
        this.player = player;
        this.transform.position = player.transform.position + offset;
        lookAtVector = (player.transform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(lookAtVector);
    }
}

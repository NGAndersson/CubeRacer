using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup3D : MonoBehaviour
{
    public float timeToFade = 2;
    public float fadeTime = 1;

    private float timer = 0;
    private Vector3 startPos;

    TextMesh text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y + 2, startPos.z), timer/timeToFade);
        
        if (timer > timeToFade)
        {
            text.color = new Color(1, 1, 0, Mathf.Lerp(1, 0, (timer - timeToFade)/fadeTime));

            if (timer > timeToFade + fadeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}

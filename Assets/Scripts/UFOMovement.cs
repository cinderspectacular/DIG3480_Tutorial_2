using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public Vector2 startMark;
    public Vector2 endMark;
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMark, endMark);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered/journeyLength;
        transform.position = Vector2.Lerp(startMark, endMark, Mathf.PingPong(fracJourney, 1));
    }
}

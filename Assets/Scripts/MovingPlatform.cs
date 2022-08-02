using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int curPoint;

    public Transform platform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[curPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(platform.position, points[curPoint].position) < 0.05f)
        {
            curPoint++;
            if(curPoint >= points.Length)
            {
                curPoint = 0;
            }
        }
    }
}

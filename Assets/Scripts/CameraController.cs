using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public float minY, maxY, height;

    public float smoothSpeed;

    private void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        Vector3 newCamPos = new Vector3(target.position.x, Mathf.Clamp(target.position.y + height, minY, maxY), transform.position.z);
        Vector3 smoothedPos = Vector3.Lerp(transform.position, newCamPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}

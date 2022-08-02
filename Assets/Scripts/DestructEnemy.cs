using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructEnemy : MonoBehaviour
{
    public bool isMonitor, isWheels;
    private float randomForce;

    private Rigidbody2D RGB;

    public float monitorForceValue, wheelsForceValue;

    // Start is called before the first frame update
    void Start()
    {
        RGB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorForce();
        WheelsForce();
    }

    void MonitorForce()
    {
        randomForce = Random.Range(-3, 3);

        if (isMonitor)
        {
            RGB.velocity = new Vector2(randomForce, monitorForceValue);
            isMonitor = false;
        }
    }
    void WheelsForce()
    {
        randomForce = Random.Range(-0.5f, 0.5f);

        if (isWheels)
        {
            RGB.velocity = new Vector2(randomForce, wheelsForceValue);
            isWheels = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}

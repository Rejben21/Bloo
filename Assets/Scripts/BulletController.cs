using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject destroyPatricle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-moveSpeed * transform.localScale.x * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(destroyPatricle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

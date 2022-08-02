using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isVitamin;

    private bool isCollected;

    public GameObject pickupParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(isVitamin && isCollected == false)
            {
                LevelManager.instance.vitaminsColected++;

                isCollected = true;

                Instantiate(pickupParticle, transform.position, transform.rotation);

                Destroy(gameObject);

                UIController.instance.UpdateVitaminCount();
            }
        }
    }
}

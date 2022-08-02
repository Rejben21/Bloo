using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public GameObject deathEffect, monitor, wheels, splashParticle;

    public GameObject collectible;
    [Range(0, 100)] public float chanceToDrop;

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
        if (other.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, transform.rotation);
            Instantiate(monitor, other.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);
            Instantiate(wheels, other.transform.position + new Vector3(0, -0.406f, 0), transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position + new Vector3(0, 0, 0), transform.rotation);
            }
        }
        else if (other.CompareTag("Flying Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, transform.rotation);
            Instantiate(monitor, other.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position + new Vector3(0, 0, 0), transform.rotation);
            }
        }
        else if (other.CompareTag("Shooter Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, transform.rotation);
            Instantiate(monitor, other.transform.position + new Vector3(0, 0.25f, 0), transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position + new Vector3(0, 0, 0), transform.rotation);
            }
        }
        else if (other.CompareTag("Water") && !PlayerController.instance.isGrounded)
        {
            Instantiate(splashParticle, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
        }
    }
}

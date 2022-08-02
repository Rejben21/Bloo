using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallToDestroy : MonoBehaviour
{
    public Sprite clear, destroy1, destroy2;
    public GameObject destroyEffect;
    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        SR.sprite = clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerController.instance.isRock)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            if (SR.sprite == clear)
            {
                SR.sprite = destroy1;
            }
            else if (SR.sprite == destroy1)
            {
                SR.sprite = destroy2;
            }
            else if (SR.sprite == destroy2)
            {
                gameObject.SetActive(false);
                PlayerController.instance.Bounce();
            }
        }
    }
}

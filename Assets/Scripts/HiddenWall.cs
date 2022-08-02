using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    private bool shouldFade;
    private SpriteRenderer SR;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        shouldFade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.MoveTowards(SR.color.a, 1, fadeSpeed * Time.deltaTime));
        }
        else
        {
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.MoveTowards(SR.color.a, 0, fadeSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            shouldFade = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shouldFade = true;
        }
    }
}

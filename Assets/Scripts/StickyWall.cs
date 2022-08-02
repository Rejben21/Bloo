using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWall : MonoBehaviour
{
    private bool isSticked;
    public Collider2D colider;
    public PointEffector2D point;


    // Start is called before the first frame update
    void Start()
    {
        colider = GetComponent<Collider2D>();
        point = GetComponent<PointEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.isGlue)
        {
            colider.enabled = true;
        }
        else
        {
            colider.enabled = false;
        }
        
        if(isSticked && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StickOff());
            PlayerController.instance.RGB.velocity = new Vector2(PlayerController.instance.RGB.velocity.x, PlayerController.instance.jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isSticked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isSticked = false;
        }
    }

    public IEnumerator StickOff()
    {
        point.forceMagnitude = 0;
        yield return new WaitForSeconds(0.3f);
        point.forceMagnitude = -1000;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPanel : MonoBehaviour
{

    public bool canSwitch;
    public bool isSwitched;

    public GameObject TurnParticle;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isSwitched", isSwitched);

        Action();
    }

    private void Action()
    {
        if(canSwitch && PlayerController.instance.isMetal && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(TurnParticle, transform.position + new Vector3(-0.4f,0,0), transform.rotation);
            if(isSwitched)
            {
                isSwitched = false;
            }
            else
            {
                isSwitched = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            canSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canSwitch = false;
        }
    }
}

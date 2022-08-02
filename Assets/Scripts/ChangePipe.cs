using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePipe : MonoBehaviour
{
    public bool toRock, toAir, toMetal, toGlue, toNormal;

    public GameObject changePoint;

    public Sprite rockPipe, airPipe, metalPipe, gluePipe, normalPipe;
    public SpriteRenderer upSprite, downSprite;

    private int playerNumber;

    private Animator anim;

    private bool canSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toRock)
        {
            anim.SetBool("toRock", toRock);
            upSprite.sprite = rockPipe;
            downSprite.sprite = rockPipe;
            playerNumber = 2;
        }
        else if(toAir)
        {
            anim.SetBool("toAir", toAir);
            upSprite.sprite = airPipe;
            downSprite.sprite = airPipe;
            playerNumber = 3;
        }
        else if (toMetal)
        {
            anim.SetBool("toMetal", toMetal);
            upSprite.sprite = metalPipe;
            downSprite.sprite = metalPipe;
            playerNumber = 4;
        }
        else if (toGlue)
        {
            anim.SetBool("toGlue", toGlue);
            upSprite.sprite = gluePipe;
            downSprite.sprite = gluePipe;
            playerNumber = 5;
        }
        else if (toNormal)
        {
            anim.SetBool("toNormal", toNormal);
            upSprite.sprite = normalPipe;
            downSprite.sprite = normalPipe;
            playerNumber = 1;
        }

        Action();
    }

    private void Action()
    {
        if (canSwitch && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ChangeCo());
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
        if(other.CompareTag("Player"))
        {
            canSwitch = false;
        }
    }

    private IEnumerator ChangeCo()
    {
        PlayerController.instance.stopInput = true;
        PlayerController.instance.transform.position = changePoint.transform.position;

        anim.SetBool("Change", true);

        yield return new WaitForSeconds(.2f);

        PlayerController.instance.whatIsPlayer = playerNumber;

        yield return new WaitForSeconds(.2f);

        PlayerController.instance.stopInput = false;
        anim.SetBool("Change", false);
    }
}

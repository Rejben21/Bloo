using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1Controller : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D RGB;
    private Animator anim;
    public SpriteRenderer SR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    // Start is called before the first frame update
    void Start()
    {
        RGB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                RGB.velocity = new Vector2(moveSpeed, RGB.velocity.y);

                SR.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                RGB.velocity = new Vector2(-moveSpeed, RGB.velocity.y);

                SR.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            anim.SetBool("speed", true);

            if (moveCount <= 0)
            {
                waitCount = Random.Range(1f,3f);
            }
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            RGB.velocity = new Vector2(0, RGB.velocity.y);
            anim.SetBool("speed", false);

            if (waitCount <= 0)
            {
                moveCount = moveTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRobotController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int curPoint;

    public float distanceToAttack, chaseSpeed;

    private Vector3 attackTarget;
    public float waitAfterAttack;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttack)
            {

                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[curPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[curPoint].position) < 0.05f)
                {
                    curPoint++;
                    if (curPoint >= points.Length)
                    {
                        curPoint = 0;
                    }
                }

                if (transform.position.x < points[curPoint].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x > points[curPoint].position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                if (attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }
}

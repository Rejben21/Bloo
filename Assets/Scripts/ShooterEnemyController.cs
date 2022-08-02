using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingPoint;
    private float shotCounter;
    public float timeBetweenShots;
    private bool shoot;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0 && shoot == true)
        {
            shotCounter = timeBetweenShots;
            var newBullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
            newBullet.transform.localScale = shootingPoint.localScale;
        }

        anim.SetBool("isShooting", shoot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            shoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shoot = false;
        }
    }

    public IEnumerator ShootCo()
    {
        yield return new WaitForSeconds(2);
    }
}

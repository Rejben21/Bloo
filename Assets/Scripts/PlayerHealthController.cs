using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxLifes, curLifes;

    public float invincibleLenght;
    private float invincibleCounter;

    private SpriteRenderer SR;

    public GameObject normalHurtParticle, metalHurtParticle, airHurtParticle, glueHurtParticle;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        curLifes = maxLifes;
    }

    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1);
            }
        }
        AirHealth();
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            //curlifes -=1;
            curLifes--;

            if (curLifes <= 0)
            {

                LevelManager.instance.deaths++;
                UIController.instance.UpdateDeathsCount();
                LevelManager.instance.RespawnPlayer();
                PlayerController.instance.transform.localScale = new Vector3(1, 1, 1);
                UIController.instance.UpdateLifeDisplay();
            }
            else
            {
                invincibleCounter = invincibleLenght;
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.5f);

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateLifeDisplay();

            if(PlayerController.instance.isNormal)
            {
                Instantiate(normalHurtParticle, transform.position, transform.rotation);
            }
            else if (PlayerController.instance.isMetal)
            {
                Instantiate(metalHurtParticle, transform.position, transform.rotation);
            }
            else if (PlayerController.instance.isAir)
            {
                Instantiate(airHurtParticle, transform.position, transform.rotation);
            }
            else if (PlayerController.instance.isGlue)
            {
                Instantiate(glueHurtParticle, transform.position, transform.rotation);
            }
        }
    }

    public void AirHealth()
    {
        if (PlayerController.instance.isAir)
        {
            maxLifes = 1;
            curLifes = 1;
            UIController.instance.UpdateLifeDisplay();
        }
        else
        {
            maxLifes = 3;
            UIController.instance.UpdateLifeDisplay();
        }
    }
}

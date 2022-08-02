using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static Water instance;

    public BuoyancyEffector2D efector;
    public Collider2D colider;
    public bool isInWater;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.isNormal)
        {
            efector.density = 2.5f;
        }
        else if(PlayerController.instance.isRock)
        {
            efector.density = 2;
        }
        else if (PlayerController.instance.isAir)
        {
            efector.density = 8;
        }
        else if (PlayerController.instance.isMetal && PlayerController.instance.canSwim)
        {
            efector.density = 2;
        }
        else if (PlayerController.instance.isGlue && PlayerController.instance.canSwim)
        {
            efector.density = 2.5f;
        }

        if (Input.GetKey(KeyCode.S) && PlayerController.instance.isInWater && PlayerController.instance.isNormal)
        {
            efector.density = 2;
        }
        else if(Input.GetKey(KeyCode.S) && PlayerController.instance.isInWater && PlayerController.instance.isGlue)
        {
            efector.density = 2;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!PlayerController.instance.canSwim && PlayerController.instance.isMetal)
            {
                PlayerHealthController.instance.curLifes = 1;
                PlayerHealthController.instance.DealDamage();
            }
            else if(!PlayerController.instance.canSwim && PlayerController.instance.isGlue)
            {
                PlayerHealthController.instance.curLifes = 1;
                PlayerHealthController.instance.DealDamage();
            }
            
            PlayerController.instance.isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController.instance.isInWater = false;
    }
}

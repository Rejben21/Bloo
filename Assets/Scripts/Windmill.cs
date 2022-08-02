using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    private Animator anim;
    public bool turnOn;

    public ParticleSystem windParticle1, windParticle2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Turn On", turnOn);

        if(turnOn == true)
        {
            if(!windParticle1.isPlaying)
            {
                windParticle1.Play();
            }
            if (!windParticle2.isPlaying)
            {
                windParticle2.Play();
            }
        }
        else
        {
            if(windParticle1.isPlaying)
            {
                windParticle1.Stop();
            }
            if (windParticle2.isPlaying)
            {
                windParticle2.Stop();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer SR;

    public Sprite cpOn, cpOff;

    public Transform spawnPoint;

    public GameObject trapdoor;
    private BoxCollider2D colider;
    private SpriteRenderer trapdoorSR;

    public float fadeSpeed;
    private bool shouldFade, shouldFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        colider = trapdoor.GetComponent<BoxCollider2D>();
        trapdoorSR = trapdoor.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            trapdoorSR.color = new Color(trapdoorSR.color.r, trapdoorSR.color.g, trapdoorSR.color.b, Mathf.MoveTowards(trapdoorSR.color.a, 1, fadeSpeed * Time.deltaTime));
            if (trapdoorSR.color.a == 1)
            {
                shouldFade = false;
            }
        }

        if (shouldFadeOut)
        {
            trapdoorSR.color = new Color(trapdoorSR.color.r, trapdoorSR.color.g, trapdoorSR.color.b, Mathf.MoveTowards(trapdoorSR.color.a, 0.4f, fadeSpeed * Time.deltaTime));
            if (trapdoorSR.color.a == 0.4f)
            {
                shouldFadeOut = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoint();

            shouldFadeOut = false;
            SR.sprite = cpOn;
            colider.enabled = true;
            shouldFade = true;
            
            PlayerController.instance.transform.position = spawnPoint.position;

            PlayerHealthController.instance.curLifes = PlayerHealthController.instance.maxLifes;
            UIController.instance.UpdateLifeDisplay();
            CheckpointController.instance.SetSpawnPoint(spawnPoint.position);
        }
        else if (other.tag == "Untagged")
        {
            other.gameObject.SetActive(false);
        }
    }

    public void ResetCheckpoint()
    {
        shouldFade = false;
        SR.sprite = cpOff;
        colider.enabled = false;
        shouldFadeOut = true;
    }
}

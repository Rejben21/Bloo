using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.deaths++;
            UIController.instance.UpdateDeathsCount();
            LevelManager.instance.RespawnPlayer();
            PlayerController.instance.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

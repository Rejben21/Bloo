using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public GameObject player;
    public GameObject ventToGo;
    public Collider2D otherVentCollider;
    private CircleCollider2D playerColider;

    // Start is called before the first frame update
    void Start()
    {
        otherVentCollider = ventToGo.GetComponent<Collider2D>();
        playerColider = player.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.isGlue == true)
        {
            StartCoroutine(TeleportCo());
        }
    }

    public IEnumerator TeleportCo()
    {
        otherVentCollider.enabled = false;
        player.SetActive(false);

        yield return new WaitForSeconds(.2f);

        CameraController.instance.target = ventToGo.transform;

        yield return new WaitForSeconds(.6f);

        CameraController.instance.target = player.transform;
        player.transform.position = ventToGo.transform.position + new Vector3( 0, -0.3f, 0);
        player.transform.localScale = ventToGo.transform.localScale;
        playerColider.offset = new Vector2(0, 0.009278297f);
        playerColider.radius = 0.4907217f;
        player.SetActive(true);
        PlayerController.instance.RGB.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(.2f);

        otherVentCollider.enabled = true;
    }
}

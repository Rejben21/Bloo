using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int deaths;

    public int vitaminsColected;

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

    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
        PlayerController.instance.transform.localScale = new Vector3(1, 1, 1);
        PlayerController.instance.GetComponent<CircleCollider2D>().radius = 0.4907217f;
        PlayerController.instance.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.009278297f);
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1 / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1 / UIController.instance.fadeSpeed) + 0.2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        PlayerController.instance.transform.localScale = new Vector3(1, 1, 1);

        PlayerHealthController.instance.curLifes = PlayerHealthController.instance.maxLifes;
        UIController.instance.UpdateLifeDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true;

        yield return new WaitForSeconds(1);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(0.5f);

        UIController.instance.LCAnim.SetBool("LCIn", true);
    }
}

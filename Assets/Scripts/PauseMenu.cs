using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string restartLevel, levelSelect, mainMenu, nextLevel;
    public GameObject pauseScreen;
    public bool isPaused;

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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(restartLevel);
        Time.timeScale = 1f;
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void EndMainMenu()
    {
        StartCoroutine(MainMenuCo());
    }

    public void EndNextLevel()
    {
        StartCoroutine(NextLevelCo());
    }
    public void EndRestartLevel()
    {
        StartCoroutine(RestartLevelCo());
    }

    private IEnumerator MainMenuCo()
    {
        UIController.instance.LCAnim.SetBool("LCIn", false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(mainMenu);
    }

    private IEnumerator NextLevelCo()
    {
        UIController.instance.LCAnim.SetBool("LCIn", false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextLevel);
    }

    private IEnumerator RestartLevelCo()
    {
        UIController.instance.LCAnim.SetBool("LCIn", false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(restartLevel);
    }
}

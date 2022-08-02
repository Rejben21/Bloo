using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image life1, life2, life3;
    public Sprite life, noLife;

    public Text vitaminText, vitaminCounter;
    public Text deathsCounter;
    private int score;
    public Text scoreText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject LevelComplete;
    public Animator LCAnim;

    private bool shouldShowNote, shouldHideNote;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateVitaminCount();

        FadeFromBlack();
        LCAnim = LevelComplete.GetComponent<Animator>();
        LCAnim.SetBool("LCIn", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0)
            {
                shouldFadeFromBlack = false;
            }
        }

        UpdateScoreCount();
    }

    public void UpdateLifeDisplay()
    {
        switch (PlayerHealthController.instance.curLifes)
        {
            case 3:
                life1.sprite = life;
                life2.sprite = life;
                life3.sprite = life;

                break;

            case 2:
                life1.sprite = life;
                life2.sprite = life;
                life3.sprite = noLife;

                break;

            case 1:
                life1.sprite = life;
                life2.sprite = noLife;
                life3.sprite = noLife;

                break;

            case 0:
                life1.sprite = noLife;
                life2.sprite = noLife;
                life3.sprite = noLife;

                break;

            default:
                life1.sprite = noLife;
                life2.sprite = noLife;
                life3.sprite = noLife;

                break;
        }
    }

    public void UpdateVitaminCount()
    {
        vitaminCounter.text = LevelManager.instance.vitaminsColected.ToString();
        vitaminText.text = LevelManager.instance.vitaminsColected.ToString();
    }

    public void UpdateDeathsCount()
    {
        deathsCounter.text = LevelManager.instance.deaths.ToString();
    }

    public void UpdateScoreCount()
    {
        if (LevelManager.instance.deaths <= 0)
        {
            scoreText.text = (LevelManager.instance.vitaminsColected * 26).ToString();
        }
        else if (LevelManager.instance.deaths > 0)
        {
            score = (LevelManager.instance.vitaminsColected * 26) - (LevelManager.instance.deaths * 5);
            if (score <= 0)
            {
                scoreText.text = (0).ToString();
            }
            else if (score > 0)
            {
                scoreText.text = score.ToString();
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}

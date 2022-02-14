using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    public GameObject result;
    public Text scoreText;
    public Text timerText;
    public Text highScoreText;
    private int highscore;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("highScore"))
        {
            highscore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            highscore = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Goal.isGoal)
        {
            Time.timeScale = 0f;
            result.SetActive(true);
            scoreText.text = "Score: " + Score.score;
            timerText.text = "Time: " + Mathf.FloorToInt(Timer.time);
            if (Score.score > highscore) highscore = Score.score; 
            highScoreText.text = "HighScore: " + highscore;
        }
        else if (StopUIController.isStop)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OnClickRestart()
    {
        StartCoroutine("RestartAction");
    }

    IEnumerator RestartAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Goal.isGoal = false;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main");
    }

    public void OnClickHome()
    {
        StartCoroutine("HomeAction");
    }

    IEnumerator HomeAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Goal.isGoal = false;
        StopUIController.isStop = false;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Start");
    }
}

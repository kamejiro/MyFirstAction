using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public GameObject result;
    public Text scoreText;
    public Text timerText;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Goal.isGoal)
        {
            result.SetActive(true);
            scoreText.text = "Score: " + Score.score;
            timerText.text = "Time: " + Mathf.FloorToInt(Timer.time);
        }
    }

    public void OnClickRestart()
    {
        StartCoroutine("RestartAction");
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator RestartAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main");
    }

    public void OnClickHome()
    {
        StartCoroutine("HomeAction");
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator HomeAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Start");
    }
}

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

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
    public void ToStart()
    {
        SceneManager.LoadScene("Start");
    }

}

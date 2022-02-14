using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StopUIController : MonoBehaviour
{
    public GameObject stopUI;
    public GameObject helpUI;
    public static bool isStop;
    public Text randomText;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        randomText.text = "óêêîÅF" + Random.Range(1.0f, 100.0f);
        isStop = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //call stopUI
            Time.timeScale = 0f;
            OnClickStop();
        }
    }

    //OnClickHome()ÇÕResultUI.cs

    //OnClickHelp()
    public void OnClickHelp()
    {
        helpUI.SetActive(true);
        stopUI.SetActive(false);
    }

    //OnClickBackFromHelp()
    public void OnClickBackFromHelp()
    {
        stopUI.SetActive(true);
        helpUI.SetActive(false);
    }

    //OnClickStop()
    public void OnClickStop()
    {
        if (!isStop)
        {
            isStop = true;
            Time.timeScale = 0f;
            stopUI.SetActive(true);
        }
        else if (isStop)
        {
            isStop = false;
            Time.timeScale = 1f;
            stopUI.SetActive(false);
        }
    }


}

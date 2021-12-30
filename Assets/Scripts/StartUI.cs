using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStart()
    {
        StartCoroutine("StartAction");
    }

    IEnumerator StartAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main");
    }

    public void OnClickExit()
    {
        StartCoroutine("ExitAction");
    }

    IEnumerator ExitAction()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        //if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //else
        Application.Quit();
        //endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{

    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void ToExit()
    {
        //if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //else
        Application.Quit();
        //endif
    }
}

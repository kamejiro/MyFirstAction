using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!Goal.isGoal)&&(!StopUIController.isStop))
        {
            time += Time.deltaTime;
            GetComponent<Text>().text = "Time: " + Mathf.FloorToInt(time);
        }

    }
}

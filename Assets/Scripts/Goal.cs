using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool isGoal;

    // Start is called before the first frame update
    void Start()
    {
        isGoal = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isGoal = true;
        }
    }
}

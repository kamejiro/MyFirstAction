using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public GameObject item;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("Bullet"))
        {
            Score.score += 1;
            item.SetActive(false);
        }
    }
}

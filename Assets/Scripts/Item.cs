using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public GameObject item;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine("DeleteItem");
        }
    }
    IEnumerator DeleteItem()
    {
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        Score.score += 1;
        item.SetActive(false);
    }
}

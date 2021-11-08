using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject WinLable;

    void Start()
    {
        WinLable.SetActive(false);
        Time.timeScale = 1f;       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        WinLable.SetActive(true);
    }
}

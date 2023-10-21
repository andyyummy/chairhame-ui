using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public KeyCode customKey; // 用於自訂鍵盤按鍵

    void Update()
    {
        if (Input.GetKey(customKey))
        {
            audioSource.Play();
        }
    }
}

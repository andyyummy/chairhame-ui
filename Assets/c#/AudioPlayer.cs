using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    void Update() // 注意這裡是 Update 而不是 start
    {
        if (Input.GetKey(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}

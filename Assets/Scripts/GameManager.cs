using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip loseMusic;
    public AudioClip winClip;
    void Awake()
    {  
        audioSource = GetComponent<AudioSource>();
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = bgMusic;
            audioSource.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioSource.clip = loseMusic;
            audioSource.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            audioSource.clip = winClip;
            audioSource.Play();
        }
    }
}
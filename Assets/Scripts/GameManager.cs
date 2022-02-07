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

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("game quit");
        Application.Quit();
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
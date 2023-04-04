using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public GameObject GameOverMenu;
    public GameObject GameCompletedMenu; // only in the boss scene
    public AudioClip winMusic;
    public AudioClip deathMusic;
    public BackgroundMusic backgroundMusic;
    
    private AudioSource bgAudioSource;
    private PlayerStats playerStats;

    public static bool GameFinished;
    public static bool GameCompleted;
    private bool alreadyOver;

    float completionTime;

    void Start()
    {
        alreadyOver = false;
        GameOverMenu.SetActive(false);

        if (GameCompletedMenu != null)
        {
            GameCompletedMenu.SetActive(false);
        }

        GameFinished = false;
        GameCompleted = false;
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.currentHealth <= 0 && !alreadyOver) 
        {
            GameOver();
        }
    }

    void GameOver()
    {
        alreadyOver = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameOverMenu.SetActive(true);
        backgroundMusic.setBackgroundMusic(winMusic);

        GameFinished = true;
        GameCompleted = true;
    }

    public void EndLevel()
    {       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        Lever.isActivated = false;
        DoorController.isOpen = false;
        LevelManager.isRoomClear = false;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            backgroundMusic.setBackgroundMusic(winMusic);

            GameCompletedMenu.SetActive(true);

            GameFinished = true;
            GameCompleted = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
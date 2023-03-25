using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public GameObject GameOverMenu;
    public GameObject LevelCompletedMenu;
    public AudioClip winMusic;
    public GameObject BackgroundMusic;
    
    private AudioSource bgAudioSource;
    private PlayerStats playerStats;

    public static bool GameFinished;
    public static bool GameCompleted;

    float completionTime;

    void Start()
    {
        GameOverMenu.SetActive(false);
        LevelCompletedMenu.SetActive(false);
        GameFinished = false;
        GameCompleted = false;
        playerStats = player.GetComponent<PlayerStats>();
        bgAudioSource = BackgroundMusic.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerStats.currentHealth <= 0) 
        {
            GameOver();
        }

        Lever.isRoomClear = CheckEnemiesAndDirtLeft();

        if (Lever.isActivated) 
        {
            EndLevel();
        }
    }

    public bool CheckEnemiesAndDirtLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] dirt = GameObject.FindGameObjectsWithTag("Dirt");

        return (enemies.Length <= 0 && dirt.Length <= 0);
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameOverMenu.SetActive(true);

        GameFinished = true;
        GameCompleted = true;
    }

    void EndLevel()
    {        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        bgAudioSource.clip = winMusic;
        bgAudioSource.Play();

        LevelCompletedMenu.SetActive(true);

        GameFinished = true;
        GameCompleted = true;
        Lever.isActivated = false;
        Lever.isRoomClear = false;
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
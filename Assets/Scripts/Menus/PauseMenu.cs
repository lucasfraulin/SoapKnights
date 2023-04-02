using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;

    void Start()
    {
        PlayGame();
    }

    void Update()
    {

        if (!GameEnding.GameFinished) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused) 
                {
                    PlayGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        
    }

    public void PlayGame()
    {
        pauseMenu.SetActive(false);
        ResumeGamePlay();
    }

    public void ResumeGamePlay()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        PauseGamePlay();
    }

    public void PauseGamePlay() 
    {
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}


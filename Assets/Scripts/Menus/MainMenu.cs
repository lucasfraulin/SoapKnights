using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    
    public void BossLevel()
    {
        SceneManager.LoadScene("BossLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

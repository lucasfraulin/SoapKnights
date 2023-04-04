using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    public GameObject controls_Menu;

    public void returnToMain() {
        SceneManager.LoadScene("MainMenu");
    }   
}

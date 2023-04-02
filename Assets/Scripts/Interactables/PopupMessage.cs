using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupMessage : MonoBehaviour
{
    public PauseMenu pauseMenu;

    private bool messageActive = false;

    private void Update()
    {
        pauseMenu.PauseGamePlay();
        if (Input.anyKeyDown && !messageActive)
        {
            pauseMenu.ResumeGamePlay();
            messageActive = true;

            gameObject.SetActive(false);
        }
    }
}
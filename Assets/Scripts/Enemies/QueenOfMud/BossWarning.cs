using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWarning : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public GameObject BossHealthBar;

    private bool bossSpawned = false;

    private void Start()
    {
        BossHealthBar.SetActive(false);
    }

    private void Update()
    {
        pauseMenu.PauseGamePlay();
        if (Input.anyKeyDown && !bossSpawned)
        {
            pauseMenu.ResumeGamePlay();
            bossSpawned = true;

            BossHealthBar.SetActive(true);

            // Disable the warning canvas
            gameObject.SetActive(false);
        }
    }
}
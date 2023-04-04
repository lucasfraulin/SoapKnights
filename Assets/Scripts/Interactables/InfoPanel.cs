using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject uiPanel;
    
    private void Update()
    {
        GameObject[] infoPanelMessages = GameObject.FindGameObjectsWithTag("InfoPanelMessage");

        if (infoPanelMessages.Length > 0)
        {
            uiPanel.SetActive(true);
        }
        else
        {
            uiPanel.SetActive(false);
        }
    }
}
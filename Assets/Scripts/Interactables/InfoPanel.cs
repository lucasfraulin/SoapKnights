using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject uiPanel;
    
    private void Update()
    {
        // Find all active game objects with the "InfoPanelMessage" tag
        GameObject[] infoPanelMessages = GameObject.FindGameObjectsWithTag("InfoPanelMessage");

        // Show the UI panel if there are any active InfoPanelMessage objects
        if (infoPanelMessages.Length > 0)
        {
            uiPanel.SetActive(true);
        }
        // Hide the UI panel if there are no active InfoPanelMessage objects
        else
        {
            uiPanel.SetActive(false);
        }
    }
}
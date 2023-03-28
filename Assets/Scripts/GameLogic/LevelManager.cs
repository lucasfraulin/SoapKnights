using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject LevelCompletedMenu;
    public GameEnding GameEnding;
    
    public static bool isRoomClear = false;

    void Update()
    {
        isRoomClear = CheckEnemiesAndDirtLeft();
        if (DoorController.isOpen || Lever.isActivated) 
        {
            GameEnding.EndLevel();
        }
    }

    public bool CheckEnemiesAndDirtLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] dirt = GameObject.FindGameObjectsWithTag("Dirt");

        return (enemies.Length <= 0 && dirt.Length <= 0);
    }
}

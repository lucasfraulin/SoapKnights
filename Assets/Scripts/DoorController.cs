using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Sprite DoorClosed;
    public Sprite DoorOpen;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject roomNotClearText;
    public static bool isOpen;

    public bool isRoomClear = false;
    private bool withinRangeAndReady = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = DoorClosed;
        interactText.SetActive(false);
        roomNotClearText.SetActive(false);
        isOpen = false;
    }
    
    void Update() 
    {
        isRoomClear = LevelManager.isRoomClear;
        if (isRoomClear && roomNotClearText.activeSelf)
        {
            interactText.SetActive(true);
            roomNotClearText.SetActive(false);
            withinRangeAndReady = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && withinRangeAndReady)
        {
            ExitLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered");
            if (isRoomClear && !isOpen)
            {
                interactText.SetActive(true);
                withinRangeAndReady = true;
            }
            else if (!isRoomClear && !isOpen) 
            {
                roomNotClearText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player exit");
            interactText.SetActive(false);
            roomNotClearText.SetActive(false);
            withinRangeAndReady = false;
        }
    }

    void ExitLevel()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            spriteRenderer.sprite = DoorOpen;
            Destroy(interactText);
        }
    }
}

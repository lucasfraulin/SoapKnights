using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite fullLeverActive;
    public Sprite fullLeverInactive;
    public GameObject Waterfall;
    public Color activeColor;
    public GameObject interactText;
    public GameObject roomNotClearText;

    public static bool isRoomClear = false;
    public static bool isActivated = false;

    private bool withinRangeAndReady = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fullLeverInactive;
        interactText.SetActive(false);
        roomNotClearText.SetActive(false);
    }
    
    void Update() 
    {
        if (isRoomClear && roomNotClearText.activeSelf)
        {
            interactText.SetActive(true);
            roomNotClearText.SetActive(false);
            withinRangeAndReady = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && withinRangeAndReady)
        {
            ActivateLever();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered");
            if (isRoomClear && !isActivated)
            {
                interactText.SetActive(true);
                withinRangeAndReady = true;
            }
            else if (!isRoomClear && !isActivated) 
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

    void ActivateLever()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            spriteRenderer.sprite = fullLeverActive;
            Waterfall.GetComponent<Waterfall>().particleColor = activeColor;
            Destroy(interactText);
        }
    }
}

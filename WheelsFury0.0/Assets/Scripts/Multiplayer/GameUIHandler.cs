using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject playerOverlay;
    [SerializeField] GameObject optionsMenu;

    GameHandler gameHandler;
    // Start is called before the first frame update
    void Awake()
    {
        playerOverlay.SetActive(true);
        optionsMenu.SetActive(false);
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public void ShowOptions()
    {
        playerOverlay.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ShowPlayerOverlay()
    {
        playerOverlay.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OnClick_LeaveRoom()
    {
        Debug.Log("Leaving Room...");
        gameHandler.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

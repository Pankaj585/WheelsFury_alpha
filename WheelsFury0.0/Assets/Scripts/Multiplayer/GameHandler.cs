using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameHandler : MonoBehaviourPunCallbacks
{
    GameUIHandler UIHandler;
    byte currentCanvas = 0;
    private void Awake()
    {
        UIHandler = FindObjectOfType<GameUIHandler>();
    }

    public void LeaveRoom()
    {
        Debug.Log("Leaving room...");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentCanvas == 0)
            {
                currentCanvas = 1;
                UIHandler.ShowOptions();
            } else if(currentCanvas == 1)
            {
                currentCanvas = 0;
                UIHandler.ShowPlayerOverlay();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject lobby;
    [SerializeField] GameObject waitingScreen;
    [SerializeField] GameObject errorScreen;
    Launcher launcher;
    // Start is called before the first frame update
    void Awake()
    {
        loadingScreen.SetActive(true);
        lobby.SetActive(false);
        waitingScreen.SetActive(false);
        errorScreen.SetActive(false);
        launcher = FindObjectOfType<Launcher>();
    }

    public void ShowLobby()
    {
        loadingScreen.SetActive(false);
        waitingScreen.SetActive(false);
        errorScreen.SetActive(false);
        lobby.SetActive(true);
    }

    public void ShowWaitingScreen()
    {
        loadingScreen.SetActive(false);
        waitingScreen.SetActive(true);
        errorScreen.SetActive(false);
        lobby.SetActive(false);
    }

    public void ShowErrorScreen()
    {
        loadingScreen.SetActive(false);
        waitingScreen.SetActive(false);
        errorScreen.SetActive(true);
        lobby.SetActive(false);
    }

    public void OnClick_QuickJoin()
    {
        launcher.JoinRandomRoom();
    }

    public void OnClick_CreateRoom()
    {
        launcher.CreateRoom();
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
}

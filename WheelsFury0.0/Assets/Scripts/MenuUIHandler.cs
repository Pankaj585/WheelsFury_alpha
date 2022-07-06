using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI, garageUI, gameModeUI, settingsUI;

    public void Play()
    {
        mainMenuUI.SetActive(false);
        gameModeUI.SetActive(true);
    }
    public void EnterGarage()
    {
        mainMenuUI.SetActive(false);
        garageUI.SetActive(true);
    }
    public void Settings()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    public void enterMainMenu()
    {
        garageUI.SetActive(false);
        gameModeUI.SetActive(false);
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}

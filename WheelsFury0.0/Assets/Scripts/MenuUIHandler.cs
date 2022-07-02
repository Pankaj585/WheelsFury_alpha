using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject garageUI;

    public void EnterGarage()
    {
        mainMenuUI.SetActive(false);
        garageUI.SetActive(true);
    }

    public void enterMainMenu()
    {
        garageUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeftButton : MonoBehaviour
{
    [SerializeField] AndroidController androidController;

    bool turnLeftButtonPressed;

    void Update()
    {
        androidController = GameObject.Find("Player").GetComponent<AndroidController>();

        if (androidController != null)
        {
            if (turnLeftButtonPressed) { androidController.TurnLeft(); }
            else { androidController.NotTurningLeft(); }
        }
    }


    public void TurnLeftButtonDown()
    {
        turnLeftButtonPressed = true;
    }
    public void TurnLeftButtonUp()
    {
        turnLeftButtonPressed = false;
    }
}

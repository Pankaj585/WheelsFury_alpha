using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightButton : MonoBehaviour
{
    [SerializeField] AndroidController androidController;

    bool turnRightButtonPressed;

    void Update()
    {
        androidController = GameObject.Find("Player").GetComponent<AndroidController>();

        if (androidController != null)
        {
            if (turnRightButtonPressed) { androidController.TurnRight(); }
            else { androidController.NotTurningRight(); }
        }
    }


    public void TurnRightButtonDown()
    {
        turnRightButtonPressed = true;
    }
    public void TurnRightButtonUp()
    {
        turnRightButtonPressed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public AndroidController androidController;
    bool accButtonPressed, turnLeftButtonPressed, revButtonPressed, turnRightButtonPressed;

    private void FixedUpdate()
    {
        if(androidController!= null)
        {
            if (accButtonPressed) { androidController.Accelerate(); }
            else { androidController.NotAccelerating(); }

            if (revButtonPressed) { androidController.Reverse(); }
            else { androidController.NotReversing(); }

            if (turnLeftButtonPressed) { androidController.TurnLeft(); }
            else { androidController.NotTurningLeft(); }

            if (turnRightButtonPressed) { androidController.TurnRight(); }
            else { androidController.NotTurningRight(); }
        }
    }
    public void AccButtonDown()
    {
        accButtonPressed = true;
    }
    public void AccButtonUp()
    {
        accButtonPressed = false;
    }

    public void RevButtonDown()
    {
        revButtonPressed = true;
    }
    public void RevButtonUp()
    {
        revButtonPressed = false;
    }

    public void TurnLeftButtonDown()
    {
        turnLeftButtonPressed = true;
    }
    public void TurnLeftButtonUp()
    {
        turnLeftButtonPressed = false;
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

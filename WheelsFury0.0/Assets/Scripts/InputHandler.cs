using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public AndroidController androidController;
    public float vertical, horizontal;
    bool accButtonPressed, turnLeftButtonPressed, revButtonPressed, turnRightButtonPressed;
    public bool drift;

    //firing
    public bool isFiring;
    public delegate void Button();
    public event Button FireButtonDownEvent;
    public event Button FireButtonUpEvent;
    //end firing
    private void FixedUpdate()
    {
        /*added this code to allow keyboard input, delete later
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        accButtonPressed = vertical > 0;
        revButtonPressed = vertical < 0;
        turnLeftButtonPressed = horizontal < 0;
        turnRightButtonPressed = horizontal > 0;
        end*/
    }
    private void Update()
    {
        //For taking input from keyboard
        TakeKeyboardInput();
        //end

        if (accButtonPressed) 
        {
            if (drift) { if (vertical > 0f) vertical -= Time.deltaTime/2f; }
            else { if (vertical < 1) { vertical += Time.deltaTime; } }
        }
        if (revButtonPressed) { if (vertical > -1) { vertical -= Time.deltaTime; } }
        if (turnLeftButtonPressed) { if (horizontal > -1) { horizontal -= Time.deltaTime * 5f; } }
        if (turnRightButtonPressed) { if (horizontal < 1) { horizontal += Time.deltaTime * 5f; } }


        if(!accButtonPressed && !revButtonPressed && vertical != 0)
        {
            normalizeVertical();
        }
        if (!turnLeftButtonPressed && !turnRightButtonPressed && horizontal != 0)
        {
            normalizeHorizontal();
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
    void normalizeVertical()
    {
        //Mathf.Lerp(input, 0f, 1f);
        if (vertical > 0f) { vertical -= Time.deltaTime; }
        else if (vertical < 0f) { vertical += Time.deltaTime; }
    }
    void normalizeHorizontal()
    {
        //Mathf.Lerp(input, 0f, 1f);
        if (horizontal > 0f) { horizontal -= Time.deltaTime * 5f; }
        else if (horizontal < 0f) { horizontal += Time.deltaTime * 5f; }
    }

    public void Drift()
    {
        if(turnLeftButtonPressed || turnRightButtonPressed)
        {
            drift = true;
        }
    }
    public void CancelDrift()
    {
        drift = false;
    }

    public void FireButtonDown()
    {
        isFiring = true;
        FireButtonDownEvent?.Invoke();
    }

    public void FireButtonUp()
    {
        isFiring = false;
        FireButtonUpEvent?.Invoke();
    }

    //For taking input from keyboard
    void TakeKeyboardInput()
    {       

        if(Input.GetKeyDown(KeyCode.W)) { AccButtonDown(); }
        if(Input.GetKeyDown(KeyCode.S)) { RevButtonDown(); }
        if(Input.GetKeyDown(KeyCode.A)) { TurnLeftButtonDown(); }
        if(Input.GetKeyDown(KeyCode.D)) { TurnRightButtonDown();  }

        if (Input.GetKeyUp(KeyCode.W)) { AccButtonUp(); }
        if (Input.GetKeyUp(KeyCode.S)) { RevButtonUp(); }
        if (Input.GetKeyUp(KeyCode.A)) { TurnLeftButtonUp(); }
        if (Input.GetKeyUp(KeyCode.D)) { TurnRightButtonUp(); }

        if (Input.GetKeyDown(KeyCode.Space)) { drift = true; }
        if (Input.GetKeyUp(KeyCode.Space)) { drift = false; }

        if (Input.GetKeyDown(KeyCode.K)) { FireButtonDown(); }
        if (Input.GetKeyUp(KeyCode.K)) { FireButtonUp(); }
    }
    //end
}

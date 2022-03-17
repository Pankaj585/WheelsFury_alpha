using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorButton : MonoBehaviour
{
    [SerializeField] AndroidController androidController;

    bool accButtonPressed;

    void Update()
    {
        androidController = GameObject.Find("Player").GetComponent<AndroidController>();

        if (androidController != null)
        {
            if (accButtonPressed) { androidController.Accelerate(); }
            else 
            {
                androidController.NotAccelerating();
            }
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
}

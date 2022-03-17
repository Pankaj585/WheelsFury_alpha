using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseButton : MonoBehaviour
{
    [SerializeField] AndroidController androidController;

    bool revButtonPressed;

    void Update()
    {
        androidController = GameObject.Find("Player").GetComponent<AndroidController>();

        if (androidController != null)
        {
            if (revButtonPressed) { androidController.Reverse(); }
            else { androidController.NotReversing(); }
        }
    }


    public void RevButtonDown()
    {
        revButtonPressed = true;
    }
    public void RevButtonUp()
    {
        revButtonPressed = false;
    }
}

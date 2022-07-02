using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] cars;
    [SerializeField] private CarDisplay carDisplay;
    private int currentIndex;
    private void Awake()
    {
        carDisplay.DisplayCar((CarModel)cars[0]);
    }
    public void ChangeCar()
    {
        currentIndex += 1;
        if (currentIndex < 0) { currentIndex = cars.Length - 1; }
        else if (currentIndex > (cars.Length - 1)){ currentIndex = 0; }
        
        if (carDisplay != null) { carDisplay.DisplayCar((CarModel)cars[currentIndex]); }
    }
}

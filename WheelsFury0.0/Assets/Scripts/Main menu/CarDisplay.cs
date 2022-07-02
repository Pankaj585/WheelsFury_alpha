using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDisplay : MonoBehaviour
{
    [SerializeField] private string carName;
    [SerializeField] private GameObject carModel;

    [SerializeField] Transform carHolder;
    
    public void DisplayCar(CarModel car)
    {
        carName = car.carName;
        
        if (carHolder.childCount > 0) { Destroy(carHolder.GetChild(0).gameObject); }
        Instantiate(car.carModel, carHolder.position, carHolder.rotation, carHolder);
    }
}

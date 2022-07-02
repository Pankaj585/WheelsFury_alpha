using UnityEngine;

[CreateAssetMenu(fileName ="New Car", menuName ="Items/Car")]
public class CarModel : ScriptableObject
{
    public int carIndex;
    public string carName;
    public GameObject carModel;
    public int speed;
    public int acceleration;
    public int handling;
}

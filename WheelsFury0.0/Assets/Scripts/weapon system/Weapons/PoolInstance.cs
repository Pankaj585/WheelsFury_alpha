using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstance
{
    public int ID { get; private set; }
    public GameObject instance { get; private set; }

    public bool isFree;

    public PoolInstanceType type { get; private set; }
    public PoolInstance(int ID, GameObject instance, PoolInstanceType type)
    {
        this.ID = ID;
        this.instance = instance;
        this.type = type;
        isFree = true;
    }

    public enum PoolInstanceType
    {
        WeaponAmmo,
        Effect
    }
}

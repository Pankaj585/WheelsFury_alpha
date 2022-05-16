using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    [HideInInspector] public PlayerID userID;
    public virtual void Fire()
    {

    }

    public virtual void Impact()
    {

    } 

}

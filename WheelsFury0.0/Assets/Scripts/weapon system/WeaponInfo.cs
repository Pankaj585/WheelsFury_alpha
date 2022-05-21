using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Guns/New Gun")]
public class WeaponInfo : ItemInfo
{    
    public int maxAmmo = 3;
    public int damage = 10;
    public int fireRate = 1;
    public bool isContinuousFire;

    [Header("Weapon Ammo")]
    public bool hasAmmoPrefab;
    public GameObject ammoPrefab;
    public int requiredPoolSize = 5;

    [Header("Impact")]
    public GameObject impactEffectPrefab;
    public int requiredImpactEffectPoolSize = 5;
}

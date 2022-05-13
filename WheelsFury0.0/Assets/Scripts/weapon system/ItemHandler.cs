using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{    
    OrbSpawner orbSpawner;
    WeaponInfo weaponInfo;
    private void Awake()
    {
        orbSpawner = FindObjectOfType<OrbSpawner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryEquipItemFromOrb(int orbIndex)
    {
        Debug.Log("Trying to equip");
       orbSpawner.RequestWeapon(orbIndex, this);        
    }

    public void EquipItem(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
        Debug.Log("Equiped item   " + this.weaponInfo.itemName);
    }
}

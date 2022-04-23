using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrb : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] Transform shocker;
    [SerializeField] Transform missile;
    [SerializeField] Transform machineGun;
    [SerializeField] Transform mine;

    

    [SerializeField] WeaponOrbSpawner weaponOrbSpawner;
    [SerializeField] WeaponController weaponController;

    void Update()
    {
        GetRefrence();
        player = weaponOrbSpawner.player;
    }
    public void GetRefrence()
    {
        weaponOrbSpawner = gameObject.GetComponentInParent<WeaponOrbSpawner>();
        weaponController = FindObjectOfType<WeaponController>();
    }

    private bool checkWeaponPick()
    {
        if(missile == null) { return false; }

        if (missile.gameObject.activeSelf) { return true; }
        else if (machineGun.gameObject.activeSelf) { return true; }
        else if (shocker.gameObject.activeSelf) { return true; }
        else if (mine.gameObject.activeSelf) { return true; }

        else { return false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        pickedUp();
        Destroy(gameObject);
    }

    private void pickedUp()
    {
        int temp = UnityEngine.Random.Range(1, 5);
        if (weaponController.weaponPicked)
        {
            weaponController.UnequipWeapons();
            weaponController.equipWeapon(temp);
        }
        else
        {
            weaponController.equipWeapon(temp);
        }
    }
}

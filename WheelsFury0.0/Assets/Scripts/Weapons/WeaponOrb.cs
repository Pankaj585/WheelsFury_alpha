using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrb : MonoBehaviour
{
    public GameObject player;

    [SerializeField] Transform shocker;
    [SerializeField] Transform missile;
    [SerializeField] Transform machineGun;
    [SerializeField] Transform mine;

    GameObject missileAmmo;
    GameObject machineGunAmmo;
    GameObject shockerAmmo;
    GameObject mineAmmo;

    [SerializeField] bool weaponPicked;

    [SerializeField] WeaponOrbSpawner weaponOrbSpawner;

    void Update()
    {
        GetRefrence();
        if (player == null) { return; }
        weaponPicked = checkWeaponPick();
    }
    private void ReferenceWeapons()
    {
        shocker = player.transform.Find("Weapons").Find("Shocker");
        missile = player.transform.Find("Weapons").Find("Missile");
        machineGun = player.transform.Find("Weapons").Find("MachineGun");
        mine = player.transform.Find("Weapons").Find("Mine");
    }

    public void GetRefrence()
    {
        weaponOrbSpawner = gameObject.GetComponentInParent<WeaponOrbSpawner>();
 
        ReferenceWeapons();

        missileAmmo = weaponOrbSpawner.GetMissileAmmo();
        machineGunAmmo = weaponOrbSpawner.GetMachineGunAmmo();
        shockerAmmo = weaponOrbSpawner.GetShockerAmmo();
        mineAmmo = weaponOrbSpawner.GetMineAmmo();
    }

    private bool checkWeaponPick()
    {
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
        if (weaponPicked == true)
        {
            if (missile.gameObject.activeSelf)
            {
                missile.GetComponent<RocketLauncher>().clipSize = 5;
                missile.gameObject.SetActive(false); missileAmmo.SetActive(false);
            }
            else if (machineGun.gameObject.activeSelf)
            {
                machineGun.GetComponent<MachineGun>().clipSize = 300;
                machineGun.gameObject.SetActive(false); machineGunAmmo.SetActive(false);
            }
            else if (shocker.gameObject.activeSelf)
            {
                shocker.GetComponent<Shocker>().clipSize = 6;
                shocker.gameObject.SetActive(false); shockerAmmo.SetActive(false);
            }
            else if (mine.gameObject.activeSelf)
            {
                mine.GetComponent<Mine>().clipSize = 4;
                mine.gameObject.SetActive(false); mineAmmo.SetActive(false);
            }

            if (temp == 1)
            {
                missile.gameObject.SetActive(true);
                missileAmmo.SetActive(true);
            }
            else if (temp == 2)
            {
                machineGun.gameObject.SetActive(true);
                machineGunAmmo.SetActive(true);
            }
            else if (temp == 3)
            {
                shocker.gameObject.SetActive(true);
                shockerAmmo.SetActive(true);
            }
            else if (temp == 4)
            {
                mine.gameObject.SetActive(true);
                mineAmmo.SetActive(true);
            }
        }
        else
        {
            if (temp == 1)
            {
                missile.gameObject.SetActive(true);
                missileAmmo.SetActive(true);
                missile.GetComponent<RocketLauncher>().clipSize = 5;
            }
            else if (temp == 2)
            {
                machineGun.gameObject.SetActive(true);
                machineGunAmmo.SetActive(true);
                machineGun.GetComponent<MachineGun>().clipSize = 300;
            }
            else if (temp == 3)
            {
                shocker.GetComponent<Shocker>().clipSize = 6;
                shocker.gameObject.SetActive(true);
                shockerAmmo.SetActive(true);
            }
            else if (temp == 4)
            {
                mine.gameObject.SetActive(true);
                mineAmmo.SetActive(true);
                mine.GetComponent<Mine>().clipSize = 4;
            }
        }
    }
}

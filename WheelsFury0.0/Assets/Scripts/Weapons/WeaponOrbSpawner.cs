using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrbSpawner : MonoBehaviour
{
    public GameObject player;

    [SerializeField] GameObject weaponOrb;
    [SerializeField] float cooldownTime = 8f;


    [SerializeField] GameObject missileAmmo;
    [SerializeField] GameObject machineGunAmmo;
    [SerializeField] GameObject shockerAmmo;
    [SerializeField] GameObject mineAmmo;

    bool spawning;

    void Start()
    {
        player = FindObjectOfType<AndroidController>().gameObject;
        var weaponUp = Instantiate(weaponOrb, transform.position, Quaternion.identity);
        weaponUp.transform.parent = transform;
    }

    void Update()
    {
        CheckIfDestroyed();
    }

    private void CheckIfDestroyed()
    {
        if (transform.childCount <= 0 && spawning == false)
        {
            StartCoroutine(SpawnPowerUp());
            spawning = true;
        }
        else { return; }
    }
    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(cooldownTime);
        var weaponUp = Instantiate(weaponOrb, transform.position, Quaternion.identity);
        weaponUp.transform.parent = transform;
        spawning = false;
    }

    public GameObject GetMissileAmmo()
    {
        return missileAmmo;
    }
    public GameObject GetMachineGunAmmo()
    {
        return machineGunAmmo;
    }
    public GameObject GetShockerAmmo()
    {
        return shockerAmmo;
    }
    public GameObject GetMineAmmo()
    {
        return mineAmmo;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public bool rocketLauncher, machineGun, Shockwave, mine, weaponPicked;
    [SerializeField] GameObject rl, mg, sw, mi;
    public GameObject missileAmmo, machineGunAmmo, shockerAmmo, mineAmmo, equipedWeapon;

    private void Awake()
    {
        rocketLauncher = false;
        machineGun = false;
        Shockwave = false;
        mine = false;
        weaponPicked = false;

        rl = transform.GetChild(0).gameObject;
        mg = transform.GetChild(1).gameObject;
        sw = transform.GetChild(2).gameObject;
        mi = transform.GetChild(3).gameObject;

    }
    private void Start()
    {
        var ammoRefrence = FindObjectOfType<AmmoRefrence>();
        missileAmmo = ammoRefrence.missileAmmo;
        machineGunAmmo = ammoRefrence.machineGunAmmo;
        shockerAmmo = ammoRefrence.shockerAmmo;
        mineAmmo = ammoRefrence.mineAmmo;

        sw.GetComponent<Shocker>().ammoUI = shockerAmmo;
        sw.GetComponent<Shocker>().ammoText = shockerAmmo.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        rl.GetComponent<RocketLauncher>().ammoUI = missileAmmo;
        rl.GetComponent<RocketLauncher>().ammoText = missileAmmo.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        mg.GetComponent<MachineGun>().ammoUI = machineGunAmmo;
        mg.GetComponent<MachineGun>().ammoText = machineGunAmmo.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        mi.GetComponent<Mine>().ammoUI = mineAmmo;
        mi.GetComponent<Mine>().ammoText = mineAmmo.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (rocketLauncher || machineGun || Shockwave || mine) { weaponPicked = true; }
        else weaponPicked = false;
    }

    public void EqRocketLauncher(bool eq)
    {
        rocketLauncher = eq;
    }
    public void EqMachineGun(bool eq)
    {
        machineGun = eq;
    }
    public void EqShockwave(bool eq)
    {
        Shockwave = eq;
    }
    public void EqMine(bool eq)
    {
        mine = eq;
    }
    public void UnequipWeapons()
    {
        if (rl.gameObject.activeSelf)
        {
            rl.GetComponent<RocketLauncher>().clipSize = 5;
            rl.gameObject.SetActive(false); missileAmmo.SetActive(false);
            equipedWeapon = null;
        }
        else if (mg.gameObject.activeSelf)
        {
            mg.GetComponent<MachineGun>().clipSize = 300;
            mg.gameObject.SetActive(false); machineGunAmmo.SetActive(false);
            equipedWeapon = null;
        }
        else if (sw.gameObject.activeSelf)
        {
            sw.GetComponent<Shocker>().clipSize = 6;
            sw.gameObject.SetActive(false); shockerAmmo.SetActive(false);
            equipedWeapon = null;
        }
        else if (mi.gameObject.activeSelf)
        {
            mi.GetComponent<Mine>().clipSize = 4;
            mi.gameObject.SetActive(false); mineAmmo.SetActive(false);
            equipedWeapon = null;
        }
    }
    public void equipWeapon(int i)
    {
        if (i == 1)
        {
            rl.gameObject.SetActive(true);
            missileAmmo.SetActive(true);
            EqRocketLauncher(true);
            equipedWeapon = rl;
        }
        else if (i == 2)
        {
            mg.gameObject.SetActive(true);
            machineGunAmmo.SetActive(true);
            EqMachineGun(true);
            equipedWeapon = mg;
        }
        else if (i == 3)
        {
            sw.gameObject.SetActive(true);
            shockerAmmo.SetActive(true);
            EqShockwave(true);
            equipedWeapon = sw;
        }
        else if (i == 4)
        {
            mi.gameObject.SetActive(true);
            mineAmmo.SetActive(true);
            EqMine(true);
            equipedWeapon = mi;
        }
    }
    public void FireWeapon()
    {
        if(equipedWeapon == null) { return; }
        equipedWeapon.GetComponent<IWeaponfire>().Fire();
    }
}

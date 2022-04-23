using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachineGun : MonoBehaviour,IWeaponfire
{
    [SerializeField] GameObject bulletTracers;
    [SerializeField] GameObject muzzleFlash;

    public float clipSize = 100;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public GameObject ammoUI;

    WeaponController weaponController;

    bool shooting = false;

    [SerializeField] AudioSource gunSound;

    void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();

        clipSize = 300;
        ammoText.text = (Mathf.FloorToInt(clipSize - 200).ToString());
    }

    void FixedUpdate()
    {
        ammoText.text = (Mathf.FloorToInt(clipSize - 200).ToString());
        if ((clipSize - 200) <= 0)
        {
            weaponController.EqMachineGun(false);
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            shooting = false;
            clipSize = 300;
        }
        if (shooting)
        {
            clipSize -= Time.deltaTime * 10;
        }
    }

    public void Fire()
    {
        if (gameObject.activeInHierarchy)
        {
            shooting = true;
            var emmissionModule = bulletTracers.gameObject.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = true;
            muzzleFlash.gameObject.SetActive(true);
            gunSound.Play();
        }
    }
    public void stopFire()
    {
        if (gameObject.activeInHierarchy)
        {
            shooting = false;
            var emmissionModule = bulletTracers.gameObject.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = false;
            muzzleFlash.gameObject.SetActive(false);
            gunSound.Stop();
        }
    }
}

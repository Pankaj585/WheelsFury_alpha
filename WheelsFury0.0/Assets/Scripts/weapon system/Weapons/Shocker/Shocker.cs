using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shocker : Ammo
{
    /*[SerializeField] GameObject upperWave;
    [SerializeField] GameObject bottomWave;

    [SerializeField] float fireRate = 0.5f;
    public int clipSize = 6;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public GameObject ammoUI;

    WeaponController weaponController;

    bool canShoot = true;

    AudioSource weaponSound;

   *//* void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();


        clipSize = 6;
        ammoText.text = clipSize.ToString();
        weaponSound = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        ammoText.text = (clipSize - 1).ToString();
        if (clipSize <= 1)
        {
            weaponController.EqShockwave(false);
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }*//*

    public void Fire()
    {
        if (gameObject.activeInHierarchy && canShoot == true)
        {
            upperWave.gameObject.SetActive(true);
            bottomWave.gameObject.SetActive(true);
            clipSize--;
            canShoot = false;
            Invoke("Delay", 0.5f);
            StartCoroutine(WaitBeforeFire());

        }
    }
    public void stopFire()
    {
        if (gameObject.activeInHierarchy)
        {
            print("f");
        }
    }
    void Delay()
    {
        upperWave.gameObject.SetActive(false);
        bottomWave.gameObject.SetActive(false);
    }
    IEnumerator WaitBeforeFire()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }*/
}

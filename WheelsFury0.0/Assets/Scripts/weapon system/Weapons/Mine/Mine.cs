using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine :MonoBehaviour
{
    /*[SerializeField] GameObject minePrefab;

    [SerializeField] float fireRate = 0.2f;
    public int clipSize = 4;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public GameObject ammoUI;

    WeaponController weaponController;

    bool canShoot = true;

   *//* void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();

        clipSize = 4;
        ammoText.text = clipSize.ToString();
    }

    void Update()
    {
        ammoText.text = clipSize.ToString();
        if (clipSize <= 0)
        {
            weaponController.EqMine(false);
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }*//*
    public void ThrowMine()
    {
        Instantiate(minePrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Shoot()
    {
        ThrowMine();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void Fire()
    {
        if (gameObject.activeInHierarchy)
        {
            canShoot = false;
            clipSize--;
            StartCoroutine(Shoot());
        }
    }*/

}

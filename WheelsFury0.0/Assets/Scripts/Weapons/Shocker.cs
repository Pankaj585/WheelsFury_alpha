using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shocker : MonoBehaviour
{
    [SerializeField] GameObject upperWave;
    [SerializeField] GameObject bottomWave;

    [SerializeField] float fireRate = 0.5f;
    public int clipSize = 6;
    [SerializeField]  Text ammoText;
    [SerializeField] GameObject ammoUI;

    bool canShoot = true;

    AudioSource weaponSound;

    void Start()
    {
        ammoText = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        ammoUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

        clipSize = 6;
        ammoText.text = clipSize.ToString();
        weaponSound = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        ammoText.text = (clipSize - 1).ToString();
        if (clipSize <= 1)
        {
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void Fire()
    {
        if (gameObject.activeInHierarchy && canShoot == true)
        {
            upperWave.gameObject.SetActive(true);
            bottomWave.gameObject.SetActive(true);
            weaponSound.Play();
            clipSize--;
            canShoot = false;
            StartCoroutine(WaitBeforeFire());

        }
    }
    public void stopFire()
    {
        if (gameObject.activeInHierarchy)
        {
            Invoke("Delay", 0.5f);
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
    }
}

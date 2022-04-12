using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
    [SerializeField] GameObject bulletTracers;
    [SerializeField] GameObject muzzleFlash;

    public float clipSize = 100;
    [SerializeField] Text ammoText;
    [SerializeField] GameObject ammoUI;

    bool shooting = false;

    [SerializeField] AudioSource gunSound;

    void Start()
    {
        ammoText = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        ammoUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

        clipSize = 300;

        ammoText.text = (Mathf.FloorToInt(clipSize - 200).ToString());
    }

    void FixedUpdate()
    {
        ammoText.text = (Mathf.FloorToInt(clipSize - 200).ToString());
        if ((clipSize - 200) <= 0)
        {
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
            if (bulletTracers == null) return;
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
            if (bulletTracers == null) return;
            shooting = false;
            var emmissionModule = bulletTracers.gameObject.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = false;
            muzzleFlash.gameObject.SetActive(false);
            gunSound.Stop();
        }
    }
}

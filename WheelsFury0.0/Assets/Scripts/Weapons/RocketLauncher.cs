using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] float propulsionForce = 35f;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] Text ammoText;
    [SerializeField] GameObject ammoUI;
    
    public int clipSize = 5;

    bool canShoot = true;

    private Transform launcherTransform;

    void Start()
    {
        ammoText = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        ammoUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

        clipSize = 5;
        ammoText.text = clipSize.ToString();
        launcherTransform = transform;
    }

    void Update()
    {
        ammoText.text = clipSize.ToString();
        if(clipSize <= 0) 
        {
            clipSize = 5;
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false); 
        }
    }

    public void Fire()
    {
        if (gameObject.activeInHierarchy)
        {
            canShoot = false;
            clipSize--;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        GunFire();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void GunFire()
    {
        GameObject rocket = Instantiate(rocketPrefab, launcherTransform.transform.TransformPoint(0f, 0f, 0f), Quaternion.Euler(90f, 0f, 0f));
        rocket.GetComponent<Rigidbody>().AddForce(launcherTransform.forward * propulsionForce, ForceMode.Impulse);
        Destroy(rocket, 5f);
    }
}

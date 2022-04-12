using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject minePrefab;

    [SerializeField] float fireRate = 0.2f;
    public int clipSize = 4;
    [SerializeField] Text ammoText;
    [SerializeField] GameObject ammoUI;
    bool canShoot = true;

    void Start()
    {
        ammoText = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        ammoUI = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

        clipSize = 4;
        ammoText.text = clipSize.ToString();
    }

    void Update()
    {
        ammoText.text = clipSize.ToString();
        if (clipSize <= 0)
        {
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
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

    public void SpawnMine()
    {
        if (gameObject.activeInHierarchy)
        {
            canShoot = false;
            clipSize--;
            StartCoroutine(Shoot());
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class RocketLauncher : WeaponLauncher
{
    [SerializeField] Transform launchTransform;
    PhotonView pv;
    GameHandler gameHandler;
    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        pool = FindObjectOfType<WeaponPool>();
        pv = GetComponent<PhotonView>();
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public override void OnFireButtonDown()
    {
        pv.RPC("LaunchRocket", RpcTarget.All);
    }    

    public override void OnFireButtonUp()
    {
        //do nothing
    }

    [PunRPC]
    void LaunchRocket()
    {
        GameObject rocket = pool.GetMissile();
        rocket.SetActive(true);
        rocket.GetComponent<Rocket>().Launch(launchTransform);

        if (pv.IsMine)
        {
            gameHandler.UpdateAmmoUI(--itemHandler.currentAmmo);
        }

        if (itemHandler.currentAmmo <= 0)
            itemHandler.UnequipWeapon();
    }

    
    /*[SerializeField] GameObject rocketPrefab;
    [SerializeField] float propulsionForce = 35f;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public GameObject ammoUI;

    WeaponController weaponController;

    public int clipSize = 5;

    bool canShoot = true;

    private Transform launcherTransform;

    void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();

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
            weaponController.EqRocketLauncher(false);
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
    }*/
}

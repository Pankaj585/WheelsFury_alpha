using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class MachineGun : WeaponLauncher
{
    [SerializeField] Transform launchPoint;
    [SerializeField] MachineGunImpactEffect impactEffect;
    [SerializeField] LayerMask layerMask;
    GameHandler gameHandler;
    bool isFiring;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public override void OnFireButtonDown()
    {
        StartFiring();
    }

    public override void OnFireButtonUp()
    {
        StopFiring();
    }

    IEnumerator FiringRoutine()
    {
        while (isFiring)
        {
            pv.RPC("Fire", RpcTarget.All);
            yield return new WaitForSeconds(1 / weaponInfo.fireRate);
        }
    }

    void StartFiring()
    {
        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(FiringRoutine());
        }        
    }

    void StopFiring()
    {
        if (isFiring)
        {
            isFiring = false;
        }
    }

    [PunRPC]
    void Fire()
    {
        if(Physics.Raycast(launchPoint.position, launchPoint.forward, out RaycastHit hitInfo, layerMask))
        {
            PoolInstance instance = poolManager.GetInstance(impactEffect);
            instance.instance.transform.position = hitInfo.point;
            MachineGunImpactEffect effect = instance.instance.GetComponent<MachineGunImpactEffect>();
            effect.SetPoolInstanceReference(instance);
            effect.gameObject.SetActive(true);
            effect.PlayEffect();

            itemHandler.currentAmmo--;

            if (pv.IsMine)
            {
                gameHandler.UpdateAmmoUI(itemHandler.currentAmmo);

            }

            if (itemHandler.currentAmmo <= 0)
            {
                itemHandler.UnequipWeapon();
                isFiring = false;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                GameObject player = hitInfo.collider.GetComponent<PlayerReference>()?.playerRoot;
                if (player == null)
                    return;

                player.GetComponent<Status>().Damage(weaponInfo.damage/weaponInfo.fireRate);
            }

            
                
        }
    }

    
    /* [SerializeField] GameObject bulletTracers;
     [SerializeField] GameObject muzzleFlash;

     public float clipSize = 100;
     [SerializeField] public TextMeshProUGUI ammoText;
     [SerializeField] public GameObject ammoUI;

     WeaponController weaponController;

     bool shooting = false;

     [SerializeField] AudioSource gunSound;

    *//* void Start()
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
     }*//*

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
     }*/
}

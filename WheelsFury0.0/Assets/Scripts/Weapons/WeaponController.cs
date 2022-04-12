using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController :MonoBehaviour
{
    public float fireRate = 0.1f;
    public int clipSize = 100;

    bool canShoot;
    int _currentAmmoInClip;

    [SerializeField] ParticleSystem tracerParticles;
    
    void Start()
    {
        _currentAmmoInClip = clipSize;
        canShoot = true;
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot && _currentAmmoInClip > 0)
        {
            canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(Shoot());
        }
        if(Input.GetMouseButton(0) && _currentAmmoInClip > 0)
        {
            BulletTracers(true);
        }
        else
        {
            BulletTracers(false);
        }
    }

    private void BulletTracers(bool isActive)
    {
        var emmisionModule = tracerParticles.GetComponent<ParticleSystem>().emission;
        emmisionModule.enabled = isActive;
    }

    IEnumerator Shoot()
    {
        RaycastForEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    void RaycastForEnemy()
    {
        RaycastHit hit;
        print("shoot");
        if(Physics.Raycast(transform.parent.position,transform.parent.forward,out hit))
        {
            if(hit.transform.tag == "Enemy")
            {
                CmdDealDamage(hit.transform.gameObject);
            }
        }
    }

    void CmdDealDamage(GameObject obj)
    {
        
    }
}

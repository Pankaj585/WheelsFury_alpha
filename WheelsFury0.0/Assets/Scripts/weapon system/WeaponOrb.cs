using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrb : MonoBehaviour
{
    [SerializeField] GameObject[] weaponGFXs;
    [HideInInspector] public WeaponInfo weaponInfo;
    [SerializeField] int orbIndex;
    SphereCollider sphereCollider;
    public bool isOrbAvailable { get; private set; }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    public void SetWeapon(WeaponInfo weaponInfo)
    {
        if(this.weaponInfo)
            weaponGFXs[this.weaponInfo.itemIndex].SetActive(false);
        this.weaponInfo = weaponInfo;
        weaponGFXs[this.weaponInfo.itemIndex].SetActive(true);
        isOrbAvailable = true;
        sphereCollider.enabled = true;
    }

    public void Disable()
    {
        weaponGFXs[weaponInfo.itemIndex].SetActive(false);
        sphereCollider.enabled = false;
        weaponInfo = null;
        isOrbAvailable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.GetComponent<ItemHandler>()?.TryEquipItemFromOrb(orbIndex);
        Debug.Log("triggered");
    }
}

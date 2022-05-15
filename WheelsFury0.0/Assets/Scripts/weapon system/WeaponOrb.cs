using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrb : MonoBehaviour
{
    [SerializeField] GameObject[] weaponGFXs;
    [HideInInspector] public WeaponInfo weaponInfo;
    [SerializeField] int orbIndex;
    SphereCollider sphereCollider;

    [SerializeField] float spawnWaitTime = 3.0f;
    public bool IsOrbAvailable { get; private set; }

    public delegate void TimeUp(int orbIndex);
    public event TimeUp Ready;
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
        IsOrbAvailable = true;
        sphereCollider.enabled = true;
    }

    public void Disable()
    {
        weaponGFXs[weaponInfo.itemIndex].SetActive(false);
        sphereCollider.enabled = false;
        weaponInfo = null;
        IsOrbAvailable = false;

        if (!PhotonNetwork.IsMasterClient)
            return;

        StopAllCoroutines();
        StartCoroutine(Wait());
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.GetComponent<PlayerReference>()?.playerRoot.GetComponent<ItemHandler>()?.TryEquipItemFromOrb(orbIndex);
        Debug.Log("triggered");
    }

    IEnumerator Wait()
    {        
        yield return new WaitForSeconds(spawnWaitTime);
        Ready?.Invoke(this.orbIndex);
    }
}

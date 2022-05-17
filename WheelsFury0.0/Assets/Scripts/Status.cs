using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Status : MonoBehaviour
{

    const int maxHealth = 100;
    int currentHealth;
    bool isDead;
    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }

        pv.RPC("SyncHealth", RpcTarget.Others, currentHealth);
    }

    [PunRPC]
    void SyncHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        if (this.currentHealth == 0)
            isDead = true;
    }
}

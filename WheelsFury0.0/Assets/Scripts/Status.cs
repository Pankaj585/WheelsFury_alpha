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
    GameHandler gameHandler;
    PlayerID playerId;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        gameHandler = FindObjectOfType<GameHandler>();
        playerId = GetComponent<PlayerID>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        gameHandler.UpdateHealthBar(1);
    }

    public void Damage(int damage)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (isDead)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            gameHandler.Respawn(playerId);
        }

        if(pv.IsMine)
            gameHandler.UpdateHealthBar((float)currentHealth / maxHealth);

        pv.RPC("SyncHealth", RpcTarget.Others, currentHealth);

        
    }

    [PunRPC]
    void SyncHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        if (this.currentHealth == 0)
            isDead = true;
    }

    public void ResetHealth()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        currentHealth = maxHealth;
        isDead = false;

        if(pv.IsMine)
            gameHandler.UpdateHealthBar(1);

        pv.RPC("SyncHealth", RpcTarget.Others, currentHealth);
    }
}

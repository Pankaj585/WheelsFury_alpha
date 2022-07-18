using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Status : MonoBehaviour
{

    const int maxHealth = 100;
    int currentHealth;
    bool isDead;
    bool isInvincible;
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
        if (isDead || isInvincible)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            gameHandler.Respawn(playerId);
        }


        pv.RPC("SyncHealth", RpcTarget.All, currentHealth);

        
    }

    [PunRPC]
    void SyncHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
        if (this.currentHealth == 0)
            isDead = true;

        if (pv.IsMine)
            gameHandler.UpdateHealthBar((float)currentHealth / maxHealth);
    }

    public void ResetHealth()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        currentHealth = maxHealth;
        isDead = false;

        pv.RPC("MakeInvincible", RpcTarget.Others);
    }

    [PunRPC]
    void MakeInvincible()
    {
        currentHealth = maxHealth;
        isInvincible = true;      
        StartCoroutine(InvincibilityCountdown(2f));  
    }

    IEnumerator InvincibilityCountdown(float seconds){

        yield return new WaitForSeconds(seconds);
        isInvincible = false;
    }
}

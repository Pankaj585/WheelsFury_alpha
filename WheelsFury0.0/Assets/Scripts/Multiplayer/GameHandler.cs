using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameHandler : MonoBehaviourPunCallbacks
{
    GameUIHandler UIHandler;
    [SerializeField] Transform[] spawnPoints;
    byte currentCanvas = 0;
    private void Awake()
    {
        UIHandler = FindObjectOfType<GameUIHandler>();
    }

    public void LeaveRoom()
    {
        Debug.Log("Leaving room...");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentCanvas == 0)
            {
                currentCanvas = 1;
                UIHandler.ShowOptions();
            } else if(currentCanvas == 1)
            {
                currentCanvas = 0;
                UIHandler.ShowPlayerOverlay();
            }
        }
    }

    public void SetWeapon(WeaponInfo info)
    {
        UIHandler.SetWeaponUI(info);
    }

    public void UpdateAmmoUI(int ammo)
    {
        UIHandler.UpdateAmmoUI(ammo);
    }

    public void UpdateHealthBar(float healthPercent)
    {
        UIHandler.UpdateHealthBar(healthPercent);
    }

    public void Respawn(PlayerID playerID)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        Status playerStatus = playerID.GetComponent<Status>();
        ItemHandler playerItemHandler = playerID.GetComponent<ItemHandler>();

        playerItemHandler.UnequipWeaponOverNetwork();
        playerStatus.ResetHealth();

        int randomIndex = Random.Range(0, spawnPoints.Length);
        playerID.GetComponent<AndroidController>().Respawn(spawnPoints[randomIndex].position);
    }
}

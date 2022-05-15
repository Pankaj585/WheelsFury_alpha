using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject playerOverlay;
    [SerializeField] GameObject optionsMenu;

    [Header("Equipped Weapon Display")]
    [SerializeField] GameObject equippedWeaponOverlay;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject rocketLauncherImage;
    [SerializeField] GameObject machineGunImage;
    [SerializeField] GameObject mineImage;
    [SerializeField] GameObject shockerImage;

    GameHandler gameHandler;
    // Start is called before the first frame update
    void Awake()
    {
        playerOverlay.SetActive(true);
        optionsMenu.SetActive(false);
        equippedWeaponOverlay.SetActive(false);
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public void ShowOptions()
    {
        playerOverlay.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ShowPlayerOverlay()
    {
        playerOverlay.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OnClick_LeaveRoom()
    {
        Debug.Log("Leaving Room...");
        gameHandler.LeaveRoom();
    }

    public void SetWeaponUI(WeaponInfo info)
    {
        if (!equippedWeaponOverlay.activeSelf)
            equippedWeaponOverlay.SetActive(true);

        rocketLauncherImage.SetActive(false);
        machineGunImage.SetActive(false);
        mineImage.SetActive(false);
        shockerImage.SetActive(false);

        ammoText.text = info.maxAmmo.ToString();

        switch (info.itemIndex)
        {
            case 0: rocketLauncherImage.SetActive(true);
                break;
            case 1: machineGunImage.SetActive(true);
                break;
            case 2: mineImage.SetActive(true);
                break;
            case 3: shockerImage.SetActive(true);
                break;
            default: break;
        }
    }

    public void UpdateAmmoUI(int ammo)
    {
        if(ammo == 0)
        {
            equippedWeaponOverlay.SetActive(false);
            return;
        }

        ammoText.text = ammo.ToString();
    }
}

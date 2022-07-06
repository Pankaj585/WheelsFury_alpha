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
    [SerializeField] Canvas equippedWeaponOverlayCanvas;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image rocketLauncherImage;
    [SerializeField] Image machineGunImage;
    [SerializeField] Image mineImage;
    [SerializeField] Image shockerImage;

    [Header("Health Display")]
    [SerializeField] RectTransform healthFill;

    GameHandler gameHandler;
    Dictionary<int, Image> weaponDisplayImages = new Dictionary<int, Image>();
    Image currentActiveWeaponDisplayImage;
    // Start is called before the first frame update
    void Awake()
    {
        playerOverlay.SetActive(true);
        gameHandler = FindObjectOfType<GameHandler>();
        weaponDisplayImages.Add(0, rocketLauncherImage);
        weaponDisplayImages.Add(1, machineGunImage);
        weaponDisplayImages.Add(2, mineImage);
        weaponDisplayImages.Add(3, shockerImage);
    }

    private void Start()
    {
        optionsMenu.SetActive(false);
        equippedWeaponOverlayCanvas.enabled = false;
        for(int i = 0; i < weaponDisplayImages.Count; i++)
        {
            weaponDisplayImages[i].enabled = false;
        }
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
        if(info == null)
        {
            equippedWeaponOverlayCanvas.enabled = false;
            return;
        }

        if (!equippedWeaponOverlayCanvas.enabled)
            equippedWeaponOverlayCanvas.enabled = true;

        if(currentActiveWeaponDisplayImage!=null)
            currentActiveWeaponDisplayImage.enabled = false;

       /* rocketLauncherImage.SetActive(false);
        machineGunImage.SetActive(false);
        mineImage.SetActive(false);
        shockerImage.SetActive(false);*/

        ammoText.text = info.maxAmmo.ToString();

        currentActiveWeaponDisplayImage = weaponDisplayImages[info.itemIndex];
        currentActiveWeaponDisplayImage.enabled = true;

        /*switch (info.itemIndex)
        {
            case 0: rocketLauncherImage.SetActive(true);
                currentActiveWeaponDisplayImage = rocketLauncherImage;
                break;
            case 1: machineGunImage.SetActive(true);
                currentActiveWeaponDisplayImage = machineGunImage;
                break;
            case 2: mineImage.SetActive(true);
                currentActiveWeaponDisplayImage = mineImage;
                break;
            case 3: shockerImage.SetActive(true);
                currentActiveWeaponDisplayImage = shockerImage;
                break;
            default: break;
        }*/
    }

    public void UpdateAmmoUI(int ammo)
    {
        if(ammo == 0)
        {
            equippedWeaponOverlayCanvas.enabled = false;
            return;
        }

        ammoText.text = ammo.ToString();
    }

    public void UpdateHealthBar(float healthPercent)
    {
        if (healthPercent < 0 || healthPercent > 100)
            return;

        healthFill.localScale = new Vector3(healthPercent, 1, 1);
    }
}

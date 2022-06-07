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
    Dictionary<int, GameObject> weaponDisplayImages = new Dictionary<int, GameObject>();
    GameObject currentActiveWeaponDisplayImage;
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
        equippedWeaponOverlay.SetActive(false);
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
            equippedWeaponOverlay.SetActive(false);
            return;
        }

        if (!equippedWeaponOverlay.activeSelf)
            equippedWeaponOverlay.SetActive(true);

        currentActiveWeaponDisplayImage?.SetActive(false);

       /* rocketLauncherImage.SetActive(false);
        machineGunImage.SetActive(false);
        mineImage.SetActive(false);
        shockerImage.SetActive(false);*/

        ammoText.text = info.maxAmmo.ToString();

        currentActiveWeaponDisplayImage = weaponDisplayImages[info.itemIndex];
        currentActiveWeaponDisplayImage.SetActive(true);

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
            equippedWeaponOverlay.SetActive(false);
            return;
        }

        ammoText.text = ammo.ToString();
    }
}

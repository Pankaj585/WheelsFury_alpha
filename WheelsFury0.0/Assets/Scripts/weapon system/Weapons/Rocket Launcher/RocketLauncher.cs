using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class RocketLauncher : WeaponLauncher
{
    [SerializeField] Transform launchTransform;
    GameHandler gameHandler;
    Transform targetEnemy;
    Camera myCam;
    PlayerID myID;

    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        poolManager = FindObjectOfType<PoolManager>();
        pv = GetComponent<PhotonView>();
        gameHandler = FindObjectOfType<GameHandler>();
        myCam = transform.root.GetComponentInChildren<Camera>(true);
        myID = transform.root.GetComponent<PlayerID>();
    }

    public override void OnFireButtonDown()
    {
        pv.RPC("LaunchRocket", RpcTarget.All);
    }    

    public override void OnFireButtonUp()
    {
        //do nothing
    }

    [PunRPC]
    void LaunchRocket()
    {
        PoolInstance instance = poolManager.GetInstance(weaponInfo);
        instance.instance.SetActive(true);
        Rocket rocket = instance.instance.GetComponent<Rocket>();

        PlayerID[] ids = FindObjectsOfType<PlayerID>();
        targetEnemy = null;
        foreach (PlayerID id in ids)
        {
            if (id == myID)
                continue;

            Vector3 vPoint = myCam.WorldToViewportPoint(id.transform.position);
            if (vPoint.x > 0 && vPoint.x < 1 && vPoint.y > 0 && vPoint.y < 1 && vPoint.z > 0 && vPoint.z < 120)
            {
                targetEnemy = id.transform;
                break;
            }
        }

        rocket.SetPoolInstanceReference(instance);
        if(targetEnemy == null) { print("nul"); };
        rocket.Launch(launchTransform, targetEnemy);

        itemHandler.currentAmmo--;

        if (pv.IsMine)
        {
            gameHandler.UpdateAmmoUI(itemHandler.currentAmmo);

        }

        if (itemHandler.currentAmmo <= 0)
            itemHandler.UnequipWeapon();
    }
    private Transform FindTarget()
    {
        Transform target;
        var planes = GeometryUtility.CalculateFrustumPlanes(myCam);
        List<Transform> enemiesOnScreen = new List<Transform>();
        PlayerID[] ids = FindObjectsOfType<PlayerID>();
        foreach (var plane in planes)
        {
            foreach (var id in ids)
            {
                if (id == myID) continue;

                if (plane.GetDistanceToPoint(id.transform.position) < 0) { enemiesOnScreen.Add(id.transform); }
            }
        }
        if (enemiesOnScreen != null)
        {
            target = FindClosestTarget(enemiesOnScreen);
            return target;
        }
        else return null;
    }
    private Transform FindClosestTarget(List<Transform> transforms)
    {
        Transform closestTarget = null;
        if (transforms == null)
            return null;
        else
        {
            float closestDistance = Mathf.Infinity;
            foreach (Transform t in transforms)
            {
                float currentDistance = Vector3.Distance(myID.transform.position, t.transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestTarget = t.transform;
                }
            }
            return closestTarget;
        }
    }

    /*[SerializeField] GameObject rocketPrefab;
    [SerializeField] float propulsionForce = 35f;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public GameObject ammoUI;

    WeaponController weaponController;

    public int clipSize = 5;

    bool canShoot = true;

    private Transform launcherTransform;

    void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();

        clipSize = 5;
        ammoText.text = clipSize.ToString();
        launcherTransform = transform;
    }

    void Update()
    {
        ammoText.text = clipSize.ToString();
        if(clipSize <= 0) 
        {
            clipSize = 5;
            weaponController.EqRocketLauncher(false);
            ammoUI.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false); 
        }
    }

    public void Fire()
    {
        if (gameObject.activeInHierarchy)
        {
            canShoot = false;
            clipSize--;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        GunFire();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void GunFire()
    {
        GameObject rocket = Instantiate(rocketPrefab, launcherTransform.transform.TransformPoint(0f, 0f, 0f), Quaternion.Euler(90f, 0f, 0f));
        rocket.GetComponent<Rigidbody>().AddForce(launcherTransform.forward * propulsionForce, ForceMode.Impulse);
        Destroy(rocket, 5f);
    }*/
}

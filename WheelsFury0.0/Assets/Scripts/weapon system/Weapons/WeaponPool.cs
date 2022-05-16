using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [Header("Pool Parents")]
    [SerializeField] Transform missilePoolParent;
    [SerializeField] Transform machineGunPoolParent;
    [SerializeField] Transform shockerPoolParent;
    [SerializeField] Transform minePoolParent;

    [Header("Pool Sizes")]
    [SerializeField] int missilePoolSize;
    [SerializeField] int machineGunPoolSize;
    [SerializeField] int shockerPoolSize;
    [SerializeField] int minePoolSize;

    [Header("Pool Prefabs")]
    [SerializeField] GameObject missile;
    [SerializeField] GameObject machineGun;
    [SerializeField] GameObject shocker;
    [SerializeField] GameObject mine;

    Pool missilePool;
    Pool machineGunPool;
    Pool shockerPool;
    Pool minePool;
    // Start is called before the first frame update
    void Start()
    {
        InitPools();
    }

    public GameObject GetMissile()
    {
        return missilePool.GetInstance();
    }

    public GameObject GetMine()
    {
        return minePool.GetInstance();
    }

    public GameObject GetMachineGun()
    {
        return machineGunPool.GetInstance();
    }

    public GameObject GetShocker()
    {
        return shockerPool.GetInstance();
    }

    public void ReturnMissile(GameObject instance)
    {
        missilePool.ReturnInstance(instance);
    }

    public void ReturnMine(GameObject instance)
    {
        minePool.ReturnInstance(instance);
    }

    public void ReturnMachineGune(GameObject machineGun)
    {
        machineGunPool.ReturnInstance(machineGun);
    }

    public void ReturnShocker(GameObject shocker)
    {
        shockerPool.ReturnInstance(shocker);
    }

    void InitPools()
    {
        List<GameObject> missiles = new List<GameObject>();
        List<GameObject> mines = new List<GameObject>();
        List<GameObject> machineGuns = new List<GameObject>();
        List<GameObject> shockers = new List<GameObject>();

        int loopSize = missilePoolSize > machineGunPoolSize ? missilePoolSize : machineGunPoolSize;
        loopSize = loopSize > shockerPoolSize ? loopSize : shockerPoolSize;
        loopSize = loopSize > minePoolSize ? loopSize : minePoolSize;

        for (int i = 0; i < loopSize; i++)
        {
            if (i < missilePoolSize)
            {
                GameObject missile = Instantiate(this.missile, missilePoolParent);
                missile.SetActive(false);
                missile.transform.position = Vector3.zero;
                missiles.Add(missile);
            }

            if (i < machineGunPoolSize)
            {
                GameObject machineGun = Instantiate(this.machineGun, machineGunPoolParent);
                machineGun.SetActive(false);
                machineGun.transform.position = Vector3.zero;
                machineGuns.Add(machineGun);
            }

            if (i < shockerPoolSize)
            {
                GameObject shocker = Instantiate(this.shocker, shockerPoolParent);
                shocker.SetActive(false);
                shocker.transform.position = Vector3.zero;
                shockers.Add(shocker);
            }

            if (i < minePoolSize)
            {
                GameObject mine = Instantiate(this.mine, minePoolParent);
                mine.SetActive(false);
                mine.transform.position = Vector3.zero;
                mines.Add(mine);
            }
        }

        missilePool = new Pool(missiles);
        minePool = new Pool(mines);
        machineGunPool = new Pool(machineGuns);
        shockerPool = new Pool(shockers);
    }
    private class Pool
    {
        public List<GameObject> currentlyUnusedInstances;
        public List<GameObject> currentlyUsedInstances;

        public Pool(List<GameObject> allInstances)
        {
            currentlyUnusedInstances = new List<GameObject>(allInstances);
            currentlyUsedInstances = new List<GameObject>();
        }

        public GameObject GetInstance()
        {
            if (currentlyUnusedInstances.Count == 0)
                return null;

            GameObject instance = currentlyUnusedInstances[0];
            currentlyUsedInstances.Add(instance);
            currentlyUnusedInstances.RemoveAt(0);
            return instance;
        }

        public void ReturnInstance(GameObject instance)
        {
            int index = currentlyUsedInstances.BinarySearch(instance);
            if (index < 0)
                return;

            GameObject returnedInstance = currentlyUsedInstances[index];
            currentlyUnusedInstances.Add(returnedInstance);
            currentlyUsedInstances.Remove(returnedInstance);
        }
    }
}

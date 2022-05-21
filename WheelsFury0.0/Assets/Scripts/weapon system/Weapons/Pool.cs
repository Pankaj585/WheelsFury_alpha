using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public int poolID;
    public Transform poolParent;
    PoolInstance[] instances;
    public PoolInstance.PoolInstanceType type { get; private set; }
    public Pool(WeaponInfo info, Transform weaponPool, PoolInstance.PoolInstanceType type)
    {
        poolID = info.itemIndex;
        GameObject poolParent = new GameObject(info.itemName);
        poolParent.transform.parent = weaponPool;
        poolParent.transform.localPosition = Vector3.zero;
        this.type = type;
        instances = new PoolInstance[info.requiredPoolSize];
        for (int i = 0; i < info.requiredPoolSize; i++)
        {
            GameObject instance = Object.Instantiate(info.ammoPrefab, poolParent.transform);
            instance.SetActive(false);
            instance.transform.localPosition = Vector3.zero;
            instances[i] = new PoolInstance(i, instance, type);
        }
        
    }

    public Pool(int poolID, Transform poolParent, int poolSize, GameObject prefab, PoolInstance.PoolInstanceType type)
    {
        this.poolID = poolID;
        this.poolParent = poolParent;
        this.type = type;
        instances = new PoolInstance[poolSize];
        for(int i = 0; i < poolSize; i++)
        {
            GameObject instance = Object.Instantiate(prefab, poolParent);
            instance.SetActive(false);
            instance.transform.localPosition = Vector3.zero;
            instances[i] = new PoolInstance(i, instance, type);
        }
    }

    public Pool(Effect effect, Transform poolParent, PoolInstance.PoolInstanceType type)
    {
        poolID = effect.effectID;
        this.poolParent = poolParent;
        this.type = type;
        instances = new PoolInstance[effect.requiredPoolSize];
        for (int i = 0; i < effect.requiredPoolSize; i++)
        {
            GameObject instance = Object.Instantiate(effect.prefab, poolParent.transform);
            instance.SetActive(false);
            instance.transform.localPosition = Vector3.zero;
            instances[i] = new PoolInstance(i, instance, type);
        }
    }

    public PoolInstance GetInstance()
    {
        foreach (PoolInstance instance in instances)
        {
            if (instance.isFree)
            {
                instance.isFree = false;
                return instance;
            }
        }

        return null;
    }

    public void ReturnInstance(PoolInstance poolInstance)
    {
        if (poolInstance.type != type)
            return;

        foreach (PoolInstance instance in instances)
        {
            if (instance.ID == poolInstance.ID)
            {
                poolInstance.instance.SetActive(false);
                poolInstance.instance.transform.localPosition = Vector3.zero;
                instance.isFree = true;
                break;
            }
        }
    }

    public PoolInstance GetInstanceByID(int ID)
    {
        foreach (PoolInstance instance in instances)
        {
            if (instance.ID == ID)
            {
                if (!instance.isFree)
                {
                    Debug.Log("Error");
                    return null;
                }

                instance.isFree = false;
                return instance;
            }
        }

        return null;
    }

    public void ReturnInstanceByID(int ID)
    {
        foreach(PoolInstance instance in instances)
        {
            if(instance.ID == ID)
            {
                instance.instance.SetActive(false);
                instance.instance.transform.localPosition = Vector3.zero;
                instance.isFree = true;
                break;
            }
        }
    }

    public int GetFreeInstanceID()
    {
        foreach(PoolInstance instance in instances)
        {
            if (instance.isFree)
                return instance.ID;
        }

        return -1;
    }

    public void SetInstance(int instanceID, bool isFree, Vector3 pos, Quaternion rot)
    {
        foreach (PoolInstance instance in instances)
        {
            if (instance.ID == instanceID)
            {
                instance.instance.SetActive(!isFree);
                instance.isFree = isFree;
                if (!isFree)
                    instance.instance.transform.SetPositionAndRotation(pos, rot);
                else
                    instance.instance.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }
}

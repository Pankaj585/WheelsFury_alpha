using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PoolManager : MonoBehaviour
{
    
    [SerializeField] WeaponInfo[] weaponInfo;
    [SerializeField] Effect[] effects;
    Pool[] weaponPools;
    Pool[] effectPools;
    PhotonView pv;
    // Start is called before the first frame update
    void Awake()
    {
        pv = GetComponent<PhotonView>();

        weaponPools = new Pool[weaponInfo.Length];

        for(int i = 0; i < weaponInfo.Length; i++)
        {
            int poolID = weaponInfo[i].itemIndex;
            int poolSize = weaponInfo[i].requiredPoolSize;
            GameObject prefab = weaponInfo[i].ammoPrefab;


            GameObject poolParent = new GameObject(weaponInfo[i].itemName);
            poolParent.transform.localPosition = Vector3.zero;
            poolParent.transform.parent = transform;

            weaponPools[i] = new Pool(poolID, poolParent.transform, poolSize, prefab, PoolInstance.PoolInstanceType.WeaponAmmo);
        }

        effectPools = new Pool[effects.Length];

        for(int i = 0; i < effects.Length; i++)
        {
            int poolID = effects[i].effectID;
            int poolSize = effects[i].requiredPoolSize;
            GameObject prefab = effects[i].prefab;

            GameObject poolParent = new GameObject(effects[i].effectName);
            poolParent.transform.localPosition = Vector3.zero;
            poolParent.transform.parent = transform;

            effectPools[i] = new Pool(poolID, poolParent.transform, poolSize, prefab, PoolInstance.PoolInstanceType.Effect);
        }
    }    

    public PoolInstance GetInstance(WeaponInfo info)
    {
        foreach(Pool pool in weaponPools)
        {
            if(pool.poolID == info.itemIndex)
            {
                return pool.GetInstance();
            }
        }

        return null;
    }
    public PoolInstance GetInstance(Effect effect)
    {
        foreach (Pool pool in effectPools)
        {
            if (pool.poolID == effect.effectID)
            {
                return pool.GetInstance();
            }
        }

        return null;
    }

    public PoolInstance GetInstanceByID(WeaponInfo info, int ID)
    {
        foreach (Pool pool in weaponPools)
        {
            if (pool.poolID == info.itemIndex)
            {
                return pool.GetInstanceByID(ID);
            }
        }

        return null;
    }

    public void ReturnInstanceByID(WeaponInfo info, int ID)
    {
        foreach(Pool pool in weaponPools)
        {
            if(pool.poolID == info.itemIndex)
            {
                pool.ReturnInstanceByID(ID);
            }
        }
    }

    public int GetFreeInstanceID(WeaponInfo info)
    {
        foreach (Pool pool in weaponPools)
        {
            if (pool.poolID == info.itemIndex)
            {
                return pool.GetFreeInstanceID();
            }
        }

        return -1;
    }
    public void ReturnInstance(PoolInstance instance)
    {
        Pool[] pools;
        int poolID;

        if(instance.type == PoolInstance.PoolInstanceType.WeaponAmmo)
        {
            pools = weaponPools;
            poolID = instance.instance.GetComponent<Ammo>().weaponInfo.itemIndex;
        } else if(instance.type == PoolInstance.PoolInstanceType.Effect)
        {
            pools = effectPools;
            poolID = instance.instance.GetComponent<Effect>().effectID;
        } else
        {
            return;
        }

        foreach(Pool pool in pools)
        {
            if(pool.poolID == poolID)
            {
                pool.ReturnInstance(instance);
                break;
            }
        }
    }

    public void SyncInstance(int type, PoolInstance instance, Vector3 position, Quaternion rotation)
    {
        //type == 0; weaponInstance;
        //type == 1; effectInstance;
        if (!PhotonNetwork.IsMasterClient)
            return;

        int poolID;
        if(type == 0)
        {
            poolID = instance.instance.GetComponent<Ammo>().weaponInfo.itemIndex;
        } else if(type == 1)
        {
            poolID = instance.instance.GetComponent<Effect>().effectID;
        } else
        {
            return;
        }

        float[] posAndRot = new float[6] {position.x,position.y,position.z,
                                          rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z};
        pv.RPC("SyncOverNetwork", RpcTarget.All, type, poolID, instance.ID, instance.isFree, posAndRot);
    }

    [PunRPC] 
    void SyncOverNetwork(int poolType, int poolID, int instanceID, bool isFree, float[] positionAndRotation)
    {
        Vector3 position = new Vector3(positionAndRotation[0], positionAndRotation[1], positionAndRotation[2]);
        Quaternion rotation = Quaternion.Euler(positionAndRotation[3], positionAndRotation[4], positionAndRotation[5]);

        Pool pool = poolType == 0 ? weaponPools[poolID] : effectPools[poolID];

        pool.SetInstance(instanceID, isFree, position, rotation);
        
    }

   

    
}

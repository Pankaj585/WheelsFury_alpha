using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [SerializeField] GameObject missilePoolGameObject;
    [SerializeField] GameObject minePoolGameObject;

    Pool missilePool;
    Pool minePool;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> missiles = new List<GameObject>();
        List<GameObject> mines = new List<GameObject>();

        for (int i = 0; i < missilePoolGameObject.transform.childCount; i++)
        {
            missiles.Add(missilePoolGameObject.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < minePoolGameObject.transform.childCount; i++)
        {
            mines.Add(minePoolGameObject.transform.GetChild(i).gameObject);
        }

        missilePool = new Pool(missiles);
        minePool = new Pool(mines);
    }

    public GameObject GetMissile()
    {
        return missilePool.GetInstance();
    }

    public GameObject GetMine()
    {
        return minePool.GetInstance();
    }

    public void ReturnMissile(GameObject instance)
    {
        missilePool.ReturnInstance(instance);
    }

    public void ReturnMine(GameObject instance)
    {
        minePool.ReturnInstance(instance);
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

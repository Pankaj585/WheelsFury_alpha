using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileImpactEffect : Effect
{
    ParticleSystem particles;
    PoolManager poolManager;
    PoolInstance poolInstance;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    public void PlayEffect()
    {
        if (poolInstance == null)
            return;

        particles.Play();
        StopAllCoroutines();
        StartCoroutine(ReturnToPool());
    }

    public void SetPoolInstanceReference(PoolInstance instance)
    {
        poolInstance = instance;
    }

    IEnumerator ReturnToPool()
    {
        while (particles.isPlaying)
            yield return null;

        poolManager.ReturnInstance(poolInstance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockerEffect : Effect
{
    PoolManager poolManager;
    PoolInstance poolInstance;
    Vector3 maxScale = new Vector3(90, 2, 90);
    Timer timer = new Timer(0.1f);
    private void Awake()
    {
        poolManager = FindObjectOfType<PoolManager>();
    }

    public void PlayEffect()
    {
        if (poolInstance == null)
            return;

        StopAllCoroutines();
        StartCoroutine(ReturnToPoolAfterPlaying());
    }

    public void SetPoolInstanceReference(PoolInstance instance)
    {
        poolInstance = instance;
    }

    IEnumerator ReturnToPoolAfterPlaying()
    {
        Vector3 initialScale = new Vector3(1, 1, 1);
        timer.StartTimer();

        while (!timer.Tick(Time.deltaTime))
        {
            transform.localScale = Vector3.Lerp(initialScale, maxScale, timer.GetProgressValue());
            yield return null;
        }

        timer.ResetTimer();
        transform.localScale = maxScale;

        poolManager.ReturnInstance(poolInstance);
    }
}

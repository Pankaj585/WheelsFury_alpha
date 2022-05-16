using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Weapon
{
    [SerializeField] GameObject ExplosionFxPrefab = null;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosionFX = Instantiate(ExplosionFxPrefab, collision.contacts[0].point, Quaternion.identity);
        Destroy(explosionFX, 2f);
        Destroy(gameObject);
    }
}

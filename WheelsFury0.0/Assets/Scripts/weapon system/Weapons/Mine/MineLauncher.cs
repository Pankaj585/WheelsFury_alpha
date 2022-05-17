using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLauncher : WeaponLauncher
{
    private void Awake()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        itemHandler = transform.root.GetComponent<ItemHandler>();
        pool = FindObjectOfType<WeaponPool>();
    }

    public override void OnFireButtonDown()
    {

    }

    public override void OnFireButtonUp()
    {

    }
}

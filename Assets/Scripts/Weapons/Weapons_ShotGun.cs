using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_ShotGun : Weapons
{
    public int bulletAmount;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Shoot();
            }
        }
    }
}

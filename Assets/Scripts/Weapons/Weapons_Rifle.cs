using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Rifle : Weapons
{
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
}

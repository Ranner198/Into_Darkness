﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour
{

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
        {
            PlayerMovement.player.AddOxygen(150);
            Destroy(gameObject);
        }
    }
}


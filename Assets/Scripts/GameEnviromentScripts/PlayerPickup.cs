using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour {

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
        {
            PlayerMovement.player.AddAmmo(3);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PlayerMovement.player.AddAmmo(3);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public GameObject SpearGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SpearGun.GetComponent<SpearGun>().ammo += 100;
            Destroy(gameObject);
        }
    }
}
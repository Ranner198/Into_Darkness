using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenItems : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<OxygenSystem>().startingOxygen += 100;
            Destroy(gameObject);
        }
    }
}

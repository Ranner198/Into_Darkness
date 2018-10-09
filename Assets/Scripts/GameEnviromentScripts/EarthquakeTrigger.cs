using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            CameraShake earthquake = other.GetComponent<CameraShake>();
            earthquake.shakecamera(5, 1);
            Destroy(gameObject);
        }
    }
}

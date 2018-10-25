using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalVentTrigger : MonoBehaviour
{

    float timer = 10;
    bool timerCheck = false;

    void Update()
    {
        if (timerCheck)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            timerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            timerCheck = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            CameraShake earthquake = other.GetComponent<CameraShake>();
            earthquake.shakecamera();

            if (timer <= 0.0)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                health.TakeDamage(1);
                timer = 10;
            }
        }
    }
}
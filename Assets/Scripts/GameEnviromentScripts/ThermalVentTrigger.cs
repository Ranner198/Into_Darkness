using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalVentTrigger : MonoBehaviour
{

    double timer = 0.2;
    bool timerCheck = false;

    public GameObject Player;
    CameraShake cameraShakeScript;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraShakeScript = Player.transform.GetChild(2).GetComponent<CameraShake>();
    }

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
            cameraShakeScript.shakecamera();
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
            cameraShakeScript.shakecamera();

            if (timer <= 0.0)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                health.TakeDamage(5);
                timer = 0.2;
            }
        }
    }
}
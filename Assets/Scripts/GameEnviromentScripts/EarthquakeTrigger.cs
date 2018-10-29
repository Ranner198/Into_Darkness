using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeTrigger : MonoBehaviour
{

    float timer = 10;
    bool timerCheck = false;

    private bool isPlaying = false;
    public AudioClip earthquakeSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            timerCheck = true;
            audioSource.PlayOneShot(earthquakeSound, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            timer = 10;
            timerCheck = false;
            Destroy(gameObject);
            isPlaying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            CameraShake earthquake = other.GetComponent<CameraShake>();
            earthquake.shakecamera();

            if (timer <= 0)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                health.Death();
            }
        }
    }
}
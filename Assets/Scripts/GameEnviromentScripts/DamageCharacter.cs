using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacter : MonoBehaviour {

    int time = 0;

    public GameObject Player;

    CameraShake cameraShakeScript;

    public AudioClip earthquakeSound;
    public AudioSource audioSource;

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraShakeScript = Player.transform.GetChild(2).GetComponent<CameraShake>();

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
        {
            PlayerMovement.player.TakeDamage(5);
            cameraShakeScript.shakecamera();
            audioSource.PlayOneShot(earthquakeSound, 1f);
        }
        time = 0;
    }

    void OnTriggerStay(Collider coll)
    {
        
        if (coll.name == "Player")
        {
            time++;
            cameraShakeScript.shakecamera();

            audioSource.PlayOneShot(earthquakeSound, 1f);
            if (time % 20 == 0)
            {
                PlayerHealth health = coll.GetComponent<PlayerHealth>();
                health.TakeDamage(5);
            }
        }
    }
}

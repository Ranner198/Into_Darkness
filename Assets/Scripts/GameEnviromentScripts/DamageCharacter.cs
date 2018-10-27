using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacter : MonoBehaviour {

    int time = 0;

    public GameObject Player;

    CameraShake cameraShakeScript;

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraShakeScript = Player.transform.GetChild(2).GetComponent<CameraShake>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
        {
            PlayerMovement.player.TakeDamage(5);
            cameraShakeScript.shakecamera();
        }
        time = 0;
    }

    void OnTriggerStay(Collider coll)
    {
        
        if (coll.name == "Player")
        {
            time++;
            cameraShakeScript.shakecamera();
            if (time % 40 == 0)
            {
                PlayerMovement.player.TakeDamage(5);
            }
        }
    }
}

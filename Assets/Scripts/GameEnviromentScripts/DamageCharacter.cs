using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacter : MonoBehaviour {

    int time = 0;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Player")
            PlayerMovement.player.TakeDamage(5);
        time = 0;
    }

    void OnTriggerStay(Collider coll)
    {
        
        if (coll.name == "Player")
        {
            time++;
            if (time % 50 == 0)
                PlayerMovement.player.TakeDamage(5);
        }
    }
}

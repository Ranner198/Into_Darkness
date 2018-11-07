using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour {

    EnemyClass fish = new EnemyClass(1, 100);

    private GameObject player;
    private Terrain terrain; 

    public float speed;
    public bool runAway;

	void Start () {
        fish.SetSpeed(speed);
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
        terrain = FindObjectOfType<Terrain>();
	}
	
	void Update () {
        if (runAway)
            fish.Retreat(player, gameObject, terrain, 10f);
        else
            fish.Passive(player, gameObject, terrain, speed);
        

        if (transform.GetDistance(player) < 10)
            runAway = true;
        if (transform.GetDistance(player) > 20 && runAway)
            runAway = false;
    }
}

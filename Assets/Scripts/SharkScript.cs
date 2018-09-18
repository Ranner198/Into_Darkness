using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkScript : MonoBehaviour {

    EnemyClass shark = new EnemyClass(6, 100);

    public GameObject player;
  
    public Terrain terrain;

    public Transform targetPoints;

	void Start () {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();
	}
	
	void Update () {
        shark.Circle(player, gameObject, 30f, 0.8f);
        /*
        if (shark.GetState() == 0)
        {
            //passive
        }
        else if (shark.GetState() == 1)
        {
            //circleing
            shark.Circle(player, gameObject, terrain, 6f);
        }
        else if (shark.GetState() == 2)
        {
            //agressive/Attacking
            shark.Attack(player, gameObject, terrain, 6f);
        }
        else
        {
            //scared
        }

        

        //Timer Controller
        if (shark.GetTimer() <= 0)
        {
            shark.RandomTimer();
            shark.RandomState();
            print(shark.GetState());
            print(shark.GetTimer());
        }
        //Reset Timer
        if (shark.GetTimer() > 0)
            shark.CountDown();
            */
    }
}
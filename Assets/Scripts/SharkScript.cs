using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkScript : MonoBehaviour {

    EnemyClass shark = new EnemyClass(12, 100);

    public GameObject player;
  
    public Terrain terrain;

    public GameObject circleParent;
    public GameObject[] CircleArea;

    public Transform targetPoints;

    private bool startingCircle = false;

	void Start () {

        if (circleParent == null)
            circleParent = GameObject.FindGameObjectWithTag("CircleArea");

        for (int i = 0; i < CircleArea.Length; i++)
        {
            CircleArea[i] = circleParent.transform.GetChild(i).gameObject;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();
	}
	
	void Update () {
        shark.Circle(CircleArea, player, gameObject, 0.8f, startingCircle);
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
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

    private Animator anim;

	void Start () {

        anim = GetComponent<Animator>();

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


        if (shark.GetState() == 0)
        {
            //passive
            print("passive");
            //anim.Play();
        }
        else if (shark.GetState() == 1)
        {           
            //circleing
            shark.Circle(CircleArea, player, gameObject, 1f);
            print("Circle");
            //Just keep swimming
            //anim.Play();
        }
        else if (shark.GetState() == 2)
        {         
            //agressive/Attacking
            shark.Attack(player, gameObject, terrain, 6f);
            print("attacking");

            if (shark.DistanceFromPlayer(player, gameObject) < 3) {
                //Attack Anim
                //anim.Play();
            }
        }
        else if (shark.GetState() == 3)
        {
            //scared
            print("scared");
            shark.Retreat(player, gameObject, terrain, 6f);
            //Retreat
            //anim.Play();
        }

        Input.GetKeyDown(KeyCode.P);
        shark.SetState(2);

        //Timer Controller
        if (shark.GetTimer() <= 0)
        {
            shark.RandomTimer();
            shark.RandomState();
            //print(shark.GetState());
            //print(shark.GetTimer());
        }
        //Reset Timer
        if (shark.GetTimer() > 0)
            shark.CountDown();

    }
}
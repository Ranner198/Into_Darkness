using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkScript : MonoBehaviour
{

    EnemyClass shark = new EnemyClass(12, 100);

    public GameObject player;

    public Terrain terrain;

    public GameObject circleParent;
    public GameObject[] CircleArea;

    public Transform targetPoints;

    private bool startingCircle = false;

    private Animator anim;

    private int lastState = -1;

    private PlayerHealth playerHealth;

    public bool decisionTime = false;

    void Start()
    {
        CircleArea = new GameObject[4];
        //playerHealth = player.GetComponent<PlayerHealth>();

        shark.SetState(0);
        shark.GenerateAggro();
        print(shark.GetAggro());

        anim = GetComponent<Animator>();

        if (circleParent == null)
            circleParent = GameObject.FindGameObjectWithTag("CircleArea");

        for (int i = 0; i < CircleArea.Length; i++)
        {
            CircleArea[i] = circleParent.transform.GetChild(i).gameObject;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;

        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();
    }

    void Update()
    {
        transform.position = shark.StayOnTopOfTerrain(terrain, gameObject);
        print(shark.GetTimer());
        //print(shark.GetTimer());

        if (shark.GetState() == 0)
        {
            //passive
            shark.Passive(player, gameObject, terrain);
            print("passive");
            anim.Play("Passive");
        }
        else if (shark.GetState() == 1)
        {
            //circleing
            shark.Circle(CircleArea, player, gameObject, terrain, 1f);
            if (decisionTime == false)
            {
                shark.RandomTimer();
                decisionTime = true;
            }
            print("Circle");
            //Just keep swimming
            anim.Play("Passive");
        }
        else if (shark.GetState() == 2)
        {
            //agressive/Attacking
            shark.Attack(player, gameObject, terrain, 50f);
            print("attacking");

            if (shark.DistanceFromPlayer(player, gameObject) < 3)
            {
                //Attack Anim
                //anim.Play();              
                //playerHealth.TakeDamage(25);
            }
        }
        else if (shark.GetState() == 3)
        {
            //scared
            print("scared");
            shark.Retreat(player, gameObject, terrain, 6f);
            //Retreat
            anim.Play("Retreat");           
        }

        if (shark.DistanceFromPlayer(player, gameObject) < 20 && shark.GetState() == 0 && lastState != 1)
        {
            shark.SetState(1);
        }

        //Count Down
        if (shark.GetTimer() >= 0)
            shark.CountDown();

        //make a choice of wheather or not to attack
        if (shark.GetTimer() <= 0 && decisionTime)
        {
            lastState = shark.GetState();
            decisionTime = false;
            var aggro = shark.GetAggro();

            if (aggro >= 50)
            {
                shark.SetState(2);
            }
            else if (aggro < 50 && aggro > 25)
            {
                shark.SetState(0);
            }
            else if (aggro <= 25)
            {
                shark.SetState(3);
            }           
        }
        //Reset Timer

    }
}
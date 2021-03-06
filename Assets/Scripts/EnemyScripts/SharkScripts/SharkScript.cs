using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkScript : MonoBehaviour
{
    public EnemyClass shark = new EnemyClass(12, 100);

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
    public bool debugMode = false;

    void Start()
    {
        CircleArea = new GameObject[4];
        //playerHealth = player.GetComponent<PlayerHealth>();

        shark.SetState(0);
        shark.GenerateAggro();

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

        if (shark.GetState() == 0)
        {
            //passive
            shark.Passive(player, gameObject, terrain, 20);
            if(debugMode)
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
            if (debugMode)
                print("Circle");
            //Just keep swimming
            anim.Play("Passive");
        }
        else if (shark.GetState() == 2)
        {
            //agressive/Attacking
            shark.Attack(player, gameObject, terrain, 5f);
            if (debugMode)
                print("attacking");
            //playerHealth.TakeDamage(25);
            if (transform.GetDistance(player) < 17)
            {
                //Attack Anim
                anim.Play("Attack");
            }

            if (transform.GetDistance(player) < 7)
            {
                shark.SetState(3);
            }
        }
        else if (shark.GetState() == 3)
        {
            //scared
            if (debugMode)
                print("scared");
            shark.Retreat(player, gameObject, terrain, 6f);
            //Retreat
            anim.Play("Retreat");           
        }

        if (transform.GetDistance(player) < 30 && shark.GetState() == 0 && lastState != 1)
        {
            shark.SetState(1);
            //audioSource.PlayOneShot(Sound[1]);
        }
        if (transform.GetDistance(player) > 45 && shark.GetState() == 2)
        {
            shark.SetState(0);
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
    }
}

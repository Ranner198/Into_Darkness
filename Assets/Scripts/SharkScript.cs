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

    private int lastState;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();

        shark.RandomState();

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

    void Update()
    {

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
            print("Circle");
            //Just keep swimming
            anim.Play("Passive");
        }
        else if (shark.GetState() == 2)
        {
            //agressive/Attacking
            shark.Attack(player, gameObject, terrain, 6f);
            print("attacking");

            if (shark.DistanceFromPlayer(player, gameObject) < 3)
            {
                //Attack Anim
                //anim.Play();              
                playerHealth.TakeDamage(25);
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

        if (shark.DistanceFromPlayer(player, gameObject) < 25 && shark.GetState() == 0)
        {
            shark.SetState(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
            shark.SetState(2);

        //Timer Controller
        if (shark.GetTimer() <= 0)
        {
            lastState = shark.GetState();
            shark.RandomTimer();
            shark.RandomState();

            /*
            while (lastState != 1)
            {
                if (shark.GetState() == 2 || shark.GetState() == 3)
                {
                    shark.SetState(0);
                    return;
                }
                return;
            }
            */

            //print(shark.GetState());            
        }
        //Reset Timer
        if (shark.GetTimer() > 0)
            shark.CountDown();

    }
}
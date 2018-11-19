using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonsterScript : MonoBehaviour
{
    public EnemyClass monster = new EnemyClass(12, 100);

    public GameObject player;

    public Terrain terrain;

    public GameObject circleParent;
    public GameObject[] CircleArea;

    public Transform targetPoints;

    public float maxSpeed = 15;

    private bool startingCircle = false;
    private float timer;

    private Animator anim;

    private int lastState = -1;

    public bool decisionTime = false;
    public bool debugMode = false;

    private Rigidbody rb;

    public LayerMask lm;

    void Start()
    {
        lm = ~lm;
        CircleArea = new GameObject[4];

        monster.SetState(0);
        monster.GenerateAggro();

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

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 27, transform.position.z);
        //transform.position = monster.StayOnTopOfTerrain(terrain, gameObject);
        if (monster.GetState() == 0)
        {
            //passive
            monster.Passive(player, gameObject, terrain, 20, lm);
            if (debugMode)
                print("passive");
            anim.Play("Passive");
        }
        else if (monster.GetState() == 1)
        {
            //circleing
            monster.Circle(CircleArea, player, gameObject, terrain, 1f);
            if (decisionTime == false)
            {
                monster.RandomTimer();
                decisionTime = true;
            }
            if (debugMode)
                print("Circle");
            //Just keep swimming
            anim.Play("Passive");
        }
        else if (monster.GetState() == 2)
        {
            //agressive/Attacking
            monster.Attack(player, gameObject, terrain, 3.5f);
            if (debugMode)
                print("attacking");
        }
        else if (monster.GetState() == 3)
        {
            //scared
            if (debugMode)
                print("scared");

            monster.Retreat(player, gameObject, terrain, 8f, maxSpeed / 2, lm);
            //Retreat
            anim.Play("Retreat");
        }
        else if (monster.GetState() == 4)
        {
            monster.Stationary(gameObject);
        }

        if (transform.GetDistance(player) < 30 && monster.GetState() == 0)
        {
            //Start The Circle State
            monster.SetState(2);
        }

        if (transform.GetDistance(player) < 25 && monster.GetState() == 2)
        {
            anim.Play("Attack");
            StartCoroutine(BiteAndRun());
        }

        if (transform.GetDistance(player) < 3 && monster.GetState() == 4)
        {
            monster.SetState(4);
        }

        //Count Down
        if (monster.GetTimer() >= 0)
            monster.CountDown();

        //make a choice of wheather or not to attack
        if (monster.GetTimer() <= 0 && decisionTime)
        {
            lastState = monster.GetState();
            decisionTime = false;
            var aggro = monster.GetAggro();

            if (aggro >= 50)
            {
                monster.SetState(2);
            }
            else if (aggro < 50 && aggro > 25)
            {
                monster.SetState(0);
            }
            else if (aggro <= 25)
            {
                monster.SetState(3);
            }
        }

        //Limiters
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        if (rb.velocity.x < -maxSpeed)
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        if (rb.velocity.y > maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);
        if (rb.velocity.y < -maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, -maxSpeed, rb.velocity.z);
        if (rb.velocity.z > maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
        if (rb.velocity.z < -maxSpeed)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxSpeed);


        //if monster leaves bounds Delete it...
        if (Mathf.Abs(transform.position.x) > 200 || Mathf.Abs(transform.position.z) > 200)
        {
            DestroyImmediate(gameObject);
        }
    }


    IEnumerator BiteAndRun()
    {
        yield return new WaitForSeconds(2f);
        monster.SetState(4);
        anim.Play("Retreat");
        yield return new WaitForSeconds(.5f);
        decisionTime = false;
        monster.SetState(3);
        yield return new WaitForSeconds(8f);
        monster.SetState(0);
    }

    public IEnumerator Hit()
    {
        monster.SetState(3);
        yield return new WaitForSeconds(8f);
        decisionTime = false;
        monster.SetState(0);
    }
}

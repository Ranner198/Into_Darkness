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

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 27, transform.position.z);
        //transform.position = shark.StayOnTopOfTerrain(terrain, gameObject);
        if (shark.GetState() == 0)
        {
            //passive
            shark.Passive(player, gameObject, terrain, 20, lm);
            if (debugMode)
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
            shark.Attack(player, gameObject, terrain, 3.5f);
            if (debugMode)
                print("attacking");
        }
        else if (shark.GetState() == 3)
        {
            //scared
            if (debugMode)
                print("scared");

            shark.Retreat(player, gameObject, terrain, 8f, maxSpeed/2, lm);
            //Retreat
            anim.Play("Retreat");
        }
        else if (shark.GetState() == 4)
        {
            shark.Stationary(gameObject);
        }

        if (transform.GetDistance(player) < 30 && shark.GetState() == 0)
        {
            //Start The Circle State
            shark.SetState(2);
        }

        if (transform.GetDistance(player) < 25 && shark.GetState() == 2)
        {
            anim.Play("Attack");
            StartCoroutine(BiteAndRun());
        }

        if (transform.GetDistance(player) < 3 && shark.GetState() == 4)
        {
            shark.SetState(4);
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


        //if shark leaves bounds Delete it...
        if (Mathf.Abs(transform.position.x) > 200 || Mathf.Abs(transform.position.z) > 200)
        {
            DestroyImmediate(gameObject);
        }
    }


    IEnumerator BiteAndRun() {
        yield return new WaitForSeconds(2f);
        shark.SetState(4);
        anim.Play("Retreat");
        yield return new WaitForSeconds(.5f);
        decisionTime = false;
        shark.SetState(3);
        yield return new WaitForSeconds(8f);
        shark.SetState(0);
    }

    public IEnumerator Hit() {
        shark.SetState(3);
        yield return new WaitForSeconds(8f);
        decisionTime = false;
        shark.SetState(0);
    }
}
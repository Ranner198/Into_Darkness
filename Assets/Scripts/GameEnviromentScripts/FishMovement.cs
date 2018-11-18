using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public EnemyClass fish = new EnemyClass(12, 100);

    public GameObject player;

    public Terrain terrain;

    public GameObject circleParent;
    public GameObject[] CircleArea;

    public Transform targetPoints;

    private bool startingCircle = false;

    private int lastState = -1;

    public bool decisionTime = false;
    public bool debugMode = false;

    private Rigidbody rb;

    public LayerMask lm;

    void Start()
    {
        lm = ~lm;
        CircleArea = new GameObject[4];

        fish.SetState(0);
        fish.GenerateAggro();

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

    void Update()
    {
        //transform.position = shark.StayOnTopOfTerrain(terrain, gameObject);
        if (fish.GetState() == 0)
        {
            //passive
            fish.Passive(player, gameObject, terrain, 20, lm);
            if (debugMode)
                print("passive");
        }
        else if (fish.GetState() == 1)
        {
            //circleing
            fish.Circle(CircleArea, player, gameObject, terrain, 1f);
            if (decisionTime == false)
            {
                fish.RandomTimer();
                decisionTime = true;
            }
            if (debugMode)
                print("Circle");
            //Just keep swimming
        }
        else if (fish.GetState() == 2)
        {
            //scared
            if (debugMode)
                print("scared");
            //Retreat
        }

        if (transform.GetDistance(player) < 30 && fish.GetState() == 0)
        {
            //Start The Circle State
            fish.SetState(2);
        }

        if (transform.GetDistance(player) < 25 && fish.GetState() == 2)
        {
            //anim.Play("Attack");
            StartCoroutine(BiteAndRun());
        }

        //Count Down
        if (fish.GetTimer() >= 0)
            fish.CountDown();

        //if shark leaves bounds Delete it...
        if (Mathf.Abs(transform.position.x) > 200 || Mathf.Abs(transform.position.z) > 200)
        {
            DestroyImmediate(gameObject);
        }
    }

    IEnumerator BiteAndRun()
    {
        yield return new WaitForSeconds(1.15f);
        fish.SetState(5);
        yield return new WaitForSeconds(.5f);
        decisionTime = false;
        fish.SetState(3);
        yield return new WaitForSeconds(8f);
        fish.SetState(0);
    }

    public IEnumerator Hit()
    {
        fish.SetState(3);
        yield return new WaitForSeconds(8f);
        decisionTime = false;
        fish.SetState(0);
    }
}
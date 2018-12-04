using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass
{

    private int health, damage, stateSystem, nextPos = 0, aggro, setDegree = 0, stillHitting = 0, counter;
    private float speed, timer;
    private bool dead, CircleMode = false, AttackDir = false, setSteer = false, steer = false;
    public GameObject bossStage;
    public string currentState;

    //Attack Point
    Vector3 dir;
    //public int spawnPoints, targetPoints;

    public EnemyClass(float speed, int health)
    {
        //Set Variables on spawn
        SetSpeed(speed);
        SetHealth(health);
    }

    //Get tha speed
    public float GetSpeed()
    {
        return this.speed;
    }

    //Set tha Speed
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    //Get tha Health
    public int GetHealth()
    {
        return this.health;
    }

    //Set tha Health
    public void SetHealth(int health)
    {
        this.health = health;
    }

    //Take tha Damage
    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public void GenerateAggro()
    {
        this.aggro = Random.Range(50, 100);
    }

    public void SetAggro(int aggro)
    {
        this.aggro = aggro;
    }

    public int GetAggro()
    {
        return this.aggro;
    }

    //✔ tha Health 
    public bool isDead()
    {
        if (health <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
        return dead;
    }

    //Randomly pick the state
    public void RandomState()
    {
        this.stateSystem = Random.Range(0, 3);

        AttackDir = false;

        if (stateSystem == 0)
        {
            currentState = "passive";
        }
        else if (stateSystem == 1)
        {
            currentState = "circleing";
        }
        else if (stateSystem == 2)
        {
            currentState = "agressive";
        }
        else if (stateSystem == 3)
        {
            currentState = "Retreat";
        }
        else if (stateSystem == 4)
        {
            currentState = "Stationary";
        }
    }

    //Debug set the state
    public void SetState(int num)
    {
        this.stateSystem = num;
    }

    public int GetState()
    {
        return this.stateSystem;
    }

    //State Mechine change timer
    public void RandomTimer()
    {
        this.timer = Random.Range(0, 2);
    }

    //Get tha timer
    public float GetTimer()
    {
        return this.timer;
    }

    public void SetTimer(float timer)
    {
        this.timer = timer;
    }

    public void CountDown()
    {
        this.timer -= Time.deltaTime;
    }

    public void Passive(GameObject PlayerPos, GameObject shark, Terrain terrain, float speed, LayerMask lm)
    {
        //Put navmesh or something to control the shark whilst patroling to keep from crashing into terrain
        Rigidbody rb;
        rb = shark.GetComponent<Rigidbody>();
        //rb.velocity = -1 * Vector3.forward * speed * Time.deltaTime * 10;
        rb.AddRelativeForce(speed * Vector3.forward * speed * Time.deltaTime);

        Debug.DrawRay(shark.transform.position, shark.transform.forward * 8, Color.red);
        //Add a Steer Controller
        RaycastHit hit;
        bool hitStick = Physics.Raycast(shark.transform.position, shark.transform.forward, out hit, 8f, lm);

        if (hitStick)
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Terrain")
                steer = true;
        }
        else
        {
            stillHitting = 0;
        }

        if (steer)
            Steer(shark);      
    }
    public void PassiveFix(GameObject PlayerPos, GameObject shark, Terrain terrain, float speed)
    {
        //Put navmesh or something to control the shark whilst patroling to keep from crashing into terrain
        Rigidbody rb;
        rb = shark.GetComponent<Rigidbody>();
        //rb.velocity = -1 * Vector3.forward * speed * Time.deltaTime * 10;
        rb.AddRelativeForce(speed * Vector3.forward * speed * Time.deltaTime);

        Debug.DrawRay(shark.transform.position, shark.transform.forward * 8, Color.red);
        //Add a Steer Controller
        RaycastHit hit;
        bool hitStick = Physics.Raycast(shark.transform.position, shark.transform.forward, out hit, 8f);

        if (hitStick)
        {
            if (hit.collider.tag == "Terrain")
            {
                steer = true;
            }
        }
        else
        {
            stillHitting = 0;
        }

        if (steer)
            Steer(shark);
    }

    public void Steer(GameObject shark) {
        //If Passive
        if (GetState() == 0)
        {
            stillHitting++;
            if (!setSteer || stillHitting % 300 == 0)
            {
                setSteer = true;
                setDegree = Mathf.FloorToInt(shark.transform.eulerAngles.y);
                setDegree += PassiveTurnDegree();
            }
            shark.transform.rotation = Quaternion.Lerp(shark.transform.rotation, Quaternion.Euler(0, setDegree, 0), Time.deltaTime);

            if (shark.transform.rotation.y < setDegree - 3 && shark.transform.rotation.y > setDegree + 3)
                steer = false;
        }
        //If Retreating
        if (GetState() == 3)
        {
            stillHitting++;
            if (!setSteer || stillHitting % 300 == 0)
            {
                setSteer = true;
                setDegree = Mathf.FloorToInt(shark.transform.eulerAngles.y);
                setDegree += RetreatTurnDegree();
            }
            shark.transform.rotation = Quaternion.Lerp(shark.transform.rotation, Quaternion.Euler(0, setDegree, 0), Time.deltaTime);
            
            if (shark.transform.rotation.y < setDegree - 3 && shark.transform.rotation.y > setDegree + 3)
                steer = false;
        }

        //if (bossStage.activeInHierarchy == true)
        //{
        //    counter++;
        //}
    }

    public int PassiveTurnDegree() {
        int degree = Random.Range(130, 230);
        return degree;
    }

    public int RetreatTurnDegree() {
        int degree = Random.Range(70, 130);
        return degree;
    }

    //Retreat State Mode
    public void Retreat(GameObject PlayerPos, GameObject shark, Terrain terrain, float rotateSpeed, float maxSpeed, LayerMask lm)
    {
        AttackDir = false;
        dir = (shark.transform.GetDirection(PlayerPos)).normalized;
        dir.y = 0;
        Rigidbody rb;       
        rb = shark.GetComponent<Rigidbody>();
        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(-dir * speed * Time.deltaTime * 400);

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //rotate us over time according to speed until we are in the required rotation
        Vector3 HeadPos = new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y + 1.25f, PlayerPos.transform.position.z);

        Vector3 inverse = -dir;

        Quaternion lookRot = Quaternion.LookRotation(inverse * Mathf.Rad2Deg);
        shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRot, Time.deltaTime * rotateSpeed);

        /*
        //For Steering
        Debug.DrawRay(shark.transform.position, (inverse) * 7, Color.red);
        //Add a Steer Controller
        RaycastHit hit;
        bool hitStick = Physics.Raycast(shark.transform.position, inverse, out hit, 8f);

        if (hitStick)
        {
            if (hit.collider.tag == "Terrain")
                steer = true;
        }
        else
        {
            stillHitting = 0;
        }

        if (steer)
            Steer(shark);
        */
    }

    //Attack State
    public void Attack(GameObject PlayerPos, GameObject shark, Terrain terrain, float attackSpeed)
    {
        //Get the attack direction
        if (!AttackDir)
        {
            dir = shark.transform.GetDirection(PlayerPos).normalized;
            dir.y += .025f;
            AttackDir = true;
        }
       
        Rigidbody rb;
        rb = shark.GetComponent<Rigidbody>(); //Get Rigidbody ref

        float Distance = shark.transform.GetDistance(PlayerPos); //Find the distance from the player

        Quaternion lookRot = Quaternion.LookRotation(dir * Mathf.Rad2Deg);

        //rotate us over time according to speed until we are in the required rotation
        shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRot, Time.deltaTime * 3);

        rb.velocity = (dir * speed * Time.deltaTime * attackSpeed * 15);
    }
    public void Stationary(GameObject shark) {

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        if (rb.velocity.z > 1)
            rb.velocity -= new Vector3(0, -1, 0);

    }

    public void Circle(GameObject[] CircleArea, GameObject Player, GameObject shark, Terrain terrain, float rotateSpeed)
    {
        float Distance = (shark.transform.position - Player.transform.position).magnitude;

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime * 60);

        if (Distance < 20)
        {

            if (!CircleMode)
            {
                nextPos = StartCircle(CircleArea, shark);
            }

            float terrainHeight = terrain.SampleHeight(CircleArea[nextPos].transform.position) + 2;

            if (Player.transform.position.y > terrainHeight)
                terrainHeight = Player.transform.position.y;

            Vector3 sampledHeightPosition = new Vector3(CircleArea[nextPos].transform.position.x, terrainHeight, CircleArea[nextPos].transform.position.z);

            Vector3 Look_Towards_Next_Point = (sampledHeightPosition - shark.transform.position).normalized;

            //Debug Draw Ray - Show Direction
            Debug.DrawRay(shark.transform.position, Look_Towards_Next_Point * 9, Color.red);

            Quaternion lookRotation = Quaternion.LookRotation(Look_Towards_Next_Point * Mathf.Rad2Deg);

            //rotate us over time according to speed until we are in the required rotation
            shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            if (Mathf.FloorToInt((shark.transform.position - sampledHeightPosition).magnitude) < 3)
            {
                nextPos++;
                if (nextPos > CircleArea.Length - 1)
                    nextPos = 0;
                Debug.Log("Next Point.");
            }
            //Move Forward            
        }
        else
        {
            Vector3 dir = (Player.transform.position - shark.transform.position).normalized;

            CircleMode = false;
        }

    }
    public int StartCircle(GameObject[] CircleArea, GameObject shark)
    {

        CircleMode = true;

        int startPoint = 0;

        float distance = 999;

        for (int i = 0; i < CircleArea.Length; i++)
        {
            if ((CircleArea[i].transform.position - shark.transform.position).magnitude < distance)
            {
                distance = (CircleArea[i].transform.position - shark.transform.position).magnitude;
                startPoint = i;
            }
        }

        if (startPoint > CircleArea.Length - 1)
            startPoint = 0;

        return startPoint;
    }
    //Debuging Purposes
    public void SetNextPos(int nextPos)
    {
        this.nextPos = nextPos;
    }
}

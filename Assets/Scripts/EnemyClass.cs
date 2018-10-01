using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass
{

    private int health, damage, stateSystem, nextPos = 0, aggro;
    private float speed, timer;
    private bool dead, CircleMode = false, AttackDir = false;
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
        this.aggro = Random.Range(0, 100);
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
        this.timer = Random.Range(6, 25);
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

    private Vector3 GetDirection(GameObject player, GameObject shark)
    {
        return -(shark.transform.position - player.transform.position).normalized;
    }
    private Vector3 GetDirection(Vector3 loc, GameObject shark)
    {
        return -(shark.transform.position - loc).normalized;
    }

    public float DistanceFromPlayer(GameObject player, GameObject shark)
    {
        return (shark.transform.position - player.transform.position).magnitude;
    }

    public Vector3 StayOnTopOfTerrain(Terrain terrain, GameObject shark) {

        Vector3 sharkPos = shark.transform.position;

        float heightMap = terrain.SampleHeight(sharkPos) + 50;

        Vector3 returnVal = sharkPos;

        if (sharkPos.y > heightMap) {
            returnVal = Vector3.Slerp(shark.transform.position, new Vector3(shark.transform.position.x, heightMap, shark.transform.position.z), Time.deltaTime);
        }

        return returnVal;
    }

    public void Passive(GameObject PlayerPos, GameObject shark, Terrain terrain)
    {
        //Put navmesh or something to control the shark whilst patroling to keep from crashing into terrain
        Rigidbody rb;
        rb = shark.GetComponent<Rigidbody>();
        rb.velocity = -1 * Vector3.forward * speed * Time.deltaTime * 10;
    }

    public void Retreat(GameObject PlayerPos, GameObject shark, Terrain terrain, float rotateSpeed)
    {

        Vector3 dir = (GetDirection(PlayerPos, shark));

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        rb.velocity = (-dir * speed * Time.deltaTime * 15);

        Vector3 rot = (PlayerPos.transform.position - shark.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(rot * Mathf.Rad2Deg);

        //rotate us over time according to speed until we are in the required rotation
        shark.transform.rotation = Quaternion.Euler(-GetDirection(PlayerPos, shark));
    }

    public void Attack(GameObject PlayerPos, GameObject shark, Terrain terrain, float attackSpeed)
    {

        if (!AttackDir)
        {
            dir = GetDirection(PlayerPos, shark);
            AttackDir = true;
        }

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        float Distance = DistanceFromPlayer(PlayerPos, shark);

        float MapHypotnuse = Mathf.Sqrt(Mathf.Pow(terrain.terrainData.size.x, 2) + (Mathf.Pow(terrain.terrainData.size.z, 2)));

        //rotate us over time according to speed until we are in the required rotation
        Vector3 HeadPos = new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y + 1.25f, PlayerPos.transform.position.z);

        Vector3 fixDir = GetDirection(HeadPos, shark);

        Quaternion lookRot = Quaternion.LookRotation(fixDir * Mathf.Rad2Deg);

        //rotate us over time according to speed until we are in the required rotation
        shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRot, Time.deltaTime * 3);

        if (Distance < 2.5)
        {
            dir *= -1;
        }

        if (Distance > MapHypotnuse / 2)
        {
            rb.velocity = (-dir * speed * Time.deltaTime * attackSpeed * 15);
        }
        else
        {
            rb.velocity = (dir * speed * Time.deltaTime * attackSpeed * 15);
        }
    }
    /*
    public void Circle(GameObject PlayerPos, GameObject shark, float attackSpeed, float rotateSpeed)
    {
        Quaternion lookRotation;
        //To change based on gameplay mechs
        Vector3 dir = (PlayerPos.transform.position - shark.transform.position).normalized;

        //Debug Draw Ray - Show Direction
        Debug.DrawRay(shark.transform.position, dir * 9, Color.red);

        lookRotation = Quaternion.LookRotation(dir * Mathf.Rad2Deg);
        
        //rotate us over time according to speed until we are in the required rotation
        shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        if ((PlayerPos.transform.position - shark.transform.position).magnitude > 8)
            rb.AddForce(shark.transform.forward * speed * Time.deltaTime * 60);
        else
        {
            Vector3 offAngle = new Vector3(0, 1, 1);
            rb.AddRelativeForce(offAngle * speed * Time.deltaTime * 60);
        }
    }
    */

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

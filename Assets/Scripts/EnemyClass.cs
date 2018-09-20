using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass {

    private int health, damage, stateSystem, nextPos = 0;
    private float speed, timer;
    private bool dead, CircleMode = false;
    public string currentState;
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
    public void RandomState() {
        this.stateSystem = Random.Range(0, 3);

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
        else
        {
            currentState = "scared";
        }
    }

    //Debug set the state
    public void SetState(int num) {
        this.stateSystem = num;
    }

    public int GetState() {
        return this.stateSystem;
    }

    //State Mechine change timer
    public void RandomTimer() {
        this.timer = Random.Range(2, 25);
    }

    //Get tha timer
    public float GetTimer() {
        return this.timer;
    }

    public void SetTimer(float timer) {
        this.timer = timer;
    }

    public void CountDown() {
        this.timer -= Time.deltaTime;
    }

    public void Attack(GameObject PlayerPos, GameObject shark, Terrain terrain, float attackSpeed) {

        //To change based on gameplay mechs
        Vector3 dir = -(shark.transform.position - PlayerPos.transform.position).normalized;

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        float Distance = (shark.transform.position - PlayerPos.transform.position).magnitude;

        float MapHypotnuse = Mathf.Sqrt(Mathf.Pow(terrain.terrainData.size.x, 2) + (Mathf.Pow(terrain.terrainData.size.z, 2)));

        if (Distance > MapHypotnuse / 2)
        {
            rb.velocity = (-dir * speed * Time.deltaTime * 60);
        }
        else
        {
            rb.velocity = (dir * speed * Time.deltaTime * 60);
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
    public void Circle(GameObject[] CircleArea, GameObject Player, GameObject shark, float rotateSpeed)
    {
        float Distance = (shark.transform.position - Player.transform.position).magnitude;

        Rigidbody rb;

        rb = shark.GetComponent<Rigidbody>();

        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime * 60);

        if (Distance < 20) {
            
            if (!CircleMode)
            {
                nextPos = StartCircle(CircleArea, shark);
            }
            
            Vector3 Look_Towards_Next_Point = (CircleArea[nextPos].transform.position - shark.transform.position).normalized;

            //Debug Draw Ray - Show Direction
            Debug.DrawRay(shark.transform.position, Look_Towards_Next_Point * 9, Color.red);

            Quaternion lookRotation = Quaternion.LookRotation(Look_Towards_Next_Point * Mathf.Rad2Deg);

            //rotate us over time according to speed until we are in the required rotation
            shark.transform.rotation = Quaternion.Slerp(shark.transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);     

            if (Mathf.FloorToInt((shark.transform.position - CircleArea[nextPos].transform.position).magnitude) < 3) {
                nextPos++;
                if (nextPos > CircleArea.Length - 1)
                    nextPos = 0;
                Debug.Log("Next Point.");
            }
            //Move Forward            
        } else {
            Vector3 dir = (Player.transform.position - shark.transform.position).normalized;

            CircleMode = false;
        }

    }
    public int StartCircle(GameObject[] CircleArea, GameObject shark) {

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

        if (startPoint > CircleArea.Length-1)
            startPoint = 0;

        return startPoint;
    }
    //Debuging Purposes
    public void SetNextPos(int nextPos) {
        this.nextPos = nextPos;
    }
}

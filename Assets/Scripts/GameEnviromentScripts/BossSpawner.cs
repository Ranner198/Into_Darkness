using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public GameObject boss;
    public GameObject[] spawnPoints;

    public float timer = 5;
    public int chooser;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            chooser = Random.Range(0, spawnPoints.Length);

            GameObject SeaMonster = Instantiate(boss, spawnPoints[chooser].transform.position, Quaternion.identity);

            SeaMonster.name = "SeaMonster";
            SeaMonster.tag = "SeaMonster";

            Destroy(SeaMonster, 5);

            timer = 5;
        }
        
    }
}
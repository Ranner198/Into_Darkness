using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSpawner : MonoBehaviour
{

    public GameObject boss;
    public GameObject[] spawnPoints;

    public double timer = 7;
    public double extraTimer = 35;
    public int chooser;

    void Update()
    {
        timer -= Time.deltaTime;
        extraTimer -= Time.deltaTime;

        if (timer <= 0)
        {
            chooser = Random.Range(0, spawnPoints.Length);

            GameObject SeaMonster = Instantiate(boss, spawnPoints[chooser].transform.position, Quaternion.identity);

            SeaMonster.name = "SeaMonster";
            SeaMonster.tag = "Shark";

            Destroy(SeaMonster, 7);

            timer = 7;
        }

        if (extraTimer <= 0)
        {
            SceneManager.LoadScene("Winning");
        }
        
    }
}
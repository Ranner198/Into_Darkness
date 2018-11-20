using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{

    public GameObject videoPlayer;
    public GameObject player;
    public GameObject seaMonsterPrefab;
    public GameObject playerLocation;
    public GameObject bossLocation;
    public GameObject bossSpawner;

    private GameObject[] enemies;
    public float timer = 5;
    bool timerBegin;

    void Start()
    {
        videoPlayer.SetActive(false);
        bossSpawner.SetActive(false);
        timerBegin = false;
    }

    void Update()
    {
        if (timerBegin == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                player.transform.position = playerLocation.transform.position;
                GameObject seaMonster = Instantiate(seaMonsterPrefab, bossLocation.transform.position, Quaternion.identity);
                seaMonster.name = "SeaMonster";
                seaMonster.tag = "SeaMonster";
                timerBegin = false;
                bossSpawner.SetActive(true);
                DestroyAllEnemies();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sub")
        {
            timerBegin = true;
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timer);
        }
    }

    void DestroyAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Shark");

        for (var i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
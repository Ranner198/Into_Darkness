using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] spawnPoints;
    int chooser;

    void Start()
    {
        chooser = Random.Range(0, spawnPoints.Length);

        player.transform.position = spawnPoints[chooser].transform.position;
    }
}
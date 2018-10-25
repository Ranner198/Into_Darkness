using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeSpawner : MonoBehaviour {

    //prefabs to instantiate
    public GameObject earthquakePrefab;

    //location of the spawn points
    public GameObject[] spawnPoints;

    //variable to keep track of spawns and update index
    int spawnCounter = 0;

    void Start()
    {
        spawnItems();
    }

    void spawnItems()
    {

        while (spawnCounter < spawnPoints.Length - 1)
        {
            Instantiate(earthquakePrefab, spawnPoints[spawnCounter].transform.position, Quaternion.identity);
            spawnCounter++;
        }
    }
}
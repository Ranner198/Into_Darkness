using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconSpawnScript : MonoBehaviour {

    public GameObject beacon;

    public GameObject[] spawnLocations;

    public int index = -1;

    void Start()
    {
        random();   
    }

    void random() {

        index = Random.Range(0, spawnLocations.Length-1);

        Instantiate(beacon, spawnLocations[index].transform.position, Quaternion.identity);

    }
}

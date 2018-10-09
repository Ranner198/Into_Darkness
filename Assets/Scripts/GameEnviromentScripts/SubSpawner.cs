using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpawner : MonoBehaviour {

    //Add reference to put player far away from sub
    public Terrain terrain;
    public GameObject[] spawnPositions;
    public GameObject sub;
    public float heightOffset = 0;

    void Awake()
    {
        spawnPositions = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions[i] = transform.GetChild(i).gameObject;
        }
    }

    void Start () {
        var index = Random.Range(0, spawnPositions.Length - 1);

        float height = terrain.SampleHeight(spawnPositions[index].transform.position);
        Vector3 spawnLocation = new Vector3(spawnPositions[index].transform.position.x, height + heightOffset, spawnPositions[index].transform.position.z);
        GameObject Sub = Instantiate(sub, spawnLocation, Quaternion.identity);
        Sub.name = "Sub";
        Sub.tag = "Sub";

        return;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float maxHeight = 29.2f, minHeight = 21;

    public int spawnNum;

    public GameObject shark;

    public Terrain terrain;

    private float sizeX = 200, sizeY = 200;
    private Vector3 offset;

    private int i = 0;

    public Vector3 spawnPos;

    public GameObject where;


    void Update()
    {
        offset = terrain.transform.position;

        if (i < spawnNum)
        {
            
            Vector3 randomPos = new Vector3(Random.Range(-sizeX, sizeX), 45.6f, Random.Range(-sizeY, sizeY));
            float sampleHeight = terrain.SampleHeight(randomPos);
            
            while (sampleHeight < minHeight && sampleHeight > maxHeight)
            { 
                randomPos = new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeY, sizeY));                
                sampleHeight = terrain.SampleHeight(randomPos);                              
            }

            if (sampleHeight > minHeight && sampleHeight < maxHeight)
            {
                spawnPos = new Vector3(randomPos.x, sampleHeight, randomPos.z);
                Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject Shark = Instantiate(shark, spawnPos, randomRot);
                Shark.name = "Shark: " + i;
                i++;
            }            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private float sizeX = 200, sizeY = 200; //spawn location size
    private int i = 0; //spawn iterator

    public float maxHeight = 29.2f, minHeight = 21;
    public int spawnNum;
    public GameObject shark; //shark spawn prefab ref
    public GameObject player; //player ref
    public Terrain terrain; //terrain ref
    public Vector3 spawnPos; //location tester

    void Update()
    {

        if (i < spawnNum) //loop through n number of times until finished
        {
            //Generate a random height
            Vector3 randomPos = new Vector3(Random.Range(-sizeX, sizeX), 45.6f, Random.Range(-sizeY, sizeY));
            float sampleHeight = terrain.SampleHeight(randomPos); //get the terrain height

            float distance = transform.GetDistance(player);

            while (sampleHeight < minHeight && sampleHeight > maxHeight && distance < 60) //if the terrain doesn't fit the desiered height resample
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
        else
        {
            this.enabled = false;
        }
    }
}

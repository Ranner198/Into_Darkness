using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    private float sizeX = 200, sizeY = 200; //spawn location size
    private int i = 0; //spawn iterator

    public float maxHeight = 29.2f, minHeight = 21;
    public int spawnNum;
    public GameObject fish; //shark spawn prefab ref
    public GameObject player; //player ref
    public Terrain terrain; //terrain ref
    public Vector3 spawnPos; //location tester

    void Update()
    {

        if (i < spawnNum) //loop through n number of times until finished
        {
            //Generate a random height
            Vector3 randomPos = new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeY, sizeY));
            float sampleHeight = terrain.SampleHeight(randomPos); //get the terrain height

            float distance = Vector3.Distance(randomPos, player.transform.position);

            if (distance > 75 && sampleHeight < maxHeight && sampleHeight > minHeight)
            {
                spawnPos = new Vector3(randomPos.x, sampleHeight, randomPos.z);
                Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject Fish = Instantiate(fish, spawnPos, randomRot);
                Fish.name = "Fish: " + i;
                i++;
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}

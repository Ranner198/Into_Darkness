using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocations : MonoBehaviour
{

    public GameObject[] prefab;

    public GameObject[] Locations;

    [Tooltip("Input a number between 0-100")]
    public int spawnPercentage = 50;
    [Tooltip("Input a number between 0-100")]
    public int[] objectSpawnRate;

    private Terrain terrain;

    void Start()
    {

        int totalChildren = gameObject.transform.GetChild(0).childCount;
        Locations = new GameObject[totalChildren];
        for (int i = 0; i < totalChildren; i++)
        {
            Locations[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject;
        }

        terrain = GameObject.FindObjectOfType<Terrain>();

        //Dont worry about this it just limits the spawn rates to equal 100 no matter what someone enters which makes the spawning process easier
        var holder = 0;

        //Check total of the array size
        for (int i = 0; i < objectSpawnRate.Length; i++)
        {
            holder += objectSpawnRate[i];
        }

        //If the total is over 100
        if (holder > 100)
        {
            var tempvar = holder;
            var times = 0;
            while (tempvar > 100)
            {
                tempvar -= 100;
                times++;
            }

            var limiter = (holder - 100 * times) / 3;
            for (int i = 0; i < objectSpawnRate.Length; i++)
            {
                objectSpawnRate[i] -= Mathf.RoundToInt(limiter);
            }
        }
        else if (holder < 100) //If it is less than
        {
            float diff = 100 - holder;
            for (int i = 0; i < objectSpawnRate.Length; i++)
            {
                objectSpawnRate[i] += Mathf.RoundToInt(diff);
            }
        }

        //Remap Calculations to 100
        objectSpawnRate[1] += objectSpawnRate[0];
        objectSpawnRate[2] += objectSpawnRate[1];

        //Finished Calculations
        for (int i = 0; i < Locations.Length; i++)
        {
            //generate random spawn
            int willSpawn = (Random.Range(0, 100));
            //is spawn higher than the spawn percentage
            if (willSpawn > spawnPercentage)
            {
                Spawn(i);
            }
        }
    }

    void Spawn(int loc)
    {

        float sampleHeight = terrain.SampleHeight(Locations[loc].transform.position);

        Vector3 spawnCords = Locations[loc].transform.position;
        spawnCords.y = sampleHeight;

        var num = (Random.Range(0, 100));

        if (num > 0 && num <= objectSpawnRate[0])
        {
            GameObject ammo = Instantiate(prefab[0], spawnCords, Quaternion.identity);
            ammo.name = "Ammo Spawn";
            ammo.transform.parent = gameObject.transform;
        }
        else if (num > objectSpawnRate[0] && num < objectSpawnRate[1])
        {
            GameObject oxygen = Instantiate(prefab[1], spawnCords, Quaternion.identity);
            oxygen.name = "Oxygen Spawn";
            oxygen.transform.parent = gameObject.transform;
        }
        else if (num >= objectSpawnRate[1])
        {
            GameObject beacon = Instantiate(prefab[2], spawnCords, Quaternion.identity);
            beacon.name = "Beacon Spawn";
            beacon.transform.parent = gameObject.transform;
        }
        else
            Debug.Log("Error in Prefab Spawn, Spawn Number was:" + num + "\nThe Spawn Numbers were:" + "\n  Ammo: " + objectSpawnRate[0] +
                "\n  Oxygen: " + objectSpawnRate[1] + "\n  Beacon: " + objectSpawnRate[2]);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocations : MonoBehaviour {

    public GameObject[] prefab;

    public GameObject[] Locations;

    [Tooltip("Input a number between 0-100")]
    public int spawnPercentage = 50;
    public int objectSpawnRate = 50;

    private Terrain terrain;

	void Start () {

        int totalChildren = gameObject.transform.GetChild(0).childCount;
        Locations = new GameObject[totalChildren];
        for (int i = 0; i < totalChildren; i++)
        {            
            Locations[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject;
        }

        terrain = GameObject.FindObjectOfType<Terrain>();

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

    void Spawn(int loc) {

        float sampleHeight = terrain.SampleHeight(Locations[loc].transform.position);

        Vector3 spawnCords = Locations[loc].transform.position;
        spawnCords.y = sampleHeight;

        var num = (Random.Range(0, 100));

        if (num > objectSpawnRate) {
            GameObject ammo = Instantiate(prefab[0], spawnCords, Quaternion.identity);
            ammo.name = "Ammo Spawn";
            ammo.transform.parent = gameObject.transform;
        }
        else {
            GameObject oxygen = Instantiate(prefab[0], spawnCords, Quaternion.identity);
            oxygen.name = "Oxygen Spawn";
            oxygen.transform.parent = gameObject.transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocations : MonoBehaviour {

    public GameObject[] prefab;

    public GameObject[] Locations;

    [Tooltip("Input a number between 0-100")]
    public int spawnPercentage = 50;

	void Start () {
        for (int i = 0; i < Locations.Length; i++)
        {
            //generate random spawn
            int willSpawn = (Random.Range(-100, 0));
            //is spawn higher than the spawn percentage
            if (willSpawn > -spawnPercentage)
            {
                int RandomSpawn = (Random.Range(0, prefab.Length));
                //Instaitate here
                print("Spawn");
            }
        }
    }
}

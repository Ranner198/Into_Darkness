using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSharks : MonoBehaviour {

    public GameObject shark;
	
	void Start () {
        InvokeRepeating("SpawnShark", 5, 12);
	}

    void SpawnShark()
    {
        GameObject clone = Instantiate(shark, transform.position, transform.rotation);
        Destroy(clone, 35);
    }
}

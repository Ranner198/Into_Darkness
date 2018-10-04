using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSandbox : MonoBehaviour {

    public Terrain terrain;

    private float sizeX, sizeZ; 

    private void Start()
    {
        if (terrain == null)
            terrain = FindObjectOfType<Terrain>();
        sizeX = terrain.terrainData.size.x;
        sizeZ = terrain.terrainData.size.z;
    }

    void Update() {

        if (gameObject.transform.position.x > sizeX / 2)
            gameObject.transform.position = new Vector3((sizeX * -1) / 2, 0, gameObject.transform.position.z);
        if (gameObject.transform.position.x < -sizeX / 2)
            gameObject.transform.position = new Vector3((sizeX)/2, 0, gameObject.transform.position.z);
        if (gameObject.transform.position.z > sizeZ/2)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, (sizeZ * -1)/2);
        if (gameObject.transform.position.z < -sizeZ/2)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, (sizeZ)/2);
    }
}

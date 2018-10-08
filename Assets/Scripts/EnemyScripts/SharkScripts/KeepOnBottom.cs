using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnBottom : MonoBehaviour {

    private Terrain terrain;

	void Start () {
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
	}
	

	void Update () {
        float height = terrain.SampleHeight(transform.position);
        height++;
        float min = height - 2;
        float max = height + 2;

        if (transform.position.y > max || transform.position.y < min)
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, height, transform.position.x), Time.deltaTime);
	}
}

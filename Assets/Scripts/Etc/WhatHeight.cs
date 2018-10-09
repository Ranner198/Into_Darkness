using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatHeight : MonoBehaviour {

    public Terrain terrain;

    public float max = -1, min = 9999;

	// Update is called once per frame
	void Update () {
        float height = terrain.SampleHeight(transform.position);
        print(height);

        if (height > max)
            max = height;

        if (height < min)
            min = height;
	}
}

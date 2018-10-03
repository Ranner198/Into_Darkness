using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCount : MonoBehaviour {

    void Awake() {
        var numTriangles = gameObject.GetComponent<MeshFilter>().mesh.triangles.Length / 3;
        Debug.Log(numTriangles);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

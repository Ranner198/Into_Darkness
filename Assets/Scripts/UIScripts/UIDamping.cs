using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamping : MonoBehaviour {

    public GameObject helmet;

	void LateUpdate () {
        transform.position = helmet.transform.position;
        transform.rotation = helmet.transform.rotation;
	}
}

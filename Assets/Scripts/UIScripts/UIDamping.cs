using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamping : MonoBehaviour {

    public GameObject helmetPos;
    public GameObject helmetHolder;

	void LateUpdate () {
        transform.position = helmetPos.transform.position;
        transform.rotation = helmetHolder.transform.rotation;
	}
}

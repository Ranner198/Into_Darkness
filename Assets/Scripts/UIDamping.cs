using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamping : MonoBehaviour {

    public GameObject helmet;

	void Update () {
        transform.position = helmet.transform.position;

        //Vector2 rot = new Vector2(helmet.transform.rotation.x, helmet.transform.rotation.y);
        //Quaternion newRot = Quaternion.EulerAngles(rot.x, rot.y, 0);

        transform.rotation = helmet.transform.rotation;
	}
}

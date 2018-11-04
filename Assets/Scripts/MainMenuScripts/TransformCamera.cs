using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCamera : MonoBehaviour {

    public GameObject[] target;
    public static int currLocation= 0;
    public float speed = 1;

    private int newLoc = 0;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target[newLoc].transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, target[newLoc].transform.rotation, Time.deltaTime * speed);
    }

    public void ChangeLocation(int nextLocation)
    {
        newLoc = nextLocation;
    }

}

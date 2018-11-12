using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimBy : MonoBehaviour {

    public Transform waypoint;
    public float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        Vector3 dir = transform.GetDirection(waypoint.transform.position);

        rb.velocity = dir * Time.deltaTime * 60 * speed;
	}
}

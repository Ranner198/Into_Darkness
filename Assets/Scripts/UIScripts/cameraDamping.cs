using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDamping : MonoBehaviour {

    public GameObject player;
    public Camera _camera;
    public float damping = 0.5f;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        _camera = player.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        /*
        var holder = _camera.transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, holder, damping * Time.deltaTime);

        print("holder:" + holder);
        */

        transform.LookAt(_camera.transform);
    }
}

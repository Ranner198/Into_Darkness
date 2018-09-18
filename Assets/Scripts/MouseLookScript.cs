using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookScript : MonoBehaviour {

    Vector2 mouse;
    Vector2 smooth;
    public float sensitivity = 6.0f;
    public float damping = 2.0f;

    public float cameraDamping = 0.5f;

    GameObject player;



	void Start () {
        player = this.transform.parent.parent.gameObject;
	}
	

	void LateUpdate () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var dir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        dir = Vector2.Scale(dir, new Vector2(sensitivity * damping, sensitivity * damping));
        smooth.x = Mathf.Lerp(smooth.x, dir.x, 1f / damping);
        smooth.y = Mathf.Lerp(smooth.y, dir.y, 1f / damping);

        Mathf.Clamp(smooth.x, -1, 1);
        Mathf.Clamp(smooth.y, -1, 1);

        mouse += smooth;

        var holder = Quaternion.Euler( -mouse.y, mouse.x,0);

        //transform.localRotation = Quaternion.Lerp(holder, Quaternion.AngleAxis(-mouse.y, Vector3.right), cameraDamping * Time.deltaTime);
        player.transform.rotation = Quaternion.Lerp(transform.rotation, holder, cameraDamping * Time.deltaTime);
    }
}

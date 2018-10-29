using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerClass player = new PlayerClass(1.0f, 100, 3);

    public float speed = 6.0f, fallSpeed = 2.0f;
    private CharacterController cc;

	void Start () {
        player.SetSpeed(speed);
        cc = GetComponent<CharacterController>();
	}

    void Update()
    {
        Vector3 movement;

        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            movement = new Vector3(Input.GetAxis("OcculusMoveX") * speed, -fallSpeed, Input.GetAxis("OcculusMoveY") * speed);
        }
        else
        {
            movement = new Vector3(Input.GetAxis("Horizontal") * speed, -fallSpeed, Input.GetAxis("Vertical") * speed);
        }

        //Limit Speed
        movement.x = Mathf.Clamp(movement.x, -speed, speed);
        movement.z = Mathf.Clamp(movement.z, -speed, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);     
    }
}

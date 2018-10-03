using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerClass player = new PlayerClass(1.0f, 100, 3);

    public float speed = 6.0f, fallSpeed = 2.0f;
    private CharacterController cc;

    public CameraShake cs;

	void Start () {
        player.SetSpeed(speed);
        cc = GetComponent<CharacterController>();        
	}

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, -fallSpeed, Input.GetAxis("Vertical") * speed);

        //Limit Speed
        movement.x = Mathf.Clamp(movement.x, -speed, speed);
        movement.z = Mathf.Clamp(movement.z, -speed, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);
        if (Input.GetKey(KeyCode.Q))
            cs.shakecamera();
    }
}

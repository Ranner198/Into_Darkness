using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerClass player = new PlayerClass(1.0f, 100, 3);

    public float speed = 6.0f;
    private CharacterController cc;

	void Start () {
        player.SetSpeed(speed);
        cc = GetComponent<CharacterController>();        
	}
	
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);

        //Limit Speed
        movement = Vector3.ClampMagnitude(movement, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);
	}
}

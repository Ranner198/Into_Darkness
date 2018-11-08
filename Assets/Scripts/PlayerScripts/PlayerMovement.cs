using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerClass player = new PlayerClass(1.0f, 100, 3);
    public float speed = 3.0f, fallSpeed = 2.0f, jumpSpeed = 6.0f, gravity = 5.0f, jumpPos = 0;
    private CharacterController cc;
    private Vector3 movement = Vector3.zero;
    private bool jump = false; 

	void Start () {
        player.SetSpeed(speed);
        cc = GetComponent<CharacterController>();
	}

    void Update()
    {
        if (cc.isGrounded)
        {
            if (SpearGun.shootState)
                speed = 3.0f / 2;
            else
                speed = 3.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(JumpAdd());
            }
        } else {
            //speed = 2.0f;
        }

        //VR
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            movement += new Vector3(Input.GetAxis("OcculusMoveX") * speed, movement.y, Input.GetAxis("OcculusMoveY") * speed);
        }
        else
        {
            movement += new Vector3(Input.GetAxis("Horizontal") * speed, movement.y, Input.GetAxis("Vertical") * speed);
        }

        jumpPos -= gravity * Time.deltaTime;
        movement.y = jumpPos;
        //Limit Speed
        movement.y = Mathf.Clamp(movement.y, -gravity, 999);
        movement.x = Mathf.Clamp(movement.x, -speed, speed);
        movement.z = Mathf.Clamp(movement.z, -speed, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        cc.Move(movement);
    }

    IEnumerator JumpAdd() {
        yield return new WaitForSeconds(.1f);
        jumpPos = jumpSpeed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerClass player = new PlayerClass(1.0f, 100, 3);
    public float _speed = 3.0f, jumpForce = 6.0f, gravity = 5.0f, raycastLength = 3f;
    public LayerMask lm;
    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;
    private bool jump = false; 
    private float speed = 0;

	void Start () {
        player.SetSpeed(speed);
        rb = GetComponent<Rigidbody>();
        lm = ~lm;
        speed = _speed;
	}

    public void Update()
    {
        //Get Player Input
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            movement += new Vector3(Input.GetAxis("OcculusMoveX") * speed, 0, Input.GetAxis("OcculusMoveY") * speed);
        }
        else
        {
            movement += new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        }

        //Speed Holder for adjusting speed upon different conditions
        var speedHolder = _speed/4;

        //Raycast to ground
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.red);

        //If Grounded
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLength, lm))
        {
            //Make sure it's the ground
            if (hit.transform.tag == "Terrain")
            {
                //Speedholder should allow for full speed
                speedHolder = 0;

                //If the player jumps 'Space'
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce * Time.deltaTime * 60, rb.velocity.z);
                }
            }

            //If the player is grounded the speed will be full speed
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        } else {
            //If not grounded pull towards ground
            rb.AddForce(0, -gravity * Time.deltaTime * 60, 0);
        }

        //If the spear gun is out slow down player speed
        if (SpearGun.shootState)
            speed = _speed / 2 - speedHolder;
        else
            speed = _speed - speedHolder;

        //Add force if player is airborn
        rb.AddForce(movement);
    }
}

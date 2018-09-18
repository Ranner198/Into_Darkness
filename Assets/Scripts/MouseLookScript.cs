using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class MouseLookScript : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float damping;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float timeCount = 0.0f;

    public GameObject Player;
    public GameObject Helmet;

    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        yaw += speedH * Input.GetAxis("Mouse X")* Time.deltaTime * 60;
        pitch -= speedV * Input.GetAxis("Mouse Y")* Time.deltaTime * 60;

        var change = Quaternion.Euler(pitch, yaw, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, change, timeCount);

        Player.transform.rotation = Quaternion.Euler(0, yaw, 0.0f);

        Helmet.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0.0f), timeCount*2);
        Helmet.transform.position = gameObject.transform.position;

        timeCount += Time.deltaTime * damping;

        if (timeCount >= 0.2)
            timeCount = 0;

        print(timeCount);
    }
}

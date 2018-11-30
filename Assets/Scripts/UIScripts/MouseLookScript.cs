using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class MouseLookScript : MonoBehaviour
{
    public float helmentTurnSpeed = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float timeCount = 0.0f;

    public GameObject Player;
    public GameObject Helmet;

    void Awake()
    {
        pitch = Player.transform.rotation.x;
        yaw = Player.transform.rotation.y;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            Debug.Log("Rift Running");
            yaw += MouseSesitivityScript.sensitivity * Input.GetAxis("RightAnalogX") * Time.deltaTime * 25;

            Helmet.transform.rotation = Quaternion.Slerp(Helmet.transform.rotation, transform.rotation, Time.deltaTime * helmentTurnSpeed);
            Helmet.transform.position = transform.position;

            var change = Quaternion.Euler(pitch, yaw, 0.0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, change, Time.deltaTime);

            Player.transform.rotation = Quaternion.Euler(0, yaw, 0.0f);

            timeCount += Time.deltaTime;
        }
        else
        {
            yaw += MouseSesitivityScript.sensitivity * Input.GetAxis("Mouse X") * Time.deltaTime * 60;
            pitch -= MouseSesitivityScript.sensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime * 60;
            var change = Quaternion.Euler(pitch, yaw, 0.0f);
            //Lock camera bounds
            if (pitch > 90)
                pitch = 90;
            if (pitch < -90)
                pitch = -90;
            transform.rotation = Quaternion.Slerp(transform.rotation, change, Time.deltaTime);
            Player.transform.rotation = Quaternion.Euler(0, yaw, 0.0f);
            Helmet.transform.rotation = Quaternion.Slerp(Helmet.transform.rotation, change, Time.deltaTime * helmentTurnSpeed);        
            Helmet.transform.position = transform.position;
        }    
    }
}

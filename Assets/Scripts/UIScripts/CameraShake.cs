using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public GameObject camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public static bool shake = false;

    Vector3 originalPos;

    void Update()
    {
        if (shake)
        {
            if (shakeDuration > 0)
            {
                camTransform.transform.position = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 1f;
                camTransform.transform.position = originalPos;
                shake = false;
            }
        }
    }

    public void shakecamera()
    {
        shake = true;
        originalPos = camTransform.transform.position;
    }
}
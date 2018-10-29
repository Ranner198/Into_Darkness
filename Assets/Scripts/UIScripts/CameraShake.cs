using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public GameObject HelmetPlaceHolder, HelmentObject;
  
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
                HelmentObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 1f;
                HelmentObject.transform.localPosition = originalPos;
                shake = false;
            }
        }
    }

    public void shakecamera()
    {
        shake = true;
        originalPos = Vector3.zero;
    }
}
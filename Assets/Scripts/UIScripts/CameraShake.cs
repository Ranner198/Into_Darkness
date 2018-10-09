using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeDuration = 0f;

    public float shakeAmount = 1.0f;
    public float decreaseFactor = 1.3f;

    public bool shake = false;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shake)
        {
            if (shakeDuration > 0)
            {

                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;

            }
            else
            {

                shakeDuration = 1f;
                camTransform.localPosition = originalPos;
                shake = false;

            }
        }
    }

    public void shakecamera(float _shakeDuration, float _shakeAmount)
    {
        shake = true;
        shakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }
}
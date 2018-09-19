using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEditor : MonoBehaviour {

    private Light dirLight;

    private bool isPlaying;
     
    void Awake () {
        
        if (dirLight == null)
            dirLight = GetComponent<Light>();

        isPlaying = EditorApplication.isPlaying;

        print(dirLight.name);
        print(dirLight.intensity);

        if (!isPlaying)
            dirLight.enabled = true;
        else
            dirLight.enabled = false;
	}
}

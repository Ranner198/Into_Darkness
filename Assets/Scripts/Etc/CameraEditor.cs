using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEditor : MonoBehaviour {

    private Light dirLight;

    private bool isPlaying;
    public bool toggleScript;
    

    void Start () {
        if (dirLight == null)
            dirLight = GetComponent<Light>();

        isPlaying = EditorApplication.isPlaying;

        if (toggleScript)
        {
            if (!isPlaying)
                dirLight.enabled = true;
            else
                dirLight.enabled = false;
        }
	}

    public void UpdateToggle(bool toggle) {
        print("This script is: " + toggle);
        this.toggleScript = toggle;
    }
}

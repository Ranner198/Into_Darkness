using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSesitivityScript : MonoBehaviour {

    public static float sensitivity = 2f;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Mouse");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSensitivty(float val) {
        sensitivity = val;
    } 
}

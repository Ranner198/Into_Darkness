using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour {

    public GameObject spawn;

    void Update () {
        spawn.transform.position = gameObject.transform.position;
        spawn.transform.localScale += Vector3.one/3;

        if (spawn.transform.localScale.magnitude > 100)
            spawn.transform.localScale = Vector3.one;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonGoTo : MonoBehaviour {

    public GameObject UIElements; 

	void Update () {
        UIElements.transform.position = transform.position;
        UIElements.transform.rotation = transform.rotation;
	}
}

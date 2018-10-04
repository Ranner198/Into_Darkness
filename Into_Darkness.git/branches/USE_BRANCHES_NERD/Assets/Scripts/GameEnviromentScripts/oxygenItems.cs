using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<OxygenSystem>().startingOxygen += 100;
            Destroy(gameObject);
        }
    }
}

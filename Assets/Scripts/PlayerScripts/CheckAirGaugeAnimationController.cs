using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAirGaugeAnimationController : MonoBehaviour {

    private Animator anim;
    public static bool checkAirGuage = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!SpearGun.shootState)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.Play("CheckGauge");
                checkAirGuage = true;
            }
            else
            {
                checkAirGuage = false;
                anim.Play("CheckGaugen't");
            }
        }
	}
}

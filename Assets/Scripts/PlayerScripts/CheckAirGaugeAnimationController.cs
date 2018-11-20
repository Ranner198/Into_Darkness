using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAirGaugeAnimationController : MonoBehaviour {

    private Animator anim;
    private float frameCount;//Anim Frame Number
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
                if (frameCount < 1)
                {                    
                    frameCount += Time.deltaTime/2;
                }
                checkAirGuage = true;
            }
            else
            {
                if (frameCount > 0)
                {
                    anim.SetFloat("Direction", -1);
                    anim.Play("CheckGauge", 0, frameCount);
                    frameCount -= Time.deltaTime/2;
                }
                else
                {
                    checkAirGuage = false;
                    anim.Play("CheckGaugen't");
                }
            }
        }
    }
}

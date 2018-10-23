using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerShooting : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (SpearGun.shootState)
        {
            anim.Play("Shoot");
        }
        else
            anim.Play("Shootn't");
    }
}

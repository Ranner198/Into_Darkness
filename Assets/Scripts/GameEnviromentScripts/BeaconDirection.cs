using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconDirection : MonoBehaviour {

    private GameObject target, player;
    private Terrain terrain;
    private bool play;
    private GameObject mover;
    private SpriteRenderer SR;
    private int counter = 0;
    private Vector3 dir;

    public AudioClip beaconSound;
    AudioSource audioSource;

    void Start () {
        terrain = FindObjectOfType<Terrain>();
        player = GameObject.FindWithTag("Player").transform.GetChild(2).gameObject;
        mover = gameObject.transform.GetChild(0).gameObject;
        SR = mover.transform.GetChild(0).GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

	void Update () {

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Sub");
        else
        {
            mover.transform.LookAt(target.transform);
            mover.transform.position = Vector3.forward;
        }

        SR.enabled = play ? true : false;
    }

    private void OnTriggerStay(Collider coll)
    {
        counter++;
        if (coll.gameObject == player)
        {
            if (counter % 50 == 0)
            {
                play = !play;
                audioSource.PlayOneShot(beaconSound);
            }
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        counter = 0;
        if (coll.gameObject == player)
        {
            play = false;
            audioSource.Stop();
        }
    }
}

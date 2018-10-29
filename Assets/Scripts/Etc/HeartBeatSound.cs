using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSound : MonoBehaviour {

    public AudioClip Sound;
    public AudioSource audioSource;

    private bool isPlaying = false;
    private GameObject player;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        float distance = (transform.position - player.transform.position).magnitude;
        if (distance < 35 && !isPlaying)
        {
            isPlaying = true;
            audioSource.PlayOneShot(Sound, 4);
        }
        if (distance > 35 && isPlaying) 
        {
            isPlaying = false;
            audioSource.Stop();
        }
    }
}

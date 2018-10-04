using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSound : MonoBehaviour {

    public AudioClip[] Sound;
    public AudioSource audioSource;

    private bool isPlaying = false;
    private float timer, setter = 10;
    private GameObject player;

    public EnemyClass shark;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        float distance = (transform.position - player.transform.position).magnitude;
        if (distance < 35)
        {
            isPlaying = true;
            audioSource.PlayOneShot(Sound[0], 1.0f);
        }

        if (timer >= 0 && isPlaying)
            timer -= Time.deltaTime;

        if (timer < 0 && isPlaying)
        {   
            isPlaying = false;
            timer = setter;
        }
    }
}

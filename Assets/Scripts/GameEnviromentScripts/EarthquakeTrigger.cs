using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthquakeTrigger : MonoBehaviour
{
    private float timer = 7;
    private float loadTimer = 12;
    private bool timerCheck = false;
    private bool isPlaying = false;

    public GameObject warning;

    public AudioClip earthquakeSound;
    public GameObject videoPlayer;
    public AudioSource audioSource;

    private void Start()
    {
        videoPlayer.SetActive(false);
        warning.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timerCheck)
        {
            timer -= Time.deltaTime;
            loadTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            timerCheck = true;
            audioSource.PlayOneShot(earthquakeSound, 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            timer = 7;
            timerCheck = false;
            Destroy(gameObject);
            isPlaying = false;
            warning.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            CameraShake earthquake = other.GetComponent<CameraShake>();
            earthquake.shakecamera();

            warning.SetActive(true);

            if (timer <= 0)
            {
                videoPlayer.SetActive(true);
                warning.SetActive(false);
                Destroy(videoPlayer, 5);
                
                if (loadTimer <= 0)
                {
                    SceneManager.LoadScene("Game_Over_Scene");
                }
            }
        }
    }
}
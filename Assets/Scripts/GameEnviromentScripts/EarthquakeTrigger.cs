using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthquakeTrigger : MonoBehaviour
{
    private float timer = 10;
    private bool timerCheck = false;
    private bool isPlaying = false;

    public AudioClip earthquakeSound;
    public GameObject videoPlayer;
    public AudioSource audioSource;

    private void Start()
    {
        videoPlayer.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timerCheck)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            timerCheck = true;
            audioSource.PlayOneShot(earthquakeSound, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            timer = 10;
            timerCheck = false;
            Destroy(gameObject);
            isPlaying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            CameraShake earthquake = other.GetComponent<CameraShake>();
            earthquake.shakecamera();

            if (timer <= 0)
            {
                videoPlayer.SetActive(true);
                Destroy(videoPlayer, 5);
                Invoke("Death", 5f);
            }
        }
    }

    private void Death()
    {
        SceneManager.LoadScene("Game_Over_Scene");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenSystem : MonoBehaviour {

    public float startingOxygen = 100.0f;
    public Text oxygenText;
    public AudioClip deathClip;

    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerClass player = new PlayerClass(1.0f, 100, 3);

    void Awake()
    {
        //set components
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //set starting oxygen of 200
        player.SetOxygenLevel(startingOxygen);
        SetOxygenText();
    }

    void Update()
    {
        SetOxygenText();
        startingOxygen -= Time.deltaTime;
        player.SetOxygenLevel(startingOxygen);
        
        //playerAudio.Play();

        if (startingOxygen <= 0)
        {
            Death();
        }
    }

    void Death()
    {

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerMovement.enabled = false;
        SceneManager.LoadScene("GameOver");

    }

    void SetOxygenText()
    {
        oxygenText.text = "Oxygen: " + System.Math.Round(player.GetOxygenLevel(), 2) + "%";
    }
}
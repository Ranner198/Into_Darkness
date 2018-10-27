using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenSystem : MonoBehaviour {

    public Text oxygenText;
    public AudioClip deathClip;
    public float startingOxygen = 300;
    public float maxOxygen = 300;

    AudioSource playerAudio;

    void Start()
    {
        //set components
        playerAudio = GetComponent<AudioSource>();

        //set starting oxygen of 200
        PlayerMovement.player.SetOxygenLevel(startingOxygen);
        SetOxygenText();
    }

    void Update()
    {
        SetOxygenText();
        PlayerMovement.player.SubtractOxygen(Time.deltaTime);
        
        //playerAudio.Play();

        if (PlayerMovement.player.GetOxygenLevel() <= 0)
        {
            Death();
        }

        if (PlayerMovement.player.GetOxygenLevel() > maxOxygen)
            PlayerMovement.player.SetOxygenLevel(maxOxygen);
    }

    void Death()
    {

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        //playerMovement.enabled = false;
        SceneManager.LoadScene("GameOver");

    }

    void SetOxygenText()
    {
        oxygenText.text = "Oxygen: " + System.Math.Round(PlayerMovement.player.GetOxygenLevel(), 2) + "%";
    }
}
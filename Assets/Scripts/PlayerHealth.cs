using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthBar;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerClass player;
    bool dead;
    bool damaged;


    void Awake()
    {
        //set components
        player = GetComponent<PlayerClass>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //set starting health of 100
        player.SetHealth(startingHealth);
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int damage)
    {
        damaged = true;

        player.TakeDamage(damage);
        healthBar.value = player.GetHealth();

        playerAudio.Play();

        dead = player.isDead();

        if (dead)
        {
            Death();
        }
    }


    void Death()
    {
        dead = true;

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
    }
}
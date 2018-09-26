using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 100;
    public Image damageImage;
    public Text healthText;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerClass player = new PlayerClass(1.0f, 100, 3);
    bool dead;
    bool damaged;


    void Awake()
    {
        //set components
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //set starting health of 100
        player.SetHealth(startingHealth);
        SetHealthText();
    }


    void Update()
    {
        SetHealthText();

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

        //playerAudio.Play();

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
        //playerAudio.Play();

        playerMovement.enabled = false;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + player.GetHealth();
    }

    void OnTriggerEnter(Collider other)
    {
        TakeDamage(5);
        print("enter trigger with _player");
    }

    void OnTriggerExit(Collider other)
    {
        print("exit trigger with _player");
    }


}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
    bool dead;
    bool damaged;

    public bool debugMode = false;

    void Awake()
    {
        //set components
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //set starting health of 100
        PlayerMovement.player.SetHealth(startingHealth);
        SetHealthText();
    }

    void Update()
    {
        SetHealthText();

        if (damaged)
        {
            Color Opaque = new Color(1, 1, 1, 1);
            damageImage.color = Color.Lerp(damageImage.color, Opaque, 20 * Time.deltaTime);
            if (damageImage.color.a >= 0.8) //Almost Opaque, close enough
            {
                damaged = false;
            }
        }
        if (!damaged)
        {
            Color Transparent = new Color(1, 1, 1, 0);
            damageImage.color = Color.Lerp(damageImage.color, Transparent, 20 * Time.deltaTime);
        }

        if (PlayerMovement.player.GetHealth() <= 0)
        {
            Death();
        }

    }

    public void TakeDamage(int damage)
    {
        damaged = true;

        PlayerMovement.player.TakeDamage(damage);

        //playerAudio.Play();
    }


    public void Death()
    {
        dead = true;

        playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerMovement.enabled = false;

        SceneManager.LoadScene("Game_Over_Scene");
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + PlayerMovement.player.GetHealth();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shark")
        {
            TakeDamage(10);
            print("damage dealt");
        }

    }

}
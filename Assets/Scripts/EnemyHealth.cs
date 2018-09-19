using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthBar;
    public Image damageImage;
    public AudioClip deathClip;

    AudioSource enemyAudio;
    EnemyClass enemy;
    bool dead;
    bool damaged;


    void Awake()
    {
        //set components
        enemy = GetComponent<EnemyClass>();
        enemyAudio = GetComponent<AudioSource>();

        //set starting health of 100
        enemy.SetHealth(startingHealth);
    }


    void Update()
    {
    }


    public void TakeDamage(int damage)
    {
        damaged = true;

        enemy.TakeDamage(damage);
        healthBar.value = enemy.GetHealth();

        enemyAudio.Play();

        dead = enemy.isDead();

        if (dead)
        {
            Death();
        }
    }


    void Death()
    {
        dead = true;

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

    }
}
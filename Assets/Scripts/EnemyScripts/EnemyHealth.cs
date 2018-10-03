using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public AudioClip deathClip;

    AudioSource enemyAudio;
    EnemyClass enemy = new EnemyClass(12, 100);
    bool dead;
    bool damaged;


    void Awake()
    {
        //set components
        enemyAudio = GetComponent<AudioSource>();

        //set starting health of 100
        enemy.SetHealth(startingHealth);
    }

    public void TakeDamage(int damage)
    {
        damaged = true;

        enemy.TakeDamage(damage);
        //enemyAudio.Play();

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
        //enemyAudio.Play();

    }
}
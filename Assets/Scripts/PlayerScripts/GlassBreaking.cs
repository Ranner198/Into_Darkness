using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreaking : MonoBehaviour
{

    public Material mat;
    public Texture[] texture;

    public AudioClip glassSound;
    public AudioSource audioSource;

    void Start()
    {
        mat.mainTexture = texture[0];
    }

    void Update()
    {
        var health = PlayerMovement.player.GetHealth();

        if (health >= 100)
        {
            mat.mainTexture = texture[0];
        } else if (health <= 80 && health >= 60)
        {
            mat.mainTexture = texture[1];
            audioSource.PlayOneShot(glassSound, 2f);
        }
        else if (health <= 60 && health >= 40)
        {
            mat.mainTexture = texture[2];
            audioSource.PlayOneShot(glassSound, 2f);
        }
        else if (health <= 40 && health >= 20)
        {
            mat.mainTexture = texture[3];
            audioSource.PlayOneShot(glassSound, 2f);
        }
        else if (health <= 20)
        {
            mat.mainTexture = texture[4];
            audioSource.PlayOneShot(glassSound, 2f);
        }
    }
}
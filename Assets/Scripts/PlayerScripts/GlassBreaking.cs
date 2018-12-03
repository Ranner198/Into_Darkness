using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreaking : MonoBehaviour
{

    public Material mat;
    public Texture[] texture;

    public AudioClip glassSound;
    public AudioSource audioSource;
    public string textureName;
    public string previousTextureName;


    void Start()
    {
        mat.mainTexture = texture[0];
        textureName = mat.mainTexture.name;
        previousTextureName = textureName;
    }

    void Update()
    {
        var health = PlayerMovement.player.GetHealth();

        if (health >= 100)
            mat.mainTexture = texture[0];
        else if (health <= 80 && health >= 60)
            mat.mainTexture = texture[1];
        else if (health <= 60 && health >= 40)
            mat.mainTexture = texture[2];
        else if (health <= 40 && health >= 20)
            mat.mainTexture = texture[3];
        else if (health <= 20)
            mat.mainTexture = texture[4];

        textureName = mat.mainTexture.name;
        PlayAudio();
    }

    void PlayAudio()
    {
        if (previousTextureName != textureName)
        {
            audioSource.PlayOneShot(glassSound, 1f);
            previousTextureName = textureName;
        }
    }
}
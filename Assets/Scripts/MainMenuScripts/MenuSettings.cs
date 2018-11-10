using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MenuSettings : MonoBehaviour {

    public AudioMixer audiomixer;
    public float startingVolume = -25;

    private void Start()
    {
        audiomixer.SetFloat("Volume", -25);
    }

    public void VolumePercentage(float vol) {
        audiomixer.SetFloat("Volume", vol);
        Debug.Log(vol);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MenuSettings : MonoBehaviour {

    public AudioMixer audiomixer;

    public void VolumePercentage(float vol) {
        audiomixer.SetFloat("volume", vol);
        Debug.Log(vol);
    }

}

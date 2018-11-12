using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;

public class WaitForVideoIntro : MonoBehaviour {

    public float waitTime = 6;
    public TransformCamera cameraTransformer;
    public GameObject VideoPlayer;
    private VideoPlayer video;

    private void Start()
    {
        video = VideoPlayer.GetComponent<VideoPlayer>();
    }

    void Update () {
        if (waitTime >= 0)
            waitTime -= Time.deltaTime;

        if (waitTime < 0)
        {
            video.enabled = false;
            cameraTransformer.enabled = true;
            this.enabled = false;
        }   
	}
}

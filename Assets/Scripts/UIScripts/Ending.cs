using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {

    public GameObject endVideo;
    public GameObject finish;
    double timer = 9;

    // Use this for initialization
    void Start () {
        endVideo.SetActive(true);
        finish.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            endVideo.SetActive(false);
            finish.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("3DSceneTesting");
        }
    }
}

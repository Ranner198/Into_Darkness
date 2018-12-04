using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public double timer = 5;

	void Update () {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene("3DSceneTesting");
        }
    }
}

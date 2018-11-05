using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonEditor : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetButton("Jump") || Input.GetButton("Fire2"))
            SceneManager.LoadScene("Game_Scene");
    }
}

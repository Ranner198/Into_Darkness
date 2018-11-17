using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public bool gamePaused = false;

    public GameObject pauseMenuScreen;
    public GameObject optionsMenuScreen;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }

        }
	}

    void Pause()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        pauseMenuScreen.SetActive(false);
        optionsMenuScreen.SetActive(true);
    }

    public void Back()
    {
        pauseMenuScreen.SetActive(true);
        optionsMenuScreen.SetActive(false);
    }
}

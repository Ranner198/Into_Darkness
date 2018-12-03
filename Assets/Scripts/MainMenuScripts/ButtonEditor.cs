using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonEditor : MonoBehaviour {

    public GameObject credits;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
    }

}

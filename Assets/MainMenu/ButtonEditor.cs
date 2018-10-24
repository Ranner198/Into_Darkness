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

}

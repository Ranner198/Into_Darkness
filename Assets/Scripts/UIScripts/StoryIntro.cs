using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StoryIntro : MonoBehaviour
{

    public GameObject intro;

    void Update()
    {
        Time.timeScale = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            intro.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }
}
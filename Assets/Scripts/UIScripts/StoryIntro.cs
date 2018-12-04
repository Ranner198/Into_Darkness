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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            intro.SetActive(false);
        }
    }
}
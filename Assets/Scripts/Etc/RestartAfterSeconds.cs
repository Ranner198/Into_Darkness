using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartAfterSeconds : MonoBehaviour {

	void Start () {
        StartCoroutine(GoHome());
	}

    IEnumerator GoHome() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game_Scene");
    }
}

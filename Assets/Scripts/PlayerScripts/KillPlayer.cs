using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public GameObject Player;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject == Player)
        {
            SceneManager.LoadScene("Game_Over_Scene");
        }
    }
}